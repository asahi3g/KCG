using KMath;

namespace FloatingText
{
    public class FloatingTextSpawnerSystem
    {
        static int uniqueID = 0;
        public FloatingTextEntity SpawnFloatingText(FloatingTextContext floatingTextContext, string text, 
                                    float timeToLive, Vec2f velocity, Vec2f position)
        {
            var entity = floatingTextContext.CreateEntity();

            entity.AddFloatingTextID(uniqueID++, -1);
            entity.AddFloatingTextState(timeToLive, text);
            entity.AddFloatingTextMovable(velocity, position);
            entity.AddFloatingTextSprite(Utility.ObjectMesh.CreateEmptyTextGameObject("FloatingText"));

            return entity;
        }
    }
}

