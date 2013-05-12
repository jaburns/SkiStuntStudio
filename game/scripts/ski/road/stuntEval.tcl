set ::curbTh 3

set ::sgbk 0
set ::dbfr 0
set ::landComment 0

set ::roadKillFrac 0.9
set ::roadCom [list $::soundDir/harshLand.wav \
		   $::soundDir/noHitch.wav]

set ::smoothLandFy 6000
set ::superLandFy  4500

proc reset {} {
    _reset
    
    set ::sgbk 0
    set ::dbfr 0

    set ::landComment 0

    landEval::reset
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
    if {[currLoc] == "road"} {
	set ci [expr int(rand() * [llength $::roadCom])]
	set c [lindex $::roadCom $ci]
	if {[playComAtTimes $c $::roadKillFrac] == 1} {
	    set ::noComment 1
	}
    }

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
		if {[expr {$::sgbk > 0} && {$::dbfr > 0}]} {
		    set goalMet 1
		}
	    }
	}
    } 

    goal $goalMet
}

proc processJump {} {
    set numRot   [lindex $::jumpInfo 1]

    if {$numRot == 1} {
	set ::sgbk [expr $::sgbk + 1]
    } elseif {$numRot == -2} {
	set ::dbfr [expr $::dbfr + 1]
    }

    # standard jump evaluation
    _processJump

    # smooth landing evaluation
    if {[currLoc] == "dip2"} {
	if {$::landComment == 0} {
	    landEval::processJump
	    set ::landComment 1
	}
    }
}

set ::nextStageName hutRock

set ::goalText "1 back flip + 1 double front + land!" 
set ::hint [list "Try staring your jump a little earlier." \
		"Try starting your jump a little later." \
		"Start jumping at bottom of the trough for distance." \
		"Control your rotations by tucks and extensions."]