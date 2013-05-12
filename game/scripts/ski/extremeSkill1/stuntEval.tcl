set ::jumpPerf 0
set ::failCom 0


proc reset {} {
    _reset

    set ::jumpPerf 0
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
	    #message "$loc"
	    if {$loc == "end"} {
		set goalMet 1
		
	    }
	}
    #} 

    goal $goalMet
}

proc processJump {} {
    _processJump

}

set ::nextStageName jump

proc changeStage {} {
    _changeStage
}

proc showGoal2 {} {
  gui add text 0.5 11 \
    "Skill Course #1" \
    -size 12 -color 0.9 0.1 0.1 -shadow
  gui add text 3 9 \
    "- Bindings are set to medium\n- Reach end to win\n- Must stabalizing final landing to win" \
    -size 12 -color 0.0 0.0 0.0
}

set ::hint [list "" ]
