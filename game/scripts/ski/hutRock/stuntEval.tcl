# Goal = to get to the end-zone (last valley in the terrain)

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

    set goalMet 0

    if {$::skierState == $::onGround} {
	# check to see if the skier is in the ravine
	set x [lindex $::currState 2]
	set y [lindex $::currState 3]
	set loc [locMap map $x $y]
	if {$loc == "dip2"} {
	    set goalMet 1
	}
    }

    goal $goalMet
}

proc processJump {} {
    _processJump
}

set ::nextStageName ravineJet
set ::goalText "Get to the end of the run!"
set ::hint [list "Crossing the ravine is easiest via front flips." \
	       "Don't go crazy with flips!" ]

