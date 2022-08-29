using System;

namespace Sevm.Sir {

    /// <summary>
    /// 指令类型
    /// </summary>
    public enum SirCodeInstructionTypes {
        /// <summary>
        /// 空指令
        /// </summary>
        None = 0x0000,

        /// <summary>
        /// 标签指令
        /// </summary>
        Label = 0x0101,

        /// <summary>
        /// 传送指令
        /// </summary>
        Mov = 0x0201,
        /// <summary>
        /// 设置指针指令
        /// </summary>
        Ptr = 0x0202,
        /// <summary>
        /// 获取指针指令
        /// </summary>
        Lea = 0x0203,
        /// <summary>
        /// 获取整数部分指令
        /// </summary>
        Int = 0x0204,
        /// <summary>
        /// 获取小数部分指令
        /// </summary>
        Frac = 0x0205,

        /// <summary>
        /// 列表指令
        /// </summary>
        List = 0x0301,
        /// <summary>
        /// 设置列表内容指针
        /// </summary>
        Ptrl = 0x0302,
        /// <summary>
        /// 设置列表内容指针
        /// </summary>
        Leal = 0x0303,
        /// <summary>
        /// 获取内容匹配索引
        /// </summary>
        Idx = 0x0304,
        /// <summary>
        /// 连接指令
        /// </summary>
        Join = 0x0305,
        /// <summary>
        /// 统计指令
        /// </summary>
        Cnt = 0x0306,
        /// <summary>
        /// 对象指令
        /// </summary>
        Obj = 0x0307,
        /// <summary>
        /// 设置对象键列表指针
        /// </summary>
        Ptrk = 0x0308,
        /// <summary>
        /// 设置对象值列表指针
        /// </summary>
        Ptrv = 0x0309,
        /// <summary>
        /// 获取对象键列表指针
        /// </summary>
        Leak = 0x030A,
        /// <summary>
        /// 获取对象值列表指针
        /// </summary>
        Leav = 0x030B,

        /// <summary>
        /// 加法指令
        /// </summary>
        Add = 0x0401,
        /// <summary>
        /// 减法指令
        /// </summary>
        Sub = 0x0402,
        /// <summary>
        /// 乘法指令
        /// </summary>
        Mul = 0x0403,
        /// <summary>
        /// 除法指令
        /// </summary>
        Div = 0x0404,

        /// <summary>
        /// 非指令
        /// </summary>
        Not = 0x0501,
        /// <summary>
        /// 与指令
        /// </summary>
        And = 0x0502,
        /// <summary>
        /// 或指令
        /// </summary>
        Or = 0x0503,
        /// <summary>
        /// 异或指令
        /// </summary>
        Xor = 0x0504,

        /// <summary>
        /// 相等比较指令
        /// </summary>
        Equal = 0x0601,
        /// <summary>
        /// 大于比较指令
        /// </summary>
        Large = 0x0602,
        /// <summary>
        /// 小于比较指令
        /// </summary>
        Small = 0x0603,

        /// <summary>
        /// 无条件跳转指令
        /// </summary>
        Jmp = 0x0701,
        /// <summary>
        /// 带条件跳转指令
        /// </summary>
        Jmpf = 0x0702,
        /// <summary>
        /// 调用指令
        /// </summary>
        Call = 0x0703,
        /// <summary>
        /// 返回指令
        /// </summary>
        Ret = 0x0704,
    }

    /// <summary>
    /// 数据类型
    /// </summary>
    public enum SirDataTypes {
        /// <summary>
        /// 空
        /// </summary>
        None = 0x00,
        /// <summary>
        /// 字符串
        /// </summary>
        String = 0x01,
        /// <summary>
        /// 数字
        /// </summary>
        Number = 0x02,
    }

    /// <summary>
    /// 作用域类型
    /// </summary>
    public enum SirScopeTypes {
        /// <summary>
        /// 私有
        /// </summary>
        Private = 0x00,
        /// <summary>
        /// 公开
        /// </summary>
        Public = 0x01,
    }

    /// <summary>
    /// 指令类型
    /// </summary>
    public enum SirExpressionTypes {
        /// <summary>
        /// 空
        /// </summary>
        None = 0x00,
        /// <summary>
        /// 值
        /// </summary>
        Value = 0x01,
        /// <summary>
        /// 指针 []
        /// </summary>
        IntPtr = 0x02,
        /// <summary>
        /// 寄存器 #
        /// </summary>
        Storage = 0x03,
        /// <summary>
        /// 变量 $
        /// </summary>
        Variable = 0x04,
        /// <summary>
        /// 标签 @
        /// </summary>
        Label = 0x05,
    }

    /// <summary>
    /// 解析类型
    /// </summary>
    public enum SirParserTypes {
        /// <summary>
        /// 空
        /// </summary>
        None = 0x00,
        /// <summary>
        /// 引入
        /// </summary>
        Import = 0x01,
        /// <summary>
        /// 数据
        /// </summary>
        Data = 0x02,
        /// <summary>
        /// 定义
        /// </summary>
        Define = 0x03,
        /// <summary>
        /// 函数
        /// </summary>
        Func = 0x04,
        /// <summary>
        /// 代码
        /// </summary>
        Code = 0x05,
    }

    /// <summary>
    /// 解析类型
    /// </summary>
    public enum SirParserLineTypes {
        /// <summary>
        /// 正常
        /// </summary>
        Normal = 0x00,
        /// <summary>
        /// 结束
        /// </summary>
        Finish = 0x01,
    }

    /// <summary>
    /// 引入类型
    /// </summary>
    public enum SirImportTypes {
        /// <summary>
        /// 引入内置函数
        /// </summary>
        Use = 0x01,
        /// <summary>
        /// 引入外部程序
        /// </summary>
        Lib = 0x02,
    }
}
