using System.Xml;
using System.Xml.Linq;

namespace MSBParser.Node;
internal abstract class Node
{
    public XElement SourceXml { get; }
    public int? Line { get; }
    public int? Position { get; }

    public Node(XElement sourceXml)
    {
        SourceXml = sourceXml;
        if (SourceXml is IXmlLineInfo lineInfo && lineInfo.HasLineInfo())
        {
            Line = lineInfo.LineNumber;
            Position = lineInfo.LinePosition;
        }
    }
}
