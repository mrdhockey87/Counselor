namespace CounselQuickPlatinum
{
    partial class SplashScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashScreen));
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.VersionNoLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(65, 212);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(325, 23);
            this.progressBar1.TabIndex = 0;
            // 
            // VersionNoLabel
            // 
            this.VersionNoLabel.AutoSize = true;
            this.VersionNoLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.VersionNoLabel.Location = new System.Drawing.Point(161, 238);
            this.VersionNoLabel.Name = "VersionNoLabel";
            this.VersionNoLabel.Size = new System.Drawing.Size(275, 13);
            this.VersionNoLabel.TabIndex = 1;
            this.VersionNoLabel.Text = "Version:  XXX.XXX.XXXX.XXXX - DataBase Version:  XX";
            // 
            // SplashScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = global::CounselQuickPlatinum.Properties.Resources.splash;
            this.ClientSize = new System.Drawing.Size(454, 346);
            this.ControlBox = false;
            this.Controls.Add(this.VersionNoLabel);
            this.Controls.Add(this.progressBar1);
            this.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SplashScreen";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.Load += new System.EventHandler(this.SplashScreen_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label VersionNoLabel;
    }
}