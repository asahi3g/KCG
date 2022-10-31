using System;
using System.Collections.Generic;
using KMath;


namespace Collisions
{
    public class PointCreationApi
    {
        // Start is called before the first frame update

        private int CurrentIndex;
        private Vec2f[] PointArray;

        public PointCreationApi()
        {
            PointArray = new Vec2f[1024];
            for(int i = 0; i < PointArray.Length; i++)
            {
                PointArray[i] = new Vec2f();
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

        public Vec2f GetPoint(TilePoint point)
        {
            if ((int)point >= 0 && (int)point < PointArray.Length)
            {
                Vec2f p = PointArray[(int)point];
                return p;
            }

            return new Vec2f();
        }

        public void Create(TilePoint point, Vec2f value)
        {
            while ((int)point >= PointArray.Length)
            {
                Array.Resize(ref PointArray, PointArray.Length * 2);
            }

            CurrentIndex = (int)point;
            if (CurrentIndex != -1)
            {
                PointArray[CurrentIndex] = value;
            }
        }


        public void InitializeResources()
        {
            Vec2f C0 = new Vec2f(0.0f, 1.0f);
            Vec2f C1 = new Vec2f(1.0f, 1.0f);
            Vec2f C2 = new Vec2f(1.0f, 0.0f);
            Vec2f C3 = new Vec2f(0.0f, 0.0f);

            Vec2f M0 = new Vec2f(0.5f, 1.0f);
            Vec2f M1 = new Vec2f(1.0f, 0.5f);
            Vec2f M2 = new Vec2f(0.5f, 0.0f);
            Vec2f M3 = new Vec2f(0.0f, 0.5f);

            GameState.PointCreationApi.Create(TilePoint.C0, C0);
            GameState.PointCreationApi.Create(TilePoint.C1, C1);
            GameState.PointCreationApi.Create(TilePoint.C2, C2);
            GameState.PointCreationApi.Create(TilePoint.C3, C3);

            GameState.PointCreationApi.Create(TilePoint.M0, M0);
            GameState.PointCreationApi.Create(TilePoint.M1, M1);
            GameState.PointCreationApi.Create(TilePoint.M2, M2);
            GameState.PointCreationApi.Create(TilePoint.M3, M3);

        }


    }

}
