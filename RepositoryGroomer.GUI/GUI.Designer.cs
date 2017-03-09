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
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FeaturesTab = new System.Windows.Forms.TabControl();
            this.linkedFilesTab = new System.Windows.Forms.TabPage();
            this.linkedFilesTreeView = new System.Windows.Forms.TreeView();
            this.LinkedFilesSettingsGroupBox = new System.Windows.Forms.GroupBox();
            this.searchLFbutton = new System.Windows.Forms.Button();
            this.fileFormatsGB = new System.Windows.Forms.GroupBox();
            this.fileTypesList = new System.Windows.Forms.CheckedListBox();
            this.pathToRepositoryGB = new System.Windows.Forms.GroupBox();
            this.textBoxPathToRepository = new System.Windows.Forms.TextBox();
            this.btnRepositoryPath = new System.Windows.Forms.Button();
            this.repositoryGroomerTab = new System.Windows.Forms.TabPage();
            this.folderBrowserDialogRepositoryPath = new System.Windows.Forms.FolderBrowserDialog();
            this.mainMenu.SuspendLayout();
            this.FeaturesTab.SuspendLayout();
            this.linkedFilesTab.SuspendLayout();
            this.LinkedFilesSettingsGroupBox.SuspendLayout();
            this.fileFormatsGB.SuspendLayout();
            this.pathToRepositoryGB.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuItem,
            this.settingsMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.mainMenu.Size = new System.Drawing.Size(1153, 24);
            this.mainMenu.TabIndex = 0;
            this.mainMenu.Text = "mainMenu";
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
            this.FeaturesTab.Controls.Add(this.linkedFilesTab);
            this.FeaturesTab.Controls.Add(this.repositoryGroomerTab);
            this.FeaturesTab.Location = new System.Drawing.Point(0, 33);
            this.FeaturesTab.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FeaturesTab.Name = "FeaturesTab";
            this.FeaturesTab.SelectedIndex = 0;
            this.FeaturesTab.Size = new System.Drawing.Size(1153, 554);
            this.FeaturesTab.TabIndex = 1;
            // 
            // linkedFilesTab
            // 
            this.linkedFilesTab.Controls.Add(this.linkedFilesTreeView);
            this.linkedFilesTab.Controls.Add(this.LinkedFilesSettingsGroupBox);
            this.linkedFilesTab.Location = new System.Drawing.Point(4, 25);
            this.linkedFilesTab.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.linkedFilesTab.Name = "linkedFilesTab";
            this.linkedFilesTab.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.linkedFilesTab.Size = new System.Drawing.Size(1145, 525);
            this.linkedFilesTab.TabIndex = 0;
            this.linkedFilesTab.Text = "Linked Files";
            this.linkedFilesTab.UseVisualStyleBackColor = true;
            // 
            // linkedFilesTreeView
            // 
            this.linkedFilesTreeView.Location = new System.Drawing.Point(284, 9);
            this.linkedFilesTreeView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.linkedFilesTreeView.Name = "linkedFilesTreeView";
            this.linkedFilesTreeView.Size = new System.Drawing.Size(847, 505);
            this.linkedFilesTreeView.TabIndex = 3;
            // 
            // LinkedFilesSettingsGroupBox
            // 
            this.LinkedFilesSettingsGroupBox.Controls.Add(this.searchLFbutton);
            this.LinkedFilesSettingsGroupBox.Controls.Add(this.fileFormatsGB);
            this.LinkedFilesSettingsGroupBox.Controls.Add(this.pathToRepositoryGB);
            this.LinkedFilesSettingsGroupBox.Location = new System.Drawing.Point(8, 4);
            this.LinkedFilesSettingsGroupBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.LinkedFilesSettingsGroupBox.Name = "LinkedFilesSettingsGroupBox";
            this.LinkedFilesSettingsGroupBox.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.LinkedFilesSettingsGroupBox.Size = new System.Drawing.Size(267, 511);
            this.LinkedFilesSettingsGroupBox.TabIndex = 2;
            this.LinkedFilesSettingsGroupBox.TabStop = false;
            this.LinkedFilesSettingsGroupBox.Text = "Settings";
            // 
            // searchLFbutton
            // 
            this.searchLFbutton.Location = new System.Drawing.Point(17, 181);
            this.searchLFbutton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.searchLFbutton.Name = "searchLFbutton";
            this.searchLFbutton.Size = new System.Drawing.Size(232, 28);
            this.searchLFbutton.TabIndex = 6;
            this.searchLFbutton.Text = "Search";
            this.searchLFbutton.UseVisualStyleBackColor = true;
            this.searchLFbutton.Click += new System.EventHandler(this.searchLFbutton_Click);
            // 
            // fileFormatsGB
            // 
            this.fileFormatsGB.Controls.Add(this.fileTypesList);
            this.fileFormatsGB.Location = new System.Drawing.Point(9, 98);
            this.fileFormatsGB.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.fileFormatsGB.Name = "fileFormatsGB";
            this.fileFormatsGB.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.fileFormatsGB.Size = new System.Drawing.Size(248, 74);
            this.fileFormatsGB.TabIndex = 5;
            this.fileFormatsGB.TabStop = false;
            this.fileFormatsGB.Text = "File formats";
            // 
            // fileTypesList
            // 
            this.fileTypesList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.fileTypesList.ColumnWidth = 40;
            this.fileTypesList.ForeColor = System.Drawing.SystemColors.WindowText;
            this.fileTypesList.FormattingEnabled = true;
            this.fileTypesList.Items.AddRange(new object[] {
            ".cs",
            ".snk",
            ".dll",
            ".???"});
            this.fileTypesList.Location = new System.Drawing.Point(8, 23);
            this.fileTypesList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.fileTypesList.MultiColumn = true;
            this.fileTypesList.Name = "fileTypesList";
            this.fileTypesList.Size = new System.Drawing.Size(232, 34);
            this.fileTypesList.TabIndex = 3;
            this.fileTypesList.Tag = "";
            // 
            // pathToRepositoryGB
            // 
            this.pathToRepositoryGB.Controls.Add(this.textBoxPathToRepository);
            this.pathToRepositoryGB.Controls.Add(this.btnRepositoryPath);
            this.pathToRepositoryGB.Location = new System.Drawing.Point(8, 23);
            this.pathToRepositoryGB.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pathToRepositoryGB.Name = "pathToRepositoryGB";
            this.pathToRepositoryGB.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pathToRepositoryGB.Size = new System.Drawing.Size(249, 66);
            this.pathToRepositoryGB.TabIndex = 4;
            this.pathToRepositoryGB.TabStop = false;
            this.pathToRepositoryGB.Text = "Path to repository";
            // 
            // textBoxPathToRepository
            // 
            this.textBoxPathToRepository.Location = new System.Drawing.Point(8, 23);
            this.textBoxPathToRepository.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxPathToRepository.Name = "textBoxPathToRepository";
            this.textBoxPathToRepository.Size = new System.Drawing.Size(179, 22);
            this.textBoxPathToRepository.TabIndex = 1;
            this.textBoxPathToRepository.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // btnRepositoryPath
            // 
            this.btnRepositoryPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRepositoryPath.Location = new System.Drawing.Point(192, 23);
            this.btnRepositoryPath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRepositoryPath.Name = "btnRepositoryPath";
            this.btnRepositoryPath.Size = new System.Drawing.Size(49, 25);
            this.btnRepositoryPath.TabIndex = 2;
            this.btnRepositoryPath.Text = "...";
            this.btnRepositoryPath.UseVisualStyleBackColor = true;
            this.btnRepositoryPath.Click += new System.EventHandler(this.btnRepositoryPath_Click);
            // 
            // repositoryGroomerTab
            // 
            this.repositoryGroomerTab.Location = new System.Drawing.Point(4, 25);
            this.repositoryGroomerTab.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.repositoryGroomerTab.Name = "repositoryGroomerTab";
            this.repositoryGroomerTab.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.repositoryGroomerTab.Size = new System.Drawing.Size(1145, 525);
            this.repositoryGroomerTab.TabIndex = 1;
            this.repositoryGroomerTab.Text = "Repository Groomer";
            this.repositoryGroomerTab.UseVisualStyleBackColor = true;
            // 
            // folderBrowserDialogRepositoryPath
            // 
            this.folderBrowserDialogRepositoryPath.SelectedPath = "C:\\";
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1153, 585);
            this.Controls.Add(this.FeaturesTab);
            this.Controls.Add(this.mainMenu);
            this.MainMenuStrip = this.mainMenu;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "GUI";
            this.Text = "RepositoryGroomer";
            this.Load += new System.EventHandler(this.GUI_Load);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.FeaturesTab.ResumeLayout(false);
            this.linkedFilesTab.ResumeLayout(false);
            this.LinkedFilesSettingsGroupBox.ResumeLayout(false);
            this.fileFormatsGB.ResumeLayout(false);
            this.pathToRepositoryGB.ResumeLayout(false);
            this.pathToRepositoryGB.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveMenuItem;
        private System.Windows.Forms.TabControl FeaturesTab;
        private System.Windows.Forms.TabPage linkedFilesTab;
        private System.Windows.Forms.TabPage repositoryGroomerTab;
        private System.Windows.Forms.GroupBox LinkedFilesSettingsGroupBox;
        private System.Windows.Forms.TextBox textBoxPathToRepository;
        private System.Windows.Forms.Button btnRepositoryPath;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogRepositoryPath;
        private System.Windows.Forms.GroupBox pathToRepositoryGB;
        private System.Windows.Forms.CheckedListBox fileTypesList;
        private System.Windows.Forms.GroupBox fileFormatsGB;
        private System.Windows.Forms.TreeView linkedFilesTreeView;
        private System.Windows.Forms.Button searchLFbutton;
    }
}

