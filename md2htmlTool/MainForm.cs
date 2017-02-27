using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace md2htmlTool
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// 标题正则
        /// </summary>
        private const string TitlePattern = @"^(?<level>#{1,6})[\s\t](?<title>.+)";
        /// <summary>
        /// github markdown2html api接口请求地址
        /// </summary>
        private const string GithubMd2HtmlAPIUrl = "https://api.github.com/markdown";

        private static LoadingForm loadingForm;

        public MainForm()
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

        /// <summary>
        /// 修改html文本
        /// </summary>
        /// <param name="text">html文本</param>
        private void ChangeHtmlText(string text)
        {
            this.txtHtmlText.Text = text;
            if (!String.IsNullOrEmpty(text))
            {
                Clipboard.SetText(text);
            }
        }

        private void txtMdText_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtMdText.Text))
            {
                return;
            }
            string mdText = txtMdText.Text;
            bool bGenerateMenu = cbGenerateMenu.Checked;
            try
            {
                string htmlText = "";
                Thread loadingThread = new Thread(new ThreadStart(ShowLoadingLayer));
                loadingThread.IsBackground = true;//设置为后台线程
                loadingThread.Name = "md2html_loading_thread";
                loadingThread.Start();
                htmlText = GetResponseHtml(mdText, bGenerateMenu);
                loadingThread.Abort();

                if (!htmlText.ToUpperInvariant().Equals("TRANSFER ERROR"))
                {
                    ChangeHtmlText(htmlText);
                    MessageBox.Show("html copyed");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("发生异常，异常信息：" + ex.Message);
            }
        }

        /// <summary>
        /// 获取html
        /// </summary>
        /// <returns></returns>
        private string GetResponseHtml(string mdText,bool bGenerateMenu)
        {
            string html = "";
            if (bGenerateMenu)
            {
                MatchCollection matches = Regex.Matches(mdText, TitlePattern, RegexOptions.Multiline | RegexOptions.ExplicitCapture);
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
                    mdText = sbText.Append(mdText).ToString();
                }
            }
            //判断网络连接
            if (Utility.GetInternetConnectedStatus())
            {
                html = Markdown2HtmlByGithubAPI(mdText);
            }
            else
            {
                MarkdownSharp.Markdown md = new MarkdownSharp.Markdown();
                html= md.Transform(mdText);
            }
            return html;
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

        /// <summary>
        /// 显示loading层
        /// </summary>
        private void ShowLoadingLayer()
        {
            if (loadingForm == null)
            {
                loadingForm = new LoadingForm
                {
                    StartPosition = FormStartPosition.WindowsDefaultLocation
                };
            }
            loadingForm.ShowDialog();
        }
    }
}