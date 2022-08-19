using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Sevm.Sir {

    /// <summary>
    /// 脚本数据
    /// </summary>
    public class SirData {

        // 缓存
        private double dbCache = 0;
        private string szCache = null;

        /// <summary>
        /// 获取或设置指针
        /// </summary>
        public int IntPtr { get; set; }

        /// <summary>
        /// 获取或设置数据类型，0为空，1为字符串，2为数字
        /// </summary>
        public SirDataTypes DataType { get; set; }

        /// <summary>
        /// 获取或设置数据
        /// </summary>
        public byte[] Bytes { get; set; }

        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <returns></returns>
        public string GetString() {
            if (szCache != null) return szCache;
            szCache = System.Text.Encoding.UTF8.GetString(Bytes);
            return szCache;
        }

        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <returns></returns>
        public double GetNumber() {
            // 当存在缓存时，直接使用缓存
            if (dbCache != 0) return dbCache;
            // 转换为数字
            string val = this.GetString();
            if (double.TryParse(val, out dbCache)) {
                return dbCache;
            } else {
                return 0;
            }
        }

        /// <summary>
        /// 获取字符串表示形式
        /// </summary>
        /// <returns></returns>
        public new string ToString() {
            StringBuilder sb = new StringBuilder();
            sb.Append($"[{this.IntPtr}]");
            sb.Append(' ');
            sb.Append(this.DataType.ToString());
            sb.Append(' ');
            if (this.DataType == SirDataTypes.String) {
                string str = this.GetString();
                sb.Append('"');
                sb.Append(str.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\r", "\\\r").Replace("\n", "\\\n"));
                sb.Append('"');
            } else {
                sb.Append(this.GetNumber());
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取字节数组
        /// </summary>
        /// <returns></returns>
        public byte[] ToBytes() {
            List<byte> ls = new List<byte>();
            // 生成长度
            ls.AddRange(Parser.GetIntegerBytes(this.IntPtr));
            ls.Add((byte)this.DataType);
            // 添加长度
            ls.AddRange(Parser.GetIntegerBytes(this.Bytes.Length));
            ls.AddRange(this.Bytes);
            return ls.ToArray();
        }

    }
}
