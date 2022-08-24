using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Sevm.Sir {

    /// <summary>
    /// 脚本导入信息
    /// </summary>
    public class SirImport {

        /// <summary>
        /// 获取或设置导入类型
        /// </summary>
        public SirImportTypes ImportType { get; set; }

        /// <summary>
        /// 获取或设置导入对象
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 获取字符串表示形式
        /// </summary>
        /// <returns></returns>
        public new string ToString() {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.ImportType.ToString());
            sb.Append(' ');
            sb.Append('"');
            sb.Append(this.Content.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\r", "\\\r").Replace("\n", "\\\n"));
            sb.Append('"');
            return sb.ToString();
        }

        /// <summary>
        /// 获取字节数组
        /// </summary>
        /// <returns></returns>
        public byte[] ToBytes() {
            List<byte> ls = new List<byte>();
            // 生成长度
            ls.Add((byte)this.ImportType);
            // 添加长度
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(this.Content);
            ls.AddRange(Parser.GetIntegerBytes(bs.Length));
            ls.AddRange(bs);
            return ls.ToArray();
        }

    }
}
