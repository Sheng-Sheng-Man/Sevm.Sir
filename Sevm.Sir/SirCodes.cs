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
        public void Add(SirCodeInstructionTypes instruction, SirExpression target, SirExpression source) {
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
        public void Add(SirCodeInstructionTypes instruction, SirExpression target) {
            this.Add(new SirCode() {
                Instruction = instruction,
                Target = target,
                Source = SirExpression.None, // { ParamType = SirExpressionTypes.None, Value = 0 }
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
                Target = SirExpression.IntPtr(target),//{ ParamType = SirExpressionTypes.IntPtr, Value = target },
                Source = SirExpression.None, // { ParamType = SirExpressionTypes.None, Value = 0 }
            });
        }

        /// <summary>
        /// 添加指令
        /// </summary>
        /// <param name="instruction"></param>
        /// <param name="targetType"></param>
        /// <param name="target"></param>
        public void Add(SirCodeInstructionTypes instruction, SirExpressionTypes targetType, int target) {
            this.Add(new SirCode() {
                Instruction = instruction,
                Target = new SirExpression() { Type = targetType, Content = target },
                Source = SirExpression.None, //  { Type = SirExpressionTypes.None, Content = 0 }
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
        public void Add(SirCodeInstructionTypes instruction, SirExpressionTypes targetType, int target, SirExpressionTypes sourceType, int source) {
            this.Add(new SirCode() {
                Instruction = instruction,
                Target = new SirExpression() { Type = targetType, Content = target },
                Source = new SirExpression() { Type = sourceType, Content = source }
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
