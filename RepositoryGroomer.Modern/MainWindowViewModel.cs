using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using RepositoryGroomer.Core;
using RepositoryGroomer.Modern.Annotations;

namespace RepositoryGroomer.Modern
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string _searchPath;
        private int _totalNumberOfProjects;
        private ObservableCollection<ProjectFileInfo> _projects;

        public ObservableCollection<ProjectFileInfo> Projects
        {
            get { return _projects; }
            set
            {
                if (Equals(value, _projects)) return;
                _projects = value;
                OnPropertyChanged();
            }
        }

        public int TotalNumberOfProjects
        {
            get { return _totalNumberOfProjects; }
            set
            {
                if (value == _totalNumberOfProjects) return;
                _totalNumberOfProjects = value;
                OnPropertyChanged();
            }
        }

        public string SearchPath
        {
            get { return _searchPath; }
            set
            {
                if (value == _searchPath) return;
                _searchPath = value;
                OnPropertyChanged();
            }
        }

        public ConcreteCommand ChangeSearchPathCommand { get; set; }

        public MainWindowViewModel()
        {
            ChangeSearchPathCommand = new ConcreteCommand(ShowFolderDialogAndReloadProjects);

            SearchPath = ConfigurationManager.AppSettings.Get(nameof(SearchPath));
            if (Directory.Exists(SearchPath))
                ReloadProjects();
            else
            {
                //TODO: then what?
            }
        }

        private void ShowFolderDialogAndReloadProjects()
        {
            
        }


        private void ReloadProjects()
        {
            var projectFileFinder = new ProjectFileFinder();
            var foundProjects = projectFileFinder.GetAllProjects(SearchPath);
            Projects = new ObservableCollection<ProjectFileInfo>(foundProjects.Where(x => x.Links.Any()));
            TotalNumberOfProjects = foundProjects.Count;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
