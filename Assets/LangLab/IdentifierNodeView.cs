using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.Experimental.GraphView;

namespace LangLab
{
    [NodeViewAttribute(typeof(IdentifierTerminalNodeAsset))]
    public class IdentifierNodeView : GrammarNodeView
    {
        Port inputPort;
        public override Port GetInputPort() => inputPort;

        public override void Initialize(GrammarNodeAsset node)
        {
            base.Initialize(node);
            inputPort = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Multi, typeof(bool));
            inputPort.name = "";
            inputContainer.Add(inputPort);
        }

        public override void UpdateAssetFromEdges()
        {
            
        }

        public override void UpdateEdgesFromAsset(LLGraphView<GrammarNodeAsset> graphView)
        {
            
        }
    }
}
