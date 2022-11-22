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

        private int CurrentMatchOffset;
        private TileLineSegment[] MatchArray;

        public LineCreationApi()
        {
            PropertiesArray = new LineProperties[1024];
            for(int i = 0; i < PropertiesArray.Length; i++)
            {
                PropertiesArray[i] = new LineProperties();
            }
            CurrentIndex = -1;

            MatchArray = new TileLineSegment[1024];
            CurrentMatchOffset = 0;
        }

        public void InitStage1()
        {

        }

        public void InitStage2()
        {
            InitializeResources();
        }

        public Line2D GetLine(TileLineSegment Id, int positionX, int positionY)
        {
            if ((int)Id >= 0 && (int)Id < PropertiesArray.Length)
            {
                Line2D line = PropertiesArray[(int)Id].Line;
                line.A.X += positionX;
                line.A.Y += positionY;

                line.B.X += positionX;
                line.B.Y += positionY;
                return line;
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

        public LineProperties GetLineProperties(TileLineSegment Id)
        {
            if ((int)Id >= 0 && (int)Id < PropertiesArray.Length)
            {
                return PropertiesArray[(int)Id];
            }

            return new LineProperties();
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
                PropertiesArray[CurrentIndex].MatchOffset = CurrentMatchOffset;
            }
        }

        public void AddMatch(TileLineSegment Match)
        {
            if (CurrentMatchOffset + 1 >= MatchArray.Length)
            {
                Array.Resize(ref MatchArray, MatchArray.Length + 1024);
            }

            MatchArray[CurrentMatchOffset++] = Match;
            PropertiesArray[CurrentIndex].MatchCount++;
        }

        public TileLineSegment GetMatch(int index)
        {
            Utility.Utils.Assert((int)index >= 0 && (int)index < MatchArray.Length);

            return MatchArray[index];
        }


        public void InitializeResources()
        {
            float epsilon = 0.00f;
            Vec2f C0 = GameState.PointCreationApi.GetPoint(TilePoint.C0);
            Vec2f C1 = GameState.PointCreationApi.GetPoint(TilePoint.C1);
            Vec2f C2 = GameState.PointCreationApi.GetPoint(TilePoint.C2);
            Vec2f C3 = GameState.PointCreationApi.GetPoint(TilePoint.C3);

            Vec2f M0 = GameState.PointCreationApi.GetPoint(TilePoint.M0);
            Vec2f M1 = GameState.PointCreationApi.GetPoint(TilePoint.M1);
            Vec2f M2 = GameState.PointCreationApi.GetPoint(TilePoint.M2);
            Vec2f M3 = GameState.PointCreationApi.GetPoint(TilePoint.M3);

            GameState.LineCreationApi.Create(TileLineSegment.L_C0_C1, C0 + new Vec2f(epsilon, epsilon), C1 + new Vec2f(-epsilon, epsilon), new Vec2f(0.0f, 1.0f));
            GameState.LineCreationApi.AddMatch(TileLineSegment.L_C2_C3);
            GameState.LineCreationApi.AddMatch(TileLineSegment.L_C3_C2);

            GameState.LineCreationApi.Create(TileLineSegment.L_C1_C2, C1 + new Vec2f(epsilon, -epsilon), C2 + new Vec2f(epsilon, epsilon), new Vec2f(1.0f, 0.0f));
            GameState.LineCreationApi.AddMatch(TileLineSegment.L_C3_C0);
            GameState.LineCreationApi.AddMatch(TileLineSegment.L_C0_C3);

            GameState.LineCreationApi.Create(TileLineSegment.L_C2_C3, C2 + new Vec2f(-epsilon, -epsilon), C3 + new Vec2f(epsilon, -epsilon), new Vec2f(0.0f, -1.0f));
            GameState.LineCreationApi.AddMatch(TileLineSegment.L_C0_C1);
            GameState.LineCreationApi.AddMatch(TileLineSegment.L_C1_C0);

            GameState.LineCreationApi.Create(TileLineSegment.L_C3_C0, C3 + new Vec2f(-epsilon, epsilon), C0 + new Vec2f(-epsilon, -epsilon), new Vec2f(-1.0f, 0.0f));
            GameState.LineCreationApi.AddMatch(TileLineSegment.L_C1_C2);
            GameState.LineCreationApi.AddMatch(TileLineSegment.L_C2_C1);


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

            GameState.LineCreationApi.Create(TileLineSegment.L_M0_C3, M0, C3, new Vec2f(-1.0f, -0.5f).Normalize()); 
            GameState.LineCreationApi.Create(TileLineSegment.L_M1_C0, M1, C0, new Vec2f(-0.5f, -1.0f).Normalize()); 
            GameState.LineCreationApi.Create(TileLineSegment.L_M2_C1, M2, C1, new Vec2f(-1.0f, 0.5f).Normalize()); 
            GameState.LineCreationApi.Create(TileLineSegment.L_C0_M1, C0, M1, new Vec2f(0.5f, 1.0f).Normalize());
            GameState.LineCreationApi.Create(TileLineSegment.L_C0_M2, C0, M2, new Vec2f(1.0f, 0.5f).Normalize());
            GameState.LineCreationApi.Create(TileLineSegment.L_C1_M3, C1, M3, new Vec2f(0.5f, -1.0f).Normalize());
            GameState.LineCreationApi.Create(TileLineSegment.L_C2_M0, C2, M0, new Vec2f(-1.0f, -0.5f).Normalize());
            GameState.LineCreationApi.Create(TileLineSegment.L_M1_C0, M1, C0, new Vec2f(-0.5f, 1.0f).Normalize());

            GameState.LineCreationApi.Create(TileLineSegment.L_C1_M2, C1, M2, new Vec2f(1.0f, -0.5f).Normalize());
            GameState.LineCreationApi.Create(TileLineSegment.L_C0_M1, C0, M1, new Vec2f(0.5f, 1.0f).Normalize());
            GameState.LineCreationApi.Create(TileLineSegment.L_C3_M0, C3, M0, new Vec2f(-1.0f, 0.5f).Normalize());
            GameState.LineCreationApi.Create(TileLineSegment.L_C2_M3, C2, M3, new Vec2f(-0.5f, -1.0f).Normalize());
            GameState.LineCreationApi.Create(TileLineSegment.L_M2_C0, M2, C0, new Vec2f(-1.0f, -0.5f).Normalize());
            GameState.LineCreationApi.Create(TileLineSegment.L_M1_C3, M1, C3, new Vec2f(0.5f, -1.0f).Normalize());
            GameState.LineCreationApi.Create(TileLineSegment.L_M0_C2, M0, C2, new Vec2f(1.0f, 0.5f).Normalize());
            GameState.LineCreationApi.Create(TileLineSegment.L_M3_C1, M3, C1, new Vec2f(0.5f, 1.0f).Normalize());
            



        }


    }

}
