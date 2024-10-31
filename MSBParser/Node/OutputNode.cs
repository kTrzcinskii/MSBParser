﻿using System.Xml.Linq;

namespace MSBParser.Node;

internal class OutputNode : Node
{
    public OutputNode(XElement sourceXml) : base(sourceXml)
    {
    }

    public override void AcceptVisitor(INodeVisitor visitor)
    {
        visitor.VisitOutputNode(this);
    }
}