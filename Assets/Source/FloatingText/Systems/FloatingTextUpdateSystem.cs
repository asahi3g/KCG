using System.Collections.Generic;
using KMath;

namespace FloatingText
{
    public class FloatingTextUpdateSystem
    {
        List<FloatingTextEntity> ToRemoveEntities = new List<FloatingTextEntity>();

<<<<<<< HEAD
        public void Update(Planet.PlanetState planetState, float deltaTime)
=======
        public void Update(float deltaTime)
>>>>>>> 3b95f36247fe313ba5f5f7bfd4f38797fb5b6059
        {
            ref var planet = ref GameState.Planet;
            FloatingTextEntity[] entities = planet.EntitasContext.floatingText.GetEntities();

            foreach (var entity in entities)
            {
                var text = entity.floatingTextText;

                if (entity.hasFloatingTextTimeToLive)
                {
                    // reduce the time to live of the floating text
                    entity.floatingTextTimeToLive.TimeToLive = entity.floatingTextTimeToLive.TimeToLive - deltaTime;
                    entity.floatingTextText.Text = text.Text;
                    // if time to live hits zero we remove the entity
                    if (entity.floatingTextTimeToLive.TimeToLive <= 0.0f)
                    {
                        ToRemoveEntities.Add(entity);
                    }
                }

                // maybe move this to its own system for movement
                if (entity.hasFloatingTextMovement)
                { 
                    var movement = entity.floatingTextMovement;

                    Vec2f newPosition = new Vec2f(movement.Position.X + movement.Velocity.X,
                                                    movement.Position.Y + movement.Velocity.Y);
                    entity.floatingTextMovement.Position = newPosition;
                }
            }

            foreach(var entity in ToRemoveEntities)
            {
                planet.RemoveFloatingText(entity.floatingTextID.Index);
            }
            ToRemoveEntities.Clear();
        }
    }
}

