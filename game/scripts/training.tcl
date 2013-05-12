#----------------------------------------------------------------------
# training.tcl
#   lists and startup training scenarios
#----------------------------------------------------------------------

# name of the different scenarios
set ::t_labels [list \
		    "Lesson   1" \
		    "Exercise 1" \
		    "Lesson   2" \
		    "Exercise 2" \
		    "Lesson   3" \
		    "Exercise 3" \
		    "Lesson   4" \
		    "Exercise 4"]

# commands to startup different scenarios
set ::t_stages [list \
		    "char_startPreview lesson1" \
		    "char_startPreview exercise1" \
		    "char_startPreview lesson2" \
		    "char_startPreview exercise2" \
		    "char_startPreview lesson3" \
		    "char_startPreview exercise3" \
		    "char_startPreview lesson4" \
		    "char_startPreview exercise4"]


#------------------------------
# loadTList - load the list of training scenarios
#------------------------------

proc loadTList {} {
    scenario_delete
    scenario_new $::LevelX [expr $::LevelY - 2] $::LevelSize \
	$::t_labels $::t_stages

    if {[info var ::PrevState] == {}} {
	scenario_select_cb 0
    }
}