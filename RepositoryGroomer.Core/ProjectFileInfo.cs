using System.Collections.Generic;
using System.Linq;

namespace RepositoryGroomer.Core
{
    public class ProjectFileInfo
    {
        public List<Reference> References { get; }
        public string ProjectName { get; }
        public string ProjectFilePath { get; }
        public string ContainingDirectoryPath { get; }
        public List<LinkedFileInfo> Links { get; }
        public bool IsProjectFileWithLinks => Links.Any();
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