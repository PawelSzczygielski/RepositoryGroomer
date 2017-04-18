using NUnit.Framework;
using RepositoryGroomer.Core.Tests.Properties;

namespace RepositoryGroomer.Core.Tests
{
    [TestFixture]
    public class ProjectFileInfoBuilderTests
    {
        [TestCase("projFilePath1", "containingFolder", "")]
        [TestCase("projFilePath1", "", "projName")]
        [TestCase("", "containingFolder", "projName")]
        public void Builder_Builds_Correctly_If_Initial_Data_Missing(string projectFilePath,
            string containingDirectoryPath, string projectName)
        {
            var projectFileXmlContain = Resources.ProjectXmlWithNonExistingLinkedFiles;
            var projectFileInfo = ProjectFileInfoBuilder.Build(projectFilePath, containingDirectoryPath, projectName, projectFileXmlContain);

            Assert.That(projectFileInfo.ProjectFilePath, Is.EqualTo(projectFilePath));
            Assert.That(projectFileInfo.ContainingDirectoryPath, Is.EqualTo(containingDirectoryPath));
            Assert.That(projectFileInfo.ProjectName, Is.EqualTo(projectName));
            Assert.That(projectFileInfo.IsProjectFileXmlCorrect, Is.False);
        }

        [Test]
        public void Builder_Builds_Correctly_If_ProjectFileXml_Empty()
        {
            const string projectFilePath = "projectFilePath";
            const string containingDirectoryPath = "containingDirectoryPath";
            const string projectName = "projectName";
            const string projectFileXmlContain = "";

            var projectFileInfo = ProjectFileInfoBuilder.Build(projectFilePath, containingDirectoryPath, projectName, projectFileXmlContain);

            Assert.That(projectFileInfo.ProjectFilePath, Is.EqualTo(projectFilePath));
            Assert.That(projectFileInfo.ContainingDirectoryPath, Is.EqualTo(containingDirectoryPath));
            Assert.That(projectFileInfo.ProjectName, Is.EqualTo(projectName));
            Assert.That(projectFileInfo.Links, Is.Empty);
            Assert.That(projectFileInfo.IsProjectFileWithLinks, Is.False);
            Assert.That(projectFileInfo.IsProjectFileXmlCorrect, Is.False);
        }

        [Test]
        public void Builder_Builds_Correctly_If_ProjectFileXml_Corrupted()
        {
            const string projectFilePath = "projectFilePath";
            const string containingDirectoryPath = "containingDirectoryPath";
            const string projectName = "projectName";
            var projectFileXmlContain = Resources.ProjectXmlCorrupted;

            var projectFileInfo = ProjectFileInfoBuilder.Build(projectFilePath, containingDirectoryPath, projectName, projectFileXmlContain);

            Assert.That(projectFileInfo.ProjectFilePath, Is.EqualTo(projectFilePath));
            Assert.That(projectFileInfo.ContainingDirectoryPath, Is.EqualTo(containingDirectoryPath));
            Assert.That(projectFileInfo.ProjectName, Is.EqualTo(projectName));
            Assert.That(projectFileInfo.Links, Is.Empty);
            Assert.That(projectFileInfo.IsProjectFileWithLinks, Is.False);
            Assert.That(projectFileInfo.IsProjectFileXmlCorrect, Is.False);
        }

        [Test]
        public void Builder_Builds_Correctly_If_ProjectFileXml_Contains_Links_To_Missing_Files()
        {
            var projectFileXmlContain = Resources.ProjectXmlWithNonExistingLinkedFiles;

            var projectFileInfo = ProjectFileInfoBuilder.Build("name1","name2", "name3", projectFileXmlContain);

            Assert.That(projectFileInfo.Links.Count, Is.EqualTo(3));
            Assert.That(projectFileInfo.IsProjectFileWithLinks, Is.True);
            Assert.That(projectFileInfo.IsProjectFileXmlCorrect, Is.True);
        }
    }
}
