set ::snowmanPat 0
set ::tooBadCom 0

proc reset {} {
    _reset

    set ::snowmanPat 0
    set ::tooBadCom 0
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

    if {[expr {[currLoc] == "end"} && {$::snowmanPat > 0}]} {
	playComAtTimes $::soundDir/almost.wav 0.8
	set ::tooBadCom 1
    }
}

proc sideEffects { arg } {
    _sideEffects arg

    if {$::skierState == $::nextStage} { return }

    set goalMet 0
    if {$::skierState == $::onGround} {
	if {[onSkies $::currState]} {
	    set loc [currLoc]
	    if {$loc == "end"} {
		if {$::snowmanPat > 0} {
		    set goalMet 1
		} elseif {$::tooBadCom == 0} {
		    message "Almost!  You didn't touch the snowman though.\nPress <space> to retry."
		    set ::tooBadCom 1
		}
	    }
	}
    }
    goal $goalMet

    # state independent tests
    set ::snowmanPat [accumCol "Snowman" 0 $::snowmanPat]
}

set ::nextStageName road
set ::goalText "Touch the snowman and\nget to the end of the run!"
set ::hint [list "Be gentle with the snowman." \
		"The faster you go, the harder the collisions."]
