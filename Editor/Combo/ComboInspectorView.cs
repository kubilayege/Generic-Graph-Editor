using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class ComboInspectorView : InspectorViewBase<ComboNodeBase> 
{
    public new class UxmlFactory : UxmlFactory<ComboInspectorView, UxmlTraits> {}
    
    public ComboInspectorView() { }
    public override void UpdateSelection(NodeViewBase nodeView)
    {
        base.UpdateSelection(nodeView);

        var comboNodeBase = ((ComboNodeBase)nodeView.Node);
        editor = Editor.CreateEditor(comboNodeBase);
        IMGUIContainer container = new IMGUIContainer((() => { editor.OnInspectorGUI(); }));
        Add(container);
        
        
        // editor.HasPreviewGUI();
        //
        //
        //
        // var comboAttackBase = ( comboNodeBase as ComboAttackBase);
        // if (comboAttackBase == null || comboAttackBase.character == null || comboAttackBase.animationClip == null) return;
        //
        // IMGUIContainer animationContainer = new IMGUIContainer((() =>
        // {
        //     
        //     AnimationMode.StartAnimationMode();
        //     AnimationMode.BeginSampling();
        //     AnimationMode.SampleAnimationClip(comboAttackBase.character,comboAttackBase.animationClip, comboAttackBase.animationClip.length);
        //     editor.DrawPreview(new Rect(0,0,500,500));
        // }));
        // Add(animationContainer);
        // Debug.Log("Animation");
    }
}