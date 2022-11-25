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
            //GameState.Planet.AddParticleEffect(new Vec2f(x, y), Enums.ParticleEffect.Explosion_2);
            //GameState.Planet.AddParticleEffect(new Vec2f(x, y), Enums.ParticleEffect.Smoke_2);
            GameState.Planet.AddParticleEffect(new Vec2f(x, y), Enums.ParticleEffect.Smoke_3);
            nodeEntity.nodeExecution.State = NodeState.Success;
        }
    }
}
