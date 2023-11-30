using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

public class ComboGraphView : GraphViewBase<ComboNodeBase, NodeViewBase>
{
    public new class UxmlFactory : UxmlFactory<ComboGraphView, UxmlTraits>
    {
    }

    public ComboGraphView() : base()
    {
        var styleSheet =
            AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/Graph/ComboGraph/ComboEditorWindow.uss");
        styleSheets.Add(styleSheet);
    }


    public override List<NodeViewBase> GetNodeViewTypes()
    {
        var viewTypes = TypeCache.GetTypesDerivedFrom<ComboNodeView>();
        var comboNodeViews = new List<NodeViewBase>();

        foreach (var viewType in viewTypes)
        {
            comboNodeViews.Add(Convert.ChangeType(Activator.CreateInstance(viewType), viewType) as NodeViewBase);
        }

        return comboNodeViews;
    }
    
    protected override void CreateNodeView(ComboNodeBase nodeBase)
    {
        base.CreateNodeView(nodeBase);
    }

    protected override void InitNewView(ComboNodeBase nodeBase, NodeViewBase view)
    {
        base.InitNewView(nodeBase, view);
    }


    public AttackType GetPortType(ComboNodeView nodeView, Port port)
    {
        return nodeView.GetPortType(port);
    }

    public override void PopulateView(Graph<ComboNodeBase> graph)
    {
        base.PopulateView(graph);

        graph.NodeBases.ForEach(x =>
        {
            var edgeDatas = graph.GetChildren<ComboEdgeData>(x);
            foreach (var edgeData in edgeDatas)
            {
                var parentNode = GraphNodeViewMap[x];
                var childNode = GraphNodeViewMap[edgeData.childNode as ComboNodeBase];
                var parentPort = GetPort(parentNode, edgeData.outPutIndex);
                var childPort = GetPort(childNode, edgeData.inputIndex);

                var edge = parentPort.ConnectTo(childPort);
                AddElement(edge);
            }
        });
    }

    protected override GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
    {
        graphViewChange = base.OnGraphViewChanged(graphViewChange);


        if (graphViewChange.edgesToCreate != null)
        {
            graphViewChange.edgesToCreate.ForEach((element =>
            {
                var parentNodeElement = element.output.node as NodeViewBase;
                var childNodeElement = element.input.node as NodeViewBase;

                CurrentGraph.AddChild(GraphViewNodeMap[parentNodeElement],
                    new ComboEdgeData()
                    {
                        outPutIndex = GetViewPortIndex(parentNodeElement, element.output),
                        inputIndex = GetViewPortIndex(childNodeElement, element.input),
                        AttackType = GetPortType(parentNodeElement as ComboNodeView, element.output),
                        childNode = GraphViewNodeMap[childNodeElement]
                    });
            }));
        }

        return graphViewChange;
    }
}