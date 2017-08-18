using System.Diagnostics;

namespace RepositoryGroomer.Core
{
    [DebuggerDisplay("{" + nameof(LinkedFileUnwrappedPath) + "} | {" + nameof(TargetLinkedFileExists) + "}")]
    public class LinkedFileInfo: IAmXmlNode
    {
        public string LinkedFileRelativePath { get; }
        public LinkTagTypes LinkTagType { get; set; }

        public bool TargetLinkedFileExists { get; }

        public string LinkedFileUnwrappedPath { get; }
        public string OriginalXml { get; }

        public LinkedFileInfo(string originalXml, string linkedFileRelativePath, LinkTagTypes linkTagType,
            string linkedFileUnwrappedPath, bool targetLinkedFileExists)
        {
            OriginalXml = originalXml;
            LinkedFileRelativePath = linkedFileRelativePath;
            LinkedFileUnwrappedPath = linkedFileUnwrappedPath;
            LinkTagType = linkTagType;
            TargetLinkedFileExists = targetLinkedFileExists;
        }
    }
}