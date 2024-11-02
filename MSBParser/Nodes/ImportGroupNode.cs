using System.Xml.Linq;

namespace MSBParser.Nodes;

internal class ImportGroupNode : Node
{
    public List<ImportNode> Imports { get; }
    
    public ImportGroupNode(XElement sourceXml, List<ImportNode> imports) : base(sourceXml)
    {
        Imports = imports;
    }

    public override void AcceptVisitor(INodeVisitor visitor)
    {
        visitor.VisitImportGroupNode(this);
    }
}