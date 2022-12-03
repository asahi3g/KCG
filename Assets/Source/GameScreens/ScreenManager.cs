namespace GameScreen
{



    public class ScreenManager
    {
        public GameScreen[] ScreenList;

        public Enums.GameScreenEnum CurrentScreen;



        public ScreenManager()
        {
            ScreenList = new GameScreen[(int)Enums.GameScreenEnum.GameScreen_Count];
            CreateScreens();
            ChangeScreen(Enums.GameScreenEnum.GameScreen_MainMenu);
        }

        public void CreateScreens()
        {
            CreateScreen(Enums.GameScreenEnum.GameScreen_MainMenu, new MainMenuScreen());
            CreateScreen(Enums.GameScreenEnum.GameScreen_Game, new GameplayScreen());
            CreateScreen(Enums.GameScreenEnum.GameScreen_GameOver, new GameoverScreen());       
        }

        public void CreateScreen(Enums.GameScreenEnum screen, GameScreen value)
        {
            ScreenList[(int)screen] =  value;
        }


        public void ChangeScreen(Enums.GameScreenEnum screen)
        {
            if (screen != CurrentScreen)
            {
                if (CurrentScreen != Enums.GameScreenEnum.GameScreen_Error)
                {
                    ScreenList[(int)CurrentScreen].UnloadResources();
                }

                CurrentScreen = screen;
                ScreenList[(int)CurrentScreen].LoadResources();
            }
        }

        public void Init(UnityEngine.Transform transform)
        {
            for(int i = 0; i < ScreenList.Length; i++)
            {
                GameScreen screen = ScreenList[i];
                if (screen != null)
                {
                    screen.Init(transform);
                }
            }
        }


        public void Draw()
        {
            ScreenList[(int)CurrentScreen].Draw();
        }

        public void Update()
        {
            ScreenList[(int)CurrentScreen].Update();
        }

        public void OnGui()
        {
            ScreenList[(int)CurrentScreen].OnGui();
        }
    }
}
