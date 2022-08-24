using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Sevm.Sir {
    /// <summary>
    /// 脚本代码参数
    /// </summary>
    public class SirExpression {

        /// <summary>
        /// 获取或设置参数类型
        /// </summary>
        public SirExpressionTypes Type { get; set; }

        /// <summary>
        /// 获取或设置内容
        /// </summary>
        public int Content { get; set; }

        /// <summary>
        /// 获取一个空定义
        /// </summary>
        public static SirExpression None { get { return new SirExpression() { Type = SirExpressionTypes.None }; } }

        /// <summary>
        /// 获取一个值定义
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static SirExpression Value(int content) { return new SirExpression() { Type = SirExpressionTypes.Value, Content = content }; }

        /// <summary>
        /// 获取一个指针定义
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static SirExpression IntPtr(int content) { return new SirExpression() { Type = SirExpressionTypes.IntPtr, Content = content }; }

        /// <summary>
        /// 获取一个标签定义
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static SirExpression Label(int content) { return new SirExpression() { Type = SirExpressionTypes.Label, Content = content }; }

        /// <summary>
        /// 获取一个变量定义
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static SirExpression Variable(int content) { return new SirExpression() { Type = SirExpressionTypes.Variable, Content = content }; }

        /// <summary>
        /// 获取一个寄存器定义
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static SirExpression Storage(int content) { return new SirExpression() { Type = SirExpressionTypes.Storage, Content = content }; }

        /// <summary>
        /// 获取字符串表示形式
        /// </summary>
        /// <returns></returns>
        public new string ToString() {
            switch (this.Type) {
                case SirExpressionTypes.None: return "";
                case SirExpressionTypes.Value: return this.Content.ToString();
                case SirExpressionTypes.IntPtr: return $"[{this.Content.ToString()}]";
                case SirExpressionTypes.Label: return $"@{this.Content.ToString()}";
                case SirExpressionTypes.Variable: return $"${this.Content.ToString()}";
                case SirExpressionTypes.Storage: return $"#{this.Content.ToString()}";
                default: throw new Exception($"不支持的参数类型'{this.Type.ToString()}'");
            }
        }

        /// <summary>
        /// 获取字节数组
        /// </summary>
        /// <returns></returns>
        public byte[] ToBytes() {
            List<byte> ls = new List<byte>();
            ls.Add((byte)this.Type);
            ls.AddRange(Parser.GetIntegerBytes(this.Content));
            return ls.ToArray();
        }

        /// <summary>
        /// 从整型数据建立对象
        /// </summary>
        /// <param name="value">内容</param>
        public static implicit operator SirExpression(int value) {
            return SirExpression.Value(value);
        }

    }
}
