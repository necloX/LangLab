using System.Collections.Generic;
using System.Reflection;

namespace LangLab
{
    public abstract class CompilationNodeAsset : NodeAsset<CompilationNodeAsset>
    {
        public List<LLInputPort> inputPorts;
        public List<LLOutputPort> outputPorts;
        public virtual void GoThrough()
        {
            foreach(var outputPort in outputPorts)
            {
                foreach(var inputPort in outputPort.connectedTo)
                {
                    foreach (var inputField in inputPort.owner.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public))
                    {
                        foreach (var outputField in outputPort.owner.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public))
                        {
                            if (inputField.Name == inputPort.fieldName && outputField.Name == outputPort.fieldName)
                            {
                                inputField.SetValue(inputPort.owner,outputField);
                            }
                        }
                    }
                }
            }
        }
    }
    public abstract class LLPort
    {
        public CompilationNodeAsset owner;
        public List<LLOutputPort> connectedTo;
        public string fieldName;
        public LLPort(string name,CompilationNodeAsset owner)
        {
            fieldName = name;
            connectedTo = new List<LLOutputPort>();
            this.owner = owner;
        } 
    }
    public class LLInputPort : LLPort
    {
        public LLInputPort(string name, CompilationNodeAsset owner) : base(name, owner) { }
    }
    public class LLOutputPort : LLPort
    {
        public LLOutputPort(string name, CompilationNodeAsset owner) : base(name, owner) { }
    }
}