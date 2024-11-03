using System.Xml.Linq;

namespace MSBParser.Nodes;

internal class ParsingErrorNode : Node
{
    public string Message { get; }
    
    public ParsingErrorNode(XElement sourceXml, string message) : base(sourceXml, [])
    {
        Message = message;
    }

    public override void AcceptVisitor(INodeVisitor visitor)
    {
        visitor.VisitParsingErrorNode(this);
    }
}