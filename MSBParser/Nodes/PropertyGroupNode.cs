using System.Xml.Linq;

namespace MSBParser.Nodes;
internal class PropertyGroupNode : Node
{
    public List<PropertyNode> Properties { get; }
    
    public PropertyGroupNode(XElement sourceXml, List<ParsingErrorNode> parsingErrors, List<PropertyNode> properties) : base(sourceXml, parsingErrors)
    {
        Properties = properties;
    }

    public override void AcceptVisitor(INodeVisitor visitor)
    {
        visitor.VisitPropertyGroupNode(this);
    }
}
