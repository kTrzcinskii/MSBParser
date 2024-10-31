using System.Xml.Linq;

namespace MSBParser.Node;
internal class ItemGroupNode : Node
{
    public ItemGroupNode(XElement sourceXml) : base(sourceXml)
    {
    }
}
