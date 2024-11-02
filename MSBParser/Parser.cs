using MSBParser.Nodes;
using System.Xml;
using System.Xml.Linq;

namespace MSBParser;
internal class Parser
{
    private readonly XDocument _content;

    public Parser(string msbuildFile)
    {
        var readerSettings = new XmlReaderSettings
        {
            DtdProcessing = DtdProcessing.Ignore
        };

        using var reader = XmlReader.Create(msbuildFile, readerSettings);
        _content = XDocument.Load(reader, LoadOptions.SetLineInfo);
    }

    public ProjectNode? Parse()
    {
        return _content.Root is { Name.LocalName: TagNames.Project } ? ParseProject(_content.Root) : null;
    }

    private ImportGroupNode ParseImportGroup(XElement importGroupElement)
    {
        var imports = new List<ImportNode>();
        foreach (var element in importGroupElement.Elements())
        {
            switch (element.Name.LocalName)
            {
                case TagNames.Import:
                    var import = ParseImport(element);
                    imports.Add(import);
                    break;
            }
        }
        return new ImportGroupNode(importGroupElement, imports);
    }

    private ImportNode ParseImport(XElement importElement)
    {
        return new ImportNode(importElement);
    }

    private ItemGroupNode ParseItemGroup(XElement itemGroupElement)
    {
        var items = itemGroupElement.Elements().Select(ParseItem).ToList();
        return new ItemGroupNode(itemGroupElement, items);
    }

    private ItemMetadataNode ParseItemMetadata(XElement itemMetadataElement)
    {
        return new ItemMetadataNode(itemMetadataElement);
    }

    private ItemNode ParseItem(XElement itemElement)
    {
        var itemMetadatas = itemElement.Elements().Select(ParseItemMetadata).ToList();
        return new ItemNode(itemElement, itemMetadatas);
    }

    private OnErrorNode ParseOnError(XElement onErrorElement)
    {
        return new OnErrorNode(onErrorElement);
    }
    
    private OutputNode ParseOutput(XElement outputElement)
    {
        return new OutputNode(outputElement);
    }
    
    private ProjectNode ParseProject(XElement projectElement)
    {
        var propertyGroups = new List<PropertyGroupNode>();
        var itemGroups = new List<ItemGroupNode>();
        var targets = new List<TargetNode>();
        var importGroups = new List<ImportGroupNode>();
        var imports = new List<ImportNode>();

        foreach (var element in projectElement.Elements())
        {
            switch (element.Name.LocalName)
            {
                case TagNames.PropertyGroup:
                    var propertyGroup = ParseProperyGroup(element);
                    propertyGroups.Add(propertyGroup);
                    break;
                case TagNames.ItemGroup:
                    var itemGroup = ParseItemGroup(element);
                    itemGroups.Add(itemGroup);
                    break;
                case TagNames.Target:
                    var target = ParseTarget(element);
                    targets.Add(target);
                    break;
                case TagNames.ImportGroup:
                    var importGroup = ParseImportGroup(element);
                    importGroups.Add(importGroup);
                    break;
                case TagNames.Import:
                    var import = ParseImport(element);
                    imports.Add(import);
                    break;
            }
        }

        return new ProjectNode(projectElement, propertyGroups, itemGroups, targets, importGroups, imports);
    }

    private PropertyGroupNode ParseProperyGroup(XElement propertyGroupElement)
    {
        var properties = propertyGroupElement.Elements().Select(ParseProperty).ToList();
        return new PropertyGroupNode(propertyGroupElement, properties);
    }

    private PropertyNode ParseProperty(XElement propertyElement)
    {
        return new PropertyNode(propertyElement);
    }

    private TargetNode ParseTarget(XElement targetElement)
    {
        var propertyGroups = new List<PropertyGroupNode>();
        var itemGroups = new List<ItemGroupNode>();
        var onErrors = new List<OnErrorNode>();
        var tasks = new List<TaskNode>();
        
        foreach (var element in targetElement.Elements())
        {
            switch (element.Name.LocalName)
            {
                case TagNames.PropertyGroup:
                    var propertyGroup = ParseProperyGroup(element);
                    propertyGroups.Add(propertyGroup);
                    break;
                case TagNames.ItemGroup:
                    var itemGroup = ParseItemGroup(element);
                    itemGroups.Add(itemGroup);
                    break;
                case TagNames.OnError:
                    var onError = ParseOnError(element);
                    onErrors.Add(onError);
                    break;
                default:
                    var task = ParseTask(element);
                    tasks.Add(task);
                    break;
            }
        }
        return new TargetNode(targetElement, tasks, propertyGroups, itemGroups, onErrors);
    }

    private TaskNode ParseTask(XElement taskElement)
    {
        var outputs = new List<OutputNode>();
        foreach (var element in taskElement.Elements())
        {
            switch (element.Name.LocalName)
            {
                case TagNames.Output:
                    var output = ParseOutput(element);
                    outputs.Add(output);
                    break;
            }
        }
        return new TaskNode(taskElement, outputs);
    }
}
