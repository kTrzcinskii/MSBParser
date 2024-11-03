using System.Xml.Linq;

namespace MSBParser.Nodes;

internal class ParameterNode : Node
{
    public ParameterNode(XElement sourceXml, List<ParsingErrorNode> parsingErrors) : base(sourceXml, parsingErrors)
    {
    }

    public override void AcceptVisitor(INodeVisitor visitor)
    {
        visitor.VisitParameterNode(this);        
    }
}