using System.Configuration;

namespace RepositoryGroomer.Modern
{
    public interface IAmConfigurationProvider
    {
        string SearchPath { get; }
    }

    public class ConfigurationProvider : IAmConfigurationProvider
    {
        public string SearchPath => ConfigurationManager.AppSettings.Get(nameof(SearchPath));
    }
}