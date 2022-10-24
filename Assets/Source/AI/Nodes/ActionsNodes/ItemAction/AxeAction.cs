//imports UnityEngine

using KMath;
using Enums;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace Node
{
    public class AxeAction : NodeBase
    {
        public override NodeType Type { get { return NodeType.AxeAction; } }

        public override void OnEnter(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);

            var mechs = planet.EntitasContext.mech.GetGroup(MechMatcher.MechID);

            if (agentEntity.isAgentPlayer)
            {
                foreach (var mech in mechs)
                {
                    if (mech.mechType.mechType == Mech.MechType.Tree)
                    {
                        if (Vec2f.Distance(agentEntity.agentPhysicsState.Position, mech.mechPosition2D.Value) < 1.3f)
                        {
                            if (mech.hasMechStatus)
                            {
                                agentEntity.agentPhysicsState.MovementState = AgentMovementState.ChoppingTree;

                                if (mech.mechStatus.Health <= 0)
                                {
                                    planet.AddParticleEmitter(mech.mechPosition2D.Value, Particle.ParticleEmitterType.WoodEmitter);

                                    planet.RemoveMech(mech.creationIndex);

                                    nodeEntity.nodeExecution.State = Enums.NodeState.Success;
                                    return;
                                }

                                planet.AddParticleEmitter(mech.mechPosition2D.Value, Particle.ParticleEmitterType.WoodEmitter);
                                planet.AddParticleEmitter(mech.mechPosition2D.Value + UnityEngine.Random.Range(-0.3f, 0.3f), Particle.ParticleEmitterType.WoodEmitter);
                                planet.AddParticleEmitter(mech.mechPosition2D.Value + UnityEngine.Random.Range(-0.3f, 0.3f), Particle.ParticleEmitterType.WoodEmitter);

                                mech.mechStatus.Health -= 20;

                                if (mech.mechStatus.TreeSize > 0)
                                {
                                    planet.AddItemParticle(new Vec2f(mech.mechPosition2D.Value.X + UnityEngine.Random.Range(-2, 2), mech.mechPosition2D.Value.Y), ItemType.Wood);
                                }

                            }
                        }
                    }
                }
            }
            nodeEntity.nodeExecution.State = Enums.NodeState.Success;
        }
    }
}
