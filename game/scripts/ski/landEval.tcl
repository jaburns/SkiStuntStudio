#----------------------------------------------------------------------
# landEval.tcl
# - make comments about landings
#----------------------------------------------------------------------

# force threshold for smooth landings
set ::smoothLandFy 4500

# force threshold for super smooth landings
set ::superLandFy  2500

# minimum on-ground time before a landing is detected
set ::minLandTime  1.0

namespace eval landEval {
    proc processJump {} {
	set jumpTime [lindex $::jumpInfo 0]
	
	if {$jumpTime > $::minLandTime} {
	    set fy 0
	    for {set i 0} {$i < $::numLinks} {set i [expr $i + 1]} {
		set fiy [expr $i * 2 + 1]
		set fy [expr $fy + [lindex $::maxLandF $fiy]]
	    }
	    
	    puts $fy
	    
	    if {$fy < $::superLandFy} {
		playCom $::soundDir/verySmooth.wav 0
	    } elseif {$fy < $::smoothLandFy} {
		playCom $::soundDir/smoothLanding.wav 0
	    } 
	}

	if {$::landCount > [expr $::maxLandTh * 1.3]} {
	    puts "lucky!"
	}
    }
    
    namespace export reset
    proc reset {} {
    }
}