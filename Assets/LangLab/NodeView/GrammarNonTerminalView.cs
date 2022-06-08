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
            inputPort.portColor = Color.green;
            inputContainer.Add(inputPort);
            outputPorts = new List<Port>();
            addBranch.clicked += AddOutput;
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
            outputPort.portColor = Color.green;
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
        public override void UpdateAssetFromEdges(Edge edge)
        {
            List<ListGrammarNode> newChildren = new List<ListGrammarNode>();
            foreach (var port in outputPorts)
            {
                ListGrammarNode children = new ListGrammarNode();
                newChildren.Add(children);
                foreach (var e in port.connections)
                {
                    var child = e.input.node as LLNodeView<GrammarNodeAsset>;
                    children.Add(child.nodeAsset);
                }
                if(edge != null)
                {
                    if (edge.input.node != this)
                    {
                        var newchild = edge.input.node as LLNodeView<GrammarNodeAsset>;
                        children.Add(newchild.nodeAsset);
                    }
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
