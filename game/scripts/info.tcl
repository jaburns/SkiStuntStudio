#----------------------------------------------------------------------
# info.tcl
# - supply instruction and other auxiliary information 
#----------------------------------------------------------------------

gui delall
message ""

# hide the preview window
preview true -0.4 0.4 0.01 0.01

# setup the title
#setTitle "Ski Stunt Simulator"

#----------------------------------------------------------------------
# constants
#----------------------------------------------------------------------

# position, size and color of text
set ::InfoTxt_x    9
set ::InfoTxt_y    20
set ::InfoTxt_size 14
set ::InfoTxt_r    1
set ::InfoTxt_g    1
set ::InfoTxt_b    1
set ::TextID {}
set ::InfoIndent 1

#------------------------------
# MenuItem
#------------------------------

proc MenuItem { y item cb } {
  set id [gui add text $::InfoIndent $y $item \
    -size $::InfoTxt_size \
    -color $::InfoTxt_r $::InfoTxt_g $::InfoTxt_b \
    -point "guiPoint" \
    -pointColor 1 0 0 -pressColor 1 1 0  -selectColor 1 1 0 \
    -select $cb -shadow ]
  return $id
}

#-------------------------------------
# EmptyList - empties list of text IDs
#-------------------------------------

proc EmptyList {} {
    foreach element $::TextID {
      gui del $element
    }
    set ::TextID {}
}

#------------------------------
# DisplayText
#------------------------------

proc DisplayText { x y text } {
    set newID [gui add text $x $y $text \
		      -size $::InfoTxt_size -color 0.78 0.78 0.39 ]
    puts $newID
    lappend ::TextID $newID
    puts $::TextID
}

#------------------------------
# groupSelect - ensures only one item selected at a time
#------------------------------
proc groupSelect { id } {
    gui eval $::ID_1 setState Normal
    gui eval $::ID_2 setState Normal
    gui eval $::ID_3 setState Normal
    gui eval $id setState Selected
}

#-------------------------------
# Callback routines for various menu selections
#-------------------------------

proc goInstruction {} {
    groupSelect $::ID_1
    EmptyList
    DisplayText 8 23 "mouse left/right:   controls forward / backwards lean"
    DisplayText 8 22 "mouse up/down:      controls crouch / extension"
    DisplayText 8 21 "left mouse:         braking action"
    DisplayText 8 20 "right mouse:        jet-pack thrust "

    DisplayText 8 18 "space:              restarts the simulation"
    DisplayText 8 17 "ESC:                stops simulation"
    DisplayText 8 16 "up arrow:           zoom out"
    DisplayText 8 15 "down arrow:         zoom in"
    DisplayText 8 14 "* :                 toggle physics display"

    DisplayText 8 12 "TAB:                next animation in demo loop"

    DisplayText 8 10 "A hit to the head or a loss of skis"
    DisplayText 8 9 "causes a loss of control of the skier."
    gui redraw
}

proc goTech {} {
    groupSelect $::ID_2
    EmptyList
    DisplayText 8 23 "All motions in this game are the product of"
    DisplayText 8 22 "physics-based simulation. The mouse position"
    DisplayText 8 21 "linearly maps to a set of target joint angles,"
    DisplayText 8 20 "which in turn are used to compute joint torques"
    DisplayText 8 19 "via PD controllers. The ski bindings are "
    DisplayText 8 18 "released based upon the forces applied to the skis."

    DisplayText 8 16 "You can toggle the display the underlying articulated"
    DisplayText 8 15 "figure, the contact forces, and the center-of-mass" 
    DisplayText 8 14 "trajectory by typing '*' during an ongoing simulation."
    gui redraw
}
proc goCredits {} {
    groupSelect $::ID_3
    EmptyList
    DisplayText 8 23   "concept, mgmt, coding   Michiel van de Panne"
    DisplayText 8 22 "code wizard, artwork    Cedric Lee"

    DisplayText 8 20 "OpenGL font library     Nate Miller"
    DisplayText 8 19   "scripting language      Tcl/Tk (see license.txt)" 
    DisplayText 8 17   "Contact:"
    DisplayText 8 15   "  Motion Playground Inc."
    DisplayText 8 14   "  Vancouver, B.C., Canada"
    DisplayText 8 13   "  van@motionplayground.com"
    gui redraw
}

set y 32
set x 2
set text "Ski Stunt Simulator v1.1     http://motionplayground.com"
gui add text $x $y $text -size $::InfoTxt_size -color 0.78 0.78 0.39 
set y [expr $y - 1.5]
set text "Try the online Java version!"
gui add text $x $y $text -size $::InfoTxt_size -color 0.78 0.78 0.39 

set y 27
set ::ID_1 [MenuItem $y "Instructions" "goInstruction"]
set y [expr $y - 1.5]
set ::ID_2 [MenuItem $y "Technical Info" "goTech"]
set y [expr $y - 1.5]
set ::ID_3 [MenuItem $y "Credits" "goCredits"]
set y [expr $y - 2.5]
setScExit "Back" $::InfoTxt_r $::InfoTxt_g $::InfoTxt_b "gui delall; < gui.tcl" $y

#----------------------------------------------------------------------
# background
#----------------------------------------------------------------------

gui bg
gui add pic props/blueGrad_small.jpg 0 0 1 1
gui fg



