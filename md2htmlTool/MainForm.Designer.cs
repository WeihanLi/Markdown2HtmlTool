namespace md2htmlTool
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMdText = new System.Windows.Forms.RichTextBox();
            this.txtHtmlText = new System.Windows.Forms.RichTextBox();
            this.cbGenerateMenu = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "markdown";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(321, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "html";
            // 
            // txtMdText
            // 
            this.txtMdText.Location = new System.Drawing.Point(12, 39);
            this.txtMdText.Name = "txtMdText";
            this.txtMdText.Size = new System.Drawing.Size(287, 389);
            this.txtMdText.TabIndex = 2;
            this.txtMdText.Text = "";
            this.txtMdText.TextChanged += new System.EventHandler(this.txtMdText_TextChanged);
            // 
            // txtHtmlText
            // 
            this.txtHtmlText.Location = new System.Drawing.Point(323, 41);
            this.txtHtmlText.Name = "txtHtmlText";
            this.txtHtmlText.Size = new System.Drawing.Size(318, 385);
            this.txtHtmlText.TabIndex = 2;
            this.txtHtmlText.Text = "";
            // 
            // cbGenerateMenu
            // 
            this.cbGenerateMenu.AutoSize = true;
            this.cbGenerateMenu.Checked = true;
            this.cbGenerateMenu.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbGenerateMenu.Location = new System.Drawing.Point(13, 442);
            this.cbGenerateMenu.Name = "cbGenerateMenu";
            this.cbGenerateMenu.Size = new System.Drawing.Size(102, 16);
            this.cbGenerateMenu.TabIndex = 3;
            this.cbGenerateMenu.Text = "Generate Menu";
            this.cbGenerateMenu.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(691, 470);
            this.Controls.Add(this.cbGenerateMenu);
            this.Controls.Add(this.txtHtmlText);
            this.Controls.Add(this.txtMdText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Md2HtmlTool";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox txtMdText;
        private System.Windows.Forms.RichTextBox txtHtmlText;
        private System.Windows.Forms.CheckBox cbGenerateMenu;
    }
}

