using System.Xml.Linq;

namespace MSBParser.Nodes;

internal class TaskForUsingNode : Node
{
    public TaskForUsingNode(XElement sourceXml, List<ParsingErrorNode> parsingErrors) : base(sourceXml, parsingErrors)
    {
    }

    public override void AcceptVisitor(INodeVisitor visitor)
    {
        visitor.VisitTaskForUsingNode(this);
    }
}