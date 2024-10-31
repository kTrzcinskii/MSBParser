using System.Xml.Linq;

namespace MSBParser.Node;
internal class TargetNode : Node
{
    public TargetNode(XElement sourceXml) : base(sourceXml)
    {
    }
}
