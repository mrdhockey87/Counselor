namespace CounselQuickPlatinum
{
    partial class AssessmentDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AssessmentDialog));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.titleLabel = new CounselQuickPlatinum.CQPLabel();
            this.assessmentTextbox = new CounselQuickPlatinum.CQPRichTextBox();
            this.dateOfAssessment = new CounselQuickPlatinum.CQPLabel();
            this.dateOfAssessmentDatetime = new CounselQuickPlatinum.CQPDateTimePicker();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.saveButton = new CounselQuickPlatinum.CQPGrayRectangleButton();
            this.saveAndCloseButton = new CounselQuickPlatinum.CQPGrayRectangleButton();
            this.closeWithoutSaving = new CounselQuickPlatinum.CQPGrayRectangleButton();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.assessmentLabel = new CounselQuickPlatinum.CQPLabel();
            this.assessmentInsertTemplateValuesLinkLabel = new CounselQuickPlatinum.CQPLinkLabel();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.Controls.Add(this.titleLabel, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.assessmentTextbox, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.dateOfAssessment, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.dateOfAssessmentDatetime, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 9);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 1, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 11;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(541, 454);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.Location = new System.Drawing.Point(13, 10);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(40, 22);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "title";
            // 
            // assessmentTextbox
            // 
            this.assessmentTextbox.BackColor = System.Drawing.SystemColors.Window;
            this.assessmentTextbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.assessmentTextbox.Location = new System.Drawing.Point(13, 83);
            this.assessmentTextbox.Name = "assessmentTextbox";
            this.assessmentTextbox.Size = new System.Drawing.Size(515, 248);
            this.assessmentTextbox.TabIndex = 1;
            this.assessmentTextbox.TextChanged += new System.EventHandler(this.assessmentTextbox_TextChanged);
            // 
            // dateOfAssessment
            // 
            this.dateOfAssessment.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.dateOfAssessment.AutoSize = true;
            this.dateOfAssessment.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateOfAssessment.Location = new System.Drawing.Point(424, 344);
            this.dateOfAssessment.Name = "dateOfAssessment";
            this.dateOfAssessment.Size = new System.Drawing.Size(104, 16);
            this.dateOfAssessment.TabIndex = 2;
            this.dateOfAssessment.Text = "Date of Assessment";
            // 
            // dateOfAssessmentDatetime
            // 
            this.dateOfAssessmentDatetime.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.dateOfAssessmentDatetime.AutoSize = true;
            this.dateOfAssessmentDatetime.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.dateOfAssessmentDatetime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dateOfAssessmentDatetime.CalendarFont = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateOfAssessmentDatetime.CustomFormat = "yyyy MM dd";
            this.dateOfAssessmentDatetime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateOfAssessmentDatetime.Location = new System.Drawing.Point(430, 363);
            this.dateOfAssessmentDatetime.Name = "dateOfAssessmentDatetime";
            this.dateOfAssessmentDatetime.Size = new System.Drawing.Size(98, 21);
            this.dateOfAssessmentDatetime.TabIndex = 3;
            this.dateOfAssessmentDatetime.Value = new System.DateTime(2012, 5, 7, 13, 15, 56, 609);
            this.dateOfAssessmentDatetime.ValueChanged += new System.EventHandler(this.dateOfAssessmentDatetime_ValueChanged);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.ColumnCount = 5;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.saveButton, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.saveAndCloseButton, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.closeWithoutSaving, 4, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(190, 410);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(338, 31);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // saveButton
            // 
            this.saveButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("saveButton.BackgroundImage")));
            this.saveButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.saveButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.saveButton.FlatAppearance.BorderSize = 0;
            this.saveButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.saveButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.saveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveButton.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveButton.GreyRectSize = CounselQuickPlatinum.CQPGrayRectangleButton.CQPGreyRectButtonSize.w90;
            this.saveButton.Location = new System.Drawing.Point(109, 3);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(90, 25);
            this.saveButton.TabIndex = 1;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // saveAndCloseButton
            // 
            this.saveAndCloseButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("saveAndCloseButton.BackgroundImage")));
            this.saveAndCloseButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.saveAndCloseButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.saveAndCloseButton.FlatAppearance.BorderSize = 0;
            this.saveAndCloseButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.saveAndCloseButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.saveAndCloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveAndCloseButton.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveAndCloseButton.GreyRectSize = CounselQuickPlatinum.CQPGrayRectangleButton.CQPGreyRectButtonSize.w90;
            this.saveAndCloseButton.Location = new System.Drawing.Point(3, 3);
            this.saveAndCloseButton.Name = "saveAndCloseButton";
            this.saveAndCloseButton.Size = new System.Drawing.Size(90, 25);
            this.saveAndCloseButton.TabIndex = 2;
            this.saveAndCloseButton.Text = "Save And Close";
            this.saveAndCloseButton.UseVisualStyleBackColor = true;
            this.saveAndCloseButton.Click += new System.EventHandler(this.saveAndCloseButton_Click);
            // 
            // closeWithoutSaving
            // 
            this.closeWithoutSaving.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.closeWithoutSaving.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("closeWithoutSaving.BackgroundImage")));
            this.closeWithoutSaving.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.closeWithoutSaving.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.closeWithoutSaving.FlatAppearance.BorderSize = 0;
            this.closeWithoutSaving.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.closeWithoutSaving.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.closeWithoutSaving.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeWithoutSaving.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeWithoutSaving.GreyRectSize = CounselQuickPlatinum.CQPGrayRectangleButton.CQPGreyRectButtonSize.w120;
            this.closeWithoutSaving.Location = new System.Drawing.Point(215, 3);
            this.closeWithoutSaving.Name = "closeWithoutSaving";
            this.closeWithoutSaving.Size = new System.Drawing.Size(120, 25);
            this.closeWithoutSaving.TabIndex = 0;
            this.closeWithoutSaving.Text = "Close without Saving";
            this.closeWithoutSaving.UseVisualStyleBackColor = true;
            this.closeWithoutSaving.Click += new System.EventHandler(this.button2_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.Controls.Add(this.assessmentLabel, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.assessmentInsertTemplateValuesLinkLabel, 1, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(13, 55);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(228, 22);
            this.tableLayoutPanel3.TabIndex = 6;
            // 
            // assessmentLabel
            // 
            this.assessmentLabel.AutoSize = true;
            this.assessmentLabel.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.assessmentLabel.Location = new System.Drawing.Point(3, 0);
            this.assessmentLabel.Name = "assessmentLabel";
            this.assessmentLabel.Size = new System.Drawing.Size(88, 22);
            this.assessmentLabel.TabIndex = 0;
            this.assessmentLabel.Text = "Assessment";
            // 
            // assessmentInsertTemplateValuesLinkLabel
            // 
            this.assessmentInsertTemplateValuesLinkLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.assessmentInsertTemplateValuesLinkLabel.AutoSize = true;
            this.assessmentInsertTemplateValuesLinkLabel.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.assessmentInsertTemplateValuesLinkLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.assessmentInsertTemplateValuesLinkLabel.LinkColor = System.Drawing.Color.Teal;
            this.assessmentInsertTemplateValuesLinkLabel.Location = new System.Drawing.Point(97, 3);
            this.assessmentInsertTemplateValuesLinkLabel.Name = "assessmentInsertTemplateValuesLinkLabel";
            this.assessmentInsertTemplateValuesLinkLabel.Size = new System.Drawing.Size(128, 16);
            this.assessmentInsertTemplateValuesLinkLabel.TabIndex = 1;
            this.assessmentInsertTemplateValuesLinkLabel.TabStop = true;
            this.assessmentInsertTemplateValuesLinkLabel.Text = "[Insert Template Values]";
            this.assessmentInsertTemplateValuesLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.assessmentInsertTemplateValuesLinkLabel_LinkClicked);
            // 
            // AssessmentDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = global::CounselQuickPlatinum.Properties.Settings.Default.DefaultBackColor;
            this.ClientSize = new System.Drawing.Size(541, 454);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AssessmentDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Assessment";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AssessmentDialog_FormClosing);
            this.Load += new System.EventHandler(this.AssessmentDialog_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private CQPLabel titleLabel;
        private CQPLabel assessmentLabel;
        private CQPRichTextBox assessmentTextbox;
        private CQPLabel dateOfAssessment;
        private CQPDateTimePicker dateOfAssessmentDatetime;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;

        //private System.Windows.Forms.Button saveButton;
        //private System.Windows.Forms.Button closeWithoutSaving;
        //private System.Windows.Forms.Button saveAndCloseButton;

        private CQPGrayRectangleButton saveButton;
        private CQPGrayRectangleButton closeWithoutSaving;
        private CQPGrayRectangleButton saveAndCloseButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private CQPLinkLabel assessmentInsertTemplateValuesLinkLabel;
    }
}