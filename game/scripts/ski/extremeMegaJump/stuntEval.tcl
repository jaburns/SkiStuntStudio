set ::jumpPerf 0
set ::failCom 0

set ::totalAirTime 0

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
		if {$::jumpPerf} {
		    set goalMet 1
		} else {
		    if {$::failCom == 0} {
			message "You missed the flip.\nPress <space> to retry."
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
#    message $::lastfy

    set vx [lindex $::currState 5]
    #message "$vx $::lastfy"

    #set time [lindex $::jumpInfo 0]
    #set ::totalAirTime [expr $::totalAirTime + $time]
    #message $::totalAirTime
    set numRot   [lindex $::jumpInfo 1]
    set from     [lindex $::jumpInfo 2]
    set to       [lindex $::jumpInfo 3]
    set absNumRot [abs $numRot]

    if {$numRot < 3} {
	if {[expr {$from == "jump"} && {$to == "land"}]} {
	    set ::jumpPerf 1
	}
    }
}

set ::nextStageName jump

proc changeStage {} {
    _changeStage
}

proc showGoal2 {} {
  gui add text 0.5 11 \
    "Quadruple Flip Death Jump" \
    -size 12 -color 0.9 0.1 0.1 -shadow
  gui add text 3 9 \
    "- Bindings are set to tight\n- Stay low on the hill\n- Rotate on jump takeoff\n- Do a quadruple front flip on jump\n- Good luck on landing!\n" \
    -size 12 -color 0.0 0.0 0.0
}

set ::hint [list "" ]
