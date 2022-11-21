using Agent;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CharacterRenderer))]
public class CharacterRendererEditor : BaseEditor<CharacterRenderer>
{

    private bool _showAgentID = false;
    private bool _showAgentStats = false;
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        AgentEntity agent = target.GetAgent();
        if (agent == null)
        {
            GUILayout.Box("No Agent");
        }
        else
        {

            _showAgentID = EditorGUILayout.Foldout(_showAgentID, "ID");
            if (_showAgentID)
            {
                IDComponent agentID = agent.agentID;
                
                GUILayout.BeginVertical();
                GUILayout.Label($"{nameof(agentID.ID)}: {agentID.ID}");
                GUILayout.Label($"{nameof(agentID.Type)}: {agentID.Type}");
                GUILayout.Label($"{nameof(agentID.Faction)}: {agentID.Faction}");
                GUILayout.Label($"{nameof(agentID.Index)}: {agentID.Index}");
                GUILayout.EndVertical();
            }
            
            _showAgentStats = EditorGUILayout.BeginFoldoutHeaderGroup(_showAgentStats, "Stats");
            if (_showAgentStats)
            {
                StatsComponent stats = agent.agentStats;
                
                DrawProgress(nameof(stats.Food), stats.Food);
                DrawProgress(nameof(stats.Fuel), stats.Fuel);
                DrawProgress(nameof(stats.Water), stats.Water);
                DrawProgress(nameof(stats.Oxygen), stats.Oxygen);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }
    }

    private void DrawProgress(string title, ContainerFloat c)
    {
        Rect r = EditorGUILayout.BeginVertical();
        r.height = 16;
        EditorGUI.ProgressBar(r, c.GetValueNormalized(), $"{title} ({c.GetValue()}/{c.GetMax()}) {c.GetPercentage()}%");
        EditorGUILayout.EndVertical();
        GUILayout.Space(20);
    }
}
