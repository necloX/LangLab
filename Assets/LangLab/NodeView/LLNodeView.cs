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
            titleContainer.style.backgroundColor = Color.grey;
            
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
        
        public abstract void UpdateAssetFromEdges(Edge edge);
        public abstract void UpdateEdgesFromAsset(LLGraphView<NodeType> graphView);
    }
}

