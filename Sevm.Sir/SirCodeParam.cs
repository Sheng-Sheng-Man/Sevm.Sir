using System;
using System.Collections.Generic;
using System.Text;

namespace Sevm.Sir {
    /// <summary>
    /// 脚本代码参数
    /// </summary>
    public class SirCodeParam {

        /// <summary>
        /// 获取或设置参数类型
        /// </summary>
        public SirCodeParamTypes ParamType { get; set; }

        /// <summary>
        /// 获取或设置值
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// 获取字符串表示形式
        /// </summary>
        /// <returns></returns>
        public new string ToString() {
            switch (this.ParamType) {
                case SirCodeParamTypes.None: return "";
                case SirCodeParamTypes.Value: return this.Value.ToString();
                case SirCodeParamTypes.IntPtr: return $"[{this.Value.ToString()}]";
                case SirCodeParamTypes.Label: return $"@{this.Value.ToString()}";
                case SirCodeParamTypes.Variable: return $"${this.Value.ToString()}";
                case SirCodeParamTypes.Storage: return $"#{this.Value.ToString()}";
                default: throw new Exception($"不支持的参数类型'{this.ParamType.ToString()}'");
            }
        }

        /// <summary>
        /// 获取字节数组
        /// </summary>
        /// <returns></returns>
        public byte[] ToBytes() {
            List<byte> ls = new List<byte>();
            ls.Add((byte)this.ParamType);
            ls.AddRange(Parser.GetIntegerBytes(this.Value));
            return ls.ToArray();
        }

    }
}
