#----------------------------------------------------------------------
# profileSetup.tcl
# - create a clean user profile with a single user (default) with
#   only the training level opened
#----------------------------------------------------------------------

# load related modules
source training.tcl
source beginner.tcl
source intermediate.tcl
source pro.tcl
source level.tcl
source profile.tcl
source user.tcl

set dbName [profile_getDbName]
if {[info var $dbName] != {}} {
    unset $dbName
}

# open up the beginner's level only
level_reset
level_init [list [llength $::t_labels]]

# initialize the profile
profile_set $dbName default [level_getComp]

# save profile to file
profile_save [profile_getFname] $dbName 1

# save current user name
user_set default
user_save [user_getFname]
