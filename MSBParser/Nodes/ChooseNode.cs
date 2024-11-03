using System.Xml.Linq;

namespace MSBParser.Nodes;

internal class ChooseNode : Node
{
    // Should always contain at least one WhenNode element - must be checked in parser
    public List<WhenNode> Whens { get; }
    public OtherwiseNode? Otherwise { get; }
    
    public ChooseNode(XElement sourceXml, List<ParsingErrorNode> parsingErrors, List<WhenNode> whens, OtherwiseNode? otherwise) : base(sourceXml, parsingErrors)
    {
        Whens = whens;
        Otherwise = otherwise;
    }

    public override void AcceptVisitor(INodeVisitor visitor)
    {
        visitor.VisitChooseNode(this);
    }
}