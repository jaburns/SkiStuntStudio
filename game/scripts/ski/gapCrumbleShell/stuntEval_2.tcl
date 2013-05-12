set ::flakyComFrac 0.5
set ::explosiveFrac 0.5

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
    if {[expr $::shellExploded && $::bridgeBroken]} {
	set ::noCrashComment 1
    } 

    _startUnconscious

    if {[expr {$::bridgeBroken} && {[currLoc] == "bridge"}]} {
	playComAtTimes $::soundDir/flakyBridge.wav $::flakyComFrac
    }

    if {$::shellExploded} {
	playComAtTimes $::soundDir/explosiveSki2.wav $::explosiveFrac
    }
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
	    if {$loc == "land"} {
		set goalMet 1
	    }
	}
    } 

    goal $goalMet
}

proc processJump {} {
    _processJump
}

set ::nextStageName camel

set ::goalText "Cross the bridge!"
set ::hint [list "Adjust your speed by braking." \
	       "Try kicking the shell."]
