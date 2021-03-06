﻿using System;
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
            XDocument xDoc;
            try
            {
                 xDoc = XDocument.Parse(projectFileXmlContain);
            }
            catch (Exception ex)
            {
                Log.Error("Cannot parse csproj's xml contain.", ex);
                return new ProjectFileInfo(projectFilePath, containingDirectoryPath, projectName, new List<LinkedFileInfo>(), new List<Reference>(), false);
            }

            List<LinkedFileInfo> links;
            var parsingLinksSucceed = TryExtractLinks(out links, xDoc, containingDirectoryPath);

            List<Reference> references;
            var parsingReferencesSucceeded = TryExtractReferences(out references, xDoc, containingDirectoryPath);


            var fileInfoCorrect = parsingLinksSucceed &&
                                  parsingReferencesSucceeded && 
                                  !string.IsNullOrWhiteSpace(projectFilePath) &&
                                  !string.IsNullOrWhiteSpace(containingDirectoryPath) &&
                                  !string.IsNullOrWhiteSpace(projectName);

            return new ProjectFileInfo(projectFilePath, containingDirectoryPath, projectName, links, references, fileInfoCorrect);
        }

        private static bool TryExtractReferences(out List<Reference> references, XDocument xDoc,
            string containingDirectoryPath)
        {
            try
            {
                references = xDoc
                    .Descendants($"{{{CSPROJ_NAMESPACE}}}Reference")
                    .Select(xElement => CreateReference(xElement, containingDirectoryPath))
                    .ToList();
                return true;
            }
            catch (Exception ex)
            {
                Log.Error($"Unable to parse project file from {containingDirectoryPath}", ex);
                references = new List<Reference>();
                return false;
            }
        }

        private static bool TryExtractLinks(out List<LinkedFileInfo> links, XDocument xDoc, string containingDirectoryPath)
        {
            try
            {
                
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

        private static Reference CreateReference(XElement element, string containingDirectoryPath)
        {
            var parent = element?.Parent;
            if (parent == null)
                return new Reference();

            var include = element.Attribute("Include")?.Value;
            var hintPath = element.Element($"{{{CSPROJ_NAMESPACE}}}HintPath")?.Value;

            var unwrappedHintPath = string.Empty;
            if(!string.IsNullOrWhiteSpace(containingDirectoryPath) && !string.IsNullOrWhiteSpace(hintPath))
                unwrappedHintPath = UnwrapRelativePath(containingDirectoryPath, hintPath);

            var embedInteropTypes = element.Element($"{{{CSPROJ_NAMESPACE}}}EmbedInteropTypes").ToNullableBool();
            var specificVersion = element.Element($"{{{CSPROJ_NAMESPACE}}}SpecificVersion").ToNullableBool();
            var @private = element.Element($"{{{CSPROJ_NAMESPACE}}}Private").ToNullableBool();
            var referenceEntryValid = CheckTarget(include, hintPath, unwrappedHintPath);
            return new Reference(element.StripNamespaces().ToString(), referenceEntryValid, include, hintPath, unwrappedHintPath, embedInteropTypes, specificVersion, @private);
        }

        private static bool CheckTarget(string include, string hintPath, string unwrappedHintPath)
        {
            var includeValid = !string.IsNullOrWhiteSpace(include);

            if (string.IsNullOrWhiteSpace(hintPath))
                return includeValid;

            return includeValid && File.Exists(unwrappedHintPath);
        }

        private static LinkedFileInfo CreateLinkedFileInfo(XElement element, string containingDirectoryPath)
        {
            var parent = element?.Parent;
            if(parent == null)
                return new LinkedFileInfo(element.StripNamespaces().ToString(), string.Empty, LinkTagTypes.Unknown, string.Empty, false);

            var linkedFileRelativePath = parent.Attribute("Include")?.Value;
            var linkTagParentName = parent.Name.LocalName;
            var linkTagType = GetTagType(linkTagParentName);
            var linkedFileUnwrappedPath = UnwrapRelativePath(containingDirectoryPath, linkedFileRelativePath);
            var targetLinkedFileExists = File.Exists(linkedFileUnwrappedPath);

            return new LinkedFileInfo(parent.StripNamespaces().ToString(), linkedFileRelativePath, linkTagType, linkedFileUnwrappedPath,
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