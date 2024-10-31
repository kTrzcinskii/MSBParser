using System.Xml.Linq;

namespace MSBParser.Node;
internal class TaskNode : Node
{
    public List<OutputNode> Outputs { get; }
    
    public TaskNode(XElement sourceXml, List<OutputNode> outputs) : base(sourceXml)
    {
        Outputs = outputs;
    }

    public override void AcceptVisitor(INodeVisitor visitor)
    {
        visitor.VisitTaskNode(this);
    }
}
