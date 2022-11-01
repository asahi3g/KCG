//import UnityEngine

using KMath;

namespace FloatingText
{
    public class FloatingTextSpawnerSystem
    {
        static int uniqueID = 0;
        public FloatingTextEntity SpawnFloatingText(string text, 
                                    float timeToLive, Vec2f velocity, Vec2f position, UnityEngine.Color color, int fontSize)
        {
            var entity = GameState.Planet.EntitasContext.floatingText.CreateEntity();

            entity.AddFloatingTextID(uniqueID++, -1);
            entity.AddFloatingTextTimeToLive(timeToLive);
            entity.AddFloatingTextMovement(velocity, position);
            entity.AddFloatingTextGameObject(Utility.ObjectMesh.CreateEmptyTextGameObject("FloatingText"));
            entity.AddFloatingTextText(text, color, fontSize);

            return entity;
        }

        public FloatingTextEntity SpawnFixedFloatingText(string text,
                            Vec2f position, UnityEngine.Color color, int fontSize)
        {
            var entity = GameState.Planet.EntitasContext.floatingText.CreateEntity();

            entity.AddFloatingTextID(uniqueID++, -1);
            entity.AddFloatingTextMovement(Vec2f.Zero, position);
            entity.AddFloatingTextGameObject(Utility.ObjectMesh.CreateEmptyTextGameObject("FloatingText"));
            entity.AddFloatingTextText(text, color, fontSize);

            return entity;
        }
    }
}

