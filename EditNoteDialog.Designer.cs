namespace CounselQuickPlatinum
{
    partial class EditNoteDialog
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
            this.cancelButton = new CounselQuickPlatinum.CQPGrayRectangleButton();
            this.saveNoteButton = new CounselQuickPlatinum.CQPGrayRectangleButton();
            this.subjectLabel = new System.Windows.Forms.Label();
            this.commentLabel = new System.Windows.Forms.Label();
            this.subjectTextbox = new CounselQuickPlatinum.CQPTextbox();
            this.commentRichTextBox = new CounselQuickPlatinum.CQPRichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new CounselQuickPlatinum.CQPDateTimePicker();
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
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 10);
            this.tableLayoutPanel1.Controls.Add(this.subjectLabel, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.commentLabel, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.subjectTextbox, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.commentRichTextBox, 1, 9);
            this.tableLayoutPanel1.Controls.Add(this.label1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.dateTimePicker1, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 12;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(346, 342);
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
            this.tableLayoutPanel2.Controls.Add(this.cancelButton, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.saveNoteButton, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(181, 287);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(152, 39);
            this.tableLayoutPanel2.TabIndex = 6;
            // 
            // cancelButton
            // 
            this.cancelButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cancelButton.BackgroundImage = global::CounselQuickPlatinum.Properties.Resources._65_up;
            this.cancelButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.cancelButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.Location = new System.Drawing.Point(84, 4);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(65, 31);
            this.cancelButton.TabIndex = 0;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // saveNoteButton
            // 
            this.saveNoteButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.saveNoteButton.BackgroundImage = global::CounselQuickPlatinum.Properties.Resources._65_up;
            this.saveNoteButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.saveNoteButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.saveNoteButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.saveNoteButton.FlatAppearance.BorderSize = 0;
            this.saveNoteButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.saveNoteButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.saveNoteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveNoteButton.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveNoteButton.Location = new System.Drawing.Point(3, 4);
            this.saveNoteButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.saveNoteButton.Name = "saveNoteButton";
            this.saveNoteButton.Size = new System.Drawing.Size(65, 31);
            this.saveNoteButton.TabIndex = 1;
            this.saveNoteButton.Text = "Save Note";
            this.saveNoteButton.UseVisualStyleBackColor = true;
            this.saveNoteButton.Click += new System.EventHandler(this.saveNoteButton_Click);
            // 
            // subjectLabel
            // 
            this.subjectLabel.AutoSize = true;
            this.subjectLabel.Location = new System.Drawing.Point(13, 91);
            this.subjectLabel.Name = "subjectLabel";
            this.subjectLabel.Size = new System.Drawing.Size(46, 16);
            this.subjectLabel.TabIndex = 2;
            this.subjectLabel.Text = "Subject";
            // 
            // commentLabel
            // 
            this.commentLabel.AutoSize = true;
            this.commentLabel.Location = new System.Drawing.Point(13, 147);
            this.commentLabel.Name = "commentLabel";
            this.commentLabel.Size = new System.Drawing.Size(54, 16);
            this.commentLabel.TabIndex = 4;
            this.commentLabel.Text = "Comment";
            // 
            // subjectTextbox
            // 
            this.subjectTextbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.subjectTextbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.subjectTextbox.Location = new System.Drawing.Point(13, 111);
            this.subjectTextbox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.subjectTextbox.Name = "subjectTextbox";
            this.subjectTextbox.Size = new System.Drawing.Size(320, 20);
            this.subjectTextbox.TabIndex = 3;
            // 
            // commentRichTextBox
            // 
            this.commentRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commentRichTextBox.Location = new System.Drawing.Point(13, 179);
            this.commentRichTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.commentRichTextBox.Name = "commentRichTextBox";
            this.commentRichTextBox.Size = new System.Drawing.Size(320, 100);
            this.commentRichTextBox.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Date";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.AutoSize = true;
            this.dateTimePicker1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.dateTimePicker1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dateTimePicker1.CalendarFont = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.CustomFormat = "yyyy MM dd";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(13, 41);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(98, 21);
            this.dateTimePicker1.TabIndex = 1;
            this.dateTimePicker1.Value = new System.DateTime(2012, 5, 7, 11, 46, 38, 365);
            // 
            // EditNoteDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = global::CounselQuickPlatinum.Properties.Settings.Default.DefaultBackColor;
            this.ClientSize = new System.Drawing.Size(346, 342);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(362, 358);
            this.Name = "EditNoteDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Note";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditNoteDialog_FormClosing);
            this.Load += new System.EventHandler(this.EditNoteDialog_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private CQPGrayRectangleButton cancelButton;
        private CQPGrayRectangleButton saveNoteButton;
        private System.Windows.Forms.Label subjectLabel;
        private System.Windows.Forms.Label commentLabel;
        private CQPTextbox subjectTextbox;
        private CQPRichTextBox commentRichTextBox;
        private System.Windows.Forms.Label label1;
        private CQPDateTimePicker dateTimePicker1;
    }
}