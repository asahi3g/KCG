using System;
using System.Collections.Generic;
using System.Globalization;
using Agent;
using UnityEditor;
using UnityEngine;

public static class EditorGUIHelper
{
    
    private static bool _showID = false;
    private static bool _showPhysics = false;
    private static bool _showPhysicsAnimations = true;
    private static bool _showStats = false;
    private static bool _showInventory = false;

    private static GUIStyle _style;
    private static readonly Color NumberColor = new Color(0.5f, 0.7f, 1f, 1f);
    private static readonly Color EnumColor = Color.Lerp(Color.red, Color.yellow, 0.5f);

    private static GUIStyle GetStyle()
    {
        if (_style == null)
        {
            _style = new GUIStyle();
            _style.normal.textColor = Color.white;
            _style.richText = true;
        }
        return _style;
    }

    public static void Draw(AgentEntity agentEntity)
    {
        if (agentEntity == null)
        {
            GUILayout.Box("No Agent");
        }
        else
        {
            // ID
            _showID = EditorGUILayout.Foldout(_showID, "ID");
            if (_showID)
            {
                IDComponent agentID = agentEntity.agentID;
                
                EditorGUI.indentLevel++;
                EditorGUILayout.LabelField($"{nameof(agentID.ID)}: {agentID.ID.ToStringPretty()}", GetStyle());
                EditorGUILayout.LabelField($"{nameof(agentID.Type)}: {agentID.Type.ToStringPretty()}", GetStyle());
                EditorGUILayout.LabelField($"{nameof(agentID.Faction)}: {agentID.Faction.ToStringPretty()}", GetStyle());
                EditorGUILayout.LabelField($"{nameof(agentID.Index)}: {agentID.Index.ToStringPretty()}", GetStyle());
                EditorGUI.indentLevel--;
            }
            
            // Physics
            _showPhysics = EditorGUILayout.Foldout(_showPhysics, "Physics");
            if (_showPhysics)
            {
                EditorGUI.indentLevel++;
                
                if (agentEntity.hasAgentPhysicsState)
                {
                    PhysicsStateComponent physics = agentEntity.agentPhysicsState;

                    EditorGUILayout.LabelField($"{nameof(physics.FacingDirection)}: {physics.FacingDirection.ToStringPretty()}", GetStyle());
                    EditorGUILayout.LabelField($"{nameof(physics.OnGrounded)}: {physics.OnGrounded.ToStringPretty()}", GetStyle());
                    EditorGUILayout.LabelField($"{nameof(physics.MovementState)}: {physics.MovementState.ToStringPretty()}", GetStyle());
                    EditorGUILayout.LabelField($"{nameof(physics.Speed)}: {physics.Speed.ToStringPretty()}", GetStyle());
                    EditorGUILayout.LabelField($"{nameof(physics.Velocity)}: {physics.Velocity.ToStringPretty()}", GetStyle());
                    EditorGUILayout.LabelField($"{nameof(physics.MovingDirection)}: {physics.MovingDirection.ToStringPretty()}", GetStyle());
                    EditorGUILayout.LabelField($"{nameof(physics.InitialJumpVelocity)}: {physics.InitialJumpVelocity.ToStringPretty()}", GetStyle());
                    EditorGUILayout.LabelField($"{nameof(physics.JumpCounter)}: {physics.JumpCounter.ToStringPretty()}", GetStyle());
                    EditorGUILayout.LabelField($"{nameof(physics.Droping)}: {physics.Droping.ToStringPretty()}", GetStyle());
                    EditorGUILayout.LabelField($"{nameof(physics.Acceleration)}: {physics.Acceleration.ToStringPretty()}", GetStyle());
                    EditorGUILayout.LabelField($"{nameof(physics.Invulnerable)}: {physics.Invulnerable.ToStringPretty()}", GetStyle());
                    EditorGUILayout.LabelField($"{nameof(physics.Position)}: {physics.Position.ToStringPretty()}", GetStyle());
                    EditorGUILayout.LabelField($"{nameof(physics.ActionDuration)}: {physics.ActionDuration.ToStringPretty()}", GetStyle());
                    EditorGUILayout.LabelField($"{nameof(physics.DashCooldown)}: {physics.DashCooldown.ToStringPretty()}", GetStyle());
                    EditorGUILayout.LabelField($"{nameof(physics.DashDuration)}: {physics.DashDuration.ToStringPretty()}", GetStyle());
                    EditorGUILayout.LabelField($"{nameof(physics.GroundNormal)}: {physics.GroundNormal.ToStringPretty()}", GetStyle());
                    EditorGUILayout.LabelField($"{nameof(physics.RollCooldown)}: {physics.RollCooldown.ToStringPretty()}", GetStyle());
                    EditorGUILayout.LabelField($"{nameof(physics.SlidingTime)}: {physics.SlidingTime.ToStringPretty()}", GetStyle());
                    EditorGUILayout.LabelField($"{nameof(physics.StaggerDuration)}: {physics.StaggerDuration.ToStringPretty()}", GetStyle());
                    EditorGUILayout.LabelField($"{nameof(physics.ActionInProgress)}: {physics.ActionInProgress.ToStringPretty()}", GetStyle());
                    EditorGUILayout.LabelField($"{nameof(physics.AffectedByFriction)}: {physics.AffectedByFriction.ToStringPretty()}", GetStyle());
                    EditorGUILayout.LabelField($"{nameof(physics.AffectedByGravity)}: {physics.AffectedByGravity.ToStringPretty()}", GetStyle());

                    _showPhysicsAnimations = EditorGUILayout.Foldout(_showPhysicsAnimations, "Animations");
                    if (_showPhysicsAnimations)
                    {
                        AgentAnimation anim = physics.LastAgentAnimation;

                        EditorGUI.indentLevel++;
                        EditorGUILayout.LabelField($"{nameof(anim.Animation)}: {anim.Animation.ToStringPretty()}", GetStyle());
                        EditorGUILayout.LabelField($"{nameof(anim.Looping)}: {anim.Looping.ToStringPretty()}", GetStyle());
                        EditorGUILayout.LabelField($"{nameof(anim.Speed)}: {anim.Speed.ToStringPretty()}", GetStyle());
                        EditorGUILayout.LabelField($"{nameof(anim.AnimationId)}: {anim.AnimationId.ToStringPretty()}", GetStyle());
                        EditorGUILayout.LabelField($"{nameof(anim.FadeTime)}: {anim.FadeTime.ToStringPretty()}", GetStyle());
                        EditorGUILayout.LabelField($"{nameof(anim.StartTime)}: {anim.StartTime.ToStringPretty()}", GetStyle());
                        EditorGUILayout.LabelField($"{nameof(anim.MovementSpeedFactor)}: {anim.MovementSpeedFactor.ToStringPretty()}", GetStyle());
                        EditorGUILayout.LabelField($"{nameof(anim.UseActionDurationForSpeed)}: {anim.UseActionDurationForSpeed.ToStringPretty()}", GetStyle());
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
                StatsComponent stats = agentEntity.agentStats;
                
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
                
                if (agentEntity.hasAgentInventory)
                {
                    InventoryComponent inventory = agentEntity.agentInventory;
                    
                    EditorGUILayout.LabelField($"{nameof(inventory.InventoryID)}: {inventory.InventoryID.ToStringPretty()}", GetStyle());
                    EditorGUILayout.LabelField($"{nameof(inventory.EquipmentInventoryID)}: {inventory.EquipmentInventoryID.ToStringPretty()}", GetStyle());
                    EditorGUILayout.LabelField($"{nameof(inventory.AutoPick)}: {inventory.AutoPick.ToStringPretty()}", GetStyle());
                }
                else
                {
                    EditorGUILayout.LabelField("None");
                }
                
                EditorGUI.indentLevel--;
            }
        }
    }
    
    
    public static void DrawProgress(string title, ContainerFloat c)
    {
        Rect r = EditorGUILayout.BeginVertical();
        r.height = 16;
        EditorGUI.ProgressBar(r, c.GetValueNormalized(), $"{title} ({c.GetValue()}/{c.GetMax()}) {c.GetPercentage()}%");
        EditorGUILayout.EndVertical();
        GUILayout.Space(20);
    }
}
