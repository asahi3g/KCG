using KMath;

namespace Agent
{
    public struct ArrayPosition
    {
        public int Size;
        public int Offset;
    }

    public class AgentMoveListPropertiesManager
    {
        // Start is called before the first frame update

        private int CurrentIndex; // current index into the Offset Array

        private ArrayPosition[] PositionArray; // an array of offsets into the array 

        private int LineOffset;
        private int LineCount;
        private AgentMoveListProperties[] PropertiesArray;


        public AgentMoveListPropertiesManager()
        {
            PositionArray = new ArrayPosition[256];
            PropertiesArray = new AgentMoveListProperties[1024];
            

            CurrentIndex = 0;
            LineOffset = 0;
            LineCount = 0;
        }

        public void InitStage1()
        {

        }

        public void InitStage2()
        {
            InitializeResources();
        }

        public ArrayPosition GetPosition(Enums.AgentMoveList index)
        {
            Utility.Utils.Assert((int)index >= 0 && (int)index < PositionArray.Length);

            return PositionArray[(int)index];
        }

        public AgentMoveListProperties Get(int index)
        {
             Utility.Utils.Assert((int)index >= 0 && (int)index < PropertiesArray.Length);

             return PropertiesArray[index];
        }

        public void Create(Enums.AgentMoveList Id)
        {
            if ((int)Id + 1 >= PositionArray.Length)
            {
                System.Array.Resize(ref PositionArray, PositionArray.Length * 2);
            }

            CurrentIndex = (int)Id;
            PositionArray[CurrentIndex] = new ArrayPosition{Offset=LineOffset};
        }
        

        
        public void AddMove(Enums.AgentMovementState moveState, float maxDelay)
        {
            if ((int)LineOffset >= PropertiesArray.Length)
            {
                System.Array.Resize(ref PropertiesArray, PropertiesArray.Length + 1024);
            }

            PropertiesArray[LineOffset++] = new AgentMoveListProperties{MovementState=moveState, MaxDelay=maxDelay};
            LineCount++;
        }


        public void End()
        {
            PositionArray[CurrentIndex].Size = LineCount;
            LineCount = 0;
        }



        public void InitializeResources()
        {
            AgentMoveListPropertiesManager Api = GameState.AgentMoveListPropertiesManager;

            const float DefaultMoveListMaxDelay = 1.0f;

            Api.Create(Enums.AgentMoveList.Sword);
            Api.AddMove(Enums.AgentMovementState.SwordSlash, DefaultMoveListMaxDelay);
            Api.AddMove(Enums.AgentMovementState.SwordSlash, DefaultMoveListMaxDelay);
            Api.AddMove(Enums.AgentMovementState.SwordSlash, DefaultMoveListMaxDelay);
            Api.End();

        
        }


    }

}
