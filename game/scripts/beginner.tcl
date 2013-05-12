#----------------------------------------------------------------------
# beginner.tcl
#   lists and startup beginner scenarios
#----------------------------------------------------------------------

# name of the different scenarios
set ::b_labels [list \
		    "jump + 1F" \
		    "jump + 1B" \
		    "jump + 2F" \
		    "jump + 2B" \
		    "triple jump" \
                    "ravine jump" ]

# commands to startup different scenarios
set ::b_stages [list \
		    "char_startPreview jump; set ::stuntEval stuntEval_1.tcl" \
		    "char_startPreview jump; set ::stuntEval stuntEval_2.tcl" \
		    "char_startPreview jump; set ::stuntEval stuntEval_3.tcl" \
		    "char_startPreview jump; set ::stuntEval stuntEval_4.tcl" \
		    "char_startPreview jump3" \
                    "char_startPreview ravine" ]

#	    "char_startPreview test1"  "char_startPreview test2" ]

#------------------------------
# loadBList - load the list of beginner scenarios
#------------------------------

proc loadBList {} {
    scenario_delete
    scenario_new $::LevelX [expr $::LevelY - 2] $::LevelSize \
	$::b_labels $::b_stages

    if {[info var ::PrevState] == {}} {
	scenario_select_cb 0
    }
}