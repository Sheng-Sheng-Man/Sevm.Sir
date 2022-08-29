Import
    Use "host"
End Import

Data
    $0 String "Hello World"
    $1 String "print"
End Data

Define
    Private $2 str
End Define

Func
    Private @1 main
End Func

Code
    @1
        Lea #2, $0
        Ptr @2, #2
        Ptr $3
        List $3
        Lea #2, $2
        Ptrl $2, 0, #2
        Lea #0, $3
        Call [0], $1
End Code
