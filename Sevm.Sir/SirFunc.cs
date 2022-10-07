using System;
using System.Collections.Generic;
using System.Text;

namespace Sevm.Sir {

    /// <summary>
    /// 脚本函数
    /// </summary>
    public class SirFunc {

        /// <summary>
        /// 获取或设置索引
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 获取或设置名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置作用域
        /// </summary>
        public SirScopeTypes Scope { get; set; }

        /// <summary>
        /// 获取字符串表示形式
        /// </summary>
        /// <returns></returns>
        public new string ToString() {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.Scope.ToString());
            sb.Append(' ');
            sb.Append($"${this.Index}");
            sb.Append(' ');
            sb.Append(this.Name);
            return sb.ToString();
        }

        /// <summary>
        /// 获取字节数组
        /// </summary>
        /// <returns></returns>
        public byte[] ToBytes() {
            List<byte> ls = new List<byte>();
            // 添加作用域
            ls.Add((byte)this.Scope);
            // 生成长度
            ls.AddRange(Parser.GetIntegerBytes(this.Index));
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(this.Name);
            ls.AddRange(Parser.GetIntegerBytes(bytes.Length));
            ls.AddRange(bytes);
            return ls.ToArray();
        }

    }
}
