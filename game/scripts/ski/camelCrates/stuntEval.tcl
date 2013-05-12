set ::crateColCount 0
set ::tooBadCom 0

proc reset {} {
    _reset

    set ::crateColCount 0
    set ::tooBadCom     0
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
    if {[expr {[currLoc] == "dip3"} && {$::crateColCount == 0}]} {
	playCom $::soundDir/almost.wav 0
    }

    _startUnconscious
}

proc sideEffects { arg } {
    _sideEffects arg

    if {$::skierState == $::nextStage} { return }

    set goalMet 0

    if {$::skierState == $::onGround} {
	if {[onSkies $::currState]} {
	    set loc [currLoc]
	    if {$loc == "end"} {
		if {$::crateColCount == 0} {
		    set goalMet 1
		} else {
		    if {$::tooBadCom == 0} {
			message "Almost!  You touched the crates though.\nPress <space> to retry."
			set ::tooBadCom 1
		    }
		}
	    }
	} 
	
    }

    goal $goalMet

    # state independent tests
    set ::crateColCount [accumCol "Crate" 1 $::crateColCount]
}

proc processJump {} {
    _processJump
}

set ::nextStageName ravine
set ::goalText "Clear the crates!"
set ::hint [list "Use your brakes wisely." \
		"The harder you land, the more speed you loose." \
		"Approach the jump quickly for height and distance." \
		"Start jumping early."]
