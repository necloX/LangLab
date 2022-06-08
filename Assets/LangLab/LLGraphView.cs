using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace LangLab
{
    public class LLGraphView<NodeType> : GraphView where NodeType:NodeAsset<NodeType>
    {
        GraphAsset<NodeType> graphAsset;
        List<LLNodeView<NodeType>> allNodeView;
        public LLGraphView()
        {
            AddManipulators();
            AddGridBackground();
            AddStyle();
            Undo.undoRedoPerformed += OnUndoRedo;
        }

        private void OnUndoRedo()
        {
            if(graphAsset == null)
                return;
            PopulateView(graphAsset);
            AssetDatabase.SaveAssets();
        }

        private void AddManipulators()
        {
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
        }

        private void AddStyle()
        {
            StyleSheet styleSheet = (StyleSheet)EditorGUIUtility.Load("LangLab/LLGraphViewStyle.uss");
            styleSheets.Add(styleSheet);
        }

        private void AddGridBackground()
        {
            GridBackground gridBackground = new GridBackground();
            gridBackground.StretchToParentSize();
            Insert(0, gridBackground);
        }

        internal void PopulateView(GraphAsset<NodeType> graphAsset)
        {
            this.graphAsset = graphAsset;
            graphViewChanged -= OnGraphViewChanged;
            DeleteElements(graphElements);
            graphViewChanged += OnGraphViewChanged;
            graphAsset.Nodes.ForEach(node =>
            {
                var nodeView = CreateNodeView(node);
            });
            graphAsset.nodes.ForEach(node =>
            {
                if (node.nodeView != null) node.nodeView.UpdateEdgesFromAsset(this);
            });
        }
        public LLNodeView<NodeType> GetViewer(NodeType nodeAsset)
        {
            foreach (var nodeView in allNodeView)
            {
                if (nodeView.nodeAsset == nodeAsset)
                    return nodeView;
            }
            return null;
        }
        void OnMouseDown()
        {

        }
        private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
        {
            if(graphViewChange.elementsToRemove != null)
            {
                foreach(var elementToRemove in graphViewChange.elementsToRemove)
                {
                    if(elementToRemove is LLNodeView<NodeType> nodeView)
                    {
                        graphAsset.DeleteNode(nodeView.nodeAsset);
                    }
                    if (elementToRemove is Edge edge)
                    {
                        var from = (LLNodeView<NodeType>)edge.output.node;
                        var to = (LLNodeView<NodeType>)edge.input.node;
                        from.UpdateAssetFromEdges();
                        to.UpdateAssetFromEdges();
                    }
                    AssetDatabase.SaveAssets();
                }
            }
            if (graphViewChange.edgesToCreate != null)
            {
                foreach (var edge in graphViewChange.edgesToCreate)
                {
                    var from = (LLNodeView<NodeType>)edge.output.node;
                    var to = (LLNodeView<NodeType>)edge.input.node;
                    from.UpdateAssetFromEdges();
                    to.UpdateAssetFromEdges();
                    AssetDatabase.SaveAssets();
                }
            }
            return graphViewChange;
        }
        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            return ports.ToList().Where(port => port != startPort && port.direction != startPort.direction).ToList();
        }

        void CreateNode(System.Type assetType)
        {
            NodeType nodeAsset = graphAsset.CreateNode(assetType);
            if (nodeAsset != null)
            {
                var nodeView = CreateNodeView(nodeAsset);
                if (nodeView == null) graphAsset.DeleteNode(nodeAsset);
            }
        }
        LLNodeView<NodeType> CreateNodeView(NodeType node)
        {
            var nodeView = LLNodeView<NodeType>.CreateNodeView(node);
            if (nodeView == null) return nodeView;
            AddElement(nodeView);
            node.nodeView = nodeView;
            return nodeView;
        }
        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            var types = TypeCache.GetTypesDerivedFrom<LLNodeView<NodeType>>();
            foreach (var type in types)
            {
                NodeViewAttribute attr = System.Attribute.GetCustomAttribute(type, typeof(NodeViewAttribute)) as NodeViewAttribute;
                if (attr != null)
                {
                    if (!attr.type.IsAbstract)
                    {
                        evt.menu.AppendAction($"{attr.type.Name}", a => CreateNode(attr.type));
                    }
                }
            }
        }
    }
}

