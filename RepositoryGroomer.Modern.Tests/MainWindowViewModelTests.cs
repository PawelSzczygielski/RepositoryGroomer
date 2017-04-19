using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using RepositoryGroomer.Core;

namespace RepositoryGroomer.Modern.Tests
{
    [TestFixture]
    public class MainWindowViewModelTests
    {
        private readonly IAmConfigurationProvider _configurationProvider;
        private readonly IAmProjectFileFinder _projectFileFinder;

        public MainWindowViewModelTests()
        {
            var configProviderMock = new Mock<IAmConfigurationProvider>();
            configProviderMock.Setup(x => x.SearchPath).Returns(() => "C:\\Repository");
            _configurationProvider = configProviderMock.Object;

            var projectFileFinderMock = new Mock<IAmProjectFileFinder>();
            projectFileFinderMock.Setup(x => x.GetAllProjects(It.IsAny<string>()))
                .Returns(() => new List<ProjectFileInfo>
                {
                    new ProjectFileInfo("ProjFilePath", "DirPath", "ProjectName", new List<LinkedFileInfo>
                    {
                        new LinkedFileInfo("RelativePath", LinkTagTypes.Compile, "UnwrappedPath", true)
                    }, true)
                });
            _projectFileFinder = projectFileFinderMock.Object;
        }

        [Test]
        public void On_Startup_SearchPathProvided_IsDisplayed()
        {
            var viewModel = new MainWindowViewModel(_configurationProvider, _projectFileFinder);
            Assert.That(viewModel.SearchPath, Is.EqualTo("C:\\Repository"));
        }

        [Test]
        public void On_Startup_ProjectsAreLoaded()
        {
            var viewModel = new MainWindowViewModel(_configurationProvider, _projectFileFinder);
            Assert.That(viewModel.TotalNumberOfProjects, Is.EqualTo(1));
            Assert.That(viewModel.TotalNumberOfProjectsWithLinkedFiles, Is.EqualTo(1));
        }

        [Test]
        public void If_ChangeRepoPathButton_Clicked_New_SearchPath_Obtained_And_Projects_Reloaded()
        {
            
        }
    }
}
