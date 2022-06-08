using System;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;

namespace LangLab
{
    [NodeViewAttribute(typeof(MatchingGraphNode))]
    public class MatchingNodeView : LLNodeView<MatchingGraphNode>
    {
        List<Port> outputPorts;
        Port inputPort;
        public override void Initialize(MatchingGraphNode node)
        {
            base.Initialize(node);
            if(!(nodeAsset as MatchingGraphNode).IsRoot)
            {
                inputPort = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
                inputPort.name = "Input";
                inputContainer.Add(inputPort);
            }
            
            mainContainer.Insert(0, inputContainer);
            Button addBranch = new Button()
            {
                text = "Add"
            };
            TextField nodeName = new TextField()
            {
                value = node.name,
            };
            nodeName.RegisterCallback<ChangeEvent<string>>(evt => nodeAsset.name = nodeName.value);
            titleContainer.Clear();

            titleContainer.Insert(0, nodeName);
            outputContainer.Add(addBranch);
            outputPorts = new List<Port>();
            addBranch.clicked += AddOutput;
        }
        void ClearOutputs()
        {
            foreach (var outputPort in outputPorts)
            {
                outputContainer.Remove(outputPort);
            }
            outputPorts = new List<Port>();
        }
        public override void UpdateAssetFromEdges(Edge edge)
        {
            List<MatchingGraphNode> newChildren = new List<MatchingGraphNode>();
            foreach(var port in outputPorts)
            {
                foreach(var e in port.connections)
                {
                    newChildren.Add((e.input.node as LLNodeView<MatchingGraphNode>).nodeAsset);
                }
            }
            if(edge != null)
            {
                if (edge.input.node != this) newChildren.Add((edge.input.node as LLNodeView<MatchingGraphNode>).nodeAsset);
            }
            (nodeAsset as MatchingGraphNode).children = newChildren;
        }

        public override void UpdateEdgesFromAsset(LLGraphView<MatchingGraphNode> graphView)
        {
            ClearOutputs();
            var matchinNode = nodeAsset as MatchingGraphNode;
            if (matchinNode.children == null) return;
            for (int i = 0; i < matchinNode.children.Count; i++)
            {
                AddOutput();
                Port outputPort = outputPorts[i];
                Edge edge = outputPort.ConnectTo((matchinNode.children[i].nodeView as MatchingNodeView).GetInputPort());
                graphView.AddElement(edge);
            }
        }

        private Port GetInputPort()
        {
            if((nodeAsset as MatchingGraphNode).IsRoot)
            {
                throw new Exception("There is no input on a root node");
            }
            return inputPort;
        }
        void AddOutput()
        {
            Port outputPort = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(bool));
            outputPort.name = "";
            outputPorts.Add(outputPort);
            outputContainer.Add(outputPort);
            Button deleteBranchButton = new Button()
            {
                text = "X"
            };
            outputPort.Add(deleteBranchButton);
            deleteBranchButton.clicked += () =>
            {
                outputPorts.Remove(outputPort);
                outputContainer.Remove(outputPort);
            };
        }
    }
}
