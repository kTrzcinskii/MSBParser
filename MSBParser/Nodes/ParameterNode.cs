using System.Xml.Linq;

namespace MSBParser.Nodes;

internal class ParameterNode : Node
{
    public ParameterNode(XElement sourceXml) : base(sourceXml)
    {
    }

    public override void AcceptVisitor(INodeVisitor visitor)
    {
        visitor.VisitParameterNode(this);        
    }
}