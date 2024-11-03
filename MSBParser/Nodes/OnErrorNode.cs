﻿using System.Xml.Linq;

namespace MSBParser.Nodes;

internal class OnErrorNode : Node
{
    public OnErrorNode(XElement sourceXml, List<ParsingErrorNode> parsingErrors) : base(sourceXml, parsingErrors)
    {
    }

    public override void AcceptVisitor(INodeVisitor visitor)
    {
        visitor.VisitOnErrorNode(this);
    }
}