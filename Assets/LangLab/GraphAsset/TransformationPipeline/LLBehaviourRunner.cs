using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangLab
{
    public class LLBehaviourRunner : CompilationNodeAsset
    {
        [CreateInputPort]
        public LLAst inputAst;
        [CreateOutputPort]
        public LLAst outputAst;
        public override void GoThrough()
        {
            base.GoThrough();

        }
    }
}
