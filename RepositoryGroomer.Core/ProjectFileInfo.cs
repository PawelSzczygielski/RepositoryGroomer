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
            const string @namespace = "http://schemas.microsoft.com/developer/msbuild/2003";
            var xDoc = XDocument.Parse(xmlContain);
            Links = xDoc.Descendants($"{{{@namespace}}}Link").Select(CreateLinkedFileInfo).ToList();
        }

        private LinkedFileInfo CreateLinkedFileInfo(XElement element)
        {
            if(element == null)
                throw new ArgumentException($"Cannot create {nameof(LinkedFileInfo)} based on invalid xElement.");

            var linkedFileRelativePath = element.Value;
            var linkTagParentName = element.Parent?.Name;
            return new LinkedFileInfo(DirectoryPath, linkedFileRelativePath, linkTagParentName?.LocalName);
        }


        /*
         public AimirEvent(XElement e)
        {
            const string ns3 = "http://iec.ch/TC57/2011/EndDeviceEvents#";
            EventType = e.Element($"{{{ns3}}}mRID")?.Value.Trim();
            EventParsedType = AimirHesEventMapper.MapEventTypeToElinEventType(EventType);
            MeterId = e.Element($"{{{ns3}}}Assets")?.Element($"{{{ns3}}}mRID")?.Value.Trim();
            CreationDate = DateTime.SpecifyKind(DateTime.Parse(e.Element($"{{{ns3}}}createdDateTime")?.Value), DateTimeKind.Utc);//meters probably are sending time in UTC
            DsoCode = e.Element($"{{{ns3}}}issuerID")?.Value;
            MeterDbId = -1;
            WasCreatedWhenMeterWasActive = false;
            
            if (ShouldContainFirmwareData)
            {
                var descendants = e.Descendants($"{{{ns3}}}EndDeviceEventDetails").ToArray();
                var name = $"{{{ns3}}}name";
                var firmwareID = descendants.SingleOrDefault(d => d.Element(name)?.Value == "firmwareID");
                var terminalFwVersion = descendants.SingleOrDefault(d => d.Element(name)?.Value == "terminalFWVersion");
                var terminalBuildNumber = descendants.SingleOrDefault(d => d.Element(name)?.Value == "terminalBuildNumber");
                var terminalHwVersion = descendants.SingleOrDefault(d => d.Element(name)?.Value == "terminalHWVersion");

                if (firmwareID != null && terminalFwVersion != null)
                {
                    var value = $"{{{ns3}}}value";
                    FirmwareData = new AimirFirmwareData(
                        firmwareID.Element(value)?.Value,
                        terminalFwVersion.Element(value)?.Value,
                        terminalBuildNumber?.Element(value)?.Value,
                        terminalHwVersion?.Element(value)?.Value);
                }
            }
        }
         */
    }

}