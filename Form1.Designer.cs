using System.Drawing;
using System.Windows.Forms;

namespace CounselQuickPlatinum
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.deleteRestoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debuggingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorAndAppearanceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.documentStatusTimerTickToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugImportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.soldiersTabButton = new CounselQuickPlatinum.CQPGraphicsButton();
            this.counselingsTabButton = new CounselQuickPlatinum.CQPGraphicsButton();
            this.documentsTabButton = new CounselQuickPlatinum.CQPGraphicsButton();
            this.reportsTabButton = new CounselQuickPlatinum.CQPGraphicsButton();
            this.referencesTabButton = new CounselQuickPlatinum.CQPGraphicsButton();
            this.resourcesTabButton = new CounselQuickPlatinum.CQPGraphicsButton();
            this.helpTabButton = new CounselQuickPlatinum.CQPGraphicsButton();
            this.cqpGraphicsButton3 = new CounselQuickPlatinum.CQPGraphicsButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cqpTabControl1 = new CounselQuickPlatinum.CQPTabControl();
            this.settingsContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.recyclingBinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importExportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cqpGraphicsCheckbox1 = new CounselQuickPlatinum.CQPGraphicsCheckbox();
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.settingsContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = global::CounselQuickPlatinum.Properties.Settings.Default.DefaultBackColor;
            this.menuStrip1.DataBindings.Add(new System.Windows.Forms.Binding("Font", global::CounselQuickPlatinum.Properties.Settings.Default, "DefaultFont", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.menuStrip1.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::CounselQuickPlatinum.Properties.Settings.Default, "DefaultForeColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.menuStrip1.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::CounselQuickPlatinum.Properties.Settings.Default, "DefaultBackColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.menuStrip1.Font = global::CounselQuickPlatinum.Properties.Settings.Default.DefaultFont;
            this.menuStrip1.ForeColor = global::CounselQuickPlatinum.Properties.Settings.Default.DefaultForeColor;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteRestoreToolStripMenuItem,
            this.debuggingToolStripMenuItem,
            this.importToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(810, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.Visible = false;
            // 
            // deleteRestoreToolStripMenuItem
            // 
            this.deleteRestoreToolStripMenuItem.Name = "deleteRestoreToolStripMenuItem";
            this.deleteRestoreToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            this.deleteRestoreToolStripMenuItem.Text = "Recycle Bin";
            this.deleteRestoreToolStripMenuItem.Click += new System.EventHandler(this.recycleBinToolStripMenuItem_Click);
            // 
            // debuggingToolStripMenuItem
            // 
            this.debuggingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.colorAndAppearanceToolStripMenuItem,
            this.documentStatusTimerTickToolStripMenuItem});
            this.debuggingToolStripMenuItem.Name = "debuggingToolStripMenuItem";
            this.debuggingToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.debuggingToolStripMenuItem.Text = "Debugging";
            // 
            // colorAndAppearanceToolStripMenuItem
            // 
            this.colorAndAppearanceToolStripMenuItem.Name = "colorAndAppearanceToolStripMenuItem";
            this.colorAndAppearanceToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.colorAndAppearanceToolStripMenuItem.Text = "Color and Appearance...";
            this.colorAndAppearanceToolStripMenuItem.Visible = false;
            this.colorAndAppearanceToolStripMenuItem.Click += new System.EventHandler(this.colorAndAppearanceToolStripMenuItem_Click);
            // 
            // documentStatusTimerTickToolStripMenuItem
            // 
            this.documentStatusTimerTickToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2});
            this.documentStatusTimerTickToolStripMenuItem.Name = "documentStatusTimerTickToolStripMenuItem";
            this.documentStatusTimerTickToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.documentStatusTimerTickToolStripMenuItem.Text = "Document status timer tick...";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(129, 22);
            this.toolStripMenuItem1.Text = "1 day";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(129, 22);
            this.toolStripMenuItem2.Text = "30 seconds";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.debugImportToolStripMenuItem,
            this.exportToolStripMenuItem});
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(89, 20);
            this.importToolStripMenuItem.Text = "Import / Export";
            // 
            // debugImportToolStripMenuItem
            // 
            this.debugImportToolStripMenuItem.Name = "debugImportToolStripMenuItem";
            this.debugImportToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.debugImportToolStripMenuItem.Text = "Import...";
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.exportToolStripMenuItem.Text = "Export...";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cqpTabControl1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(810, 528);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.tableLayoutPanel2.ColumnCount = 9;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 0F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 74F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel2.Controls.Add(this.soldiersTabButton, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.counselingsTabButton, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.documentsTabButton, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.reportsTabButton, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.referencesTabButton, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.resourcesTabButton, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.helpTabButton, 6, 0);
            this.tableLayoutPanel2.Controls.Add(this.cqpGraphicsButton3, 8, 0);
            this.tableLayoutPanel2.Controls.Add(this.pictureBox1, 7, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(810, 44);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // soldiersTabButton
            // 
            this.soldiersTabButton.ClickIcon = global::CounselQuickPlatinum.Properties.Resources.soldiers_down;
            this.soldiersTabButton.DisabledIcon = global::CounselQuickPlatinum.Properties.Resources.soldiers_disabled;
            this.soldiersTabButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.soldiersTabButton.FlatAppearance.BorderSize = 0;
            this.soldiersTabButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.soldiersTabButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.soldiersTabButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.soldiersTabButton.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.soldiersTabButton.HoverIcon = global::CounselQuickPlatinum.Properties.Resources.soldiers_highlight;
            this.soldiersTabButton.Image = global::CounselQuickPlatinum.Properties.Resources.soldiers_up;
            this.soldiersTabButton.Location = new System.Drawing.Point(0, 0);
            this.soldiersTabButton.Margin = new System.Windows.Forms.Padding(0);
            this.soldiersTabButton.Name = "soldiersTabButton";
            this.soldiersTabButton.NormalIcon = global::CounselQuickPlatinum.Properties.Resources.soldiers_up;
            this.soldiersTabButton.Size = new System.Drawing.Size(75, 44);
            this.soldiersTabButton.TabIndex = 0;
            this.soldiersTabButton.UseVisualStyleBackColor = false;
            this.soldiersTabButton.Click += new System.EventHandler(this.soldiersTabButton_Click);
            this.soldiersTabButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.soldiersTabButton_MouseUp);
            // 
            // counselingsTabButton
            // 
            this.counselingsTabButton.ClickIcon = global::CounselQuickPlatinum.Properties.Resources.counseling_down;
            this.counselingsTabButton.DisabledIcon = global::CounselQuickPlatinum.Properties.Resources.counseling_disabled;
            this.counselingsTabButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.counselingsTabButton.FlatAppearance.BorderSize = 0;
            this.counselingsTabButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.counselingsTabButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.counselingsTabButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.counselingsTabButton.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.counselingsTabButton.HoverIcon = global::CounselQuickPlatinum.Properties.Resources.counseling_highlight;
            this.counselingsTabButton.Image = global::CounselQuickPlatinum.Properties.Resources.counseling_up;
            this.counselingsTabButton.Location = new System.Drawing.Point(75, 0);
            this.counselingsTabButton.Margin = new System.Windows.Forms.Padding(0);
            this.counselingsTabButton.Name = "counselingsTabButton";
            this.counselingsTabButton.NormalIcon = global::CounselQuickPlatinum.Properties.Resources.counseling_up;
            this.counselingsTabButton.Size = new System.Drawing.Size(1, 44);
            this.counselingsTabButton.TabIndex = 1;
            this.counselingsTabButton.UseVisualStyleBackColor = false;
            this.counselingsTabButton.Visible = false;
            this.counselingsTabButton.Click += new System.EventHandler(this.counselingsTabButton_Click);
            // 
            // documentsTabButton
            // 
            this.documentsTabButton.ClickIcon = global::CounselQuickPlatinum.Properties.Resources.counseling_down;
            this.documentsTabButton.DisabledIcon = global::CounselQuickPlatinum.Properties.Resources.counseling_disabled;
            this.documentsTabButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.documentsTabButton.FlatAppearance.BorderSize = 0;
            this.documentsTabButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.documentsTabButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.documentsTabButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.documentsTabButton.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.documentsTabButton.HoverIcon = global::CounselQuickPlatinum.Properties.Resources.counseling_highlight;
            this.documentsTabButton.Image = global::CounselQuickPlatinum.Properties.Resources.counseling_up;
            this.documentsTabButton.Location = new System.Drawing.Point(75, 0);
            this.documentsTabButton.Margin = new System.Windows.Forms.Padding(0);
            this.documentsTabButton.Name = "documentsTabButton";
            this.documentsTabButton.NormalIcon = global::CounselQuickPlatinum.Properties.Resources.counseling_up;
            this.documentsTabButton.Size = new System.Drawing.Size(75, 44);
            this.documentsTabButton.TabIndex = 2;
            this.documentsTabButton.UseVisualStyleBackColor = false;
            this.documentsTabButton.Click += new System.EventHandler(this.documentsTabButton_Click);
            // 
            // reportsTabButton
            // 
            this.reportsTabButton.ClickIcon = global::CounselQuickPlatinum.Properties.Resources.reports_down;
            this.reportsTabButton.DisabledIcon = global::CounselQuickPlatinum.Properties.Resources.reports_disabled;
            this.reportsTabButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.reportsTabButton.FlatAppearance.BorderSize = 0;
            this.reportsTabButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.reportsTabButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.reportsTabButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.reportsTabButton.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reportsTabButton.HoverIcon = global::CounselQuickPlatinum.Properties.Resources.reports_highlight;
            this.reportsTabButton.Image = global::CounselQuickPlatinum.Properties.Resources.reports_up;
            this.reportsTabButton.Location = new System.Drawing.Point(150, 0);
            this.reportsTabButton.Margin = new System.Windows.Forms.Padding(0);
            this.reportsTabButton.Name = "reportsTabButton";
            this.reportsTabButton.NormalIcon = global::CounselQuickPlatinum.Properties.Resources.reports_up;
            this.reportsTabButton.Size = new System.Drawing.Size(74, 44);
            this.reportsTabButton.TabIndex = 3;
            this.reportsTabButton.UseVisualStyleBackColor = false;
            this.reportsTabButton.Click += new System.EventHandler(this.reportsTabButton_Click);
            // 
            // referencesTabButton
            // 
            this.referencesTabButton.ClickIcon = global::CounselQuickPlatinum.Properties.Resources.references_down;
            this.referencesTabButton.DisabledIcon = global::CounselQuickPlatinum.Properties.Resources.references_disabled;
            this.referencesTabButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.referencesTabButton.FlatAppearance.BorderSize = 0;
            this.referencesTabButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.referencesTabButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.referencesTabButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.referencesTabButton.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.referencesTabButton.HoverIcon = global::CounselQuickPlatinum.Properties.Resources.references_highlight;
            this.referencesTabButton.Image = global::CounselQuickPlatinum.Properties.Resources.references_up;
            this.referencesTabButton.Location = new System.Drawing.Point(224, 0);
            this.referencesTabButton.Margin = new System.Windows.Forms.Padding(0);
            this.referencesTabButton.Name = "referencesTabButton";
            this.referencesTabButton.NormalIcon = global::CounselQuickPlatinum.Properties.Resources.references_up;
            this.referencesTabButton.Size = new System.Drawing.Size(75, 44);
            this.referencesTabButton.TabIndex = 4;
            this.referencesTabButton.UseVisualStyleBackColor = false;
            this.referencesTabButton.Click += new System.EventHandler(this.referencesTabButton_Click);
            // 
            // resourcesTabButton
            // 
            this.resourcesTabButton.ClickIcon = global::CounselQuickPlatinum.Properties.Resources.resources_down;
            this.resourcesTabButton.DisabledIcon = global::CounselQuickPlatinum.Properties.Resources.resources_disabled;
            this.resourcesTabButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.resourcesTabButton.FlatAppearance.BorderSize = 0;
            this.resourcesTabButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.resourcesTabButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.resourcesTabButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resourcesTabButton.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resourcesTabButton.HoverIcon = global::CounselQuickPlatinum.Properties.Resources.resources_highlight;
            this.resourcesTabButton.Image = global::CounselQuickPlatinum.Properties.Resources.resources_up;
            this.resourcesTabButton.Location = new System.Drawing.Point(299, 0);
            this.resourcesTabButton.Margin = new System.Windows.Forms.Padding(0);
            this.resourcesTabButton.Name = "resourcesTabButton";
            this.resourcesTabButton.NormalIcon = global::CounselQuickPlatinum.Properties.Resources.resources_up;
            this.resourcesTabButton.Size = new System.Drawing.Size(75, 44);
            this.resourcesTabButton.TabIndex = 5;
            this.resourcesTabButton.UseVisualStyleBackColor = false;
            this.resourcesTabButton.Click += new System.EventHandler(this.resourcesTabButton_Click);
            // 
            // helpTabButton
            // 
            this.helpTabButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(34)))), ((int)(((byte)(43)))));
            this.helpTabButton.BackgroundImage = global::CounselQuickPlatinum.Properties.Resources.dropshadow2;
            this.helpTabButton.ClickIcon = global::CounselQuickPlatinum.Properties.Resources.help_down1;
            this.helpTabButton.DisabledIcon = global::CounselQuickPlatinum.Properties.Resources.help_disabled1;
            this.helpTabButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.helpTabButton.FlatAppearance.BorderSize = 0;
            this.helpTabButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.helpTabButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.helpTabButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.helpTabButton.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.helpTabButton.HoverIcon = global::CounselQuickPlatinum.Properties.Resources.help_highlight1;
            this.helpTabButton.Image = global::CounselQuickPlatinum.Properties.Resources.help_up1;
            this.helpTabButton.Location = new System.Drawing.Point(374, 0);
            this.helpTabButton.Margin = new System.Windows.Forms.Padding(0);
            this.helpTabButton.Name = "helpTabButton";
            this.helpTabButton.NormalIcon = global::CounselQuickPlatinum.Properties.Resources.help_up1;
            this.helpTabButton.Size = new System.Drawing.Size(75, 44);
            this.helpTabButton.TabIndex = 6;
            this.helpTabButton.UseVisualStyleBackColor = false;
            this.helpTabButton.Click += new System.EventHandler(this.helpTabButton_Click);
            // 
            // cqpGraphicsButton3
            // 
            this.cqpGraphicsButton3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cqpGraphicsButton3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(34)))), ((int)(((byte)(43)))));
            this.cqpGraphicsButton3.BackgroundImage = global::CounselQuickPlatinum.Properties.Resources.dropshadow2;
            this.cqpGraphicsButton3.ClickIcon = global::CounselQuickPlatinum.Properties.Resources.settings_down;
            this.cqpGraphicsButton3.DisabledIcon = global::CounselQuickPlatinum.Properties.Resources.settings_disabled;
            this.cqpGraphicsButton3.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.cqpGraphicsButton3.FlatAppearance.BorderSize = 0;
            this.cqpGraphicsButton3.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.cqpGraphicsButton3.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.cqpGraphicsButton3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cqpGraphicsButton3.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cqpGraphicsButton3.HoverIcon = global::CounselQuickPlatinum.Properties.Resources.settings_highlight;
            this.cqpGraphicsButton3.Image = global::CounselQuickPlatinum.Properties.Resources.settings_up;
            this.cqpGraphicsButton3.Location = new System.Drawing.Point(765, 0);
            this.cqpGraphicsButton3.Margin = new System.Windows.Forms.Padding(0);
            this.cqpGraphicsButton3.Name = "cqpGraphicsButton3";
            this.cqpGraphicsButton3.NormalIcon = global::CounselQuickPlatinum.Properties.Resources.settings_up;
            this.cqpGraphicsButton3.Size = new System.Drawing.Size(45, 44);
            this.cqpGraphicsButton3.TabIndex = 7;
            this.cqpGraphicsButton3.UseVisualStyleBackColor = false;
            this.cqpGraphicsButton3.Click += new System.EventHandler(this.OnSettingsButtonClicked);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(34)))), ((int)(((byte)(43)))));
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::CounselQuickPlatinum.Properties.Resources.dropshadow2;
            this.pictureBox1.Location = new System.Drawing.Point(449, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(316, 44);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // cqpTabControl1
            // 
            this.cqpTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cqpTabControl1.Location = new System.Drawing.Point(0, 44);
            this.cqpTabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.cqpTabControl1.Name = "cqpTabControl1";
            this.cqpTabControl1.Size = new System.Drawing.Size(810, 484);
            this.cqpTabControl1.TabIndex = 0;
            this.cqpTabControl1.Load += new System.EventHandler(this.cqpTabControl1_Load);
            // 
            // settingsContextMenu
            // 
            this.settingsContextMenu.Font = new System.Drawing.Font("Trebuchet MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.settingsContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.recyclingBinToolStripMenuItem,
            this.importExportToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.settingsContextMenu.Name = "settingsContextMenu";
            this.settingsContextMenu.Size = new System.Drawing.Size(167, 92);
            // 
            // recyclingBinToolStripMenuItem
            // 
            this.recyclingBinToolStripMenuItem.Name = "recyclingBinToolStripMenuItem";
            this.recyclingBinToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.recyclingBinToolStripMenuItem.Text = "Recycling Bin...";
            this.recyclingBinToolStripMenuItem.Click += new System.EventHandler(this.recyclingBinToolStripMenuItem_Click);
            // 
            // importExportToolStripMenuItem
            // 
            this.importExportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importToolStripMenuItem1,
            this.exportToolStripMenuItem1});
            this.importExportToolStripMenuItem.Name = "importExportToolStripMenuItem";
            this.importExportToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.importExportToolStripMenuItem.Text = "Import/Export...";
            // 
            // importToolStripMenuItem1
            // 
            this.importToolStripMenuItem1.Name = "importToolStripMenuItem1";
            this.importToolStripMenuItem1.Size = new System.Drawing.Size(260, 22);
            this.importToolStripMenuItem1.Text = "Import Soldiers and Documents...";
            this.importToolStripMenuItem1.Click += new System.EventHandler(this.ImportToolStripMenuItem1_Click);
            // 
            // exportToolStripMenuItem1
            // 
            this.exportToolStripMenuItem1.Name = "exportToolStripMenuItem1";
            this.exportToolStripMenuItem1.Size = new System.Drawing.Size(260, 22);
            this.exportToolStripMenuItem1.Text = "Export Soldiers and Documents...";
            this.exportToolStripMenuItem1.Click += new System.EventHandler(this.ExportToolStripMenuItem1_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.optionsToolStripMenuItem.Text = "Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.OptionsToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.aboutToolStripMenuItem.Text = "About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // cqpGraphicsCheckbox1
            // 
            this.cqpGraphicsCheckbox1.ActiveImage = null;
            this.cqpGraphicsCheckbox1.BackColor = System.Drawing.SystemColors.Control;
            this.cqpGraphicsCheckbox1.ClickIcon = null;
            this.cqpGraphicsCheckbox1.DisabledIcon = null;
            this.cqpGraphicsCheckbox1.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.cqpGraphicsCheckbox1.FlatAppearance.BorderSize = 0;
            this.cqpGraphicsCheckbox1.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.cqpGraphicsCheckbox1.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.cqpGraphicsCheckbox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cqpGraphicsCheckbox1.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cqpGraphicsCheckbox1.HoverIcon = null;
            this.cqpGraphicsCheckbox1.Location = new System.Drawing.Point(3, 3);
            this.cqpGraphicsCheckbox1.Name = "cqpGraphicsCheckbox1";
            this.cqpGraphicsCheckbox1.NormalIcon = null;
            this.cqpGraphicsCheckbox1.Size = new System.Drawing.Size(69, 23);
            this.cqpGraphicsCheckbox1.TabIndex = 2;
            this.cqpGraphicsCheckbox1.UseVisualStyleBackColor = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 567);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Font", global::CounselQuickPlatinum.Properties.Settings.Default, "DefaultFont", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::CounselQuickPlatinum.Properties.Settings.Default, "DefaultForeColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::CounselQuickPlatinum.Properties.Settings.Default, "DefaultBackColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Font = global::CounselQuickPlatinum.Properties.Settings.Default.DefaultFont;
            this.ForeColor = global::CounselQuickPlatinum.Properties.Settings.Default.DefaultForeColor;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(725, 555);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;         
            this.Text = "Counselor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Move += new System.EventHandler(this.Form1_Move);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.settingsContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem deleteRestoreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem debuggingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem colorAndAppearanceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem debugImportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem documentStatusTimerTickToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private CQPTabControl cqpTabControl1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private CQPGraphicsCheckbox cqpGraphicsCheckbox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private CQPGraphicsButton soldiersTabButton;
        private CQPGraphicsButton counselingsTabButton;
        private CQPGraphicsButton documentsTabButton;
        private CQPGraphicsButton reportsTabButton;
        private CQPGraphicsButton referencesTabButton;
        private CQPGraphicsButton resourcesTabButton;
        private CQPGraphicsButton helpTabButton;
        private CQPGraphicsButton cqpGraphicsButton3;
        private System.Windows.Forms.ContextMenuStrip settingsContextMenu;
        private System.Windows.Forms.ToolStripMenuItem recyclingBinToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importExportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    }
}