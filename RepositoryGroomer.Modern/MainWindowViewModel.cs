using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Linq;
using RepositoryGroomer.Core;
using Caliburn.Micro;

namespace RepositoryGroomer.Modern
{
    public class MainWindowViewModel : PropertyChangedBase
    {
        private string _searchPath;
        private int _totalNumberOfProjects;
        private int _totalNumberOfProjectsWithLinkedFiles;
        private ObservableCollection<ProjectFileInfo> _projects;

        public ObservableCollection<ProjectFileInfo> Projects
        {
            get { return _projects; }
            set
            {
                if (Equals(value, _projects)) return;
                _projects = value;
                NotifyOfPropertyChange(() => Projects);
            }
        }

        public int TotalNumberOfProjects
        {
            get { return _totalNumberOfProjects; }
            set
            {
                if (value == _totalNumberOfProjects) return;
                _totalNumberOfProjects = value;
                NotifyOfPropertyChange(() => TotalNumberOfProjects);
            }
        }

        public int TotalNumberOfProjectsWithLinkedFiles
        {
            get { return _totalNumberOfProjectsWithLinkedFiles; }
            set
            {
                if (value == _totalNumberOfProjectsWithLinkedFiles) return;
                _totalNumberOfProjectsWithLinkedFiles = value;
                NotifyOfPropertyChange(() => TotalNumberOfProjectsWithLinkedFiles);
            }
        }

        public string SearchPath
        {
            get { return _searchPath; }
            set
            {
                if (value == _searchPath) return;
                _searchPath = value;
                NotifyOfPropertyChange(() => SearchPath);
            }
        }

        public MainWindowViewModel()
        {
            SearchPath = ConfigurationManager.AppSettings.Get(nameof(SearchPath));
            if (Directory.Exists(SearchPath))
                ReloadProjects();
            else
            {
                //TODO: then what?
            }
        }

        [CaliburnMicroActionTarget]
        public void SearchPathChanged()
        {
           
        }

        private void ReloadProjects()
        {
            var projectFileFinder = new ProjectFileFinder();
            var foundProjects = projectFileFinder.GetAllProjects(SearchPath);
            Projects = new ObservableCollection<ProjectFileInfo>(foundProjects.Where(x => x.Links.Any()));
            TotalNumberOfProjects = foundProjects.Count;
            TotalNumberOfProjectsWithLinkedFiles = Projects.Count;
        }
    }
}
