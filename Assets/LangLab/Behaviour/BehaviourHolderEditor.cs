using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using System.Collections.Generic;
using System;
using System.Reflection;
namespace LangLab
{
    [CustomEditor(typeof(LLBehaviourHolder))]
    public class BehaviourHolderEditor : Editor
    {
        ComponentSearchWindow searchWindow;
        public override void OnInspectorGUI()
        {
            
            var holder = (LLBehaviourHolder)target;
            var serializedGrammarNode = new SerializedObject(holder.grammarNode);
            SerializedProperty serializedProperty = serializedGrammarNode.GetIterator();
            while (serializedProperty.NextVisible(enterChildren: true))
            {
                EditorGUILayout.PropertyField(serializedProperty);
            }
            serializedGrammarNode.ApplyModifiedProperties();
            if (holder.grammarNode.futurBehaviors == null) holder.grammarNode.futurBehaviors = new List<FuturBehaviour>();
            EditorGUILayout.Space();
            Rect rect = EditorGUILayout.GetControlRect(false, 1);
            rect.height = 1;
            EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 1));
            for (int i = holder.grammarNode.futurBehaviors.Count - 1; i >= 0; i--)
            { 
                EditorGUILayout.Space();
                EditorGUILayout.BeginHorizontal();
                var futureBehaviour = holder.grammarNode.futurBehaviors[i];
                EditorGUILayout.LabelField(futureBehaviour.name);
                bool delete = GUILayout.Button("Delete");
                if (delete)
                {
                    holder.grammarNode.RemoveBehaviour(futureBehaviour);
                }
                
                if(!delete)
                {
                    if(GUILayout.Button("Open matching graph"))
                    {
                        LLExplorerWindow.Instance.SwitchTo(futureBehaviour.matchingGraph);
                    }
                }
                EditorGUILayout.EndHorizontal();
                Type behaviourType = Type.GetType(futureBehaviour.assemblyName); 
                foreach (var field in behaviourType.GetFields(BindingFlags.Instance | BindingFlags.Public))
                {
                    if (Attribute.IsDefined(field, typeof(Match)))
                    {
                        if (futureBehaviour.nodesToMatch == null) futureBehaviour.nodesToMatch = new StringIndexedMatchingNode();
                        if(!futureBehaviour.nodesToMatch.ContainsKey(field.Name))
                        {
                            futureBehaviour.nodesToMatch.Add(field.Name, null);
                        }
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField(field.Name);
                        futureBehaviour.nodesToMatch[field.Name] = (MatchingGraphNode)EditorGUILayout.ObjectField(futureBehaviour.nodesToMatch[field.Name], typeof(MatchingGraphNode), true);
                        EditorGUILayout.EndHorizontal();
                    }
                }
                //holder.futurBehaviors[i].matchingGraph = (MatchingGraph)EditorGUILayout.ObjectField(holder.futurBehaviors[i].matchingGraph,typeof(MatchingGraph),true);
                EditorGUILayout.Space();
                rect = EditorGUILayout.GetControlRect(false, 1);
                rect.height = 1;
                EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 1));
            }
            EditorGUILayout.Space();
            if (GUILayout.Button("Add Component"))
            {
                if (searchWindow == null)
                {
                    searchWindow = ScriptableObject.CreateInstance<ComponentSearchWindow>();
                    searchWindow.Initialize(holder.grammarNode);
                }
                SearchWindow.Open(new SearchWindowContext(GUIUtility.GUIToScreenPoint(Event.current.mousePosition)), searchWindow);
            }
        }
    }
}

