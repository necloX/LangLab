using UnityEditor;
using UnityEngine;

namespace LangLab
{
    public abstract class CompilationNodeAsset : NodeAsset<CompilationNodeAsset>
    {
        public abstract void GoThrough();
    }
}