using System;
namespace LangLab
{
    [AttributeUsage(AttributeTargets.Class |
                       AttributeTargets.Struct)]
    public class NodeViewAttribute : Attribute
    {
        public Type type;
        public NodeViewAttribute(Type type)
        {
            this.type = type;  
        }
    }
}
