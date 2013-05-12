#----------------------------------------------------------------------
# gui.tcl
# - GUI main menu for the game
# - precondition: guiInit.tcl is loaded before the very first
#   call to gui.tcl
#----------------------------------------------------------------------

#----------------------------------------------------------------------
# initialization
#----------------------------------------------------------------------

simulate stop
set stopDemo 1
pb stop
guiMode levels
set ::stuntEval stuntEval_1.tcl
set ::editable 0
gui delall
message ""

source guiConst.tcl

#----------------------------------------------------------------------
# load other modules
#----------------------------------------------------------------------

source spinBox.tcl
source vlist.tcl
source scenario.tcl

#----------------------------------------------------------------------
# title
#----------------------------------------------------------------------

set ::titleSize 14
set ::titleId   -1

#------------------------------
# setTitle - set the title text at the upper-right hand
#            corner of the display
#------------------------------

proc setTitleSetLen { str len } {
    set x [expr ($::screenWidth / $::titleSize) - $len]
    set y [expr ($::screenHeight / $::titleSize) - 1.5]

    # remove old text
    if {$::titleId > -1} {
	gui del $::titleId
    }

    # create new title (in italics)
    set $::titleId [gui add text $x $y \
			"\\i+$str" \
			-color 0.8 0.8 1 \
			-size $::titleSize \
			-shadow]
}

proc setTitle { str } {
	setTitleSetLen $str [string length $str]
}

# default title
setTitleSetLen " Ski Stunt Simulator\nStunt Studio Edition" 20

#----------------------------------------------------------------------
# screenExit
#----------------------------------------------------------------------

set ::scExitSize 14
set ::scExitId   -1

#------------------------------
# setScExit - setup the exit button at the lower-right corner
#             of the display
#   str    = button text
#   r,g,b  = color of the button (normalized components)
#   cb     = callback to call when the button is pressed
#------------------------------

proc setScExit { str r g b cb y } {
#    set x [expr ($::screenWidth / $::scExitSize) - [string length $str]]

    # remove old button
    if {$::scExitId > -1} {
	gui del $::scExitId
    }

    # create new button
    set id [gui add text $::MenuIndent $y $str \
		-color $r $g $b -size $::scExitSize \
		-point "guiPoint" \
		-pointColor 1 0 0 \
		-pressColor 1 1 0 \
		-select $cb \
		-shadow]
    return $id
}

#------------------------------
# quit - quit the game
#------------------------------

proc quit {} {
    # save user profile
    profile_save [profile_getFname] [profile_getDbName] 1

    # exit game
    exit
}

#----------------------------------------------------------------------
# callbacks
#----------------------------------------------------------------------

#------------------------------
# guiPoint - dummy callback to be called when
#            the mouse pointer points at a 
#            clickable text
#------------------------------

proc guiPoint {} {
}

#------------------------------
# goSim - callback for the "Go" button
#------------------------------

proc goSim {} {
    set fname $::charDir/$::stageName/go
    if {[file exists $fname]} {
       char_startSim $::stageName
    } else {
	puts "gui.tcl, goSim: File $fname not found."
    }
    displayOn false
    gui delall
    preview false
    undoZoom
    displayOn true
    initSimSpeed $::SimRtRatio
    showGoal

    # store system state so we can recover it
    set ::PrevState [list $::levelIdx $::selectIdx]

    # begin scenario
    guiBeginScenario
}

#------------------------------
# goPreview - callback for the "Preview"/"Stop Preview" button
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

#----------------------------------------------------------------------
# constants and variables for level selection
#----------------------------------------------------------------------

# size and location of the level spinbox
set ::LevelSize 12
set ::LevelX    2
set ::LevelY    [expr ($::screenHeight / $::LevelSize) - 5]

set ::newLevel_show   0
set ::newLevel_index -1
set ::newLevel_id    -1
set ::newLevel_r     1
set ::newLevel_g     0.8
set ::newLevel_b     0.2
set ::newLevel_x     3.5
set ::newLevel_y     [expr $::LevelY + 1]

#----------------------------------------------------------------------
# levels
#----------------------------------------------------------------------

# level titles
set ::levels [list "  Training  " \
		   "  Beginner  " \
		   "Intermediate" \
		   "Professional" \
		   "  Extreme   " \
		   "   Studio   "]

# commands to load the list of the different levels
set ::LevelCmds [list loadTList loadBList loadIList loadPList loadEList loadStudioList]

#------------------------------
# level_cb - callback to call when a new level is shown
#   arg = index into the ::levels list
#------------------------------

