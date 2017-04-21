using System.Xml.Linq;

namespace RepositoryGroomer.Core
{
    public static class ExtensionMethods
    {
        public static bool? ToNullableBool(this XElement element)
        {
            if (element == null)
                return null;

            bool result;
            var parseResult = bool.TryParse(element.Value, out result);
            if (parseResult)
                return result;
            else
                return null;
        }
    }
}
