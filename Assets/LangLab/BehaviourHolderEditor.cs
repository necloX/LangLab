using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.GraphView;

namespace LangLab
{
    [CustomEditor(typeof(LLBehaviourHolder))]
    public class BehaviourHolderEditor : Editor
    {
        ComponentSearchWindow searchWindow;
        public override void OnInspectorGUI()
        {
            var holder = (LLBehaviourHolder)target;
            var t = new SerializedObject(holder.grammarNode);
            SerializedProperty serializedProperty = t.GetIterator();
            while (serializedProperty.NextVisible(enterChildren: true))
            {
                EditorGUILayout.PropertyField(serializedProperty);
            }
            if (GUILayout.Button("Add Component"))
            {
                if(searchWindow == null)
                {
                    searchWindow = ScriptableObject.CreateInstance<ComponentSearchWindow>();
                    searchWindow.Initialize(holder);
                }
                SearchWindow.Open(new SearchWindowContext(GUIUtility.GUIToScreenPoint(Event.current.mousePosition)), searchWindow);
            }
            if (holder.futurBehaviors == null) return;
            for(int i = holder.futurBehaviors.Count - 1; i >= 0; i--)
            {
                EditorGUILayout.Space();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(holder.futurBehaviors[i].type.Name);
                bool delete = GUILayout.Button("Delete");
                if (delete)
                {
                    holder.futurBehaviors.Remove(holder.futurBehaviors[i]);
                    
                }
                EditorGUILayout.EndHorizontal();
                if(!delete)
                {
                    if(GUILayout.Button("Open matching graph"))
                    {
                        if(holder.futurBehaviors[i].matchingGraph == null)
                            holder.futurBehaviors[i].matchingGraph = (MatchingGraph)ScriptableObject.CreateInstance<MatchingGraph>();
                        Selection.activeObject = holder.futurBehaviors[i].matchingGraph;
                    }
                }
                    //holder.futurBehaviors[i].matchingGraph = (MatchingGraph)EditorGUILayout.ObjectField(holder.futurBehaviors[i].matchingGraph,typeof(MatchingGraph),true);
                EditorGUILayout.Space();
                Rect rect = EditorGUILayout.GetControlRect(false, 1);
                rect.height = 1;
                EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 1));
                
            }
        }
    }
}

