using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace LangLab
{
    [NodeViewAttribute(typeof(KeyTermTerminalNode))]
    internal class KeyTermNodeView : GrammarNodeView
    {
        Port inputPort;
        public override Port GetInputPort() => inputPort;

        public override void Initialize(GrammarNodeAsset node)
        {
            base.Initialize(node);
            inputPort = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Multi, typeof(bool));
            inputPort.name = "";
            inputPort.portColor = Color.magenta;
            inputContainer.Add(inputPort);
        }
        public override void UpdateAssetFromEdges(Edge edge)
        {

        }

        public override void UpdateEdgesFromAsset(LLGraphView<GrammarNodeAsset> graphView)
        {

        }
    }
}
