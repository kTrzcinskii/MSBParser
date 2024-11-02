using System.Xml.Linq;

namespace MSBParser.Nodes;

internal class OnErrorNode : Node
{
    public OnErrorNode(XElement sourceXml) : base(sourceXml)
    {
    }

    public override void AcceptVisitor(INodeVisitor visitor)
    {
        visitor.VisitOnErrorNode(this);
    }
}