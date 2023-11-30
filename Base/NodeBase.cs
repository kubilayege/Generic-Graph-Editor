using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class NodeBase : ScriptableObject
{
    [HideInInspector] public string id;
    [HideInInspector] public Vector2 position;
    public object graph;

    [HideInInspector]public List<EdgeData> edges = new List<EdgeData>();

    public virtual void AddChild(EdgeData edgeData)
    {
        edges.Add(edgeData);
    }
    
    public virtual void RemoveChild(EdgeData edgeData)
    {
        for (int i = 0; i < edges.Count; i++)
        {
            if (edges[i] == edgeData)
            {        
                edges.RemoveAt(i);
                break;
            }
        }
    }
    
    public virtual List<T> GetChildren<T>() where T : EdgeData
    {
        return edges as List<T>;
    }
    
    public virtual T Clone<T>() where T : NodeBase
    {
        T nodeBase = (T)Instantiate(this);
        
        for (int i = 0; i < nodeBase.edges.Count; i++)
        {
            nodeBase.edges[i].childNode = (nodeBase.edges[i].childNode).Clone<T>();
        }

        nodeBase.Init();

        return nodeBase;
    }

    public abstract void Init();
}