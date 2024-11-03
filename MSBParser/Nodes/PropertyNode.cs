using System.Xml.Linq;

namespace MSBParser.Nodes;
internal class PropertyNode : Node
{
    public PropertyNode(XElement sourceXml, List<ParsingErrorNode> parsingErrors) : base(sourceXml, parsingErrors)
    {
    }

    public override void AcceptVisitor(INodeVisitor visitor)
    {
        visitor.VisitPropertyNode(this);
    }
}
