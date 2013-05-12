#----------------------------------------------------------------------
# setup.tcl
# - setup character, simulation speed, etc.
#----------------------------------------------------------------------

gui delall
message ""

source util.tcl

#----------------------------------------------------------------------
# set title
#----------------------------------------------------------------------

setTitle "Ski Stunt Simulator : Setup"

#----------------------------------------------------------------------
# contants 
#----------------------------------------------------------------------

# location and size
set ::SpinBoxIndent 13
set ::SetupTxt_x    3
set ::SetupTxt_y    20
set ::SetupTxt_size 13

# text color
set ::SetupTxt_r    1
set ::SetupTxt_g    1
set ::SetupTxt_b    1

#----------------------------------------------------------------------
# background
#----------------------------------------------------------------------

gui bg
gui add pic props/blueGrad_small.jpg 0 0 1 1
gui fg

#----------------------------------------------------------------------
# character selection
#----------------------------------------------------------------------

# show title
set y $::SetupTxt_y
gui add text $::MenuIndent $y "Character" \
    -size $::SetupTxt_size \
    -color $::SetupTxt_r $::SetupTxt_g $::SetupTxt_b \
    -shadow

# show character
preview true 0.3 0.6 0.35 0.35
world clear
< char.setup

#------------------------------
# create spinbox for characters
set names [char_getNames]

# center the names
#set maxLen [maxStrLen $names]
set maxLen 10
set cNames {}
foreach n $names {
    lappend cNames [centerStr $n $maxLen]
}

# make spinbox
sb_new char $::SpinBoxIndent $y $maxLen $cNames $::SetupTxt_size setupChar_cb 0 1 0 1 0 0 1 1 0

#------------------------------
# setupChar_cb - callback to call when the spinbox is spun
#------------------------------

proc setupChar_cb { ci } {
    puts "ci = $ci"
    if {$ci == [char_getChar]} { return }

    # select character and reload 
    char_setChar $ci
    < setup.tcl
}

# select current character
sb_set char [char_getChar]

#----------------------------------------------------------------------
# simulation speed
#----------------------------------------------------------------------

# setup title
set y [expr $y - 2]
gui add text $::MenuIndent $y "Sim Speed" \
    -size $::SetupTxt_size \
    -color $::SetupTxt_r $::SetupTxt_g $::SetupTxt_b \
    -shadow

#------------------------------
# create sim speed spin box

set ::simSpeeds [list Normal Fast "Very Fast" "Very Slow" Slow]
set ::simSpeedVals [list 1.0 0.8 0.6 2.0 1.5]

# center the speeds
#set maxLen [maxStrLen $::simSpeeds]
set cSimSpeeds {}
foreach s $::simSpeeds {
    lappend cSimSpeeds [centerStr $s $maxLen]
}

# make spinbox
sb_new simSpeed $::SpinBoxIndent $y $maxLen $cSimSpeeds $::SetupTxt_size setupSimSpeed_cb \
    0 1 0 1 0 0 1 1 0

if {[info var ::Setup_simSpeedIdx] == {}} {
    set ::Setup_simSpeedIdx 0
}

#------------------------------
# setupSimSpeed_cb - callback to call when the spinbox is spun
#------------------------------

proc setupSimSpeed_cb { si } {
    set ::SimRtRatio [lindex $::simSpeedVals $si]
    set ::Setup_simSpeedIdx $si
    gui redraw
}

# select the current simulation speed
sb_set simSpeed $::Setup_simSpeedIdx



#----------------------------------------------------------------------
# god mode enabled
#----------------------------------------------------------------------

# setup title
#set y [expr $y - 2]
#gui add text $::MenuIndent $y "God Mode" \
#    -size $::SetupTxt_size \
#    -color $::SetupTxt_r $::SetupTxt_g $::SetupTxt_b \
#    -shadow

#------------------------------
# create spinbox for god mode

#set ::godModes [list "Disabled" "Enabled" ]
#set ::godModesVals [list "no" "yes"]

# center the modes strengths
#set cGodModes {}
#foreach s $::godModes {
#    lappend cGodModes [centerStr $s $maxLen]
#}

# make spinbox
#sb_new godModeBox $::SpinBoxIndent $y $maxLen $cGodModes $::SetupTxt_size \
#    setupGodMode_cb 0 1 0 1 0 0 1 1 0

#if {[info var ::Setup_godModeIdx] == {}} {
#    set ::Setup_godModeIdx 0
#}

#------------------------------
# setupGodMode_cb - callback to call when the spinbox is spun
#------------------------------

#proc setupGodMode_cb { si } {
#    set ::godMode [lindex $::godModesVals $si]
#}

# select god mode enabled or not
#sb_set godModeBox $::Setup_godModeIdx


#----------------------------------------------------------------------
# ski binding strength
#----------------------------------------------------------------------

