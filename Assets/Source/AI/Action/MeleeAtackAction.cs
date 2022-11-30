using KMath;
using Item;
using NodeSystem;
using Planet;
using Entitas;
using Agent;
using UnityEngine;

namespace Action
{
    // Melee Attack with no weapon equipped.
    // Action used by either player and AI.
    public class BasicMeleeAtackAction
    {
        public static NodeState OnEnter(object ptr, int index)
        {
            ref PlanetState planet = ref GameState.Planet;
            ref NodesExecutionState stateData = ref NodesExecutionState.GetRef((ulong)ptr);
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(stateData.AgentID);
            agentEntity.MonsterAttack(4.0f);

            return NodeState.Running;
        }

        public static NodeState OnUpdate(object ptr, int index)
        {
            ref PlanetState planet = ref GameState.Planet;
            ref NodesExecutionState stateData = ref NodesExecutionState.GetRef((ulong)ptr);
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(stateData.AgentID);
            ref AgentPropertiesTemplate agentProperties = ref GameState.AgentCreationApi.GetRef((int)agentEntity.agentID.Type);

            if (stateData.NodesExecutiondata[index].ExecutionTime <= agentProperties.Attack.Windup)
                return NodeState.Running;

            float range = agentProperties.Attack.Range;
            int damage = agentProperties.Attack.Demage;
            var physicsState = agentEntity.agentPhysicsState;
            for (int i = 0; i < planet.AgentList.Length; i++)
            {
                AgentEntity agent = planet.AgentList.Get(i);
                if (agent.agentID.ID == agentEntity.agentID.ID || !agent.isAgentAlive || agent.agentID.Faction == agentEntity.agentID.Faction)
                    continue;
                var agentPhysicsState = agent.agentPhysicsState;
                Vec2f pos = (physicsState.FacingDirection == -1) ? physicsState.Position :
                    physicsState.Position + new Vec2f(agentEntity.physicsBox2DCollider.Size.X, 0f);

                //TODO(): not good we need collision checks
                if (Vec2f.Distance(pos, physicsState.Position) <= range)
                {
                    int direction = (int)Mathf.Sign(physicsState.Position.X - agentPhysicsState.Position.X);

                    agent.Knockback(7.0f, -direction);

                    // spawns a debug floating text for damage 
                    planet.AddFloatingText("" + damage, 0.5f, new Vec2f(direction * 0.05f, 0.05f),
                    new Vec2f(agentPhysicsState.Position.X, agentPhysicsState.Position.Y + 0.35f));

                    agent.agentStats.Health.Remove(damage);
                }
            }

            var destructableMechs = planet.EntitasContext.mech.GetGroup(MechMatcher.AllOf(MechMatcher.MechDurability));
            foreach (var mech in destructableMechs)
            {
                var testMechPosition = mech.mechPosition2D;

                if (Vec2f.Distance(testMechPosition.Value, physicsState.Position) <= range)
                    mech.mechDurability.Durability -= damage;
            }

            return NodeState.Success;
        }
    }

    // Todo: Add weapon melee atack action.
    // Todo: Move damage out of here. (Create damage event? event input: Damage, knockback?, stunt?)
}
