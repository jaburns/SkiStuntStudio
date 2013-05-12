#----------------------------------------------------------------------
# playback.tcl 
# - maintains a "playback stack"
# - system states are pushed onto the stack before the start of
#   a (nested) playback
# - system states are poped at the end of playback
#----------------------------------------------------------------------

# the stack itself
set ::pbStackList {}

#------------------------------
# pbStack - function for pushing and poping commands
#           from the playback stack
#   argv = (push ... | undo ....)
#   push = push <cmd>
#   undo = undo
# 
# e.g. pbStack push "puts animation1"
#      play animation1
#      pbStack push "puts animation2"
#      play animation2
#      pbStack undo
#      -> animation2
#         animation1
#------------------------------

proc pbStack { argv } {
    global ::pbStackList
    set cmd [lindex $argv 0]

    if {$cmd == "push"} {
	set pbCmd [lindex $argv 1]
	set ::pbStackList [lappend ::pbStackList $pbCmd]
    } elseif {$cmd == "undo"} {
	foreach pbCmd $::pbStackList {
	    eval $pbCmd
	}
	set ::pbStackList {}
    }
}