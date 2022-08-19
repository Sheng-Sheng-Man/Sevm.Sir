using System;
using System.Collections.Generic;
using System.Text;

namespace Sevm.Sir {

    /// <summary>
    /// 数据集合
    /// </summary>
    public class SirDatas : List<SirData> {

        /// <summary>
        /// 添加数字
        /// </summary>
        /// <param name="ptr"></param>
        /// <param name="value"></param>
        public void Add(int ptr, double value) {
            this.Add(new SirData() {
                DataType = SirDataTypes.Number,
                IntPtr = ptr,
                Bytes = System.Text.Encoding.UTF8.GetBytes(value.ToString())
            });
        }

        /// <summary>
        /// 添加字符串
        /// </summary>
        /// <param name="ptr"></param>
        /// <param name="value"></param>
        public void Add(int ptr, string value) {
            this.Add(new SirData() {
                DataType = SirDataTypes.String,
                IntPtr = ptr,
                Bytes = System.Text.Encoding.UTF8.GetBytes(value)
            });
        }

        /// <summary>
        /// 添加字符串
        /// </summary>
        /// <param name="ptr"></param>
        /// <param name="tp"></param>
        /// <param name="value"></param>
        public void Add(int ptr, SirDataTypes tp, Span<byte> value) {
            this.Add(new SirData() {
                DataType = tp,
                IntPtr = ptr,
                Bytes = value.ToArray()
            });
        }

        /// <summary>
        /// 获取字符串表示形式
        /// </summary>
        /// <returns></returns>
        public new string ToString() {
            StringBuilder sb = new StringBuilder();
            sb.Append("Data\r\n");
            for (int i = 0; i < this.Count; i++) {
                sb.Append("    ");
                sb.Append(this[i].ToString());
                sb.Append("\r\n");
            }
            sb.Append("End Data\r\n");
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
