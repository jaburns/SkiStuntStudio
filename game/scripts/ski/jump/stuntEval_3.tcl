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
	    if {$loc == "end"} {
		if {$::jumpPerf} {
		    set goalMet 1
		} else {
		    if {$::failCom == 0} {
			message "You missed the double front.\nPress <space> to retry."
			set ::failCom 1
		    }
		} 
	    }
	}
    } 

    goal $goalMet
}

proc processJump {} {
    _processJump

    set numRot   [lindex $::jumpInfo 1]
    set from     [lindex $::jumpInfo 2]
    set to       [lindex $::jumpInfo 3]
    set absNumRot [abs $numRot]

    puts "numRot = $numRot"

    if {$numRot < -1} {
	if {[expr {$from == "jump"} && {$to == "land"}]} {
	    set ::jumpPerf 1
	}
    }
}

set ::nextStageName jump

proc changeStage {} {
    file copy -force $::scenarioDir/stuntEval_4.tcl $::scenarioDir/stuntEval.tcl
    _changeStage
}

set ::goalText "Double front off the jump!"
set ::hint [list "Keep your body low for stability!" \
	       "Get bonus points by doing extra tricks!"]
