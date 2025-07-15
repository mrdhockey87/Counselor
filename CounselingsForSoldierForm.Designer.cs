namespace CounselQuickPlatinum
{
    partial class CounselingsForSoldierForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CounselingsForSoldierForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.button1 = new CounselQuickPlatinum.CQPGrayRectangleButton();
            this.attachToDocumentButton = new CounselQuickPlatinum.CQPGrayRectangleButton();
            this.documentsDataGridView = new CounselQuickPlatinum.CQPDocumentsDataGridView();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.documentsDataGridView, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(397, 351);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.button1, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.attachToDocumentButton, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(122, 302);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(262, 33);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.GreyRectSize = CounselQuickPlatinum.CQPGrayRectangleButton.CQPGreyRectButtonSize.w120;
            this.button1.Location = new System.Drawing.Point(139, 4);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 25);
            this.button1.TabIndex = 0;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // attachToDocumentButton
            // 
            this.attachToDocumentButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.attachToDocumentButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.attachToDocumentButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("attachToDocumentButton.BackgroundImage")));
            this.attachToDocumentButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.attachToDocumentButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.attachToDocumentButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.attachToDocumentButton.FlatAppearance.BorderSize = 0;
            this.attachToDocumentButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.attachToDocumentButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.attachToDocumentButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.attachToDocumentButton.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.attachToDocumentButton.GreyRectSize = CounselQuickPlatinum.CQPGrayRectangleButton.CQPGreyRectButtonSize.w120;
            this.attachToDocumentButton.Location = new System.Drawing.Point(3, 4);
            this.attachToDocumentButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.attachToDocumentButton.Name = "attachToDocumentButton";
            this.attachToDocumentButton.Size = new System.Drawing.Size(120, 25);
            this.attachToDocumentButton.TabIndex = 1;
            this.attachToDocumentButton.Text = "Attach To Document";
            this.attachToDocumentButton.UseVisualStyleBackColor = true;
            this.attachToDocumentButton.Click += new System.EventHandler(this.attachToDocumentButton_Click);
            // 
            // documentsDataGridView
            // 
            this.documentsDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.documentsDataGridView.CheckboxColumnVisible = false;
            this.documentsDataGridView.DateColumnVisible = true;
            this.documentsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.documentsDataGridView.DocumentNameColumnVisible = true;
            this.documentsDataGridView.FirstNameColumnVisible = false;
            this.documentsDataGridView.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.documentsDataGridView.LastNameColumnVisible = false;
            this.documentsDataGridView.Location = new System.Drawing.Point(13, 41);
            this.documentsDataGridView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.documentsDataGridView.Name = "documentsDataGridView";
            this.documentsDataGridView.RankColumnVisible = false;
            this.documentsDataGridView.Size = new System.Drawing.Size(371, 228);
            this.documentsDataGridView.StatusColumnVisible = true;
            this.documentsDataGridView.TabIndex = 0;
            this.documentsDataGridView.TypeColumnVisible = false;
            // 
            // CounselingsForSoldierForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = global::CounselQuickPlatinum.Properties.Settings.Default.DefaultBackColor;
            this.ClientSize = new System.Drawing.Size(397, 351);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "CounselingsForSoldierForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select a Counseling Document...";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CounselingsForSoldierForm_FormClosing);
            this.Load += new System.EventHandler(this.CounselingsForSoldierForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private CQPGrayRectangleButton button1;
        private CQPGrayRectangleButton attachToDocumentButton;
        private CQPDocumentsDataGridView documentsDataGridView;
    }
}