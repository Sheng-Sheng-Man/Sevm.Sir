using System;
using System.Collections.Generic;
using System.Text;

namespace Sevm.Sir {

    /// <summary>
    /// 代码集合
    /// </summary>
    public class SirCodes : List<SirCode> {

        /// <summary>
        /// 添加指令
        /// </summary>
        /// <param name="instruction"></param>
        /// <param name="target"></param>
        /// <param name="source"></param>
        public void Add(SirCodeInstructionTypes instruction, SirCodeParam target, SirCodeParam source) {
            this.Add(new SirCode() {
                Instruction = instruction,
                Target = target,
                Source = source
            });
        }

        /// <summary>
        /// 添加指令
        /// </summary>
        /// <param name="instruction"></param>
        /// <param name="target"></param>
        public void Add(SirCodeInstructionTypes instruction, SirCodeParam target) {
            this.Add(new SirCode() {
                Instruction = instruction,
                Target = target,
                Source = new SirCodeParam() { ParamType = SirCodeParamTypes.None, Value = 0 }
            });
        }

        /// <summary>
        /// 添加指令
        /// </summary>
        /// <param name="instruction"></param>
        /// <param name="target"></param>
        public void AddToIntPtr(SirCodeInstructionTypes instruction, int target) {
            this.Add(new SirCode() {
                Instruction = instruction,
                Target = new SirCodeParam() { ParamType = SirCodeParamTypes.IntPtr, Value = target },
                Source = new SirCodeParam() { ParamType = SirCodeParamTypes.None, Value = 0 }
            });
        }

        /// <summary>
        /// 添加指令
        /// </summary>
        /// <param name="instruction"></param>
        /// <param name="targetType"></param>
        /// <param name="target"></param>
        public void Add(SirCodeInstructionTypes instruction, SirCodeParamTypes targetType, int target) {
            this.Add(new SirCode() {
                Instruction = instruction,
                Target = new SirCodeParam() { ParamType = targetType, Value = target },
                Source = new SirCodeParam() { ParamType = SirCodeParamTypes.None, Value = 0 }
            });
        }

        /// <summary>
        /// 添加指令
        /// </summary>
        /// <param name="instruction"></param>
        /// <param name="targetType"></param>
        /// <param name="target"></param>
        /// <param name="sourceType"></param>
        /// <param name="source"></param>
        public void Add(SirCodeInstructionTypes instruction, SirCodeParamTypes targetType, int target, SirCodeParamTypes sourceType, int source) {
            this.Add(new SirCode() {
                Instruction = instruction,
                Target = new SirCodeParam() { ParamType = targetType, Value = target },
                Source = new SirCodeParam() { ParamType = sourceType, Value = source }
            });
        }

        /// <summary>
        /// 获取字符串表示形式
        /// </summary>
        /// <returns></returns>
        public new string ToString() {
            StringBuilder sb = new StringBuilder();
            sb.Append("Code\r\n");
            for (int i = 0; i < this.Count; i++) {
                sb.Append("    ");
                sb.Append(this[i].ToString());
                sb.Append("\r\n");
            }
            sb.Append("End Code\r\n");
            return sb.ToString();
        }

        /// <summary>
        /// 获取字节数组
        /// </summary>
        /// <returns></returns>
        public byte[] ToBytes() {
            List<byte> ls = new List<byte>();
            // 生成集合
            List<byte> lss = new List<byte>();
            for (int i = 0; i < this.Count; i++) {
                lss.AddRange(this[i].ToBytes());
            }
            // 生成长度
            ls.AddRange(Parser.GetIntegerBytes(lss.Count));
            ls.AddRange(lss);
            return ls.ToArray();
        }

    }

}
