#----------------------------------------------------------------------
# char.tcl
# - character related routines
#----------------------------------------------------------------------

# --- list of characters
set ::char_list {}
lappend ::char_list [list Skier ski skier.setup]
lappend ::char_list [list Santa ski santa.setup]

# -- current character (set variable only once during session)
if {[info var ::char_curr] == {}} {
    set ::char_curr 0
}

#------------------------------
# char_startPreview - start simulation with the current character
#------------------------------

proc char_startPreview { s } {
    # setup character specific parameters
    set ::stageName $s
    set param [lindex $::char_list $::char_curr]
    set ::charName  [lindex $param 0]
    set ::charDir   [lindex $param 1]
    set ::charSetup [lindex $param 2]

    previewNewStage "$::charDir/$s/setup"
}

#------------------------------
# char_startSim - start simulation with the current character
#------------------------------

proc char_startSim { s } {
    # setup character specific parameters
    set ::stageName $s
    set param [lindex $::char_list $::char_curr]
    set ::charName  [lindex $param 0]
    set ::charDir   [lindex $param 1]
    set ::charSetup [lindex $param 2]

    startNewStage "$::charDir/$s/go"
}


#------------------------------
# char_getNames - get the names of all characters
#------------------------------

proc char_getNames {} {
    set names {}
    foreach c $::char_list {
	set n [lindex $c 0]
	lappend names $n
    }
    return $names
}

#------------------------------
# char_getChar - get the current character's index
#------------------------------

proc char_getChar {} {
    return $::char_curr
}


#------------------------------
# char_setChar - set the current character (by index)
#------------------------------

proc char_setChar { i } {
    set ::char_curr $i

    # setup character specific parameters
    set param [lindex $::char_list $::char_curr]
    set ::charName  [lindex $param 0]
    set ::charDir   [lindex $param 1]
    set ::charSetup [lindex $param 2]
}

