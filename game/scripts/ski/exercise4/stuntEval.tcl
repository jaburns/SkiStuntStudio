set ::jumpPerf 0

proc reset {} {
    _reset

    set ::jumpPerf 0
}

proc sideEffects { arg } {
    _sideEffects $arg

    if {$::skierState == $::nextStage} { return }

    set goalMet 0

    if {$::skierState == $::onGround} {
	if {[onSkies $::currState]} {
	    if {$::jumpPerf} {
		set goalMet 1
	    }
	}
    } 

    goal $goalMet
}

proc processJump {} {
    _processJump

    set numRot   [lindex $::jumpInfo 1]

    if {$numRot > 0} {
	set ::jumpPerf 1
    }
}

set ::goalText "Do a back flip!"
set ::hint [list "Start the flip with a\\nlow forward lean" \
	       "The faster you lean backward,\\nthe faster you spin" \
	       "Tuck (curl up) to spin faster" \
	       "Extend (stretch out) to spin slower"]

