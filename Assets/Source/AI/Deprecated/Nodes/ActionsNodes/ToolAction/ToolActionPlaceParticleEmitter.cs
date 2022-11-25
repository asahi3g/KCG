using UnityEngine;
using KMath;
using Enums;

namespace Node
{
    public class ToolActionPlaceParticleEmitter : NodeBase
    {
        public override ItemUsageActionType Type => ItemUsageActionType.ToolActionPlaceParticleEmitter;

        public override void OnEnter(NodeEntity nodeEntity)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float x = worldPosition.x;
            float y = worldPosition.y;
            int t = System.Math.Abs((int)KMath.Random.Mt19937.genrand_int32() % System.Enum.GetNames(typeof(Particle.ParticleType)).Length);

            
            GameState.Planet.AddDebris(new Vec2f(x, y), GameState.ItemCreationApi.ChestIconItem, 1.5f, 1.0f);
            GameState.Planet.AddParticleEmitter(new Vec2f(x - 0.3f, y - 0.3f), Particle.ParticleEmitterType.Explosion_2_Part4);
            GameState.Planet.AddParticleEmitter(new Vec2f(x, y), Particle.ParticleEmitterType.Explosion_2_Part3);
            GameState.Planet.AddParticleEmitter(new Vec2f(x, y), Particle.ParticleEmitterType.Explosion_2_Part2);
            GameState.Planet.AddParticleEmitter(new Vec2f(x, y), Particle.ParticleEmitterType.Explosion_2_Part1);
            nodeEntity.nodeExecution.State = NodeState.Success;
        }
    }
}
