using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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

        }
    }
}
