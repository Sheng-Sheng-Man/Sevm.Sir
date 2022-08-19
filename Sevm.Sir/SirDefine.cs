using System;
using System.Collections.Generic;
using System.Text;

namespace Sevm.Sir {

    /// <summary>
    /// 脚本定义
    /// </summary>
    public class SirDefine {

        /// <summary>
        /// 获取或设置索引
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 获取或设置名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置指针
        /// </summary>
        public int IntPtr { get; set; }

        /// <summary>
        /// 获取字符串表示形式
        /// </summary>
        /// <returns></returns>
        public new string ToString() {
            StringBuilder sb = new StringBuilder();
            sb.Append($"${this.Index}");
            sb.Append(' ');
            sb.Append(this.Name);
            sb.Append(' ');
            sb.Append(this.IntPtr.ToString());
            return sb.ToString();
        }

        /// <summary>
        /// 获取字节数组
        /// </summary>
        /// <returns></returns>
        public byte[] ToBytes() {
            List<byte> ls = new List<byte>();
            // 生成长度
            ls.AddRange(Parser.GetIntegerBytes(this.Index));
            ls.AddRange(Parser.GetIntegerBytes(this.Name.Length));
            ls.AddRange(System.Text.Encoding.UTF8.GetBytes(this.Name));
            // 添加长度
            ls.AddRange(Parser.GetIntegerBytes(this.IntPtr));
            return ls.ToArray();
        }
    }
}
