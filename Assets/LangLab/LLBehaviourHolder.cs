using UnityEngine;
using System.Collections.Generic;
namespace LangLab
{
    public class LLBehaviourHolder : ScriptableObject
    {
        [HideInInspector]public GrammarNodeAsset grammarNode;
        public List<FuturBehaviour> futurBehaviors;
        public int a;
    }
    public class FuturBehaviour
    {
        public System.Type type;
        public MatchingGraph matchingGraph;
    }
}
