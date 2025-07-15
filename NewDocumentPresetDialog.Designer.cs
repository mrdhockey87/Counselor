namespace CounselQuickPlatinum
{
    partial class NewDocumentPresetDialog
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.newPresetNameLabel = new CounselQuickPlatinum.CQPLabel();
            this.filterNameTextbox = new CounselQuickPlatinum.CQPTextbox();
            this.baseFilterOnLabel = new System.Windows.Forms.Label();
            this.reportFilterComboBox = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.cqpGrayRectangleButton1 = new CounselQuickPlatinum.CQPGrayRectangleButton();
            this.createNewFilterButton = new CounselQuickPlatinum.CQPGrayRectangleButton();
            this.generateBlankTemplateCheckbox = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(475, 218);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.newPresetNameLabel, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.filterNameTextbox, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.baseFilterOnLabel, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.reportFilterComboBox, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 7);
            this.tableLayoutPanel2.Controls.Add(this.generateBlankTemplateCheckbox, 0, 5);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(23, 23);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 8;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(429, 172);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // newPresetNameLabel
            // 
            this.newPresetNameLabel.AutoSize = true;
            this.newPresetNameLabel.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newPresetNameLabel.Location = new System.Drawing.Point(3, 0);
            this.newPresetNameLabel.Name = "newPresetNameLabel";
            this.newPresetNameLabel.Size = new System.Drawing.Size(100, 16);
            this.newPresetNameLabel.TabIndex = 0;
            this.newPresetNameLabel.Text = "New Report Name:";
            // 
            // filterNameTextbox
            // 
            this.filterNameTextbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.filterNameTextbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filterNameTextbox.Location = new System.Drawing.Point(3, 19);
            this.filterNameTextbox.Name = "filterNameTextbox";
            this.filterNameTextbox.Size = new System.Drawing.Size(423, 20);
            this.filterNameTextbox.TabIndex = 1;
            // 
            // baseFilterOnLabel
            // 
            this.baseFilterOnLabel.AutoSize = true;
            this.baseFilterOnLabel.Location = new System.Drawing.Point(3, 62);
            this.baseFilterOnLabel.Name = "baseFilterOnLabel";
            this.baseFilterOnLabel.Size = new System.Drawing.Size(113, 16);
            this.baseFilterOnLabel.TabIndex = 2;
            this.baseFilterOnLabel.Text = "Base New Report On:";
            // 
            // reportFilterComboBox
            // 
            this.reportFilterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.reportFilterComboBox.FormattingEnabled = true;
            this.reportFilterComboBox.Location = new System.Drawing.Point(3, 81);
            this.reportFilterComboBox.Name = "reportFilterComboBox";
            this.reportFilterComboBox.Size = new System.Drawing.Size(269, 24);
            this.reportFilterComboBox.TabIndex = 3;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.Controls.Add(this.cqpGrayRectangleButton1, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.createNewFilterButton, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(264, 138);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(162, 31);
            this.tableLayoutPanel3.TabIndex = 5;
            // 
            // cqpGrayRectangleButton1
            // 
            this.cqpGrayRectangleButton1.BackColor = System.Drawing.SystemColors.Control;
            this.cqpGrayRectangleButton1.BackgroundImage = global::CounselQuickPlatinum.Properties.Resources._65_up;
            this.cqpGrayRectangleButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.cqpGrayRectangleButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cqpGrayRectangleButton1.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.cqpGrayRectangleButton1.FlatAppearance.BorderSize = 0;
            this.cqpGrayRectangleButton1.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.cqpGrayRectangleButton1.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.cqpGrayRectangleButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cqpGrayRectangleButton1.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cqpGrayRectangleButton1.Location = new System.Drawing.Point(94, 3);
            this.cqpGrayRectangleButton1.Name = "cqpGrayRectangleButton1";
            this.cqpGrayRectangleButton1.Size = new System.Drawing.Size(65, 25);
            this.cqpGrayRectangleButton1.TabIndex = 0;
            this.cqpGrayRectangleButton1.Text = "Cancel";
            this.cqpGrayRectangleButton1.UseVisualStyleBackColor = false;
            // 
            // createNewFilterButton
            // 
            this.createNewFilterButton.BackColor = System.Drawing.SystemColors.Control;
            this.createNewFilterButton.BackgroundImage = global::CounselQuickPlatinum.Properties.Resources._65_up;
            this.createNewFilterButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.createNewFilterButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.createNewFilterButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.createNewFilterButton.FlatAppearance.BorderSize = 0;
            this.createNewFilterButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.createNewFilterButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.createNewFilterButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.createNewFilterButton.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.createNewFilterButton.Location = new System.Drawing.Point(3, 3);
            this.createNewFilterButton.Name = "createNewFilterButton";
            this.createNewFilterButton.Size = new System.Drawing.Size(65, 25);
            this.createNewFilterButton.TabIndex = 1;
            this.createNewFilterButton.Text = "Create";
            this.createNewFilterButton.UseVisualStyleBackColor = false;
            this.createNewFilterButton.Click += new System.EventHandler(this.createNewFilterButton_Click);
            // 
            // generateBlankTemplateCheckbox
            // 
            this.generateBlankTemplateCheckbox.AutoSize = true;
            this.generateBlankTemplateCheckbox.Location = new System.Drawing.Point(3, 111);
            this.generateBlankTemplateCheckbox.Name = "generateBlankTemplateCheckbox";
            this.generateBlankTemplateCheckbox.Size = new System.Drawing.Size(406, 20);
            this.generateBlankTemplateCheckbox.TabIndex = 4;
            this.generateBlankTemplateCheckbox.Text = "None - Generate a Report with no filters. I will choose which filters to apply.";
            this.generateBlankTemplateCheckbox.UseVisualStyleBackColor = true;
            this.generateBlankTemplateCheckbox.CheckedChanged += new System.EventHandler(this.generateBlankTemplateCheckbox_CheckedChanged);
            // 
            // NewDocumentPresetDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(475, 235);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "NewDocumentPresetDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Custom Report";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NewDocumentPresetDialog_FormClosing);
            this.Load += new System.EventHandler(this.NewDocumentPresetDialog_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private CQPLabel newPresetNameLabel;
        private CQPTextbox filterNameTextbox;
        private System.Windows.Forms.Label baseFilterOnLabel;
        private System.Windows.Forms.ComboBox reportFilterComboBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private CQPGrayRectangleButton cqpGrayRectangleButton1;
        private CQPGrayRectangleButton createNewFilterButton;
        private System.Windows.Forms.CheckBox generateBlankTemplateCheckbox;
    }
}