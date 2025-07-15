namespace CounselQuickPlatinum
{
    partial class XFDLEditorPage1
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
            this.titleLabel = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.organizationLabel = new System.Windows.Forms.Label();
            this.formTable = new System.Windows.Forms.TableLayoutPanel();
            this.firstLineTable = new System.Windows.Forms.TableLayoutPanel();
            this.dateOfCounselingDateTimePicker = new CounselQuickPlatinum.CQPDateTimePicker();
            this.rankComboBox = new System.Windows.Forms.ComboBox();
            this.nameTextbox = new CounselQuickPlatinum.CQPTextbox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.rankLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.dateOfCounselingLabel = new System.Windows.Forms.Label();
            this.secondLineTable = new System.Windows.Forms.TableLayoutPanel();
            this.organizationTextbox = new CounselQuickPlatinum.CQPTextbox();
            this.nameAndTitleOfCounselor = new System.Windows.Forms.Label();
            this.nameAndTitleOfCounselorTextbox = new CounselQuickPlatinum.CQPTextbox();
            this.purposeOfCounselingTextbox = new CounselQuickPlatinum.CQPRichTextBox();
            this.keyPointsOfDiscussionTextbox = new CounselQuickPlatinum.CQPRichTextBox();
            this.titleAndStatusTable = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.documentStatusComboBox = new System.Windows.Forms.ComboBox();
            this.statusOfCounselingLabel = new System.Windows.Forms.Label();
            this.purposeOfCounselingLabelsTable = new System.Windows.Forms.TableLayoutPanel();
            this.purposeOfCounselingLink = new CounselQuickPlatinum.CQPLinkLabel();
            this.purposeOfCounselingLabel = new System.Windows.Forms.Label();
            this.keyPointsOfDiscussionLabelsTable = new System.Windows.Forms.TableLayoutPanel();
            this.keyPointsOfDiscussion = new System.Windows.Forms.Label();
            this.keyPointsOfDiscussionLink = new CounselQuickPlatinum.CQPLinkLabel();
            this.statusAndProgressBarTable = new System.Windows.Forms.Panel();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.statusLabel = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.tableLayoutPanel12 = new System.Windows.Forms.TableLayoutPanel();
            this.ribbonTable = new System.Windows.Forms.TableLayoutPanel();
            this.saveButton = new CounselQuickPlatinum.CQPGraphicsButton();
            this.exportFormButton = new CounselQuickPlatinum.CQPGraphicsButton();
            this.discardChangesButton = new CounselQuickPlatinum.CQPGraphicsButton();
            this.createAssessmentButton = new CounselQuickPlatinum.CQPGraphicsButton();
            this.saveAndCloseButton = new CounselQuickPlatinum.CQPGraphicsButton();
            this.continuationButton = new CounselQuickPlatinum.CQPGraphicsButton();
            this.nextButtonTable = new System.Windows.Forms.TableLayoutPanel();
            this.cqpGraphicsButton1 = new CounselQuickPlatinum.CQPGraphicsButton();
            this.cqpLabel1 = new CounselQuickPlatinum.CQPLabel();
            this.formTable.SuspendLayout();
            this.firstLineTable.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.secondLineTable.SuspendLayout();
            this.titleAndStatusTable.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.purposeOfCounselingLabelsTable.SuspendLayout();
            this.keyPointsOfDiscussionLabelsTable.SuspendLayout();
            this.statusAndProgressBarTable.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.tableLayoutPanel12.SuspendLayout();
            this.ribbonTable.SuspendLayout();
            this.nextButtonTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.Location = new System.Drawing.Point(3, 0);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(42, 22);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Title";
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(3, 0);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(141, 16);
            this.nameLabel.TabIndex = 0;
            this.nameLabel.Text = "Counselee (Last, First, MI)";
            // 
            // organizationLabel
            // 
            this.organizationLabel.AutoSize = true;
            this.organizationLabel.Location = new System.Drawing.Point(3, 0);
            this.organizationLabel.Name = "organizationLabel";
            this.organizationLabel.Size = new System.Drawing.Size(74, 16);
            this.organizationLabel.TabIndex = 0;
            this.organizationLabel.Text = "Organization";
            // 
            // formTable
            // 
            this.formTable.AutoSize = true;
            this.formTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.formTable.ColumnCount = 1;
            this.formTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.formTable.Controls.Add(this.firstLineTable, 0, 1);
            this.formTable.Controls.Add(this.secondLineTable, 0, 2);
            this.formTable.Controls.Add(this.purposeOfCounselingTextbox, 0, 4);
            this.formTable.Controls.Add(this.keyPointsOfDiscussionTextbox, 0, 6);
            this.formTable.Controls.Add(this.titleAndStatusTable, 0, 0);
            this.formTable.Controls.Add(this.purposeOfCounselingLabelsTable, 0, 3);
            this.formTable.Controls.Add(this.keyPointsOfDiscussionLabelsTable, 0, 5);
            this.formTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formTable.Location = new System.Drawing.Point(3, 56);
            this.formTable.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.formTable.Name = "formTable";
            this.formTable.RowCount = 9;
            this.formTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.formTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.formTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.formTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.formTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.formTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.formTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.formTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.formTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.formTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.formTable.Size = new System.Drawing.Size(636, 389);
            this.formTable.TabIndex = 0;
            // 
            // firstLineTable
            // 
            this.firstLineTable.AutoSize = true;
            this.firstLineTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.firstLineTable.ColumnCount = 5;
            this.firstLineTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.firstLineTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.firstLineTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.firstLineTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.firstLineTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.firstLineTable.Controls.Add(this.dateOfCounselingDateTimePicker, 4, 1);
            this.firstLineTable.Controls.Add(this.rankComboBox, 2, 1);
            this.firstLineTable.Controls.Add(this.nameTextbox, 0, 1);
            this.firstLineTable.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.firstLineTable.Controls.Add(this.tableLayoutPanel4, 2, 0);
            this.firstLineTable.Controls.Add(this.tableLayoutPanel5, 4, 0);
            this.firstLineTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.firstLineTable.Location = new System.Drawing.Point(3, 48);
            this.firstLineTable.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.firstLineTable.Name = "firstLineTable";
            this.firstLineTable.RowCount = 2;
            this.firstLineTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.firstLineTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.firstLineTable.Size = new System.Drawing.Size(630, 48);
            this.firstLineTable.TabIndex = 0;
            // 
            // dateOfCounselingDateTimePicker
            // 
            this.dateOfCounselingDateTimePicker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dateOfCounselingDateTimePicker.AutoSize = true;
            this.dateOfCounselingDateTimePicker.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.dateOfCounselingDateTimePicker.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dateOfCounselingDateTimePicker.CalendarFont = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateOfCounselingDateTimePicker.CustomFormat = "yyyy MM dd";
            this.dateOfCounselingDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateOfCounselingDateTimePicker.Location = new System.Drawing.Point(529, 20);
            this.dateOfCounselingDateTimePicker.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dateOfCounselingDateTimePicker.Name = "dateOfCounselingDateTimePicker";
            this.dateOfCounselingDateTimePicker.Size = new System.Drawing.Size(98, 21);
            this.dateOfCounselingDateTimePicker.TabIndex = 5;
            this.dateOfCounselingDateTimePicker.Value = new System.DateTime(2012, 5, 7, 15, 39, 2, 203);
            this.dateOfCounselingDateTimePicker.ValueChanged += new System.EventHandler(this.OnChangedValue);
            // 
            // rankComboBox
            // 
            this.rankComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rankComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.rankComboBox.FormattingEnabled = true;
            this.rankComboBox.Location = new System.Drawing.Point(425, 20);
            this.rankComboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rankComboBox.Name = "rankComboBox";
            this.rankComboBox.Size = new System.Drawing.Size(69, 24);
            this.rankComboBox.TabIndex = 4;
            this.rankComboBox.SelectedIndexChanged += new System.EventHandler(this.OnChangedValue);
            // 
            // nameTextbox
            // 
            this.nameTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.nameTextbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nameTextbox.Location = new System.Drawing.Point(3, 22);
            this.nameTextbox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.nameTextbox.Name = "nameTextbox";
            this.nameTextbox.Size = new System.Drawing.Size(396, 20);
            this.nameTextbox.TabIndex = 3;
            this.nameTextbox.TextChanged += new System.EventHandler(this.OnChangedValue);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.nameLabel, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(396, 16);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel4.AutoSize = true;
            this.tableLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Controls.Add(this.rankLabel, 0, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(425, 0);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 13F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(69, 16);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // rankLabel
            // 
            this.rankLabel.AutoSize = true;
            this.rankLabel.Location = new System.Drawing.Point(3, 0);
            this.rankLabel.Name = "rankLabel";
            this.rankLabel.Size = new System.Drawing.Size(32, 16);
            this.rankLabel.TabIndex = 0;
            this.rankLabel.Text = "Rank";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.AutoSize = true;
            this.tableLayoutPanel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.Controls.Add(this.dateOfCounselingLabel, 0, 0);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(520, 0);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.Size = new System.Drawing.Size(107, 16);
            this.tableLayoutPanel5.TabIndex = 2;
            // 
            // dateOfCounselingLabel
            // 
            this.dateOfCounselingLabel.AutoSize = true;
            this.dateOfCounselingLabel.Location = new System.Drawing.Point(3, 0);
            this.dateOfCounselingLabel.Name = "dateOfCounselingLabel";
            this.dateOfCounselingLabel.Size = new System.Drawing.Size(101, 16);
            this.dateOfCounselingLabel.TabIndex = 0;
            this.dateOfCounselingLabel.Text = "Date of Counseling";
            // 
            // secondLineTable
            // 
            this.secondLineTable.AutoSize = true;
            this.secondLineTable.ColumnCount = 3;
            this.secondLineTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.secondLineTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.secondLineTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.secondLineTable.Controls.Add(this.organizationLabel, 0, 0);
            this.secondLineTable.Controls.Add(this.organizationTextbox, 0, 1);
            this.secondLineTable.Controls.Add(this.nameAndTitleOfCounselor, 2, 0);
            this.secondLineTable.Controls.Add(this.nameAndTitleOfCounselorTextbox, 2, 1);
            this.secondLineTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.secondLineTable.Location = new System.Drawing.Point(3, 96);
            this.secondLineTable.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.secondLineTable.Name = "secondLineTable";
            this.secondLineTable.RowCount = 2;
            this.secondLineTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.secondLineTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.secondLineTable.Size = new System.Drawing.Size(630, 44);
            this.secondLineTable.TabIndex = 1;
            // 
            // organizationTextbox
            // 
            this.organizationTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.organizationTextbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.organizationTextbox.Location = new System.Drawing.Point(3, 20);
            this.organizationTextbox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.organizationTextbox.Name = "organizationTextbox";
            this.organizationTextbox.Size = new System.Drawing.Size(299, 20);
            this.organizationTextbox.TabIndex = 1;
            this.organizationTextbox.TextChanged += new System.EventHandler(this.OnChangedValue);
            // 
            // nameAndTitleOfCounselor
            // 
            this.nameAndTitleOfCounselor.AutoSize = true;
            this.nameAndTitleOfCounselor.Location = new System.Drawing.Point(328, 0);
            this.nameAndTitleOfCounselor.Name = "nameAndTitleOfCounselor";
            this.nameAndTitleOfCounselor.Size = new System.Drawing.Size(171, 16);
            this.nameAndTitleOfCounselor.TabIndex = 2;
            this.nameAndTitleOfCounselor.Text = "Counselor (Last, First, MI, Rank)";
            // 
            // nameAndTitleOfCounselorTextbox
            // 
            this.nameAndTitleOfCounselorTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.nameAndTitleOfCounselorTextbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nameAndTitleOfCounselorTextbox.Location = new System.Drawing.Point(328, 20);
            this.nameAndTitleOfCounselorTextbox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.nameAndTitleOfCounselorTextbox.Name = "nameAndTitleOfCounselorTextbox";
            this.nameAndTitleOfCounselorTextbox.Size = new System.Drawing.Size(299, 20);
            this.nameAndTitleOfCounselorTextbox.TabIndex = 3;
            this.nameAndTitleOfCounselorTextbox.TextChanged += new System.EventHandler(this.OnChangedValue);
            // 
            // purposeOfCounselingTextbox
            // 
            this.purposeOfCounselingTextbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.purposeOfCounselingTextbox.Location = new System.Drawing.Point(3, 160);
            this.purposeOfCounselingTextbox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.purposeOfCounselingTextbox.Name = "purposeOfCounselingTextbox";
            this.purposeOfCounselingTextbox.Size = new System.Drawing.Size(630, 100);
            this.purposeOfCounselingTextbox.TabIndex = 3;
            this.purposeOfCounselingTextbox.TextChanged += new System.EventHandler(this.purposeOfCounselingUpdated);
            this.purposeOfCounselingTextbox.Leave += new System.EventHandler(this.purposeOfCounselingTextbox_Leave);
            // 
            // keyPointsOfDiscussionTextbox
            // 
            this.keyPointsOfDiscussionTextbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.keyPointsOfDiscussionTextbox.Location = new System.Drawing.Point(3, 280);
            this.keyPointsOfDiscussionTextbox.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.keyPointsOfDiscussionTextbox.Name = "keyPointsOfDiscussionTextbox";
            this.keyPointsOfDiscussionTextbox.Size = new System.Drawing.Size(630, 108);
            this.keyPointsOfDiscussionTextbox.TabIndex = 5;
            this.keyPointsOfDiscussionTextbox.TextChanged += new System.EventHandler(this.keyPointsOfDiscussionTextbox_TextChanged);
            // 
            // titleAndStatusTable
            // 
            this.titleAndStatusTable.AutoSize = true;
            this.titleAndStatusTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.titleAndStatusTable.ColumnCount = 2;
            this.titleAndStatusTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.titleAndStatusTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.titleAndStatusTable.Controls.Add(this.titleLabel, 0, 1);
            this.titleAndStatusTable.Controls.Add(this.tableLayoutPanel8, 1, 1);
            this.titleAndStatusTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.titleAndStatusTable.Location = new System.Drawing.Point(3, 0);
            this.titleAndStatusTable.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.titleAndStatusTable.Name = "titleAndStatusTable";
            this.titleAndStatusTable.RowCount = 2;
            this.titleAndStatusTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.titleAndStatusTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.titleAndStatusTable.Size = new System.Drawing.Size(630, 48);
            this.titleAndStatusTable.TabIndex = 6;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel8.AutoSize = true;
            this.tableLayoutPanel8.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel8.ColumnCount = 1;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Controls.Add(this.documentStatusComboBox, 0, 1);
            this.tableLayoutPanel8.Controls.Add(this.statusOfCounselingLabel, 0, 0);
            this.tableLayoutPanel8.Location = new System.Drawing.Point(500, 0);
            this.tableLayoutPanel8.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 2;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel8.Size = new System.Drawing.Size(127, 48);
            this.tableLayoutPanel8.TabIndex = 1;
            // 
            // documentStatusComboBox
            // 
            this.documentStatusComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.documentStatusComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.documentStatusComboBox.FormattingEnabled = true;
            this.documentStatusComboBox.Location = new System.Drawing.Point(3, 20);
            this.documentStatusComboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.documentStatusComboBox.Name = "documentStatusComboBox";
            this.documentStatusComboBox.Size = new System.Drawing.Size(121, 24);
            this.documentStatusComboBox.TabIndex = 1;
            this.documentStatusComboBox.SelectedIndexChanged += new System.EventHandler(this.OnChangedValue);
            // 
            // statusOfCounselingLabel
            // 
            this.statusOfCounselingLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.statusOfCounselingLabel.AutoSize = true;
            this.statusOfCounselingLabel.Location = new System.Drawing.Point(15, 0);
            this.statusOfCounselingLabel.Name = "statusOfCounselingLabel";
            this.statusOfCounselingLabel.Size = new System.Drawing.Size(109, 16);
            this.statusOfCounselingLabel.TabIndex = 0;
            this.statusOfCounselingLabel.Text = "Status of Counseling";
            // 
            // purposeOfCounselingLabelsTable
            // 
            this.purposeOfCounselingLabelsTable.AutoSize = true;
            this.purposeOfCounselingLabelsTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.purposeOfCounselingLabelsTable.ColumnCount = 2;
            this.purposeOfCounselingLabelsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.purposeOfCounselingLabelsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.purposeOfCounselingLabelsTable.Controls.Add(this.purposeOfCounselingLink, 1, 0);
            this.purposeOfCounselingLabelsTable.Controls.Add(this.purposeOfCounselingLabel, 0, 0);
            this.purposeOfCounselingLabelsTable.Location = new System.Drawing.Point(3, 140);
            this.purposeOfCounselingLabelsTable.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.purposeOfCounselingLabelsTable.Name = "purposeOfCounselingLabelsTable";
            this.purposeOfCounselingLabelsTable.RowCount = 1;
            this.purposeOfCounselingLabelsTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.purposeOfCounselingLabelsTable.Size = new System.Drawing.Size(258, 16);
            this.purposeOfCounselingLabelsTable.TabIndex = 2;
            // 
            // purposeOfCounselingLink
            // 
            this.purposeOfCounselingLink.AutoSize = true;
            this.purposeOfCounselingLink.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.purposeOfCounselingLink.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.purposeOfCounselingLink.LinkColor = System.Drawing.Color.Teal;
            this.purposeOfCounselingLink.Location = new System.Drawing.Point(128, 0);
            this.purposeOfCounselingLink.Name = "purposeOfCounselingLink";
            this.purposeOfCounselingLink.Size = new System.Drawing.Size(127, 16);
            this.purposeOfCounselingLink.TabIndex = 1;
            this.purposeOfCounselingLink.TabStop = true;
            this.purposeOfCounselingLink.Text = "[ Suggested Comments ]";
            this.purposeOfCounselingLink.Visible = false;
            this.purposeOfCounselingLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.purposeOfCounselingAddValuesClicked);
            // 
            // purposeOfCounselingLabel
            // 
            this.purposeOfCounselingLabel.AutoSize = true;
            this.purposeOfCounselingLabel.Location = new System.Drawing.Point(3, 0);
            this.purposeOfCounselingLabel.Name = "purposeOfCounselingLabel";
            this.purposeOfCounselingLabel.Size = new System.Drawing.Size(119, 16);
            this.purposeOfCounselingLabel.TabIndex = 0;
            this.purposeOfCounselingLabel.Text = "Purpose Of Counseling";
            // 
            // keyPointsOfDiscussionLabelsTable
            // 
            this.keyPointsOfDiscussionLabelsTable.AutoSize = true;
            this.keyPointsOfDiscussionLabelsTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.keyPointsOfDiscussionLabelsTable.ColumnCount = 2;
            this.keyPointsOfDiscussionLabelsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.keyPointsOfDiscussionLabelsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.keyPointsOfDiscussionLabelsTable.Controls.Add(this.keyPointsOfDiscussion, 0, 0);
            this.keyPointsOfDiscussionLabelsTable.Controls.Add(this.keyPointsOfDiscussionLink, 1, 0);
            this.keyPointsOfDiscussionLabelsTable.Location = new System.Drawing.Point(3, 264);
            this.keyPointsOfDiscussionLabelsTable.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.keyPointsOfDiscussionLabelsTable.Name = "keyPointsOfDiscussionLabelsTable";
            this.keyPointsOfDiscussionLabelsTable.RowCount = 1;
            this.keyPointsOfDiscussionLabelsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.keyPointsOfDiscussionLabelsTable.Size = new System.Drawing.Size(268, 16);
            this.keyPointsOfDiscussionLabelsTable.TabIndex = 4;
            // 
            // keyPointsOfDiscussion
            // 
            this.keyPointsOfDiscussion.AutoSize = true;
            this.keyPointsOfDiscussion.Location = new System.Drawing.Point(3, 0);
            this.keyPointsOfDiscussion.Name = "keyPointsOfDiscussion";
            this.keyPointsOfDiscussion.Size = new System.Drawing.Size(129, 16);
            this.keyPointsOfDiscussion.TabIndex = 0;
            this.keyPointsOfDiscussion.Text = "Key Points Of Discussion";
            // 
            // keyPointsOfDiscussionLink
            // 
            this.keyPointsOfDiscussionLink.AutoSize = true;
            this.keyPointsOfDiscussionLink.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.keyPointsOfDiscussionLink.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.keyPointsOfDiscussionLink.LinkColor = System.Drawing.Color.Teal;
            this.keyPointsOfDiscussionLink.Location = new System.Drawing.Point(138, 0);
            this.keyPointsOfDiscussionLink.Name = "keyPointsOfDiscussionLink";
            this.keyPointsOfDiscussionLink.Size = new System.Drawing.Size(127, 16);
            this.keyPointsOfDiscussionLink.TabIndex = 1;
            this.keyPointsOfDiscussionLink.TabStop = true;
            this.keyPointsOfDiscussionLink.Text = "[ Suggested Comments ]";
            this.keyPointsOfDiscussionLink.Visible = false;
            this.keyPointsOfDiscussionLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.KeyPointsOfDiscussionLinkClicked);
            // 
            // statusAndProgressBarTable
            // 
            this.statusAndProgressBarTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.statusAndProgressBarTable.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.statusAndProgressBarTable.Controls.Add(this.tableLayoutPanel9);
            this.statusAndProgressBarTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusAndProgressBarTable.Location = new System.Drawing.Point(3, 511);
            this.statusAndProgressBarTable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.statusAndProgressBarTable.Name = "statusAndProgressBarTable";
            this.statusAndProgressBarTable.Size = new System.Drawing.Size(636, 22);
            this.statusAndProgressBarTable.TabIndex = 2;
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.AutoSize = true;
            this.tableLayoutPanel9.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel9.ColumnCount = 2;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.tableLayoutPanel9.Controls.Add(this.statusLabel, 0, 0);
            this.tableLayoutPanel9.Controls.Add(this.progressBar1, 1, 0);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 1;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(632, 18);
            this.tableLayoutPanel9.TabIndex = 0;
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(3, 0);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(11, 16);
            this.statusLabel.TabIndex = 0;
            this.statusLabel.Text = " ";
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar1.Location = new System.Drawing.Point(510, 3);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(119, 12);
            this.progressBar1.TabIndex = 1;
            this.progressBar1.Visible = false;
            // 
            // tableLayoutPanel12
            // 
            this.tableLayoutPanel12.AutoSize = true;
            this.tableLayoutPanel12.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel12.ColumnCount = 1;
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel12.Controls.Add(this.statusAndProgressBarTable, 0, 3);
            this.tableLayoutPanel12.Controls.Add(this.formTable, 0, 1);
            this.tableLayoutPanel12.Controls.Add(this.ribbonTable, 0, 0);
            this.tableLayoutPanel12.Controls.Add(this.nextButtonTable, 0, 2);
            this.tableLayoutPanel12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel12.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel12.Name = "tableLayoutPanel12";
            this.tableLayoutPanel12.RowCount = 4;
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel12.Size = new System.Drawing.Size(642, 537);
            this.tableLayoutPanel12.TabIndex = 0;
            // 
            // ribbonTable
            // 
            this.ribbonTable.AutoSize = true;
            this.ribbonTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ribbonTable.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ribbonTable.ColumnCount = 6;
            this.ribbonTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.ribbonTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.ribbonTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.ribbonTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.ribbonTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.ribbonTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.ribbonTable.Controls.Add(this.saveButton, 0, 0);
            this.ribbonTable.Controls.Add(this.exportFormButton, 2, 0);
            this.ribbonTable.Controls.Add(this.discardChangesButton, 5, 0);
            this.ribbonTable.Controls.Add(this.createAssessmentButton, 4, 0);
            this.ribbonTable.Controls.Add(this.saveAndCloseButton, 1, 0);
            this.ribbonTable.Controls.Add(this.continuationButton, 3, 0);
            this.ribbonTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ribbonTable.Location = new System.Drawing.Point(3, 0);
            this.ribbonTable.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.ribbonTable.MinimumSize = new System.Drawing.Size(636, 56);
            this.ribbonTable.Name = "ribbonTable";
            this.ribbonTable.RowCount = 1;
            this.ribbonTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.ribbonTable.Size = new System.Drawing.Size(636, 56);
            this.ribbonTable.TabIndex = 3;
            // 
            // saveButton
            // 
            this.saveButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.saveButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.saveButton.ClickIcon = global::CounselQuickPlatinum.Properties.Resources.save_down1;
            this.saveButton.DisabledIcon = global::CounselQuickPlatinum.Properties.Resources.save_disabled1;
            this.saveButton.FlatAppearance.BorderSize = 0;
            this.saveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveButton.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveButton.HoverIcon = global::CounselQuickPlatinum.Properties.Resources.save_highlight1;
            this.saveButton.Image = global::CounselQuickPlatinum.Properties.Resources.save_up1;
            this.saveButton.Location = new System.Drawing.Point(3, 3);
            this.saveButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.saveButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.saveButton.Name = "saveButton";
            this.saveButton.NormalIcon = global::CounselQuickPlatinum.Properties.Resources.save_up1;
            this.saveButton.Size = new System.Drawing.Size(100, 50);
            this.saveButton.TabIndex = 0;
            this.saveButton.UseVisualStyleBackColor = false;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // exportFormButton
            // 
            this.exportFormButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.exportFormButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.exportFormButton.ClickIcon = global::CounselQuickPlatinum.Properties.Resources.xfdl_down;
            this.exportFormButton.DisabledIcon = global::CounselQuickPlatinum.Properties.Resources.xfdl_disabled;
            this.exportFormButton.FlatAppearance.BorderSize = 0;
            this.exportFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exportFormButton.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exportFormButton.HoverIcon = global::CounselQuickPlatinum.Properties.Resources.xfdl_highlight;
            this.exportFormButton.Image = global::CounselQuickPlatinum.Properties.Resources.xfdl_up;
            this.exportFormButton.Location = new System.Drawing.Point(213, 3);
            this.exportFormButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.exportFormButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.exportFormButton.Name = "exportFormButton";
            this.exportFormButton.NormalIcon = global::CounselQuickPlatinum.Properties.Resources.xfdl_up;
            this.exportFormButton.Size = new System.Drawing.Size(100, 50);
            this.exportFormButton.TabIndex = 2;
            this.exportFormButton.UseVisualStyleBackColor = false;
            this.exportFormButton.Click += new System.EventHandler(this.exportToLotusFormsButton_Click);
            // 
            // discardChangesButton
            // 
            this.discardChangesButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.discardChangesButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.discardChangesButton.ClickIcon = global::CounselQuickPlatinum.Properties.Resources.discard_down;
            this.discardChangesButton.DisabledIcon = global::CounselQuickPlatinum.Properties.Resources.discard_disabled;
            this.discardChangesButton.FlatAppearance.BorderSize = 0;
            this.discardChangesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.discardChangesButton.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.discardChangesButton.HoverIcon = global::CounselQuickPlatinum.Properties.Resources.discard_highlight;
            this.discardChangesButton.Image = global::CounselQuickPlatinum.Properties.Resources.discard_up;
            this.discardChangesButton.Location = new System.Drawing.Point(530, 3);
            this.discardChangesButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.discardChangesButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.discardChangesButton.Name = "discardChangesButton";
            this.discardChangesButton.NormalIcon = global::CounselQuickPlatinum.Properties.Resources.discard_up;
            this.discardChangesButton.Size = new System.Drawing.Size(100, 50);
            this.discardChangesButton.TabIndex = 5;
            this.discardChangesButton.UseVisualStyleBackColor = false;
            this.discardChangesButton.Click += new System.EventHandler(this.discardChangesButton_Click);
            // 
            // createAssessmentButton
            // 
            this.createAssessmentButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.createAssessmentButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.createAssessmentButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.createAssessmentButton.ClickIcon = global::CounselQuickPlatinum.Properties.Resources.assess_down;
            this.createAssessmentButton.DisabledIcon = global::CounselQuickPlatinum.Properties.Resources.assess_disabled;
            this.createAssessmentButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.createAssessmentButton.FlatAppearance.BorderSize = 0;
            this.createAssessmentButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.createAssessmentButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.createAssessmentButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.createAssessmentButton.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.createAssessmentButton.HoverIcon = global::CounselQuickPlatinum.Properties.Resources.assess_highlight;
            this.createAssessmentButton.Image = global::CounselQuickPlatinum.Properties.Resources.assess_up;
            this.createAssessmentButton.Location = new System.Drawing.Point(423, 3);
            this.createAssessmentButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.createAssessmentButton.Name = "createAssessmentButton";
            this.createAssessmentButton.NormalIcon = global::CounselQuickPlatinum.Properties.Resources.assess_up;
            this.createAssessmentButton.Size = new System.Drawing.Size(100, 50);
            this.createAssessmentButton.TabIndex = 4;
            this.createAssessmentButton.UseVisualStyleBackColor = true;
            this.createAssessmentButton.Click += new System.EventHandler(this.createAssessmentButton_Click);
            // 
            // saveAndCloseButton
            // 
            this.saveAndCloseButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.saveAndCloseButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.saveAndCloseButton.ClickIcon = global::CounselQuickPlatinum.Properties.Resources.savex_down;
            this.saveAndCloseButton.DisabledIcon = global::CounselQuickPlatinum.Properties.Resources.savex_disabled;
            this.saveAndCloseButton.FlatAppearance.BorderSize = 0;
            this.saveAndCloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveAndCloseButton.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveAndCloseButton.HoverIcon = global::CounselQuickPlatinum.Properties.Resources.savex_highlight;
            this.saveAndCloseButton.Image = global::CounselQuickPlatinum.Properties.Resources.savex_up;
            this.saveAndCloseButton.Location = new System.Drawing.Point(108, 3);
            this.saveAndCloseButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.saveAndCloseButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.saveAndCloseButton.Name = "saveAndCloseButton";
            this.saveAndCloseButton.NormalIcon = global::CounselQuickPlatinum.Properties.Resources.savex_up;
            this.saveAndCloseButton.Size = new System.Drawing.Size(100, 50);
            this.saveAndCloseButton.TabIndex = 1;
            this.saveAndCloseButton.UseVisualStyleBackColor = false;
            this.saveAndCloseButton.Click += new System.EventHandler(this.saveAndCloseButton_Click);
            // 
            // continuationButton
            // 
            this.continuationButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.continuationButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.continuationButton.ClickIcon = global::CounselQuickPlatinum.Properties.Resources.cont_down;
            this.continuationButton.DisabledIcon = global::CounselQuickPlatinum.Properties.Resources.cont_disabled;
            this.continuationButton.FlatAppearance.BorderSize = 0;
            this.continuationButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.continuationButton.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.continuationButton.HoverIcon = global::CounselQuickPlatinum.Properties.Resources.cont_highlight;
            this.continuationButton.Image = global::CounselQuickPlatinum.Properties.Resources.cont_up;
            this.continuationButton.Location = new System.Drawing.Point(318, 3);
            this.continuationButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.continuationButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.continuationButton.Name = "continuationButton";
            this.continuationButton.NormalIcon = global::CounselQuickPlatinum.Properties.Resources.cont_up;
            this.continuationButton.Size = new System.Drawing.Size(100, 50);
            this.continuationButton.TabIndex = 3;
            this.continuationButton.UseVisualStyleBackColor = false;
            this.continuationButton.Click += new System.EventHandler(this.continuationButton_Click);
            // 
            // nextButtonTable
            // 
            this.nextButtonTable.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.nextButtonTable.AutoSize = true;
            this.nextButtonTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.nextButtonTable.ColumnCount = 2;
            this.nextButtonTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.nextButtonTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.nextButtonTable.Controls.Add(this.cqpGraphicsButton1, 1, 0);
            this.nextButtonTable.Controls.Add(this.cqpLabel1, 0, 0);
            this.nextButtonTable.Location = new System.Drawing.Point(515, 445);
            this.nextButtonTable.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.nextButtonTable.Name = "nextButtonTable";
            this.nextButtonTable.RowCount = 1;
            this.nextButtonTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.nextButtonTable.Size = new System.Drawing.Size(124, 62);
            this.nextButtonTable.TabIndex = 1;
            // 
            // cqpGraphicsButton1
            // 
            this.cqpGraphicsButton1.AutoSize = true;
            this.cqpGraphicsButton1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cqpGraphicsButton1.ClickIcon = global::CounselQuickPlatinum.Properties.Resources.next_down;
            this.cqpGraphicsButton1.DisabledIcon = global::CounselQuickPlatinum.Properties.Resources.next_disabled;
            this.cqpGraphicsButton1.FlatAppearance.BorderSize = 0;
            this.cqpGraphicsButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cqpGraphicsButton1.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cqpGraphicsButton1.HoverIcon = global::CounselQuickPlatinum.Properties.Resources.next_highlight;
            this.cqpGraphicsButton1.Image = global::CounselQuickPlatinum.Properties.Resources.next_up;
            this.cqpGraphicsButton1.Location = new System.Drawing.Point(65, 3);
            this.cqpGraphicsButton1.Name = "cqpGraphicsButton1";
            this.cqpGraphicsButton1.NormalIcon = global::CounselQuickPlatinum.Properties.Resources.next_up;
            this.cqpGraphicsButton1.Size = new System.Drawing.Size(56, 56);
            this.cqpGraphicsButton1.TabIndex = 1;
            this.cqpGraphicsButton1.UseVisualStyleBackColor = false;
            this.cqpGraphicsButton1.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // cqpLabel1
            // 
            this.cqpLabel1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.cqpLabel1.AutoSize = true;
            this.cqpLabel1.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cqpLabel1.Location = new System.Drawing.Point(27, 23);
            this.cqpLabel1.Name = "cqpLabel1";
            this.cqpLabel1.Size = new System.Drawing.Size(32, 16);
            this.cqpLabel1.TabIndex = 0;
            this.cqpLabel1.Text = "Next";
            // 
            // XFDLEditorPage1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = global::CounselQuickPlatinum.Properties.Settings.Default.DefaultBackColor;
            this.ClientSize = new System.Drawing.Size(642, 537);
            this.Controls.Add(this.tableLayoutPanel12);
            this.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(658, 575);
            this.Name = "XFDLEditorPage1";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Counseling - Page 1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.XFDLEditorPage1_FormClosing);
            this.Load += new System.EventHandler(this.XFDLEditorPage1_Load);
            this.VisibleChanged += new System.EventHandler(this.XFDLEditorPage1_VisibleChanged);
            this.formTable.ResumeLayout(false);
            this.formTable.PerformLayout();
            this.firstLineTable.ResumeLayout(false);
            this.firstLineTable.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.secondLineTable.ResumeLayout(false);
            this.secondLineTable.PerformLayout();
            this.titleAndStatusTable.ResumeLayout(false);
            this.titleAndStatusTable.PerformLayout();
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            this.purposeOfCounselingLabelsTable.ResumeLayout(false);
            this.purposeOfCounselingLabelsTable.PerformLayout();
            this.keyPointsOfDiscussionLabelsTable.ResumeLayout(false);
            this.keyPointsOfDiscussionLabelsTable.PerformLayout();
            this.statusAndProgressBarTable.ResumeLayout(false);
            this.statusAndProgressBarTable.PerformLayout();
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel9.PerformLayout();
            this.tableLayoutPanel12.ResumeLayout(false);
            this.tableLayoutPanel12.PerformLayout();
            this.ribbonTable.ResumeLayout(false);
            this.nextButtonTable.ResumeLayout(false);
            this.nextButtonTable.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label organizationLabel;
        private CQPTextbox organizationTextbox;
        private System.Windows.Forms.TableLayoutPanel formTable;
        private System.Windows.Forms.TableLayoutPanel firstLineTable;
        private CQPDateTimePicker dateOfCounselingDateTimePicker;
        private System.Windows.Forms.Label dateOfCounselingLabel;
        private System.Windows.Forms.ComboBox rankComboBox;
        private CQPTextbox nameTextbox;
        private System.Windows.Forms.Label rankLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TableLayoutPanel secondLineTable;
        private System.Windows.Forms.Label nameAndTitleOfCounselor;
        private CQPTextbox nameAndTitleOfCounselorTextbox;
        private System.Windows.Forms.Label purposeOfCounselingLabel;
        private System.Windows.Forms.Label keyPointsOfDiscussion;
        private CQPRichTextBox purposeOfCounselingTextbox;
        private CQPRichTextBox keyPointsOfDiscussionTextbox;
        private System.Windows.Forms.TableLayoutPanel titleAndStatusTable;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.ComboBox documentStatusComboBox;
        private System.Windows.Forms.Label statusOfCounselingLabel;
        private System.Windows.Forms.Panel statusAndProgressBarTable;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.TableLayoutPanel purposeOfCounselingLabelsTable;
        private CQPLinkLabel purposeOfCounselingLink;
        private System.Windows.Forms.TableLayoutPanel keyPointsOfDiscussionLabelsTable;
        private CQPLinkLabel keyPointsOfDiscussionLink;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel12;
        private System.Windows.Forms.TableLayoutPanel ribbonTable;
        private CQPGraphicsButton saveButton;
        private CQPGraphicsButton saveAndCloseButton;
        private CQPGraphicsButton exportFormButton;
        private CQPGraphicsButton continuationButton;
        private CQPGraphicsButton discardChangesButton;
        private System.Windows.Forms.TableLayoutPanel nextButtonTable;
        private CQPGraphicsButton cqpGraphicsButton1;
        private CQPLabel cqpLabel1;
        private CQPGraphicsButton createAssessmentButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}