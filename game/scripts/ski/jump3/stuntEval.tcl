set ::ff1 0
set ::ff2 0
set ::ff3 0
set ::failCom 0

proc reset {} {
    _reset 

    set ::ff1 0
    set ::ff2 0
    set ::ff3 0
    set ::failCom 0
}

proc startFlight {} {
    _startFlight
}

proc startLanding {} {
    _startLanding
}

proc startGround {} {
    _startGround
}

proc startUnconscious {} {
    _startUnconscious
}

proc sideEffects { arg } {
    _sideEffects arg

    if {$::skierState == $::nextStage} { return }

    set goalMet 0

    if {$::skierState == $::onGround} {
	if {[onSkies $::currState]} {
	    # check to see if the skier is in the end-zone
	    set x [lindex $::currState 2]
	    set y [lindex $::currState 3]
	    set loc [locMap map $x $y]
	    if {$loc == "end"} {
		if {$::ff1 == 0} { 
		    if {$::failCom == 0} {
			message "Awww... you missed the 1st jump.\nPress <space> to retry"
			set ::failCom 1
		    }
		} elseif {$::ff2 == 0} {
		    if {$::failCom == 0} {
			message "Oooo... you missed the 2nd jump.\\nPress <space> to retry"
			set ::failCom 1
		    }
		} elseif {$::ff3 == 0} {
		    if {$::failCom == 0} {
			message "Almost!... You missed the 3rd jump though.\\nPress <space> to retry"
			set ::failCom 1
		    }
		} else {
		    set goalMet 1
		}
	    }
	}
    } 

    goal $goalMet
}

proc processJump {} {
    _processJump

    set numRot   [lindex $::jumpInfo 1]
    set from     [lindex $::jumpInfo 2]
    set to       [lindex $::jumpInfo 3]
    set absNumRot [abs $numRot]

    if {$numRot < 0} {
	if {$from == "jump1"} {
	    if {$to == "land1"} {
		set ::ff1 1
	    }
	} elseif {$from == "jump2"} {
	    if {$to == "land2"} {
		set ::ff2 1
	    }
	} elseif {$from == "hill3"} {
	    if {$to == "hill3"} {
		set ::ff3 1
	    }
	}
    }
}

set ::nextStageName camelCrates

set ::goalText "A front flip for each of 3 jumps and\nget to the end of the run!"
set ::hint [list "Keep your body low for stability!" \
	       "Get bonus points by doing extra tricks!"]
