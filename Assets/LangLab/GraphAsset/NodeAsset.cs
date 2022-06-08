using UnityEngine;

namespace LangLab
{
    public abstract class NodeAsset<NodeType> : ScriptableObject where NodeType : NodeAsset<NodeType>
    {
        [HideInInspector]public Vector2 position;
        GraphAsset<NodeType> graph;
        [HideInInspector] public LLNodeView<NodeType> nodeView;
        
        public virtual bool Init(GraphAsset<NodeType> graph) 
        {
            this.graph = graph;
            return true; 
        }
        public virtual void Clean(GraphAsset<NodeType> graph) { }
    }
}

