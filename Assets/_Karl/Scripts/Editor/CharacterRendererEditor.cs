using System;
using System.Globalization;
using Agent;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CharacterRenderer))]
public class CharacterRendererEditor : BaseEditor<CharacterRenderer>
{

    private bool _showID = false;
    private bool _showPhysics = false;
    private bool _showPhysicsAnimations = true;
    private bool _showStats = false;
    private bool _showInventory = false;

    private GUIStyle _style;
    private readonly Color NumberColor = new Color(0.5f, 0.7f, 1f, 1f);
    private readonly Color EnumColor = Color.Lerp(Color.red, Color.yellow, 0.5f);

    private GUIStyle GetStyle()
    {
        if (_style == null)
        {
            _style = new GUIStyle();
            _style.normal.textColor = Color.white;
            _style.richText = true;
        }
        return _style;
    }
    
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
            // ID
            _showID = EditorGUILayout.Foldout(_showID, "ID");
            if (_showID)
            {
                IDComponent agentID = agent.agentID;
                
                EditorGUI.indentLevel++;
                EditorGUILayout.LabelField($"{nameof(agentID.ID)}: {Value(agentID.ID)}", GetStyle());
                EditorGUILayout.LabelField($"{nameof(agentID.Type)}: {Value(agentID.Type)}", GetStyle());
                EditorGUILayout.LabelField($"{nameof(agentID.Faction)}: {Value(agentID.Faction)}", GetStyle());
                EditorGUILayout.LabelField($"{nameof(agentID.Index)}: {Value(agentID.Index)}", GetStyle());
                EditorGUI.indentLevel--;
            }
            
            // Physics
            _showPhysics = EditorGUILayout.Foldout(_showPhysics, "Physics");
            if (_showPhysics)
            {
                EditorGUI.indentLevel++;
                
                if (agent.hasAgentPhysicsState)
                {
                    PhysicsStateComponent physics = agent.agentPhysicsState;

                    EditorGUILayout.LabelField($"{nameof(physics.FacingDirection)}: {Value(physics.FacingDirection)}", GetStyle());
                    EditorGUILayout.LabelField($"{nameof(physics.OnGrounded)}: {Value(physics.OnGrounded)}", GetStyle());
                    EditorGUILayout.LabelField($"{nameof(physics.MovementState)}: {Value(physics.MovementState)}", GetStyle());
                    EditorGUILayout.LabelField($"{nameof(physics.Speed)}: {Value(physics.Speed)}", GetStyle());
                    EditorGUILayout.LabelField($"{nameof(physics.Velocity)}: {Value(physics.Velocity)}", GetStyle());
                    EditorGUILayout.LabelField($"{nameof(physics.MovingDirection)}: {Value(physics.MovingDirection)}", GetStyle());
                    EditorGUILayout.LabelField($"{nameof(physics.InitialJumpVelocity)}: {Value(physics.InitialJumpVelocity)}", GetStyle());
                    EditorGUILayout.LabelField($"{nameof(physics.JumpCounter)}: {Value(physics.JumpCounter)}", GetStyle());
                    EditorGUILayout.LabelField($"{nameof(physics.Droping)}: {Value(physics.Droping)}", GetStyle());
                    EditorGUILayout.LabelField($"{nameof(physics.Acceleration)}: {Value(physics.Acceleration)}", GetStyle());
                    EditorGUILayout.LabelField($"{nameof(physics.Invulnerable)}: {Value(physics.Invulnerable)}", GetStyle());
                    EditorGUILayout.LabelField($"{nameof(physics.Position)}: {Value(physics.Position)}", GetStyle());
                    EditorGUILayout.LabelField($"{nameof(physics.ActionDuration)}: {Value(physics.ActionDuration)}", GetStyle());
                    EditorGUILayout.LabelField($"{nameof(physics.DashCooldown)}: {Value(physics.DashCooldown)}", GetStyle());
                    EditorGUILayout.LabelField($"{nameof(physics.DashDuration)}: {Value(physics.DashDuration)}", GetStyle());
                    EditorGUILayout.LabelField($"{nameof(physics.GroundNormal)}: {Value(physics.GroundNormal)}", GetStyle());
                    EditorGUILayout.LabelField($"{nameof(physics.RollCooldown)}: {Value(physics.RollCooldown)}", GetStyle());
                    EditorGUILayout.LabelField($"{nameof(physics.SlidingTime)}: {Value(physics.SlidingTime)}", GetStyle());
                    EditorGUILayout.LabelField($"{nameof(physics.StaggerDuration)}: {Value(physics.StaggerDuration)}", GetStyle());
                    EditorGUILayout.LabelField($"{nameof(physics.ActionInProgress)}: {Value(physics.ActionInProgress)}", GetStyle());
                    EditorGUILayout.LabelField($"{nameof(physics.AffectedByFriction)}: {Value(physics.AffectedByFriction)}", GetStyle());
                    EditorGUILayout.LabelField($"{nameof(physics.AffectedByGravity)}: {Value(physics.AffectedByGravity)}", GetStyle());

                    _showPhysicsAnimations = EditorGUILayout.Foldout(_showPhysicsAnimations, "Animations");
                    if (_showPhysicsAnimations)
                    {
                        AgentAnimation anim = physics.LastAgentAnimation;

                        EditorGUI.indentLevel++;
                        EditorGUILayout.LabelField($"{nameof(anim.Animation)}: {Value(anim.Animation)}", GetStyle());
                        EditorGUILayout.LabelField($"{nameof(anim.Looping)}: {Value(anim.Looping)}", GetStyle());
                        EditorGUILayout.LabelField($"{nameof(anim.Speed)}: {Value(anim.Speed)}", GetStyle());
                        EditorGUILayout.LabelField($"{nameof(anim.AnimationId)}: {Value(anim.AnimationId)}", GetStyle());
                        EditorGUILayout.LabelField($"{nameof(anim.FadeTime)}: {Value(anim.FadeTime)}", GetStyle());
                        EditorGUILayout.LabelField($"{nameof(anim.StartTime)}: {Value(anim.StartTime)}", GetStyle());
                        EditorGUILayout.LabelField($"{nameof(anim.MovementSpeedFactor)}: {Value(anim.MovementSpeedFactor)}", GetStyle());
                        EditorGUILayout.LabelField($"{nameof(anim.UseActionDurationForSpeed)}: {Value(anim.UseActionDurationForSpeed)}", GetStyle());
                        EditorGUI.indentLevel--;
                    }
                }
                else
                {
                    EditorGUILayout.LabelField("None");
                }
                
                EditorGUI.indentLevel--;
            }

