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
        private Mock<IAmConfigurationProvider> _configurationProvider;
        private Mock<IAmProjectFileFinder> _projectFileFinder;
        private Mock<IAmFileReader> _fileReader;

        [SetUp]
        public void Setup()
        {
            _configurationProvider = new Mock<IAmConfigurationProvider>();
            _configurationProvider.Setup(x => x.SearchPath).Returns(() => "C:\\Repository");

            _projectFileFinder = new Mock<IAmProjectFileFinder>();
            _projectFileFinder.Setup(x => x.GetAllProjects("C:\\Repository"))
                .Returns(() => new List<ProjectFileInfo>
                {
                    new ProjectFileInfo("ProjectWithBrokenReference", "DirPath1", "ProjectName1", new List<LinkedFileInfo>(),
                    new List<Reference> {new Reference(false, "Include", "HintPath", "UnwrappedHintPath")},
                    true),
                    new ProjectFileInfo("CorrectProject", "DirPath2", "ProjectName2", new List<LinkedFileInfo>(),
                    new List<Reference> {new Reference(true, "Include", "HintPath", string.Empty)}, true),

                });
            _projectFileFinder.Setup(x => x.GetAllProjects("C:\\OtherRepository")).Returns(() => new List<ProjectFileInfo>
            {
                new ProjectFileInfo("ProjFilePath3", "DirPath3", "ProjectName3", new List<LinkedFileInfo>(),
                new List<Reference>(),
                    true),
            });

            _fileReader = new Mock<IAmFileReader>();
            _fileReader.Setup(x => x.ReadFromFile("ProjectWithBrokenReference")).Returns("ProjectXmlContain1");
            _fileReader.Setup(x => x.ReadFromFile("CorrectProject")).Returns("ProjectXmlContain2");
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
            Assert.That(viewModel.TotalNumberOfProjectsWithInvalidReferences, Is.EqualTo(1));
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
            var viewModel = new MainWindowViewModel(_configurationProvider.Object, _projectFileFinder.Object, _fileReader.Object);
            Assert.That(viewModel.Projects.OfType<ProjectFileInfo>().Count(), Is.EqualTo(1));

            viewModel.ShowOnlyLinkedProjects = false;
            viewModel.ShowOnlyInvalidlyReferencedProjects = false;

            Assert.That(viewModel.Projects.OfType<ProjectFileInfo>().Count(), Is.EqualTo(2));
        }

        [Test]
        public void TotalNumberOfProjectsWithInvalidReferences_Is_Filled_Correctly()
        {
            var viewModel = new MainWindowViewModel(_configurationProvider.Object, _projectFileFinder.Object, _fileReader.Object);
            Assert.That(viewModel.TotalNumberOfProjectsWithInvalidReferences, Is.EqualTo(1));
        }

        [Test]
        public void Displayed_Xml_Changes_When_Selected_Project_Is_Changed()
        {

            var viewModel = new MainWindowViewModel(_configurationProvider.Object, _projectFileFinder.Object, _fileReader.Object);
            Assert.That(viewModel.ProjectXmlContain, Is.EqualTo("ProjectXmlContain1"));
            viewModel.ShowOnlyLinkedProjects = false;
            viewModel.ShowOnlyInvalidlyReferencedProjects = false;
            viewModel.Projects.MoveCurrentToLast();
            viewModel.SelectedProject = (ProjectFileInfo)viewModel.Projects.CurrentItem;
            Assert.That(viewModel.ProjectXmlContain, Is.EqualTo("ProjectXmlContain2"));
        }

        [Test]
        public void Project_Initial_Filtering_Works()
        {
            _projectFileFinder = new Mock<IAmProjectFileFinder>();
            _projectFileFinder.Setup(x => x.GetAllProjects(It.IsAny<string>())).Returns(() =>
            {
                return new List<ProjectFileInfo>().AddProjectsWithBrokenReference()
                    .AddProjectsWithBrokenLinks()
                    .AddCorrectProjects();
            });

            var viewModel = new MainWindowViewModel(_configurationProvider.Object, _projectFileFinder.Object, _fileReader.Object);

            Assert.That(viewModel.Projects.Cast<ProjectFileInfo>().Count(), Is.EqualTo(2));
        }

        [Test]
        public void Repo_Reloaded_Filtering_Stays_The_Same()
        {
            _projectFileFinder.Setup(x => x.GetAllProjects("C:\\Repository1")).Returns(() =>
            {
                return new List<ProjectFileInfo>()
                    .AddProjectsWithBrokenReference(5)
                    .AddProjectsWithBrokenLinks(4)
                    .AddCorrectProjects(3);
            });
            _projectFileFinder.Setup(x => x.GetAllProjects("C:\\Repository2")).Returns(() =>
            {
                return new List<ProjectFileInfo>()
                .AddProjectsWithBrokenReference(10)
                .AddProjectsWithBrokenLinks(9)
                .AddCorrectProjects(8);
            });


            _configurationProvider = new Mock<IAmConfigurationProvider>();
            _configurationProvider.Setup(x => x.SearchPath).Returns("C:\\Repository1");
            var viewModel = new MainWindowViewModel(_configurationProvider.Object, _projectFileFinder.Object, _fileReader.Object);

            Assert.That(viewModel.Projects.OfType<ProjectFileInfo>().Count(x => x.ProjectFileContainsInvalidReferences), Is.EqualTo(5));
            Assert.That(viewModel.Projects.OfType<ProjectFileInfo>().Count(x => x.ProjectFileContainsLinksToFiles), Is.EqualTo(4));
            Assert.That(viewModel.Projects.OfType<ProjectFileInfo>().Count(), Is.EqualTo(9));

            viewModel.SearchPath = "C:\\Repository2";
            viewModel.SearchPathChanged();

            Assert.That(viewModel.Projects.OfType<ProjectFileInfo>().Count(x => x.ProjectFileContainsInvalidReferences), Is.EqualTo(10));
            Assert.That(viewModel.Projects.OfType<ProjectFileInfo>().Count(x => x.ProjectFileContainsLinksToFiles), Is.EqualTo(9));
            Assert.That(viewModel.Projects.OfType<ProjectFileInfo>().Count(), Is.EqualTo(19));
        }
    }
}
