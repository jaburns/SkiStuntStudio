set ::crateColCount 0
#set ::unconsciousOk 1

proc reset {} {
    _reset

    set ::crateColCount 0
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
    if {$::crateColCount > 0} {
    	set goalMet 1
    }
    
    # flag goal
    goal $goalMet

    # state independent tests
    set ::crateColCount [accumCol "Crate" 1 $::crateColCount]
}

proc processJump {} {
    _processJump
}

set ::nextStageName jump

proc changeStage {} {
    _changeStage
}

# show goal
proc showGoal2 {} {
  gui add text 0.5 11 \
    "Touch the Crate!" \
    -size 12 -color 0.9 0.1 0.1 -shadow
  gui add text 3 9 \
    "- Bindings are set to medium\n- Keep your balance centered\n- Watch your head on the loop\n- Must hold contact with crate to win" \
    -size 12 -color 0.0 0.0 0.0
}

set ::hint [list "" ]
