#----------------------------------------------------------------------
# util.tcl
# - utility functions
#----------------------------------------------------------------------

#------------------------------
# linkToPts
# - convert a .afig file into a point file
# - extract the coordinates and
# - reverse the line order
#------------------------------

proc linkToPts { inFname outFname } {
    
    if {[file exists $inFname]} {
	set istr [open $inFname r]
	set ostr [open $outFname w]

	set oTxt ""
	
	while {[eof $istr] == 0} {
	    gets $istr line
            if {[lindex $line 0] == "new_pt"} {
                set x [lindex $line 1]
                set y [lindex $line 2]
		set oTxt "$x $y\n$oTxt"
            }
	}
	puts $ostr $oTxt
	close $istr
	close $ostr
    } else {
	puts "Error: file not found";
    }
}

#------------------------------
# ptsToGround - convert 2D points to points suitable for ground files
#------------------------------

proc ptsToGround { inFname outFname cfric } {
    if {[file exists $inFname]} {
	set istr [open $inFname r]
	set ostr [open $outFname w]

	set oTxt ""
	
	while {[eof $istr] == 0} {
	    gets $istr line
	    if {$line != ""} {
		set oTxt "$oTxt$line $cfric 0.5\n"
	    }
	}
	puts $ostr $oTxt
	close $istr
	close $ostr
    } else {
	puts "Error: file not found";
    }
}

#------------------------------
# revLineOrder - reverse the line order in a file
#------------------------------

proc revLineOrder { inFname outFname } {
    
    if {[file exists $inFname]} {
	set istr [open $inFname r]
	set ostr [open $outFname w]

	set oTxt ""
	
	while {[eof $istr] == 0} {
	    gets $istr line
	    set oTxt "$line\n$oTxt"
	}

	puts $ostr $oTxt
	close $istr
	close $ostr
    } else {
	puts "Error: file not found";
    }
}

#------------------------------
# saveanim - save a copy of the current text (uncompressed) animation
#            in a file in the current scenario's animation directory
#------------------------------

proc saveanim { name } {
    global ::scenarioDir
    file copy log.ani $scenarioDir/anim/$name
}

#------------------------------
# delTree - recursively delete matching files in a directory tree
#------------------------------

proc delTree { srcDir pattern } {

    set fail [catch {set files [glob $srcDir/*]} returnString]
    if {!$fail} {
	foreach f $files {
	    set t [file tail $f]

	    if {[file isdirectory $f]} {
		delTree $f $pattern
	    } elseif {[file isfile $f]} {
		set fl [list $f]
		if {[lsearch $fl $pattern] == 0} {
		    puts "file del $f"
		    file del $f
		}
	    }
	}
    }
}

#------------------------------
# treeSize - get the number of files in a directory tree
#------------------------------

proc treeSize { srcDir avoidList } {
    return [treeSizeHelper $srcDir $avoidList 0]
}

#------------------------------
# treeSizeHelper - helper function for treeSize
#------------------------------

proc treeSizeHelper { srcDir avoidList s } {

    set fail [catch {set files [glob $srcDir/*]} returnString]
    if {!$fail} {
	foreach f $files {
	    set t [file tail $f]
	    set aIndex [lsearch $avoidList $t]
	    set bkup [expr [lsearch [list $t] "*~"] >= 0]

	    if {[expr {$aIndex < 0} && {!$bkup}]} {
		if {[file isdirectory $f]} {
		    set s [treeSizeHelper $f $avoidList $s]
		} elseif {[file isfile $f]} {
		    file lstat $f ls
		    set s [expr $s + $ls(size)]
		}
	    }
	}
    }

    return $s
}

# directory and files to examine when determining tree size
set sDir ../scripts
set flist [list $sDir/bkup.tcl \
	       $sDir/glshMenu.tk \
	       $sDir/logo.jpg \
	       $sDir/logo.setup \
	       $sDir/logo.txfm \
	       $sDir/logo.wobj \
	       $sDir/playback.tcl \
	       $sDir/ui \
	       $sDir/util.tcl \
	       $sDir/ski/common.setup \
	       $sDir/ski/common.keys \
	       $sDir/ski/crashEval.tcl \
	       $sDir/ski/default.map \
	       $sDir/ski/jetPack.skin \
	       $sDir/ski/jetPack.wobj \
	       $sDir/ski/jetPackFlame.skin \
	       $sDir/ski/jetPackFlame.wobj \
	       $sDir/ski/jetPackFlame_tess.wobj \
	       $sDir/ski/jetPack_tess.wobj \
	       $sDir/ski/landEval.tcl \
	       $sDir/ski/skier.afig \
	       $sDir/ski/skier_arm.skin \
	       $sDir/ski/skier_arm_tess.wobj \
	       $sDir/ski/skier_body.skin \
	       $sDir/ski/skier_body_tess.wobj \
	       $sDir/ski/skier_lski.skin \
	       $sDir/ski/skier_rski.skin \
	       $sDir/ski/skier_ski.afig \
	       $sDir/ski/skier_ski_tess.wobj \
	       $sDir/ski/skier_tex.jpg \
	       $sDir/ski/standin.setup \
	       $sDir/ski/stuntEval.tcl \
	       $sDir/santaNew/common.setup \
	       $sDir/santaNew/santa.afig \
	       $sDir/santaNew/santa.jpg \
	       $sDir/santaNew/santa.keys \
	       $sDir/santaNew/santa.wobj \
	       $sDir/santaNew/santa_arm.skin \
	       $sDir/santaNew/santa_arm_tess.wobj \
	       $sDir/santaNew/santa_body.skin \
	       $sDir/santaNew/santa_body_tess.wobj]


#------------------------------
# miscSize - compute total size of a list of files
#------------------------------

proc miscSize { flist } {
    set s 0
    foreach f $flist {
	file lstat $f l
	set s [expr $s + $l(size)]
    }

    return $s
}

#------------------------------
# oldAz - rename foo.az files to _foo.az
#------------------------------

proc oldAz { dirName } {
    set fail [catch {set files [glob $dirName/*.az]} returnString]
    if {$fail == 0} {
	foreach f $files {
	    set t [file tail $f]
	    set d [file dirname $f]
	    puts "file rename $f $d/_$t"
	    file rename $f $d/_$t
	}
    }
}

#------------------------------
# maxStrLen - determine the length of the longest string in a list
#------------------------------

proc maxStrLen { strList } {
    set m 0
    foreach s $strList {
	set sl [string length $s]
	if {$sl > $m} {
	    set m $sl
	}
    }
    return $m
}

#------------------------------
# centerStr - center a list of strings by prepending short strings with
#             extra spaces
#------------------------------

proc centerStr { str l } {
    set outStr $str

    set sl [string length $str]
    set fp [expr ($l - $sl) / 2]
    for {set p 0} {$p < $fp} {set p [expr $p + 1]} {
	set outStr " $outStr"
    }

    return $outStr
}

