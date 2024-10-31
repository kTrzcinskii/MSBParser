using System.Xml.Linq;

namespace MSBParser.Node;
internal class TaskNode : Node
{
    public TaskNode(XElement sourceXml) : base(sourceXml)
    {
    }
}
