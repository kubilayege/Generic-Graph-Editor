using System;

[Serializable]
public class ComboEdgeData : EdgeData
{
    public AttackType AttackType;
    public ComboNodeBase Node => (ComboNodeBase) childNode;
}