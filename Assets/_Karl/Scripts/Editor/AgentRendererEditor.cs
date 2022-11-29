using UnityEditor;

[CustomEditor(typeof(AgentRenderer))]
public class AgentRendererEditor : BaseEditor<AgentRenderer>
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUIHelper.Draw(target.GetAgent());
    }
}
