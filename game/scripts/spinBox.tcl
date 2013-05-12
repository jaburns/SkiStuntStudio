#----------------------------------------------------------------------
# spin box widget
# - use sb_new to create new spinbox
# - use sb_delete to delete existing spinbox
#----------------------------------------------------------------------

source guiConst.tcl

#------------------------------
# sb_new - create a new spinbox
#    labels  = values selectable by the spinbox
#    cb      = callback to call when the spinbox is spun
#    r, g, b = normal text color
#    pr, pg, pb = text color when the button is pointed at by the mouse pointer
#    sr, sg, sb = text color when the mouse clicks on the text
#------------------------------

proc sb_new { name x y w labels size cb r g b pr pg pb sr sg sb} {
    set laVar  [sb_leftArrow $name]
    set raVar  [sb_rightArrow $name]
    set txtVar [sb_text $name]
    set lbsVar [sb_labels $name]
    set ciVar  [sb_currIdx $name]
    set cbVar  [sb_callback $name]

    # left arrow
    set $laVar [gui add text $x $y $::LeftArrow \
		    -color $r $g $b \
		    -size $size \
		    -point "sb_point" \
		    -pointColor $pr $pg $pb \
		    -pressColor $sr $sg $sb \
		    -select "sb_cb $name -1" \
		    -shadow]

    # right arrow
    set $raVar [gui add text [expr $x + $w + 0] $y $::RightArrow \
		    -color $r $g $b \
		    -size $size \
		    -point "sb_point" \
		    -pointColor $pr $pg $pb \
		    -pressColor $sr $sg $sb \
		    -select "sb_cb $name 1" \
		    -shadow]

    # text
    set defText [lindex $labels 0]
    set $txtVar [gui add text [expr $x + 2] $y $defText \
		    -color $r $g $b \
		    -size $size \
		    -shadow]

    set $lbsVar $labels
    set $ciVar 0

    set $cbVar $cb

    # call the callback with the default index
    # [expr $$cbVar] 0
}

#------------------------------
# sb_delete - delete a spinbox and all related variables
#------------------------------

proc sb_delete { name } {
    set laVar  [sb_leftArrow $name]
    set raVar  [sb_rightArrow $name]
    set txtVar [sb_text $name]
    set lbsVar [sb_labels $name]
    set ciVar  [sb_currIdx $name]
    set cbVar  [sb_callback $name]

    gui del [expr $$laVar]
    gui del [expr $$raVar]
    gui del [expr $$txtVar]

    unset $laVar
    unset $raVar
    unset $txtVar
    unset $lbsVar
    unset $ciVar
    unset $cbVar
}

#------------------------------
# sb_set - set the spinbox selection
#   name = name of spinbox
#   idx  = spinbox entry index
#------------------------------

proc sb_set { name idx } {
    set lbsVar [sb_labels $name]
    set ciVar  [sb_currIdx $name]
    set txtVar [sb_text $name]
    set cbVar  [sb_callback $name]

    set lbs [expr $$lbsVar]
    set ci  $idx

    set $ciVar $ci

    gui eval [expr $$txtVar] setText [lindex $lbs $ci]
    [expr $$cbVar] $ci
}

#------------------------------
# sb_get - retrieve the currently selected entry in 
#          a spinbox
#------------------------------

proc sb_get { name } {
    set ciVar  [sb_currIdx $name]
    return [expr $$ciVar]
}

#----------------------------------------------------------------------
# functions for generating variable names

#------------------------------
# sb_leftArrow - get the name of the variable which contains the ID 
#                to the left-arrow of the spinbox
#------------------------------

proc sb_leftArrow { name } {
    return "::sb_$name\_al"
}

#------------------------------
# sb_rightArrow - get the name of the variable which contains the ID 
#                 to the right-arrow of the spinbox
#------------------------------

proc sb_rightArrow { name } {
    return "::sb_$name\_ar"
}


#------------------------------
# sb_text - get the name of the variable which contains the ID to the
#           text of the spinbox
#------------------------------

proc sb_text { name } {
    return "::sb_$name\_t"
}

#------------------------------
# sb_labels - get the name of the variable which holds the labels
#             to be displayed in the spinbox
#------------------------------

proc sb_labels { name } {
    return "::sb_$name\_l"
}

#------------------------------
# sb_currIdx - get the name of the variable which holds the index
#              for the current entry of the spinbox
#------------------------------

proc sb_currIdx { name } {
    return "::sb_$name\_c"
}

#------------------------------
# sb_callback - get the name of the variable which holds the name of 
#               the callback / command to call when the spin box
#               is spun
#------------------------------

proc sb_callback { name } {
    return "::sb_$name\_cb"
}

#----------------------------------------------------------------------
# callbacks
#----------------------------------------------------------------------

#--------------------------
# sb_cb -- callback for left and right arrows
#--------------------------

proc sb_cb { name incDec } {
    set lbsVar [sb_labels $name]
    set ciVar  [sb_currIdx $name]
    set txtVar [sb_text $name]
    set cbVar  [sb_callback $name]

    set lbs [expr $$lbsVar]
    set ci  [expr $$ciVar]
    set newCi [expr $ci + $incDec]

    if {$newCi < 0} {
	set newCi [expr [llength $lbs] - 1]
    }
    if {$newCi >= [llength $lbs]} {
	set newCi 0
    }

    if {$newCi != $ci} {
	set ci $newCi
	set $ciVar $ci
        set text [lindex $lbs $ci]
        gui eval [expr $$txtVar] setText $text
	[expr $$cbVar] $ci
    }
}

#------------------------------
# sb_point - arrow pointed at by mouse pointer callback
#------------------------------

proc sb_point {} {
}

#----------------------------------------------------------------------
# test driver
#----------------------------------------------------------------------

# set labels [list " one " " two " three four five]
# set size 14
# 
# proc cb { arg } {
#     puts "test $arg"
# }
# 
# sb_new test 0 0.5 5 $labels $size cb 1 1 1 1 0 0 1 1 0
