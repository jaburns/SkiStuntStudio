#----------------------------------------------------------------------
# dynVerif.tcl - routines to assist the verification of dynamics
#----------------------------------------------------------------------

#------------------------------
# revMouseX - reverse the x-component of the normalized mouse coordinates
#------------------------------

proc revMouseX { inFname outFname } {

    if {[file exists $inFname]} {
	set istr [open $inFname r]
	set ostr [open $outFname w]

	while {[eof $istr] == 0} {
	    gets $istr line

	    # read in the x and y values
	    set numArg [scan $line "%f %f" x y]

	    if { $numArg == 2 } {
		# invert the normalized x value
		set x [expr 1 - $x]
		puts $ostr "$x $y"
	    }
	}
	close $istr
	close $ostr
    } else {
	puts "Error: file not found";
    }

}

#------------------------------
# revGroundX - reverse the x-component of the ground data about a given
#              pivot point
#------------------------------

proc revGroundX { inFname outFname pivotX } {

    if {[file exists $inFname]} {
	set istr [open $inFname r]
	set ostr [open $outFname w]

	set twoPivotX [expr $pivotX * 2]

	while {[eof $istr] == 0} {
	    gets $istr line

	    # read in the x and y values
	    set numParam [scan $line "%f %f %f %n" x y cfric num]
	    if { $numParam >= 3 } {
		set restLine [string range $line $num end]
		set x [expr $twoPivotX - $x]
		puts $ostr "$x $y $cfric $restLine"
	    } else {
		puts $ostr $line
	    }
	}
	close $istr
	close $ostr
    } else {
	puts "Error: file not found";
    }
    
}


#------------------------------
# transGroundX - translate the ground vertices in the x direction
#------------------------------

proc transGroundX { inFname outFname dx } {

    if {[file exists $inFname]} {
	set istr [open $inFname r]
	set ostr [open $outFname w]

	while {[eof $istr] == 0} {
	    gets $istr line

	    # read in the x and y values
	    set numParam [scan $line "%f %f %f %n" x y cfric num]
	    if { $numParam >= 3 } {
		set restLine [string range $line $num end]
		set x [expr $x + $dx]
		puts $ostr "$x $y $cfric $restLine"
	    } else {
		puts $ostr $line
	    }
	}
	close $istr
	close $ostr
    } else {
	puts "Error: file not found";
    }

}

source util.tcl
