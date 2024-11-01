using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using MSBParser.Node;

namespace MSBParser;

internal class SyntaxHighlighter : INodeVisitor
{
    private RichTextBox _textBox;
    private TextRange _fullRange;
    private const int StartOffset = 4;
    private PriorityQueue<ApplyHighlight, int> _higlightQueue = new();

    public SyntaxHighlighter(RichTextBox richTextBox)
    {
        _textBox = richTextBox;
        _fullRange = new TextRange(_textBox.Document.ContentStart, _textBox.Document.ContentEnd);
    }

    private void HighlightList(List<Node.Node> nodes)
    {
        foreach (var node in nodes)
        {
            node.AcceptVisitor(this);
        }
    }

    private void AddToQueue(ApplyHighlight ap, int position)
    {
        // -position because we want to get max first
        _higlightQueue.Enqueue(ap, -position);
    }
    
    private void QueueAddOpeningTagHighlight(Node.Node node)
    {
        string name = node.SourceXml.Name.LocalName;
        int position = node.StartPosition + StartOffset;
        var start = _textBox.Document.ContentStart.GetPositionAtOffset(position, LogicalDirection.Forward);
        var end = start?.GetPositionAtOffset(name.Length, LogicalDirection.Forward);
        if (start == null || end == null)
        {
            return;
        }
        var tagRange = new TextRange(start, end);
        var ap = new ApplyHighlight(() => tagRange.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Blue));
        AddToQueue(ap, position);
    }
    
    private void QueueAddClosingTagHighlight(Node.Node node)
    {
        if (node.EndPosition == null)
        {
            return;
        }
        string name = node.SourceXml.Name.LocalName;
        var position = node.EndPosition.Value + StartOffset;
        var start = _textBox.Document.ContentStart.GetPositionAtOffset(position, LogicalDirection.Forward);
        var end = start?.GetPositionAtOffset(name.Length, LogicalDirection.Forward);
        if (start == null || end == null)
        {
            return;
        }
        var tagRange = new TextRange(start, end);
        var ap = new ApplyHighlight(() => tagRange.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Blue));
        AddToQueue(ap, position);
    }
    
    public void HighlightContent(ProjectNode project)
    {
        PrepareQueue(project);
        ApplyAllHighlights();
    }

    private void PrepareQueue(ProjectNode project)
    {
        project.AcceptVisitor(this);    
    }

    private void ApplyAllHighlights()
    {
        _fullRange.ClearAllProperties();
        while (_higlightQueue.Count > 0)
        {
            var ap = _higlightQueue.Dequeue();
            ap.Run();
        }
    }
    
    public void VisitItemGroupNode(ItemGroupNode itemGroupNode)
    {
        throw new NotImplementedException();
    }

    public void VisitItemMetadataNode(ItemMetadataNode itemMetadataNode)
    {
        throw new NotImplementedException();
    }

    public void VisitItemNode(ItemNode itemNode)
    {
        throw new NotImplementedException();
    }

    public void VisitOnErrorNode(OnErrorNode onErrorNode)
    {
        throw new NotImplementedException();
    }

    public void VisitOutputNode(OutputNode outputNode)
    {
        throw new NotImplementedException();
    }

    public void VisitProjectNode(ProjectNode projectNode)
    {
        QueueAddOpeningTagHighlight(projectNode);
        QueueAddClosingTagHighlight(projectNode);
        HighlightList(projectNode.PropertyGroups.Cast<Node.Node>().ToList());
    }

    public void VisitPropertyGroupNode(PropertyGroupNode propertyGroupNode)
    {
        QueueAddOpeningTagHighlight(propertyGroupNode);
        QueueAddClosingTagHighlight(propertyGroupNode);
        HighlightList(propertyGroupNode.Properties.Cast<Node.Node>().ToList());
    }

    public void VisitPropertyNode(PropertyNode propertyNode)
    {
        QueueAddOpeningTagHighlight(propertyNode);
        QueueAddClosingTagHighlight(propertyNode);
    }

    public void VisitTargetNode(TargetNode targetNode)
    {
        throw new NotImplementedException();
    }

    public void VisitTaskNode(TaskNode taskNode)
    {
        throw new NotImplementedException();
    }
}