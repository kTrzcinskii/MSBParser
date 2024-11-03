namespace MSBParser.Nodes;

internal interface INodeVisitor
{
    public void VisitImportGroupNode(ImportGroupNode importGroupNode);
    public void VisitImportNode(ImportNode importNode);
    public void VisitItemDefinitionGroupNode(ItemDefinitionGroupNode itemDefinitionGroupNode);
    public void VisitItemGroupNode(ItemGroupNode itemGroupNode);
    public void VisitItemMetadataNode(ItemMetadataNode itemMetadataNode);
    public void VisitItemNode(ItemNode itemNode);
    public void VisitOnErrorNode(OnErrorNode onErrorNode);
    public void VisitOutputNode(OutputNode outputNode);
    public void VisitParameterGroupNode(ParameterGroupNode parameterGroupNode);
    public void VisitParameterNode(ParameterNode parameterNode);
    public void VisitParsingErrorNode(ParsingErrorNode parsingErrorNode);
    public void VisitProjectExtensionsNode(ProjectExtensionsNode projectExtensionsNode);
    public void VisitProjectNode(ProjectNode projectNode);
    public void VisitPropertyGroupNode(PropertyGroupNode propertyGroupNode);
    public void VisitPropertyNode(PropertyNode propertyNode);
    public void VisitSdkNode(SdkNode sdkNode);
    public void VisitTargetNode(TargetNode targetNode);
    public void VisitTaskForUsingNode(TaskForUsingNode taskForUsingNode);
    public void VisitTaskNode(TaskNode taskNode);
    public void VisitUsingTaskNode(UsingTaskNode usingTaskNode);
}