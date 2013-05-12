set ::treeCount 0
set ::outCount 0

set ::outTh 2

set ::endZoneReached 0
set ::failCom 0

proc reset {} {
    _reset

    set ::treeCount 0
    set ::outCount 0
    set ::endZoneReached 0
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
		set ::endZoneReached 1
		if {$::treeCount > 0} {
		    set goalMet 1
		} else {
		    if {$::failCom == 0} {
			set ::failCom 1
			message "You missed the trees before you landed.\nPress <space> to try again."
		    }
		}
	    }
	}
    } 

    if {$::skierState != $::onGround} {
	# only count the tree contacts if we haven't reached the endzone
	if {$::endZoneReached == 0} {
	    set ::treeCount [accumCol Tree 0 $::treeCount]
	}
    }

    if {$::skierState == $::out} {
	if {$::treeCount > 0} {
	    if {$::outCount == $::outTh} {
		puts "Don't you just hate those darn trees?"
	    } 
	    set ::outCount [expr $::outCount + 1]
	}
    }

    goal $goalMet
}

proc processJump {} {
    _processJump
}

set ::nextStageName deathValley
set ::goalText "Touch the trees and land!"
set ::hint [list "Reduce speed to reduce risk."]
