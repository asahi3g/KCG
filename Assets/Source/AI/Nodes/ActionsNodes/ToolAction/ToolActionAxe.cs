using UnityEngine;
using KMath;
using Enums;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace Node
{
    public class ToolActionAxe : NodeBase
    {
        public override NodeType Type { get { return NodeType.ToolActionAxe; } }

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
                            if(mech.hasMechTree)
                            {
                                if(mech.mechTree.Health <= 0)
                                {
                                    for(int i = 0; i < mech.mechTree.TreeSize; i++)
                                    {
                                       // planet.AddItemParticle(mech.mechPosition2D.Value, ItemType.Wood);
                                    }

                                    planet.RemoveMech(mech.creationIndex);

                                    nodeEntity.nodeExecution.State = Enums.NodeState.Success;
                                    return;
                                }

                                planet.AddParticleEmitter(mech.mechPosition2D.Value, Particle.ParticleEmitterType.WoodEmitter);
                                planet.AddParticleEmitter(mech.mechPosition2D.Value + Random.Range(-0.3f, 0.3f), Particle.ParticleEmitterType.WoodEmitter);
                                planet.AddParticleEmitter(mech.mechPosition2D.Value + Random.Range(-0.3f, 0.3f), Particle.ParticleEmitterType.WoodEmitter);

                                mech.mechTree.Health -= 20;
                            }
                        }
                    }
                }
            }
            nodeEntity.nodeExecution.State = Enums.NodeState.Success;
        }
    }
}
