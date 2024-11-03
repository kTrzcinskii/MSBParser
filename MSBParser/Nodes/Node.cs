using System.Xml;
using System.Xml.Linq;

namespace MSBParser.Nodes;
internal abstract class Node
{
    public XElement SourceXml { get; }
    public int StartPosition { get; }
    public int? EndPosition { get; }
    public List<ParsingErrorNode> ParsingErrors { get; }

    public Node(XElement sourceXml, List<ParsingErrorNode> parsingErrors)
    {
        SourceXml = sourceXml;
        ParsingErrors = parsingErrors;
        // should throw some exception here
        if (SourceXml is not IXmlLineInfo lineInfo || !lineInfo.HasLineInfo())
            return;
        int line = lineInfo.LineNumber - 1;
        int position = lineInfo.LinePosition - 2;
        StartPosition = LineAndPositionToAbsolutePosition(sourceXml, line, position) + 1; // +1 for <
        
        var (closeLine, closePosition) = CalculateClosingLineAndPosition(sourceXml, line);
        if (closeLine != null && closePosition != null)
        {
            EndPosition = LineAndPositionToAbsolutePosition(sourceXml, closeLine.Value, closePosition.Value) + 2; // + 2 for </
        }
    }

    private (int?, int?) CalculateClosingLineAndPosition(XElement sourceXml, int startLine)
    {
        string elementString = SourceXml.ToString(SaveOptions.DisableFormatting);
        bool isSelfClosing = elementString.TrimEnd().EndsWith("/>");
        if (isSelfClosing)
        {
            return (null, null);
        }

        var closingStart = $"</{sourceXml.Name.LocalName}";

        var linesCount = elementString.Split(Environment.NewLine).Length;
        var documentLines = SourceXml.Document!.ToString(SaveOptions.DisableFormatting).Split(Environment.NewLine);

        var closingLine = startLine + linesCount - 1;
        var closingPosition = documentLines[closingLine].IndexOf(closingStart, StringComparison.Ordinal);
        return (closingLine,  closingPosition);
    }

    public static int LineAndPositionToAbsolutePosition(XObject sourceXml, int line, int position)
    {
        var lines = sourceXml.Document!.ToString(SaveOptions.DisableFormatting).Split(Environment.NewLine);
        int id = 0;
        for (int i = 0; i < line; i++)
        {
            id += lines[i].Length + Environment.NewLine.Length;
        }
        id += position;
        return id;
    }

    public abstract void AcceptVisitor(INodeVisitor visitor);
}
