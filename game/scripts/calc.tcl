proc txCalc { xImg yImg } {
    set pivx 8.85454
    set pivy 486.961
    set orgx -9.96252
    set orgy 0.36688
    set sx 0.0913476
    set sy 0.0918559
    set xRes 1024
    set yRes 512
    set xp [expr $sx*($xImg-$pivx)+$orgx]
    set yp [expr $sy*($yImg-$pivy)+$orgy]
    set xTex [expr 1.0*$xImg/$xRes]
    set yTex [expr 1.0 - 1.0*$yImg/$yRes]
    puts $xp,$yp
    puts $xTex,$yTex
}

proc tfm { xP yP } {
    set pivx 8.85454
    set pivy 486.961
    set orgx -9.96252
    set orgy 0.316688
    set sx 0.0913476
    set sy 0.0918559
    set xRes 1024
    set yRes 512
    set xImg [expr ($xP-$orgx)/$sx+$pivx]
    set yImg [expr ($yP-$orgy)/$sy+$pivy]
    set xTex [expr 1.0*$xImg/$xRes]
    set yTex [expr 1.0 - 1.0*$yImg/$yRes]
    puts $xImg,$yImg
    puts $xTex,$yTex
}


