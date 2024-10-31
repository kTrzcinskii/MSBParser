using System.Xml.Linq;

namespace MSBParser.Node;
internal class PropertyGroupNode : Node
{
    public PropertyGroupNode(XElement sourceXml) : base(sourceXml)
    {
    }
}
