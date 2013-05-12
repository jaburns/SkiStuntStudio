#----------------------------------------------------------------------
# start.tcl - common functions used for setting up scenarios
#----------------------------------------------------------------------

#------------------------------
# startNewStage - start up a new stage/scenario
#   stageName = setup script to be called to create the new stage
#               (including pathname and setup filename)
#------------------------------

proc startNewStage { stageName } {
    simulate stop
    world clear
    < $stageName
    loaddemo
    hotzone 0.005 0.48 0.0065 0.64
    world pbMode timeDep
    world pbFrameSkip true
#    initSimSpeed2 $::SimRtRatio
    initSimSpeed $::SimRtRatio
    showGoal
}

#------------------------------
# PreviewNewStage 
#------------------------------

proc previewNewStage { stageName } {
    simulate stop
    world clear
    < $stageName
    loaddemo
    hotzone 0.005 0.48 0.0065 0.64
    world pbMode timeDep
    world pbFrameSkip true
#    initSimSpeed2 $::SimRtRatio
#    initSimSpeed $::SimRtRatio
    showGoal
}


#------------------------------
# initSimSpeed - initialize the simulation speed
#    rtratio = realtime ratio (real-time / sim-time)
#
# Note: the simulation speed depends on dt_disp, the
#       display time step; "simulate set_rtratio" is
#       used to estimate the value of dt_disp given
#       the rtratio; it is called a number of times to
#       improve the consistency of the estimate
#------------------------------

proc initSimSpeed { rtratio } {
    if {[info procs resetActiveObj] == "resetActiveObj"} { resetActiveObj }
    if {[info procs seInit] == "seInit"} { seInit }
    world setaf skier
    restpose $::restLocX $::restLocY

    # skip rt-ratio setting if we are in preview mode
    if [preview] {
	return
    }

    # first estimate
    simulate set_rtratio $rtratio 10

    set iter 5
    set mr [simulate measureRtRatio]
    puts $mr
    while {[expr {$mr > [expr $rtratio * 0.8]} && {$iter > 0}]} {
	puts "rtratio = $mr; redo... $iter"

	if {[info procs resetActiveObj] == "resetActiveObj"} { resetActiveObj }
	if {[info procs seInit] == "seInit"} { seInit }

	world setaf skier
	restpose $::restLocX $::restLocY
	simulate set_rtratio $rtratio
	set iter [expr $iter - 1]
	set mr [simulate measureRtRatio]
	puts $mr
    }
}

proc initSimSpeed2 { rtratio } {
    if {[info procs resetActiveObj] == "resetActiveObj"} { resetActiveObj }
    if {[info procs seInit] == "seInit"} { seInit }
#    world setaf skier
#    restpose $::restLocX $::restLocY

    # skip rt-ratio setting if we are in preview mode
    if [preview] {
	return
    }

    # first estimate
    simulate set_rtratio $rtratio 10

    set iter 5
    set mr [simulate measureRtRatio]
    puts $mr
    while {[expr {$mr > [expr $rtratio * 0.8]} && {$iter > 0}]} {
	puts "rtratio = $mr; redo... $iter"

	if {[info procs resetActiveObj] == "resetActiveObj"} { resetActiveObj }
	if {[info procs seInit] == "seInit"} { seInit }

#	world setaf skier
#	restpose $::restLocX $::restLocY
	simulate set_rtratio $rtratio
	set iter [expr $iter - 1]
	set mr [simulate measureRtRatio]
	puts $mr
    }
}

