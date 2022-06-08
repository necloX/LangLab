

using UnityEngine.UIElements;

namespace LangLab
{
    [NodeViewAttribute(typeof(IronyParserAsset))]
    public class IronyParserView : CompilationNodeView
    {
        public override void Initialize(CompilationNodeAsset node)
        {
            base.Initialize(node);
            Label title = new Label("Irony Parser");
            titleContainer.Insert(0, title);
        }
    }
}
