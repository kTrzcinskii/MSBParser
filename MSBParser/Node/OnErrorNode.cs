using System.Xml.Linq;

namespace MSBParser.Node;

internal class OnErrorNode : Node
{
    public OnErrorNode(XElement sourceXml) : base(sourceXml)
    {
    }
}