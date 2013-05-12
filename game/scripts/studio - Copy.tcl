set ::studio_labels [list \
"Skill Course + Bombs" \
"Test Level" \
"Rhombus" \
"Jetpack Collect"
]
set ::studio_stages [list \
"char_startPreview studio/extremeSkill1" \
"char_startPreview studio/jabTestingGrounds" \
"char_startPreview studio/jabRhombus" \
"char_startPreview studio/jabTestJetpack" \
]
proc loadStudioList {} {
scenario_delete
scenario_new $::LevelX [expr $::LevelY - 2] $::LevelSize \
$::studio_labels $::studio_stages
if {[info var ::PrevState] == {}} {
scenario_select_cb 0
}
}