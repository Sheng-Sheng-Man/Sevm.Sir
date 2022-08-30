using System;
using System.Collections.Generic;
using System.Text;

namespace Sevm.Sir {

    /// <summary>
    /// 中间语言层错误信息
    /// </summary>
    public class SirException : System.Exception {

        /// <summary>
        /// 获取或设置错误类型
        /// </summary>
        public SirExceptionTypes Type { get; private set; }

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
        /// <param name="tp"></param>
        /// <param name="sourceLine"></param>
        /// <param name="codeLine"></param>
        /// <param name="msg"></param>
        public SirException(SirExceptionTypes tp, int sourceLine, int codeLine, string msg) : base(msg) {
            this.Type = tp;
            SourceLine = sourceLine;
            CodeLine = codeLine;
        }

        /// <summary>
        /// 实例化一个错误信息
        /// </summary>
        /// <param name="sourceLine"></param>
        /// <param name="codeLine"></param>
        /// <param name="msg"></param>
        public SirException(int sourceLine, int codeLine, string msg) : base(msg) {
            this.Type = SirExceptionTypes.General;
            SourceLine = sourceLine;
            CodeLine = codeLine;
        }

        /// <summary>
        /// 实例化一个错误信息
        /// </summary>
        /// <param name="codeLine"></param>
        /// <param name="msg"></param>
        public SirException(int codeLine, string msg) : base(msg) {
            this.Type = SirExceptionTypes.General;
            SourceLine = 0;
            CodeLine = codeLine;
        }

        /// <summary>
        /// 实例化一个错误信息
        /// </summary>
        /// <param name="msg"></param>
        public SirException(string msg) : base(msg) {
            this.Type = SirExceptionTypes.General;
            SourceLine = 0;
            CodeLine = 0;
        }

        /// <summary>
        /// 获取字符串表示形式
        /// </summary>
        /// <returns></returns>
        public new string ToString() {
            StringBuilder sb = new StringBuilder();
            if (this.SourceLine > 0) {
                sb.Append($"第{SourceLine}行代码 ");
            }
            if (this.CodeLine > 0) {
                sb.Append($"第{CodeLine}指令 ");
            }
            sb.Append($"发生[{this.Type.ToString()}]异常：");
            sb.Append(base.ToString());
            return sb.ToString();
        }

    }
}
