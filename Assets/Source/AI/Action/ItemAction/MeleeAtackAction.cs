using KMath;
using Item;
using NodeSystem;
using Planet;

namespace Action
{
    public class MeleeAtackAction
    {
        public NodeState Action(object ptr, int index)
        {
            ref PlanetState planet = ref GameState.Planet;
            ref NodesExecutionState stateData = ref NodesExecutionState.GetRef((ulong)ptr);
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(stateData.AgentID);
            ItemInventoryEntity itemEntity = agentEntity.GetItem();
            int damage = 0;
            float range = 0;
            if (itemEntity == null)
            {
                FireWeaponPropreties WeaponProperty = GameState.ItemCreationApi.GetWeapon(itemEntity.itemType.Type);
                damage = WeaponProperty.BasicDemage;
                range = WeaponProperty.Range;
                agentEntity.SwordSlash();
            }
            else 
            {
                agentEntity.MonsterAttack(1.0f, 2.0f);
            }

            // Check if attack has hit an enemy.
            var agents = planet.EntitasContext.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentID));

            var physicsState = agentEntity.agentPhysicsState;
            foreach (var agent in agents)
            {
                if (agent.agentID.ID != agentEntity.agentID.ID || agent.isAgentAlive || agent.agentID.Faction != agentEntity.agentID.Faction)
                    continue;
                var agentPhysicsState = agent.agentPhysicsState;

                //TODO(): not good we need collision checks
                if (Vec2f.Distance(agentPhysicsState.Position, physicsState.Position) <= range)
                {
                    Vec2f direction = physicsState.Position - agentPhysicsState.Position;
                    int KnockbackDir = 0;
                    if (direction.X > 0)
                    {
                        KnockbackDir = 1;
                    }
                    else if (direction.X < 0)
                    {
                        KnockbackDir = -1;
                    }
                    direction.Y = 0;
                    direction.Normalize();

                    agent.Knockback(7.0f, -KnockbackDir);

                    // spawns a debug floating text for damage 
                    planet.AddFloatingText("" + damage, 0.5f, new Vec2f(direction.X * 0.05f, direction.Y * 0.05f), 
                    new Vec2f(agentPhysicsState.Position.X, agentPhysicsState.Position.Y + 0.35f));

                    agent.agentStats.Health -= damage;
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
}
