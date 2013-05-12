#----------------------------------------------------------------------
# goal.tcl
# - display the goal of the stage and allow the user to preview
#   the terrain
#----------------------------------------------------------------------

#----------------------------------------------------------------------
# init
#----------------------------------------------------------------------

gui delall
camera stop

source guiConst.tcl

#----------------------------------------------------------------------
# Preview button
#----------------------------------------------------------------------

# color of the "Preview" button
set ::goal_preview_r 1
set ::goal_preview_g 0.8
set ::goal_preview_b 0

# size, position and text for the button
set str "Stop Preview"
set ::PvSize 14
set ::PvX [expr ($::screenWidth / $::PvSize) - [string length $str]]
set ::PvY 0.5
set ::PvId     -1

# create button
# set ::PvId [gui add text $::PvX $::PvY "Preview"  -size $::PvSize \
#		-color $::goal_preview_r $::goal_preview_g $::goal_preview_b \
#		-point "goalPoint" \
#		-pointColor 1 0 0 \
#		-pressColor 1 1 0 \
#		-select "goPreview" \
#		-shadow]

#------------------------------
# goPreview
#   callback to start/stop the terrain preview
#------------------------------

proc goPreview {} {
    camera stop

    if [camera isFlying] {
	camera stop

	# "Stop Preview" -> "Preview"
	gui eval $::PvId setText "Preview"
	gui redraw
    } else {
	# "Preview" -> "Stop Preview"
	gui eval $::PvId setText "Stop Preview"
	gui redraw
	autoscroll
	
	# "Stop Preview" -> "Preview"
	gui eval $::PvId setText "Preview"
	gui redraw
    }
}

#------------------------------
# goalPoint
#   dummy callback to be called when the mouse
#   points at the "preview" button
#------------------------------

proc goalPoint {} {
}

#----------------------------------------------------------------------
# show the goal of the scenario

# position, size and color of the goal text
set ::goalX    0.5
set ::goalY    0.5
set ::goalSize 12
set ::goal_r   0.2
set ::goal_g   0.2
set ::goal_b   0.8

#------------------------------
# numLines - count the number of lines in a string
#------------------------------

proc numLines { str } {
    set nl 1
    for {set i 0} {$i < [string length $str]} {set i [expr $i + 1]} {
	set c [string index $str $i]
	if {$c == "\n"} {
	    set nl [expr $nl + 1]
	}
    }

    return $nl
}

set nl [numLines $::goalText]

# draw goal text
gui add text $::goalX [expr $::goalY + $nl + 2] $::goalText \
    -size $::goalSize \
    -color $::goal_r $::goal_g $::goal_b \
    -shadow

# draw instruction text
gui add text $::goalX [expr $::goalY + 1] " <space> to start simulation" \
    -size $::goalSize \
    -color 0.5 0.5 1 \
    -shadow
gui add text $::goalX $::goalY " <Esc> to stop simulation" \
   -size $::goalSize \
   -color 0.5 0.5 1 \
    -shadow

showGoal2

gui redraw
