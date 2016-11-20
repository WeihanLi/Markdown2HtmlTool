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

        public Form1()
        {
            InitializeComponent();
        }

        private void txtMdText_TextChanged(object sender , EventArgs e)
        {
            if (String.IsNullOrEmpty(txtMdText.Text))
            {
                return;
            }
            MatchCollection matches = Regex.Matches(txtMdText.Text , TitlePattern , RegexOptions.Multiline | RegexOptions.ExplicitCapture);
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
                        if (titleIndex[j]!=0)
                        {
                            titleIndex[j] = 0;
                        }                        
                    }
                    title = item.Groups["title"].Value.GenerateMenuItem(titleIndex);
                    sbText.AppendFormat("{0}{1}" , "\t".Multiple(level - 1) + title , Environment.NewLine);
                }
                txtMdText.Text = sbText+ txtMdText.Text;
            }
            string mdText = txtMdText.Text;
            MarkdownSharp.Markdown md = new MarkdownSharp.Markdown();
            string htmlText = md.Transform(mdText);
            txtHtmlText.Text = htmlText;
            try
            {
                Clipboard.SetText(htmlText);
                MessageBox.Show("html copied");
            }
            catch (Exception ex)
            {
                MessageBox.Show("发生异常，异常信息："+ex.Message);
            }
        }
    }
}