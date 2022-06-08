using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LangLab
{
    public abstract class GrammarNodeAsset : NodeAsset<GrammarNodeAsset>
    {
        public bool hideInAst;
        [HideInInspector]public List<FuturBehaviour> futurBehaviors;
        public LLBehaviourHolder GetBehaviourHolder()
        {
            var behaviourHolder = ScriptableObject.CreateInstance(typeof(LLBehaviourHolder)) as LLBehaviourHolder;
            behaviourHolder.grammarNode = this;
            behaviourHolder.name = "Semantics";
            return behaviourHolder;
        }
        public void AddBehaviour(System.Type type)
        {
            if(futurBehaviors == null) futurBehaviors = new List<FuturBehaviour>();
            var futurBehaviour = new FuturBehaviour();
            futurBehaviour.assemblyName = type.AssemblyQualifiedName;
            futurBehaviour.name = type.Name;
            var mGraph = ScriptableObject.CreateInstance<MatchingGraph>();
            AssetDatabase.AddObjectToAsset(mGraph, this);
            AssetDatabase.SaveAssets();
            futurBehaviour.matchingGraph = mGraph;
            futurBehaviors.Add(futurBehaviour);
        }
        public void RemoveBehaviour(FuturBehaviour futurBehaviour)
        {
            futurBehaviors.Remove(futurBehaviour);
            AssetDatabase.RemoveObjectFromAsset(futurBehaviour.matchingGraph);
            AssetDatabase.SaveAssets();
        }
        public override void Clean(GraphAsset<GrammarNodeAsset> graph)
        {
            base.Clean(graph);
            foreach(var behaviour in futurBehaviors)
            {
                AssetDatabase.RemoveObjectFromAsset(behaviour.matchingGraph);
            }
            AssetDatabase.SaveAssets();
        }
    }
    [Serializable]
    public class FuturBehaviour
    {
        public string name;
        public string assemblyName;
        public MatchingGraph matchingGraph;
        public StringIndexedMatchingNode nodesToMatch;
    }
    [Serializable]
    public class StringIndexedMatchingNode : Dictionary<string, MatchingGraphNode>, ISerializationCallbackReceiver
    {
        [SerializeField, HideInInspector]
        private List<string> keyData = new List<string>();

        [SerializeField, HideInInspector]
        private List<MatchingGraphNode> valueData = new List<MatchingGraphNode>();
        public void OnAfterDeserialize()
        {
            this.Clear();
            for (int i = 0; i < this.keyData.Count && i < this.valueData.Count; i++)
            {
                this[this.keyData[i]] = this.valueData[i];
            }
        }

        public void OnBeforeSerialize()
        {
            this.keyData.Clear();
            this.valueData.Clear();

            foreach (var item in this)
            {
                this.keyData.Add(item.Key);
                this.valueData.Add(item.Value);
            }
        }
    }
}
