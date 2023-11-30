using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Graph/Combo Graph")]
public class ComboGraph : Graph<ComboNodeBase>
{
    public ComboNodeBase rootNode;
    [NonSerialized] public ComboNodeBase currentNode;

    public override ComboNodeBase CreateNode(Type type)
    {
        var comboNode = base.CreateNode(type);

        var candidateRootNode = comboNode.GetType() == typeof(ComboStartNode);
        
        if (candidateRootNode)
        {
            if (!rootNode)
            {
                rootNode = comboNode;
                currentNode = rootNode;
            }
        }

        return comboNode;
    }

    public ComboAttackBase Attack(AttackType attackType)
    {
        if (!currentNode) currentNode = rootNode;
        
        var currentComboCallBack = currentNode.Attack(attackType);

        if (!currentComboCallBack.hasChild)
        {
            currentNode = rootNode;

            currentComboCallBack = currentNode.Attack(attackType);
        }

        currentNode = currentComboCallBack.comboNodeBase;

        return currentNode as ComboAttackBase;
    }

    public override List<T> GetChildren<T>(ComboNodeBase parent)
    {
        return parent.GetChildren<T>();
    }

    public ComboHoldNode Hold(AttackType attackType)
    {
        if (!currentNode) currentNode = rootNode;
        
        var currentComboCallBack = currentNode.Attack(attackType);

        if (!currentComboCallBack.hasChild)
        {
            currentNode = rootNode;
            
            currentComboCallBack = currentNode.Attack(attackType);
        }
        
        currentNode = currentComboCallBack.comboNodeBase;

        
        return currentNode as ComboHoldNode;
    }

    public void ResetCurrentCombo()
    {
        currentNode = rootNode;
    }

    public ComboGraph Clone()
    {
        ComboGraph comboGraph = Instantiate(this);
        comboGraph.rootNode = comboGraph.rootNode.Clone();

        var paths = GetPaths(new List<List<ComboEdgeData>>(), new List<ComboEdgeData>(), rootNode);

        foreach (var path in paths)
        {
            string comboWord = "";

            comboWord += comboGraph.name + " : ";
            
            for (var index = 0; index < path.Count; index++)
            {
                var edgeData = path[index];
                comboWord += $"{edgeData.AttackType}";

                if (index != path.Count - 1)
                {
                    comboWord += " -> ";
                }
            }

            Debug.Log(comboWord);
        }
        
        return comboGraph;
    }

    public List<List<ComboEdgeData>> GetCombos()
    {
         return GetPaths(new List<List<ComboEdgeData>>(), new List<ComboEdgeData>(), rootNode);
    }
    
    private List<List<ComboEdgeData>> GetPaths(List<List<ComboEdgeData>> paths, List<ComboEdgeData> currentPath, ComboNodeBase comboNodeBase)
    {
        foreach (var comboEdgeData in comboNodeBase.comboEdges)
        {
            List<ComboEdgeData> comboEdgeDatas = new List<ComboEdgeData>(currentPath);
            comboEdgeDatas.Add(comboEdgeData);
            paths = (GetPaths(paths, comboEdgeDatas,  ((ComboNodeBase) comboEdgeData.childNode)));
        }

        if (comboNodeBase.comboEdges.Count == 0)
        {
            paths.Add(currentPath);
        }
        
        return paths;
    }
}