using System;
using System.Collections.Generic;
using KMath;


namespace Collisions
{
    public class LineCreationApi
    {
        // Start is called before the first frame update

        private int CurrentIndex;
        private LineProperties[] PropertiesArray;

        public LineCreationApi()
        {
            PropertiesArray = new LineProperties[1024];
            for(int i = 0; i < PropertiesArray.Length; i++)
            {
                PropertiesArray[i] = new LineProperties();
            }
            CurrentIndex = -1;
        }

        public void InitStage1()
        {

        }

        public void InitStage2()
        {
            InitializeResources();
        }

        public Line2D GetLine(TileLineSegment Id)
        {
            if ((int)Id >= 0 && (int)Id < PropertiesArray.Length)
            {
                return PropertiesArray[(int)Id].Line;
            }

            return new Line2D();
        }

        public Vec2f GetNormal(TileLineSegment Id)
        {
            if ((int)Id >= 0 && (int)Id < PropertiesArray.Length)
            {
                return PropertiesArray[(int)Id].Normal;
            }

            return new Vec2f();
        }
        public void Create(TileLineSegment Id, Vec2f pointA, Vec2f pointB, Vec2f Normal)
        {
            while ((int)Id >= PropertiesArray.Length)
            {
                Array.Resize(ref PropertiesArray, PropertiesArray.Length * 2);
            }

            CurrentIndex = (int)Id;
            if (CurrentIndex != -1)
            {
                PropertiesArray[CurrentIndex].Line = new Line2D(pointA, pointB);
                PropertiesArray[CurrentIndex].Normal = Normal;   
            }
        }


