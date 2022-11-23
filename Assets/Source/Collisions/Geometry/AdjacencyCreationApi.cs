using System;
using System.Collections.Generic;
using KMath;


namespace Collisions
{
    public class AdjacencyCreationApi
    {
        // Start is called before the first frame update

        private int CurrentIndex; // current index into the Offset Array

        private AdjacencyProperties[] PropertiesArray; // an array of offsets into the LineSegment array 

        private int LineOffset;
        private int LineCount;
        private TileLineSegment[] SegmentArray;


        public AdjacencyCreationApi()
        {
            PropertiesArray = new AdjacencyProperties[256];
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

        public AdjacencyProperties GetProperties(Enums.TileGeometryAndRotationAndAdjacency shapeAndAdjacency)
        {
            Utility.Utils.Assert((int)shapeAndAdjacency >= 0 && (int)shapeAndAdjacency < PropertiesArray.Length);

            return PropertiesArray[(int)shapeAndAdjacency];
        }

        public TileLineSegment GetLine(int index)
        {
             Utility.Utils.Assert((int)index >= 0 && (int)index < SegmentArray.Length);

             return SegmentArray[index];
        }

        public void Create(Enums.TileGeometryAndRotationAndAdjacency Id)
        {
            if ((int)Id + 1 >= PropertiesArray.Length)
            {
                Array.Resize(ref PropertiesArray, PropertiesArray.Length * 2);
            }

            CurrentIndex = (int)Id;
            PropertiesArray[CurrentIndex] = new AdjacencyProperties{Offset=LineOffset};
        }
        

        
        public void AddLine(TileLineSegment line)
        {
            if ((int)line >= SegmentArray.Length)
            {
                Array.Resize(ref SegmentArray, SegmentArray.Length + 1024);
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
            AdjacencyCreationApi Api = GameState.AdjacencyCreationApi;

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.SB_R0_A0000);
            Api.AddLine(TileLineSegment.L_C0_C1); // top (0010)
            Api.AddLine(TileLineSegment.L_C1_C2); // right (1000)
            Api.AddLine(TileLineSegment.L_C2_C3); // bottom (0001)
            Api.AddLine(TileLineSegment.L_C3_C0); // left (0100)
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.SB_R0_A0001);
            Api.AddLine(TileLineSegment.L_C0_C1); // top (0010)
            Api.AddLine(TileLineSegment.L_C1_C2); // right (1000)
            Api.AddLine(TileLineSegment.L_C3_C0); // left (0100)
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.SB_R0_A0010);
            Api.AddLine(TileLineSegment.L_C1_C2); // right (1000)
            Api.AddLine(TileLineSegment.L_C2_C3); // bottom (0001)
            Api.AddLine(TileLineSegment.L_C3_C0); // left (0100)
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.SB_R0_A0011);
            Api.AddLine(TileLineSegment.L_C1_C2); // right (1000)
            Api.AddLine(TileLineSegment.L_C3_C0); // left (0100)
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.SB_R0_A0100);
            Api.AddLine(TileLineSegment.L_C0_C1); // top (0010)
            Api.AddLine(TileLineSegment.L_C1_C2); // right (1000)
            Api.AddLine(TileLineSegment.L_C2_C3); // bottom (0001)
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.SB_R0_A0101);
            Api.AddLine(TileLineSegment.L_C0_C1); // top (0010)
            Api.AddLine(TileLineSegment.L_C1_C2); // right (1000)
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.SB_R0_A0110);
            Api.AddLine(TileLineSegment.L_C1_C2); // right (1000)
            Api.AddLine(TileLineSegment.L_C2_C3); // bottom (0001)
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.SB_R0_A0111);
            Api.AddLine(TileLineSegment.L_C1_C2); // right (1000)
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.SB_R0_A1000);
            Api.AddLine(TileLineSegment.L_C0_C1); // top (0010)
            Api.AddLine(TileLineSegment.L_C2_C3); // bottom (0001)
            Api.AddLine(TileLineSegment.L_C3_C0); // left (0100)
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.SB_R0_A1001);
            Api.AddLine(TileLineSegment.L_C0_C1); // top (0010)
            Api.AddLine(TileLineSegment.L_C3_C0); // left (0100)
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.SB_R0_A1010);
            Api.AddLine(TileLineSegment.L_C2_C3); // bottom (0001)
            Api.AddLine(TileLineSegment.L_C3_C0); // left (0100)
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.SB_R0_A1011);
            Api.AddLine(TileLineSegment.L_C3_C0); // left (0100)
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.SB_R0_A1100);
            Api.AddLine(TileLineSegment.L_C0_C1); // top (0010)
            Api.AddLine(TileLineSegment.L_C2_C3); // bottom (0001)
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.SB_R0_A1101);
            Api.AddLine(TileLineSegment.L_C0_C1); // top (0010)
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.SB_R0_A1110);
            Api.AddLine(TileLineSegment.L_C2_C3); // bottom (0001)
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.SB_R0_A1111);
            Api.End();


        /*    Api.Create(Enums.TileGeometryAndRotation.HB_R0);
            Api.AddLine(TileLineSegment.L_C0_M0);
            Api.AddLine(TileLineSegment.L_M0_M2);
            Api.AddLine(TileLineSegment.L_M2_C3);
            Api.AddLine(TileLineSegment.L_C3_C0);
            Api.End();

            Api.Create(Enums.TileGeometryAndRotation.HB_R1);
            Api.AddLine(TileLineSegment.L_C0_C1);
            Api.AddLine(TileLineSegment.L_C1_M1);
            Api.AddLine(TileLineSegment.L_M1_M3);
            Api.AddLine(TileLineSegment.L_M3_C0);
            Api.End();

            Api.Create(Enums.TileGeometryAndRotation.HB_R2);
            Api.AddLine(TileLineSegment.L_M0_C1);
            Api.AddLine(TileLineSegment.L_C1_C2);
            Api.AddLine(TileLineSegment.L_C2_M2);
            Api.AddLine(TileLineSegment.L_M2_M0);
            Api.End();

            Api.Create(Enums.TileGeometryAndRotation.HB_R3);
            Api.AddLine(TileLineSegment.L_M3_M1);
            Api.AddLine(TileLineSegment.L_M1_C2);
            Api.AddLine(TileLineSegment.L_C2_C3);
            Api.AddLine(TileLineSegment.L_C3_M3);
            Api.End();*/



         /*   Api.Create(Enums.TileGeometryAndRotation.L1_R0);
            Api.AddLine(TileLineSegment.L_C0_M0);
            Api.AddLine(TileLineSegment.L_M0_C3);
            Api.AddLine(TileLineSegment.L_C3_C0);
            Api.End();

            Api.Create(Enums.TileGeometryAndRotation.L1_R1);
            Api.AddLine(TileLineSegment.L_C0_C1);
            Api.AddLine(TileLineSegment.L_C1_M1);
            Api.AddLine(TileLineSegment.L_M1_C0);
            Api.End();

            Api.Create(Enums.TileGeometryAndRotation.L1_R2);
            Api.AddLine(TileLineSegment.L_M2_C1);
            Api.AddLine(TileLineSegment.L_C1_C2);
            Api.AddLine(TileLineSegment.L_C2_M2);
            Api.End();

            Api.Create(Enums.TileGeometryAndRotation.L1_R3);
            Api.AddLine(TileLineSegment.L_C0_M1);
            Api.AddLine(TileLineSegment.L_M1_M3);
            Api.AddLine(TileLineSegment.L_M3_C0);
            Api.End();

            Api.Create(Enums.TileGeometryAndRotation.L1_R4);
            Api.AddLine(TileLineSegment.L_C0_M2);
            Api.AddLine(TileLineSegment.L_M2_C3);
            Api.AddLine(TileLineSegment.L_C3_C0);
            Api.End();

            Api.Create(Enums.TileGeometryAndRotation.L1_R5);
            Api.AddLine(TileLineSegment.L_C0_C1);
            Api.AddLine(TileLineSegment.L_C1_M3);
            Api.AddLine(TileLineSegment.L_M3_C0);
            Api.End();

            Api.Create(Enums.TileGeometryAndRotation.L1_R6);
            Api.AddLine(TileLineSegment.L_M0_C1);
            Api.AddLine(TileLineSegment.L_C1_C2);
            Api.AddLine(TileLineSegment.L_C2_M0);
            Api.End();

            Api.Create(Enums.TileGeometryAndRotation.L1_R7);
            Api.AddLine(TileLineSegment.L_M1_C2);
            Api.AddLine(TileLineSegment.L_C2_C3);
            Api.AddLine(TileLineSegment.L_C3_M1);
            Api.End();

            Api.Create(Enums.TileGeometryAndRotation.L2_R0);
            Api.AddLine(TileLineSegment.L_C0_C1);
            Api.AddLine(TileLineSegment.L_C1_M2);
            Api.AddLine(TileLineSegment.L_M2_C3);
            Api.AddLine(TileLineSegment.L_C3_C0);
            Api.End();

            Api.Create(Enums.TileGeometryAndRotation.L2_R1);
            Api.AddLine(TileLineSegment.L_C0_M1);
            Api.AddLine(TileLineSegment.L_M1_C2);
            Api.AddLine(TileLineSegment.L_C2_C3);
            Api.AddLine(TileLineSegment.L_C3_C0);
            Api.End();

            Api.Create(Enums.TileGeometryAndRotation.L2_R2);
            Api.AddLine(TileLineSegment.L_M0_C1);
            Api.AddLine(TileLineSegment.L_C1_C2);
            Api.AddLine(TileLineSegment.L_C2_C3);
            Api.AddLine(TileLineSegment.L_C3_M0);
            Api.End();

            Api.Create(Enums.TileGeometryAndRotation.L2_R3);
            Api.AddLine(TileLineSegment.L_C0_C1);
            Api.AddLine(TileLineSegment.L_C1_C2);
            Api.AddLine(TileLineSegment.L_C2_M3);
            Api.AddLine(TileLineSegment.L_M3_C0);
            Api.End();

            Api.Create(Enums.TileGeometryAndRotation.L2_R4);
            Api.AddLine(TileLineSegment.L_C0_C1);
            Api.AddLine(TileLineSegment.L_C1_C2);
            Api.AddLine(TileLineSegment.L_C2_M2);
            Api.AddLine(TileLineSegment.L_M2_C0);
            Api.End();

            Api.Create(Enums.TileGeometryAndRotation.L2_R5);
            Api.AddLine(TileLineSegment.L_C0_C1);
            Api.AddLine(TileLineSegment.L_C1_M1);
            Api.AddLine(TileLineSegment.L_M1_C3);
            Api.AddLine(TileLineSegment.L_C3_C0);
            Api.End();

            Api.Create(Enums.TileGeometryAndRotation.L2_R6);
            Api.AddLine(TileLineSegment.L_C0_M0);
            Api.AddLine(TileLineSegment.L_M0_C2);
            Api.AddLine(TileLineSegment.L_C2_C3);
            Api.AddLine(TileLineSegment.L_C3_C0);
            Api.End();

            Api.Create(Enums.TileGeometryAndRotation.L2_R7);
            Api.AddLine(TileLineSegment.L_M3_C1);
            Api.AddLine(TileLineSegment.L_C1_C2);
            Api.AddLine(TileLineSegment.L_C2_C3);
            Api.AddLine(TileLineSegment.L_C3_M3);
            Api.End();*/


            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.TB_R0_A0000);
            Api.AddLine(TileLineSegment.L_C3_C1); // top (0010)
            Api.AddLine(TileLineSegment.L_C1_C2); // right (1000)
            Api.AddLine(TileLineSegment.L_C2_C3); // bottom (0001)
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.TB_R0_A0001);
            Api.AddLine(TileLineSegment.L_C3_C1); // top (0010)
            Api.AddLine(TileLineSegment.L_C1_C2); // right (1000)
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.TB_R0_A0010);
            Api.AddLine(TileLineSegment.L_C3_C1); // top (0010)
            Api.AddLine(TileLineSegment.L_C1_C2); // right (1000)
            Api.AddLine(TileLineSegment.L_C2_C3); // bottom (0001)
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.TB_R0_A0011);
            Api.AddLine(TileLineSegment.L_C3_C1); // top (0010)
            Api.AddLine(TileLineSegment.L_C1_C2); // right (1000)
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.TB_R0_A0100);
            Api.AddLine(TileLineSegment.L_C3_C1); // top (0010)
            Api.AddLine(TileLineSegment.L_C1_C2); // right (1000)
            Api.AddLine(TileLineSegment.L_C2_C3); // bottom (0001)
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.TB_R0_A0101);
            Api.AddLine(TileLineSegment.L_C3_C1); // top (0010)
            Api.AddLine(TileLineSegment.L_C1_C2); // right (1000)
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.TB_R0_A0110);
            Api.AddLine(TileLineSegment.L_C3_C1); // top (0010)
            Api.AddLine(TileLineSegment.L_C1_C2); // right (1000)
            Api.AddLine(TileLineSegment.L_C2_C3); // bottom (0001)
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.TB_R0_A0111);
            Api.AddLine(TileLineSegment.L_C3_C1); // top (0010)
            Api.AddLine(TileLineSegment.L_C1_C2); // right (1000)
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.TB_R0_A1000);
            Api.AddLine(TileLineSegment.L_C3_C1); // top (0010)
            Api.AddLine(TileLineSegment.L_C2_C3); // bottom (0001)
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.TB_R0_A1001);
            Api.AddLine(TileLineSegment.L_C3_C1); // top (0010)
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.TB_R0_A1010);
            Api.AddLine(TileLineSegment.L_C3_C1); // top (0010)
            Api.AddLine(TileLineSegment.L_C2_C3); // bottom (0001)
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.TB_R0_A1011);
            Api.AddLine(TileLineSegment.L_C3_C1); // top (0010)

            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.TB_R0_A1100);
            Api.AddLine(TileLineSegment.L_C3_C1); // top (0010)
            Api.AddLine(TileLineSegment.L_C2_C3); // bottom (0001)
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.TB_R0_A1101);
            Api.AddLine(TileLineSegment.L_C3_C1); // top (0010)
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.TB_R0_A1110);
            Api.AddLine(TileLineSegment.L_C3_C1); // top (0010)
            Api.AddLine(TileLineSegment.L_C2_C3); // bottom (0001)
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.TB_R0_A1111);
            Api.AddLine(TileLineSegment.L_C3_C1); // top (0010)
            Api.End();











            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.TB_R1_A0000);
            Api.AddLine(TileLineSegment.L_C3_C1); // top (0010)
            Api.AddLine(TileLineSegment.L_C1_C2); // right (1000)
            Api.AddLine(TileLineSegment.L_C2_C3); // bottom (0001)

            Api.AddLine(TileLineSegment.L_C0_C2); // 
            Api.AddLine(TileLineSegment.L_C2_C3);
            Api.AddLine(TileLineSegment.L_C3_C0);

            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.TB_R1_A0001);
            Api.AddLine(TileLineSegment.L_C3_C1); // top (0010)
            Api.AddLine(TileLineSegment.L_C1_C2); // right (1000)
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.TB_R1_A0010);
            Api.AddLine(TileLineSegment.L_C3_C1); // top (0010)
            Api.AddLine(TileLineSegment.L_C1_C2); // right (1000)
            Api.AddLine(TileLineSegment.L_C2_C3); // bottom (0001)
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.TB_R1_A0011);
            Api.AddLine(TileLineSegment.L_C3_C1); // top (0010)
            Api.AddLine(TileLineSegment.L_C1_C2); // right (1000)
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.TB_R1_A0100);
            Api.AddLine(TileLineSegment.L_C3_C1); // top (0010)
            Api.AddLine(TileLineSegment.L_C1_C2); // right (1000)
            Api.AddLine(TileLineSegment.L_C2_C3); // bottom (0001)
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.TB_R1_A0101);
            Api.AddLine(TileLineSegment.L_C3_C1); // top (0010)
            Api.AddLine(TileLineSegment.L_C1_C2); // right (1000)
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.TB_R1_A0110);
            Api.AddLine(TileLineSegment.L_C3_C1); // top (0010)
            Api.AddLine(TileLineSegment.L_C1_C2); // right (1000)
            Api.AddLine(TileLineSegment.L_C2_C3); // bottom (0001)
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.TB_R1_A0111);
            Api.AddLine(TileLineSegment.L_C3_C1); // top (0010)
            Api.AddLine(TileLineSegment.L_C1_C2); // right (1000)
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.TB_R1_A1000);
            Api.AddLine(TileLineSegment.L_C3_C1); // top (0010)
            Api.AddLine(TileLineSegment.L_C2_C3); // bottom (0001)
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.TB_R1_A1001);
            Api.AddLine(TileLineSegment.L_C3_C1); // top (0010)
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.TB_R1_A1010);
            Api.AddLine(TileLineSegment.L_C3_C1); // top (0010)
            Api.AddLine(TileLineSegment.L_C2_C3); // bottom (0001)
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.TB_R1_A1011);
            Api.AddLine(TileLineSegment.L_C3_C1); // top (0010)
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.TB_R1_A1100);
            Api.AddLine(TileLineSegment.L_C3_C1); // top (0010)
            Api.AddLine(TileLineSegment.L_C2_C3); // bottom (0001)
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.TB_R1_A1101);
            Api.AddLine(TileLineSegment.L_C3_C1); // top (0010)
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.TB_R1_A1110);
            Api.AddLine(TileLineSegment.L_C3_C1); // top (0010)
            Api.AddLine(TileLineSegment.L_C2_C3); // bottom (0001)
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.TB_R1_A1111);
            Api.AddLine(TileLineSegment.L_C3_C1); // top (0010)
            Api.End();


            /*Api.Create(Enums.TileGeometryAndRotation.TB_R1);
            Api.AddLine(TileLineSegment.L_C0_C2);
            Api.AddLine(TileLineSegment.L_C2_C3);
            Api.AddLine(TileLineSegment.L_C3_C0);
            Api.End();


            Api.Create(Enums.TileGeometryAndRotation.TB_R2);
            Api.AddLine(TileLineSegment.L_C0_C1);
            Api.AddLine(TileLineSegment.L_C1_C2);
            Api.AddLine(TileLineSegment.L_C2_C0);
            Api.End();


            Api.Create(Enums.TileGeometryAndRotation.TB_R3);
            Api.AddLine(TileLineSegment.L_C0_C1);
            Api.AddLine(TileLineSegment.L_C1_C3);
            Api.AddLine(TileLineSegment.L_C3_C0);
            Api.End();*/

        }


    }

}
