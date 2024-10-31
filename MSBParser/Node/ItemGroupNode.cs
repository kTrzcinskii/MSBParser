using System.Xml.Linq;

namespace MSBParser.Node;
internal class ItemGroupNode : Node
{
    public List<ItemNode> Items { get; }
    
    public ItemGroupNode(XElement sourceXml, List<ItemNode> items) : base(sourceXml)
    {
        Items = items;
    }
}
