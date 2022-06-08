using UnityEditor;
using UnityEngine;
namespace LangLab
{
    [CustomEditor(typeof(CompilationNodeAsset),true)]
    public class CompilationNodeEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            if(GUILayout.Button("Go Through")) ((CompilationNodeAsset)target).GoThrough();
        }
    }
    [CustomEditor(typeof(IronyGrammarAsset), true)]
    public class IronyGrammarAssetEditor : CompilationNodeEditor{}
    [CustomEditor(typeof(IronyParserAsset), true)]
    public class IronyParserAssetEditor : CompilationNodeEditor { }

}
