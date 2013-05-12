#----------------------------------------------------------------------
# vertical list
# - use vl_new to create new list
# - use vl_delete to delete existing list
#----------------------------------------------------------------------

proc vl_doNothing {} {}

#------------------------------
# vl_new - create new vertical text list
#    labels  = values selectable by the spinbox
#    cb      = callback to call when a list entry is clicked
#    r, g, b = normal text color
#    pr, pg, pb = text color when the button is pointed at by the mouse pointer
#    sr, sg, sb = text color when the mouse clicks on the text
#------------------------------

proc vl_new { name x y labels size cb r g b pr pg pb sr sg sb scrollDownCallback scrollUpCallback } {
    set txtVar [vl_txt $name]

    # create text list
    set $txtVar {}
    set i 0
	
	set scrollR 1.0
	set scrollG 0.5
	set scrollB 1.0
		
		set id [gui add text $x [expr $y + 1] "" -color 0 0 0 -size $size -point "vl_point" -pointColor 0 0 0 \
		        -pressColor 0 0 0 -selectColor 0 0 0 -select "vl_doNothing"]
		lappend $txtVar $id

	set saveStartY $y
	set drawScrollers [expr $::scenarioBrowseIndex > 0]
	
	set y [expr $y - 1]
	
    foreach l $labels {
	if {$i >= $::scenarioBrowseIndex} {

		set id [gui add text $x $y $l \
		-color $r $g $b \
		-size $size \
		-point "vl_point" \
		-pointColor $pr $pg $pb \
		-pressColor $sr $sg $sb \
		-selectColor $sr $sg $sb \
		-select "$cb $i" \
		-shadow]

		# append the id onto the list of id's
		lappend $txtVar $id

		# count number of lines there are in label
		set numLines 1
		for {set li 0} {$li < [string length $l]} {set li [expr $li + 1]} {
			set lc [string index $l $li]
			if {$lc == "\n"} {
				set numLines [expr $numLines + 1]
			}
		}

		# move onto the next row
		set y [expr $y - $numLines]
		
		if {[expr $i - $::scenarioBrowseIndex] >= $::scenarioMaxIndex} {
			set drawScrollers 1
			break
		}
	}
	set i [expr $i + 1]
    }
	
	if { $drawScrollers } {
		set id [gui add text $x $saveStartY "<UP>" \
		-color $scrollR $scrollG $scrollB \
		-size $size \
		-point "vl_point" \
		-pointColor $scrollR $scrollG $scrollB \
		-pressColor $scrollR $scrollG $scrollB \
		-selectColor $scrollR $scrollG $scrollB \
		-select "$scrollUpCallback" \
		-shadow]
		lappend $txtVar $id
		set id [gui add text $x $y "<DOWN>" \
		-color $scrollR $scrollG $scrollB \
		-size $size \
		-point "vl_point" \
		-pointColor $scrollR $scrollG $scrollB \
		-pressColor $scrollR $scrollG $scrollB \
		-selectColor $scrollR $scrollG $scrollB \
		-select "$scrollDownCallback" \
		-shadow]
		lappend $txtVar $id
	}
}

#------------------------------
# vl_delete - delete all GUI objects and variables associated with a
#             vertical list
#------------------------------

proc vl_delete { name } {
    set txtVar [vl_txt $name]

    if {[info var $txtVar] != {}} {
	set txt [expr $$txtVar]
	foreach t $txt {
	    gui del $t
	}

	unset $txtVar
    }
}

#------------------------------
# vl_txt - get the name of the variable that contains the list of IDs
#          for the list of text
#------------------------------

proc vl_txt { name } {
    return "::vi_$name\_txt"
}

#------------------------------
# vl_point - dummy callback function that is called when text in the
#            list is pointed at by the mouse pointer
#------------------------------

proc vl_point {} {
}

#----------------------------------------------------------------------
# test driver
#----------------------------------------------------------------------

# set labels [list zero one two three four five]
# set size 14
# 
# proc cb { arg } {
#     puts "test $arg"
# }
# 
# vl_new test 2 10 $labels $size cb 1 1 1 1 0 0 1 1 0
