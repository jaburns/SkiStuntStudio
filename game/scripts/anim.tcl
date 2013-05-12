#----------------------------------------------------------------------
# anim.tcl
# - animation user interface
# - with pause and resume support
# - initial use = for playing back the lessons during the training sessions
#----------------------------------------------------------------------

#----------------------------------------------------------------------
# Insert the following into animation files:
# 1. Insert "anim_end" at the end of the animation.
# 2. Insert "anim_pause" where you want pauses to occur
#
# Override "anim_resume" to add user-defined behavior to the resumption
# of the animation.
#----------------------------------------------------------------------

#----------------------------------------------------------------------
# state variables
#----------------------------------------------------------------------

set ::anim_playing 0

#----------------------------------------------------------------------

set ::anim_mm_r 0
set ::anim_mm_g 0.8
set ::anim_mm_b 0

set ::escProc 1

proc level_select {} {
#    anim_end

    gui delall
    < gui.tcl
    gui redraw
}

proc keySpace {} {
    set ::escProc 1
    if {$::anim_playing == 0} {
	anim_begin
    } else {
	if {[pb isPaused]} {
	    anim_resume
	}
    }
}

#----------------------------------------------------------------------

proc anim_begin {} {
    # to be overridden
    _anim_begin
}

proc anim_end {} {
    # to be overridden
    _anim_end
}

proc anim_reset {} {
    # to be overridden
}

proc anim_pause {} {
    pb pause
}

proc anim_resume {} {
    # to be overridden
    pb resume
}

#----------------------------------------------------------------------

proc _anim_begin {} {
    set ::anim_playing 1
}

proc _anim_end {} {
    set ::anim_playing 0
}
