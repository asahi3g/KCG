using Entitas;
using KMath;


namespace Agent
{



    public class SwordMoveList
    {


        public static void HandleActionDiagonal(AgentEntity entity, Vec2f attackPosition, Planet.PlanetState planet)
        {
            var physicsState = entity.agentPhysicsState;
            var box2dCollider = entity.physicsBox2DCollider;

            ParticleEntity particle = null;
            if (physicsState.FacingDirection == 1)
            {
                //attackPosition.X -= 3.0f * 0.5f; // size of the sprite
                //attackPosition.Y -= 3.0f * 0.5f; // size of the sprite
                
                if (physicsState.MoveIndex == 0)
                {
                    particle = planet.AddParticle(attackPosition, new Vec2f(), Particle.ParticleType.SwordSlash_1_Right);
                }
                else if (physicsState.MoveIndex == 1)
                {
                    particle = planet.AddParticle(attackPosition, new Vec2f(), Particle.ParticleType.SwordSlash_2_Right);
                }
                else if (physicsState.MoveIndex == 2)
                {
                    particle = planet.AddParticle(attackPosition, new Vec2f(), Particle.ParticleType.SwordSlash_3_Right);
                }
            }
            else if (physicsState.FacingDirection == -1)
            {
                //attackPosition.X -= 3.0f * 0.5f; // size of the sprite
               // attackPosition.Y -= 3.0f * 0.5f; // size of the sprite

                if (physicsState.MoveIndex == 0)
                {
                    particle = planet.AddParticle(attackPosition, new Vec2f(), Particle.ParticleType.SwordSlash_1_Left);
                }
                else if (physicsState.MoveIndex == 1)
                {
                    particle = planet.AddParticle(attackPosition, new Vec2f(), Particle.ParticleType.SwordSlash_2_Left);
                }
                else if (physicsState.MoveIndex == 2)
                {
                    particle = planet.AddParticle(attackPosition, new Vec2f(), Particle.ParticleType.SwordSlash_3_Left);

                }
            }

            if (particle != null)
            {
               //particle.particlePhysicsState.Rotation = angle;
             //emitter.particleEmitter2dPosition.Velocity = new UnityEngine.Vector2(entity.agentPhysicsState.Velocity.X, entity.agentPhysicsState.Velocity.Y);
            }


        }


        public static void HandleActionUp(AgentEntity entity, Vec2f attackPosition, Planet.PlanetState planet)
        {
            var physicsState = entity.agentPhysicsState;
            var box2dCollider = entity.physicsBox2DCollider;

            ParticleEntity particle = null;
            if (physicsState.FacingDirection == 1)
            {
                attackPosition.X -= 3.0f * 0.5f; // size of the sprite
                //attackPosition.Y -= 3.0f * 0.5f; // size of the sprite
                
                if (physicsState.MoveIndex == 0)
                {
                    particle = planet.AddParticle(attackPosition, new Vec2f(), Particle.ParticleType.SwordSlash_1_Up_Right);
                }
                else if (physicsState.MoveIndex == 1)
                {
                    particle = planet.AddParticle(attackPosition, new Vec2f(), Particle.ParticleType.SwordSlash_2_Up_Right);
                }
                else if (physicsState.MoveIndex == 2)
                {
                    particle = planet.AddParticle(attackPosition, new Vec2f(), Particle.ParticleType.SwordSlash_3_Up_Right);
                }
            }
            else if (physicsState.FacingDirection == -1)
            {
                attackPosition.X -= 3.0f * 0.5f; // size of the sprite
               // attackPosition.Y -= 3.0f * 0.5f; // size of the sprite

                if (physicsState.MoveIndex == 0)
                {
                    particle = planet.AddParticle(attackPosition, new Vec2f(), Particle.ParticleType.SwordSlash_1_Left);
                }
                else if (physicsState.MoveIndex == 1)
                {
                    particle = planet.AddParticle(attackPosition, new Vec2f(), Particle.ParticleType.SwordSlash_2_Left);
                }
                else if (physicsState.MoveIndex == 2)
                {
                    particle = planet.AddParticle(attackPosition, new Vec2f(), Particle.ParticleType.SwordSlash_3_Left);

                }
            }

            if (particle != null)
            {
              // particle.particlePhysicsState.Rotation = -0.0f;
             //emitter.particleEmitter2dPosition.Velocity = new UnityEngine.Vector2(entity.agentPhysicsState.Velocity.X, entity.agentPhysicsState.Velocity.Y);
            }


        }


        
        public static void HandleActionDown(AgentEntity entity, Vec2f attackPosition, Planet.PlanetState planet)
        {
            var physicsState = entity.agentPhysicsState;
            var box2dCollider = entity.physicsBox2DCollider;

            ParticleEntity particle = null;
            if (physicsState.FacingDirection == 1)
            {
                attackPosition.X -= 3.0f * 0.5f; // size of the sprite
                //attackPosition.Y -= 3.0f * 0.5f; // size of the sprite
                
                if (physicsState.MoveIndex == 0)
                {
                    particle = planet.AddParticle(attackPosition, new Vec2f(), Particle.ParticleType.SwordSlash_1_Right);
                }
                else if (physicsState.MoveIndex == 1)
                {
                    particle = planet.AddParticle(attackPosition, new Vec2f(), Particle.ParticleType.SwordSlash_2_Right);
                }
                else if (physicsState.MoveIndex == 2)
                {
                    particle = planet.AddParticle(attackPosition, new Vec2f(), Particle.ParticleType.SwordSlash_3_Right);
                }
            }
            else if (physicsState.FacingDirection == -1)
            {
                attackPosition.X -= 3.0f * 0.5f; // size of the sprite
               // attackPosition.Y -= 3.0f * 0.5f; // size of the sprite

                if (physicsState.MoveIndex == 0)
                {
                    particle = planet.AddParticle(attackPosition, new Vec2f(), Particle.ParticleType.SwordSlash_1_Left);
                }
                else if (physicsState.MoveIndex == 1)
                {
                    particle = planet.AddParticle(attackPosition, new Vec2f(), Particle.ParticleType.SwordSlash_2_Left);
                }
                else if (physicsState.MoveIndex == 2)
                {
                    particle = planet.AddParticle(attackPosition, new Vec2f(), Particle.ParticleType.SwordSlash_3_Left);

                }
            }

            if (particle != null)
            {
               particle.particlePhysicsState.Rotation = 70.0f;
             //emitter.particleEmitter2dPosition.Velocity = new UnityEngine.Vector2(entity.agentPhysicsState.Velocity.X, entity.agentPhysicsState.Velocity.Y);
            }


        }
    }
}