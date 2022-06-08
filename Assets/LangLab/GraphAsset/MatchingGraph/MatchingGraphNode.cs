using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LangLab;
using System;

public class MatchingGraphNode : NodeAsset<MatchingGraphNode>
{
    public GrammarNodeAsset grammarNode;
    public List<MatchingGraphNode> matchingNodes;
    public List<MatchingGraphNode> GetMatchingNodeChildren()
    {
        return matchingNodes;
    }
}
