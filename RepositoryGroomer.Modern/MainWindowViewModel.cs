using System.Collections.ObjectModel;
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
        private readonly IAmConfigurationProvider _configurationProvider;
        private readonly IAmProjectFileFinder _projectFileFinder;

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

        public MainWindowViewModel(IAmConfigurationProvider configurationProvider, IAmProjectFileFinder projectFileFinder)
        {
            _configurationProvider = configurationProvider;
            _projectFileFinder = projectFileFinder;

            SearchPath = _configurationProvider.SearchPath;
            ReloadProjects();
        }

        private void ReloadProjects()
        {
            Projects = new ObservableCollection<ProjectFileInfo>(_projectFileFinder.GetAllProjects(SearchPath));
            TotalNumberOfProjects = Projects.Count;
            TotalNumberOfProjectsWithLinkedFiles = Projects.Count(proj=>proj.IsProjectFileWithLinks);
        }

        [CaliburnMicroActionTarget]
        public void SearchPathChanged()
        {
            ReloadProjects();
        }
    }
}
