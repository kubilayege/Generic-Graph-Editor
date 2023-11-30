
using System;

[Serializable]
public class PortIndexTuple
{
    public int containerIndex;
    public int portIndex;
    
    public static bool operator ==(PortIndexTuple portIndexTuple1, PortIndexTuple portIndexTuple2)
    {
        return portIndexTuple1.containerIndex == portIndexTuple2.containerIndex &&
               portIndexTuple1.portIndex == portIndexTuple2.portIndex;
    }

    public static bool operator !=(PortIndexTuple portIndexTuple1, PortIndexTuple portIndexTuple2)
    {
        return !(portIndexTuple1 == portIndexTuple2);
    }
    
    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}