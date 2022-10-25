using UnityEngine;


namespace KGui
{




    public class CharacterStatsDisplay
    {

        public float x;
        public float y;
        public float width;
        public float height;

        public bool Enabled;
        public bool DebugDraw;


        public CharacterStatsDisplay()
        {
            x = 200.0f;
            y = 200.0f;

            width = 200.0f;


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
                float leftPanelWidth = width * 0.6f - marginX - padding;
                float rightPanelWidth = width * 0.4f - marginX - padding;
                TextAnchor leftSizeAnchor = TextAnchor.MiddleLeft;
                TextAnchor rightSizeAnchor = TextAnchor.MiddleRight;

                height = marginY * 2 + 5 * lineSize;

                Color backgroundColor = new Color(0.5f, 0.5f, 0.5f, 1f);
                Color debugColor = new Color(0.7f, 0.7f, 0.7f, 0.5f);
                Color statTextColor = new Color(0.0f, 1.0f, 0.0f, 1.0f);
                

                GameState.Renderer.DrawQuadColorGui(x, y, width, height, backgroundColor);

                if (DebugDraw)
                {
                    GameState.Renderer.DrawQuadColorGui(x + marginX, y + lineSize * 0 + marginY, leftPanelWidth, lineSize, debugColor);
                    GameState.Renderer.DrawQuadColorGui(x + marginX, y + lineSize * 1 + marginY, leftPanelWidth, lineSize, debugColor);
                    GameState.Renderer.DrawQuadColorGui(x + marginX, y + lineSize * 2 + marginY, leftPanelWidth, lineSize, debugColor);
                    GameState.Renderer.DrawQuadColorGui(x + marginX, y + lineSize * 3 + marginY, leftPanelWidth, lineSize, debugColor);
                    GameState.Renderer.DrawQuadColorGui(x + marginX, y + lineSize * 4 + marginY, leftPanelWidth, lineSize, debugColor);
                }

                GameState.Renderer.DrawStringGui(x + marginX, y + lineSize * 0 + marginY, leftPanelWidth, lineSize, "Health :", fontSize, leftSizeAnchor, Color.white);
                GameState.Renderer.DrawStringGui(x + marginX, y + lineSize * 1 + marginY, leftPanelWidth, lineSize, "Vitality :", fontSize, leftSizeAnchor, Color.white);
                GameState.Renderer.DrawStringGui(x + marginX, y + lineSize * 2 + marginY, leftPanelWidth, lineSize, "Strength :", fontSize, leftSizeAnchor, Color.white);
                GameState.Renderer.DrawStringGui(x + marginX, y + lineSize * 3 + marginY, leftPanelWidth, lineSize, "Dexterity :", fontSize, leftSizeAnchor, Color.white);
                GameState.Renderer.DrawStringGui(x + marginX, y + lineSize * 4 + marginY, leftPanelWidth, lineSize, "Intelligence :", fontSize, leftSizeAnchor, Color.white);


                if (DebugDraw)
                {
                    GameState.Renderer.DrawQuadColorGui(x + leftPanelWidth + padding * 2 + marginX, y + lineSize * 0 + marginY, rightPanelWidth, lineSize, debugColor);
                    GameState.Renderer.DrawQuadColorGui(x + leftPanelWidth + padding * 2 + marginX, y + lineSize * 1 + marginY, rightPanelWidth, lineSize, debugColor);
                    GameState.Renderer.DrawQuadColorGui(x + leftPanelWidth + padding * 2 + marginX, y + lineSize * 2 + marginY, rightPanelWidth, lineSize, debugColor);
                    GameState.Renderer.DrawQuadColorGui(x + leftPanelWidth + padding * 2 + marginX, y + lineSize * 3 + marginY, rightPanelWidth, lineSize, debugColor);
                    GameState.Renderer.DrawQuadColorGui(x + leftPanelWidth + padding * 2 + marginX, y + lineSize * 4 + marginY, rightPanelWidth, lineSize, debugColor);
                }


                GameState.Renderer.DrawStringGui(x + leftPanelWidth + padding * 2 + marginX, y + lineSize * 0 + marginY, rightPanelWidth, lineSize, "255", fontSize, rightSizeAnchor, statTextColor);
                GameState.Renderer.DrawStringGui(x + leftPanelWidth + padding * 2 + marginX, y + lineSize * 1 + marginY, rightPanelWidth, lineSize, "134", fontSize, rightSizeAnchor, statTextColor);
                GameState.Renderer.DrawStringGui(x + leftPanelWidth + padding * 2 + marginX, y + lineSize * 2 + marginY, rightPanelWidth, lineSize, "16", fontSize, rightSizeAnchor, statTextColor);
                GameState.Renderer.DrawStringGui(x + leftPanelWidth + padding * 2 + marginX, y + lineSize * 3 + marginY, rightPanelWidth, lineSize, "17", fontSize, rightSizeAnchor, statTextColor);
                GameState.Renderer.DrawStringGui(x + leftPanelWidth + padding * 2 + marginX, y + lineSize * 4 + marginY, rightPanelWidth, lineSize, "87", fontSize, rightSizeAnchor, statTextColor);

            }
        }
    }
}