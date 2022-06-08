using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using System.Reflection;
using UnityEngine;

namespace LangLab
{
    public class CompilationNodeView : LLNodeView<CompilationNodeAsset>
    {
        List<Port> outputPorts;
        List<Port> inputPorts;
        public override void Initialize(CompilationNodeAsset node)
        {
            base.Initialize(node);
            Type nodeType = node.GetType();
            inputPorts = new List<Port>();
            outputPorts = new List<Port>();
            foreach (var field in nodeType.GetFields(BindingFlags.Instance | BindingFlags.Public))
            {
                if (Attribute.IsDefined(field, typeof(CreateInputPort)))
                {
                    var inputPort = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(bool));
                    inputPort.name = field.Name;
                    inputPort.portColor = Color.cyan;
                    inputPorts.Add(inputPort);
                    inputContainer.Add(inputPort);
                }
                if (Attribute.IsDefined(field, typeof(CreateOutputPort)))
                {
                    var outputPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(bool));
                    outputPort.name = field.Name;
                    outputPort.portColor = Color.cyan;
                    outputPorts.Add(outputPort);
                    outputContainer.Add(outputPort);
                }
            }
        }
        public override void UpdateAssetFromEdges(Edge edge)
        {
            List<CompilationNodeAsset> newChildren = new List<CompilationNodeAsset>();
            foreach(var outputPort in outputPorts)
            {
                foreach (var e in outputPort.connections)
                {
                    var other = (e.input.node as CompilationNodeView);
                    var llinputPort = other.TryGetLLInputPort(e.input);
                    var lloutputPort = TryGetLLOutputPort(outputPort);
                    if (!llinputPort.connectedTo.Contains(lloutputPort))
                        llinputPort.connectedTo.Add(lloutputPort);
                }
            }
            
            if (edge != null)
            {
                var other = (edge.input.node as CompilationNodeView);
                var llinputPort = other.TryGetLLInputPort(edge.input);
                var lloutputPort = TryGetLLOutputPort(edge.output);
                if (!llinputPort.connectedTo.Contains(lloutputPort))
                    llinputPort.connectedTo.Add(lloutputPort);
            }
        }
        public LLInputPort TryGetLLInputPort(Port port)
        {
            if (nodeAsset.inputPorts == null) nodeAsset.inputPorts = new List<LLInputPort>();
            foreach (var inputPort in nodeAsset.inputPorts)
            {
                if(inputPort.fieldName == port.name)
                {
                    return inputPort;
                }
            }
            LLInputPort newInputPort = new LLInputPort(port.name, nodeAsset);
            nodeAsset.inputPorts.Add(newInputPort);
            return newInputPort;
        }
        public LLOutputPort TryGetLLOutputPort(Port port)
        {
            if (nodeAsset.outputPorts == null) nodeAsset.outputPorts = new List<LLOutputPort>();
            foreach (var outputPort in nodeAsset.outputPorts)
            {
                if (outputPort.fieldName == port.name)
                {
                    return outputPort;
                }
            }
            LLOutputPort newOutputPort = new LLOutputPort(port.name, nodeAsset);
            nodeAsset.outputPorts.Add(newOutputPort);
            return newOutputPort;
        }
        public override void UpdateEdgesFromAsset(LLGraphView<CompilationNodeAsset> graphView)
        {

        }
    }
}
