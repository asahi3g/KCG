using Agent;
using UnityEditor;
using UnityEngine;

public static class EditorGUIHelper
{
    
    private static bool _showID = false;
    private static bool _model3D = false;
    private static bool _action = false;
    private static bool _controller = false;
    private static bool _stagger = false;
    private static bool _showPhysics = false;
    private static bool _showPhysicsAnimations = true;
    private static bool _showStats = false;
    private static bool _showInventory = false;

    private static GUIStyle _style;

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
                EditorGUI.indentLevel++;
                Draw(agentEntity.agentID);
                EditorGUI.indentLevel--;
            }
            
            // 3D Model
            _model3D = EditorGUILayout.Foldout(_model3D, "Model 3D");
            if (_model3D)
            {
                EditorGUI.indentLevel++;
                if (agentEntity.hasAgentModel3D)
                {
                    Draw(agentEntity.agentModel3D);
                }
                else GUILayout.Box("None");
                
                EditorGUI.indentLevel--;
            }

            // Action
            _action = EditorGUILayout.Foldout(_action, "Action");
            if (_action)
            {
                EditorGUI.indentLevel++;
                if (agentEntity.hasAgentAction)
                {
                    Draw(agentEntity.agentAction);
                }
                else GUILayout.Box("None");

                EditorGUI.indentLevel--;
            }

            // Controller
            _controller = EditorGUILayout.Foldout(_controller, "Controller");
            if (_controller)
            {
                EditorGUI.indentLevel++;
                if (agentEntity.hasAgentController)
                {
                    Draw(agentEntity.agentController);
                }
                else GUILayout.Box("None");

                EditorGUI.indentLevel--;
            }

            _stagger = EditorGUILayout.Foldout(_stagger, "Stagger");
            if (_stagger)
            {
                EditorGUI.indentLevel++;
                if (agentEntity.hasAgentStagger)
                {
                    Draw(agentEntity.agentStagger);
                }
                else GUILayout.Box("None");
                EditorGUI.indentLevel--;
            }
            
            
            
            // Physics
            _showPhysics = EditorGUILayout.Foldout(_showPhysics, "Physics");
            if (_showPhysics)
            {
                EditorGUI.indentLevel++;
                
                if (agentEntity.hasAgentPhysicsState)
                {
                    Draw(agentEntity.agentPhysicsState);

                    _showPhysicsAnimations = EditorGUILayout.Foldout(_showPhysicsAnimations, "Animations");
                    if (_showPhysicsAnimations)
                    {

                        EditorGUI.indentLevel++;
                        Draw(agentEntity.agentPhysicsState.LastAgentAnimation);
                        EditorGUI.indentLevel--;
                    }
                }
                else EditorGUILayout.LabelField("None");

                EditorGUI.indentLevel--;
            }

            // Stats
            _showStats = EditorGUILayout.Foldout(_showStats, "Stats");
            if (_showStats)
            {
                EditorGUI.indentLevel++;
                Draw(agentEntity.agentStats);
                EditorGUI.indentLevel--;
            }

            // Inventory
            _showInventory = EditorGUILayout.Foldout(_showInventory, "Inventory");
            if (_showInventory)
            {
                EditorGUI.indentLevel++;
                if (agentEntity.hasAgentInventory) Draw(agentEntity.agentInventory);
                else EditorGUILayout.LabelField("None");
                EditorGUI.indentLevel--;
            }
        }
    }


    public static void Draw(IDComponent value)
    {
        EditorGUILayout.LabelField($"{nameof(value.ID)}: {value.ID.ToStringPretty()}", GetStyle());
        EditorGUILayout.LabelField($"{nameof(value.Type)}: {value.Type.ToStringPretty()}", GetStyle());
        EditorGUILayout.LabelField($"{nameof(value.Faction)}: {value.Faction.ToStringPretty()}", GetStyle());
        EditorGUILayout.LabelField($"{nameof(value.Index)}: {value.Index.ToStringPretty()}", GetStyle());
    }
    
    public static void Draw(InventoryComponent value)
    {
        EditorGUILayout.LabelField($"{nameof(value.InventoryID)}: {value.InventoryID.ToStringPretty()}", GetStyle());
        EditorGUILayout.LabelField($"{nameof(value.EquipmentInventoryID)}: {value.EquipmentInventoryID.ToStringPretty()}", GetStyle());
        EditorGUILayout.LabelField($"{nameof(value.AutoPick)}: {value.AutoPick.ToStringPretty()}", GetStyle());
    }

    public static void Draw(AgentAnimation value)
    {
        EditorGUILayout.LabelField($"{nameof(value.Animation)}: {value.Animation.ToStringPretty()}", GetStyle());
        EditorGUILayout.LabelField($"{nameof(value.Looping)}: {value.Looping.ToStringPretty()}", GetStyle());
        EditorGUILayout.LabelField($"{nameof(value.Speed)}: {value.Speed.ToStringPretty()}", GetStyle());
        EditorGUILayout.LabelField($"{nameof(value.AnimationId)}: {value.AnimationId.ToStringPretty()}", GetStyle());
        EditorGUILayout.LabelField($"{nameof(value.FadeTime)}: {value.FadeTime.ToStringPretty()}", GetStyle());
        EditorGUILayout.LabelField($"{nameof(value.StartTime)}: {value.StartTime.ToStringPretty()}", GetStyle());
        EditorGUILayout.LabelField($"{nameof(value.MovementSpeedFactor)}: {value.MovementSpeedFactor.ToStringPretty()}", GetStyle());
        EditorGUILayout.LabelField($"{nameof(value.UseActionDurationForSpeed)}: {value.UseActionDurationForSpeed.ToStringPretty()}", GetStyle());
    }

    public static void Draw(ActionComponent value)
    {
        EditorGUILayout.LabelField($"{nameof(value.Action)}: {value.Action.ToStringPretty()}", GetStyle());
    }

    public static void Draw(StatsComponent value)
    {
        DrawProgress(nameof(value.Food), value.Food);
        DrawProgress(nameof(value.Fuel), value.Fuel);
        DrawProgress(nameof(value.Water), value.Water);
        DrawProgress(nameof(value.Oxygen), value.Oxygen);
    }

    public static void Draw(PhysicsStateComponent value)
    {
        EditorGUILayout.LabelField($"{nameof(value.FacingDirection)}: {value.FacingDirection.ToStringPretty()}", GetStyle());
        EditorGUILayout.LabelField($"{nameof(value.OnGrounded)}: {value.OnGrounded.ToStringPretty()}", GetStyle());
        EditorGUILayout.LabelField($"{nameof(value.MovementState)}: {value.MovementState.ToStringPretty()}", GetStyle());
        EditorGUILayout.LabelField($"{nameof(value.Speed)}: {value.Speed.ToStringPretty()}", GetStyle());
        EditorGUILayout.LabelField($"{nameof(value.Velocity)}: {value.Velocity.ToStringPretty()}", GetStyle());
        EditorGUILayout.LabelField($"{nameof(value.MovingDirection)}: {value.MovingDirection.ToStringPretty()}", GetStyle());
        EditorGUILayout.LabelField($"{nameof(value.InitialJumpVelocity)}: {value.InitialJumpVelocity.ToStringPretty()}", GetStyle());
        EditorGUILayout.LabelField($"{nameof(value.JumpCounter)}: {value.JumpCounter.ToStringPretty()}", GetStyle());
        EditorGUILayout.LabelField($"{nameof(value.Droping)}: {value.Droping.ToStringPretty()}", GetStyle());
        EditorGUILayout.LabelField($"{nameof(value.Acceleration)}: {value.Acceleration.ToStringPretty()}", GetStyle());
        EditorGUILayout.LabelField($"{nameof(value.Invulnerable)}: {value.Invulnerable.ToStringPretty()}", GetStyle());
        EditorGUILayout.LabelField($"{nameof(value.Position)}: {value.Position.ToStringPretty()}", GetStyle());
        EditorGUILayout.LabelField($"{nameof(value.ActionDuration)}: {value.ActionDuration.ToStringPretty()}", GetStyle());
        EditorGUILayout.LabelField($"{nameof(value.DashCooldown)}: {value.DashCooldown.ToStringPretty()}", GetStyle());
        EditorGUILayout.LabelField($"{nameof(value.DashDuration)}: {value.DashDuration.ToStringPretty()}", GetStyle());
        EditorGUILayout.LabelField($"{nameof(value.GroundNormal)}: {value.GroundNormal.ToStringPretty()}", GetStyle());
        EditorGUILayout.LabelField($"{nameof(value.RollCooldown)}: {value.RollCooldown.ToStringPretty()}", GetStyle());
        EditorGUILayout.LabelField($"{nameof(value.SlidingTime)}: {value.SlidingTime.ToStringPretty()}", GetStyle());
        EditorGUILayout.LabelField($"{nameof(value.StaggerDuration)}: {value.StaggerDuration.ToStringPretty()}", GetStyle());
        EditorGUILayout.LabelField($"{nameof(value.ActionInProgress)}: {value.ActionInProgress.ToStringPretty()}", GetStyle());
        EditorGUILayout.LabelField($"{nameof(value.AffectedByFriction)}: {value.AffectedByFriction.ToStringPretty()}", GetStyle());
        EditorGUILayout.LabelField($"{nameof(value.AffectedByGravity)}: {value.AffectedByGravity.ToStringPretty()}", GetStyle());
    }
    
    public static void Draw(ControllerComponent value)
    {
        EditorGUILayout.LabelField($"{nameof(value.BehaviorTreeId)}: {value.BehaviorTreeId.ToStringPretty()}", GetStyle());
        EditorGUILayout.LabelField($"{nameof(value.BlackboardID)}: {value.BlackboardID.ToStringPretty()}", GetStyle());
        EditorGUILayout.LabelField($"{nameof(value.SensorsID)}: {value.SensorsID.ToStringPretty()}", GetStyle());
        EditorGUILayout.LabelField($"{nameof(value.SquadID)}: {value.SquadID.ToStringPretty()}", GetStyle());
    }

    public static void Draw(StaggerComponent value)
    {
        EditorGUILayout.LabelField($"{nameof(value.Stagger)}: {value.Stagger.ToStringPretty()}", GetStyle());
        EditorGUILayout.LabelField($"{nameof(value.StaggerAffectTime)}: {value.StaggerAffectTime.ToStringPretty()}", GetStyle());
        EditorGUILayout.LabelField($"{nameof(value.elapsed)}: {value.elapsed.ToStringPretty()}", GetStyle());
    }
    
    public static void Draw(Model3DComponent value)
    {
        // GameObject
        value.GameObject = (GameObject)EditorGUILayout
            .ObjectField($"{nameof(value.GameObject)}", value.GameObject, typeof(GameObject), true);
        // LeftHand
        value.LeftHand = (GameObject)EditorGUILayout
            .ObjectField($"{nameof(value.LeftHand)}", value.LeftHand, typeof(GameObject), true);
        // RightHand
        value.RightHand = (GameObject)EditorGUILayout
            .ObjectField($"{nameof(value.RightHand)}", value.RightHand, typeof(GameObject), true);
        // Current Weapon
        EditorGUILayout.LabelField($"{nameof(value.CurrentWeapon)}: {value.CurrentWeapon.ToStringPretty()}", GetStyle());
        // Weapon
        value.Weapon = (GameObject)EditorGUILayout
            .ObjectField($"{nameof(value.Weapon)}", value.Weapon, typeof(GameObject), true);
        // Animation Type
        EditorGUILayout.LabelField($"{nameof(value.AnimationType)}: {value.AnimationType.ToStringPretty()}", GetStyle());
        // Item Animation Set
        EditorGUILayout.LabelField($"{nameof(value.ItemAnimationSet)}: {value.ItemAnimationSet.ToStringPretty()}", GetStyle());
        // Model Scale
        EditorGUILayout.LabelField($"{nameof(value.ModelScale)}: {value.ModelScale.ToStringPretty()}", GetStyle());
        // Aim Target
        EditorGUILayout.LabelField($"{nameof(value.AimTarget)}: {value.AimTarget.ToStringPretty()}", GetStyle());
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
