using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.Experimental.GraphView;

namespace LangLab
{
    public abstract class GrammarNodeView : LLNodeView<GrammarNodeAsset>
    {
        public abstract Port GetInputPort();
    }
}
