using System;
using System.Collections.Generic;

using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace LangLab
{
    public class ComponentSearchWindow : ScriptableObject, ISearchWindowProvider
    {
        GrammarNodeAsset grammarNode;
        public void Initialize(GrammarNodeAsset grammarNode)
        {
            this.grammarNode = grammarNode;
        }
        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            List<SearchTreeEntry> searchTreeEntries = new List<SearchTreeEntry>()
            {
                new SearchTreeGroupEntry(new GUIContent("Search component")),
            };
            var types = TypeCache.GetTypesDerivedFrom<LLBehaviour>();
            foreach (var type in types)
            {
                var entry = new SearchTreeEntry(new GUIContent(type.Name));
                entry.level = 1;
                entry.userData = type;
                searchTreeEntries.Add(entry);
            }
            return searchTreeEntries;
        }

        public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
        {
            grammarNode.AddBehaviour(SearchTreeEntry.userData as Type);
            return true;
        }
    }
}
