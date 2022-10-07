using System;
using System.Collections.Generic;
using System.Text;

namespace Sevm.Sir {
    /// <summary>
    /// 脚本代码
    /// </summary>
    public class SirCode {

        /// <summary>
        /// 获取或设置指令类型
        /// </summary>
        public int SourceLine { get; set; }

        /// <summary>
        /// 获取或设置指令类型
        /// </summary>
        public SirCodeInstructionTypes Instruction { get; set; }

        /// <summary>
        /// 获取或设置第一参数
        /// </summary>
        public int Exp1 { get; set; }

        /// <summary>
        /// 获取或设置第二参数
        /// </summary>
        public int Exp2 { get; set; }

        /// <summary>
        /// 获取或设置第三参数
        /// </summary>
        public int Exp3 { get; set; }

        /// <summary>
        /// 获取字符串表示形式
        /// </summary>
        /// <returns></returns>
        public new string ToString() {
            StringBuilder sb = new StringBuilder();
            if (this.Instruction != SirCodeInstructionTypes.Label) {
                sb.Append("    ");
                sb.Append(this.Instruction.ToString());
                sb.Append(' ');
            } else {
                sb.Append(this.Instruction.ToString());
                sb.Append(' ');
            }
            if (this.Exp1 >= 0) {
                sb.Append(SirExpression.Variable(this.Exp1).ToString());
                if (this.Exp2 >= 0) {
                    sb.Append(", ");
                    sb.Append(SirExpression.Variable(this.Exp2).ToString());
                    if (this.Exp3 >= 0) {
                        sb.Append(", ");
                        sb.Append(SirExpression.Variable(this.Exp3).ToString());
                    }
                }
            }
            return sb.ToString();
        }

        // 添加整型到数组
        private void AddIntToList(List<byte> ls, int value) {
            ls.Add((byte)value);
            ls.Add((byte)(value >> 8));
            ls.Add((byte)(value >> 16));
            ls.Add((byte)(value >> 24));
        }

        /// <summary>
        /// 获取字节数组
        /// </summary>
        /// <returns></returns>
        public byte[] ToBytes() {
            List<byte> ls = new List<byte>();
            ls.Add((byte)((int)Instruction));
            ls.Add((byte)((int)Instruction >> 8));
            if (this.Exp1 >= 0) {
                ls.AddRange(SirExpression.Variable(this.Exp1).ToBytes());
            } else {
                ls.AddRange(SirExpression.None.ToBytes());
            }
            if (this.Exp2 >= 0) {
                ls.AddRange(SirExpression.Variable(this.Exp2).ToBytes());
            } else {
                ls.AddRange(SirExpression.None.ToBytes());
            }
            if (this.Exp3 >= 0) {
                ls.AddRange(SirExpression.Variable(this.Exp3).ToBytes());
            } else {
                ls.AddRange(SirExpression.None.ToBytes());
            }
            return ls.ToArray();
        }

    }
}
