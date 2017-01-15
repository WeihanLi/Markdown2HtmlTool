using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace md2htmlTool
{
    /// <summary>
    /// Github markdown2html API 请求model
    /// </summary>
    public class GithubMd2htmlRequestModel
    {
        /// <summary>
        /// markdown 文本
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// 转换模式
        /// 支持模式：【markdown】、【gfm】
        /// </summary>
        public string mode { get; set; }

        /// <summary>
        /// 关联的Github 仓库
        /// 只有在 mode 为 gfm 时才会用到
        /// </summary>
        public string context { get; set; }
    }
}
