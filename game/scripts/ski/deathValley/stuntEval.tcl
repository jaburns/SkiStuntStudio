set ::bonusFrac 0.5
set ::failCom 0

set ::epsilon 0.1

proc reset {} {
    _reset 

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

    # check to see if the skier is in the end-zone
    set loc [currLoc]
    if {$loc == "end"} {
	set goalMet 1
    } elseif {$::skierState == $::out} {
	set vx [lindex $::currState 5]
	set vy [lindex $::currState 6]

	if [expr {[abs $vx] < $::epsilon} && {[abs $vy] < $::epsilon}] {
	    if {$::failCom == 0} {
		set ::failCom 1
		showFailMsg
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

set ::nextStageName camelSnowman

set ::goalText "Ski/Crash to the bottom of the hill!"

set ::hint [list "Keep your body low for stability!" \
	       "Get bonus points by doing extra tricks!"]

set ::unconsciousOk 1
