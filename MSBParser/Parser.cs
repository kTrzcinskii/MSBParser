using MSBParser.Node;
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

        using (var reader = XmlReader.Create(msbuildFile, readerSettings))
        {
            _content = XDocument.Load(reader, LoadOptions.SetLineInfo);
        }
    }

    public ProjectNode Parse()
    {
        return ParseProject(_content.Root);
    }

    private ProjectNode ParseProject(XElement projectElement)
    {
        var propertyGroups = new List<PropertyGroupNode>();
        var targets = new List<TargetNode>();

        foreach (var element in projectElement.Elements())
        {
            switch (element.Name.LocalName)
            {
                case TagNames.PropertyGroup:
                    var propertyGroup = ParseProperyGroup(element);
                    propertyGroups.Add(propertyGroup);
                    break;
                case TagNames.Target:
                    var target = ParseTarget(element);
                    targets.Add(target);
                    break;
            }
        }

        return new ProjectNode(projectElement, propertyGroups, targets);
    }

    private PropertyGroupNode ParseProperyGroup(XElement propertyGroupElement)
    {
        return new PropertyGroupNode(propertyGroupElement);
    }

    private TargetNode ParseTarget(XElement targetElement)
    {
        return new TargetNode(targetElement);
    }
}
