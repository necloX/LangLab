using System;
using System.Collections.Generic;

using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace LangLab
{
    public class ComponentSearchWindow : ScriptableObject, ISearchWindowProvider
    {
        LLBehaviourHolder behaviourHolder;
        public void Initialize(LLBehaviourHolder behaviourHolder)
        {
            this.behaviourHolder = behaviourHolder;
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
            if (behaviourHolder.futurBehaviors == null) behaviourHolder.futurBehaviors = new List<FuturBehaviour>();
            behaviourHolder.futurBehaviors.Add(new FuturBehaviour() {type =  SearchTreeEntry.userData as Type });
            return true;
        }
    }
}
