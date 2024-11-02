using System.Xml.Linq;

namespace MSBParser.Nodes;
internal class PropertyNode : Node
{
    public PropertyNode(XElement sourceXml) : base(sourceXml)
    {
    }

    public override void AcceptVisitor(INodeVisitor visitor)
    {
        visitor.VisitPropertyNode(this);
    }
}
