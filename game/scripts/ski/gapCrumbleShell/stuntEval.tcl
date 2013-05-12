set ::flakyComFrac 0.5

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

    if {$::bridgeBroken} {
	playComAtTimes $::soundDir/flakyBridge.wav $::flakyComFrac
    }
}

proc sideEffects { arg } {
    _sideEffects arg

    if {$::skierState == $::nextStage} { return }

    set goalMet 0

    if {[expr $::shellExploded && $::bridgeBroken]} {
	set goalMet 1
    }

    goal $goalMet
}

proc goalReached {} {
    playCom $::soundDir/explosiveSki2.wav 1
    _goalReached
}

proc processJump {} {
    _processJump
}

set ::nextStageName jump3

set ::goalText "Explode the shell and break\nthe bridge!" 

set ::hint [list "Adjust your speed by braking."]
