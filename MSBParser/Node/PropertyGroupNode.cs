using System.Xml.Linq;

namespace MSBParser.Node;
internal class PropertyGroupNode : Node
{
    public List<PropertyNode> Properties { get; }
    
    public PropertyGroupNode(XElement sourceXml, List<PropertyNode> properties) : base(sourceXml)
    {
        Properties = properties;
    }

    public override void AcceptVisitor(INodeVisitor visitor)
    {
        visitor.VisitPropertyGroupNode(this);
    }
}
