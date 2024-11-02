using System.Xml.Linq;

namespace MSBParser.Nodes;

internal class ItemNode : Node
{
    public List<ItemMetadataNode> ItemMetadatas { get; }
    public ItemNode(XElement sourceXml, List<ItemMetadataNode> itemMetadatas) : base(sourceXml)
    {
        ItemMetadatas = itemMetadatas;
    }

    public override void AcceptVisitor(INodeVisitor visitor)
    {
        visitor.VisitItemNode(this);
    }
}