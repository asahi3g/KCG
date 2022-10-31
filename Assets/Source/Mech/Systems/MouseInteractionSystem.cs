//import UnityEngine

using KMath;
using System.Collections.Generic;
using System.Web.WebPages;

namespace Mech
{
    public class MouseInteractionSystem
    {
        // Return list of meches mouse is over.
        /// <returns>If error return null</returns>
        public List<MechEntity> GetMechFromMousePos()
        {
            UnityEngine.Vector3 position = UnityEngine.Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            Vec2f mousePos = new Vec2f(position.x, position.y);

            List<MechEntity> meches = new List<MechEntity>(5);

            ref var planet = ref GameState.Planet;
            for (int i = 0; i < planet.MechList.Length; i++)
            {
                MechEntity mech = planet.MechList.Get(i);
                Vec2f pos = Vec2f.Zero;
                Vec2f size = Vec2f.Zero;
                if (mech.hasMechPosition2D)
                {
                    pos = mech.mechPosition2D.Value;
                    size = mech.mechSprite2D.Size;
                }

                // Is mouse over it?
                if (mousePos.X < pos.X || mousePos.Y < pos.Y)
                    continue;

                if (mousePos.X > pos.X + size.X || mousePos.Y > pos.Y + size.Y)
                    continue;

                meches.Add(mech);
            }
            return meches;
        }

        public void Update()
        {
            ref var planet = ref GameState.Planet;
            UnityEngine.Vector3 position = UnityEngine.Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            Vec2f mousePos = new Vec2f(position.x, position.y);
            Vec2f playerPos = planet.Player.agentPhysicsState.Position;

            for (int i = 0; i < planet.MechList.Length; i++)
            {
                Vec2f pos = Vec2f.Zero;
                Vec2f size = Vec2f.Zero;

                if (planet.MechList.Get(i).hasMechPosition2D)
                {
                    pos = planet.MechList.Get(i).mechPosition2D.Value;
                    size = planet.MechList.Get(i).mechSprite2D.Size;
                }

                // Is mouse over it?
                if (mousePos.X < pos.X || mousePos.Y < pos.Y)
                    continue;

                if (mousePos.X > pos.X + size.X || mousePos.Y > pos.Y + size.Y)
                    continue;

                var proprieties = planet.MechList.Get(i).GetProperties();
                string str;

                if (Vec2f.Distance(pos, playerPos) < 2.0f && proprieties.Action != Enums.NodeType.None)
                {
                    string nodeDescription = "";
                    str = "Press E to " + (!nodeDescription.IsEmpty() ?
                        nodeDescription : "interact");
                }
                else
                {
                    str = proprieties.Name;
                }
                    
                float h = 30;
                float w = 256;
                int fontSize = 22;

                float scale = UnityEngine.Screen.height / 1080f;

                GameState.Renderer.DrawStringGui(UnityEngine.Input.mousePosition.x, UnityEngine.Input.mousePosition.y, 
                    w * scale, h * scale, str, fontSize, UnityEngine.TextAnchor.LowerLeft, UnityEngine.Color.white);
                return;
            }
        }
    }
}
