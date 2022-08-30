using System;
using System.Collections.Generic;
using System.Text;

namespace Sevm.Sir {

    /// <summary>
    /// 中间语言层错误类型
    /// </summary>
    public enum SirExceptionTypes {

        /// <summary>
        /// 常规错误
        /// </summary>
        General = 0x00,

        /// <summary>
        /// 版本不匹配
        /// </summary>
        VersionMismatch = 0x01,

    }
}
