using UnityEngine.UIElements;

public class SplitViewBase : TwoPaneSplitView
{
    public new class UxmlFactory : UxmlFactory<SplitViewBase, UxmlTraits> {}

    public SplitViewBase() {}
}