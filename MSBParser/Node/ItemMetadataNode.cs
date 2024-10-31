using System.Xml.Linq;

namespace MSBParser.Node;
internal class ItemMetadataNode : Node
{
    public ItemMetadataNode(XElement sourceXml) : base(sourceXml)
    {
    }
}
