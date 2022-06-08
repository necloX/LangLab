using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UIElements;
namespace LangLab
{
    [NodeViewAttribute(typeof(IronyGrammarAsset))]
    public class IronyGrammarView : CompilationNodeView
    {
        public override void Initialize(CompilationNodeAsset node)
        {
            base.Initialize(node);
            Label title = new Label("Irony Grammar");
            titleContainer.Insert(0, title);
        }
    }
}
