using System;
using System.Collections.Generic;
using Irony.Parsing;
using UnityEngine;

namespace LangLab
{
    public abstract class GrammarNodeAsset : NodeAsset<GrammarNodeAsset>
    {
        public bool hideInAst;
        LLBehaviourHolder behaviourHolder;
        public LLBehaviourHolder BehaviourHolder
        {
            get
            {
                if (behaviourHolder == null)
                {
                    behaviourHolder = ScriptableObject.CreateInstance(typeof(LLBehaviourHolder)) as LLBehaviourHolder;
                    behaviourHolder.grammarNode = this;
                }
                return behaviourHolder;
            }
        }
    }
}
