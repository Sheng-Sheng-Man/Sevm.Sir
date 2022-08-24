Import
    Use "host"
End Import

Data
    [1] String "Hello World"
    [2] String "print"
End Data

Define
    $1 str 1
End Define

Func
    @1 main
End Func

Code
    @1
        New $2
        List $2
        Mov #0, 0
        Lea #2, $1
        Mov #0, 1
        Mov #1, 0
        Ptr $2, #2
        Mov #0, 0
        Lea #0, $2
        Call [0], [2]
End Code
