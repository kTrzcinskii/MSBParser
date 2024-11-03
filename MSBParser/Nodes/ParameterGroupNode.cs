using System.Xml.Linq;

namespace MSBParser.Nodes;

internal class ParameterGroupNode : Node
{
    public List<ParameterNode> Parameters { get; }
    
    public ParameterGroupNode(XElement sourceXml, List<ParameterNode> parameters) : base(sourceXml)
    {
        Parameters = parameters;
    }

    public override void AcceptVisitor(INodeVisitor visitor)
    {
        visitor.VisitParameterGroupNode(this);
    }
}