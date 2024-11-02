using System.Xml.Linq;

namespace MSBParser.Nodes;
internal class ItemMetadataNode : Node
{
    public ItemMetadataNode(XElement sourceXml) : base(sourceXml)
    {
    }

    public override void AcceptVisitor(INodeVisitor visitor)
    {
        visitor.VisitItemMetadataNode(this);
    }
}
