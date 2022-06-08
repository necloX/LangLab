using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Irony.Parsing;
namespace LangLab
{
    [CreateAssetMenu()]
    public class GrammarGraphAsset : GraphAsset<GrammarNodeAsset>
    {
        public EntryNodeAsset entryNode;
    }   
}