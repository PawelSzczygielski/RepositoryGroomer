using System;
using System.IO;

namespace RepositoryGroomer.Core
{
    public class LinkedFileInfo
    {
        public string LinkedFileRelativePath { get; }
        public string LinkTagParentName { get; set; }

        public bool IsLinkValid { get; }

        public string LinkedFileUnwrappedPath { get; }

        public LinkedFileInfo(string directoryWhereCsprojExists, string linkedFileRelativePath, string linkTagParentName)
        {
            LinkedFileRelativePath = linkedFileRelativePath;
            LinkTagParentName = linkTagParentName;
            LinkedFileUnwrappedPath = UnwrapRelativePath(directoryWhereCsprojExists, LinkedFileRelativePath);
            IsLinkValid = File.Exists(LinkedFileUnwrappedPath);
        }

        private static string UnwrapRelativePath(string directoryWhereCsprojExists, string linkedFileRelativePath)
        {
            var gluedPaths = Path.Combine(directoryWhereCsprojExists, linkedFileRelativePath);
            var unwrapped = new Uri(gluedPaths).LocalPath;//Path.GetFullPath(gluedPaths);
            return unwrapped;
        }
    }
}