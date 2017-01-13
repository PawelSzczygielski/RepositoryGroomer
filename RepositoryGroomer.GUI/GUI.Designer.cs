namespace RepositoryGroomer.GUI
{
    partial class GUI
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
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem(new string[] {
            "Sth1",
            "Sth12",
            "Sth12",
            "",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("");
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem("");
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem("");
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FeaturesTab = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.LinkedFilesSettingsGroupBox = new System.Windows.Forms.GroupBox();
            this.textBoxPathToRepository = new System.Windows.Forms.TextBox();
            this.labelLFSettingsPath = new System.Windows.Forms.Label();
            this.linkedFilesListView = new System.Windows.Forms.ListView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnRepositoryPath = new System.Windows.Forms.Button();
            this.folderBrowserDialogRepositoryPath = new System.Windows.Forms.FolderBrowserDialog();
            this.menuStrip1.SuspendLayout();
            this.FeaturesTab.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.LinkedFilesSettingsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuItem,
            this.settingsMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(865, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileMenuItem
            // 
            this.fileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitMenuItem});
            this.fileMenuItem.Name = "fileMenuItem";
            this.fileMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileMenuItem.Text = "File";
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Name = "exitMenuItem";
            this.exitMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitMenuItem.Text = "Exit";
            // 
            // settingsMenuItem
            // 
            this.settingsMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveMenuItem});
            this.settingsMenuItem.Name = "settingsMenuItem";
            this.settingsMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsMenuItem.Text = "Settings";
            // 
            // saveMenuItem
            // 
            this.saveMenuItem.Name = "saveMenuItem";
            this.saveMenuItem.Size = new System.Drawing.Size(98, 22);
            this.saveMenuItem.Text = "Save";
            // 
            // FeaturesTab
            // 
            this.FeaturesTab.Controls.Add(this.tabPage1);
            this.FeaturesTab.Controls.Add(this.tabPage2);
            this.FeaturesTab.Location = new System.Drawing.Point(0, 27);
            this.FeaturesTab.Name = "FeaturesTab";
            this.FeaturesTab.SelectedIndex = 0;
            this.FeaturesTab.Size = new System.Drawing.Size(865, 450);
            this.FeaturesTab.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.LinkedFilesSettingsGroupBox);
            this.tabPage1.Controls.Add(this.linkedFilesListView);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(857, 424);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Linked Files";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // LinkedFilesSettingsGroupBox
            // 
            this.LinkedFilesSettingsGroupBox.Controls.Add(this.btnRepositoryPath);
            this.LinkedFilesSettingsGroupBox.Controls.Add(this.textBoxPathToRepository);
            this.LinkedFilesSettingsGroupBox.Controls.Add(this.labelLFSettingsPath);
            this.LinkedFilesSettingsGroupBox.Location = new System.Drawing.Point(6, 7);
            this.LinkedFilesSettingsGroupBox.Name = "LinkedFilesSettingsGroupBox";
            this.LinkedFilesSettingsGroupBox.Size = new System.Drawing.Size(200, 130);
            this.LinkedFilesSettingsGroupBox.TabIndex = 2;
            this.LinkedFilesSettingsGroupBox.TabStop = false;
            this.LinkedFilesSettingsGroupBox.Text = "Settings";
            // 
            // textBoxPathToRepository
            // 
            this.textBoxPathToRepository.Location = new System.Drawing.Point(9, 41);
            this.textBoxPathToRepository.Name = "textBoxPathToRepository";
            this.textBoxPathToRepository.Size = new System.Drawing.Size(135, 20);
            this.textBoxPathToRepository.TabIndex = 1;
            this.textBoxPathToRepository.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // labelLFSettingsPath
            // 
            this.labelLFSettingsPath.AutoSize = true;
            this.labelLFSettingsPath.Location = new System.Drawing.Point(6, 23);
            this.labelLFSettingsPath.Name = "labelLFSettingsPath";
            this.labelLFSettingsPath.Size = new System.Drawing.Size(89, 13);
            this.labelLFSettingsPath.TabIndex = 0;
            this.labelLFSettingsPath.Text = "Path to repository";
            // 
            // linkedFilesListView
            // 
            listViewItem5.Checked = true;
            listViewItem5.StateImageIndex = 1;
            this.linkedFilesListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem5,
            listViewItem6,
            listViewItem7,
            listViewItem8});
            this.linkedFilesListView.Location = new System.Drawing.Point(212, 7);
            this.linkedFilesListView.Name = "linkedFilesListView";
            this.linkedFilesListView.Size = new System.Drawing.Size(642, 411);
            this.linkedFilesListView.TabIndex = 0;
            this.linkedFilesListView.UseCompatibleStateImageBehavior = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(857, 424);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnRepositoryPath
            // 
            this.btnRepositoryPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRepositoryPath.Location = new System.Drawing.Point(150, 39);
            this.btnRepositoryPath.Name = "btnRepositoryPath";
            this.btnRepositoryPath.Size = new System.Drawing.Size(43, 23);
            this.btnRepositoryPath.TabIndex = 2;
            this.btnRepositoryPath.Text = "...";
            this.btnRepositoryPath.UseVisualStyleBackColor = true;
            this.btnRepositoryPath.Click += new System.EventHandler(this.btnRepositoryPath_Click);
            // 
            // folderBrowserDialogRepositoryPath
            // 
            this.folderBrowserDialogRepositoryPath.SelectedPath = "C:\\";
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(865, 475);
            this.Controls.Add(this.FeaturesTab);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "GUI";
            this.Text = "RepositoryGroomer";
            this.Load += new System.EventHandler(this.GUI_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.FeaturesTab.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.LinkedFilesSettingsGroupBox.ResumeLayout(false);
            this.LinkedFilesSettingsGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveMenuItem;
        private System.Windows.Forms.TabControl FeaturesTab;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListView linkedFilesListView;
        private System.Windows.Forms.GroupBox LinkedFilesSettingsGroupBox;
        private System.Windows.Forms.TextBox textBoxPathToRepository;
        private System.Windows.Forms.Label labelLFSettingsPath;
        private System.Windows.Forms.Button btnRepositoryPath;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogRepositoryPath;
    }
}

