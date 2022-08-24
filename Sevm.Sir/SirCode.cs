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
        /// 获取或设置目标参数
        /// </summary>
        public SirExpression Target { get; set; }

        /// <summary>
        /// 获取或设置源参数
        /// </summary>
        public SirExpression Source { get; set; }

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
            sb.Append(this.Target.ToString());
            if (this.Source.Type != SirExpressionTypes.None) {
                sb.Append(", ");
                sb.Append(this.Source.ToString());
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
            ls.AddRange(this.Target.ToBytes());
            ls.AddRange(this.Source.ToBytes());
            return ls.ToArray();
        }

    }
}
