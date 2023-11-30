using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class GraphViewBase<T1, T2> : GraphView where T1 : NodeBase where T2 : NodeViewBase
{
    // public new class UxmlFactory : UxmlFactory<GraphViewBase<T>, UxmlTraits> {}
    public Action<NodeViewBase> OnNodeSelected;
    protected Graph<T1> CurrentGraph;
    protected Dictionary<T2, T1> GraphViewNodeMap = new Dictionary<T2, T1>();
    protected Dictionary<T1, T2> GraphNodeViewMap = new Dictionary<T1, T2>();
    protected Dictionary<GraphElement, T1> GraphElementLookUp = new Dictionary<GraphElement, T1>();

    public GraphViewBase()
    {
        Insert(0, new GridBackground());
        
        this.AddManipulator(new ContentZoomer());
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());
    }

    public virtual void PopulateView(Graph<T1> graph)
    {
        CurrentGraph = graph;

        graphViewChanged -= OnGraphViewChanged;
        DeleteElements(graphElements.ToList());
        ClearGraphs();
        graphViewChanged += OnGraphViewChanged;
        
        CurrentGraph.NodeBases.ForEach(x =>
        {
            CreateNodeView(x);
        });
    }

    private void ClearGraphs()
    {
        GraphViewNodeMap.Clear();
        GraphNodeViewMap.Clear();
        GraphElementLookUp.Clear();
    }

    protected T2 FindNodeViewBase(string edgeDataChildId)
    {
        var nodeElement = (T2) GetNodeByGuid(edgeDataChildId);

        return nodeElement;
    }

    protected  virtual GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
    {
        if (graphViewChange.elementsToRemove != null)
        {
            graphViewChange.elementsToRemove.ForEach((element =>
            {
                if (GraphElementLookUp.ContainsKey(element))
                {
                    CurrentGraph.RemoveNode(GraphElementLookUp[element]);
                }
                
                Edge edge = element as Edge;
                
                if (edge != null)
                {
                    var parentNodeElement = (T2) edge.output.node;
                    var childNodeElement = (T2) edge.input.node;
                
                    CurrentGraph.RemoveChild(GraphViewNodeMap[parentNodeElement], new EdgeData()
                    {
                        outPutIndex = GetViewPortIndex(parentNodeElement, edge.output),
                        inputIndex = GetViewPortIndex(childNodeElement, edge.input),
                        childNode = GraphViewNodeMap[childNodeElement]
                    });
                }
            }));
        }

        return graphViewChange;
    }

    public virtual List<NodeViewBase> GetNodeViewTypes()
    {
        return new List<NodeViewBase>();
    }
    
    protected virtual void CreateNodeView(T1 nodeBase)
    {
        foreach (var comboNodeView in GetNodeViewTypes())
        {
            var nodeType = comboNodeView.GetNodeType();
            if (nodeType == nodeBase.GetType())
            {
                var view = (T2) Convert.ChangeType(Activator.CreateInstance(comboNodeView.GetType()), comboNodeView.GetType());
                InitNewView(nodeBase, view);
            }
        }
    }

    protected virtual void InitNewView(T1 nodeBase, T2 view)
    {
        view.Create(nodeBase);
        GraphViewNodeMap.Add(view, nodeBase);
        GraphNodeViewMap.Add(nodeBase, view);
        GraphElementLookUp.Add(view, nodeBase);
        Delegate(view, OnNodeSelected);
        AddElement(view);
    }
    
    protected virtual void CreateNode(Type type, Vector2 position)
    {
        var node = CurrentGraph.CreateNode(type);
        node.position = position;
        CreateNodeView(node);
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        return ports.ToList().Where(port => port.direction != startPort.direction && port.node != startPort.node).ToList();
    }
    
    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        var types = TypeCache.GetTypesDerivedFrom<T1>();
        foreach (var type in types)
        {
            if(type.IsAbstract) continue;
            VisualElement contentView = ElementAt(1);
            var screenMousePosition = evt.localMousePosition;
            var nodePosition = screenMousePosition - (Vector2)contentView.transform.position;
            nodePosition *= 1 / contentView.transform.scale.x;
            
            evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}"
                , x => CreateNode(type, nodePosition));
        }
    }
    
    public PortIndexTuple GetViewPortIndex(T2 nodeView, Port port)
    {
        return nodeView.PortIndexLookUp[port];
    }
    
    public Port GetPort(T2 nodeView,PortIndexTuple index)
    {
        return (nodeView.IndexPortLookUp[index.containerIndex])[index.portIndex];
    }

    public void Delegate(T2 nodeView,Action<NodeViewBase> onSelectionChanged)
    {
        nodeView.OnNodeSelected += onSelectionChanged;
    }
    
}