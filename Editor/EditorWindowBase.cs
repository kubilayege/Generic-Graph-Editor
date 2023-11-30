using System;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UIElements;

public class EditorWindowBase<T1, T2, T3, T4, T5> : EditorWindow where T1 : NodeBase
                                                                 where T2 : EditorWindow 
                                                                 where T3 : NodeViewBase
                                                                 where T4 : GraphViewBase<T1, T3>
                                                                 where T5 : InspectorViewBase<T1>
{
    public static T4 GraphViewBase { get; protected set; }
    public static T5 InspectorViewBase { get; protected set; }
    
    protected string UxmlPath;
    protected string USSPath;
    
    public static void OpenWindow(Graph<T1> graph)
    {
        var window = GetWindow<T2>();
        window.titleContent = new GUIContent("" + graph.name);
        GraphViewBase.PopulateView(graph);
    }

    public virtual void SetPaths()
    {
        UxmlPath = "Assets/Scripts/Graph/Base/DefaultEditorWindow.uxml";
        USSPath = "Assets/Scripts/Graph/Base/DefaultEditorWindow.uss";
    }
    
    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;
        
        SetPaths();

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(UxmlPath);
        
        if (visualTree == null)
        {
            Debug.LogWarning("Check UXML, USS files.");
            return;
        }
        
        visualTree.CloneTree(root);

        // A stylesheet can be added to a VisualElement.
        // The style will be applied to the VisualElement and all of its children.
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(USSPath);
        root.styleSheets.Add(styleSheet);

        FindVisualObjects(root);
        GraphViewBase.OnNodeSelected = OnNodeSelectionChanged;
    }

    public virtual void FindVisualObjects(VisualElement root)
    {
        GraphViewBase = root.Q<T4>();
        InspectorViewBase = root.Q<T5>();
    }

    private void OnNodeSelectionChanged(NodeViewBase nodeView)
    {
        InspectorViewBase.UpdateSelection(nodeView);
    }
}