namespace WebPageOcr
{
    partial class CameraCaptureForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnStartCam = new System.Windows.Forms.Button();
            this.cbCameras = new System.Windows.Forms.ComboBox();
            this.camview = new System.Windows.Forms.PictureBox();
            this.btnCamRefresh = new System.Windows.Forms.Button();
            this.btnSaveScreen = new System.Windows.Forms.Button();
            this.chkCollimator = new System.Windows.Forms.CheckBox();
            this.chkROI = new System.Windows.Forms.CheckBox();
            this.chkFlipX = new System.Windows.Forms.CheckBox();
            this.chkFlipY = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.camview)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStartCam
            // 
            this.btnStartCam.Location = new System.Drawing.Point(694, 101);
            this.btnStartCam.Name = "btnStartCam";
            this.btnStartCam.Size = new System.Drawing.Size(94, 23);
            this.btnStartCam.TabIndex = 1;
            this.btnStartCam.Text = "Start";
            this.btnStartCam.UseVisualStyleBackColor = true;
            this.btnStartCam.Click += new System.EventHandler(this.btnStartCam_Click);
            // 
            // cbCameras
            // 
            this.cbCameras.FormattingEnabled = true;
            this.cbCameras.Location = new System.Drawing.Point(694, 13);
            this.cbCameras.Name = "cbCameras";
            this.cbCameras.Size = new System.Drawing.Size(99, 20);
            this.cbCameras.TabIndex = 2;
            this.cbCameras.SelectedIndexChanged += new System.EventHandler(this.cbCameras_SelectedIndexChanged);
            // 
            // camview
            // 
            this.camview.Location = new System.Drawing.Point(0, 1);
            this.camview.Name = "camview";
            this.camview.Size = new System.Drawing.Size(684, 448);
            this.camview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.camview.TabIndex = 0;
            this.camview.TabStop = false;
            // 
            // btnCamRefresh
            // 
            this.btnCamRefresh.Location = new System.Drawing.Point(694, 56);
            this.btnCamRefresh.Name = "btnCamRefresh";
            this.btnCamRefresh.Size = new System.Drawing.Size(94, 23);
            this.btnCamRefresh.TabIndex = 3;
            this.btnCamRefresh.Text = "Refresh";
            this.btnCamRefresh.UseVisualStyleBackColor = true;
            this.btnCamRefresh.Click += new System.EventHandler(this.btnCamRefresh_Click);
            // 
            // btnSaveScreen
            // 
            this.btnSaveScreen.Location = new System.Drawing.Point(694, 148);
            this.btnSaveScreen.Name = "btnSaveScreen";
            this.btnSaveScreen.Size = new System.Drawing.Size(94, 23);
            this.btnSaveScreen.TabIndex = 4;
            this.btnSaveScreen.Text = "ScreenShot";
            this.btnSaveScreen.UseVisualStyleBackColor = true;
            this.btnSaveScreen.Click += new System.EventHandler(this.btnSaveScreen_Click);
            // 
            // chkCollimator
            // 
            this.chkCollimator.AutoSize = true;
            this.chkCollimator.Location = new System.Drawing.Point(694, 197);
            this.chkCollimator.Name = "chkCollimator";
            this.chkCollimator.Size = new System.Drawing.Size(84, 16);
            this.chkCollimator.TabIndex = 5;
            this.chkCollimator.Text = "Collimator";
            this.chkCollimator.UseVisualStyleBackColor = true;
            this.chkCollimator.CheckedChanged += new System.EventHandler(this.chkCollimator_CheckedChanged);
            // 
            // chkROI
            // 
            this.chkROI.AutoSize = true;
            this.chkROI.Location = new System.Drawing.Point(694, 234);
            this.chkROI.Name = "chkROI";
            this.chkROI.Size = new System.Drawing.Size(42, 16);
            this.chkROI.TabIndex = 6;
            this.chkROI.Text = "ROI";
            this.chkROI.UseVisualStyleBackColor = true;
            this.chkROI.CheckedChanged += new System.EventHandler(this.chkROI_CheckedChanged);
            // 
            // chkFlipX
            // 
            this.chkFlipX.AutoSize = true;
            this.chkFlipX.Location = new System.Drawing.Point(694, 267);
            this.chkFlipX.Name = "chkFlipX";
            this.chkFlipX.Size = new System.Drawing.Size(54, 16);
            this.chkFlipX.TabIndex = 7;
            this.chkFlipX.Text = "FlipX";
            this.chkFlipX.UseVisualStyleBackColor = true;
            this.chkFlipX.CheckedChanged += new System.EventHandler(this.chkFlipX_CheckedChanged);
            // 
            // chkFlipY
            // 
            this.chkFlipY.AutoSize = true;
            this.chkFlipY.Location = new System.Drawing.Point(694, 300);
            this.chkFlipY.Name = "chkFlipY";
            this.chkFlipY.Size = new System.Drawing.Size(54, 16);
            this.chkFlipY.TabIndex = 8;
            this.chkFlipY.Text = "FlipY";
            this.chkFlipY.UseVisualStyleBackColor = true;
            this.chkFlipY.CheckedChanged += new System.EventHandler(this.chkFlipY_CheckedChanged);
            // 
            // CameraCaptureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.chkFlipY);
            this.Controls.Add(this.chkFlipX);
            this.Controls.Add(this.chkROI);
            this.Controls.Add(this.chkCollimator);
            this.Controls.Add(this.btnSaveScreen);
            this.Controls.Add(this.btnCamRefresh);
            this.Controls.Add(this.cbCameras);
            this.Controls.Add(this.btnStartCam);
            this.Controls.Add(this.camview);
            this.Name = "CameraCaptureForm";
            this.Text = "CameraCaptureForm";
            this.Load += new System.EventHandler(this.CameraCaptureForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.camview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnStartCam;
        private System.Windows.Forms.ComboBox cbCameras;
        private System.Windows.Forms.PictureBox camview;
        private System.Windows.Forms.Button btnCamRefresh;
        private System.Windows.Forms.Button btnSaveScreen;
        private System.Windows.Forms.CheckBox chkCollimator;
        private System.Windows.Forms.CheckBox chkROI;
        private System.Windows.Forms.CheckBox chkFlipX;
        private System.Windows.Forms.CheckBox chkFlipY;
    }
}