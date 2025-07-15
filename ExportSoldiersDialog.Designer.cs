namespace CounselQuickPlatinum
{
    partial class ExportSoldiersDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportSoldiersDialog));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.soldiersTreeView = new System.Windows.Forms.TreeView();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.selectAllButton = new CounselQuickPlatinum.CQPGrayRectangleButton();
            this.deselectAllButton = new CounselQuickPlatinum.CQPGrayRectangleButton();
            this.filepathLabel = new CounselQuickPlatinum.CQPLabel();
            this.filepathTextbox = new CounselQuickPlatinum.CQPTextbox();
            this.browseButton = new CounselQuickPlatinum.CQPGrayRectangleButton();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.cancelButton = new CounselQuickPlatinum.CQPGrayRectangleButton();
            this.exportButton = new CounselQuickPlatinum.CQPGrayRectangleButton();
            this.errorLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(408, 470);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(358, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select the Soldiers you want to export, then select a file to output to.";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel5, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.errorLabel, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(13, 33);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(382, 424);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 92F));
            this.tableLayoutPanel3.Controls.Add(this.soldiersTreeView, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.filepathLabel, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.filepathTextbox, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.browseButton, 2, 3);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 4;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(376, 361);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // soldiersTreeView
            // 
            this.soldiersTreeView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.soldiersTreeView.CheckBoxes = true;
            this.soldiersTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.soldiersTreeView.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.soldiersTreeView.Location = new System.Drawing.Point(3, 3);
            this.soldiersTreeView.Name = "soldiersTreeView";
            this.soldiersTreeView.Size = new System.Drawing.Size(258, 288);
            this.soldiersTreeView.TabIndex = 0;
            this.soldiersTreeView.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.soldiersTreeView_AfterCheck);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.tableLayoutPanel4.AutoSize = true;
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.selectAllButton, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.deselectAllButton, 0, 2);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(287, 219);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 3;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.Size = new System.Drawing.Size(86, 72);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // selectAllButton
            // 
            this.selectAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.selectAllButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("selectAllButton.BackgroundImage")));
            this.selectAllButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.selectAllButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.selectAllButton.FlatAppearance.BorderSize = 0;
            this.selectAllButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.selectAllButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.selectAllButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.selectAllButton.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectAllButton.GreyRectSize = CounselQuickPlatinum.CQPGrayRectangleButton.CQPGreyRectButtonSize.w90;
            this.selectAllButton.Location = new System.Drawing.Point(3, 3);
            this.selectAllButton.Name = "selectAllButton";
            this.selectAllButton.Size = new System.Drawing.Size(80, 25);
            this.selectAllButton.TabIndex = 0;
            this.selectAllButton.Text = "Select All";
            this.selectAllButton.UseVisualStyleBackColor = false;
            this.selectAllButton.Click += new System.EventHandler(this.selectAllButton_Click);
            // 
            // deselectAllButton
            // 
            this.deselectAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.deselectAllButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("deselectAllButton.BackgroundImage")));
            this.deselectAllButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.deselectAllButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.deselectAllButton.FlatAppearance.BorderSize = 0;
            this.deselectAllButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.deselectAllButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.deselectAllButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deselectAllButton.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deselectAllButton.GreyRectSize = CounselQuickPlatinum.CQPGrayRectangleButton.CQPGreyRectButtonSize.w90;
            this.deselectAllButton.Location = new System.Drawing.Point(3, 44);
            this.deselectAllButton.Name = "deselectAllButton";
            this.deselectAllButton.Size = new System.Drawing.Size(80, 25);
            this.deselectAllButton.TabIndex = 1;
            this.deselectAllButton.Text = "Deselect All";
            this.deselectAllButton.UseVisualStyleBackColor = false;
            this.deselectAllButton.Click += new System.EventHandler(this.deselectAllButton_Click);
            // 
            // filepathLabel
            // 
            this.filepathLabel.AutoSize = true;
            this.filepathLabel.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filepathLabel.Location = new System.Drawing.Point(3, 314);
            this.filepathLabel.Name = "filepathLabel";
            this.filepathLabel.Size = new System.Drawing.Size(68, 16);
            this.filepathLabel.TabIndex = 2;
            this.filepathLabel.Text = "Output File:";
            // 
            // filepathTextbox
            // 
            this.filepathTextbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.filepathTextbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.filepathTextbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filepathTextbox.Enabled = false;
            this.filepathTextbox.Location = new System.Drawing.Point(3, 333);
            this.filepathTextbox.Name = "filepathTextbox";
            this.filepathTextbox.Size = new System.Drawing.Size(258, 20);
            this.filepathTextbox.TabIndex = 3;
            // 
            // browseButton
            // 
            this.browseButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("browseButton.BackgroundImage")));
            this.browseButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.browseButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.browseButton.FlatAppearance.BorderSize = 0;
            this.browseButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.browseButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.browseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.browseButton.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.browseButton.GreyRectSize = CounselQuickPlatinum.CQPGrayRectangleButton.CQPGreyRectButtonSize.w90;
            this.browseButton.Location = new System.Drawing.Point(287, 333);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(86, 25);
            this.browseButton.TabIndex = 4;
            this.browseButton.Text = "Browse";
            this.browseButton.UseVisualStyleBackColor = false;
            this.browseButton.Click += new System.EventHandler(this.cqpGrayRectangleButton3_Click);
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel5.AutoSize = true;
            this.tableLayoutPanel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel5.ColumnCount = 3;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.Controls.Add(this.cancelButton, 2, 0);
            this.tableLayoutPanel5.Controls.Add(this.exportButton, 0, 0);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(217, 390);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.Size = new System.Drawing.Size(162, 31);
            this.tableLayoutPanel5.TabIndex = 2;
            // 
            // cancelButton
            // 
            this.cancelButton.BackgroundImage = global::CounselQuickPlatinum.Properties.Resources._65_up;
            this.cancelButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.cancelButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.Location = new System.Drawing.Point(94, 3);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(65, 25);
            this.cancelButton.TabIndex = 0;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            // 
            // exportButton
            // 
            this.exportButton.BackgroundImage = global::CounselQuickPlatinum.Properties.Resources._65_up;
            this.exportButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.exportButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.exportButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.exportButton.FlatAppearance.BorderSize = 0;
            this.exportButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.exportButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.exportButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exportButton.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exportButton.Location = new System.Drawing.Point(3, 3);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(65, 25);
            this.exportButton.TabIndex = 1;
            this.exportButton.Text = "Export";
            this.exportButton.UseVisualStyleBackColor = false;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // errorLabel
            // 
            this.errorLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.errorLabel.AutoSize = true;
            this.errorLabel.ForeColor = System.Drawing.Color.Red;
            this.errorLabel.Location = new System.Drawing.Point(320, 371);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(59, 16);
            this.errorLabel.TabIndex = 1;
            this.errorLabel.Text = "* Required";
            this.errorLabel.Visible = false;
            // 
            // ExportSoldiersDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = global::CounselQuickPlatinum.Properties.Settings.Default.DefaultBackColor;
            this.ClientSize = new System.Drawing.Size(408, 470);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExportSoldiersDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Export Soldiers...";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ExportSoldiersDialog_FormClosing);
            this.Load += new System.EventHandler(this.ExportSoldiersDialog_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TreeView soldiersTreeView;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private CQPGrayRectangleButton selectAllButton;
        private CQPGrayRectangleButton deselectAllButton;
        private CQPLabel filepathLabel;
        private CQPTextbox filepathTextbox;
        private CQPGrayRectangleButton browseButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private CQPGrayRectangleButton cancelButton;
        private CQPGrayRectangleButton exportButton;
        private System.Windows.Forms.Label errorLabel;
    }
}