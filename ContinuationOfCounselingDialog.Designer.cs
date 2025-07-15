namespace CounselQuickPlatinum
{
    partial class ContinuationOfCounselingDialog
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
            this.continuationLabel = new System.Windows.Forms.Label();
            this.continuationRichTextBox = new CounselQuickPlatinum.CQPRichTextBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.exportButton = new CounselQuickPlatinum.CQPGrayRectangleButton();
            this.saveButton = new CounselQuickPlatinum.CQPGrayRectangleButton();
            this.cancelButton = new CounselQuickPlatinum.CQPGrayRectangleButton();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.nameGradeCounseleeTextbox = new CounselQuickPlatinum.CQPTextbox();
            this.nameGradeCounselorTextbox = new CounselQuickPlatinum.CQPTextbox();
            this.dateTimePicker1 = new CounselQuickPlatinum.CQPDateTimePicker();
            this.dateTimePicker2 = new CounselQuickPlatinum.CQPDateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.statusLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(644, 514);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.continuationLabel, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.continuationRichTextBox, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 0, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(8, 10);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(628, 472);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // continuationLabel
            // 
            this.continuationLabel.AutoSize = true;
            this.continuationLabel.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.continuationLabel.Location = new System.Drawing.Point(3, 0);
            this.continuationLabel.Name = "continuationLabel";
            this.continuationLabel.Size = new System.Drawing.Size(200, 22);
            this.continuationLabel.TabIndex = 0;
            this.continuationLabel.Text = "Continuation of Counseling";
            // 
            // continuationRichTextBox
            // 
            this.continuationRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.continuationRichTextBox.Location = new System.Drawing.Point(3, 26);
            this.continuationRichTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.continuationRichTextBox.Name = "continuationRichTextBox";
            this.continuationRichTextBox.Size = new System.Drawing.Size(622, 293);
            this.continuationRichTextBox.TabIndex = 1;
            this.continuationRichTextBox.TextChanged += new System.EventHandler(this.ContinuationTextBoxTextChanged);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.ColumnCount = 5;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.Controls.Add(this.exportButton, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.saveButton, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.cancelButton, 4, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(392, 435);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(233, 33);
            this.tableLayoutPanel3.TabIndex = 3;
            // 
            // exportButton
            // 
            this.exportButton.BackgroundImage = global::CounselQuickPlatinum.Properties.Resources._65_up;
            this.exportButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.exportButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.exportButton.FlatAppearance.BorderSize = 0;
            this.exportButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.exportButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.exportButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exportButton.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exportButton.Location = new System.Drawing.Point(3, 4);
            this.exportButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(65, 25);
            this.exportButton.TabIndex = 0;
            this.exportButton.Text = "Export";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.OnExportButtonClicked);
            // 
            // saveButton
            // 
            this.saveButton.BackgroundImage = global::CounselQuickPlatinum.Properties.Resources._65_up;
            this.saveButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.saveButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.saveButton.FlatAppearance.BorderSize = 0;
            this.saveButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.saveButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.saveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveButton.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveButton.Location = new System.Drawing.Point(84, 4);
            this.saveButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(65, 25);
            this.saveButton.TabIndex = 2;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.BackgroundImage = global::CounselQuickPlatinum.Properties.Resources._65_up;
            this.cancelButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.cancelButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.cancelButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.Location = new System.Drawing.Point(165, 4);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(65, 25);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.AutoSize = true;
            this.tableLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.label2, 2, 1);
            this.tableLayoutPanel4.Controls.Add(this.label3, 0, 4);
            this.tableLayoutPanel4.Controls.Add(this.label4, 2, 4);
            this.tableLayoutPanel4.Controls.Add(this.nameGradeCounseleeTextbox, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.nameGradeCounselorTextbox, 0, 5);
            this.tableLayoutPanel4.Controls.Add(this.dateTimePicker1, 2, 2);
            this.tableLayoutPanel4.Controls.Add(this.dateTimePicker2, 2, 5);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 327);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 7;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.Size = new System.Drawing.Size(622, 90);
            this.tableLayoutPanel4.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name and Grade of Counselee";
            this.label1.Visible = false;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(587, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Date";
            this.label2.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(154, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Name and Grade of Counselor";
            this.label3.Visible = false;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(587, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "Date";
            this.label4.Visible = false;
            // 
            // nameGradeCounseleeTextbox
            // 
            this.nameGradeCounseleeTextbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nameGradeCounseleeTextbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nameGradeCounseleeTextbox.Location = new System.Drawing.Point(3, 20);
            this.nameGradeCounseleeTextbox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.nameGradeCounseleeTextbox.Name = "nameGradeCounseleeTextbox";
            this.nameGradeCounseleeTextbox.Size = new System.Drawing.Size(475, 20);
            this.nameGradeCounseleeTextbox.TabIndex = 1;
            this.nameGradeCounseleeTextbox.Visible = false;
            this.nameGradeCounseleeTextbox.TextChanged += new System.EventHandler(this.OnNameTextboxTextChanged);
            // 
            // nameGradeCounselorTextbox
            // 
            this.nameGradeCounselorTextbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nameGradeCounselorTextbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nameGradeCounselorTextbox.Location = new System.Drawing.Point(3, 65);
            this.nameGradeCounselorTextbox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.nameGradeCounselorTextbox.Name = "nameGradeCounselorTextbox";
            this.nameGradeCounselorTextbox.Size = new System.Drawing.Size(475, 20);
            this.nameGradeCounselorTextbox.TabIndex = 5;
            this.nameGradeCounselorTextbox.Visible = false;
            this.nameGradeCounselorTextbox.TextChanged += new System.EventHandler(this.OnNameTextboxTextChanged);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimePicker1.AutoSize = true;
            this.dateTimePicker1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.dateTimePicker1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dateTimePicker1.CalendarFont = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.CustomFormat = "yyyy MM dd";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(521, 20);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(98, 21);
            this.dateTimePicker1.TabIndex = 3;
            this.dateTimePicker1.Value = new System.DateTime(2012, 5, 7, 13, 56, 9, 219);
            this.dateTimePicker1.Visible = false;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.OnDateTimePickerValueChanged);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimePicker2.AutoSize = true;
            this.dateTimePicker2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.dateTimePicker2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dateTimePicker2.CalendarFont = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker2.CustomFormat = "yyyy MM dd";
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker2.Location = new System.Drawing.Point(521, 65);
            this.dateTimePicker2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(98, 21);
            this.dateTimePicker2.TabIndex = 7;
            this.dateTimePicker2.Value = new System.DateTime(2012, 5, 7, 13, 56, 9, 229);
            this.dateTimePicker2.Visible = false;
            this.dateTimePicker2.ValueChanged += new System.EventHandler(this.OnDateTimePickerValueChanged);
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.statusLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(8, 490);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(628, 20);
            this.panel1.TabIndex = 1;
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.statusLabel.Location = new System.Drawing.Point(3, 0);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(11, 16);
            this.statusLabel.TabIndex = 0;
            this.statusLabel.Text = " ";
            // 
            // ContinuationOfCounselingDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = global::CounselQuickPlatinum.Properties.Settings.Default.DefaultBackColor;
            this.ClientSize = new System.Drawing.Size(644, 514);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ContinuationOfCounselingDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Continuation Of Counseling";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ContinuationOfCounselingDialog_FormClosing);
            this.Load += new System.EventHandler(this.ContinuationOfCounselingDialog_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label continuationLabel;
        private CQPRichTextBox continuationRichTextBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private CQPGrayRectangleButton cancelButton;
        private CQPGrayRectangleButton saveButton;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Panel panel1;
        private CQPGrayRectangleButton exportButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private CQPTextbox nameGradeCounseleeTextbox;
        private CQPTextbox nameGradeCounselorTextbox;
        private CQPDateTimePicker dateTimePicker1;
        private CQPDateTimePicker dateTimePicker2;
    }
}