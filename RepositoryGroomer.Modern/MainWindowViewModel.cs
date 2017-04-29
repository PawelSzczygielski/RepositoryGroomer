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
        private bool _showOnlyLinkedProjects;
        private readonly IAmConfigurationProvider _configurationProvider;
        private readonly IAmProjectFileFinder _projectFileFinder;
        private int _totalNumberOfProjectsWithInvalidReferences;
        private string _projectXmlContain;
        private ProjectFileInfo _selectedProject;
        private readonly IAmFileReader _fileReader;

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

        public bool ShowOnlyLinkedProjects
        {
            get { return _showOnlyLinkedProjects; }
            set
            {
                if (value == _showOnlyLinkedProjects)
                    return;

                _showOnlyLinkedProjects = value;
                NotifyOfPropertyChange(() => ShowOnlyLinkedProjects);
                ChangeProjectFilter();
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

        public int TotalNumberOfProjectsWithInvalidReferences
        {
            get { return _totalNumberOfProjectsWithInvalidReferences; }
            set
            {
                if (value == _totalNumberOfProjectsWithInvalidReferences)
                    return;

                _totalNumberOfProjectsWithInvalidReferences = value;
                NotifyOfPropertyChange(() => TotalNumberOfProjectsWithInvalidReferences);
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

        public MainWindowViewModel(IAmConfigurationProvider configurationProvider, IAmProjectFileFinder projectFileFinder, IAmFileReader fileReader)
        {
            _configurationProvider = configurationProvider;
            _projectFileFinder = projectFileFinder;
            _fileReader = fileReader;

            SearchPath = _configurationProvider.SearchPath;
            ReloadProjects();
            ShowOnlyLinkedProjects = true;
        }

        private void ReloadProjects()
        {
            var projects = _projectFileFinder.GetAllProjects(SearchPath);
            TotalNumberOfProjects = projects.Count;
            TotalNumberOfProjectsWithLinkedFiles = projects.Count(proj => proj.IsProjectFileWithLinks);
            TotalNumberOfProjectsWithInvalidReferences = projects.Count(proj => proj.ProjectFileContainsInvalidReferences);

            Projects = CollectionViewSource.GetDefaultView(projects);
            SelectedProject = projects.First();
        }

        [CaliburnMicroActionTarget]
        public void SearchPathChanged()
        {
            ReloadProjects();
        }

        private void FilterProjectsByLinks()
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

        private void DontFilterProjects()
        {
            Projects.Filter = null;
        }

        private void ChangeProjectFilter()
        {
            if (ShowOnlyLinkedProjects)
                FilterProjectsByLinks();
            else
                DontFilterProjects();
        }

        public ProjectFileInfo SelectedProject
        {
            get { return _selectedProject; }
            set
            {
                if (value == _selectedProject)
                    return;

                _selectedProject = value;
                NotifyOfPropertyChange(()=>SelectedProject);
                ProjectXmlContain = _fileReader.ReadFromFile(_selectedProject.ProjectFilePath);
            }
        }

        public string ProjectXmlContain
        {
            get { return _projectXmlContain; }
            set
            {
                if (value == _projectXmlContain)
                    return;

                _projectXmlContain = value;
                NotifyOfPropertyChange(() => ProjectXmlContain);
            }
        }
    }
}
