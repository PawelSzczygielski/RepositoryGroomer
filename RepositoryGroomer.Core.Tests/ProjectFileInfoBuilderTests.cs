using System.Linq;
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

            var projectFileInfo = ProjectFileInfoBuilder.Build("name1", "name2", "name3", projectFileXmlContain);

            Assert.That(projectFileInfo.Links.Count, Is.EqualTo(3));
            Assert.That(projectFileInfo.IsProjectFileWithLinks, Is.True);
            Assert.That(projectFileInfo.IsProjectFileXmlCorrect, Is.True);
        }

        [Test]
        public void Builder_Extracts_References()
        {
            var projectFileXmlContain = Resources.ProjectXmlWithReferences;
            var projectFileInfo = ProjectFileInfoBuilder.Build("C:\\Repository\\Project.csproj", "C:\\Repository",
                "Project", projectFileXmlContain);

            Assert.That(projectFileInfo.References.Count, Is.EqualTo(9));
        }

        [Test]
        public void Builder_Fills_Reference_Data()
        {
            var projectFileXmlContain = Resources.ProjectXmlWithReferences;
            var projectFileInfo = ProjectFileInfoBuilder.Build("C:\\Repository\\Project.csproj", "C:\\Repository",
                "Project", projectFileXmlContain);
            var references = projectFileInfo.References;

            Assert.That(references.All(reference => !string.IsNullOrWhiteSpace(reference.Include)));
            Assert.That(references.Any(reference => !string.IsNullOrWhiteSpace(reference.HintPath)));
            Assert.That(references.Any(reference => reference.EmbedInteropTypes.HasValue));
            Assert.That(references.Any(reference => reference.SpecificVersion.HasValue));
            Assert.That(references.Any(reference => reference.Private.HasValue));
            Assert.That(
                references.Where(reference => !string.IsNullOrWhiteSpace(reference.HintPath))
                    .All(reference => !string.IsNullOrWhiteSpace(reference.UnwrappedHintPath)));
            Assert.That(references.All(reference => reference.ReferenceEntryValid));

        }
    }
}
