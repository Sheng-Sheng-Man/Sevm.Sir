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
    $0 number 3; 添加一个数字虚拟内存空间    
    $1 number 5            
    $2 string "a+b的和是"; 添加一个字符串虚拟内存空间 
    $3 string "print"
end data

define
    private $4 a; 定义一个变量a,指向第6个虚拟内存空间
    private $5 b; 定义一个变量b,指向第7个虚拟内存空间
    private $6 c
    private $7 str
end define

func
    private @1 main
    private @2 print; 定义编号为1的函数，外部暴露名称为print
end func

code
    ; 定义输出函数print
    @2
        call [0], $3; // 执行print函数
        ret 0
    @1
        ; 指定 a 的内存地址
        lea #2, $0
        ptr $4, #2
        ; 指定 b 的内存地址
        lea #2, $0
        ptr $4, #2
        ; c=a+b
        mov #2, $4; 将变量a中的内容赋值给0号寄存器
        add #2, $5
        mov $6, #2
        ; str="a+b的和是"+c
        ptr $8; 申请一个临时变量$4,并指向新的虚拟内存空间
        list $8; 设置临时变量$4为一个列表
        lea #2, $2
        ptrl $8, 0, #2; 临时变量$4列表的第1项指向变量$2的虚拟内存空间
        ptr $9;
        lea #2, $9
        ptr $8, 1, #2; 临时变量$4列表的第2项指向临时变量$6的虚拟内存空间
        mov $9, $6
        join $7, $8; 将临时变量$4中的所有项目进行连接，再将结果赋值给变量$3
        ; print(str)
        ptr $10;
        list $10;
        lea #2, $7
        ptrl $10, 0, #2
        lea #0, $10; 将临时变量$10的地址赋值给0号寄存器
        call [0], @2
        ret 0
end code
