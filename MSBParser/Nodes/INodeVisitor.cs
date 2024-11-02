namespace MSBParser.Nodes;

internal interface INodeVisitor
{
    public void VisitImportGroupNode(ImportGroupNode importGroupNode);
    public void VisitImportNode(ImportNode importNode);
    public void VisitItemGroupNode(ItemGroupNode itemGroupNode);
    public void VisitItemMetadataNode(ItemMetadataNode itemMetadataNode);
    public void VisitItemNode(ItemNode itemNode);
    public void VisitOnErrorNode(OnErrorNode onErrorNode);
    public void VisitOutputNode(OutputNode outputNode);
    public void VisitProjectNode(ProjectNode projectNode);
    public void VisitPropertyGroupNode(PropertyGroupNode propertyGroupNode);
    public void VisitPropertyNode(PropertyNode propertyNode);
    public void VisitTargetNode(TargetNode targetNode);
    public void VisitTaskNode(TaskNode taskNode);
}