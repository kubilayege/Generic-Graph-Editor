using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine.UIElements;

public class ComboEditorWindow : EditorWindowBase<ComboNodeBase, ComboEditorWindow, NodeViewBase, ComboGraphView, ComboInspectorView>
{
    public override void SetPaths()
    {
        UxmlPath = "Assets/Scripts/Graph/ComboGraph/ComboEditorWindow.uxml";
        USSPath = "Assets/Scripts/Graph/ComboGraph/ComboEditorWindow.uss";
    }
    
    [OnOpenAsset]
    public static bool OnOpenAsset(int instanceID, int line)
    {
        var graph = Selection.activeObject as ComboGraph;
        
        if (graph != null)
        {
            OpenWindow(graph);
            
            return true;
        }
        
        return false;
    }
}