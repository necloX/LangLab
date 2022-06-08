using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using UnityEditor;
using UnityEngine;

namespace LangLab
{
    public abstract class GrammarNodeView : LLNodeView<GrammarNodeAsset>
    {
        public abstract Port GetInputPort();
        public override void Initialize(GrammarNodeAsset node)
        {
            base.Initialize(node);
            Button openInInspector = new Button()
            {
                text = "Behaviors"
            };
            mainContainer.Insert(0,openInInspector);
            openInInspector.clicked += () =>
            {
                Selection.activeObject = (nodeAsset as GrammarNodeAsset).GetBehaviourHolder();
            };
            base.Initialize(node);
            TextField nodeName = new TextField()
            {
                value = node.name,
            };
            nodeName.RegisterCallback<ChangeEvent<string>>(evt => nodeAsset.name = nodeName.value);
            titleContainer.Clear();

            titleContainer.Insert(0, nodeName);
            mainContainer.Insert(0, inputContainer);
        }
    }
}
