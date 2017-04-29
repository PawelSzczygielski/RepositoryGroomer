using System.IO;

namespace RepositoryGroomer.Core
{
    public interface IAmFileReader
    {
        string ReadFromFile(string filePath);
    }

    public class FileReader : IAmFileReader
    {
        public string ReadFromFile(string filePath)
        {
            if (!File.Exists(filePath))
                return string.Empty;

            return File.ReadAllText(filePath);
        }
    }
}
