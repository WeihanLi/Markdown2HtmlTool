using System;
using System.Threading;
using System.Windows.Forms;

namespace md2htmlTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void txtMdText_TextChanged(object sender, EventArgs e)
        {
            string mdText = txtMdText.Text;
            if (String.IsNullOrEmpty(mdText))
            {
                return;
            }
            MarkdownSharp.Markdown md = new MarkdownSharp.Markdown();
            string htmlText = md.Transform(mdText);
            txtHtmlText.Text = htmlText;
            try
            {
                Clipboard.SetText(htmlText);
                MessageBox.Show("html copied");
            }
            catch (System.Runtime.InteropServices.ExternalException ex)
            {
                throw ex;
            }
            catch (ThreadStateException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }
    }
}