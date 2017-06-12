using NUnit.Framework;
using RepositoryGroomer.Core;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;

namespace RepositoryGroomer.Modern.Tests
{
    [TestFixture]
    public class ProjectFileInfoGroupTests
    {
        [Test]
        public void Adding_Link_Filter_Removes_Linked_Projects_From_View()
        {
            var projectsInternal =
                new List<ProjectFileInfo>().AddProjectsWithBrokenReference()
                    .AddProjectsWithBrokenLinks()
                    .AddCorrectProjects();
            var view = CollectionViewSource.GetDefaultView(projectsInternal);

            Assert.That(view.Cast<ProjectFileInfo>().Count(), Is.EqualTo(3));

            var filterGroup = new ProjectFileFilterGroup();
            filterGroup.AddLinkFilter();

            view.Filter = filterGroup.Filter;

            Assert.That(view.Cast<ProjectFileInfo>().Count(), Is.EqualTo(1));
        }

        [Test]
        public void Adding_Filter_Twice_Doesnt_Make_Filtering_Wrong()
        {
            var projectsInternal =
                    new List<ProjectFileInfo>().AddProjectsWithBrokenReference()
                        .AddProjectsWithBrokenLinks()
                        .AddCorrectProjects();
            var view = CollectionViewSource.GetDefaultView(projectsInternal);

            Assert.That(view.Cast<ProjectFileInfo>().Count(), Is.EqualTo(3));

            var filterGroup = new ProjectFileFilterGroup();
            filterGroup.AddLinkFilter();
            filterGroup.AddLinkFilter();

            view.Filter = filterGroup.Filter;

            Assert.That(view.Cast<ProjectFileInfo>().Count(), Is.EqualTo(1));
        }

        [Test]
        public void Removing_Filter_Restores_Initial_View()
        {
            var projectsInternal =
                    new List<ProjectFileInfo>().AddProjectsWithBrokenReference()
                        .AddProjectsWithBrokenLinks()
                        .AddCorrectProjects();
            var view = CollectionViewSource.GetDefaultView(projectsInternal);

            var filterGroup = new ProjectFileFilterGroup();
            filterGroup.AddLinkFilter();
            view.Filter = filterGroup.Filter;
            Assert.That(view.Cast<ProjectFileInfo>().Count(), Is.EqualTo(1));
            filterGroup.RemoveLinkFilter();
            view.Refresh();
            Assert.That(view.Cast<ProjectFileInfo>().Count(), Is.EqualTo(3));
        }

        [Test]
        public void Filters_For_Link_And_Reference_Works_Together()
        {
            var projectsInternal =
                    new List<ProjectFileInfo>().AddProjectsWithBrokenReference()
                        .AddProjectsWithBrokenLinks()
                        .AddCorrectProjects();
            var view = CollectionViewSource.GetDefaultView(projectsInternal);

            var filterGroup = new ProjectFileFilterGroup();
            filterGroup.AddLinkFilter();
            filterGroup.AddReferenceFilter();
            view.Filter = filterGroup.Filter;
            Assert.That(view.Cast<ProjectFileInfo>().Count(), Is.EqualTo(2));
        }

        [Test]
        public void Filtering_Works_For_Example_From_Tests()
        {
            var projectsInternal = new List<ProjectFileInfo>
            {
                new ProjectFileInfo("ProjectWithBrokenReference", "DirPath1", "ProjectName1", new List<LinkedFileInfo>(),
                    new List<Reference> {new Reference(string.Empty, false, "Include", "HintPath", "UnwrappedHintPath")},
                    true),
                new ProjectFileInfo("CorrectProject", "DirPath2", "ProjectName2", new List<LinkedFileInfo>(),
                new List<Reference> {new Reference(string.Empty, true, "Include", "HintPath", string.Empty)}, true),

            };

            var view = CollectionViewSource.GetDefaultView(projectsInternal);

            var filterGroup = new ProjectFileFilterGroup();
            filterGroup.AddLinkFilter();
            filterGroup.AddReferenceFilter();
            view.Filter = filterGroup.Filter;

            Assert.That(view.Cast<ProjectFileInfo>().ToList().Count, Is.EqualTo(1));
            
            filterGroup.RemoveReferenceFilter();
            filterGroup.RemoveLinkFilter();
            view.Refresh();

            Assert.That(view.Cast<ProjectFileInfo>().Count(), Is.EqualTo(2));
        }
    }
}
