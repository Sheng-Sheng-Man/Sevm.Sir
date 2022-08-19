using System;
using System.Collections.Generic;
using System.Text;

namespace Sevm.Sir {

    /// <summary>
    /// 定义集合
    /// </summary>
    public class SirDefines : List<SirDefine> {

        /// <summary>
        /// 添加定义
        /// </summary>
        /// <param name="index"></param>
        /// <param name="name"></param>
        /// <param name="ptr"></param>
        public void Add(int index, string name, int ptr) {
            this.Add(new SirDefine() {
                Index = index,
                Name = name,
                IntPtr = ptr
            });
        }

        /// <summary>
        /// 添加定义
        /// </summary>
        /// <param name="index"></param>
        /// <param name="ptr"></param>
        public void Add(int index, int ptr) {
            this.Add(new SirDefine() {
                Index = index,
                Name = "",
                IntPtr = ptr
            });
        }

        /// <summary>
        /// 获取字符串表示形式
        /// </summary>
        /// <returns></returns>
        public new string ToString() {
            StringBuilder sb = new StringBuilder();
            sb.Append("Define\r\n");
            for (int i = 0; i < this.Count; i++) {
                sb.Append("    ");
                sb.Append(this[i].ToString());
                sb.Append("\r\n");
            }
            sb.Append("End Define\r\n");
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
