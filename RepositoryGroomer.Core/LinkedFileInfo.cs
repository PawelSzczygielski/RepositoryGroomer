using System.Diagnostics;

namespace RepositoryGroomer.Core
{
    [DebuggerDisplay("{" + nameof(LinkedFileUnwrappedPath) + "} | {" + nameof(TargetLinkedFileExists) + "}")]
    public class LinkedFileInfo
    {
        public string LinkedFileRelativePath { get; }
        public LinkTagTypes LinkTagType { get; set; }

        public bool TargetLinkedFileExists { get; }

        public string LinkedFileUnwrappedPath { get; }

        public LinkedFileInfo(string linkedFileRelativePath, LinkTagTypes linkTagType,
            string linkedFileUnwrappedPath, bool targetLinkedFileExists)
        {
            LinkedFileRelativePath = linkedFileRelativePath;
            LinkedFileUnwrappedPath = linkedFileUnwrappedPath;
            LinkTagType = linkTagType;
            TargetLinkedFileExists = targetLinkedFileExists;
        }

       

      
    }
}