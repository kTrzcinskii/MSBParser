﻿using System.Xml.Linq;

namespace MSBParser.Nodes;
internal class ItemGroupNode : Node
{
    public List<ItemNode> Items { get; }
    
    public ItemGroupNode(XElement sourceXml, List<ItemNode> items) : base(sourceXml)
    {
        Items = items;
    }

    public override void AcceptVisitor(INodeVisitor visitor)
    {
        visitor.VisitItemGroupNode(this);
    }
}