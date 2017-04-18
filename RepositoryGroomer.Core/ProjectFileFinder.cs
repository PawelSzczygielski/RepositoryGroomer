using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using log4net;

namespace RepositoryGroomer.Core
{
    public interface IProjectFileFinder
    {
        
    }

    public class ProjectFileFinder : IProjectFileFinder
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ProjectFileFinder));
        private const string SEARCH_PATTERN = "*.csproj";

        public List<ProjectFileInfo> GetAllProjects(string folderPath)
        {
            var folderToSearch = new DirectoryInfo(folderPath);
            var foundFiles = new List<ProjectFileInfo>();
            WalkDirectoryTree(folderToSearch, ref foundFiles);
            return foundFiles;
        }

        private void WalkDirectoryTree(DirectoryInfo folderToSearch, ref List<ProjectFileInfo> foundFiles)
        {
            if (!folderToSearch.Exists)
            {
                Log.Error($"Cannot find directory '{folderToSearch.FullName}'.");
                return;
            }

            try
            {
                var filesInsideDirectory = folderToSearch.GetFiles(SEARCH_PATTERN);
                foundFiles.AddRange(filesInsideDirectory.Select(fi => ProjectFileInfoBuilder.Build(fi)));
            }
            catch (PathTooLongException ptlex)
            {
                Log.Error(
                    $"Directory '{folderToSearch.Name}' cannot be accessed. {nameof(PathTooLongException)}: '{ptlex.Message}'");
                return;
            }
            catch (UnauthorizedAccessException uaex)
            {
                Log.Error(
                    $"Directory '{folderToSearch.FullName}' cannot be accessed. {nameof(UnauthorizedAccessException)}: '{uaex.Message}'");
                return;
            }
            catch (Exception ex)
            {
                Log.Error($"Directory '{folderToSearch.FullName}' cannot be accessed. {ex.GetType().Name}: '{ex.Message}'");
                return;
            }

            var subDirectories = folderToSearch.GetDirectories();
            foreach (var subDirectory in subDirectories)
                WalkDirectoryTree(subDirectory, ref foundFiles);
        }
    }
}