            // Stats
            _showStats = EditorGUILayout.Foldout(_showStats, "Stats");
            if (_showStats)
            {
                StatsComponent stats = agent.agentStats;
                
                EditorGUI.indentLevel++;
                DrawProgress(nameof(stats.Food), stats.Food);
                DrawProgress(nameof(stats.Fuel), stats.Fuel);
                DrawProgress(nameof(stats.Water), stats.Water);
                DrawProgress(nameof(stats.Oxygen), stats.Oxygen);
                EditorGUI.indentLevel--;
            }

            // Inventory
            _showInventory = EditorGUILayout.Foldout(_showInventory, "Inventory");
            if (_showInventory)
            {
                EditorGUI.indentLevel++;
                
                if (agent.hasAgentInventory)
                {
                    InventoryComponent inventory = agent.agentInventory;
                    
                    EditorGUILayout.LabelField($"{nameof(inventory.InventoryID)}: {Value(inventory.InventoryID)}", GetStyle());
                    EditorGUILayout.LabelField($"{nameof(inventory.EquipmentInventoryID)}: {Value(inventory.EquipmentInventoryID)}", GetStyle());
                    EditorGUILayout.LabelField($"{nameof(inventory.AutoPick)}: {Value(inventory.AutoPick)}", GetStyle());
                }
                else
                {
                    EditorGUILayout.LabelField("None");
                }
                
                EditorGUI.indentLevel--;
            }
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

    private string Value(System.Object obj)
    {
        if (obj == null) return "null";
        
        if (obj is bool b)
        {
            return b.ToString().Color(b ? Color.green : Color.red);
        }

        if (obj is Enum e)
        {
            return $"[{e.ToString().Color(EnumColor)}]";
        }

        if (obj is int i)
        {
            return i.ToString().Color(NumberColor);
        }
        
        if (obj is float f)
        {
            return $"{f.ToString(CultureInfo.InvariantCulture)}f".Color(NumberColor);
        }
        
        return obj.ToString();
    }
}
