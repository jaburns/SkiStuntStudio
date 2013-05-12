#----------------------------------------------------------------------
# beginner.tcl
#   lists and startup beginner scenarios
#----------------------------------------------------------------------

# name of the different scenarios
set ::e_labels [list \
		    "Ravine Board" \
		    "Mega Jump" \
		    "Cratejets" \
		    "Loopy" \
		    "Skill Course #1" \
                    "Long Jump #1" \
                    "Long Jump #2"]

# commands to startup different scenarios
set ::e_stages [list \
		    "char_startPreview extremeRavineBoard" \
		    "char_startPreview extremeMegaJump" \
		    "char_startPreview extremeCrateJet" \
		    "char_startPreview extremeLoop" \
		    "char_startPreview extremeSkill1" \
                    "char_startPreview extremeLongsteady" \
                    "char_startPreview extremeLongflip"]

#------------------------------
# loadBList - load the list of beginner scenarios
#------------------------------

proc loadEList {} {
    scenario_delete
    scenario_new $::LevelX [expr $::LevelY - 2] $::LevelSize \
	$::e_labels $::e_stages

    if {[info var ::PrevState] == {}} {
	scenario_select_cb 0
    }
}