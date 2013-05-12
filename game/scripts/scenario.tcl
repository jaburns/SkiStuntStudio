#----------------------------------------------------------------------
# scenario
# - functions for selecting scenarios
#----------------------------------------------------------------------

# define index of first level to display in a long list of levels.
set ::scenarioBrowseIndex 0
set ::scenarioMaxIndex 11

# size and text location 
set ::comSize 14
set ::comX 18
#set ::comY [expr ($::screenHeight / $::comSize) - 4]
set ::comY 30
set ::comId [gui add text $::comX $::comY "" \
		 -size $::comSize \
		 -color 1 0.8 0 \
		 -shadow]

# only directly set the comment/scenario label once
if {[info var ::comTxt] == {}} {
    set ::comTxt ""
}

#------------------------------
# scenario_init - initialize the scenario selected
#------------------------------

proc scenario_init {} {
    set ::selectIdx -1
}

#------------------------------
# scenario_new - insert new scenarios into a vertical text list
#------------------------------

proc scenario_new { x y size labels stages } {
    set ::scenario_labels $labels
    set ::scenario_stages $stages

    vl_new scenario $x $y $labels $size scenario_select_cb 1 1 1 1 1 1 1 1 0 scenario_scrollDown_cb scenario_scrollUp_cb
}

#------------------------------
# scenario_delete - delete vertical list
#------------------------------

proc scenario_delete {} {
    vl_delete scenario
}

#------------------------------
# scenario_select_cb - callback to call when a scenario listed
#                      is selected
#------------------------------

proc scenario_select_cb { arg } {

    set txtVar [vl_txt scenario]
    if {[info var $txtVar] != {}} {
	set txt [expr $$txtVar]
        set txtID [lindex $txt [expr $arg + 1 - $::scenarioBrowseIndex]]
        foreach t $txt {
	    if {$t==$txtID} {
                gui eval $t setState Selected
            } else {
		gui eval $t setState Normal
	    }
        }
    }

    # stop any camera fly-bys
    camera stop

    # replace old comment
    set newCom [lindex $::scenario_labels $arg]
    gui eval $::comId setText $newCom
    set ::comTxt $newCom

    if {$::selectIdx != $arg} {
	set ::selectIdx $arg
    set stageCmd [lindex $::scenario_stages $arg]
	displayOn false
	eval $stageCmd	
	displayOn true
    }
    enableGo 1
}


#---------------------------------
#  When the scroll down button at the base of the list is clicked, this is called.
#  The scenarioBrowseIndex is incremented and the list of scenarios is destroyed and re-rendered.
#---------------------------------
proc scenario_scrollDown_cb {} {

	set numScenarios 0
	foreach i $::scenario_labels {
		set numScenarios [expr $numScenarios + 1]
	}
	
	if { [expr $numScenarios - $::scenarioBrowseIndex] > [expr $::scenarioMaxIndex + 1] } {
		set ::scenarioBrowseIndex [expr $::scenarioBrowseIndex + 1]
		level_cb $::levelIdx
	}
}

proc scenario_scrollUp_cb {} {
	if {$::scenarioBrowseIndex > 0} {
		set ::scenarioBrowseIndex [expr $::scenarioBrowseIndex - 1]
		level_cb $::levelIdx	
	}
}
