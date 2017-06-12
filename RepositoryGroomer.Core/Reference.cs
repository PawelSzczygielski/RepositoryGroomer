using System.Diagnostics;


namespace RepositoryGroomer.Core
{
    [DebuggerDisplay("{" + nameof(Include) + "} | {" + nameof(ReferenceEntryValid) + "}")]
    public class Reference : IAmXmlNode
    {
        public Reference(string originalXml, bool referenceEntryValid, string include, string hintPath = null, string unwrappedHintPath = null, bool? embedInteropTypes = null, bool? specificVersion = null,
            bool? @private = null)
        {
            OriginalXml = originalXml;
            Include = include;
            HintPath = hintPath;
            UnwrappedHintPath = unwrappedHintPath;
            EmbedInteropTypes = embedInteropTypes;
            SpecificVersion = specificVersion;
            Private = @private;
            ReferenceEntryValid = referenceEntryValid;
        }
        
        public Reference()
        {
        }

        public bool ReferenceEntryValid { get; } //TODO: Possible place for validation rules Only Include is needed in order to be valid?
        public string Include { get; }
        public bool? EmbedInteropTypes { get; }
        public bool? SpecificVersion { get; }
        public string HintPath { get; }
        public string UnwrappedHintPath { get; }
        public bool? Private { get; }
        public string OriginalXml { get; }

        public override string ToString() => OriginalXml;
    }

    public interface IAmXmlNode
    {
        string OriginalXml { get; }
    }
}