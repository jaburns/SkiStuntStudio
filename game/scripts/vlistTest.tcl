set labels [list zero one two three four five]
set size 14

proc cb { arg } {
    puts "test $arg"
}

vl_new test 2 10 $labels $size cb 1 1 1 1 0 0 1 1 0
