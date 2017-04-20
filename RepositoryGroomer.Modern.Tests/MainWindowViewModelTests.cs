using System.Collections.Generic;
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

        public MainWindowViewModelTests()
        {
            var configProviderMock = new Mock<IAmConfigurationProvider>();
            configProviderMock.Setup(x => x.SearchPath).Returns(() => "C:\\Repository");
            _configurationProvider = configProviderMock;

            var projectFileFinderMock = new Mock<IAmProjectFileFinder>();
            projectFileFinderMock.Setup(x => x.GetAllProjects("C:\\Repository"))
                .Returns(() => new List<ProjectFileInfo>
                {
                    new ProjectFileInfo("ProjFilePath1", "DirPath1", "ProjectName1", new List<LinkedFileInfo>(),
                    true),
                    new ProjectFileInfo("ProjFilePath2", "DirPath2", "ProjectName2", new List<LinkedFileInfo>
                    {
                        new LinkedFileInfo("RelativePath2", LinkTagTypes.Compile, "UnwrappedPath2", true)
                    }, true),

                });
            projectFileFinderMock.Setup(x => x.GetAllProjects("C:\\OtherRepository")).Returns(() => new List<ProjectFileInfo>
            {
                new ProjectFileInfo("ProjFilePath3", "DirPath3", "ProjectName3", new List<LinkedFileInfo>(),
                    true),
            });

            _projectFileFinder = projectFileFinderMock;
        }

        [Test]
        public void On_Startup_SearchPathProvided_IsDisplayed()
        {
            var viewModel = new MainWindowViewModel(_configurationProvider.Object, _projectFileFinder.Object);
            Assert.That(viewModel.SearchPath, Is.EqualTo("C:\\Repository"));
        }

        [Test]
        public void On_Startup_ProjectsAreLoaded()
        {
            var viewModel = new MainWindowViewModel(_configurationProvider.Object, _projectFileFinder.Object);
            Assert.That(viewModel.TotalNumberOfProjects, Is.EqualTo(2));
            Assert.That(viewModel.TotalNumberOfProjectsWithLinkedFiles, Is.EqualTo(1));
        }

        [Test]
        public void If_ChangeRepoPathButton_Clicked_And_SearchPath_Not_Changed_Projects_Reloaded()
        {
            var viewModel = new MainWindowViewModel(_configurationProvider.Object, _projectFileFinder.Object);
            viewModel.SearchPathChanged();

            Assert.That(viewModel.TotalNumberOfProjects, Is.EqualTo(2));
        }

        [Test]
        public void If_ChangeRepoPathButton_Clicked_New_SearchPath_Obtained_And_Projects_Reloaded()
        {
            var viewModel = new MainWindowViewModel(_configurationProvider.Object, _projectFileFinder.Object);
            viewModel.SearchPath = "C:\\OtherRepository";
            viewModel.SearchPathChanged();

            Assert.That(viewModel.TotalNumberOfProjects, Is.EqualTo(1));
        }
    }
}
