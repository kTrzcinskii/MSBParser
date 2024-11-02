using System.Xml.Linq;

namespace MSBParser.Nodes;

internal class ImportNode : Node
{
    public ImportNode(XElement sourceXml) : base(sourceXml)
    {
    }

    public override void AcceptVisitor(INodeVisitor visitor)
    {
        visitor.VisitImportNode(this);
    }
}