        public void InitializeResources()
        {
            float epsilon = 0.00f;
            Vec2f C0 = new Vec2f(0.0f, 1.0f);
            Vec2f C1 = new Vec2f(1.0f, 1.0f);
            Vec2f C2 = new Vec2f(1.0f, 0.0f);
            Vec2f C3 = new Vec2f(0.0f, 0.0f);

            Vec2f M0 = new Vec2f(0.5f, 1.0f);
            Vec2f M1 = new Vec2f(1.0f, 0.5f);
            Vec2f M2 = new Vec2f(0.5f, 0.0f);
            Vec2f M3 = new Vec2f(0.0f, 0.5f);

            GameState.LineCreationApi.Create(TileLineSegment.L_C0_C1, C0 + new Vec2f(epsilon, epsilon), C1 + new Vec2f(-epsilon, epsilon), new Vec2f(0.0f, 1.0f));
            GameState.LineCreationApi.Create(TileLineSegment.L_C1_C2, C1 + new Vec2f(epsilon, -epsilon), C2 + new Vec2f(epsilon, epsilon), new Vec2f(1.0f, 0.0f));
            GameState.LineCreationApi.Create(TileLineSegment.L_C2_C3, C2 + new Vec2f(-epsilon, -epsilon), C3 + new Vec2f(epsilon, -epsilon), new Vec2f(0.0f, -1.0f));
            GameState.LineCreationApi.Create(TileLineSegment.L_C3_C0, C3 + new Vec2f(-epsilon, epsilon), C0 + new Vec2f(-epsilon, -epsilon), new Vec2f(-1.0f, 0.0f));

            GameState.LineCreationApi.Create(TileLineSegment.L_C0_M0, C0, M0, new Vec2f(0.0f, 1.0f)); 
            GameState.LineCreationApi.Create(TileLineSegment.L_M0_M2, M0, M2, new Vec2f(1.0f, 0.0f)); 
            GameState.LineCreationApi.Create(TileLineSegment.L_M2_C3, M2, C3, new Vec2f(0.0f, -1.0f)); 
          //  GameState.LineCreationApi.Create(TileLineSegment.L_C3_C0, C3, C0, new Vec2f(1.0f, 0.0f)); 

            GameState.LineCreationApi.Create(TileLineSegment.L_C1_M1, C1, M1, new Vec2f(1.0f, 0.0f)); 
            GameState.LineCreationApi.Create(TileLineSegment.L_M1_M3, M1, M3, new Vec2f(0.0f, -1.0f)); 
            GameState.LineCreationApi.Create(TileLineSegment.L_M3_C0, M3, C0, new Vec2f(-1.0f, 0.0f)); 

            GameState.LineCreationApi.Create(TileLineSegment.L_M0_C1, M0, C1, new Vec2f(0.0f, 1.0f)); 
            GameState.LineCreationApi.Create(TileLineSegment.L_C2_M2, C2, M2, new Vec2f(0.0f, -1.0f)); 
            GameState.LineCreationApi.Create(TileLineSegment.L_M2_M0, M2, M0, new Vec2f(-1.0f, 0.0f)); 

            GameState.LineCreationApi.Create(TileLineSegment.L_M3_M1, M3, M1, new Vec2f(0.0f, 1.0f)); 
            GameState.LineCreationApi.Create(TileLineSegment.L_M1_C2, M1, C2, new Vec2f(1.0f, 0.0f)); 
            GameState.LineCreationApi.Create(TileLineSegment.L_C3_M3, C3, M3, new Vec2f(-1.0f, 0.0f)); 


            GameState.LineCreationApi.Create(TileLineSegment.L_C3_C1, C3, C1, new Vec2f(-1.0f, 1.0f).Normalize());
            GameState.LineCreationApi.Create(TileLineSegment.L_C1_C3, C1, C3, new Vec2f(1.0f, -1.0f).Normalize());
            GameState.LineCreationApi.Create(TileLineSegment.L_C0_C2, C0, C2, new Vec2f(1.0f, 1.0f).Normalize());
            GameState.LineCreationApi.Create(TileLineSegment.L_C2_C0, C0, C2, new Vec2f(-1.0f, 1.0f).Normalize());

            GameState.LineCreationApi.Create(TileLineSegment.L_M0_C3, M0, C3, new Vec2f(-0.5f, -1.0f).Normalize()); 
            GameState.LineCreationApi.Create(TileLineSegment.L_M1_C0, M1, C0, new Vec2f(-1.0f, -0.5f).Normalize()); 
            GameState.LineCreationApi.Create(TileLineSegment.L_M2_C1, M2, C1, new Vec2f(-0.5f, 1.0f).Normalize()); 
            GameState.LineCreationApi.Create(TileLineSegment.L_C0_M1, C0, M1, new Vec2f(1.0f, 0.5f).Normalize());
            GameState.LineCreationApi.Create(TileLineSegment.L_C0_M2, C0, M2, new Vec2f(0.5f, 1.0f).Normalize());
            GameState.LineCreationApi.Create(TileLineSegment.L_C1_M3, C1, M3, new Vec2f(1.0f, -0.5f).Normalize());
            GameState.LineCreationApi.Create(TileLineSegment.L_C2_M0, C2, M0, new Vec2f(-0.5f, -1.0f).Normalize());
            GameState.LineCreationApi.Create(TileLineSegment.L_M1_C0, M1, C0, new Vec2f(-1.0f, 0.5f).Normalize());

            GameState.LineCreationApi.Create(TileLineSegment.L_C1_M2, C1, M2, new Vec2f(0.5f, -1.0f).Normalize());
            GameState.LineCreationApi.Create(TileLineSegment.L_C0_M1, C0, M1, new Vec2f(1.0f, 0.5f).Normalize());
            GameState.LineCreationApi.Create(TileLineSegment.L_C3_M0, C3, M0, new Vec2f(-0.5f, 1.0f).Normalize());
            GameState.LineCreationApi.Create(TileLineSegment.L_C2_M3, C2, M3, new Vec2f(-1.0f, -0.5f).Normalize());
            GameState.LineCreationApi.Create(TileLineSegment.L_M2_C0, M2, C0, new Vec2f(-0.5f, -1.0f).Normalize());
            GameState.LineCreationApi.Create(TileLineSegment.L_M1_C3, M1, C3, new Vec2f(1.0f, -0.5f).Normalize());
            GameState.LineCreationApi.Create(TileLineSegment.L_M0_C2, M0, C2, new Vec2f(0.5f, 1.0f).Normalize());
            GameState.LineCreationApi.Create(TileLineSegment.L_M3_C1, M3, C1, new Vec2f(1.0f, 0.5f).Normalize());



        }


    }

}