proc level_cb { arg } {
    if {$::levelIdx != $arg} {
		set ::scenarioBrowseIndex 0
	}
	
	set ::levelIdx $arg
	set ::selectIdx -1

	level_hide
#	level_draw $::levelIdx

    set cb [lindex $::LevelCmds $arg]
    eval $cb

    # remove the "New Level!" text if it was shown and the
    # user uses the spinbox to check out the new level

    if {$::newLevel_show} {
	if {$::newLevel_index == $arg} {
	    set ::newLevel_show 0
	    gui del $::newLevel_id
	    set ::newLevel_id -1
	    gui redraw
	}
    }
}

# load the different levels
source training.tcl
source beginner.tcl
source intermediate.tcl
source pro.tcl
source extreme.tcl
source studio.tcl

# load in modules for display the levels, user profile, and the
# current user
source level.tcl
source profile.tcl
source user.tcl
source char.tcl

# load all user profiles
profile_load [profile_getFname] [profile_getDbName]

# load user
user_load [user_getFname]

# display user
set ::userSize 14
set ::userX    0.5
set ::userY [expr ($::screenHeight / $::userSize) - 1]
#gui add text $::userX $::userY "Player: $::user" -color 1 1 1 \
#    -size $::userSize \
#    -shadow

# open levels for user according to the user profile
set openedLevels [profile_openLevels [profile_getDbName] $::user $::levels]

# create spinbox for selecting different levels
sb_new level 0.5 $::LevelY 12 $levels \
    $::LevelSize level_cb 0 1 0 1 0 0 1 1 0

#----------------------------------------------------------------------
# Initialize the levels completed
#----------------------------------------------------------------------

set ::levelDepths [list [llength $::t_labels] \
		       [llength $::b_labels] \
		       [llength $::i_labels] \
		       [llength $::p_labels]]

# save the user's profile in the levels completed
level_setComp [profile_lookup [profile_getDbName] $::user]


#----------------------------------------------------------------------
# Go! button
#----------------------------------------------------------------------

# size and location
set ::GoSize 14
set ::GoY 15
set ::GoId -1

#------------------------------
# enableGo - enable/disable the "Go!" button
#   e = 0 to disable, 1 to enable
#------------------------------

proc enableGo { e } {
    set x [expr $::MenuIndent + 1.5]
    if {$e} {
	if {$::GoId > -1} { gui del $::GoId }
	set ::GoId [gui add text $x $::GoY "Go!"  -size $::GoSize \
			-color 1 1 1 \
			-point "guiPoint" \
			-pointColor 1 0 0 \
			-pressColor 1 1 0 \
			-select "goSim" \
			-shadow]
    } else {
	if {$::GoId > -1} { gui del $::GoId }
	set ::GoId [gui add text $x $::GoY "Go!"  -size $::GoSize \
			-color 0.5 0.5 0.5 \
			-shadow]
    }
}

#----------------------------------------------------------------------
# Preview button
#----------------------------------------------------------------------

# size and location
set ::PvSize 14
set ::PvX 18
set ::PvY 4.5
set ::PvId     -1

#------------------------------
# enablePreview - enable/disable the Preview button
#   e = 0 to disable, 1 to enable
#------------------------------

proc enablePreview { e } {
    if {$e} {
	if {$::PvId > -1} { gui del $::PvId }
	set ::PvId [gui add text $::PvX $::PvY "Preview"  -size $::PvSize \
			-color 1 1 1 \
			-point "guiPoint" \
			-pointColor 1 0 0 \
			-pressColor 1 1 0 \
			-select "goPreview" \
			-shadow]
    } else {
	if {$::PvId > -1} { gui del $::PvId }
	set ::PvId [gui add text $::PvX $::PvY "Preview"  -size $::PvSize \
			-color 0.5 0.5 0.5 \
			-shadow]
    }
}

if {$::PvId == -1} {
#    enablePreview 0
}

#----------------------------------------------------------------------
# preview window
#----------------------------------------------------------------------

#  preview true <startX> <startY> <widthX> <widthY>
preview true 0.3 0.3 0.55 0.55

# previewNewStage "$::charDir/$::stageName/setup"

#------------------------------
# goInfo - callback to call to bring up
#          the info screen
#------------------------------

proc goInfo {} {
    # store system state so we can recover it
    set ::PrevState [list $::levelIdx $::selectIdx]

    < info.tcl
}

#------------------------------
# goSetup - callback to call to bring up the 
#           setup screen
#------------------------------

