
using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ComboStartNodeView : ComboNodeView
{

    public override void InitPorts()
    {
        Debug.Log("InitPort");

        LightOut = CreatePort(new PortData
        {           
            Name = "Light",
            Capacity = Port.Capacity.Single,
            Direction = Direction.Output,
            Orientation = Orientation.Vertical,
            Type = typeof(bool),
            PortColor = Color.green
        });
        
        HeavyOut = CreatePort(new PortData
        {           
            Name = "Heavy",
            Capacity = Port.Capacity.Single,
            Direction = Direction.Output,
            Orientation = Orientation.Vertical,
            Type = typeof(bool),
            PortColor = Color.red
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
        
        portTypes.Add(LightOut, AttackType.Light);
        portTypes.Add(HeavyOut, AttackType.Heavy);
        portTypes.Add(HoldLightOut, AttackType.HoldLight);
        portTypes.Add(HoldHeavyOut, AttackType.HoldHeavy);
    }

    public override Type GetNodeType()
    {
        return typeof(ComboStartNode);
    }
}