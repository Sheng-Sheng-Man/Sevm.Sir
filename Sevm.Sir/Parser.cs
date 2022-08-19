using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Xml.Linq;

namespace Sevm.Sir {

    /// <summary>
    /// 解析器
    /// </summary>
    public static class Parser {

        /// <summary>
        /// 获取整型数字
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] GetIntegerBytes(int value) {
            byte[] bytes = new byte[4];
            bytes[0] = (byte)(value % 256);
            value = value / 256;
            bytes[1] = (byte)(value % 256);
            value = value / 256;
            bytes[2] = (byte)(value % 256);
            value = value / 256;
            bytes[3] = (byte)(value % 256);
            return bytes;
        }

        /// <summary>
        /// 获取整型数字
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int GetInteger(Span<byte> value) {
            int res = 0;
            for (int i = 0; i < value.Length; i++) {
                res += value[i] * (int)Math.Pow(256, i);
            }
            return res;
        }

        /// <summary>
        /// 获取指令类型
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static SirCodeInstructionTypes GetInstructionType(string name) {
            switch (name) {
                // 二、数据指令
                case "mov": return SirCodeInstructionTypes.Mov;
                case "new": return SirCodeInstructionTypes.New;
                case "ptr": return SirCodeInstructionTypes.Ptr;
                case "lea": return SirCodeInstructionTypes.Lea;
                // 三、类型操作指令
                case "list": return SirCodeInstructionTypes.List;
                case "join": return SirCodeInstructionTypes.Join;
                case "cnt": return SirCodeInstructionTypes.Cnt;
                case "Obj": return SirCodeInstructionTypes.Obj;
                case "keys": return SirCodeInstructionTypes.Keys;
                // 四、运算操作指令
                case "add": return SirCodeInstructionTypes.Add;
                case "sub": return SirCodeInstructionTypes.Sub;
                case "mul": return SirCodeInstructionTypes.Mul;
                case "div": return SirCodeInstructionTypes.Div;
                // 五、逻辑操作指令
                case "not": return SirCodeInstructionTypes.Not;
                case "and": return SirCodeInstructionTypes.And;
                case "or": return SirCodeInstructionTypes.Or;
                case "xor": return SirCodeInstructionTypes.Xor;
                // 六、比较指令
                case "equal": return SirCodeInstructionTypes.Equal;
                case "large": return SirCodeInstructionTypes.Large;
                case "small": return SirCodeInstructionTypes.Small;
                // 七、区域操作指令
                case "jmp": return SirCodeInstructionTypes.Jmp;
                case "jmpf": return SirCodeInstructionTypes.Jmpf;
                case "call": return SirCodeInstructionTypes.Call;
                case "ret": return SirCodeInstructionTypes.Ret;
                default: return SirCodeInstructionTypes.None;
            }
        }

