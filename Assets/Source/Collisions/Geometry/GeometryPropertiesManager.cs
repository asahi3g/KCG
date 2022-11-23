using System;
using System.Collections.Generic;
using KMath;


namespace Collisions
{
    public class GeometryPropertiesManager
    {
        // Start is called before the first frame update

        private int CurrentIndex; // current index into the Offset Array

        private GeometryProperties[] PropertiesArray; // an array of offsets into the LineSegment array 

        private int LineOffset;
        private int LineCount;
        private GeometryLineProperty[] SegmentArray;


        public GeometryPropertiesManager()
        {
            PropertiesArray = new GeometryProperties[256];
            SegmentArray = new GeometryLineProperty[1024];
            

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

        public GeometryProperties GetProperties(Enums.TileGeometryAndRotation shape)
        {
            Utility.Utils.Assert((int)shape >= 0 && (int)shape < PropertiesArray.Length);

            return PropertiesArray[(int)shape];
        }
        public TileLineSegment GetLine(int index)
        {
             Utility.Utils.Assert((int)index >= 0 && (int)index < SegmentArray.Length);

             return SegmentArray[index].Line;
        }

        public GeometryLineSide GetSide(int index)
        {
            Utility.Utils.Assert((int)index >= 0 && (int)index < SegmentArray.Length);

             return SegmentArray[index].Side;
        }

        public void Create(Enums.TileGeometryAndRotation Id)
        {
            if ((int)Id + 1 >= PropertiesArray.Length)
            {
                Array.Resize(ref PropertiesArray, PropertiesArray.Length + 1024);
            }

            CurrentIndex = (int)Id;
            PropertiesArray[CurrentIndex] = new GeometryProperties{Offset=LineOffset};
        }
        

        
        public void AddLine(TileLineSegment line, GeometryLineSide side = GeometryLineSide.Right)
        {
            if ((int)line >= SegmentArray.Length)
            {
                Array.Resize(ref SegmentArray, SegmentArray.Length * 2);
            }

            SegmentArray[LineOffset].Side = side;
            SegmentArray[LineOffset++].Line = line;
            LineCount++;
        }


        public void End()
        {
            PropertiesArray[CurrentIndex].Size = LineCount;
            LineCount = 0;
        }



        public void InitializeResources()
        {
            GeometryPropertiesManager Api = GameState.GeometryPropertiesManager;

            Api.Create(Enums.TileGeometryAndRotation.SB_R0);
            Api.AddLine(TileLineSegment.L_C0_C1, GeometryLineSide.Top);
            Api.AddLine(TileLineSegment.L_C1_C2, GeometryLineSide.Right);
            Api.AddLine(TileLineSegment.L_C2_C3, GeometryLineSide.Bottom);
            Api.AddLine(TileLineSegment.L_C3_C0, GeometryLineSide.Left);
            Api.End();


            Api.Create(Enums.TileGeometryAndRotation.SA_R0);
            Api.AddLine(TileLineSegment.L_C0_C1);
            Api.AddLine(TileLineSegment.L_C1_C2);
            Api.AddLine(TileLineSegment.L_C2_C3);
            Api.AddLine(TileLineSegment.L_C3_C0);
            Api.End();




            Api.Create(Enums.TileGeometryAndRotation.HB_R0);
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
            Api.End();



            Api.Create(Enums.TileGeometryAndRotation.L1_R0);
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
            Api.End();



            

            Api.Create(Enums.TileGeometryAndRotation.TB_R0);
            Api.AddLine(TileLineSegment.L_C3_C1, GeometryLineSide.Top);
            Api.AddLine(TileLineSegment.L_C1_C2, GeometryLineSide.Right);
            Api.AddLine(TileLineSegment.L_C2_C3, GeometryLineSide.Bottom);
            Api.End();

            Api.Create(Enums.TileGeometryAndRotation.TB_R1);
            Api.AddLine(TileLineSegment.L_C0_C2, GeometryLineSide.Top);
            Api.AddLine(TileLineSegment.L_C2_C3, GeometryLineSide.Bottom);
            Api.AddLine(TileLineSegment.L_C3_C0, GeometryLineSide.Left);
            Api.End();


            Api.Create(Enums.TileGeometryAndRotation.TB_R2);
            Api.AddLine(TileLineSegment.L_C0_C1, GeometryLineSide.Top);
            Api.AddLine(TileLineSegment.L_C1_C2, GeometryLineSide.Right);
            Api.AddLine(TileLineSegment.L_C2_C0, GeometryLineSide.Bottom);
            Api.End();


            Api.Create(Enums.TileGeometryAndRotation.TB_R3);
            Api.AddLine(TileLineSegment.L_C0_C1, GeometryLineSide.Top);
            Api.AddLine(TileLineSegment.L_C1_C3, GeometryLineSide.Right);
            Api.AddLine(TileLineSegment.L_C3_C0, GeometryLineSide.Left);
            Api.End();

        }


    }

}
