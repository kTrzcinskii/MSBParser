using System.Xml.Linq;

namespace MSBParser.Node;
internal class ProjectNode : Node
{
    public List<PropertyGroupNode> PropertyGroups { get; set; } = new();
    public List<TargetNode> Targets { get; set; } = new();

    public ProjectNode(XElement sourceXml, List<PropertyGroupNode> propertyGroups, List<TargetNode> targets) : base(sourceXml)
    {
        PropertyGroups = propertyGroups;
        Targets = targets;
    }
}
