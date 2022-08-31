using UnityEngine;


namespace KGui
{




    public class CharacterDisplay
    {

        public float statsPanelX;
        public float statsPanelY;
        public float statsPanelWidth;
        public float statsPanelHeight;

        public bool Enabled;
        public bool DebugDraw;


        public CharacterDisplay()
        {
            statsPanelX = 200.0f;
            statsPanelY = 200.0f;

            statsPanelWidth = 200.0f;


            Enabled = true;
            DebugDraw = true;
        }

        
        public void Draw(AgentEntity player)
        {
            if (Enabled)
            {
                int fontSize = 16;
                float marginX = 4.0f;
                float marginY = 8.0f;
                float padding = 4.0f;
                float lineSize = fontSize + padding * 2.0f;
                float halfLifeWidth = statsPanelWidth * 0.5f - marginX - padding;
                TextAnchor leftSizeAnchor = TextAnchor.MiddleLeft;
                TextAnchor rightSizeAnchor = TextAnchor.MiddleRight;

                int numberOfItems = 5;

                statsPanelHeight = marginY * 2 + 5 * lineSize;

                Color backgroundColor = new Color(0.5f, 0.5f, 0.5f, 1f);
                Color debugColor = new Color(0.7f, 0.7f, 0.7f, 0.5f);
                Color statTextColor = new Color(0.0f, 1.0f, 0.0f, 1.0f);
                

                GameState.Renderer.DrawQuadColorGui(statsPanelX, statsPanelY, statsPanelWidth, statsPanelHeight, backgroundColor);

                if (DebugDraw)
                {
                    GameState.Renderer.DrawQuadColorGui(statsPanelX + marginX, statsPanelY + lineSize * 0 + marginY, halfLifeWidth, lineSize, debugColor);
                    GameState.Renderer.DrawQuadColorGui(statsPanelX + marginX, statsPanelY + lineSize * 1 + marginY, halfLifeWidth, lineSize, debugColor);
                    GameState.Renderer.DrawQuadColorGui(statsPanelX + marginX, statsPanelY + lineSize * 2 + marginY, halfLifeWidth, lineSize, debugColor);
                    GameState.Renderer.DrawQuadColorGui(statsPanelX + marginX, statsPanelY + lineSize * 3 + marginY, halfLifeWidth, lineSize, debugColor);
                    GameState.Renderer.DrawQuadColorGui(statsPanelX + marginX, statsPanelY + lineSize * 4 + marginY, halfLifeWidth, lineSize, debugColor);
                }

                GameState.Renderer.DrawStringGui(statsPanelX + marginX, statsPanelY + lineSize * 0 + marginY, halfLifeWidth, lineSize, "Health :", fontSize, leftSizeAnchor, Color.white);
                GameState.Renderer.DrawStringGui(statsPanelX + marginX, statsPanelY + lineSize * 1 + marginY, halfLifeWidth, lineSize, "Vitality :", fontSize, leftSizeAnchor, Color.white);
                GameState.Renderer.DrawStringGui(statsPanelX + marginX, statsPanelY + lineSize * 2 + marginY, halfLifeWidth, lineSize, "Strength :", fontSize, leftSizeAnchor, Color.white);
                GameState.Renderer.DrawStringGui(statsPanelX + marginX, statsPanelY + lineSize * 3 + marginY, halfLifeWidth, lineSize, "Dexterity :", fontSize, leftSizeAnchor, Color.white);
                GameState.Renderer.DrawStringGui(statsPanelX + marginX, statsPanelY + lineSize * 4 + marginY, halfLifeWidth, lineSize, "Intelligence :", fontSize, leftSizeAnchor, Color.white);


                if (DebugDraw)
                {
                    GameState.Renderer.DrawQuadColorGui(statsPanelX + statsPanelWidth * 0.5f + padding, statsPanelY + lineSize * 0 + marginY, halfLifeWidth, lineSize, debugColor);
                    GameState.Renderer.DrawQuadColorGui(statsPanelX + statsPanelWidth * 0.5f + padding, statsPanelY + lineSize * 1 + marginY, halfLifeWidth, lineSize, debugColor);
                    GameState.Renderer.DrawQuadColorGui(statsPanelX + statsPanelWidth * 0.5f + padding, statsPanelY + lineSize * 2 + marginY, halfLifeWidth, lineSize, debugColor);
                    GameState.Renderer.DrawQuadColorGui(statsPanelX + statsPanelWidth * 0.5f + padding, statsPanelY + lineSize * 3 + marginY, halfLifeWidth, lineSize, debugColor);
                    GameState.Renderer.DrawQuadColorGui(statsPanelX + statsPanelWidth * 0.5f + padding, statsPanelY + lineSize * 4 + marginY, halfLifeWidth, lineSize, debugColor);
                }


                GameState.Renderer.DrawStringGui(statsPanelX + statsPanelWidth * 0.5f + padding, statsPanelY + lineSize * 0 + marginY, halfLifeWidth, lineSize, "255", fontSize, rightSizeAnchor, statTextColor);
                GameState.Renderer.DrawStringGui(statsPanelX + statsPanelWidth * 0.5f + padding, statsPanelY + lineSize * 1 + marginY, halfLifeWidth, lineSize, "134", fontSize, rightSizeAnchor, statTextColor);
                GameState.Renderer.DrawStringGui(statsPanelX + statsPanelWidth * 0.5f + padding, statsPanelY + lineSize * 2 + marginY, halfLifeWidth, lineSize, "16", fontSize, rightSizeAnchor, statTextColor);
                GameState.Renderer.DrawStringGui(statsPanelX + statsPanelWidth * 0.5f + padding, statsPanelY + lineSize * 3 + marginY, halfLifeWidth, lineSize, "17", fontSize, rightSizeAnchor, statTextColor);
                GameState.Renderer.DrawStringGui(statsPanelX + statsPanelWidth * 0.5f + padding, statsPanelY + lineSize * 4 + marginY, halfLifeWidth, lineSize, "87", fontSize, rightSizeAnchor, statTextColor);

            }
        }
    }
}