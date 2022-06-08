using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UIElements;

namespace LangLab
{
    [NodeViewAttribute(typeof(SingleTextAsset))]
    public class SingleTextAssetView : CompilationNodeView
    {
        public override void Initialize(CompilationNodeAsset node)
        {
            base.Initialize(node);
            TextField nodeName = new TextField()
            {
                value = node.name,
            };
            nodeName.RegisterCallback<ChangeEvent<string>>(evt => nodeAsset.name = nodeName.value);
            titleContainer.Clear();

            titleContainer.Insert(0, nodeName);
        }
    }
}
