﻿using MSBParser.Nodes;
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
        var parsingErrors = new List<ParsingErrorNode>();
        foreach (var element in importGroupElement.Elements())
        {
            switch (element.Name.LocalName)
            {
                case TagNames.Import:
                    var import = ParseImport(element);
                    imports.Add(import);
                    break;
                default:
                    var parsingError = CreateParsingErrorNode(element,
                        $"Unexpected tag {element.Name.LocalName} in 'ImportGroup'. Expects 'Import' tag");
                    parsingErrors.Add(parsingError);
                    break;
            }
        }
        return new ImportGroupNode(importGroupElement, parsingErrors, imports);
    }

    private ImportNode ParseImport(XElement importElement)
    {
        var parsingErrors = AllChildrenToParsingErrors(importElement, "Import");
        return new ImportNode(importElement, parsingErrors);
    }

    private ItemDefinitionGroupNode ParseItemDefinitionGroup(XElement itemDefinitionGroupElement)
    {
        var items = itemDefinitionGroupElement.Elements().Select(ParseItem).ToList();
        return new ItemDefinitionGroupNode(itemDefinitionGroupElement, [], items);
    }
    
    private ItemGroupNode ParseItemGroup(XElement itemGroupElement)
    {
        var items = itemGroupElement.Elements().Select(ParseItem).ToList();
        return new ItemGroupNode(itemGroupElement, [], items);
    }

    private ItemMetadataNode ParseItemMetadata(XElement itemMetadataElement)
    {
        var parsingErrors = AllChildrenToParsingErrors(itemMetadataElement, "ItemMetadata");
        return new ItemMetadataNode(itemMetadataElement, parsingErrors);
    }

    private ItemNode ParseItem(XElement itemElement)
    {
        var itemMetadatas = itemElement.Elements().Select(ParseItemMetadata).ToList();
        return new ItemNode(itemElement, [], itemMetadatas);
    }

    private OnErrorNode ParseOnError(XElement onErrorElement)
    {
        var parsingErrors = AllChildrenToParsingErrors(onErrorElement, "OnError");
        return new OnErrorNode(onErrorElement, parsingErrors);
    }
    
    private OutputNode ParseOutput(XElement outputElement)
    {
        var parsingErrors = AllChildrenToParsingErrors(outputElement, "Output");
        return new OutputNode(outputElement, parsingErrors);
    }

    private ParameterGroupNode ParseParameterGroup(XElement parameterGroupElement)
    {
        var parameters = parameterGroupElement.Elements().Select(ParseParameter).ToList();
        return new ParameterGroupNode(parameterGroupElement, [], parameters);
    }
    
    private ParameterNode ParseParameter(XElement parameterElement)
    {
        var parsingErrors = AllChildrenToParsingErrors(parameterElement, "Parameter");
        return new ParameterNode(parameterElement, parsingErrors);
    }

    private ParsingErrorNode CreateParsingErrorNode(XElement parsingErrorElement, string message)
    {
        return new ParsingErrorNode(parsingErrorElement, message);
    }
    
    private ProjectNode ParseProject(XElement projectElement)
    {
        var propertyGroups = new List<PropertyGroupNode>();
        var itemGroups = new List<ItemGroupNode>();
        var targets = new List<TargetNode>();
        var importGroups = new List<ImportGroupNode>();
        var imports = new List<ImportNode>();
        var itemDefinitionGroups = new List<ItemDefinitionGroupNode>();
        var parsingErrors = new List<ParsingErrorNode>();

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
                case TagNames.ItemDefinitionGroup:
                    var itemDefintionGroup = ParseItemDefinitionGroup(element);
                    itemDefinitionGroups.Add(itemDefintionGroup);
                    break;
                default:
                    var parsingError = CreateParsingErrorNode(element, $"Unexpected tag {element.Name.LocalName
                    } in 'Project' tag.");
                    parsingErrors.Add(parsingError);
                    break;
            }
        }

        return new ProjectNode(projectElement, parsingErrors, propertyGroups, itemGroups, targets, importGroups, imports, itemDefinitionGroups);
    }

    private PropertyGroupNode ParseProperyGroup(XElement propertyGroupElement)
    {
        var properties = propertyGroupElement.Elements().Select(ParseProperty).ToList();
        return new PropertyGroupNode(propertyGroupElement, [], properties);
    }

    private PropertyNode ParseProperty(XElement propertyElement)
    {
        var parsingErrors = AllChildrenToParsingErrors(propertyElement, "Property");
        return new PropertyNode(propertyElement, parsingErrors);
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
        return new TargetNode(targetElement, [], tasks, propertyGroups, itemGroups, onErrors);
    }

    private TaskNode ParseTask(XElement taskElement)
    {
        var outputs = new List<OutputNode>();
        var parsingErrors = new List<ParsingErrorNode>();
        foreach (var element in taskElement.Elements())
        {
            switch (element.Name.LocalName)
            {
                case TagNames.Output:
                    var output = ParseOutput(element);
                    outputs.Add(output);
                    break;
                default:
                    var parsingError = CreateParsingErrorNode(element, $"Unexpected tag {element.Name.LocalName} in 'Task'. Expected 'Output' tag.");
                    parsingErrors.Add(parsingError);
                    break;
            }
        }
        return new TaskNode(taskElement, parsingErrors, outputs);
    }

    private List<ParsingErrorNode> AllChildrenToParsingErrors(XElement element, string tagName)
    {
        var parsingErrors = element.Elements().Select((el) =>
            CreateParsingErrorNode(el, $"Unexpected tag in {tagName}. {tagName} should not have any child.")).ToList();
        return parsingErrors;
    }
}
