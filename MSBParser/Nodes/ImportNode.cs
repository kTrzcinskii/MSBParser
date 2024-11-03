using System.Xml.Linq;

namespace MSBParser.Nodes;

internal class ImportNode : Node
{
    public ImportNode(XElement sourceXml, List<ParsingErrorNode> parsingErrors) : base(sourceXml, parsingErrors)
    {
    }

    public override void AcceptVisitor(INodeVisitor visitor)
    {
        visitor.VisitImportNode(this);
    }
}