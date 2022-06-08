using System.Collections.Generic;

namespace LangLab
{
    /// <summary>
    /// A node belonging to an absract syntax tree.
    /// </summary>
    public class LLAstNode : NodeAsset<LLAstNode>
    {
        public GrammarNodeAsset grammarNodeAsset;
        public List<LLAstNode> children;
        public List<LLBehaviour> behaviours;
        public string text;
        public ComponentType GetComponent<ComponentType>()
        {
            foreach(var behaviour in behaviours)
            {
                if(behaviour is ComponentType component) return component;
            }
            throw new System.Exception("There is non component of type behaviour on this node.");
        }
        public LLAstNode GetChild(int id) => children[id];
    }
}

