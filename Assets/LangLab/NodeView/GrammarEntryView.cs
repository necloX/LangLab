using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace LangLab
{
    [NodeViewAttribute(typeof(EntryNodeAsset))]
    public class GrammarEntryView : GrammarNodeView
    {
        Port outputPort;

        public override Port GetInputPort()
        {
            throw new System.Exception("There is no input port for an entry node");
        }
        public override void Initialize(GrammarNodeAsset node)
        {
            base.Initialize(node);
            outputPort = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Multi, typeof(bool));
            outputPort.name = "";
            outputPort.portColor = Color.blue;
            outputContainer.Add(outputPort);
        }
        public override void UpdateAssetFromEdges(Edge edge)
        {
            List<GrammarNodeAsset> newChildren = new List<GrammarNodeAsset>();
            foreach (var e in outputPort.connections)
            {
                newChildren.Add((e.input.node as LLNodeView<GrammarNodeAsset>).nodeAsset);
            }
            newChildren.Add((edge.input.node as LLNodeView<GrammarNodeAsset>).nodeAsset);
            (nodeAsset as EntryNodeAsset).children = newChildren;
        }
        public override void UpdateEdgesFromAsset(LLGraphView<GrammarNodeAsset> graphView)
        {
            var entryNodeAsset = nodeAsset as EntryNodeAsset;
            outputPort.DisconnectAll();
            
            foreach (var node in entryNodeAsset.children)
            {
                LLNodeView<GrammarNodeAsset> nodeView = node.nodeView;
                if (nodeView is GrammarNodeView grammarNodeView)
                {
                    Edge edge = outputPort.ConnectTo(grammarNodeView.GetInputPort());
                    graphView.AddElement(edge);
                }
            }
        }
    }
}
