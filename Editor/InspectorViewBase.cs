using UnityEditor;
using UnityEngine.UIElements;

public class InspectorViewBase<T> : VisualElement where T : NodeBase
{
    // public new class UxmlFactory : UxmlFactory<InspectorViewBase<T>, UxmlTraits> {}

    public InspectorViewBase() {}
    protected Editor editor;

    public virtual void UpdateSelection(NodeViewBase nodeView)
    {
        Clear();
        
        UnityEngine.Object.DestroyImmediate(editor);
    }
}