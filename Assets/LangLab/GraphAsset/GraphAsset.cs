using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace LangLab
{
    /// <summary>
    /// The base class for implementing visual graph in LangLab.
    /// </summary>

    public abstract class GraphAsset<NodeType> : ScriptableObject where NodeType : NodeAsset<NodeType>
    {
        public List<NodeType> nodes;
        public List<NodeType> Nodes 
        { 
            get
            {
                if(nodes == null) nodes = new List<NodeType>();
                return nodes;
            }
        }
        /// <summary>
        /// Create and initialize a visual node into this graph.
        /// </summary>
        public NodeType CreateNode(System.Type type)
        {
            NodeType node = ScriptableObject.CreateInstance(type) as NodeType;
            if (!node.Init(this)) return null;
            node.name = type.Name;
            if (nodes == null) nodes = new List<NodeType>();
            nodes.Add(node);
            AssetDatabase.AddObjectToAsset(node, this);
            AssetDatabase.SaveAssets();
            return node;
        }
        /// <summary>
        /// Delete a visual node into this graph.
        /// </summary>
        public void DeleteNode(NodeType node)
        {
            node.Clean(this);
            nodes.Remove(node);

            AssetDatabase.RemoveObjectFromAsset(node);
            AssetDatabase.SaveAssets();
        }
    }
}

