namespace CounselQuickPlatinum
{
    public partial class CQPButton
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
            this.SuspendLayout();
            // 
            // CQPButton
            // 
            this.FlatAppearance.BorderSize = 0;
            this.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UseVisualStyleBackColor = false;
            this.EnabledChanged += new System.EventHandler(this.CQPButton_EnabledChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.CQPButton_Paint);
            //this.Enter += new System.EventHandler(this.CQPButton_Enter);
            //this.Leave += new System.EventHandler(this.CQPButton_Leave);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CQPButton_MouseDown);
            this.MouseEnter += new System.EventHandler(this.CQPButton_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.CQPButton_MouseLeave);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CQPButton_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
