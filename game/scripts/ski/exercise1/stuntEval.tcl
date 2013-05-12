set ::tgtMPos [list 0.5 0.5]
set ::tol 0.2
set ::upright 0
set ::tgtReachedCount 0
set ::tgtReachedTh    5

proc pose1 {} {
    # neutral
    world setaf skier2
    showall 2 0 0.58 0 0.039 -0.074 0.460 -0.025 -0.435 -0.018 1.048 -0.013     0.000     0.000     0.000    25.598     0.000     0.000   -24.755     0.000     0.000    60.349     0.000     0.000 
    
    set ::tgtMPos [list 0.429688 0.483073]
    set ::upright 0
    set ::tol 0.2
}

proc pose2 {} {
    # crouch down
    world setaf skier2
    showall 2 0 0.58 0 0.020 -0.003 1.643 -0.003 -1.928 0.001 0.436 -0.001     0.000     0.000     0.000    93.727     0.000     0.000  -110.000     0.000     0.000    25.544     0.000     0.000 

    set ::tgtMPos [list 0.441406 0.924479]
    set ::upright 0
    set ::tol 0.2
}

proc pose3 {} {
    # straight-up
    world setaf skier2
    showall 2 0 0.58 0 0.021 0.063 0.008 0.020 0.349 0.014 1.220 0.012     0.000     0.000     0.000     0.000     0.000     0.000    20.000     0.000     0.000    70.000     0.000     0.000 

    set ::tgtMPos [list 0.461914 0.036458]
    set ::upright 1
    set ::tol 0.2
}

proc pose4 {} {
    # lean back
    world setaf skier2
    showall 2 0 0.58 0 0.054 0.558 1.256 0.461 0.258 0.354 1.099 0.077     0.000     0.000     0.000    70.751     0.000     0.000    14.244     0.000     0.000    62.402     0.000     0.000 

    set ::tgtMPos [list 0.089063 0.457031]
    set ::upright 0
    set ::tol 0.2
}

proc pose5 {} {
    # lean forward
    world setaf skier2
    showall 2 0 0.58 0 0.009 0.006 -0.002 -0.003 -1.160 -0.000 1.052 -0.003     0.000     0.000     0.000     0.000     0.000     0.000   -65.005     0.000     0.000    60.760     0.000     0.000 

    set ::tgtMPos [list 0.861875 0.477865]
    set ::upright 0
    set ::tol 0.27
}

proc pose6 {} {
    # low forward 
    world setaf skier2
    showall 2 0 0.58 0 0.015 -0.003 0.449 -0.001 -1.931 -0.000 0.570 0.000     0.000     0.000     0.000    25.567     0.000     0.000  -110.000     0.000     0.000    32.834     0.000     0.000 

    set ::tgtMPos [list 0.865234 0.832031]
    set ::upright 0
    set ::tol 0.25
}

proc pose7 {} {
    # low seated
    world setaf skier2
    showall 2 0 0.58 0 0.033 0.001 1.752 0.000 -1.350 0.000 0.564 0.000     0.000     0.000     0.000   100.000     0.000     0.000   -77.321     0.000     0.000    32.731     0.000     0.000 

    set ::tgtMPos [list 0.130313 0.833333]
    set ::upright 0
    set ::tol 0.25
}

proc pose8 {} {
    # high lean back
    world setaf skier2
    showall 2 0 0.58 0 0.043 -0.000 0.494 -0.000 0.356 -0.000 1.223 -0.000     0.000     0.000     0.000    27.226     0.000     0.000    20.000     0.000     0.000    70.000     0.000     0.000 

    set ::tgtMPos [list 0.130742 0.157552]
    set ::upright 0
    set ::tol 0.27
}

proc pose9 {} {
    # high lean forward
    world setaf skier2
    showall 2 0 0.58 0 0.010 -0.005 -0.001 -0.002 -0.675 0.045 1.211 -0.021     0.000     0.000     0.000     0.000     0.000     0.000   -37.132     0.000     0.000    70.000     0.000     0.000 

    set ::tgtMPos [list 0.863164 0.334635]
    set ::upright 0
    set ::tol 0.27
}

#----------------------------------------------------------------------
# posture sequencing
#----------------------------------------------------------------------

set ::tgtPoses [list pose1 pose3 pose2 pose3 pose2 pose4 pose5 pose4 pose5 \
		    pose1 pose9 pose8 pose9 pose8 pose7 pose6 pose7 pose6 \
		    pose3 pose7 pose9 pose1 pose8 pose6 pose4 pose5 pose2] 
set ::tgtPoseIdx 0

#----------------------------------------------------------------------

proc xComp { p } {
    return [lindex $p 0]
}

proc yComp { p } {
    return [lindex $p 1]
}

proc isUpright { p } {
    set p1 [list 0 0]
    set p2 [list 0.44 0.45]
    set p3 [list 1 0]

    set x [xComp $p]
    set y [yComp $p]

    if {$x < [xComp $p2]} {
	set f [expr $x / [xComp $p2]]
	set ly [expr ($f * [yComp $p2]) + ((1 - $f) * [yComp $p1])]
	if {$y < $ly} {
	    return 1
	}
    } else {
	set f [expr ($x - [xComp $p2]) / ([xComp $p3] - [xComp $p2])]
	set ly [expr ($f * [yComp $p3]) + ((1 - $f) * [yComp $p2])]
	if {$y < $ly} {
	    return 1
	}
    }

    return 0
}

proc equal { p1 p2 tol } {
    set dx [expr [xComp $p2] - [xComp $p1] ]
    set dy [expr [yComp $p2] - [yComp $p1] ]
    set distSq [expr $dx * $dx + $dy * $dy]
    set tolSq [expr $tol * $tol]
    if {$distSq < $tolSq} {
	return 1
    }
    return 0
}

proc poseMatch {} {
    set p [mpos]
    if {$::upright} {
	if [isUpright $p] {
	    return 1
	}
    } else {
	if [equal $p $::tgtMPos $::tol] {
	    return 1
	}
    }

    return 0
}

#----------------------------------------------------------------------

proc reset {} {
    _reset 

    # initialize state info
    set ::tgtPoseIdx 0
    set ::tgtReachedCount 0

    # setup first pose
    set p [lindex $::tgtPoses $::tgtPoseIdx]
    eval $p
}

proc sideEffects { arg } {
    _sideEffects arg

    if {$::skierState == $::nextStage} { return }

    set goalMet 0

    if [poseMatch] {
	set ::tgtReachedCount [expr $::tgtReachedCount + 1]
	if {$::tgtReachedCount > $::tgtReachedTh} {
	    set ::tgtReachedCount 0
	    set ::tgtPoseIdx [expr $::tgtPoseIdx + 1]
	    if {$::tgtPoseIdx >= [llength $::tgtPoses]} {
		set goalMet 1
	    }

	    set p [lindex $::tgtPoses $::tgtPoseIdx]
	    eval $p
	}
    }

    if {$goalMet} {
	goalReached
    }
}

set ::goalText "Monkey-see Monkey-do... you do what I do!"
set ::hint [list "mouse UP = stand UP" \
	       "mouse DOWN = crouch DOWN" \
	       "mouse RIGHT = lean FORWARD" \
	       "mouse LEFT = lean BACKWARD" \
	       "Try other mouse positions!"]
