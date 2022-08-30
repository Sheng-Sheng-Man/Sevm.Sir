using System;
using System.Collections.Generic;
using System.Text;

namespace Sevm.Sir {

    /// <summary>
    /// 中间语言层错误信息
    /// </summary>
    public class SirException : System.Exception {

        /// <summary>
        /// 获取或设置源代码错误行号
        /// </summary>
        public int SourceLine { get; private set; }

        /// <summary>
        /// 获取或设置指令错误行号
        /// </summary>
        public int CodeLine { get; private set; }

        /// <summary>
        /// 实例化一个错误信息
        /// </summary>
        /// <param name="sourceLine"></param>
        /// <param name="codeLine"></param>
        /// <param name="msg"></param>
        public SirException(int sourceLine, int codeLine, string msg) : base(msg) {
            SourceLine = sourceLine;
            CodeLine = codeLine;
        }

        /// <summary>
        /// 实例化一个错误信息
        /// </summary>
        /// <param name="codeLine"></param>
        /// <param name="msg"></param>
        public SirException(int codeLine, string msg) : base(msg) {
            SourceLine = 0;
            CodeLine = codeLine;
        }

        /// <summary>
        /// 获取字符串表示形式
        /// </summary>
        /// <returns></returns>
        public new string ToString() {
            StringBuilder sb = new StringBuilder();
            if (this.SourceLine > 0) {
                sb.Append("代码行 ");
                sb.Append(SourceLine);
                sb.Append(",");
            }
            sb.Append($"第{CodeLine}指令 发生异常：");
            sb.Append(base.ToString());
            return sb.ToString();
        }

    }
}
