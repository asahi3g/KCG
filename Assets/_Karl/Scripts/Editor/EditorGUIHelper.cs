using System;
using System.Collections.Generic;
using Agent;
using Inventory;
using Item;
using UnityEditor;
using UnityEngine;
using Utility;
using InventoryComponent = Agent.InventoryComponent;
using PhysicsStateComponent = Agent.PhysicsStateComponent;

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
    private static bool _showInventoryEntity = true;
    private static bool _showInventoryEntityBitSet = false;
    private static bool _showInventoryEntitySlots = false;
    private static Flag2Map _showInventoryEntitySlotsEntry = new Flag2Map();
    private static Flag2Map _showInventoryEntitySlotsEntryItem = new Flag2Map();
    private static Flag1Map _showItemIdComponent = new Flag1Map();
    private static Flag1Map _showItemTypeComponent = new Flag1Map();

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
            DrawNone();
        }
        else
        {
            // ID
            _showID = EditorGUILayout.Foldout(_showID, $"{nameof(Agent.IDComponent)}");
            if (_showID)
            {
                EditorGUI.indentLevel++;
                Draw(agentEntity.agentID);
                EditorGUI.indentLevel--;
            }
            
            // 3D Model
            _model3D = EditorGUILayout.Foldout(_model3D, $"{nameof(Agent3DModel)}");
            if (_model3D)
            {
                EditorGUI.indentLevel++;
                if (agentEntity.hasAgent3DModel) Draw(agentEntity.Agent3DModel);
                else DrawNone();
                EditorGUI.indentLevel--;
            }

            // Action
            _action = EditorGUILayout.Foldout(_action, $"{nameof(ActionComponent)}");
            if (_action)
            {
                EditorGUI.indentLevel++;
                if (agentEntity.hasAgentAction) Draw(agentEntity.agentAction);
                else DrawNone();
                EditorGUI.indentLevel--;
            }

            // Controller
            _controller = EditorGUILayout.Foldout(_controller, $"{nameof(ControllerComponent)}");
            if (_controller)
            {
                EditorGUI.indentLevel++;
                if (agentEntity.hasAgentController) Draw(agentEntity.agentController);
                else DrawNone();
                EditorGUI.indentLevel--;
            }

            // Stagger
            _stagger = EditorGUILayout.Foldout(_stagger, $"{nameof(StaggerComponent)}");
            if (_stagger)
            {
                EditorGUI.indentLevel++;
                if (agentEntity.hasAgentStagger) Draw(agentEntity.agentStagger);
                else DrawNone();
                EditorGUI.indentLevel--;
            }

            // Physics
            _showPhysics = EditorGUILayout.Foldout(_showPhysics, $"{nameof(PhysicsStateComponent)}");
            if (_showPhysics)
            {
                EditorGUI.indentLevel++;
                if (agentEntity.hasAgentPhysicsState)
                {
                    Draw(agentEntity.agentPhysicsState);

                    _showPhysicsAnimations = EditorGUILayout.Foldout(_showPhysicsAnimations, $"{nameof(AgentAnimation)}");
                    if (_showPhysicsAnimations)
                    {
                        EditorGUI.indentLevel++;
                        Draw(agentEntity.agentPhysicsState.LastAgentAnimation);
                        EditorGUI.indentLevel--;
                    }
                }
                else DrawNone();
                EditorGUI.indentLevel--;
            }

            // Stats
            _showStats = EditorGUILayout.Foldout(_showStats, $"{nameof(StatsComponent)}");
            if (_showStats)
            {
                EditorGUI.indentLevel++;
                Draw(agentEntity.agentStats);
                EditorGUI.indentLevel--;
            }

            // Inventory
            _showInventory = EditorGUILayout.Foldout(_showInventory, $"{nameof(InventoryComponent)}");
            if (_showInventory)
            {
                EditorGUI.indentLevel++;
                if (agentEntity.hasAgentInventory) Draw(agentEntity.agentInventory);
                else DrawNone();
                EditorGUI.indentLevel--;
            }
        }
    }

    public static void DrawNone()
    {
        EditorGUILayout.LabelField("None");
    }

    public static void Draw(Agent.IDComponent value)
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

        _showInventoryEntity = EditorGUILayout.Foldout(_showInventory, $"{nameof(InventoryEntityComponent)}");
        if (_showInventoryEntity)
        {
            EditorGUI.indentLevel++;
            InventoryEntityComponent inventoryEntityComponent = GameState.Planet.GetInventoryEntityComponent(value.InventoryID);
            Draw(inventoryEntityComponent);
            EditorGUI.indentLevel--;
        }
    }
    
    public static void Draw(InventoryEntityComponent value)
    {
        EditorGUILayout.LabelField($"{nameof(value.Index)}: {value.Index.ToStringPretty()}", GetStyle());
        EditorGUILayout.LabelField($"{nameof(value.InventoryEntityTemplateID)}: {value.InventoryEntityTemplateID.ToStringPretty()}", GetStyle());
        EditorGUILayout.LabelField($"{nameof(value.SelectedSlotIndex)}: {value.SelectedSlotIndex.ToStringPretty()}", GetStyle());
        EditorGUILayout.LabelField($"{nameof(value.Size)}: {value.Size.ToStringPretty()}", GetStyle());
        EditorGUILayout.LabelField($"{nameof(value.InventoryType)}: {value.InventoryType.ToStringPretty()}", GetStyle());
        
        // Bitset
        _showInventoryEntityBitSet = EditorGUILayout.Foldout(_showInventoryEntityBitSet, $"{nameof(BitSet)}");
        if (_showInventoryEntityBitSet)
        {
            EditorGUI.indentLevel++;
            Draw(value.SlotsMask);
            EditorGUI.indentLevel--;
        }
        
        // Slots
        _showInventoryEntitySlots = EditorGUILayout.Foldout(_showInventoryEntitySlots, $"{nameof(value.Slots)}");
        if (_showInventoryEntitySlots)
        {
            EditorGUI.indentLevel++;
            Draw(value.Slots);
            EditorGUI.indentLevel--;
        }
    }
    
    public static void Draw(Slot[] value)
    {
        if (value == null)
        {
            DrawNone();
            return;
        }
        
        int length = value.Length;
        for (int i = 0; i < length; i++)
        {
            Slot slot = value[i];

            bool foldout = Draw(slot, $"{i}", _showInventoryEntitySlotsEntry.Get(slot.InventoryId, slot.Index));
            _showInventoryEntitySlotsEntry.Set(slot.InventoryId, slot.Index, foldout);
        }
    }

    public static bool Draw(Slot value, string foldoutTitle, bool foldout)
    {
        foldout = EditorGUILayout.Foldout(foldout, foldoutTitle);
        if (foldout)
        {
            EditorGUI.indentLevel++;
            if (value != null)
            {
                Draw(value);
            }
            else
            {
                DrawNone();
            }
            
            EditorGUI.indentLevel--;
        }
        return foldout;
    }

    public static void Draw(Slot value)
    {
        EditorGUILayout.LabelField($"{nameof(value.InventoryId)}: {value.InventoryId.ToStringPretty()}", GetStyle());
        EditorGUILayout.LabelField($"{nameof(value.Index)}: {value.Index.ToStringPretty()}", GetStyle());
        EditorGUILayout.LabelField($"{nameof(value.ItemGroups)}: {value.ItemGroups.ToStringPretty()}", GetStyle());
        EditorGUILayout.LabelField($"{nameof(value.ItemID)}: {value.ItemID.ToStringPretty()}", GetStyle());
        
        bool foldout = EditorGUILayout.Foldout(_showInventoryEntitySlotsEntryItem.Get(value.InventoryId, value.Index), $"Item");
        if (_showInventoryEntitySlotsEntryItem.Set(value.InventoryId, value.Index, foldout))
        {
            EditorGUI.indentLevel++;
            if (GameState.Planet.GetItemInventoryEntity(value, out ItemInventoryEntity itemInventoryEntity))
            {
                Draw(itemInventoryEntity);
            }
            else
            {
                DrawNone();
            }
            EditorGUI.indentLevel--;
        }
    }

    public static void Draw(ItemInventoryEntity value)
    {
        Draw(value.itemID);
        Draw(value.itemType);
        
        ItemProperties itemProperties = GameState.ItemCreationApi.GetItemProperties(value.itemType.Type);
        
        
    }

    public static void Draw(Item.IDComponent idComponent)
    {
        if (idComponent == null)
        {
            EditorGUILayout.LabelField($"{nameof(Item.IDComponent)} (None)", GetStyle());
            return;
        }
        
        bool foldout = EditorGUILayout.Foldout(_showItemIdComponent.Get(idComponent.ID), $"{nameof(Item.IDComponent)}");
        if (_showItemIdComponent.Set(idComponent.ID, foldout))
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.LabelField($"{nameof(idComponent.ID)}: {idComponent.ID.ToStringPretty()}", GetStyle());
            EditorGUILayout.LabelField($"{nameof(idComponent.Index)}: {idComponent.Index.ToStringPretty()}", GetStyle());
            EditorGUILayout.LabelField($"{nameof(idComponent.ItemName)}: {idComponent.ItemName.ToStringPretty()}", GetStyle());
            EditorGUI.indentLevel--;
        }
    }

    public static void Draw(Item.TypeComponent typeComponent)
    {
        if (typeComponent == null)
        {
            EditorGUILayout.LabelField($"{nameof(Item.TypeComponent)} (None)", GetStyle());
            return;
        }
        
        bool foldout = EditorGUILayout.Foldout(true, $"{nameof(Item.TypeComponent)}");
        if (foldout)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.LabelField($"{nameof(typeComponent.Type)}: {typeComponent.Type.ToStringPretty()}", GetStyle());
            EditorGUI.indentLevel--;
        }
    }

    public static void Draw(BitSet value)
    {
        EditorGUILayout.LabelField($"{nameof(value.Length)}: {value.Length.ToStringPretty()}", GetStyle());
        EditorGUILayout.LabelField($"{nameof(value.BitMask)}: {value.BitMask.ToStringBinary()}", GetStyle());
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
        DrawProgress(nameof(value.Food), new ContainerInt(value.Food.GetValue(), value.Food.GetMin(), value.Food.GetMax()));
        DrawProgress(nameof(value.Fuel), new ContainerInt(value.Fuel.GetValue(), value.Fuel.GetMin(), value.Fuel.GetMax()));
        DrawProgress(nameof(value.Water), new ContainerInt(value.Water.GetValue(), value.Water.GetMin(), value.Water.GetMax()));
        DrawProgress(nameof(value.Oxygen), new ContainerInt(value.Oxygen.GetValue(), value.Oxygen.GetMin(), value.Oxygen.GetMax()));
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
    
    public static void Draw(Agent3DModel value)
    {
        // Renderer
        value.SetRenderer((AgentRenderer)EditorGUILayout
            .ObjectField($"{nameof(value.Renderer)}", value.Renderer, typeof(AgentRenderer), true));
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
        EditorGUILayout.LabelField($"{nameof(value.LocalScale)}: {value.LocalScale.ToStringPretty()}", GetStyle());
        // Aim Target
        EditorGUILayout.LabelField($"{nameof(value.AimTarget)}: {value.AimTarget.ToStringPretty()}", GetStyle());
    }

    public static void DrawProgress(string title, ContainerInt c)
    {
        Rect r = EditorGUILayout.BeginVertical();
        r.height = 16;
        EditorGUI.ProgressBar(r, c.GetValueNormalized(), $"{title} ({c.GetValue()}/{c.GetMax()}) {c.GetPercentage()}%");
        EditorGUILayout.EndVertical();
        GUILayout.Space(20);
    }

    
    private class Flag1Map
    {
        private readonly Dictionary<int, bool> Map = new Dictionary<int, bool>();

        public bool Get(int key)
        {
            if (!Map.ContainsKey(key)) return false;
            return Map[key];
        }

        public bool Set(int key, bool value)
        {
            if (Map.ContainsKey(key))
            {
                Map[key] = value;
            }
            else
            {
                Map.Add(key, value);
            }
            return value;
        }
    }

    private class Flag2Map
    {
        private readonly Dictionary<Tuple<int, int>, bool> Map = new Dictionary<Tuple<int, int>, bool>();

        public bool Get(int keyA, int keyB)
        {
            Tuple<int, int> key = new Tuple<int, int>(keyA, keyB);
            if (!Map.ContainsKey(key)) return false;
            return Map[key];
        }

        public bool Set(int keyA, int keyB, bool value)
        {
            Tuple<int, int> key = new Tuple<int, int>(keyA, keyB);
            if (Map.ContainsKey(key))
            {
                Map[key] = value;
            }
            else
            {
                Map.Add(key, value);
            }
            return value;
        }
    }
}
