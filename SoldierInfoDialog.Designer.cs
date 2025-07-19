using System.Windows.Forms;

namespace CounselQuickPlatinum
{
    partial class SoldierInfoDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SoldierInfoDialog));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.editButton = new CounselQuickPlatinum.CQPGrayRectangleButton();
            this.closeButton = new CounselQuickPlatinum.CQPGrayRectangleButton();
            this.statusLabelTable = new System.Windows.Forms.TableLayoutPanel();
            this.soldierStatusLabel = new System.Windows.Forms.Label();
            this.soldierStatusEnumsLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.rankNameLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.dorLabel = new System.Windows.Forms.Label();
            this.squadLabel = new System.Windows.Forms.Label();
            this.platoonLabel = new System.Windows.Forms.Label();
            this.ageLabel = new System.Windows.Forms.Label();
            this.battalionLabel = new System.Windows.Forms.Label();
            this.unitLabel = new System.Windows.Forms.Label();
            this.formattedDORLabel = new System.Windows.Forms.Label();
            this.formattedAgeLabel = new System.Windows.Forms.Label();
            this.formattedBattalionLabel = new System.Windows.Forms.Label();
            this.formattedUnitLabel = new System.Windows.Forms.Label();
            this.formattedPlatoonLabel = new System.Windows.Forms.Label();
            this.formattedSquadLabel = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.statusLabelTable.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.statusLabelTable, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(481, 283);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.editButton, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.closeButton, 2, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(316, 245);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(152, 33);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // editButton
            // 
            this.editButton.BackgroundImage = global::CounselQuickPlatinum.Properties.Resources._65_up;
            this.editButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.editButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.editButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.editButton.FlatAppearance.BorderSize = 0;
            this.editButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.editButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.editButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.editButton.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editButton.Location = new System.Drawing.Point(3, 4);
            this.editButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(65, 25);
            this.editButton.TabIndex = 0;
            this.editButton.Text = "Edit";
            this.editButton.UseVisualStyleBackColor = true;
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.BackgroundImage = global::CounselQuickPlatinum.Properties.Resources._65_up;
            this.closeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.closeButton.FlatAppearance.BorderSize = 0;
            this.closeButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.closeButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeButton.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeButton.Location = new System.Drawing.Point(84, 4);
            this.closeButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(65, 25);
            this.closeButton.TabIndex = 1;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // statusLabelTable
            // 
            this.statusLabelTable.AutoSize = true;
            this.statusLabelTable.ColumnCount = 2;
            this.statusLabelTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.statusLabelTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.statusLabelTable.Controls.Add(this.soldierStatusLabel, 0, 0);
            this.statusLabelTable.Controls.Add(this.soldierStatusEnumsLabel, 1, 0);
            this.statusLabelTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusLabelTable.Location = new System.Drawing.Point(13, 198);
            this.statusLabelTable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.statusLabelTable.Name = "statusLabelTable";
            this.statusLabelTable.RowCount = 1;
            this.statusLabelTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.statusLabelTable.Size = new System.Drawing.Size(455, 16);
            this.statusLabelTable.TabIndex = 1;
            // 
            // soldierStatusLabel
            // 
            this.soldierStatusLabel.AutoSize = true;
            this.soldierStatusLabel.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.soldierStatusLabel.Location = new System.Drawing.Point(3, 0);
            this.soldierStatusLabel.Name = "soldierStatusLabel";
            this.soldierStatusLabel.Size = new System.Drawing.Size(85, 16);
            this.soldierStatusLabel.TabIndex = 0;
            this.soldierStatusLabel.Text = "Soldier Status:";
            // 
            // soldierStatusEnumsLabel
            // 
            this.soldierStatusEnumsLabel.AutoSize = true;
            this.soldierStatusEnumsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.soldierStatusEnumsLabel.Location = new System.Drawing.Point(94, 0);
            this.soldierStatusEnumsLabel.Name = "soldierStatusEnumsLabel";
            this.soldierStatusEnumsLabel.Size = new System.Drawing.Size(358, 16);
            this.soldierStatusEnumsLabel.TabIndex = 1;
            this.soldierStatusEnumsLabel.Text = "[No Outstanding Items]";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel5, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.pictureBox1, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(13, 14);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(455, 166);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.rankNameLabel, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel6, 0, 1);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(94, 4);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85.71428F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(358, 158);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // rankNameLabel
            // 
            this.rankNameLabel.AutoSize = true;
            this.rankNameLabel.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rankNameLabel.Location = new System.Drawing.Point(3, 3);
            this.rankNameLabel.Margin = new System.Windows.Forms.Padding(3);
            this.rankNameLabel.Name = "rankNameLabel";
            this.rankNameLabel.Size = new System.Drawing.Size(126, 16);
            this.rankNameLabel.TabIndex = 0;
            this.rankNameLabel.Text = "[Rank][Last],[First][MI]";
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Controls.Add(this.dorLabel, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.squadLabel, 0, 5);
            this.tableLayoutPanel6.Controls.Add(this.platoonLabel, 0, 4);
            this.tableLayoutPanel6.Controls.Add(this.ageLabel, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.battalionLabel, 0, 2);
            this.tableLayoutPanel6.Controls.Add(this.unitLabel, 0, 3);
            this.tableLayoutPanel6.Controls.Add(this.formattedDORLabel, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.formattedAgeLabel, 1, 1);
            this.tableLayoutPanel6.Controls.Add(this.formattedBattalionLabel, 1, 2);
            this.tableLayoutPanel6.Controls.Add(this.formattedUnitLabel, 1, 3);
            this.tableLayoutPanel6.Controls.Add(this.formattedPlatoonLabel, 1, 4);
            this.tableLayoutPanel6.Controls.Add(this.formattedSquadLabel, 1, 5);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(0, 22);
            this.tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 6;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(358, 136);
            this.tableLayoutPanel6.TabIndex = 3;
            // 
            // dorLabel
            // 
            this.dorLabel.AutoSize = true;
            this.dorLabel.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dorLabel.Location = new System.Drawing.Point(3, 0);
            this.dorLabel.Name = "dorLabel";
            this.dorLabel.Size = new System.Drawing.Size(37, 16);
            this.dorLabel.TabIndex = 1;
            this.dorLabel.Text = "DOR: ";
            // 
            // squadLabel
            // 
            this.squadLabel.AutoSize = true;
            this.squadLabel.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.squadLabel.Location = new System.Drawing.Point(3, 110);
            this.squadLabel.Name = "squadLabel";
            this.squadLabel.Size = new System.Drawing.Size(48, 16);
            this.squadLabel.TabIndex = 6;
            this.squadLabel.Text = "Squad: ";
            // 
            // platoonLabel
            // 
            this.platoonLabel.AutoSize = true;
            this.platoonLabel.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.platoonLabel.Location = new System.Drawing.Point(3, 88);
            this.platoonLabel.Name = "platoonLabel";
            this.platoonLabel.Size = new System.Drawing.Size(55, 16);
            this.platoonLabel.TabIndex = 5;
            this.platoonLabel.Text = "Platoon: ";
            // 
            // ageLabel
            // 
            this.ageLabel.AutoSize = true;
            this.ageLabel.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ageLabel.Location = new System.Drawing.Point(3, 22);
            this.ageLabel.Name = "ageLabel";
            this.ageLabel.Size = new System.Drawing.Size(36, 16);
            this.ageLabel.TabIndex = 2;
            this.ageLabel.Text = "Age: ";
            // 
            // battalionLabel
            // 
            this.battalionLabel.AutoSize = true;
            this.battalionLabel.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.battalionLabel.Location = new System.Drawing.Point(3, 44);
            this.battalionLabel.Name = "battalionLabel";
            this.battalionLabel.Size = new System.Drawing.Size(62, 16);
            this.battalionLabel.TabIndex = 3;
            this.battalionLabel.Text = "Battalion: ";
            // 
            // unitLabel
            // 
            this.unitLabel.AutoSize = true;
            this.unitLabel.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.unitLabel.Location = new System.Drawing.Point(3, 66);
            this.unitLabel.Name = "unitLabel";
            this.unitLabel.Size = new System.Drawing.Size(36, 16);
            this.unitLabel.TabIndex = 4;
            this.unitLabel.Text = "Unit: ";
            // 
            // formattedDORLabel
            // 
            this.formattedDORLabel.AutoSize = true;
            this.formattedDORLabel.Location = new System.Drawing.Point(71, 0);
            this.formattedDORLabel.Name = "formattedDORLabel";
            this.formattedDORLabel.Size = new System.Drawing.Size(11, 16);
            this.formattedDORLabel.TabIndex = 7;
            this.formattedDORLabel.Text = " ";
            // 
            // formattedAgeLabel
            // 
            this.formattedAgeLabel.AutoSize = true;
            this.formattedAgeLabel.Location = new System.Drawing.Point(71, 22);
            this.formattedAgeLabel.Name = "formattedAgeLabel";
            this.formattedAgeLabel.Size = new System.Drawing.Size(11, 16);
            this.formattedAgeLabel.TabIndex = 8;
            this.formattedAgeLabel.Text = " ";
            // 
            // formattedBattalionLabel
            // 
            this.formattedBattalionLabel.AutoSize = true;
            this.formattedBattalionLabel.Location = new System.Drawing.Point(71, 44);
            this.formattedBattalionLabel.Name = "formattedBattalionLabel";
            this.formattedBattalionLabel.Size = new System.Drawing.Size(11, 16);
            this.formattedBattalionLabel.TabIndex = 9;
            this.formattedBattalionLabel.Text = " ";
            // 
            // formattedUnitLabel
            // 
            this.formattedUnitLabel.AutoSize = true;
            this.formattedUnitLabel.Location = new System.Drawing.Point(71, 66);
            this.formattedUnitLabel.Name = "formattedUnitLabel";
            this.formattedUnitLabel.Size = new System.Drawing.Size(11, 16);
            this.formattedUnitLabel.TabIndex = 10;
            this.formattedUnitLabel.Text = " ";
            // 
            // formattedPlatoonLabel
            // 
            this.formattedPlatoonLabel.AutoSize = true;
            this.formattedPlatoonLabel.Location = new System.Drawing.Point(71, 88);
            this.formattedPlatoonLabel.Name = "formattedPlatoonLabel";
            this.formattedPlatoonLabel.Size = new System.Drawing.Size(11, 16);
            this.formattedPlatoonLabel.TabIndex = 11;
            this.formattedPlatoonLabel.Text = " ";
            // 
            // formattedSquadLabel
            // 
            this.formattedSquadLabel.AutoSize = true;
            this.formattedSquadLabel.Location = new System.Drawing.Point(71, 110);
            this.formattedSquadLabel.Name = "formattedSquadLabel";
            this.formattedSquadLabel.Size = new System.Drawing.Size(11, 16);
            this.formattedSquadLabel.TabIndex = 12;
            this.formattedSquadLabel.Text = " ";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(3, 4);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(75, 75);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // SoldierInfoDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = global::CounselQuickPlatinum.Properties.Settings.Default.DefaultBackColor;
            this.ClientSize = new System.Drawing.Size(481, 283);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(497, 321);
            this.Name = "SoldierInfoDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Soldier Info";
            this.Load += new System.EventHandler(this.SoldierInfoDialog_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.statusLabelTable.ResumeLayout(false);
            this.statusLabelTable.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private CQPGrayRectangleButton editButton;
        private CQPGrayRectangleButton closeButton;
        private System.Windows.Forms.TableLayoutPanel statusLabelTable;
        private System.Windows.Forms.Label soldierStatusLabel;
        private System.Windows.Forms.Label soldierStatusEnumsLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label rankNameLabel;
        private System.Windows.Forms.Label dorLabel;
        private System.Windows.Forms.Label ageLabel;
        private System.Windows.Forms.Label unitLabel;
        private System.Windows.Forms.Label platoonLabel;
        private System.Windows.Forms.Label squadLabel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label battalionLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Label formattedDORLabel;
        private System.Windows.Forms.DateTimePicker timePicker;
        private System.Windows.Forms.Label formattedAgeLabel;
        private System.Windows.Forms.Label formattedBattalionLabel;
        private System.Windows.Forms.Label formattedUnitLabel;
        private System.Windows.Forms.Label formattedPlatoonLabel;
        private System.Windows.Forms.Label formattedSquadLabel;
    }
}