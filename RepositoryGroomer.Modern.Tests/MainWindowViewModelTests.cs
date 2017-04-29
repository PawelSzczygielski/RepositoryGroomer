using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using RepositoryGroomer.Core;

namespace RepositoryGroomer.Modern.Tests
{
    [TestFixture]
    public class MainWindowViewModelTests
    {
        private readonly Mock<IAmConfigurationProvider> _configurationProvider;
        private readonly Mock<IAmProjectFileFinder> _projectFileFinder;
        private readonly Mock<IAmFileReader> _fileReader;

        public MainWindowViewModelTests()
        {
            _configurationProvider = new Mock<IAmConfigurationProvider>();
            _configurationProvider.Setup(x => x.SearchPath).Returns(() => "C:\\Repository");

            _projectFileFinder = new Mock<IAmProjectFileFinder>();
            _projectFileFinder.Setup(x => x.GetAllProjects("C:\\Repository"))
                .Returns(() => new List<ProjectFileInfo>
                {
                    new ProjectFileInfo("ProjFilePath1", "DirPath1", "ProjectName1", new List<LinkedFileInfo>(), 
                    new List<Reference> {new Reference(false, "Include", "HintPath", "UnwrappedHintPath")}, 
                    true),
                    new ProjectFileInfo("ProjFilePath2", "DirPath2", "ProjectName2", new List<LinkedFileInfo>
                    {
                        new LinkedFileInfo("RelativePath2", LinkTagTypes.Compile, "UnwrappedPath2", true)
                    }, new List<Reference> {new Reference(true, "Include", "HintPath", string.Empty)}, true),

                });
            _projectFileFinder.Setup(x => x.GetAllProjects("C:\\OtherRepository")).Returns(() => new List<ProjectFileInfo>
            {
                new ProjectFileInfo("ProjFilePath3", "DirPath3", "ProjectName3", new List<LinkedFileInfo>(), 
                new List<Reference>(), 
                    true),
            });

            _fileReader = new Mock<IAmFileReader>();
            _fileReader.Setup(x => x.ReadFromFile("ProjFilePath1")).Returns("ProjectXmlContain1");
            _fileReader.Setup(x => x.ReadFromFile("ProjFilePath2")).Returns("ProjectXmlContain2");
        }

        [Test]
        public void On_Startup_SearchPathProvided_IsDisplayed()
        {
            var viewModel = new MainWindowViewModel(_configurationProvider.Object, _projectFileFinder.Object, _fileReader.Object);
            Assert.That(viewModel.SearchPath, Is.EqualTo("C:\\Repository"));
        }

        [Test]
        public void On_Startup_ProjectsAreLoaded()
        {
            var viewModel = new MainWindowViewModel(_configurationProvider.Object, _projectFileFinder.Object, _fileReader.Object);
            Assert.That(viewModel.TotalNumberOfProjects, Is.EqualTo(2));
            Assert.That(viewModel.TotalNumberOfProjectsWithLinkedFiles, Is.EqualTo(1));
        }

        [Test]
        public void If_ChangeRepoPathButton_Clicked_And_SearchPath_Not_Changed_Projects_Reloaded()
        {
            var viewModel = new MainWindowViewModel(_configurationProvider.Object, _projectFileFinder.Object, _fileReader.Object);
            viewModel.SearchPathChanged();

            Assert.That(viewModel.TotalNumberOfProjects, Is.EqualTo(2));
        }

        [Test]
        public void If_ChangeRepoPathButton_Clicked_New_SearchPath_Obtained_And_Projects_Reloaded()
        {
            var viewModel = new MainWindowViewModel(_configurationProvider.Object, _projectFileFinder.Object, _fileReader.Object);
            viewModel.SearchPath = "C:\\OtherRepository";
            viewModel.SearchPathChanged();

            Assert.That(viewModel.TotalNumberOfProjects, Is.EqualTo(1));
        }

        [Test]
        public void FilterCheckbox_Changes_Filtering_Of_ProjectsCollection()
        {
            var viewModel= new MainWindowViewModel(_configurationProvider.Object, _projectFileFinder.Object, _fileReader.Object);
            Assert.That(viewModel.Projects.OfType<ProjectFileInfo>().Count(), Is.EqualTo(1));

            viewModel.ShowOnlyLinkedProjects = false;
            Assert.That(viewModel.Projects.OfType<ProjectFileInfo>().Count(), Is.EqualTo(2));
        }

        [Test]
        public void TotalNumberOfProjectsWithInvalidReferences_Is_Filled_Correctly()
        {
            var viewModel = new MainWindowViewModel(_configurationProvider.Object, _projectFileFinder.Object, _fileReader.Object);
            Assert.That(viewModel.TotalNumberOfProjectsWithInvalidReferences, Is.EqualTo(1));
        }

        [Test]
        public void Displayed_Xml_Changes_When_Selected_Project_Is_Being_Changed()
        {
            var viewModel = new MainWindowViewModel(_configurationProvider.Object, _projectFileFinder.Object, _fileReader.Object);
            Assert.That(viewModel.ProjectXmlContain, Is.EqualTo("ProjectXmlContain1"));
            viewModel.Projects.MoveCurrentToLast();
            viewModel.SelectedProject = (ProjectFileInfo)viewModel.Projects.CurrentItem;
            Assert.That(viewModel.ProjectXmlContain, Is.EqualTo("ProjectXmlContain2"));
        }
    }
}
