using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;

public abstract class ComboNodeView : NodeViewBase
{   
    public Port ComboPort;
    public Port LightOut;
    public Port HeavyOut;
    public Port HoldLightOut;
    public Port HoldHeavyOut;
    public Dictionary<Port, AttackType> portTypes = new Dictionary<Port, AttackType>();

    public virtual AttackType GetPortType(Port port)
    {
        return portTypes[port];
    }
}