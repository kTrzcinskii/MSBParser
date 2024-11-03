using System.Xml.Linq;

namespace MSBParser.Nodes;

internal class ImportGroupNode : Node
{
    public List<ImportNode> Imports { get; }
    
    public ImportGroupNode(XElement sourceXml, List<ParsingErrorNode> parsingErrors, List<ImportNode> imports) : base(sourceXml, parsingErrors)
    {
        Imports = imports;
    }

    public override void AcceptVisitor(INodeVisitor visitor)
    {
        visitor.VisitImportGroupNode(this);
    }
}