using System.Collections.ObjectModel;
using System.Linq;
using RepositoryGroomer.Core;

namespace RepositoryGroomer.Modern
{
    public class MainWindowViewModel
    {
        public ObservableCollection<ProjectFileInfo> Projects { get; set; }

        public MainWindowViewModel()
        {
            var projectFileFinder = new ProjectFileFinder();
            var foundProjects = projectFileFinder.GetAllProjects(@"C:\");
            Projects = new ObservableCollection<ProjectFileInfo>(foundProjects.Where(x=>x.Links.Any()));
        }
    }
}
