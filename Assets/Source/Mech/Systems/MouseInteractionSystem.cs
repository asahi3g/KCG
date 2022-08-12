using Planet;
using UnityEngine;
using KMath;

namespace Mech
{
    public class MouseInteractionSystem
    {
        public int FloatingTextID = -1;

        public void Update(PlanetState planet)
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vec2f mousePos = new Vec2f(position.x, position.y);
            Vec2f playerPos = planet.Player.agentPhysicsState.Position;

            for (int i = 0; i < planet.MechList.Length; i++)
            {
                var proprieties = GameState.MechCreationApi.Get((int)planet.MechList.Get(i).mechType.mechType);
                if (proprieties.Action == Enums.ActionType.None)
                    continue;

                Vec2f pos = planet.MechList.Get(i).mechPosition2D.Value;
                Vec2f size = planet.MechList.Get(i).mechSprite2D.Size;

                if (Vec2f.Distance(pos, playerPos) > 2.0f)
                    continue;

                // Is mouse over it?
                if (mousePos.X < pos.X || mousePos.Y < pos.Y)
                    continue;

                if (mousePos.X > pos.X + size.X || mousePos.Y > pos.Y + size.Y)
                    continue;

                if (FloatingTextID > 0)
                    return;

                FloatingTextEntity floatingTextEntity = GameState.FloatingTextSpawnerSystem.SpawnFloatingText();
                FloatingTextID = floatingTextEntity.floatingTextID.ID;
                return;
            }

            // If is not over anything remove floating textEntity if exists.
            FloatingTextEntity floatingTextEntity = planet.EntitasContext.floatingText.GetEntityWithFloatingTextID(FloatingTextID);
            if (floatingTextEntity.floatingTextID == null)
                return;

            int index = floatingTextEntity.floatingTextID.Index;
            planet.RemoveFloatingText(index);
            FloatingTextID = -1;
        }
    }
}
