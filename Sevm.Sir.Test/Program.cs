// See https://aka.ms/new-console-template for more information
using Sevm.Sir;

/*
* 此段代码模拟简单的程序
* string str="Hello World"
* print(str)
*/

Sevm.Sir.SirScript sir = new Sevm.Sir.SirScript();
// 添加引入
sir.Imports.Add(Sevm.Sir.SirImportTypes.Use, "host");
// 添加数据
sir.Datas.Add(1, "Hello World");
sir.Datas.Add(2, "print");
// 添加变量定义
sir.Defines.Add(1, "str", 1);
// 添加函数定义
sir.Funcs.Add(1, "main");
// @1
sir.Codes.Add(Sevm.Sir.SirCodeInstructionTypes.Label, Sevm.Sir.SirExpressionTypes.Label, 1);
// new $2
sir.Codes.Add(Sevm.Sir.SirCodeInstructionTypes.New, Sevm.Sir.SirExpressionTypes.Variable, 2);
// list $2
sir.Codes.Add(Sevm.Sir.SirCodeInstructionTypes.List, Sevm.Sir.SirExpressionTypes.Variable, 2);
// mov #0, 0
sir.Codes.Add(Sevm.Sir.SirCodeInstructionTypes.Mov, Sevm.Sir.SirExpressionTypes.Storage, 0, Sevm.Sir.SirExpressionTypes.Value, 0);
// lea #2, $1
sir.Codes.Add(Sevm.Sir.SirCodeInstructionTypes.Lea, Sevm.Sir.SirExpressionTypes.Storage, 2, Sevm.Sir.SirExpressionTypes.Variable, 1);
// mov #0, 1
sir.Codes.Add(Sevm.Sir.SirCodeInstructionTypes.Mov, Sevm.Sir.SirExpressionTypes.Storage, 0, Sevm.Sir.SirExpressionTypes.Value, 1);
// mov #1, 0
sir.Codes.Add(Sevm.Sir.SirCodeInstructionTypes.Mov, Sevm.Sir.SirExpressionTypes.Storage, 1, Sevm.Sir.SirExpressionTypes.Value, 0);
// ptr $2, #2
sir.Codes.Add(Sevm.Sir.SirCodeInstructionTypes.Ptr, Sevm.Sir.SirExpressionTypes.Variable, 2, Sevm.Sir.SirExpressionTypes.Storage, 2);
// mov #0, 0
sir.Codes.Add(Sevm.Sir.SirCodeInstructionTypes.Mov, Sevm.Sir.SirExpressionTypes.Storage, 0, Sevm.Sir.SirExpressionTypes.Value, 0);
// lea #0, $2
sir.Codes.Add(Sevm.Sir.SirCodeInstructionTypes.Lea, Sevm.Sir.SirExpressionTypes.Storage, 0, Sevm.Sir.SirExpressionTypes.Variable, 2);
// call [0], [2]
sir.Codes.Add(Sevm.Sir.SirCodeInstructionTypes.Call, Sevm.Sir.SirExpressionTypes.IntPtr, 0, Sevm.Sir.SirExpressionTypes.IntPtr, 2);
string str = sir.ToString();
Console.WriteLine("[SIR]");
Console.WriteLine(str);
string localPath = AppDomain.CurrentDomain.BaseDirectory;
Console.WriteLine("[OUTPUT]");
Console.WriteLine(localPath);
string pathHelloSc = $"{localPath}hello.sc";
string pathHelloSbc = $"{localPath}hello.sbc";
using (var f = System.IO.File.Open(pathHelloSc, FileMode.Create)) {
    f.Write(System.Text.Encoding.UTF8.GetBytes(str));
    f.Close();
}
using (var f = System.IO.File.Open(pathHelloSbc, FileMode.Create)) {
    f.Write(sir.ToBytes());
    f.Close();
}
Console.WriteLine($"[{pathHelloSc}]");
using (var f = System.IO.File.Open(pathHelloSc, FileMode.Open)) {
    List<byte> ls = new List<byte>();
    byte[] buffer = new byte[1024];
    int len;
    do {
        len = f.Read(buffer, 0, buffer.Length);
        for (int i = 0; i < len; i++) {
            ls.Add(buffer[i]);
        }
    } while (len > 0);
    var script = Parser.GetScript(System.Text.Encoding.UTF8.GetString(ls.ToArray()));
    Console.WriteLine(script.ToString());
    f.Close();
}
Console.WriteLine($"[{pathHelloSbc}]");
using (var f = System.IO.File.Open(pathHelloSbc, FileMode.Open)) {
    List<byte> ls = new List<byte>();
    byte[] buffer = new byte[1024];
    int len;
    do {
        len = f.Read(buffer, 0, buffer.Length);
        for (int i = 0; i < len; i++) {
            ls.Add(buffer[i]);
        }
    } while (len > 0);
    var script = Parser.GetScript(ls.ToArray());
    Console.WriteLine(script.ToString());
    f.Close();
}
string pathHello2Sc = $"{localPath}hello2.sc";
string pathHello2Sbc = $"{localPath}hello2.sbc";
Console.WriteLine($"[{pathHello2Sc}]");
using (var f = System.IO.File.Open(pathHello2Sc, FileMode.Open)) {
    List<byte> ls = new List<byte>();
    byte[] buffer = new byte[1024];
    int len;
    do {
        len = f.Read(buffer, 0, buffer.Length);
        for (int i = 0; i < len; i++) {
            ls.Add(buffer[i]);
        }
    } while (len > 0);
    var script = Parser.GetScript(System.Text.Encoding.UTF8.GetString(ls.ToArray()));
    Console.WriteLine(script.ToString());
    using (var ff = System.IO.File.Open(pathHello2Sbc, FileMode.Create)) {
        ff.Write(script.ToBytes());
        ff.Close();
    }
    f.Close();
}
string pathHello3Sbc = $"{localPath}hello3.sbc";
Console.WriteLine($"[{pathHello3Sbc}]");
using (var f = System.IO.File.Open(pathHello3Sbc, FileMode.Open)) {
    List<byte> ls = new List<byte>();
    byte[] buffer = new byte[1024];
    int len;
    do {
        len = f.Read(buffer, 0, buffer.Length);
        for (int i = 0; i < len; i++) {
            ls.Add(buffer[i]);
        }
    } while (len > 0);
    var script = Parser.GetScript(ls.ToArray());
    Console.WriteLine(script.ToString());
    f.Close();
}