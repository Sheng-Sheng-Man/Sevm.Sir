// See https://aka.ms/new-console-template for more information
using Sevm.Sir;

Console.WriteLine("[SIR]");
Console.WriteLine();
Sevm.Sir.SirScript sir = new Sevm.Sir.SirScript();
sir.Datas.Add(1, "Hello World");
sir.Datas.Add(2, "print");
sir.Defines.Add(1, "str", 1);
sir.Funcs.Add(1, "main");
sir.Codes.Add(Sevm.Sir.SirCodeInstructionTypes.Label, Sevm.Sir.SirCodeParamTypes.Label, 1);
sir.Codes.Add(Sevm.Sir.SirCodeInstructionTypes.New, Sevm.Sir.SirCodeParamTypes.Variable, 2);
sir.Codes.Add(Sevm.Sir.SirCodeInstructionTypes.List, Sevm.Sir.SirCodeParamTypes.Variable, 2);
sir.Codes.Add(Sevm.Sir.SirCodeInstructionTypes.Mov, Sevm.Sir.SirCodeParamTypes.Storage, 0, Sevm.Sir.SirCodeParamTypes.Value, 1);
sir.Codes.Add(Sevm.Sir.SirCodeInstructionTypes.Mov, Sevm.Sir.SirCodeParamTypes.Storage, 1, Sevm.Sir.SirCodeParamTypes.Value, 0);
sir.Codes.Add(Sevm.Sir.SirCodeInstructionTypes.Lea, Sevm.Sir.SirCodeParamTypes.Storage, 2, Sevm.Sir.SirCodeParamTypes.Variable, 1);
sir.Codes.Add(Sevm.Sir.SirCodeInstructionTypes.Ptr, Sevm.Sir.SirCodeParamTypes.Variable, 2, Sevm.Sir.SirCodeParamTypes.Storage, 2);
sir.Codes.Add(Sevm.Sir.SirCodeInstructionTypes.Call, Sevm.Sir.SirCodeParamTypes.IntPtr, 0, Sevm.Sir.SirCodeParamTypes.Variable, 2);
string str = sir.ToString();
Console.WriteLine(str);
using (var f = System.IO.File.Open("X:\\hello.sc", FileMode.Create)) {
    f.Write(System.Text.Encoding.UTF8.GetBytes(str));
    f.Close();
}
using (var f = System.IO.File.Open("X:\\hello.sbc", FileMode.Create)) {
    f.Write(sir.ToBytes());
    f.Close();
}
Console.WriteLine("[X:\\hello.sc]");
using (var f = System.IO.File.Open("X:\\hello.sc", FileMode.Open)) {
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
Console.WriteLine("[X:\\hello.sbc]");
using (var f = System.IO.File.Open("X:\\hello.sbc", FileMode.Open)) {
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
Console.WriteLine("[X:\\hello2.sc]");
using (var f = System.IO.File.Open("X:\\hello2.sc", FileMode.Open)) {
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
    using (var ff = System.IO.File.Open("X:\\hello2.sbc", FileMode.Create)) {
        ff.Write(script.ToBytes());
        ff.Close();
    }
    f.Close();
}