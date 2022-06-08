using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LangLab;

namespace LangLab
{
    /// <summary>
    /// A simple graph that can be match against a parse tree node.
    /// </summary>
    public class MatchingGraph : GraphAsset<MatchingGraphNode>
    {
        public MatchingGraphNode root;
    }
}

