#----------------------------------------------------------------------
# profile.tcl
# - module for loading and saving game profiles to disk
# - game profiles are stored in associative arrays
# - key of array = user name; value = level-completion profile
#----------------------------------------------------------------------

# default filename for the profile data file
set ::profile_fname "profile.dat"

#----------------------------------------------------------------------
# Help Routines
#----------------------------------------------------------------------

#----------------------------------------------------------------------
# routines for converting between arrays and lists
# - arrays are used for storing user profiles
# - lists are used for storing user profiles in files
#----------------------------------------------------------------------

#------------------------------
# arrayToList - convert an associative array into a list of pairs
#    a = name of array
#------------------------------

proc arrayToList { a } {
    set names [array names $a]
    set l {}

    foreach n $names {
	set pair [list $n [expr $$a\($n)]]
	lappend l $pair
    }

    return $l
}

#------------------------------
# listToArray - convert a list of pairs into an associative array
#     l = list of pairs
#     a = name of the associative array
#------------------------------

proc listToArray { l a } {
    foreach pair $l {
	set name [lindex $pair 0]
	set val  [lindex $pair 1]
	
	set $a\($name) $val
    }
}

#------------------------------
# newToOldVal - replace new completions (2s) with old completions (1s)
#------------------------------

proc newToOldVal { val } {
    set newVal {}

    foreach v $val {
	set newV {}

	foreach e $v {
	    if {$e == 2} {
		# replace 2's by 1's
		lappend newV 1
	    } else {
		lappend newV $e
	    }
	}

	lappend newVal $newV
    }

    return $newVal
}

#------------------------------
# newToOld - for each level, replace
#            all new completions (2s) with old completions (1s)
#------------------------------

proc newToOld { l } {
    set l2 {}

    foreach pair $l {
	set name [lindex $pair 0]
	set val  [lindex $pair 1]
	
	set newPair [list $name [newToOldVal $val]]
	lappend l2 $newPair
    }

    return $l2
}


#----------------------------------------------------------------------
# Module Routines
#----------------------------------------------------------------------

#------------------------------
# profile_load - load all user profiles from file into 
#                an array
#------------------------------

proc profile_load { fname arrayName } {
    set istr [open $fname r]
    set length [gets $istr l]
    listToArray $l $arrayName
    close $istr
}

#------------------------------
# profile_save - save user profiles into a file
#    final - if 0, then there's no need to convert all new-completions into
#            old ones; otherwise, do the conversion
#------------------------------

proc profile_save { fname arrayName final } {
    set ostr [open $fname w]
    set l [arrayToList $arrayName]

    # modify the profile entries: mark all "new completions" as 
    # "old completions"

    if {$final} {
	set lMod [newToOld $l]
	puts $ostr $lMod
    } else {
	puts $ostr $l
    }

    close $ostr
}

#------------------------------
# profile_lookup - lookup the profile for
#                  a given use
#------------------------------

proc profile_lookup { arrayName username } {
    set names [array names $arrayName]
    set i [lsearch $names $username]
    if {$i < 0} {
	return {}
    }
    return [expr $$arrayName\($username)]
}

#------------------------------
# profile_set - set the profile for a given user
#------------------------------

proc profile_set { arrayName username profile } {
    set $arrayName\($username) $profile
}

#------------------------------
# profile_openLevels - retrieve the names of the levels that
#                      are opened for a given user
#------------------------------

proc profile_openLevels { arrayName username levels } {
    set openLevels {}

    set profile [profile_lookup $arrayName $username]
    if {$profile != {}} {
	for {set i 0} {$i < [llength $profile]} {set i [expr $i + 1]} {
	    lappend openLevels [lindex $levels $i]
	}
    }

    return $openLevels
}

#------------------------------
# profile_getDbName - get the name of the associate array
#                     used for storing the user profiles
#------------------------------

proc profile_getDbName {} {
    return ::profile_allusers
}

proc profile_getFname {} {
    return $::profile_fname
}

proc profile_numScenarioComp { ls } {
    set c 0
    foreach s $ls {
	if {$s > 0} {
	    set c [expr $c + 1]
	}
    }
    return $c
}

#------------------------------
# profile_user - get the name of the users
#------------------------------

proc profile_users { arrayName } {
    return [array names $arrayName]
}

#------------------------------
# profile_newUser - create new user profile
#------------------------------

proc profile_newUser { arrayName username } {
    profile_set $arrayName $username [level_newUser [llength $::t_labels]]
}

#----------------------------------------------------------------------
# Test / Example code
#----------------------------------------------------------------------

# set ::a(default) [list {0} {1 2} {3 4 5}]
# set ::a(Cedric) [list {6} {7 8} {9 10 11}]
# profile_save $::profile_fname ::a
# 
# profile_load $::profile_fname ::b
# puts [array names ::b]
# puts "default = $::b(default)"
# puts "Cedric  = $::b(Cedric)"
