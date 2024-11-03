using System.Xml.Linq;

namespace MSBParser.Nodes;

internal class UsingTaskNode : Node
{
    public List<ParameterGroupNode> ParameterGroups { get; }
    public List<TaskForUsingNode> Tasks { get; }
    
    public UsingTaskNode(XElement sourceXml, List<ParsingErrorNode> parsingErrors, List<ParameterGroupNode> parameterGroups, List<TaskForUsingNode> tasks) : base(sourceXml, parsingErrors)
    {
        ParameterGroups = parameterGroups;
        Tasks = tasks;
    }

    public override void AcceptVisitor(INodeVisitor visitor)
    {
        visitor.VisitUsingTaskNode(this);
    }
}