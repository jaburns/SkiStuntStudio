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
    set ::crateColCount [accumCol "Crate" 2 $::crateColCount]
}

proc processJump {} {
    _processJump
}

set ::nextStageName ravine

proc showGoal2 {} {
  gui add text 0.5 11 \
    "Avoid Crates - Jetpack" \
    -size 12 -color 0.9 0.1 0.1 -shadow
  gui add text 3 9 \
    "- Bindings are set to medium\n- Clear the crates to pass!" \
    -size 12 -color 0.0 0.0 0.0
}

set ::hint [list "" ]
