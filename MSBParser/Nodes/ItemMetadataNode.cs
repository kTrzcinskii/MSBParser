using System.Xml.Linq;

namespace MSBParser.Nodes;
internal class ItemMetadataNode : Node
{
    public ItemMetadataNode(XElement sourceXml, List<ParsingErrorNode> parsingErrors) : base(sourceXml, parsingErrors)
    {
    }

    public override void AcceptVisitor(INodeVisitor visitor)
    {
        visitor.VisitItemMetadataNode(this);
    }
}
