using System;
using System.Collections.Generic;
using System.Text;

namespace Sevm.Sir {

    /// <summary>
    /// 定义集合
    /// </summary>
    public class SirImports : List<SirImport> {

        /// <summary>
        /// 添加引入
        /// </summary>
        /// <param name="tp"></param>
        /// <param name="content"></param>
        public void Add(SirImportTypes tp, string content) {
            this.Add(new SirImport() {
                ImportType = tp,
                Content = content,
            });
        }

        /// <summary>
        /// 获取字符串表示形式
        /// </summary>
        /// <returns></returns>
        public new string ToString() {
            StringBuilder sb = new StringBuilder();
            sb.Append("Import\r\n");
            for (int i = 0; i < this.Count; i++) {
                sb.Append("    ");
                sb.Append(this[i].ToString());
                sb.Append("\r\n");
            }
            sb.Append("End Import\r\n");
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
