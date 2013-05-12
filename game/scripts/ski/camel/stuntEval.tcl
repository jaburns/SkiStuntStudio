set ::bonusFrac 0.5

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
		set goalMet 1
	    }
	}
    } 

    goal $goalMet
}

proc processJump {} {
    _processJump

    set numRot   [lindex $::jumpInfo 1]
    set absNumRot [abs $numRot]

    if {$absNumRot > 0} {
	playComAtTimes $::soundDir/bonus.wav  $::bonusFrac
    }
}

set ::nextStageName jump

proc changeStage {} {
    file copy -force $::scenarioDir/../$::nextStageName/stuntEval_1.tcl $::scenarioDir/../$::nextStageName/stuntEval.tcl
    _changeStage
}

set ::goalText "Get to the end of the run!"
set ::hint [list "Keep your body low for stability!" \
	       "Get bonus points by doing extra tricks!"]
