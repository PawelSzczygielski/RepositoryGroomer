using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace RepositoryGroomer.Core
{
    [DebuggerDisplay("{ProjectName} | BrokenRefs: {ProjectFileContainsInvalidReferences} | Links: {ProjectFileContainsLinksToFiles}")]
    public class ProjectFileInfo
    {
        public List<Reference> References { get; }
        public string ProjectName { get; }
        public string ProjectFilePath { get; }
        public string ContainingDirectoryPath { get; }
        public List<LinkedFileInfo> Links { get; }
        public bool ProjectFileContainsLinksToFiles => Links.Any();
        public bool ProjectFileContainsInvalidReferences => References.Any(reference => !reference.ReferenceEntryValid);
        public bool IsProjectFileXmlCorrect { get; }

        public ProjectFileInfo(
            string projectFilePath,
            string containingDirectoryPath,
            string projectName,
            List<LinkedFileInfo> links,
            List<Reference> references,
            bool projectFileValid)
        {
            ProjectFilePath = projectFilePath;
            ContainingDirectoryPath = containingDirectoryPath;
            ProjectName = projectName;
            Links = links;
            References = references;
            IsProjectFileXmlCorrect = projectFileValid;
        }
    }
}