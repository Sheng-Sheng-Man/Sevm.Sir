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
sir.Codes.Add(Sevm.Sir.SirCodeInstructionTypes.Label, SirExpression.Label(1));
// new $2
sir.Codes.Add(Sevm.Sir.SirCodeInstructionTypes.New, SirExpression.Variable(2));
// list $2
sir.Codes.Add(Sevm.Sir.SirCodeInstructionTypes.List, SirExpression.Variable(2));
// lea #2, $1
sir.Codes.Add(Sevm.Sir.SirCodeInstructionTypes.Lea, SirExpression.Storage(2), SirExpression.Variable(1));
// ptr $2, #2
sir.Codes.Add(Sevm.Sir.SirCodeInstructionTypes.Ptrl, SirExpression.Variable(2), 0, SirExpression.Storage(2));
// lea #0, $2
sir.Codes.Add(Sevm.Sir.SirCodeInstructionTypes.Lea, SirExpression.Storage(0), SirExpression.Variable(2));
// call [0], [2]
sir.Codes.Add(Sevm.Sir.SirCodeInstructionTypes.Call, SirExpression.IntPtr(0), SirExpression.IntPtr(2));
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