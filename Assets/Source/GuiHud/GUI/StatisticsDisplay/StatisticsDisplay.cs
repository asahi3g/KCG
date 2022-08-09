using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using Planet;

namespace KGUI.Statistics
{
    public static class StatisticsDisplay
    {
        private static KMath.Vec2f playerPosition;

        static float frameCount = 0;
        static double dt = 0.0;
        static double fps = 0.0;
        static double ms = 0.0;
        static double updateRate = 4.0;  // 4 updates per sec.

        public static bool canDraw = true;
        public static KGUI.Elements.Text text;

        public static void DrawStatistics(ref PlanetState planet)
        {
            frameCount++;
            dt += Time.deltaTime;
            if (dt > 1.0 / updateRate)
            {
                fps = frameCount / dt;
                frameCount = 0;
                dt -= 1.0 / updateRate;
            }
            ms = 1 / Time.deltaTime;
            if (canDraw)
            {

                // Get Player Position Info
                IGroup<AgentEntity> Playerentities = planet.EntitasContext.agent.GetGroup(AgentMatcher.AgentPlayer);
                foreach (var entity in Playerentities)
                {
                    playerPosition = entity.agentPhysicsState.Position;
                }

                // Render Position Info in 16:09
                text = GameState.HUDManager.guiManager.AddText("Position: X: " + playerPosition.X + ", Y: " + playerPosition.Y + " \n" + "World Size: X: " + planet.TileMap.MapSize.X + ", Y: " + planet.TileMap.MapSize.Y + " \n" + "MS: " + ms.ToString() + " \n" + "FPS: " + (int)fps, new KMath.Vec2f(350.0f, 150.0f), new KMath.Vec2f(250,120));
            }
            // Render Position Info in 16:09
            text.UpdateText("Position: X: " + playerPosition.X + ", Y: " + playerPosition.Y + " \n" + "World Size: X: " + planet.TileMap.MapSize.X + ", Y: " + planet.TileMap.MapSize.Y + " \n" + "MS: " + ms.ToString() + " \n" + "FPS: " + (int)fps);
            canDraw = false;
        }
    }
}
