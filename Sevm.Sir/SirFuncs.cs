using System;
using System.Collections.Generic;
using System.Text;

namespace Sevm.Sir {

    /// <summary>
    /// 函数集合
    /// </summary>
    public class SirFuncs : List<SirFunc> {

        /// <summary>
        /// 添加定义
        /// </summary>
        /// <param name="index"></param>
        /// <param name="name"></param>
        /// <param name="scope"></param>
        public void Add(SirScopeTypes scope, int index, string name) {
            this.Add(new SirFunc() {
                Index = index,
                Name = name,
                Scope = scope,
            });
        }

        /// <summary>
        /// 添加函数
        /// </summary>
        /// <param name="index"></param>
        /// <param name="name"></param>
        public void Add(int index, string name) {
            this.Add(new SirFunc() {
                Index = index,
                Name = name
            });
        }

        /// <summary>
        /// 获取字符串表示形式
        /// </summary>
        /// <returns></returns>
        public new string ToString() {
            StringBuilder sb = new StringBuilder();
            sb.Append("Func\r\n");
            for (int i = 0; i < this.Count; i++) {
                sb.Append("    ");
                sb.Append(this[i].ToString());
                sb.Append("\r\n");
            }
            sb.Append("End Func\r\n");
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
