using System.Xml.Linq;

namespace MSBParser.Nodes;

internal class ItemDefinitionGroupNode : Node
{
    public List<ItemNode> Items { get; }
    
    public ItemDefinitionGroupNode(XElement sourceXml, List<ItemNode> items) : base(sourceXml)
    {
        Items = items;
    }

    public override void AcceptVisitor(INodeVisitor visitor)
    {
        visitor.VisitItemDefinitionGroupNode(this);
    }
}