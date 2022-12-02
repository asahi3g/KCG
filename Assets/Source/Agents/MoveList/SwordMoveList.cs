using Entitas;
using KMath;


namespace Agent
{



    public class SwordMoveList
    {


        public static void HandleAction(AgentEntity entity, Vec2f attackPosition, Planet.PlanetState planet)
        {
            var physicsState = entity.agentPhysicsState;
            var box2dCollider = entity.physicsBox2DCollider;


            ParticleEntity emitter = null;
            if (physicsState.FacingDirection == 1)
            {
                attackPosition.X -= 3.0f * 0.5f; // size of the sprite
                
                if (physicsState.MoveIndex == 0)
                {
                    emitter = planet.AddParticleEmitter(attackPosition, Particle.ParticleEmitterType.SwordSlash_1_Right);
                }
                else if (physicsState.MoveIndex == 1)
                {
                    emitter = planet.AddParticleEmitter(attackPosition, Particle.ParticleEmitterType.SwordSlash_2_Right);
                }
                else if (physicsState.MoveIndex == 2)
                {
                    emitter = planet.AddParticleEmitter(attackPosition, Particle.ParticleEmitterType.SwordSlash_3_Right);
                }
            }
            else if (physicsState.FacingDirection == -1)
            {
                attackPosition.X -= 3.0f * 0.5f; // size of the sprite

                if (physicsState.MoveIndex == 0)
                {
                    emitter = planet.AddParticleEmitter(attackPosition, Particle.ParticleEmitterType.SwordSlash_1_Left);
                }
                else if (physicsState.MoveIndex == 1)
                {
                    emitter = planet.AddParticleEmitter(attackPosition, Particle.ParticleEmitterType.SwordSlash_2_Left);
                }
                else if (physicsState.MoveIndex == 2)
                {
                    emitter = planet.AddParticleEmitter(attackPosition, Particle.ParticleEmitterType.SwordSlash_3_Left);

                }
            }

            if (emitter != null)
            {
             //emitter.particleEmitter2dPosition.Velocity = new UnityEngine.Vector2(entity.agentPhysicsState.Velocity.X, entity.agentPhysicsState.Velocity.Y);
            }


        }
    }
}