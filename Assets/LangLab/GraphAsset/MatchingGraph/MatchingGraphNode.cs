using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LangLab;
using System;

public class MatchingGraphNode : NodeAsset<MatchingGraphNode>
{
    public List<MatchingGraphNode> children;
    bool isRoot;
    public bool IsRoot { get { return isRoot; } }
    public override bool Init(GraphAsset<MatchingGraphNode> graph)
    {
        base.Init(graph);
        var matchingGraph = graph as MatchingGraph;
        if (matchingGraph.root == null)
        {
            matchingGraph.root = this;
            isRoot = true;
        }
        return true;
    }
    public override void Clean(GraphAsset<MatchingGraphNode> graph)
    {
        base.Clean(graph);
        var matchingGraph = graph as MatchingGraph;
        if (isRoot) matchingGraph.root = null;
    }
}
