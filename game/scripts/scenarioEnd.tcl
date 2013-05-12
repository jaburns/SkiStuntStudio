#----------------------------------------------------------------------
# scenarioEnd.tcl
# - display the options available to the player at the end of 
#   a scenario
#----------------------------------------------------------------------

gui delall

#----------------------------------------------------------------------
# contants
#----------------------------------------------------------------------

# size, location and spacing of the scenario-end menu items
set ::se_size   14
set ::se_row1Y  0.5 
set ::se_row1X  8
#set ::se_spW    0.8
set ::se_spW    0.65

# color for the first letter of the menu items
set ::se_l1_r 1
set ::se_l1_g 0
set ::se_l1_b 0

# color for the rest of the letters of the menu items
set ::se_norm_r 1
set ::se_norm_g 0.6
set ::se_norm_b 0

# color to turn the menu items into when they are pointed to
# by the mouse pointer
set ::se_point_r 0.5
set ::se_point_g 0.5
set ::se_point_b 1

# color to turn the menu items into when they are clicked
set ::se_press_r 1
set ::se_press_g 0
set ::se_press_b 0

# color for the "Main Menu" button
set ::se_mm_r 0
set ::se_mm_g 0.8
set ::se_mm_b 0

#----------------------------------------------------------------------
# implementation
#----------------------------------------------------------------------

#------------------------------
# se_point - dummy callback to call when the mouse points at a
#            menu item
#------------------------------

proc se_point {} {
}

#------------------------------
# se_new_entry - create new menu item
#------------------------------

proc se_new_entry { x y str cb } {
    set sl1 [string index $str 0]
    set snorm [string range $str 1 [expr [string length $str] - 1]]

    set id2 [gui add text $x $y $str -size $::se_size \
		 -color $::se_norm_r $::se_norm_g $::se_norm_b \
		 -point se_point \
		 -pointColor $::se_point_r $::se_point_g $::se_point_b \
		 -pressColor $::se_press_r $::se_press_g $::se_press_b \
		 -select $cb \
 		 -shadow]
    set id1 [gui add text $x $y $sl1 -size $::se_size \
		 -color $::se_l1_r $::se_l1_g $::se_l1_b \
		 -shadow]
    return [list $id1 $id2]
}

#------------------------------
# "Retry" button
set x $::se_row1X
set y $::se_row1Y
set str "Playback"
set strLen [string length $str]
    se_new_entry $x $y $str "keyp"

# only display the "Playback" and "Save" button after
# a new animation is recorded for this scenario
if {$::sim_rec} {
    set x [expr $x + $::se_spW + $strLen]
    set str "Replay"
    set strLen [string length $str]
    se_new_entry $x $y $str "keyr"
    
    set x [expr $x + $::se_spW + $strLen]
    set str "Save"
    set strLen [string length $str]
    se_new_entry $x $y $str saveAnimBegin
}

#------------------------------
# "Goal" button
set x [expr $x + $::se_spW + $strLen]
set str "Goal"
set strLen [string length $str]
se_new_entry $x $y $str keyg

#------------------------------
# "Hint" button
set x [expr $x + $::se_spW + $strLen]
set str "Hint"
set strLen [string length $str]
se_new_entry $x $y $str keyh


if {$::editable} {
  #------------------------------
  # "Edit" button
  set x [expr $x + $::se_spW + $strLen]
  set str "Edit"
  set strLen [string length $str]
  se_new_entry $x $y $str keye
} else {
  #------------------------------
  # "Demo" button
  set x [expr $x + $::se_spW + $strLen]
  set str "Demo"
  set strLen [string length $str]
  se_new_entry $x $y $str keyd
}

#------------------------------
# constants for prompting the user for the names to
# record animations as
set ::se_prompt_size 12   
set ::se_prompt_x    2
set ::se_prompt_y    [expr ($::screenHeight / $::se_prompt_size) / 2]
set ::se_dir_y       [expr $::se_prompt_y + 2]
set ::se_prompt_r    0.2
set ::se_prompt_g    0.2
set ::se_prompt_b    0.8
set ::se_prompt_r2   1
set ::se_prompt_g2   0.8
set ::se_prompt_b2   0
set ::se_prompt_str  "Animation name: "
set ::se_prompt_maxLen 20

set ::se_prompt_pid  -1
set ::se_prompt_tid  -1

#------------------------------
# saveAnimBegin - begin the saving of an animation
#------------------------------

proc saveAnimBegin {} {
    message ""

    # display prompt
    set ::se_prompt_pid [gui add text \
			     $::se_prompt_x $::se_prompt_y $::se_prompt_str \
			     -color $::se_prompt_r $::se_prompt_g $::se_prompt_b \
			     -shadow]
    set ::se_dir_pid [gui add text \
                            $::se_prompt_x $::se_dir_y \
                            "Saving to directory $::RootDir/saved" \
			     -color $::se_prompt_r $::se_prompt_g $::se_prompt_b \
			     -shadow]
    # display text
    set x [expr $::se_prompt_x + [string length $::se_prompt_str]]
    set ::se_prompt_tid [gui add text \
			     $x $::se_prompt_y "" \
			     -color $::se_prompt_r2 $::se_prompt_g2 $::se_prompt_b2 \
			     -shadow]

    # start editing
    gui edit text $::se_prompt_tid \
	-maxLen $::se_prompt_maxLen \
	-callback saveAnimEnd
}

#------------------------------
# saveAnimEnd - end the saving of an animation
#------------------------------

proc saveAnimEnd {} {
    set txt [gui eval $::se_prompt_tid getText]

    # save animation if we didn't abort the name entry
    # (abort -> name text stays blank)
    if {$txt != ""} {
        buf_csave $::RootDir/saved/$txt.az
    }

    gui del $::se_prompt_pid
    gui del $::se_dir_pid
    gui del $::se_prompt_tid
    set ::se_prompt_pid -1
    set ::se_prompt_tid -1
}


#----------------------------------------------------------------------
# back to "main menu" button
#----------------------------------------------------------------------

setScExit "Menu" $::se_mm_r $::se_mm_g $::se_mm_b "puts done; gui delall; < gui.tcl" 0.5

guiMode menu
gui redraw
