using System.Xml.Linq;

namespace MSBParser.Node;
internal class TargetNode : Node
{
    public List<TaskNode> Tasks { get; }
    public List<PropertyGroupNode> PropertyGroups { get; }
    public List<ItemGroupNode> ItemGroups { get; }
    public List<OnErrorNode> OnErrors { get; }
    
    public TargetNode(XElement sourceXml, List<TaskNode> tasks, List<PropertyGroupNode> propertyGroups, List<ItemGroupNode> itemGroups, List<OnErrorNode> onErrors) : base(sourceXml)
    {
        Tasks = tasks;
        PropertyGroups = propertyGroups;
        ItemGroups = itemGroups;
        OnErrors = onErrors;
    }

    public override void AcceptVisitor(INodeVisitor visitor)
    {
        visitor.VisitTargetNode(this);
    }
}
