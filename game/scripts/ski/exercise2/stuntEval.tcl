set ::numReset 3

proc reset {} {
    if {$::skierState == $::out} {
	if {$::numReset > 0} {
	    set ::numReset [expr $::numReset - 1]
	}
    } elseif {$::skierState == $::nextStage} {
	set ::numReset 3
	set ::skierState $::onGround
    }

    _reset 
}

proc startUnconscious {} {
    _startUnconscious

    message "Nice crash! $::numReset resets to go.\\n\\nPress <space> to restart simulation"
}

proc sideEffects { arg } {
    _sideEffects $arg

    if {$::skierState == $::nextStage} { return }

    if {$::numReset == 0} {
	goalReached
    }
}


set ::goalText "Crash the skier and revive him - 3 times!"
set ::hint [list "Try jumping around!" \
		"Go crazy!" \
		"Press <space> to restart simulation"]

proc guiBeginScenario {} {
    set ::numReset 3
    set ::skierState $::onGround
}

set ::unconsciousOk 1
