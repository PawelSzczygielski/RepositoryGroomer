using System.Linq;
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

        public static XElement IgnoreNamespace(this XElement xelem)
        {
            XNamespace xmlns = string.Empty;
            var name = xmlns + xelem.Name.LocalName;
            return new XElement(name, xelem.Elements().Select(e => e.IgnoreNamespace()), xelem.Attributes());
        }

        public static XNode StripNamespaces(this XNode n)
        {
            var xe = n as XElement;
            if (xe == null)
                return n;
            var contents =
                // add in all attributes there were on the original
                xe.Attributes()
                // eliminate the default namespace declaration
                .Where(xa => xa.Name.LocalName != "xmlns")
                .Cast<object>()
                // add in all other element children (nodes and elements, not just elements)
                .Concat(xe.Nodes().Select(node => node.StripNamespaces()).Cast<object>()).ToArray();
            var result = new XElement(XNamespace.None + xe.Name.LocalName, contents);
            return result;

        }
    }
}
