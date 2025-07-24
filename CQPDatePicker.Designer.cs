namespace CounselQuickPlatinum
{
    partial class CQPDatePicker
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.yearCBO = new System.Windows.Forms.ComboBox();
            this.monthCBO = new System.Windows.Forms.ComboBox();
            this.dayCBO = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // yearCBO
            // 
            this.yearCBO.FormattingEnabled = true;
            this.yearCBO.Location = new System.Drawing.Point(0, 0);
            this.yearCBO.MaxDropDownItems = 20;
            this.yearCBO.Name = "yearCBO";
            this.yearCBO.Size = new System.Drawing.Size(65, 21);
            this.yearCBO.TabIndex = 0;
            // 
            // monthCBO
            // 
            this.monthCBO.FormattingEnabled = true;
            this.monthCBO.Location = new System.Drawing.Point(66, 0);
            this.monthCBO.MaxDropDownItems = 13;
            this.monthCBO.Name = "monthCBO";
            this.monthCBO.Size = new System.Drawing.Size(40, 21);
            this.monthCBO.TabIndex = 1;
            // 
            // dayCBO
            // 
            this.dayCBO.FormattingEnabled = true;
            this.dayCBO.Location = new System.Drawing.Point(107, 0);
            this.dayCBO.Name = "dayCBO";
            this.dayCBO.Size = new System.Drawing.Size(40, 21);
            this.dayCBO.TabIndex = 2;
            // 
            // CQPDatePicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dayCBO);
            this.Controls.Add(this.monthCBO);
            this.Controls.Add(this.yearCBO);
            this.Name = "CQPDatePicker";
            this.Size = new System.Drawing.Size(147, 21);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox yearCBO;
        private System.Windows.Forms.ComboBox monthCBO;
        private System.Windows.Forms.ComboBox dayCBO;
    }
}
