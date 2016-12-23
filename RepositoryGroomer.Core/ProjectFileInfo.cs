using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using log4net;

namespace RepositoryGroomer.Core
{
    public class ProjectFileInfo
    {
        private const string CSPROJ_NAMESPACE = "http://schemas.microsoft.com/developer/msbuild/2003";
        private static readonly ILog Log = LogManager.GetLogger(typeof(ProjectFileInfo));

        public string FilePath { get; }
        public string DirectoryPath { get; }

        public string XmlContain { get; set; }

        public List<LinkedFileInfo> Links { get; private set; }

        public bool IsProjectFileValid { get; }

        public ProjectFileInfo(FileInfo fi)
        {
            if(fi == null)
                throw new ArgumentException($"Cannot create {nameof(ProjectFileInfo)} based on invalid file info.");

            FilePath = fi.FullName;
            IsProjectFileValid = true;
            DirectoryPath = fi.DirectoryName;
            Links = new List<LinkedFileInfo>();

            XmlContain = File.ReadAllText(FilePath);
            if (string.IsNullOrEmpty(XmlContain))
            {
                IsProjectFileValid = false;
                Log.Error($"Project file '{FilePath}' is an empty file.");
            }
            ParseXml(XmlContain);
        }

        private void ParseXml(string xmlContain)
        {
            var xDoc = XDocument.Parse(xmlContain);
            Links = xDoc.Descendants($"{{{CSPROJ_NAMESPACE}}}Link").Select(CreateLinkedFileInfo).ToList();
        }

        private LinkedFileInfo CreateLinkedFileInfo(XElement element)
        {
            if(element == null)
                throw new ArgumentException($"Cannot create {nameof(LinkedFileInfo)} based on invalid xElement.");
            var parent = element.Parent;
            if(parent == null)
                throw new ArgumentException($"Cannot create {nameof(LinkedFileInfo)} based on orphan xElement.");

            var linkedFileRelativePath = parent.Attribute($"Include")?.Value;
            var linkTagParentName = parent.Name.LocalName;
            return new LinkedFileInfo(DirectoryPath, linkedFileRelativePath, linkTagParentName);
        }
        
    }

}