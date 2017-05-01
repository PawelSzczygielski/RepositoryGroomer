using System;
using System.Collections.Generic;
using System.Linq;
using RepositoryGroomer.Core;

namespace RepositoryGroomer.Modern
{
    public class ProjectFileFilterGroup
    {
        private readonly List<Predicate<object>> _filters;
        private readonly Predicate<object> _linkFilter = obj =>
            {
                var projFileInfo = obj as ProjectFileInfo;
                if (projFileInfo == null)
                    return false;
                return projFileInfo.ProjectFileContainsLinksToFiles;
            };
        private readonly Predicate<object> _referenceFilter = obj =>
        {
            var projFileInfo = obj as ProjectFileInfo;
            if (projFileInfo == null)
                return false;
            return projFileInfo.ProjectFileContainsInvalidReferences;
        };

        public Predicate<object> Filter { get; }

        public ProjectFileFilterGroup()
        {
            _filters = new List<Predicate<object>>();
            Filter = InternalFilter;
        }

        private bool InternalFilter(object o)
        {
            if (!_filters.Any())
                return true;

            return _filters.Any(filter => filter(o));
        }
        
        public void RemoveLinkFilter()
        {
            _filters.Remove(_linkFilter);
        }

        public void AddLinkFilter()
        {
            _filters.Add(_linkFilter);
        }

        public void AddReferenceFilter()
        {
            _filters.Add(_referenceFilter);
        }

        public void RemoveReferenceFilter()
        {
            _filters.Remove(_referenceFilter);
        }
    }
}
