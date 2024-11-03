using System.Xml.Linq;

namespace MSBParser.Nodes;

internal class OutputNode : Node
{
    public OutputNode(XElement sourceXml, List<ParsingErrorNode> parsingErrors) : base(sourceXml, parsingErrors)
    {
    }

    public override void AcceptVisitor(INodeVisitor visitor)
    {
        visitor.VisitOutputNode(this);
    }
}