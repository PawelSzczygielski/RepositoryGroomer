using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using RepositoryGroomer.Core;
using RepositoryGroomer.GUI.Properties;

namespace RepositoryGroomer.GUI
{
    public partial class GUI : Form
    {
        public GUI()
        {
            InitializeComponent();
            // Load settings
            
            textBoxPathToRepository.Text = Settings.Default.PathToRepository;
        }

        private void GUI_Load(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnRepositoryPath_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialogRepositoryPath.ShowDialog();
        }

        private void searchLFbutton_Click(object sender, EventArgs e)
        {
            var projectFileFinder = new ProjectFileFinder();
            var foundProjects = projectFileFinder.GetAllProjects(textBoxPathToRepository.Text);
            var linkedProjects = new ObservableCollection<ProjectFileInfo>(foundProjects.Where(x => x.Links.Any()));

            linkedFilesTreeView.Nodes.AddRange(linkedProjects.Select(p=>
            {
                var node = new TreeNode(p.ProjectName);
                node.Nodes.AddRange(p.Links.Select(l=>new TreeNode(l.LinkedFileRelativePath)).ToArray());
                return node;
            }).ToArray());
        }
    }
}
