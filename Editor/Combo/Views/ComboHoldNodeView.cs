using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ComboHoldNodeView : ComboNodeView
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

        HoldLightOut = CreatePort(new PortData
        {           
            Name = "HoldLight",
            Capacity = Port.Capacity.Single,
            Direction = Direction.Output,
            Orientation = Orientation.Vertical,
            Type = typeof(bool),
            PortColor = Color.yellow
        });
        
        HoldHeavyOut = CreatePort(new PortData
        {           
            Name = "HoldHeavy",
            Capacity = Port.Capacity.Single,
            Direction = Direction.Output,
            Orientation = Orientation.Vertical,
            Type = typeof(bool),
            PortColor = Color.yellow
        });
        portTypes.Add(HoldLightOut, AttackType.Light);
        portTypes.Add(HoldHeavyOut, AttackType.Heavy);
    }

    public override Type GetNodeType()
    {
        return typeof(ComboHoldNode);
    }
}