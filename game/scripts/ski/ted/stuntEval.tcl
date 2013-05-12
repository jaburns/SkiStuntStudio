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

proc showGoal2 {} {
  gui add text 0.5 11 \
    "Design your own stunt terrain" \
    -size 12 -color 0.8 0.6 0.4 -shadow
  gui add text 3 9 \
    "Left Mouse:   repositions objects\nRight Mouse:  camera pan\nUp arrow:     zoom out\nDown arrow:   zoom in" \
    -size 12 -color 0.5 0.5 1.0 -shadow
  gui add text 3 4 \
    "Select 'Edit' from ESC menu to re-enter this mode" \
    -size 12 -color 0.5 0.5 1.0 -shadow
}

set ::hint [list "" ]
