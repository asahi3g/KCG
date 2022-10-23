using Planet;
using UnityEngine;
using KMath;
using Node;
using System.Web.WebPages;

namespace Mech
{
    public class MouseInteractionSystem
    {
        public void Update(PlanetState planet)
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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

                var proprieties = GameState.MechCreationApi.Get((int)planet.MechList.Get(i).mechType.mechType);
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

                float scale = Screen.height / 1080f;

                GameState.Renderer.DrawStringGui(Input.mousePosition.x, Input.mousePosition.y, 
                    w * scale, h * scale, str, fontSize, TextAnchor.LowerLeft, Color.white);
                return;
            }
        }
    }
}
