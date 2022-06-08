using UnityEditor;
using UnityEngine;
namespace LangLab.Sample
{
    public class BasicCodeEditor : EditorWindow
    {
        public SingleTextAsset textAsset;
        public IronyGrammarAsset ironyGrammarAsset;
        public IronyParserAsset ironyParserAsset;
        [MenuItem("Tools/Basic code editor")]
        static void OpenWindow()
        {
            BasicCodeEditor codeEditor = (BasicCodeEditor)EditorWindow.GetWindow(typeof(BasicCodeEditor));
            codeEditor.Show();
        }
        private void OnGUI()
        {
            textAsset = (SingleTextAsset)EditorGUILayout.ObjectField(textAsset, typeof(SingleTextAsset), true);
            if (textAsset != null) textAsset.text = EditorGUILayout.TextArea(textAsset.text);
            ironyGrammarAsset = (IronyGrammarAsset)EditorGUILayout.ObjectField(ironyGrammarAsset, typeof(IronyGrammarAsset), true);
            ironyParserAsset = (IronyParserAsset)EditorGUILayout.ObjectField(ironyParserAsset, typeof(IronyParserAsset), true);
            EditorGUILayout.PrefixLabel("Name");
            GUILayout.FlexibleSpace();
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Compile"))
            {
                ironyGrammarAsset.GoThrough();
                ironyParserAsset.GoThrough();
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}
