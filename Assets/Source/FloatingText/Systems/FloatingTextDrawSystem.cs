//import UnityEngine

namespace FloatingText
{
    public class FloatingTextDrawSystem
    {
        public void Draw(int drawOrder)
        {
            var entities = GameState.Planet.EntitasContext.floatingText.GetEntities();

            foreach (var entity in entities)
            {
                var movement = entity.floatingTextMovement;
                var go = entity.floatingTextGameObject;
                var text = entity.floatingTextText;

                GameState.Renderer.DrawString(go.GameObject, movement.Position.X, movement.Position.Y,
                     0.35f, text.Text, text.fontSize, text.color, drawOrder);
            }
        }
    }
}

