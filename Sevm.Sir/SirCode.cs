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
        public SirCodeInstructionTypes Instruction { get; set; }

        /// <summary>
        /// 获取或设置第一参数
        /// </summary>
        public SirExpression Exp1 { get; set; }

        /// <summary>
        /// 获取或设置第二参数
        /// </summary>
        public SirExpression Exp2 { get; set; }

        /// <summary>
        /// 获取或设置第三参数
        /// </summary>
        public SirExpression Exp3 { get; set; }

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
            }
            if (this.Exp1.Type != SirExpressionTypes.None) {
                sb.Append(this.Exp1.ToString());
                if (this.Exp2.Type != SirExpressionTypes.None) {
                    sb.Append(", ");
                    sb.Append(this.Exp2.ToString());
                    if (this.Exp3.Type != SirExpressionTypes.None) {
                        sb.Append(", ");
                        sb.Append(this.Exp3.ToString());
                    }
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取字节数组
        /// </summary>
        /// <returns></returns>
        public byte[] ToBytes() {
            List<byte> ls = new List<byte>();
            ls.Add((byte)((int)Instruction % 256));
            ls.Add((byte)((int)Instruction / 256));
            ls.AddRange(this.Exp1.ToBytes());
            ls.AddRange(this.Exp2.ToBytes());
            ls.AddRange(this.Exp3.ToBytes());
            return ls.ToArray();
        }

    }
}
