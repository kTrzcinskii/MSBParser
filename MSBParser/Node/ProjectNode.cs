using System.Xml.Linq;

namespace MSBParser.Node;
internal class ProjectNode : Node
{
    public List<PropertyGroupNode> PropertyGroups { get; }
    public List<ItemGroupNode> ItemGroups { get; }
    public List<TargetNode> Targets { get; }

    public ProjectNode(XElement sourceXml, List<PropertyGroupNode> propertyGroups, List<ItemGroupNode> itemGroups, List<TargetNode> targets ) : base(sourceXml)
    {
        PropertyGroups = propertyGroups;
        ItemGroups = itemGroups;
        Targets = targets;
    }

    public override void AcceptVisitor(INodeVisitor visitor)
    {
        visitor.VisitProjectNode(this);
    }
}
