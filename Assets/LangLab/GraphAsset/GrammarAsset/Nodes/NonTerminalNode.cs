using System;
using System.Collections.Generic;
using UnityEngine;

namespace LangLab
{
    [CreateAssetMenu(menuName = "Grammar Node/Chain Node")]
    public class NonTerminalNode : GrammarNodeAsset
    {
        public bool zeroAllowed;
        public bool moreThanOneAllowed;
        public List<List<GrammarNodeAsset>> children;

        public void AddChildAt(GrammarNodeAsset child,int id)
        {
            if(children == null) children = new List<List<GrammarNodeAsset>>();
            while (children.Count < id+1) { children.Add(new List<GrammarNodeAsset>());}
            children[id].Add(child);
        }
        public void RemoveChildAt(GrammarNodeAsset child, int id)
        {
            if(children == null) return;
            if (children.Count < id + 1) return;
            children[id].Remove(child);
        }
    }
}