proc goSetup {} {
    forceScenarioReload
    level_hide
    scenario_delete
    < setup.tcl
}

#------------------------------------------
# goPlayFiles - playback files from given directory for demo loop
#------------------------------------------

proc goPlayFiles { dir } {
  char_startPreview $dir
  goSim
  set fileList [glob $::RootDir/scripts/ski/$dir/anim/*]
  set nf [llength $fileList]
  set nplay 1
  if {$nplay > $nf} { set nplay $nf }
  set f1 -1
  set f2 -1
  while {$nplay > 0} {
     set f [expr int(rand() * $nf)]
       if { $f != $f1 && $f != $f2} {
         set f2 $f1
         set f1 $f
	 set fname [lindex $fileList $f]
         buf_cload "$fname"
         set ::sim_rec 1
         play logbuf
         if {$::stopDemo==1} break
         set nplay [expr $nplay - 1]
       }
  }
}

#------------------------------------------
# goDemoLoop - callback to playback demo loop file
#------------------------------------------

proc goDemoLoop {} {
    set demoDirList {road camelCrates ravineJet camelTrees deathValley gapCrumbleShell hutRock jump3}
    set ndir [llength $demoDirList]
    set x 20
    set d1 -1
    set d2 -1
    set d3 -1
    while {$x > 0} {
	set d [expr int(rand() * $ndir)]
        set dir [lindex $demoDirList $d]
        goPlayFiles $dir
        if {$::stopDemo==1} break
    }
    set ::stopDemo 0
    < scenarioEnd.tcl
}


#------------------------------------------
# goPlayback - callback to call to playback arbitrary .az file
#------------------------------------------

proc goPlayback {} {
   ## get file name
   set fname [tk_getOpenFile -initialdir $::RootDir/saved]
   if {[string length fname] > 0} {
       set scenario [getScenario $fname]
       char_startPreview $scenario
       goSim
       buf_cload "$fname"
       gui delall
       set ::sim_rec 1
       play logbuf
       < scenarioEnd.tcl
   }
}

#----------------------------------------------------------------------
# draw background
#----------------------------------------------------------------------

gui bg
gui add pic props/blueGrad_small.jpg 0 0 1 1
gui fg

#----------------------------------------------------------------------
# recover previous level/scenario states after the end
# of a previous scenario
#----------------------------------------------------------------------

if {[info var ::PrevState] != {}} {
    
    # -- previous state exists so we are returning to the main menu
    #    after visiting a scenario

    set li [lindex $::PrevState 0]
    set si [lindex $::PrevState 1]

    # By default, we do not open a new level.
    #
    # We only open up new levels when the user finishes a scenario
    # in the highest level she has opened, and she has completed
    # a certain fraction of scenarios in that level.
    set openNewLevel 0

    if {$::skierState == $::nextStage} {
	level_set $li $si 2

	# update the user profile
	set userProfile [profile_lookup [profile_getDbName] $::user]
	if {$userProfile != {}} {
	    profile_set [profile_getDbName] $::user [level_getComp]
	    profile_save [profile_getFname] [profile_getDbName] 0
	}

	# check to see if should open up a new level
	set levelsOpened [llength $userProfile]
	if {$li == [expr $levelsOpened - 1]} {
	    # highest level
	    set nc [profile_numScenarioComp [lindex $userProfile $li]]
	    set ns [llength [lindex $userProfile $li]]

	    if {$nc >= [expr int($ns * $::LevelOpenTh)]} {
		if {$li < [expr [llength $::levelDepths] - 1]} {
		    set openNewLevel 1
		}
	    }
	}
    }

    # update the spin box 
    sb_set level $li
    scenario_select_cb $si
    level_hide
 #   level_draw $li
    unset ::PrevState

    if {$openNewLevel} {
	# open up new level...
	#   display text to indicate that a new level is available

	set ::newLevel_show 1
	set ::newLevel_index [expr $li + 1]
	set ::newLevel_id [gui add text \
			       $::newLevel_x $::newLevel_y "New Level!  $::RightArrow" \
			       -size $::LevelSize \
			       -color $::newLevel_r $::newLevel_g $::newLevel_b \
			       -shadow]
	gui redraw
	addLevel
    }
} else {
    # -- previous state does not exist so we just got to the main menu
    #    for the very first time

    sb_set level 0
    level_hide
#    level_draw 0
}

#----------------------------------------------------------------------
# init states
#----------------------------------------------------------------------

# clear flag before the next scenario to indicate that 
# no animation playback/save should be allowed until
# the user run the simulation

set ::sim_rec 0

#----------------------------------------------------------------------
# helper routines
#----------------------------------------------------------------------

#------------------------------
# addLevel - allow the user to access one additional level (if it exists)
#------------------------------

proc addLevel {} {
    set userProfile [profile_lookup [profile_getDbName] $::user]
    if {$userProfile != {}} {
	if {[llength $userProfile] < [llength $::levelDepths]} {
	    set newLevel [llength $userProfile]
	    set l [empty_level [lindex $::levelDepths $newLevel]]
	    lappend userProfile $l
	    profile_set [profile_getDbName] $::user $userProfile
	    
	    # update the spinbox

	    # - get original index
	    set lOrig [sb_get level]
	    set sOrig $::selectIdx

	    # - delete old spinbox
	    sb_delete level
	    
	    # - setup new spinbox
	    set openedLevels [profile_openLevels [profile_getDbName] \
				  $::user $::levels]
	    
	    sb_new level 0.5 $::LevelY 12 $openedLevels \
		$::LevelSize level_cb 0 1 0 1 0 0 1 1 0

	    # - reset index
	    sb_set level $lOrig
	    scenario_select_cb $sOrig

	    profile_save [profile_getFname] [profile_getDbName] 0
	}
    }
}

proc keyO {} {
  addLevel
}

#------------------------------
# setChar - select a charcter
#   ci = character index
#------------------------------

proc setChar { ci } {
    # do nothing if we are selecting the current character
    if {$ci == [char_getChar]} { return }

    forceScenarioReload

    # select character and reload 
    char_setChar $ci
    < gui.tcl
}

#------------------------------
# forceScenarioReload - set the previous level & scenario states
#                       so that they will be reloaded the next
#                       time this script is run
#------------------------------

proc forceScenarioReload {} {
    # remember the current scenario
    set ::PrevState [list $::levelIdx $::selectIdx]

    set ::levelIdx -1
    set ::selectIdx -1
}

#--------------------------------
# create 'Go!' and 'Quit' buttons
#--------------------------------

########### Go! button
if {$::GoId == -1} {
    enableGo 0
}

########### Playback 
set y [expr $::GoY - 2.5]
set ::PlaybackId   [gui add text $::MenuIndent $y "Playback" \
		    -size $::defFontSize \
		    -color 1 1 1 \
		    -point "guiPoint" \
		    -pointColor 1 0 0 \
		    -pressColor 1 1 0 \
		    -select "goPlayback" \
		    -shadow]

########### Demo Loop button
set y [expr $y - 2]
set ::DemoloopId   [gui add text $::MenuIndent $y "Demo Loop" \
		    -size $::defFontSize \
		    -color 1 1 1 \
		    -point "guiPoint" \
		    -pointColor 1 0 0 \
		    -pressColor 1 1 0 \
		    -select "goDemoLoop" \
		    -shadow]


########### Info button
set ::InfoSize 14
set y [expr $y - 2]
set ::InfoId   [gui add text $::MenuIndent $y "Info" \
		    -size $::defFontSize \
		    -color 1 1 1 \
		    -point "guiPoint" \
		    -pointColor 1 0 0 \
		    -pressColor 1 1 0 \
		    -select "goInfo" \
		    -shadow]

########### Setup button
set y [expr $y - 2]
set ::SetupId   [gui add text $::MenuIndent $y "Setup" \
		    -size $::defFontSize \
		    -color 1 1 1 \
		    -point "guiPoint" \
		    -pointColor 1 0 0 \
		    -pressColor 1 1 0 \
		    -select "goSetup" \
		    -shadow]

proc goFullScreen {} {
  set ::FullScreen 1
  gui del $::fsID
  fullscreen
}

########### FullScreen button
if {$::FullScreen == 0} {
  set y [expr $y - 2]
    set ::fsID [setScExit "Full Screen" 1 1 1 "goFullScreen" $y]
}

########### Quit button
set y [expr $y - 3]

setScExit "  Quit" 1 1 1 "quit" $y

#----------------------------------------------------------------------
#   deal with AutoStart
#----------------------------------------------------------------------

if {$::AutoStart == 1} {
  set ::AutoStart 0
  char_startPreview $::AutoScenario
#  set ::stuntEval stuntEval_1.tcl
  goSim
  buf_cload "$::AutoStartFile"
#  < ski/common.keys
  gui delall
  set ::sim_rec 1
  play logbuf
  < scenarioEnd.tcl
} else {
  ##################### refresh screen
#  level_cb $::levelIdx
  gui redraw          
}

