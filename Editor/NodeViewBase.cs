using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[CanEditMultipleObjects]
public abstract class NodeViewBase : Node
{
    public Action<NodeViewBase> OnNodeSelected;
    public NodeBase Node;
    public Dictionary<Port, PortIndexTuple> PortIndexLookUp = new Dictionary<Port, PortIndexTuple>();
    public Dictionary<int, Dictionary<int, Port>> IndexPortLookUp 
        = new Dictionary<int, Dictionary<int, Port>>
            {
                {0, new Dictionary<int, Port>()},
                {1, new Dictionary<int, Port>()}
            };

    public Dictionary<Direction, int> indexCounter = new Dictionary<Direction, int>() {
        {Direction.Input, 0},
        {Direction.Output,0}};

    public virtual void Create(NodeBase node)
    {
        Node = node;
        title = node.name;
        viewDataKey = node.id;
        style.left = node.position.x;
        style.top = node.position.y;
        InitPorts();
    }

    public abstract void InitPorts();
    
    public override void SetPosition(Rect newPos)
    {
        base.SetPosition(newPos);
        Node.position.x = newPos.xMin;
        Node.position.y = newPos.yMin;
    }

    public Port CreatePort(PortData portData)
    {
        var port = InstantiatePort(Orientation.Horizontal, portData.Direction, portData.Capacity, portData.Type);
        var container = portData.Direction == Direction.Input ? inputContainer : outputContainer;
        container.Add(port);
        port.portName = portData.Name;
        port.portColor = portData.PortColor;

        var portDataIndex = new PortIndexTuple(){
            containerIndex = (int)portData.Direction,
            portIndex = indexCounter[portData.Direction]};

        PortIndexLookUp.Add(port, portDataIndex);
        IndexPortLookUp[portDataIndex.containerIndex].Add(portDataIndex.portIndex, port);

        indexCounter[portData.Direction] += 1;
        return port;
    }

    public override void OnSelected()
    {
        base.OnSelected();
        
        OnNodeSelected?.Invoke(this);
    }

    public abstract Type GetNodeType();

}

public class PortData
{
    public string Name;
    public Orientation Orientation;
    public Direction Direction;
    public Port.Capacity Capacity;
    public Type Type;
    public Color PortColor;
}