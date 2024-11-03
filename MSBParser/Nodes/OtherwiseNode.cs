﻿using System.Xml.Linq;

namespace MSBParser.Nodes;

internal class OtherwiseNode : Node
{
    public List<ChooseNode> Chooses { get; }
    public List<ItemGroupNode> ItemGroups { get; }
    public List<PropertyGroupNode> PropertyGroups { get; }

    public OtherwiseNode(XElement sourceXml, List<ParsingErrorNode> parsingErrors, List<ChooseNode> chooses, List<ItemGroupNode> itemGroups, List<PropertyGroupNode> propertyGroups) : base(sourceXml, parsingErrors)
    {
        Chooses = chooses;
        ItemGroups = itemGroups;
        PropertyGroups = propertyGroups;
    }

    public override void AcceptVisitor(INodeVisitor visitor)
    {
        visitor.VisitOtherwiseNode(this);
    }
}