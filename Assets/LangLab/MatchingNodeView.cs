using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangLab
{
    [NodeViewAttribute(typeof(MatchingGraphNode))]
    public class MatchingNodeView : LLNodeView<MatchingGraphNode>
    {
        public override void UpdateAssetFromEdges()
        {
            throw new NotImplementedException();
        }

        public override void UpdateEdgesFromAsset(LLGraphView<MatchingGraphNode> graphView)
        {
            throw new NotImplementedException();
        }
    }
}
