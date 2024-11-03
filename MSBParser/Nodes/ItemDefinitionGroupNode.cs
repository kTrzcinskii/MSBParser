using System.Xml.Linq;

namespace MSBParser.Nodes;

internal class ItemDefinitionGroupNode : Node
{
    public List<ItemNode> Items { get; }
    
    public ItemDefinitionGroupNode(XElement sourceXml, List<ParsingErrorNode> parsingErrors, List<ItemNode> items) : base(sourceXml, parsingErrors)
    {
        Items = items;
    }

    public override void AcceptVisitor(INodeVisitor visitor)
    {
        visitor.VisitItemDefinitionGroupNode(this);
    }
}