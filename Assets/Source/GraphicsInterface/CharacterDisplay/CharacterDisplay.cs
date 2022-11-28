//imports UnityEngine

using Agent;

namespace KGui
{
    public class CharacterDisplay
    {


        public float x;
        public float y;
        public float width;
        public float height;

        public bool Enabled;
        public bool DebugDraw;

        CharacterModelDisplay CharacterModelDisplay;
        CharacterStatsDisplay StatsDisplay;
        public AgentEntity Player;


        public CharacterDisplay()
        {
            Enabled = false;
            DebugDraw = false;

            x = 800;
            y = 200;
            width = 300;
            height = 200;

            StatsDisplay = new CharacterStatsDisplay();
            CharacterModelDisplay = new CharacterModelDisplay();
        }


        public void SetPlayer(AgentEntity player)
        {
            Player = player;
            Agent3DModel agent3DModel = player.Agent3DModel;
            CharacterModelDisplay.SetModel(agent3DModel.Renderer.gameObject);
        }

        public void Update()
        {

            if (Enabled)
            {
                CharacterModelDisplay.Enabled = true;
                CharacterModelDisplay.DebugDraw = DebugDraw;

                if (Player.hasAgent3DModel)
                {
                    var agentModel3D = Player.Agent3DModel;

                    CharacterModelDisplay.Update();
                }
            }
        }

        public void Draw()
        {
            if (Enabled)
            {
                

                StatsDisplay.Enabled = true;
                StatsDisplay.DebugDraw = DebugDraw;

                float marginX = 4.0f;
                float marginY = 2.0f;

                UnityEngine.Color backgroundColor = new UnityEngine.Color(0.75f, 0.75f, 0.75f, 0.75f);

                GameState.Renderer.DrawQuadColorGui(x, y, width, height, backgroundColor);

                StatsDisplay.x = x + width * 0.4f + marginX * 2;
                StatsDisplay.y = y + marginX;
                StatsDisplay.width = width * 0.6f - marginX * 2;

                StatsDisplay.Draw(Player);

                CharacterModelDisplay.x = x + marginX;
                CharacterModelDisplay.y = y + marginY * 2;
                CharacterModelDisplay.width = width * 0.4f - marginX;
                CharacterModelDisplay.height = height - marginY * 2.0f;


                CharacterModelDisplay.Draw();




            }
        }


    }
}