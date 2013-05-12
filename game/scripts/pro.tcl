#----------------------------------------------------------------------
# pro.tcl
#   lists and startup professional scenarios
#----------------------------------------------------------------------

# name of the different scenarios
set ::p_labels [list \
		    "road" \
		    "death valley" \
                    "snowman" \
		    "ravine + jetpack" \
		    "collapsing bridge"]


# commands to startup different scenarios
set ::p_stages [list \
		    "char_startPreview road" \
		    "char_startPreview deathValley" \
                    "char_startPreview camelSnowman" \
		    "char_startPreview ravineJet" \
		    "char_startPreview gapCrumbleShell; set ::stuntEval stuntEval_2.tcl"]


#------------------------------
# loadPList - load the list of pro scenarios
#------------------------------

proc loadPList {} {
    scenario_delete
    scenario_new $::LevelX [expr $::LevelY - 2] $::LevelSize \
	$::p_labels $::p_stages
 
    if {[info var ::PrevState] == {}} {
	scenario_select_cb 0
    }
}