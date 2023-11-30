using System;
using System.Collections.Generic;
using UnityEngine;

public class Graph<T> : ScriptableObject where T : NodeBase
{
    public List<T> NodeBases = new List<T>();
    
    public virtual T CreateNode(Type type)
    {
        if (type == null) return null;
        var instance = ScriptableObject.CreateInstance(type) as T;
        instance.name = type.Name;
        instance.id = AssetManager.GenerateAndGetGUID();
        instance.graph = this;
        NodeBases.Add(instance);

        AssetManager.CreateAssetAndAddObject(instance, this, true);
        
        return instance;
    }

    public void RemoveNode(T node)
    {
        NodeBases.Remove(node);

        AssetManager.RemoveFromAsset(node, true);
    }

    public void AddChild(T parent, EdgeData edgeData)
    {
        parent.AddChild(edgeData);
        
        AssetManager.Save(parent);
    }

    public void RemoveChild(T parent, EdgeData edgeData)
    {
        parent.RemoveChild(edgeData);
        
        AssetManager.Save(parent);
    }
    
    
    public virtual List<T1> GetChildren<T1>(T parent) where T1 : EdgeData
    {
        return parent.GetChildren<T1>();
    }
}