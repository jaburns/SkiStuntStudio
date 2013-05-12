#----------------------------------------------------------------------
# level.tcl
# - control the loading, saving, managing and displaying of
#   completed levels
#----------------------------------------------------------------------

# color for new-completion check-marks
set ::NewComp_r 1
set ::NewComp_g 0.8
set ::NewComp_b 0

# color for old-completion check-marks
set ::OldComp_r 0
set ::OldComp_g 0.7
set ::OldComp_b 0.4

# deferred declaration of ::LevelComp
# set ::LevelComp {}

# GUI object IDs for the levels-completed check-marks
set ::LevelCompId {}

#------------------------------
# level_reset - reset the levels completed
#------------------------------

proc level_reset {} {
    if {[info var ::LevelComp] != {}} { 
	unset ::LevelComp
    }
}

#------------------------------
# level_init - initialize the level of completion
#   lDepths = list that contains the number of sub-levels contained in each
#             level
#------------------------------

proc level_init { lDepths } {
    if {[info var ::LevelComp] != {}} { return }

    set ::LevelComp {}

    foreach d $lDepths {
	set l [empty_level $d]
	lappend ::LevelComp $l
    }
}

#------------------------------
# level_set - set the completion status of a sublevel to the given value
#   l  = level index (starts from 0)
#   sl = sublevel index (starts from 0)
#   v  = new value
#------------------------------

proc level_set {l sl v} {
    set lc [lindex $::LevelComp $l]
    set lc [lreplace $lc $sl $sl $v]
    set ::LevelComp [lreplace $::LevelComp $l $l $lc]
}


#------------------------------
# level_draw - draw the scenarios in a given
#              level
#   l = level index
#------------------------------

proc level_draw { l } {
    set lc [lindex $::LevelComp $l]

    set ::LevelCompId {}

    set x [expr $::LevelX - 1.5]
    set y [expr $::LevelY - 2]
    foreach sl $lc {
	if {$sl == 1} {
	    set id [gui add text $x $y $::CheckMark \
			-size $::LevelSize \
			-color $::OldComp_r $::OldComp_g $::OldComp_b \
			-shadow]
	    lappend ::LevelCompId $id
	} elseif {$sl == 2} {
	    set id [gui add text $x $y $::CheckMark \
			-size $::LevelSize \
			-color $::NewComp_r $::NewComp_g $::NewComp_b \
			-shadow]
	    lappend ::LevelCompId $id
	}

	set y [expr $y - 1]
    }
}

#------------------------------
# level_hide - hide the level completed check-marks
#------------------------------

proc level_hide {} {
    foreach id $::LevelCompId {
	gui del $id
    }
    set ::LevelCompId {}
}

#------------------------------
# empty_level - generate data struct for an empty
#               level with N scenarios
#------------------------------

proc empty_level { nScenario } {
    set l {}
    for {set i 0} {$i < $nScenario} {set i [expr $i + 1]} {
	lappend l 0
    }
    return $l
}

#------------------------------
# level_getComp - retrieve the levels completed
#------------------------------                

proc level_getComp {} {
    if {[info var ::LevelComp] != {}} {
	return $::LevelComp
    }
    return {}
}

#------------------------------
# level_setComp - set the levels completed
#------------------------------

proc level_setComp { lc } {
    set ::LevelComp $lc
}


#------------------------------
# level_newUser - create data structure for
#                 a new user; open up only the
#                 very first level (training level)
#    td = number of scenarios in the training level
#------------------------------

proc level_newUser { td } {
    set l [list [empty_level $td]]
    return $l
}

#----------------------------------------------------------------------
# test driver for level.tcl
#----------------------------------------------------------------------

# < level.tcl
# 
# set lDepths {}
# lappend lDepths [llength $::b_labels]
# lappend lDepths [llength $::i_labels]
# lappend lDepths [llength $::p_labels]
# 
# level_init $lDepths
# 
# level_set 0 0 1
# level_set 0 1 1
# level_set 0 3 2
# 
# level_draw 0
