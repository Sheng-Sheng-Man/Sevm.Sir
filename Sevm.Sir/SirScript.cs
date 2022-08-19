using System;
using System.Collections.Generic;
using System.Text;

namespace Sevm.Sir {
    /// <summary>
    /// 中间语言脚本
    /// </summary>
    public class SirScript : IDisposable {

        /// <summary>
        /// 获取数据集合
        /// </summary>
        public SirDatas Datas { get; private set; }

        /// <summary>
        /// 获取变量集合
        /// </summary>
        public SirDefines Defines { get; private set; }

        /// <summary>
        /// 获取函数集合
        /// </summary>
        public SirFuncs Funcs { get; private set; }

        /// <summary>
        /// 获取代码集合
        /// </summary>
        public SirCodes Codes { get; private set; }

        /// <summary>
        /// 对象实例化
        /// </summary>
        public SirScript() {
            this.Datas = new SirDatas();
            this.Defines = new SirDefines();
            this.Funcs = new SirFuncs();
            this.Codes = new SirCodes();
        }

        /// <summary>
        /// 获取字符串表示形式
        /// </summary>
        /// <returns></returns>
        public new string ToString() {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.Datas.ToString());
            sb.Append("\r\n");
            sb.Append(this.Defines.ToString());
            sb.Append("\r\n");
            sb.Append(this.Funcs.ToString());
            sb.Append("\r\n");
            sb.Append(this.Codes.ToString());
            return sb.ToString();
        }

        /// <summary>
        /// 获取字节数组
        /// </summary>
        /// <returns></returns>
        public byte[] ToBytes() {
            List<byte> ls = new List<byte>();
            // 生成集合
            ls.AddRange(System.Text.Encoding.ASCII.GetBytes("SIRBC"));
            ls.AddRange(this.Datas.ToBytes());
            ls.AddRange(this.Defines.ToBytes());
            ls.AddRange(this.Funcs.ToBytes());
            ls.AddRange(this.Codes.ToBytes());
            return ls.ToArray();
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose() {
            //throw new NotImplementedException();
            this.Datas.Clear();
            this.Defines.Clear();
            this.Funcs.Clear();
            this.Codes.Clear();
        }
    }
}
