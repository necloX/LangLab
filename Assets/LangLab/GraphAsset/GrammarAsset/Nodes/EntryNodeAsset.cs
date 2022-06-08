using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using LangLab;

[CreateAssetMenu(menuName = "Grammar Node/Entry")]
public class EntryNodeAsset : GrammarNodeAsset
{
    public List<GrammarNodeAsset> children;
    public override bool Init(GraphAsset<GrammarNodeAsset> graph)
    {
        base.Init(graph);
        if(((GrammarGraphAsset)graph).entryNode != null) 
        { 
            Debug.Log("There is already an entry node in this graph."); 
            return false;
        }
        else
        {
            (graph as GrammarGraphAsset).entryNode = this;
            return true;
        }
    }
    public override void Clean(GraphAsset<GrammarNodeAsset> graph)
    {
        (graph as GrammarGraphAsset).entryNode = null;
    }
}
