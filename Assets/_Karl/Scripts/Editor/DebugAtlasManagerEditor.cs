using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DebugAtlasManager))]
public class DebugAtlasManagerEditor : BaseEditor<DebugAtlasManager>
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (!target.DirectoryExists()) EditorGUILayout.HelpBox("Directory does not exist", MessageType.Warning);
        else EditorGUILayout.HelpBox("Directory exists", MessageType.Info);

        if (GUILayout.Button("Save To Disk"))
        {
            target.Save();
        }
    }
}
