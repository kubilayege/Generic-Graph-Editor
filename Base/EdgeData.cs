using System;

[Serializable]
public class EdgeData
{
    public PortIndexTuple inputIndex;
    public PortIndexTuple outPutIndex;
    public NodeBase childNode;

    public static bool operator ==(EdgeData edge, EdgeData edgeData)
    {
        return (edgeData.inputIndex == edge.inputIndex &&
                edgeData.outPutIndex == edge.outPutIndex &&
                edgeData.childNode == edge.childNode);
    }

    public static bool operator !=(EdgeData edge, EdgeData edgeData)
    {
        return !(edge == edgeData);
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