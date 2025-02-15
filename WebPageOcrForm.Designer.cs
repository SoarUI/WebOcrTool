
namespace WebPageOcr
{
    partial class WebPageOcrForm
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
            helppen.Dispose();
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.windowtitle = new System.Windows.Forms.Label();
            this.processid = new System.Windows.Forms.Label();
            this.btnGetOcrString = new System.Windows.Forms.Button();
            this.btnpause = new System.Windows.Forms.Button();
            this.btnWriteText = new System.Windows.Forms.Button();
            this.labelscript = new System.Windows.Forms.Label();
            this.labelmessage = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.MainMenuBar = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.fromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fromToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fromWebCamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fromScreenCaptureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveStringsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eDITToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoDetectRectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customerRectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.languageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.二值化方法ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thresholdOneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thresholdTwoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainsplitContainer = new System.Windows.Forms.SplitContainer();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txbfx = new System.Windows.Forms.TextBox();
            this.txbfy = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ThresholdnumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.picroteright = new System.Windows.Forms.PictureBox();
            this.picroteleft = new System.Windows.Forms.PictureBox();
            this.MainMenuBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainsplitContainer)).BeginInit();
            this.mainsplitContainer.Panel1.SuspendLayout();
            this.mainsplitContainer.Panel2.SuspendLayout();
            this.mainsplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ThresholdnumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picroteright)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picroteleft)).BeginInit();
            this.SuspendLayout();
            // 
            // windowtitle
            // 
            this.windowtitle.AutoSize = true;
            this.windowtitle.Location = new System.Drawing.Point(223, 31);
            this.windowtitle.Name = "windowtitle";
            this.windowtitle.Size = new System.Drawing.Size(0, 12);
            this.windowtitle.TabIndex = 2;
            // 
            // processid
            // 
            this.processid.AutoSize = true;
            this.processid.Location = new System.Drawing.Point(223, 59);
            this.processid.Name = "processid";
            this.processid.Size = new System.Drawing.Size(0, 12);
            this.processid.TabIndex = 4;
            // 
            // btnGetOcrString
            // 
            this.btnGetOcrString.Location = new System.Drawing.Point(580, 31);
            this.btnGetOcrString.Name = "btnGetOcrString";
            this.btnGetOcrString.Size = new System.Drawing.Size(88, 23);
            this.btnGetOcrString.TabIndex = 7;
            this.btnGetOcrString.Text = "Get Strings";
            this.btnGetOcrString.UseVisualStyleBackColor = true;
            this.btnGetOcrString.Click += new System.EventHandler(this.btnGetOcrString_Click);
            // 
            // btnpause
            // 
            this.btnpause.Location = new System.Drawing.Point(148, 31);
            this.btnpause.Name = "btnpause";
            this.btnpause.Size = new System.Drawing.Size(104, 23);
            this.btnpause.TabIndex = 8;
            this.btnpause.Text = "AutoRect Range";
            this.btnpause.UseVisualStyleBackColor = true;
            this.btnpause.Click += new System.EventHandler(this.btnAutoFit_Click);
            // 
            // btnWriteText
            // 
            this.btnWriteText.Location = new System.Drawing.Point(402, 31);
            this.btnWriteText.Name = "btnWriteText";
            this.btnWriteText.Size = new System.Drawing.Size(156, 23);
            this.btnWriteText.TabIndex = 9;
            this.btnWriteText.Text = "Save Strings  to File";
            this.btnWriteText.UseVisualStyleBackColor = true;
            this.btnWriteText.Click += new System.EventHandler(this.btnWriteText_Click);
            // 
            // labelscript
            // 
            this.labelscript.AutoSize = true;
            this.labelscript.Location = new System.Drawing.Point(97, 99);
            this.labelscript.Name = "labelscript";
            this.labelscript.Size = new System.Drawing.Size(0, 12);
            this.labelscript.TabIndex = 11;
            // 
            // labelmessage
            // 
            this.labelmessage.AutoSize = true;
            this.labelmessage.Location = new System.Drawing.Point(22, 205);
            this.labelmessage.Name = "labelmessage";
            this.labelmessage.Size = new System.Drawing.Size(0, 12);
            this.labelmessage.TabIndex = 12;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(705, 624);
            this.richTextBox1.TabIndex = 14;
            this.richTextBox1.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(259, 31);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(123, 23);
            this.button1.TabIndex = 16;
            this.button1.Text = "Custom Rect Range";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainMenuBar
            // 
            this.MainMenuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.eDITToolStripMenuItem,
            this.languageToolStripMenuItem,
            this.二值化方法ToolStripMenuItem});
            this.MainMenuBar.Location = new System.Drawing.Point(0, 0);
            this.MainMenuBar.Name = "MainMenuBar";
            this.MainMenuBar.Size = new System.Drawing.Size(1299, 25);
            this.MainMenuBar.TabIndex = 17;
            this.MainMenuBar.Text = "MenuBar";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fromFileToolStripMenuItem,
            this.fromToolStripMenuItem,
            this.fromWebCamToolStripMenuItem,
            this.fromScreenCaptureToolStripMenuItem,
            this.saveStringsToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(52, 21);
            this.toolStripMenuItem1.Text = "&Open";
            // 
            // fromFileToolStripMenuItem
            // 
            this.fromFileToolStripMenuItem.Name = "fromFileToolStripMenuItem";
            this.fromFileToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.fromFileToolStripMenuItem.Text = "from File";
            this.fromFileToolStripMenuItem.Click += new System.EventHandler(this.fromFileToolStripMenuItem_Click);
            // 
            // fromToolStripMenuItem
            // 
            this.fromToolStripMenuItem.Name = "fromToolStripMenuItem";
            this.fromToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.fromToolStripMenuItem.Text = "From VedioFile";
            // 
            // fromWebCamToolStripMenuItem
            // 
            this.fromWebCamToolStripMenuItem.Name = "fromWebCamToolStripMenuItem";
            this.fromWebCamToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.fromWebCamToolStripMenuItem.Text = "From WebCam";
            this.fromWebCamToolStripMenuItem.Click += new System.EventHandler(this.fromWebCamToolStripMenuItem_Click);
            // 
            // fromScreenCaptureToolStripMenuItem
            // 
            this.fromScreenCaptureToolStripMenuItem.Name = "fromScreenCaptureToolStripMenuItem";
            this.fromScreenCaptureToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.fromScreenCaptureToolStripMenuItem.Text = "From ScreenCapture";
            this.fromScreenCaptureToolStripMenuItem.Click += new System.EventHandler(this.fromScreenCaptureToolStripMenuItem_Click);
            // 
            // saveStringsToolStripMenuItem
            // 
            this.saveStringsToolStripMenuItem.Name = "saveStringsToolStripMenuItem";
            this.saveStringsToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.saveStringsToolStripMenuItem.Text = "&Save Strings";
            // 
            // eDITToolStripMenuItem
            // 
            this.eDITToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.autoDetectRectToolStripMenuItem,
            this.customerRectToolStripMenuItem});
            this.eDITToolStripMenuItem.Name = "eDITToolStripMenuItem";
            this.eDITToolStripMenuItem.Size = new System.Drawing.Size(47, 21);
            this.eDITToolStripMenuItem.Text = "&EDIT";
            // 
            // autoDetectRectToolStripMenuItem
            // 
            this.autoDetectRectToolStripMenuItem.Name = "autoDetectRectToolStripMenuItem";
            this.autoDetectRectToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.autoDetectRectToolStripMenuItem.Text = "Auto Detect Rect";
            this.autoDetectRectToolStripMenuItem.Click += new System.EventHandler(this.autoDetectRectToolStripMenuItem_Click);
            // 
            // customerRectToolStripMenuItem
            // 
            this.customerRectToolStripMenuItem.Name = "customerRectToolStripMenuItem";
            this.customerRectToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.customerRectToolStripMenuItem.Text = "Customer Rect";
            this.customerRectToolStripMenuItem.Click += new System.EventHandler(this.customerRectToolStripMenuItem_Click);
            // 
            // languageToolStripMenuItem
            // 
           
            this.languageToolStripMenuItem.Name = "languageToolStripMenuItem";
            this.languageToolStripMenuItem.Size = new System.Drawing.Size(77, 21);
            this.languageToolStripMenuItem.Text = "Language";
            
            // 
            // 二值化方法ToolStripMenuItem
            // 
            this.二值化方法ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.thresholdOneToolStripMenuItem,
            this.thresholdTwoToolStripMenuItem});
            this.二值化方法ToolStripMenuItem.Name = "二值化方法ToolStripMenuItem";
            this.二值化方法ToolStripMenuItem.Size = new System.Drawing.Size(128, 21);
            this.二值化方法ToolStripMenuItem.Text = "Threshold Method";
            // 
            // thresholdOneToolStripMenuItem
            // 
            this.thresholdOneToolStripMenuItem.Name = "thresholdOneToolStripMenuItem";
            this.thresholdOneToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.thresholdOneToolStripMenuItem.Text = "Threshold One";
            this.thresholdOneToolStripMenuItem.Click += new System.EventHandler(this.thresholdOneToolStripMenuItem_Click);
            // 
            // thresholdTwoToolStripMenuItem
            // 
            this.thresholdTwoToolStripMenuItem.Name = "thresholdTwoToolStripMenuItem";
            this.thresholdTwoToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.thresholdTwoToolStripMenuItem.Text = "Threshold Two";
            this.thresholdTwoToolStripMenuItem.Click += new System.EventHandler(this.thresholdTwoToolStripMenuItem_Click);
            // 
            // mainsplitContainer
            // 
            this.mainsplitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainsplitContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mainsplitContainer.Location = new System.Drawing.Point(0, 74);
            this.mainsplitContainer.Name = "mainsplitContainer";
            // 
            // mainsplitContainer.Panel1
            // 
            this.mainsplitContainer.Panel1.AutoScroll = true;
            this.mainsplitContainer.Panel1.Controls.Add(this.pictureBox2);
            // 
            // mainsplitContainer.Panel2
            // 
            this.mainsplitContainer.Panel2.AutoScroll = true;
            this.mainsplitContainer.Panel2.Controls.Add(this.pictureBox1);
            this.mainsplitContainer.Panel2.Controls.Add(this.richTextBox1);
            this.mainsplitContainer.Size = new System.Drawing.Size(1299, 626);
            this.mainsplitContainer.SplitterDistance = 588;
            this.mainsplitContainer.TabIndex = 18;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox2.Location = new System.Drawing.Point(0, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(586, 624);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 13;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.SizeChanged += new System.EventHandler(this.pictureBox2_SizeChanged);
            this.pictureBox2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseDown);
            this.pictureBox2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseMove);
            this.pictureBox2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseUp);
            this.pictureBox2.Resize += new System.EventHandler(this.pictureBox2_Resize);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(35, 235);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(638, 376);
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(686, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 21;
            this.label1.Text = "ScaleX";
            // 
            // txbfx
            // 
            this.txbfx.Location = new System.Drawing.Point(732, 36);
            this.txbfx.Name = "txbfx";
            this.txbfx.Size = new System.Drawing.Size(72, 21);
            this.txbfx.TabIndex = 22;
            this.txbfx.Validating += new System.ComponentModel.CancelEventHandler(this.txbfx_Validated);
            // 
            // txbfy
            // 
            this.txbfy.Location = new System.Drawing.Point(857, 36);
            this.txbfy.Name = "txbfy";
            this.txbfy.Size = new System.Drawing.Size(77, 21);
            this.txbfy.TabIndex = 24;
            this.txbfy.Validating += new System.ComponentModel.CancelEventHandler(this.txbfy_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(810, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 23;
            this.label2.Text = "ScaleY";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(941, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 12);
            this.label3.TabIndex = 25;
            this.label3.Text = "Threshold Value";
            // 
            // ThresholdnumericUpDown
            // 
            this.ThresholdnumericUpDown.Location = new System.Drawing.Point(1042, 39);
            this.ThresholdnumericUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.ThresholdnumericUpDown.Minimum = new decimal(new int[] {
            250,
            0,
            0,
            -2147483648});
            this.ThresholdnumericUpDown.Name = "ThresholdnumericUpDown";
            this.ThresholdnumericUpDown.Size = new System.Drawing.Size(64, 21);
            this.ThresholdnumericUpDown.TabIndex = 27;
            // 
            // picroteright
            // 
            this.picroteright.Image = global::WebPageOcr.Properties.Resources.r;
            this.picroteright.Location = new System.Drawing.Point(84, 27);
            this.picroteright.Name = "picroteright";
            this.picroteright.Size = new System.Drawing.Size(35, 37);
            this.picroteright.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picroteright.TabIndex = 20;
            this.picroteright.TabStop = false;
            this.picroteright.Click += new System.EventHandler(this.picroteright_Click);
            // 
            // picroteleft
            // 
            this.picroteleft.Image = global::WebPageOcr.Properties.Resources.leftrotate;
            this.picroteleft.Location = new System.Drawing.Point(30, 26);
            this.picroteleft.Name = "picroteleft";
            this.picroteleft.Size = new System.Drawing.Size(35, 37);
            this.picroteleft.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picroteleft.TabIndex = 19;
            this.picroteleft.TabStop = false;
            this.picroteleft.Click += new System.EventHandler(this.picroteleft_Click);
            // 
            // WebPageOcrForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1299, 712);
            this.Controls.Add(this.ThresholdnumericUpDown);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txbfy);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txbfx);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.picroteright);
            this.Controls.Add(this.picroteleft);
            this.Controls.Add(this.mainsplitContainer);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.labelmessage);
            this.Controls.Add(this.labelscript);
            this.Controls.Add(this.btnWriteText);
            this.Controls.Add(this.btnpause);
            this.Controls.Add(this.btnGetOcrString);
            this.Controls.Add(this.processid);
            this.Controls.Add(this.windowtitle);
            this.Controls.Add(this.MainMenuBar);
            this.MainMenuStrip = this.MainMenuBar;
            this.Name = "WebPageOcrForm";
            this.Text = "ImageTranslator";
            this.Load += new System.EventHandler(this.WebPageOcrForm_Load);
            this.MainMenuBar.ResumeLayout(false);
            this.MainMenuBar.PerformLayout();
            this.mainsplitContainer.Panel1.ResumeLayout(false);
            this.mainsplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainsplitContainer)).EndInit();
            this.mainsplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ThresholdnumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picroteright)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picroteleft)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label windowtitle;
        private System.Windows.Forms.Label processid;
        private System.Windows.Forms.Button btnGetOcrString;
        private System.Windows.Forms.Button btnpause;
        private System.Windows.Forms.Button btnWriteText;
        private System.Windows.Forms.Label labelscript;
        private System.Windows.Forms.Label labelmessage;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.MenuStrip MainMenuBar;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem fromFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fromToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fromWebCamToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fromScreenCaptureToolStripMenuItem;
        private System.Windows.Forms.SplitContainer mainsplitContainer;
        private System.Windows.Forms.ToolStripMenuItem eDITToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem languageToolStripMenuItem;
       
        private System.Windows.Forms.ToolStripMenuItem saveStringsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoDetectRectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customerRectToolStripMenuItem;
        private System.Windows.Forms.PictureBox picroteleft;
        private System.Windows.Forms.PictureBox picroteright;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txbfx;
        private System.Windows.Forms.TextBox txbfy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown ThresholdnumericUpDown;
        private System.Windows.Forms.ToolStripMenuItem 二值化方法ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem thresholdOneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem thresholdTwoToolStripMenuItem;
    }
}

