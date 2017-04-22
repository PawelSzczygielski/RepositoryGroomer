using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using RepositoryGroomer.Core;
using Caliburn.Micro;

namespace RepositoryGroomer.Modern
{
    public class MainWindowViewModel : PropertyChangedBase
    {
        private string _searchPath;
        private int _totalNumberOfProjects;
        private int _totalNumberOfProjectsWithLinkedFiles;
        private readonly IAmConfigurationProvider _configurationProvider;
        private readonly IAmProjectFileFinder _projectFileFinder;

        public ICollectionView Projects { get; set; }

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
            var projects = _projectFileFinder.GetAllProjects(SearchPath);
            TotalNumberOfProjects = projects.Count;
            TotalNumberOfProjectsWithLinkedFiles = projects.Count(proj => proj.IsProjectFileWithLinks);

            Projects = CollectionViewSource.GetDefaultView(projects);
        }

        [CaliburnMicroActionTarget]
        public void SearchPathChanged()
        {
            ReloadProjects();
        }

        [CaliburnMicroActionTarget]
        public void FilterProjectsByLinks()
        {
            var predicate = new Predicate<object>(bj =>
            {
                var projFileInfo = bj as ProjectFileInfo;
                if (projFileInfo == null)
                    return false;
                return projFileInfo.IsProjectFileWithLinks;
            });
            Projects.Filter = predicate;
        }

        [CaliburnMicroActionTarget]
        public void DisableProjectsFiltering()
        {
            Projects.Filter = null;
        }
    }
}
