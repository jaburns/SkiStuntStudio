#----------------------------------------------------------------------
# crashEval.tcl
# - make comments about crashes
#----------------------------------------------------------------------

# Crashes are divided into 4 levels of severity.

# fraction of time to make comments for the 4 levels of crashes
set ::commentFrac1 0.7
set ::commentFrac2 0.8
set ::commentFrac3 0.9
set ::commentFrac4 1.0

# sound files to play for the different levels
set ::comment1 [list $::soundDir/bummer.wav \
		   $::soundDir/bummerDude.wav]
set ::comment2 [list $::soundDir/hurt.wav \
		    $::soundDir/justSim.wav]
set ::comment3 [list $::soundDir/hateIt.wav \
		   $::soundDir/bunnyHill.wav \
		    $::soundDir/dontTry.wav]
set ::comment4 [list $::soundDir/crazyNut.wav \
		    $::soundDir/ambulance.wav \
		    $::soundDir/history.wav \
		   $::soundDir/noSkiersHurt.wav]

# flag for disabling crash commenting
set ::noCrashComment 0

# fraction of time to say "Ooooo" when a crash occurs
set ::awwTh          0.6

namespace eval crashEval {

    variable outComment 0
    
    namespace export sideEffects
    proc sideEffects { arg } {
	variable outComment
	if {$::skierState == $::out} {
	    if {$outComment == 0} {
		set fx 0
		set fy 0
		for {set i 0} {$i < $::numLinks} {set i [expr $i + 1]} {
		    set fix [expr $i * 2]
		    set fiy [expr $i * 2 + 1]
		    set fx [expr $fx + [lindex $::crashLandF $fix]]
		    set fy [expr $fy + [lindex $::crashLandF $fiy]]
		}

		set comLevel 0

		# use fy to determine the crash severity
		if {$fy > 13000} {
		    if {$fy < 16000} {
			set comLevel [max $comLevel 1]
		    } elseif {$fy < 20000} {
			set comLevel [max $comLevel 2]
		    } elseif {$fy < 25000} {
			set comLevel [max $comLevel 3]
		    } else {
			set comLevel [max $comLevel 4]
		    }
		} 

		# use fx to determine the crash severity
		if {$fx > 13000} {
		    if {$fx < 16000} {
			set comLevel [max $comLevel 1]
		    } elseif {$fx < 20000} {
			set comLevel [max $comLevel 2]
		    } elseif {$fx < 25000} {
			set comLevel [max $comLevel 3]
		    } else {
			set comLevel [max $comLevel 4]
		    }
		} 

		# make comment
		if {$::noCrashComment} { return }
		if {$comLevel > 0} {
		    set outComment 1

		    # awww
		    if {[expr rand() < $::awwTh]} {
			sound queue cheer $::soundDir/awww3Clip.wav
		    }

		    # crash comment
		    if {$comLevel == 1} {
			set ci [expr int(rand() * [llength $::comment1])]
			set c [lindex $::comment1 $ci]
			playComAtTimes $c $::commentFrac1
		    } elseif {$comLevel == 2} {
			set ci [expr int(rand() * [llength $::comment2])]
			set c [lindex $::comment2 $ci]
			playComAtTimes $c $::commentFrac2
		    } elseif {$comLevel == 3} {
			set ci [expr int(rand() * [llength $::comment3])]
			set c [lindex $::comment3 $ci]
			playComAtTimes $c $::commentFrac3
		    } elseif {$comLevel == 4} {
			set ci [expr int(rand() * [llength $::comment4])]
			set c [lindex $::comment4 $ci]
			playComAtTimes $c $::commentFrac4
		    }
		}
	    }
	}
    }

    namespace export reset
    proc reset {} {
	variable outComment
	set outComment 0

	set ::noCrashComment 0
    }
}


#------------------------------
# max - return the larger of two numbers
#------------------------------

proc max { n1 n2 } {
    if {$n1 > $n2} { return $n1 }
    return $n2
}