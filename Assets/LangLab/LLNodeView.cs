using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEditor;
using UnityEngine.UIElements;

namespace LangLab
{
    public abstract class LLNodeView<NodeType> : Node where NodeType : NodeAsset<NodeType>
    {
        public NodeType nodeAsset;
         
        public virtual void Initialize(NodeType node)
        {
            base.SetPosition(new Rect(node.position, Vector2.zero));
            this.nodeAsset = node;

            TextField nodeName = new TextField()
            {
                value = node.name,
            };
            nodeName.RegisterCallback<ChangeEvent<string>>(evt => nodeAsset.name = nodeName.value);
            titleContainer.Clear();
            titleContainer.style.backgroundColor = Color.white;
            titleContainer.Insert(0, nodeName);
            mainContainer.Insert(0, inputContainer);

            Button openInInspector = new Button()
            {
                text = "Behaviors"
            };
            titleContainer.Add(openInInspector);
            openInInspector.clicked += () =>
            {
                
                Selection.activeObject = (nodeAsset as GrammarNodeAsset).BehaviourHolder;
            };
        }
        public override void SetPosition(Rect newPos)
        {
            base.SetPosition(newPos);
            Undo.RecordObject(nodeAsset, "Graph Explorer (Set position)");
            nodeAsset.position = new Vector2(newPos.xMin, newPos.yMin);
            EditorUtility.SetDirty(nodeAsset);
        }
        public static LLNodeView<NodeType> CreateNodeView(NodeType nodeAsset)
        {
            var types = TypeCache.GetTypesDerivedFrom<LLNodeView<NodeType>>();
            foreach (var type in types)
            {
                NodeViewAttribute attr = System.Attribute.GetCustomAttribute(type, typeof(NodeViewAttribute)) as NodeViewAttribute;
                if(attr != null)
                {
                    if(attr.type == nodeAsset.GetType())
                    {
                        var nodeView = (LLNodeView<NodeType>)System.Activator.CreateInstance(type);
                        nodeView.Initialize(nodeAsset);
                        return nodeView;
                    }
                    
                }
            }
            Debug.Log("There is no node view corresponding to "+nodeAsset.GetType() +" node asset.");
            return null;
        }
        
        public abstract void UpdateAssetFromEdges();
        public abstract void UpdateEdgesFromAsset(LLGraphView<NodeType> graphView);
    }
}

