using System.Collections.Generic;
using UnityEngine;
using KMath;

namespace FloatingText
{
    public class DrawSystem
    {
        public void Draw(Transform transform, int drawOrder)
        {
            var entities = Contexts.sharedInstance.game.GetGroup(GameMatcher.AllOf(GameMatcher.FloatingTextState));

            foreach (var entity in entities)
            {
                var movable = entity.floatingTextMovable;
                var state = entity.floatingTextState;

                Utility.Render.DrawString(movable.Position.x, movable.Position.y,
                     0.35f, state.Text, 18, new Color(255, 0, 0, 255),
                    transform, drawOrder);

            }
        }
    }
}

