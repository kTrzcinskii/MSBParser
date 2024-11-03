using System.Xml.Linq;

namespace MSBParser.Nodes;
internal class TaskNode : Node
{
    public List<OutputNode> Outputs { get; }
    
    public TaskNode(XElement sourceXml, List<ParsingErrorNode> parsingErrors, List<OutputNode> outputs) : base(sourceXml, parsingErrors)
    {
        Outputs = outputs;
    }

    public override void AcceptVisitor(INodeVisitor visitor)
    {
        visitor.VisitTaskNode(this);
    }
}
