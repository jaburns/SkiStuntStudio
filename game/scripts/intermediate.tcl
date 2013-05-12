#----------------------------------------------------------------------
# intermediate.tcl
#   lists and startup intermediate scenarios
#----------------------------------------------------------------------

# name of the different scenarios
set ::i_labels [list \
		    "hut jump" \
                    "stack of crates" \
		    "tree skiing" \
                    "custom stunt"]

# commands to startup different scenarios
set ::i_stages [list \
                    "char_startPreview hutRock" \
                    "char_startPreview camelCrates" \
		    "char_startPreview camelTrees" \
		    "char_startPreview ted"]

#------------------------------
# loadIList - load the list of intermediate scenarios
#------------------------------

proc loadIList {} {
    scenario_delete
    scenario_new $::LevelX [expr $::LevelY - 2] $::LevelSize \
	$::i_labels $::i_stages

    if {[info var ::PrevState] == {}} {
	scenario_select_cb 0
    }
}