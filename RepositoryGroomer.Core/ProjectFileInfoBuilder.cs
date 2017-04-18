using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using log4net;

namespace RepositoryGroomer.Core
{
    public static class ProjectFileInfoBuilder
    {
        private const string CSPROJ_NAMESPACE = "http://schemas.microsoft.com/developer/msbuild/2003";

        private static readonly ILog Log = LogManager.GetLogger(typeof(ProjectFileInfo));

        public static ProjectFileInfo Build(FileInfo fileInfo)
        {
            var projectFilePath = fileInfo.FullName;
            var containingDirectoryPath = fileInfo.DirectoryName;
            var projectName = fileInfo.Name.Replace(fileInfo.Extension, string.Empty);
            var projectFileXml = File.ReadAllText(projectFilePath);
            return Build(projectFilePath, containingDirectoryPath, projectName, projectFileXml);
        }

        public static ProjectFileInfo Build(string projectFilePath, string containingDirectoryPath, string projectName,
            string projectFileXmlContain)
        {
            List<LinkedFileInfo> links;
            var parsingSucceed = TryExtractLinks(out links, projectFileXmlContain, containingDirectoryPath);
            var fileInfoCorrect = parsingSucceed && !string.IsNullOrWhiteSpace(projectFilePath) &&
                                  !string.IsNullOrWhiteSpace(containingDirectoryPath) &&
                                  !string.IsNullOrWhiteSpace(projectName);

            return new ProjectFileInfo(projectFilePath, containingDirectoryPath, projectName, links, fileInfoCorrect);
        }

        private static bool TryExtractLinks(out List<LinkedFileInfo> links, string projectFileXml, string containingDirectoryPath)
        {
            try
            {
                var xDoc = XDocument.Parse(projectFileXml);
                links =
                    xDoc.Descendants($"{{{CSPROJ_NAMESPACE}}}Link")
                        .Select(xElement => CreateLinkedFileInfo(xElement, containingDirectoryPath))
                        .ToList();
                return true;
            }
            catch (Exception ex)
            {
                Log.Error($"Unable to parse project file from {containingDirectoryPath}", ex);
                links = new List<LinkedFileInfo>();
                return false;
            }
        }
        
        private static LinkedFileInfo CreateLinkedFileInfo(XElement element, string containingDirectoryPath)
        {
            var parent = element?.Parent;
            if(parent == null)
                return new LinkedFileInfo(string.Empty, LinkTagTypes.Unknown, string.Empty, false);

            var linkedFileRelativePath = parent.Attribute("Include")?.Value;
            var linkTagParentName = parent.Name.LocalName;
            var linkTagType = GetTagType(linkTagParentName);
            var linkedFileUnwrappedPath = UnwrapRelativePath(containingDirectoryPath, linkedFileRelativePath);
            var targetLinkedFileExists = File.Exists(linkedFileUnwrappedPath) && linkTagType != LinkTagTypes.Unknown;

            return new LinkedFileInfo(linkedFileRelativePath, linkTagType, linkedFileUnwrappedPath,
                targetLinkedFileExists);
        }

        private static string UnwrapRelativePath(string directoryWhereCsprojExists, string linkedFileRelativePath)
        {
            try
            {
                var gluedPaths = Path.Combine(directoryWhereCsprojExists, linkedFileRelativePath);
                var unwrapped = new Uri(gluedPaths).LocalPath;
                return unwrapped;
            }
            catch (Exception ex)
            {
                Log.Error($"Cannot unwrap relative path glued from two pieces: '{directoryWhereCsprojExists}' and '{linkedFileRelativePath}'.", ex);
                return string.Empty;
            }
        }

        private static LinkTagTypes GetTagType(string linkTagParentName)
        {
            LinkTagTypes result;
            if (Enum.TryParse(linkTagParentName, true, out result))
                return result;

            return LinkTagTypes.Unknown;
        }
    }
}