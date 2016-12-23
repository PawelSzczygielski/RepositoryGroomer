using System;
using System.IO;

namespace RepositoryGroomer.Core
{
    public class LinkedFileInfo
    {
        public string LinkedFileRelativePath { get; }
        public LinkTagTypes LinkTagType { get; set; }

        public bool IsLinkValid { get; }

        public string LinkedFileUnwrappedPath { get; }

        public LinkedFileInfo(string directoryWhereCsprojExists, string linkedFileRelativePath, string linkTagParentName)
        {
            LinkedFileRelativePath = linkedFileRelativePath;
            LinkTagType = GetTagType(linkTagParentName);
            LinkedFileUnwrappedPath = UnwrapRelativePath(directoryWhereCsprojExists, LinkedFileRelativePath);
            IsLinkValid = File.Exists(LinkedFileUnwrappedPath) && LinkTagType != LinkTagTypes.Unknown;
        }

        private static LinkTagTypes GetTagType(string linkTagParentName)
        {
            LinkTagTypes result;
            if(Enum.TryParse(linkTagParentName, true, out result))
                return result;

            return LinkTagTypes.Unknown;
        }

        private static string UnwrapRelativePath(string directoryWhereCsprojExists, string linkedFileRelativePath)
        {
            var gluedPaths = Path.Combine(directoryWhereCsprojExists, linkedFileRelativePath);
            var unwrapped = new Uri(gluedPaths).LocalPath;//Path.GetFullPath(gluedPaths);
            return unwrapped;
        }
    }
}