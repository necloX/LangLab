using System;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.Callbacks;
using UnityEngine;

namespace LangLab
{
    public class LLExplorerWindow : EditorWindow
    {
        static LLExplorerWindow instance;
        public static LLExplorerWindow Instance { get { return instance; } }
        LLGraphView<GrammarNodeAsset> grammarView;
        LLGraphView<LLAstNode> astView;
        LLGraphView<MatchingGraphNode> matchingGraphView;
        LLGraphView<CompilationNodeAsset> compilationView;
        [MenuItem("Window/LangLab/GraphExplorer")]
        public static void Open()
        {
            GetWindow<LLExplorerWindow>("Graph explorer");
        }
        private void OnEnable()
        {
            AddGraphView(grammarView);
            AddGraphView(astView);
            AddGraphView(matchingGraphView);
            AddGraphView(compilationView);
            instance = this;
            OnSelectionChange();
        }
        [OnOpenAsset]
        public static bool OnOpenAsset(int instanceId,int line)
        {
            if(Selection.activeObject is GraphAsset<GrammarNodeAsset>
                or GraphAsset<LLAstNode>
                or GraphAsset<MatchingGraphNode>
                or GraphAsset<CompilationNodeAsset>)
            {
                Open();
                return true;
            }
            return false;
        }
        private LLGraphView<NodeType> AddGraphView<NodeType>(LLGraphView<NodeType> graphView) where NodeType : NodeAsset<NodeType>
        {
            graphView = new LLGraphView<NodeType>();
            graphView.StretchToParentSize();
            return graphView;
        }
        private LLGraphView<NodeType> SetGraphView<NodeType>(LLGraphView<NodeType> graphView) where NodeType : NodeAsset<NodeType>
        {
            if (graphView == null)
            {
                graphView = AddGraphView<NodeType>(graphView);
            }
            rootVisualElement.Clear();
            rootVisualElement.Add(graphView);
            return graphView;
        }
        public void SwitchTo<NodeType>(GraphAsset<NodeType> graphAsset) where NodeType : NodeAsset<NodeType>
        {
            if (typeof(NodeType) == typeof(GrammarNodeAsset)) 
            {
                grammarView = SetGraphView(grammarView);
                grammarView.PopulateView(graphAsset as GraphAsset<GrammarNodeAsset>);
            }
            if (typeof(NodeType) == typeof(LLAst))
            {
                astView = SetGraphView(astView);
                astView.PopulateView(graphAsset as GraphAsset<LLAstNode>);
            }
            if (typeof(NodeType) == typeof(MatchingGraphNode))
            {
                matchingGraphView = SetGraphView(matchingGraphView);
                matchingGraphView.PopulateView(graphAsset as GraphAsset<MatchingGraphNode>);
            }
            if (typeof(NodeType) == typeof(CompilationNodeAsset))
            {
                compilationView = SetGraphView(compilationView);
                compilationView.PopulateView(graphAsset as GraphAsset<CompilationNodeAsset>);
            }
        }
        private void OnSelectionChange()
        { 
            switch (Selection.activeObject)
            {
                case GraphAsset<GrammarNodeAsset> graphAsset:    
                    grammarView = SetGraphView<GrammarNodeAsset>(grammarView); 
                    grammarView.PopulateView(graphAsset);
                    break;
                case GraphAsset<LLAstNode> graphAsset:
                    astView = SetGraphView<LLAstNode>(astView);
                    astView.PopulateView(graphAsset);
                    break;
                case GraphAsset<MatchingGraphNode> graphAsset:
                    matchingGraphView = SetGraphView<MatchingGraphNode>(matchingGraphView);
                    matchingGraphView.PopulateView(graphAsset);
                    break;
                case GraphAsset<CompilationNodeAsset> graphAsset:
                    compilationView = SetGraphView<CompilationNodeAsset>(compilationView);
                    compilationView.PopulateView(graphAsset);
                    break;

                default:
                    break;
            }
        }
    }
}