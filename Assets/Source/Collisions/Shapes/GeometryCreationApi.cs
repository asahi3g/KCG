using System;
using System.Collections.Generic;
using KMath;


namespace Collisions
{
    public class GeometryCreationApi
    {
        // Start is called before the first frame update

        private int CurrentIndex; // current index into the Offset Array

        private GeometryProperties[] PropertiesArray; // an array of offsets into the LineSegment array 

        private int LineOffset;
        private int LineCount;
        private TileLineSegment[] SegmentArray;


        public GeometryCreationApi()
        {
            PropertiesArray = new GeometryProperties[256];
            SegmentArray = new TileLineSegment[1024];
            

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

        public GeometryProperties GetProperties(Enums.GeometryTileShape shape)
        {
            Utils.Assert((int)shape >= 0 && (int)shape < PropertiesArray.Length);

            return PropertiesArray[(int)shape];
        }
        public TileLineSegment GetLine(int index)
        {
             Utils.Assert((int)index >= 0 && (int)index < SegmentArray.Length);

             return SegmentArray[index];
        }

        public void Create(Enums.GeometryTileShape Id)
        {
            if ((int)Id + 1 >= PropertiesArray.Length)
            {
                Array.Resize(ref PropertiesArray, PropertiesArray.Length * 2);
            }

            CurrentIndex = (int)Id;
            PropertiesArray[CurrentIndex] = new GeometryProperties{Offset=LineOffset};
        }
        

        
        public void AddLine(TileLineSegment line)
        {
            if ((int)line >= SegmentArray.Length)
            {
                Array.Resize(ref SegmentArray, SegmentArray.Length * 2);
            }

            SegmentArray[LineOffset++] = line;
            LineCount++;
        }


        public void End()
        {
            PropertiesArray[CurrentIndex].Size = LineCount;
            LineCount = 0;
        }



        public void InitializeResources()
        {
            GameState.GeometryCreationApi.Create(Enums.GeometryTileShape.SB_R0);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C0_C1);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C1_C2);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C2_C3);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C3_C0);
            /*GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C3_C1);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C1_C2);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C2_C3);*/
            GameState.GeometryCreationApi.End();


            GameState.GeometryCreationApi.Create(Enums.GeometryTileShape.SA_R0);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C0_C1);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C1_C2);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C2_C3);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C3_C0);
            GameState.GeometryCreationApi.End();




            GameState.GeometryCreationApi.Create(Enums.GeometryTileShape.HB_R0);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C0_M0);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_M0_M2);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_M2_C3);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C3_C0);
            GameState.GeometryCreationApi.End();

            GameState.GeometryCreationApi.Create(Enums.GeometryTileShape.HB_R1);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C0_C1);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C1_M1);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_M1_M3);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_M3_C0);
            GameState.GeometryCreationApi.End();

            GameState.GeometryCreationApi.Create(Enums.GeometryTileShape.HB_R2);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_M0_C1);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C1_C2);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C2_M2);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_M2_M0);
            GameState.GeometryCreationApi.End();

            GameState.GeometryCreationApi.Create(Enums.GeometryTileShape.HB_R3);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_M3_M1);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_M1_C2);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C2_C3);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C3_M3);
            GameState.GeometryCreationApi.End();



            GameState.GeometryCreationApi.Create(Enums.GeometryTileShape.L1_R0);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C0_M0);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_M0_C3);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C3_C0);
            GameState.GeometryCreationApi.End();

            GameState.GeometryCreationApi.Create(Enums.GeometryTileShape.L1_R1);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C0_C1);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C1_M1);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_M1_C0);
            GameState.GeometryCreationApi.End();

            GameState.GeometryCreationApi.Create(Enums.GeometryTileShape.L1_R2);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C1_C2);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C2_M2);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_M2_C1);
            GameState.GeometryCreationApi.End();

            GameState.GeometryCreationApi.Create(Enums.GeometryTileShape.L1_R3);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C0_M1);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_M1_M3);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_M3_C0);
            GameState.GeometryCreationApi.End();

            GameState.GeometryCreationApi.Create(Enums.GeometryTileShape.L1_R4);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C0_M2);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_M2_C3);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C3_C0);
            GameState.GeometryCreationApi.End();

            GameState.GeometryCreationApi.Create(Enums.GeometryTileShape.L1_R5);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C0_C1);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C1_M3);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_M3_C0);
            GameState.GeometryCreationApi.End();

            GameState.GeometryCreationApi.Create(Enums.GeometryTileShape.L1_R6);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_M0_C1);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C1_C2);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C2_M0);
            GameState.GeometryCreationApi.End();

            GameState.GeometryCreationApi.Create(Enums.GeometryTileShape.L1_R7);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_M1_C2);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C2_C3);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C3_M1);
            GameState.GeometryCreationApi.End();

            GameState.GeometryCreationApi.Create(Enums.GeometryTileShape.L2_R0);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C0_C1);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C1_M2);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_M2_C3);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C3_C0);
            GameState.GeometryCreationApi.End();

            GameState.GeometryCreationApi.Create(Enums.GeometryTileShape.L2_R1);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C0_M1);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_M1_C2);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C2_C3);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C3_C0);
            GameState.GeometryCreationApi.End();

            GameState.GeometryCreationApi.Create(Enums.GeometryTileShape.L2_R2);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_M0_C1);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C1_C2);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C2_C3);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C3_M0);
            GameState.GeometryCreationApi.End();

            GameState.GeometryCreationApi.Create(Enums.GeometryTileShape.L2_R3);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C0_C1);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C1_C2);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C2_M3);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_M3_C0);
            GameState.GeometryCreationApi.End();

            GameState.GeometryCreationApi.Create(Enums.GeometryTileShape.L2_R4);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C0_C1);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C1_C2);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C2_M2);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_M2_C0);
            GameState.GeometryCreationApi.End();

            GameState.GeometryCreationApi.Create(Enums.GeometryTileShape.L2_R5);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C0_C1);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C1_M1);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_M1_C3);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C3_C0);
            GameState.GeometryCreationApi.End();

            GameState.GeometryCreationApi.Create(Enums.GeometryTileShape.L2_R6);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C0_M0);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_M0_C2);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C2_C3);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C3_C0);
            GameState.GeometryCreationApi.End();

            GameState.GeometryCreationApi.Create(Enums.GeometryTileShape.L2_R7);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_M3_C1);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C1_C2);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C2_C3);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C3_M3);
            GameState.GeometryCreationApi.End();



            






            GameState.GeometryCreationApi.Create(Enums.GeometryTileShape.TB_R0);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C3_C1);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C1_C2);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C2_C3);
            GameState.GeometryCreationApi.End();

            GameState.GeometryCreationApi.Create(Enums.GeometryTileShape.TB_R1);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C0_C2);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C2_C3);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C3_C0);
            GameState.GeometryCreationApi.End();


            GameState.GeometryCreationApi.Create(Enums.GeometryTileShape.TB_R2);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C0_C1);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C1_C2);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C2_C0);
            GameState.GeometryCreationApi.End();


            GameState.GeometryCreationApi.Create(Enums.GeometryTileShape.TB_R3);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C0_C1);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C1_C3);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C3_C0);
            GameState.GeometryCreationApi.End();



            GameState.GeometryCreationApi.Create(Enums.GeometryTileShape.TB_R7);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C3_C1);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C1_C2);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C2_C3);
            GameState.GeometryCreationApi.End();

            GameState.GeometryCreationApi.Create(Enums.GeometryTileShape.TB_R6);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C0_C2);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C2_C3);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C3_C0);
            GameState.GeometryCreationApi.End();

            GameState.GeometryCreationApi.Create(Enums.GeometryTileShape.TB_R5);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C0_C1);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C1_C2);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C2_C0);
            GameState.GeometryCreationApi.End();

            GameState.GeometryCreationApi.Create(Enums.GeometryTileShape.TB_R4);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C0_C1);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C1_C3);
            GameState.GeometryCreationApi.AddLine(TileLineSegment.L_C3_C0);
            GameState.GeometryCreationApi.End();



        }


    }

}
