using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(UIContentElementInventorySlot))]
public class UIContentElementInventorySlotEditor : BaseEditor<UIContentElementInventorySlot>
{
    private bool _drawSlot = true;
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        _drawSlot = EditorGUIHelper.Draw(target.GetSlot(), "Slot", _drawSlot);
    }
}
