using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Xml;
using System.Xml.Linq;
using MSBParser.Nodes;

namespace MSBParser;

internal class SyntaxHighlighter : INodeVisitor
{
    private RichTextBox _textBox;
    private PriorityQueue<ApplyHighlight, int> _higlightQueue = new();
    private const int StartOffset = 4;

    public SyntaxHighlighter(RichTextBox richTextBox)
    {
        _textBox = richTextBox;
    }

    private void HighlightList(List<Node> nodes)
    {
        foreach (var node in nodes)
        {
            node.AcceptVisitor(this);
        }
    }

    private void HighlightNode(Node node)
    {
        QueueAddOpeningTagHighlight(node);
        QueueAddClosingTagHighlight(node);
        QueueAddAttributesHighlight(node);
    }

    private void AddToQueue(ApplyHighlight ap, int position)
    {
        // -position because we want to get max first
        _higlightQueue.Enqueue(ap, -position);
    }
    
    private void QueueAddOpeningTagHighlight(Node node)
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
    
    private void QueueAddClosingTagHighlight(Node node)
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

    private void QueueAddAttributesHighlight(Node node)
    {
        foreach (var attribute in node.SourceXml.Attributes())
        {
            if (attribute is not IXmlLineInfo attrLineInfo || !attrLineInfo.HasLineInfo()) 
                continue;
            int line = attrLineInfo.LineNumber - 1;
            int position = attrLineInfo.LinePosition - 1;
            int absolutePosition = Node.LineAndPositionToAbsolutePosition(attribute, line, position);
            QueueAddAttributeHighlight(attribute, absolutePosition);
        }
    }

    private void QueueAddAttributeHighlight(XAttribute attribute, int absolutePosition)
    {
        string name = attribute.Name.LocalName;
        int namePosition = absolutePosition + StartOffset;
        var nameStart = _textBox.Document.ContentStart.GetPositionAtOffset(namePosition, LogicalDirection.Forward);
        var nameEnd = nameStart?.GetPositionAtOffset(name.Length, LogicalDirection.Forward);
        if (nameStart == null || nameEnd == null)
        {
            return;
        }
        var attributeNameRange = new TextRange(nameStart, nameEnd);
        var apName = new ApplyHighlight(() =>
            attributeNameRange.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.MediumSeaGreen));
        AddToQueue(apName, namePosition);

        if (attribute.Value == "")
        {
            return;
        }

        string value = attribute.Value;
        int valuePosition = namePosition + name.Length + 1 + 1; // +1 for '=' and + 1 for '"'
        var valueStart = _textBox.Document.ContentStart.GetPositionAtOffset(valuePosition, LogicalDirection.Forward);
        var valueEnd = valueStart?.GetPositionAtOffset(value.Length, LogicalDirection.Forward);
        if (valueStart == null || valueEnd == null)
        {
            return;
        }
        var attributeValueRange = new TextRange(valueStart, valueEnd);
        var apValue = new ApplyHighlight(() => attributeValueRange.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Goldenrod));
        AddToQueue(apValue, valuePosition);
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
        new TextRange(_textBox.Document.ContentStart, _textBox.Document.ContentEnd).ClearAllProperties();
        while (_higlightQueue.Count > 0)
        {
            var ap = _higlightQueue.Dequeue();
            ap.Run();
        }
    }

    public void VisitImportGroupNode(ImportGroupNode importGroupNode)
    {
        HighlightNode(importGroupNode);
        HighlightList(importGroupNode.Imports.Cast<Node>().ToList());
    }

    public void VisitImportNode(ImportNode importNode)
    {
        HighlightNode(importNode);
    }

    public void VisitItemGroupNode(ItemGroupNode itemGroupNode)
    {
        HighlightNode(itemGroupNode);
        HighlightList(itemGroupNode.Items.Cast<Node>().ToList());
    }

    public void VisitItemMetadataNode(ItemMetadataNode itemMetadataNode)
    {
        HighlightNode(itemMetadataNode);
    }

    public void VisitItemNode(ItemNode itemNode)
    {
        HighlightNode(itemNode);
        HighlightList(itemNode.ItemMetadatas.Cast<Node>().ToList());
    }

    public void VisitOnErrorNode(OnErrorNode onErrorNode)
    {
        HighlightNode(onErrorNode);
    }

    public void VisitOutputNode(OutputNode outputNode)
    {
        HighlightNode(outputNode);
    }

    public void VisitProjectNode(ProjectNode projectNode)
    {
        HighlightNode(projectNode);
        HighlightList(projectNode.PropertyGroups.Cast<Node>().ToList());
        HighlightList(projectNode.ItemGroups.Cast<Node>().ToList());
        HighlightList(projectNode.Targets.Cast<Node>().ToList());
        HighlightList(projectNode.ImportGroups.Cast<Node>().ToList());
        HighlightList(projectNode.Imports.Cast<Node>().ToList());
    }

    public void VisitPropertyGroupNode(PropertyGroupNode propertyGroupNode)
    {
        HighlightNode(propertyGroupNode);
        HighlightList(propertyGroupNode.Properties.Cast<Node>().ToList());
    }

    public void VisitPropertyNode(PropertyNode propertyNode)
    {
        HighlightNode(propertyNode);
    }

    public void VisitTargetNode(TargetNode targetNode)
    {
        HighlightNode(targetNode);
        HighlightList(targetNode.Tasks.Cast<Node>().ToList());
        HighlightList(targetNode.PropertyGroups.Cast<Node>().ToList());
        HighlightList(targetNode.ItemGroups.Cast<Node>().ToList());
        HighlightList(targetNode.OnErrors.Cast<Node>().ToList());
    }

    public void VisitTaskNode(TaskNode taskNode)
    {
        HighlightNode(taskNode);
        HighlightList(taskNode.Outputs.Cast<Node>().ToList());
    }
}