using Planet;
using UnityEngine;
using KMath;

namespace Agent
{
    public class MouseInteractionSystem
    {
        public void Update(ref PlanetState planet)
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vec2f mousePos = new Vec2f(position.x, position.y);
            Vec2f playerPos = planet.Player.agentPhysicsState.Position;

            for (int i = 0; i < planet.AgentList.Length; i++)
            {
                AgentEntity agentEntity = planet.AgentList.Get(i);
                if (agentEntity.isAgentPlayer)
                    continue;
                    
                
                Vec2f pos = agentEntity.agentPhysicsState.Position;
                Vec2f size = agentEntity.physicsBox2DCollider.Size;
                AgentProperties properties = GameState.AgentCreationApi.Get((int)agentEntity.agentID.Type);

                // Is mouse over it?
                if (mousePos.X < pos.X || mousePos.Y < pos.Y)
                    continue;

                if (mousePos.X > pos.X + size.X || mousePos.Y > pos.Y + size.Y)
                    continue;

                string str;

                if (Vec2f.Distance(pos, playerPos) < 2.0f && agentEntity.isAgentCorpse)
                {
                    str = "Press E to open corpse inventory";
                }
                else
                {
                    str = properties.Name;
                    if (agentEntity.isAgentCorpse)
                    {
                        str += " corpse";
                    }
                }

                    
                float h = 30;
                float w = 512;
                int fontSize = 22;

                float scale = Screen.height / 1080f;

                GameState.Renderer.DrawStringGui(Input.mousePosition.x, Input.mousePosition.y, 
                    w * scale, h * scale, str, fontSize, TextAnchor.LowerLeft, Color.white);
                return;
            }
        }
    }
}
