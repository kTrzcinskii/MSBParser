using System.Xml.Linq;

namespace MSBParser.Nodes;

internal class SdkNode : Node
{
    public SdkNode(XElement sourceXml, List<ParsingErrorNode> parsingErrors) : base(sourceXml, parsingErrors)
    {
    }

    public override void AcceptVisitor(INodeVisitor visitor)
    {
        visitor.VisitSdkNode(this);
    }
}