using System.Xml.Linq;

namespace MSBParser.Nodes;

internal class ProjectExtensionsNode : Node
{
    public ProjectExtensionsNode(XElement sourceXml, List<ParsingErrorNode> parsingErrors) : base(sourceXml, parsingErrors)
    {
    }

    public override void AcceptVisitor(INodeVisitor visitor)
    {
        visitor.VisitProjectExtensionsNode(this);
    }
}