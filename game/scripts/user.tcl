#----------------------------------------------------------------------
# user.tcl
# - loads and saves the current user
#----------------------------------------------------------------------

# default filename for the file that contains the name of the 
# current user
set ::user_fname "user.dat"

# initialization
if {[info var ::user] == {}} {
    set ::user ""
}

#------------------------------
# user_default - set the user to the default user
#------------------------------

proc user_default {} {
    set ::user "default"
}

#------------------------------
# user_load - load the current user name from file
#------------------------------

proc user_load { fname } {
    set istr [open $fname r]
    set length [gets $istr l]
    set ::user $l
    close $istr
}

#------------------------------
# user_save - save the current user name in file
#------------------------------

proc user_save { fname } {
    set ostr [open $fname w]
    set l $::user
    puts $ostr $l
    close $ostr
}

#------------------------------
# user_set - set the current user's name
#------------------------------

proc user_set { name } {
    set ::user $name
}

#------------------------------
# user_getFname - get the user-data file's filename
#------------------------------

proc user_getFname {} {
    return $::user_fname
}
