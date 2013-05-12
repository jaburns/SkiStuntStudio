set ::studio_labels [list \
"Dark Cavern" \
"Jungle Jetpack" \
"Paper World" \
"Demo Level" \
]
set ::studio_stages [list \
"char_startPreview studio/0" \
"char_startPreview studio/2" \
"char_startPreview studio/3" \
"char_startPreview studio/6" \
]
proc loadStudioList {} {
scenario_delete
scenario_new $::LevelX [expr $::LevelY - 2] $::LevelSize \
$::studio_labels $::studio_stages
if {[info var ::PrevState] == {}} {
scenario_select_cb 0
}
}
