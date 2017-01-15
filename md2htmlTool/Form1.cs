using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace md2htmlTool
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 标题正则
        /// </summary>
        private const string TitlePattern = @"^(?<level>#{1,6})[\s\t](?<title>.+)";
        /// <summary>
        /// github markdown2html api接口请求地址
        /// </summary>
        private const string GithubMd2HtmlAPIUrl = "https://api.github.com/markdown";

        public Form1()
        {
            InitializeComponent();
            Init();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            if (Utility.GetInternetConnectedStatus())
            {
                cbGenerateMenu.Checked = false;
            }
            else
            {
                cbGenerateMenu.Checked = true;
            }
        }

        private void txtMdText_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtMdText.Text))
            {
                return;
            }
            if (cbGenerateMenu.Checked)
            {
                MatchCollection matches = Regex.Matches(txtMdText.Text, TitlePattern, RegexOptions.Multiline | RegexOptions.ExplicitCapture);
                if (matches.Count > 0)
                {
                    StringBuilder sbText = new StringBuilder();
                    int level = 0;
                    int[] titleIndex = new int[6];
                    string title;
                    for (int i = 0; i < matches.Count; i++)
                    {
                        Match item = matches[i];
                        level = item.Groups["level"].Value.Length;
                        titleIndex[level - 1]++;
                        for (int j = level; j < 6; j++)
                        {
                            if (titleIndex[j] != 0)
                            {
                                titleIndex[j] = 0;
                            }
                        }
                        title = item.Groups["title"].Value.GenerateMenuItem(titleIndex);
                        sbText.AppendFormat("{0}[{1}](#{2}){3}", "\t".Multiple(level - 1), title, item.Groups["title"].Value.UrlEncode(), Environment.NewLine);
                    }
                    txtMdText.Text = sbText + txtMdText.Text;
                }
            }
            string mdText = txtMdText.Text;
            try
            {
                //判断网络连接
                if (Utility.GetInternetConnectedStatus())
                {
                    //MessageBox.Show("网络已连接");
                    var html = Markdown2HtmlByGithubAPI(mdText);
                    txtHtmlText.Text = html;
                }
                else
                {
                    //MessageBox.Show("网络未连接");
                    MarkdownSharp.Markdown md = new MarkdownSharp.Markdown();
                    string htmlText = md.Transform(mdText);
                    txtHtmlText.Text = htmlText;
                }
                Clipboard.SetText(txtHtmlText.Text);
                MessageBox.Show("html copied");
            }
            catch (Exception ex)
            {
                MessageBox.Show("发生异常，异常信息：" + ex.Message);
            }
        }

        /// <summary>
        /// 获取通过Github markdown2html API 转换的html
        /// </summary>
        /// <param name="markdownText">要转换的markdown文本</param>
        /// <returns></returns>
        private string Markdown2HtmlByGithubAPI(string markdownText)
        {
            GithubMd2htmlRequestModel request = new GithubMd2htmlRequestModel()
            {
                text = markdownText,
                mode = "markdown",
                context = ""
            };
            string requestJson = request.ToJson();
            byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);
            return Utility.DoHttpPost(GithubMd2HtmlAPIUrl, requestBytes);
        }
    }
}