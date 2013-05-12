#----------------------------------------------------------------------
# snapshot.tcl
# - routines for taking multiple exposure pictures during simulation
#----------------------------------------------------------------------

#------------------------------
# snapshot - setup parameters for controlling the exposure rate
#            during a multi-exposure shot
#------------------------------

proc snapshot { skip } {
    global erase
    global pbFrameSkip
    global autopanEnable

    if {$skip > 0} {
	set ::erase 0
	set ::pbFrameSkip $skip
	set ::autopanEnable 0
    } else {
	set ::erase 1
	set ::pbFrameSkip 0
	set ::autopanEnable 1
    }
}
