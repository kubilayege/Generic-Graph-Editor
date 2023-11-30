using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ComboFinisherNodeView : ComboNodeView
{

    public override void InitPorts()
    {
        ComboPort = CreatePort(new PortData
        {
            Name = "Combo",
            Capacity = Port.Capacity.Multi,
            Direction = Direction.Input,
            Orientation = Orientation.Vertical,
            Type = typeof(bool),
            PortColor = Color.cyan
        });
    }

    public override Type GetNodeType()
    {
        return typeof(ComboFinisherNode);
    }
}