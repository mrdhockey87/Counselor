namespace CounselQuickPlatinum
{
    partial class CQPGradientPanel
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        ///// <summary> 
        ///// Clean up any resources being used.
        ///// </summary>
        ///// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.paintTimer = new System.Windows.Forms.Timer(this.components);
            this.resizeTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // resizeTimer
            // 
            //this.resizeTimer.Tick += new System.EventHandler(this.resizeTimer_Tick);
            // 
            // CQPGradientPanel
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer paintTimer;
        private System.Windows.Forms.Timer resizeTimer;
    }
}
