using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System.Collections.Generic;
using UnityEditor;

namespace LangLab
{
    [NodeViewAttribute(typeof(NonTerminalNode))]
    public class GrammarNonTerminalView : GrammarNodeView
    {
        Port inputPort;
        List<Port> outputPorts; 
        public override void Initialize(GrammarNodeAsset node)
        {
            base.Initialize(node);

            Button addBranch = new Button()
            {
                text = "Add"
            };
            outputContainer.Add(addBranch);
            inputPort = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Multi, typeof(bool));
            inputPort.name = "Input";
            
            inputContainer.Add(inputPort);
            addBranch.clicked += AddOutput;
            outputPorts = new List<Port>();


        }
        void AddOutput()
        {
            Port outputPort = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Multi, typeof(bool));
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
        void ClearOutputs()
        {
            foreach(var outputPort in outputPorts)
            {
                outputContainer.Remove(outputPort);
            }
            outputPorts = new List<Port>();
        }
        public override void UpdateAssetFromEdges()
        {
            List<List<GrammarNodeAsset>> newChildren = new List<List<GrammarNodeAsset>>();
            foreach (var port in outputPorts)
            {
                List<GrammarNodeAsset> children = new List<GrammarNodeAsset>();
                newChildren.Add(children);
                foreach (var edge in port.connections)
                {
                    var child = edge.input.node as LLNodeView<GrammarNodeAsset>;
                    children.Add(child.nodeAsset);
                }
            }
            (nodeAsset as NonTerminalNode).children = newChildren;
            
        }

        public override Port GetInputPort() => inputPort;

        public override void UpdateEdgesFromAsset(LLGraphView<GrammarNodeAsset> graphView)
        {
            ClearOutputs();
            var nonTerminal = nodeAsset as NonTerminalNode;
            if (nonTerminal.children == null) return;
            for(int i = 0;i<nonTerminal.children.Count;i++)
            {
                List<GrammarNodeAsset> childList = nonTerminal.children[i];
                AddOutput();
                Port outputPort = outputPorts[i];
                foreach(var child in childList)
                {
                    LLNodeView<GrammarNodeAsset> nodeView = child.nodeView;
                    if (nodeView is GrammarNodeView grammarNodeView)
                    {
                        Edge edge = outputPort.ConnectTo(grammarNodeView.GetInputPort());
                        graphView.AddElement(edge);
                    }
                }
            }
        }
    }
}
