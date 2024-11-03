using System.Xml.Linq;

namespace MSBParser.Nodes;
internal class ProjectNode : Node
{
    public List<PropertyGroupNode> PropertyGroups { get; }
    public List<ItemGroupNode> ItemGroups { get; }
    public List<TargetNode> Targets { get; }
    public List<ImportGroupNode> ImportGroups { get; }
    public List<ImportNode> Imports { get; }
    public List<ItemDefinitionGroupNode> ItemDefinitionGroups { get; }

    public ProjectNode(XElement sourceXml, List<PropertyGroupNode> propertyGroups, List<ItemGroupNode> itemGroups, List<TargetNode> targets, List<ImportGroupNode> importGroups, List<ImportNode> imports, List<ItemDefinitionGroupNode> itemDefinitionGroups) : base(sourceXml)
    {
        PropertyGroups = propertyGroups;
        ItemGroups = itemGroups;
        Targets = targets;
        ImportGroups = importGroups;
        Imports = imports;
        ItemDefinitionGroups = itemDefinitionGroups;
    }

    public override void AcceptVisitor(INodeVisitor visitor)
    {
        visitor.VisitProjectNode(this);
    }
}
