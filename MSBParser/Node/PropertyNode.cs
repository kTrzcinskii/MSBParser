﻿using System.Xml.Linq;

namespace MSBParser.Node;
internal class PropertyNode : Node
{
    public PropertyNode(XElement sourceXml) : base(sourceXml)
    {
    }
}