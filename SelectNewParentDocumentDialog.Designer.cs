namespace CounselQuickPlatinum
{
    partial class SelectNewParentDocumentDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectNewParentDocumentDialog));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.cancelButton = new CounselQuickPlatinum.CQPGrayRectangleButton();
            this.selectAsParentButton = new CounselQuickPlatinum.CQPGrayRectangleButton();
            this.deletedDocumentsDataGridView = new CounselQuickPlatinum.CQPDocumentsDataGridView();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(471, 357);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.deletedDocumentsDataGridView, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(13, 16);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(445, 325);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.Controls.Add(this.cancelButton, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.selectAsParentButton, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(150, 288);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(292, 33);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // cancelButton
            // 
            this.cancelButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cancelButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cancelButton.BackgroundImage")));
            this.cancelButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.cancelButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.GreyRectSize = CounselQuickPlatinum.CQPGrayRectangleButton.CQPGreyRectButtonSize.w90;
            this.cancelButton.Location = new System.Drawing.Point(199, 4);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(90, 25);
            this.cancelButton.TabIndex = 0;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // selectAsParentButton
            // 
            this.selectAsParentButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.selectAsParentButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("selectAsParentButton.BackgroundImage")));
            this.selectAsParentButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.selectAsParentButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.selectAsParentButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.selectAsParentButton.FlatAppearance.BorderSize = 0;
            this.selectAsParentButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.selectAsParentButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.selectAsParentButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.selectAsParentButton.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectAsParentButton.GreyRectSize = CounselQuickPlatinum.CQPGrayRectangleButton.CQPGreyRectButtonSize.w180;
            this.selectAsParentButton.Location = new System.Drawing.Point(3, 4);
            this.selectAsParentButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.selectAsParentButton.Name = "selectAsParentButton";
            this.selectAsParentButton.Size = new System.Drawing.Size(180, 25);
            this.selectAsParentButton.TabIndex = 1;
            this.selectAsParentButton.Text = "Select As New Parent Document";
            this.selectAsParentButton.UseVisualStyleBackColor = true;
            this.selectAsParentButton.Click += new System.EventHandler(this.selectAsParentButton_Click);
            // 
            // deletedDocumentsDataGridView
            // 
            this.deletedDocumentsDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.deletedDocumentsDataGridView.CheckboxColumnVisible = false;
            this.deletedDocumentsDataGridView.DateColumnVisible = true;
            this.deletedDocumentsDataGridView.DocumentNameColumnVisible = true;
            this.deletedDocumentsDataGridView.FirstNameColumnVisible = false;
            this.deletedDocumentsDataGridView.LastNameColumnVisible = false;
            this.deletedDocumentsDataGridView.Location = new System.Drawing.Point(3, 4);
            this.deletedDocumentsDataGridView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.deletedDocumentsDataGridView.Name = "deletedDocumentsDataGridView";
            this.deletedDocumentsDataGridView.RankColumnVisible = false;
            this.deletedDocumentsDataGridView.Size = new System.Drawing.Size(439, 262);
            this.deletedDocumentsDataGridView.StatusColumnVisible = true;
            this.deletedDocumentsDataGridView.TabIndex = 0;
            this.deletedDocumentsDataGridView.TypeColumnVisible = false;
            // 
            // SelectNewParentDocumentDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = global::CounselQuickPlatinum.Properties.Settings.Default.DefaultBackColor;
            this.ClientSize = new System.Drawing.Size(471, 357);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "SelectNewParentDocumentDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Document...";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SelectNewParentDocumentDialog_FormClosing);
            this.Load += new System.EventHandler(this.SelectNewParentDocumentDialog_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private CQPGrayRectangleButton cancelButton;
        private CQPGrayRectangleButton selectAsParentButton;
        private CQPDocumentsDataGridView deletedDocumentsDataGridView;
    }
}