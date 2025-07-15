namespace CounselQuickPlatinum
{
    partial class AboutDialog
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
            this.cqpGrayRectangleButton1 = new CounselQuickPlatinum.CQPGrayRectangleButton();
            this.versionLabel = new CounselQuickPlatinum.CQPLabel();
            this.databaseVersionLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cqpGrayRectangleButton1
            // 
            this.cqpGrayRectangleButton1.BackgroundImage = global::CounselQuickPlatinum.Properties.Resources._65_up;
            this.cqpGrayRectangleButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.cqpGrayRectangleButton1.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.cqpGrayRectangleButton1.FlatAppearance.BorderSize = 0;
            this.cqpGrayRectangleButton1.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.cqpGrayRectangleButton1.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.cqpGrayRectangleButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cqpGrayRectangleButton1.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cqpGrayRectangleButton1.Location = new System.Drawing.Point(382, 343);
            this.cqpGrayRectangleButton1.Name = "cqpGrayRectangleButton1";
            this.cqpGrayRectangleButton1.Size = new System.Drawing.Size(65, 25);
            this.cqpGrayRectangleButton1.TabIndex = 2;
            this.cqpGrayRectangleButton1.Text = "OK";
            this.cqpGrayRectangleButton1.UseVisualStyleBackColor = false;
            this.cqpGrayRectangleButton1.Click += new System.EventHandler(this.cqpGrayRectangleButton1_Click);
            // 
            // versionLabel
            // 
            this.versionLabel.AutoSize = true;
            this.versionLabel.BackColor = System.Drawing.Color.Transparent;
            this.versionLabel.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.versionLabel.Location = new System.Drawing.Point(159, 217);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(144, 16);
            this.versionLabel.TabIndex = 1;
            this.versionLabel.Text = "Version xxx.xxx.xxxx.xxxx";
            this.versionLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // databaseVersionLabel
            // 
            this.databaseVersionLabel.AutoSize = true;
            this.databaseVersionLabel.Location = new System.Drawing.Point(159, 233);
            this.databaseVersionLabel.Name = "databaseVersionLabel";
            this.databaseVersionLabel.Size = new System.Drawing.Size(99, 16);
            this.databaseVersionLabel.TabIndex = 3;
            this.databaseVersionLabel.Text = "Database Version:";
            // 
            // AboutDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = global::CounselQuickPlatinum.Properties.Settings.Default.DefaultBackColor;
            this.BackgroundImage = global::CounselQuickPlatinum.Properties.Resources.splash;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(456, 374);
            this.Controls.Add(this.databaseVersionLabel);
            this.Controls.Add(this.cqpGrayRectangleButton1);
            this.Controls.Add(this.versionLabel);
            this.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(472, 413);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(472, 413);
            this.Name = "AboutDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About Counselor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AboutDialog_FormClosing);
            this.Load += new System.EventHandler(this.AboutDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CQPLabel versionLabel;
        private CQPGrayRectangleButton cqpGrayRectangleButton1;
        private System.Windows.Forms.Label databaseVersionLabel;
    }
}