# setup title
set y [expr $y - 2]
gui add text $::MenuIndent $y "Ski Bindings" \
    -size $::SetupTxt_size \
    -color $::SetupTxt_r $::SetupTxt_g $::SetupTxt_b \
    -shadow

#------------------------------
# create spinbox for binding strength

set ::bindStrengths [list Normal Tight "Very Tight" "Very Loose" Loose]
set ::bindStrengthVals [list 1300 2600 3900 500 900]

# center the binding strengths
#set maxLen [maxStrLen $::bindStrengths]
set cBindStrengths {}
foreach s $::bindStrengths {
    lappend cBindStrengths [centerStr $s $maxLen]
}

# make spinbox
sb_new bindStrength $::SpinBoxIndent $y $maxLen $cBindStrengths $::SetupTxt_size \
    setupBindStr_cb 0 1 0 1 0 0 1 1 0

if {[info var ::Setup_bindStrIdx] == {}} {
    set ::Setup_bindStrIdx 0
}

#------------------------------
# setupBindStr_cb - callback to call when the spinbox is spun
#------------------------------

proc setupBindStr_cb { si } {
    set ::MaxHzImpact [lindex $::bindStrengthVals $si]
    set ::MaxDnImpact [lindex $::bindStrengthVals $si]
    set ::Setup_bindStrIdx $si

    world setaf skier
    artfig setmonitor Ski2Monitor
    monitor eval maxHzImpact $::MaxHzImpact
    monitor eval maxDnImpact $::MaxHzImpact
    gui redraw
}

# select the current binding strength
sb_set bindStrength $::Setup_bindStrIdx


#----------------------------------------------------------------------
# player profile
#----------------------------------------------------------------------

# setup title
set y [expr $y - 2]
gui add text $::MenuIndent $y "Player" \
    -size $::SetupTxt_size \
    -color $::SetupTxt_r $::SetupTxt_g $::SetupTxt_b \
    -shadow

#------------------------------
# create spinbox for player names

set ::players [profile_users [profile_getDbName]]

# center the players
set maxLen [maxStrLen $::players]
set cPlayers {}
foreach s $::players {
    lappend cPlayers [centerStr $s $maxLen]
}

# make spinbox
sb_new player $::SpinBoxIndent $y $maxLen $cPlayers $::SetupTxt_size \
    setupPlayer_cb 0 1 0 1 0 0 1 1 0

#------------------------------
# setupPlayer_cb - callback to call when the spinbox is spun
#------------------------------

proc setupPlayer_cb { pi } {
    user_set [lindex $::players $pi]
    user_save [user_getFname]

    set ::PrevState [list 0 0]
    gui redraw
}

# select the current player
sb_set player [lsearch $::players $::user]

#----------------------------------------------------------------------
# new player
#----------------------------------------------------------------------

# setup title
set y [expr $y - 2]
gui add text $::MenuIndent $y "New Player" \
    -size $::SetupTxt_size \
    -color $::SetupTxt_r $::SetupTxt_g $::SetupTxt_b \
    -point "guiPoint" \
    -pointColor 1 0 0 \
    -pressColor 1 1 0 \
    -select "newPlayerBegin" \
    -shadow

# location and color of text
set ::newPlayerX [expr $::SpinBoxIndent + [string length "New Player   "]]
set ::newPlayerY $y
set ::newPlayerNameId -1
set ::newPlayerMaxNameLen 15

set ::newPlayer_r 1
set ::newPlayer_g 0.8
set ::newPlayer_b 0

#------------------------------
# newPlayerBegin - start the editing of new player name
#------------------------------

proc newPlayerBegin {} {
    set ::newPlayerNameId [gui add text $::SpinBoxIndent $::newPlayerY "" \
			       -size $::SetupTxt_size \
			       -color $::newPlayer_r $::newPlayer_g $::newPlayer_b \
			       -shadow]
    
    # start editing
    gui edit text $::newPlayerNameId \
	-maxLen $::newPlayerMaxNameLen \
	-callback newPlayerEnd
}

#------------------------------
# newPlayerEnd - terminate the editing of new player name
#------------------------------

proc newPlayerEnd {} {
    set txt [gui eval $::newPlayerNameId getText]
    set reloadPage 0

    if {$txt != ""} {
	profile_newUser [profile_getDbName] $txt
	user_set $txt
	addLevel
	addLevel
	addLevel
	user_save [user_getFname]
	set reloadPage 1
    }

    gui del $::newPlayerNameId
    set ::newPlayerNameId -1

    if {$reloadPage} {
	< setup.tcl
    }
}


#----------------------------------------------------------------------
# back to "Main Menu" button
#----------------------------------------------------------------------

set ::setup_mm_r 0
set ::setup_mm_g 0.8
set ::setup_mm_b 0
set y [expr $y - 4]

setScExit "Back" $::SetupTxt_r $::SetupTxt_g $::SetupTxt_b "gui delall; < gui.tcl" $y