        /// <summary>
        /// 获取指令类型
        /// </summary>
        /// <param name="param"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static void FillSirCodeParam(SirCodeParam param, string name) {
            if (name.StartsWith("[") && name.EndsWith("]")) {
                param.ParamType = SirCodeParamTypes.IntPtr;
                param.Value = int.Parse(name.Substring(1, name.Length - 2));
            } else if (name.StartsWith("@")) {
                param.ParamType = SirCodeParamTypes.Label;
                param.Value = int.Parse(name.Substring(1));
            } else if (name.StartsWith("#")) {
                param.ParamType = SirCodeParamTypes.Storage;
                param.Value = int.Parse(name.Substring(1));
            } else if (name.StartsWith("$")) {
                param.ParamType = SirCodeParamTypes.Variable;
                param.Value = int.Parse(name.Substring(1));
            } else {
                param.ParamType = SirCodeParamTypes.Value;
                param.Value = int.Parse(name);
            }
        }

        /// <summary>
        /// 获取脚本对象
        /// </summary>
        /// <param name="sir"></param>
        /// <returns></returns>
        public static SirScript GetScript(string sir) {
            SirScript script = new SirScript();
            StringBuilder code = new StringBuilder();
            SirParserTypes tp = SirParserTypes.None;
            SirParserLineTypes ltp = SirParserLineTypes.Normal;
            List<string> ls = new List<string>();
            bool inString = false;
            bool inEscape = false;
            for (int i = 0; i < sir.Length; i++) {
                char chr = sir[i];
                switch (chr) {
                    case '\r': break;
                    case '\n':
                        #region [=====换行=====]
                        // 字符串中
                        if (inString) throw new Exception($"意外的'\\n'符号");
                        // 判断是否有内容
                        if (code.Length > 0) { ls.Add(code.ToString()); code.Clear(); }
                        if (ls.Count > 0) {
                            string name = ls[0].ToLower();
                            switch (name) {
                                case "code":
                                    if (tp != SirParserTypes.None) throw new Exception($"意外的'{name}'指令");
                                    tp = SirParserTypes.Code;
                                    break;
                                case "data":
                                    if (tp != SirParserTypes.None) throw new Exception($"意外的'{name}'指令");
                                    tp = SirParserTypes.Data;
                                    break;
                                case "define":
                                    if (tp != SirParserTypes.None) throw new Exception($"意外的'{name}'指令");
                                    tp = SirParserTypes.Define;
                                    break;
                                case "func":
                                    if (tp != SirParserTypes.None) throw new Exception($"意外的'{name}'指令");
                                    tp = SirParserTypes.Func;
                                    break;
                                case "end":
                                    if (ls.Count < 2) throw new Exception($"意外的'{name}'指令");
                                    if (tp == SirParserTypes.None) throw new Exception($"意外的'{name}'指令");
                                    string sign = ls[1].ToLower();
                                    if (tp == SirParserTypes.Data && sign == "data") { tp = SirParserTypes.None; break; }
                                    if (tp == SirParserTypes.Define && sign == "define") { tp = SirParserTypes.None; break; }
                                    if (tp == SirParserTypes.Func && sign == "func") { tp = SirParserTypes.None; break; }
                                    if (tp == SirParserTypes.Code && sign == "code") { tp = SirParserTypes.None; break; }
                                    throw new Exception($"意外的'{name}'指令");
                                default:
                                    // 判断异常
                                    switch (tp) {
                                        case SirParserTypes.Data:
                                            if (ls.Count < 3) throw new Exception($"意外的'{name}'指令");
                                            if (!name.StartsWith("[")) throw new Exception($"意外的'{name}'指令");
                                            if (!name.EndsWith("]")) throw new Exception($"意外的'{name}'指令");
                                            int ptr = int.Parse(name.Substring(1, name.Length - 2));
                                            string dataType = ls[1].ToLower();
                                            string value = ls[2];
                                            if (dataType == "string") {
                                                // 字符串
                                                if (value.Length < 2) throw new Exception($"不符合规范的 {value} 字符串");
                                                if (!value.StartsWith("\"")) throw new Exception($"不符合规范的 {value} 字符串");
                                                if (!value.EndsWith("\"")) throw new Exception($"不符合规范的 {value} 字符串");
                                                script.Datas.Add(ptr, value.Substring(1, value.Length - 2));
                                            } else {
                                                // 数值
                                                script.Datas.Add(ptr, double.Parse(value));
                                            }
                                            break;
                                        case SirParserTypes.Define:
                                            if (ls.Count < 3) throw new Exception($"意外的'{name}'指令");
                                            if (!name.StartsWith("$")) throw new Exception($"意外的'{name}'指令");
                                            int index = int.Parse(name.Substring(1));
                                            string defName = ls[1];
                                            ptr = int.Parse(ls[2]);
                                            script.Defines.Add(index, defName, ptr);
                                            break;
                                        case SirParserTypes.Func:
                                            if (ls.Count < 2) throw new Exception($"意外的'{name}'指令");
                                            if (!name.StartsWith("@")) throw new Exception($"意外的'{name}'指令");
                                            index = int.Parse(name.Substring(1));
                                            string funName = ls[1];
                                            script.Funcs.Add(index, funName);
                                            break;
                                        case SirParserTypes.Code:
                                            if (name.StartsWith("@")) {
                                                SirCodeInstructionTypes instructionType = SirCodeInstructionTypes.Label;
                                                SirCodeParam target = new SirCodeParam();
                                                FillSirCodeParam(target, name);
                                                script.Codes.Add(instructionType, target);
                                            } else {
                                                SirCodeInstructionTypes instructionType = GetInstructionType(name);
                                                SirCodeParam target = new SirCodeParam();
                                                SirCodeParam source = new SirCodeParam();
                                                if (ls.Count < 2) {
                                                    target.ParamType = SirCodeParamTypes.None;
                                                    source.ParamType = SirCodeParamTypes.None;
                                                } else if (ls.Count < 3) {
                                                    FillSirCodeParam(target, ls[1]);
                                                    source.ParamType = SirCodeParamTypes.None;
                                                } else {
                                                    FillSirCodeParam(target, ls[1]);
                                                    FillSirCodeParam(source, ls[2]);
                                                }
                                                script.Codes.Add(instructionType, target, source);
                                            }
                                            break;
                                        default:
                                            throw new Exception($"意外的'{name}'指令");
                                    }
                                    break;
                            }
                            ls.Clear();
                        }
                        ltp = SirParserLineTypes.Normal;
                        #endregion
                        break;
                    case ';':
                        #region [=====分号=====]
                        // 字符串中
                        if (inString) { code.Append(chr); break; }
                        // 判断是否有内容
                        if (code.Length > 0) { ls.Add(code.ToString()); code.Clear(); }
                        ltp = SirParserLineTypes.Finish;
                        #endregion
                        break;
                    case ' ':
                        #region [=====空格=====]
                        // 当行结束，则忽略
                        if (ltp == SirParserLineTypes.Finish) break;
                        // 字符串中
                        if (inString) { code.Append(chr); break; }
                        // 无内容则忽略
                        if (code.Length <= 0) break;
                        if (ls.Count == 1) {
                            if (tp == SirParserTypes.Code) break;
                            ls.Add(code.ToString());
                            code.Clear();
                        } else {
                            ls.Add(code.ToString());
                            code.Clear();
                        }
                        #endregion
                        break;
                    case ',':
                        #region [=====逗号=====]
                        // 当行结束，则忽略
                        if (ltp == SirParserLineTypes.Finish) break;
                        // 字符串中
                        if (inString) { code.Append(chr); break; }
                        // 指令目标和源之间的分隔符
                        if (ls.Count == 1 && tp == SirParserTypes.Code) {
                            ls.Add(code.ToString());
                            code.Clear();
                            break;
                        }
                        throw new Exception($"意外的'{chr}'字符");
                    #endregion
                    //break;
                    case '"':
                        #region [=====引号=====]
                        // 当行结束，则忽略
                        if (ltp == SirParserLineTypes.Finish) break;
                        if (inEscape) {
                            code.Append(chr);
                            inEscape = false;
                        } else {
                            code.Append(chr);
                            inString = !inString;
                        }
                        #endregion
                        break;
                    case '\\':
                        #region [=====反斜杠=====]
                        // 当行结束，则忽略
                        if (ltp == SirParserLineTypes.Finish) break;
                        if (!inString) throw new Exception($"意外的'{chr}'字符");
                        if (inEscape) {
                            code.Append(chr);
                            inEscape = false;
                        } else {
                            inEscape = true;
                        }
                        #endregion
                        break;
                    case 'r':
                        #region [=====r=====]
                        // 当行结束，则忽略
                        if (ltp == SirParserLineTypes.Finish) break;
                        if (inEscape) {
                            code.Append('\r');
                            inEscape = false;
                        } else {
                            code.Append(chr);
                        }
                        #endregion
                        break;
                    case 'n':
                        #region [=====n=====]
                        // 当行结束，则忽略
                        if (ltp == SirParserLineTypes.Finish) break;
                        if (inEscape) {
                            code.Append('\n');
                            inEscape = false;
                        } else {
                            code.Append(chr);
                        }
                        #endregion
                        break;
                    default:
                        // 意外的转义符
                        if (inEscape) throw new Exception($"意外的'{chr}'字符");
                        // 当行结束，则忽略
                        if (ltp == SirParserLineTypes.Finish) break;
                        code.Append(chr);
                        break;
                }
            }
            return script;
        }

        /// <summary>
        /// 获取脚本对象
        /// </summary>
        /// <param name="sir"></param>
        /// <returns></returns>
        public static SirScript GetScript(byte[] sir) {
            SirScript script = new SirScript();
            if (sir.Length <= 5) throw new Exception("不是标准的sbc文件");
            if (System.Text.Encoding.ASCII.GetString(sir, 0, 5) != "SIRBC") throw new Exception("不是标准的sbc文件");
            int dataAddr = 5;
            int dataSize = 0;
            int defineAddr = 0;
            int defineSize = 0;
            int funcAddr = 0;
            int funcSize = 0;
            int codeAddr = 0;
            int codeSize = 0;
            dataSize = GetInteger(new Span<byte>(sir, dataAddr, 4));
            defineAddr = dataAddr + dataSize + 4;
            defineSize = GetInteger(new Span<byte>(sir, defineAddr, 4));
            funcAddr = defineAddr + defineSize + 4;
            funcSize = GetInteger(new Span<byte>(sir, funcAddr, 4));
            codeAddr = funcAddr + funcSize + 4;
            codeSize = GetInteger(new Span<byte>(sir, codeAddr, 4));
            // 输出调试
            //Console.WriteLine($"dataAddr: {dataAddr}, dataSize: {dataSize}, defineAddr: {defineAddr}, defineSize: {defineSize}, funcAddr: {funcAddr}, funcSize: {funcSize}, codeAddr: {codeAddr}, codeSize: {codeSize}");
            // 加载所有数据
            int addr = dataAddr + 4;
            int offset = 0;
            do {
                int ptr = GetInteger(new Span<byte>(sir, addr + offset, 4));
                offset += 4;
                SirDataTypes dataType = (SirDataTypes)sir[addr + offset];
                offset++;
                int dataLen = GetInteger(new Span<byte>(sir, addr + offset, 4));
                offset += 4;
                script.Datas.Add(ptr, dataType, new Span<byte>(sir, addr + offset, dataLen));
                offset += dataLen;
            } while (offset < dataSize);
            // 加载所有定义
            addr = defineAddr + 4;
            offset = 0;
            do {
                int index = GetInteger(new Span<byte>(sir, addr + offset, 4));
                offset += 4;
                int nameLen = GetInteger(new Span<byte>(sir, addr + offset, 4));
                offset += 4;
                string name = System.Text.Encoding.UTF8.GetString(new Span<byte>(sir, addr + offset, nameLen));
                offset += nameLen;
                int ptr = GetInteger(new Span<byte>(sir, addr + offset, 4));
                offset += 4;
                script.Defines.Add(index, name, ptr);
            } while (offset < defineSize);
            // 加载所有函数
            addr = funcAddr + 4;
            offset = 0;
            do {
                int index = GetInteger(new Span<byte>(sir, addr + offset, 4));
                offset += 4;
                int nameLen = GetInteger(new Span<byte>(sir, addr + offset, 4));
                offset += 4;
                string name = System.Text.Encoding.UTF8.GetString(new Span<byte>(sir, addr + offset, nameLen));
                offset += nameLen;
                script.Funcs.Add(index, name);
            } while (offset < funcSize);
            // 加载所有指令
            addr = codeAddr + 4;
            offset = 0;
            do {
                SirCodeInstructionTypes ins = (SirCodeInstructionTypes)GetInteger(new Span<byte>(sir, addr + offset, 2));
                offset += 2;
                SirCodeParamTypes targetType = (SirCodeParamTypes)sir[addr + offset];
                offset++;
                int targetValue = GetInteger(new Span<byte>(sir, addr + offset, 4));
                offset += 4;
                SirCodeParamTypes sourceType = (SirCodeParamTypes)sir[addr + offset];
                offset++;
                int sourceValue = GetInteger(new Span<byte>(sir, addr + offset, 4));
                offset += 4;
                script.Codes.Add(ins, targetType, targetValue, sourceType, sourceValue);
            } while (offset < codeSize);
            return script;
        }

    }
}
