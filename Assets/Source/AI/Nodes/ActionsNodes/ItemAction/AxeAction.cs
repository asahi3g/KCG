//imports UnityEngine

using KMath;
using Enums;

namespace Node
{
    public class AxeAction : NodeBase
    {
        public override NodeType Type => NodeType.AxeAction;

        public override void OnEnter(NodeEntity nodeEntity)
        {
            AgentEntity agentEntity = GameState.Planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);

            var mechs = GameState.Planet.EntitasContext.mech.GetGroup(MechMatcher.MechID);

            if (agentEntity.isAgentPlayer)
            {
                foreach (var mech in mechs)
                {
                    if (mech.mechType.mechType == MechType.Tree)
                    {
                        if (Vec2f.Distance(agentEntity.agentPhysicsState.Position, mech.mechPosition2D.Value) < 1.3f)
                        {
                            if (mech.hasMechStatus)
                            {
                                agentEntity.agentPhysicsState.MovementState = AgentMovementState.ChoppingTree;

                                if (mech.mechStatus.Health <= 0)
                                {
                                    GameState.Planet.AddParticleEmitter(mech.mechPosition2D.Value, Particle.ParticleEmitterType.WoodEmitter);

                                    GameState.Planet.RemoveMech(mech.creationIndex);

                                    nodeEntity.nodeExecution.State = NodeState.Success;
                                    return;
                                }

                                GameState.Planet.AddParticleEmitter(mech.mechPosition2D.Value, Particle.ParticleEmitterType.WoodEmitter);
                                GameState.Planet.AddParticleEmitter(mech.mechPosition2D.Value + UnityEngine.Random.Range(-0.3f, 0.3f), Particle.ParticleEmitterType.WoodEmitter);
                                GameState.Planet.AddParticleEmitter(mech.mechPosition2D.Value + UnityEngine.Random.Range(-0.3f, 0.3f), Particle.ParticleEmitterType.WoodEmitter);

                                mech.mechStatus.Health -= 20;

                                if (mech.mechStatus.TreeSize > 0)
                                {
                                    GameState.Planet.AddItemParticle(new Vec2f(mech.mechPosition2D.Value.X + UnityEngine.Random.Range(-2, 2), mech.mechPosition2D.Value.Y), ItemType.Wood);
                                }

                            }
                        }
                    }
                }
            }
            nodeEntity.nodeExecution.State = NodeState.Success;
        }
    }
}
