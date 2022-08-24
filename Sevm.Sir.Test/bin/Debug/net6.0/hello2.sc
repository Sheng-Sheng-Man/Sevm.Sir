; 这段程序模拟实现加法
; a=3
; b=5
; c=a+b
; str="a+b的和是"+c
; print(str)

import
    use "host"
end import

data
    [1] number 3; 添加一个数字虚拟内存空间    
    [2] number 5            
    [3] string "a+b的和是"; 添加一个字符串虚拟内存空间 
    [4] string "print"
end data

define
    $0 a 1; 定义一个变量a,指向第6个虚拟内存空间
    $1 b 2; 定义一个变量b,指向第7个虚拟内存空间
    $2 c 5
    $3 str 6
end define

func
    @1 main
    @2 print; 定义编号为1的函数，外部暴露名称为print
end func

code
    ; 定义输出函数print
    @2
        call [0], [4]; 将临时变量$1中的所有项目进行连接，再将结果赋值给变量str
        ret 0
    @1
        ; c=a+b
        mov #2, $0; 将变量a中的内容赋值给0号寄存器
        add #2, $1
        mov $2, #2
        ; str="a+b的和是"+c
        new $4; 申请一个临时变量$4,并指向新的虚拟内存空间
        list $4; 设置临时变量$4为一个列表
        mov #0, 1
        mov #1, 0
        ptr $4, 3; 临时变量$4列表的第1项指向第3个虚拟内存空间
        new $5;
        mov #0, 0
        lea #2, $5
        mov #0, 1
        mov #1, 1
        ptr $4, #2; 临时变量$4列表的第2项指向临时变量$5的虚拟内存空间
        mov $5, $2
        join $3, $4; 将临时变量$4中的所有项目进行连接，再将结果赋值给变量$3
        ; print(str)
        new $6;
        list $6;
        mov #0, 0
        lea #2, $3
        mov #0, 1
        mov #1, 0
        ptr $6, #2
        mov #0, 0
        lea #0, $6; 将临时变量$3的地址赋值给0号寄存器
        call [0], @2
end code
