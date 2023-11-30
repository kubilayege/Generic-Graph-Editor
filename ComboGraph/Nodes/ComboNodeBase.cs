using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class ComboNodeBase : NodeBase
{
    public List<ComboEdgeData> comboEdges = new List<ComboEdgeData>();
    
    [Header("Base References")]
    public IndicatorBase indicator;
    public CastBase caster;

    [Range(0f,1f)]
    public float staminaCost;
    public float attackRange = 3f;
    
    public override void AddChild(EdgeData edgeData)
    {
        base.AddChild(edgeData);
        comboEdges.Add((ComboEdgeData) edgeData);
    }

    public override void RemoveChild(EdgeData edgeData)
    {
        base.RemoveChild(edgeData);
        for (int i = 0; i < comboEdges.Count; i++)
        {
            if (comboEdges[i] == edgeData)
            {        
                comboEdges.RemoveAt(i);
                break;
            }
        }
    }

    public override List<T> GetChildren<T>()
    {
        return comboEdges as List<T>;
    }

    public virtual (ComboNodeBase comboNodeBase, bool hasChild) Attack(AttackType attackType)
    {
        var comboNodeBase = GetComboNodeBase(attackType);
        return (comboNodeBase, comboNodeBase);
    }

    public ComboNodeBase GetComboNodeBase(AttackType type)
    {
        foreach (var comboEdge in comboEdges)
        {
            if (((ComboEdgeData) comboEdge).AttackType == type)
            {
                return comboEdge.childNode as ComboNodeBase;
            }
        }

        return null;
    }

    public override void Init()
    {
        indicator = indicator.Clone();
    }

    public ComboNodeBase Clone()
    {
        ComboNodeBase comboNodeBase = Instantiate(this);
        
        for (int i = 0; i < comboNodeBase.comboEdges.Count; i++)
        {
            comboNodeBase.comboEdges[i].childNode = ((ComboNodeBase)comboNodeBase.comboEdges[i].childNode).Clone();
        }
    
        comboNodeBase.Init();
    
        return comboNodeBase;
    }

    public float GetRange()
    {
        return attackRange;
    }
}