using System;
using System.Collections.Generic;
using RepositoryGroomer.Core;

namespace RepositoryGroomer.Modern.Tests
{
    public static class TestRepoPreparators
    {
        public static List<ProjectFileInfo> AddProjectsWithBrokenReference(this List<ProjectFileInfo> projects, int numberOfProjects = 1)
        {
            if(projects == null)
                projects = new List<ProjectFileInfo>();

            for (var i = 0; i < numberOfProjects; i++)
                projects.Add(GetProjectWithBrokenReference());

            return projects;
        }
        public static List<ProjectFileInfo> AddProjectsWithBrokenLinks(this List<ProjectFileInfo> projects, int numberOfProjects = 1)
        {
            if(projects == null)
                projects = new List<ProjectFileInfo>();

            for (var i = 0; i < numberOfProjects; i++)
                projects.Add(GetProjectWithBrokenLink());

            return projects;
        }

        public static List<ProjectFileInfo> AddCorrectProjects(this List<ProjectFileInfo> projects,
            int numberOfProjects = 1)
        {
            if (projects == null)
                projects = new List<ProjectFileInfo>();

            for (var i = 0; i < numberOfProjects; i++)
                projects.Add(GetCorrectProject());

            return projects;

        }

        private static ProjectFileInfo GetCorrectProject()
        {
            var random = new Random((int)DateTime.Now.Ticks);
            var suffix = random.Next(1, 10000000);

            return new ProjectFileInfo("FP" + suffix, "DP" + suffix, "PN" + suffix,
                new List<LinkedFileInfo>(),
                new List<Reference>(), true);
        }
        private static ProjectFileInfo GetProjectWithBrokenLink()
        {
            var random = new Random((int)DateTime.Now.Ticks);
            var suffix = random.Next(1, 10000000);
            return new ProjectFileInfo("FP" + suffix, "DP" + suffix, "PN" + suffix, 
                new List<LinkedFileInfo>
                {
                    new LinkedFileInfo("FRP" + random, LinkTagTypes.Compile, "UP"+random, false)
                },
                new List<Reference>(), true);

        }

        private static ProjectFileInfo GetProjectWithBrokenReference()
        {
            var random = new Random((int)DateTime.Now.Ticks);
            var suffix = random.Next(1, 10000000);
            return new ProjectFileInfo("FP" + suffix, "DP" + suffix, "PN" + suffix, new List<LinkedFileInfo>(),
                new List<Reference>
                {
                    new Reference(false, "Include" + suffix)
                }, true);
        }
    }
}
