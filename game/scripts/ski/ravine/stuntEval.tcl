proc startFlight {} {
    _startFlight
}

proc startLanding {} {
    _startLanding
}

proc startGround {} {
    _startGround
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
		set goalMet 1
	    }
	}
    } 

    goal $goalMet
}

proc processJump {} {
    _processJump
}

set ::nextStageName camelTrees

set ::goalText "Get to the other side of the ravine!"

proc startUnconscious {} {
    _startUnconscious

    set crashSite [locMap map [lindex $::currState 2] [lindex $::currState 3]]

    if [expr {$crashSite == "land"} && [almostLand -30]] {
	playCom $::soundDir/almost.wav 0

	# skip all playback of comments
	set ::noComment 1
    }

    if {[currLoc] == "ravine"} {
	puts "Hey!  Thanks for dropping by!"
    } else {
	puts "currLoc = [currLoc]"
    }
}

set ::hint [list "Try front flipping across." \
		"Double flips are fun too!" \
	       "Try pumping your jumps."]

