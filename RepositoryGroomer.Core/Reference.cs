﻿using System.Diagnostics;
using System.IO;

namespace RepositoryGroomer.Core
{
    [DebuggerDisplay("{" + nameof(Include) + "} | {" + nameof(ReferenceEntryValid) + "}")]
    public class Reference
    {
        public Reference(bool referenceEntryValid, string include, string hintPath = null, string unwrappedHintPath = null, bool? embedInteropTypes = null, bool? specificVersion = null,
            bool? @private = null)
        {
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
    }
}