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
        private GeometryLineProperty[] SegmentArray; // an array of lines
        private Enums.TileGeometryAndRotationAndAdjacency[] AdjacencyArray; // adjacency map


        public GeometryPropertiesManager()
        {
            PropertiesArray = new GeometryProperties[256];
            SegmentArray = new GeometryLineProperty[1024];
            AdjacencyArray = new Enums.TileGeometryAndRotationAndAdjacency[PropertiesArray.Length * (int)Enums.TileAdjacency.Count];
            

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

        public Enums.TileGeometryAndRotationAndAdjacency GetAdjacency(Enums.TileGeometryAndRotation shape, Enums.TileAdjacency adjacency)
        {
            Utility.Utils.Assert((int)shape * (int)Enums.TileAdjacency.Count + (int)adjacency < AdjacencyArray.Length);

            return AdjacencyArray[(int)shape * (int)Enums.TileAdjacency.Count + (int)adjacency];
        }

        public void Create(Enums.TileGeometryAndRotation Id,
         int mask = (int)GeometryLineSide.North | (int)GeometryLineSide.East | (int)GeometryLineSide.South | (int)GeometryLineSide.West)
        {
            if ((int)Id + 1 >= PropertiesArray.Length)
            {
                Array.Resize(ref PropertiesArray, PropertiesArray.Length + 1024);
                Array.Resize(ref AdjacencyArray, PropertiesArray.Length * (int)Enums.TileAdjacency.Count);
            }

            CurrentIndex = (int)Id;
            PropertiesArray[CurrentIndex] = new GeometryProperties{Offset=LineOffset, Mask=mask};
        }

        public void SetAdjacency(Enums.TileAdjacency adjacency, Enums.TileGeometryAndRotationAndAdjacency value)
        {
            Utility.Utils.Assert(CurrentIndex * (int)Enums.TileAdjacency.Count + (int)adjacency < AdjacencyArray.Length);

            AdjacencyArray[CurrentIndex * (int)Enums.TileAdjacency.Count + (int)adjacency] = value;
        }

        

        
        public void AddLine(TileLineSegment line, GeometryLineSide side = GeometryLineSide.North)
        {
            if ((int)line >= SegmentArray.Length)
            {
                Array.Resize(ref SegmentArray, SegmentArray.Length + 1024);         
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

            Api.Create(Enums.TileGeometryAndRotation.SB_R0, 
            (int)GeometryLineSide.North | (int)GeometryLineSide.East | (int)GeometryLineSide.South | (int)GeometryLineSide.West);
            Api.AddLine(TileLineSegment.L_C0_C1, GeometryLineSide.North);
            Api.AddLine(TileLineSegment.L_C1_C2, GeometryLineSide.East);
            Api.AddLine(TileLineSegment.L_C2_C3, GeometryLineSide.South);
            Api.AddLine(TileLineSegment.L_C3_C0, GeometryLineSide.West);

            Api.SetAdjacency(Enums.TileAdjacency.A0000, Enums.TileGeometryAndRotationAndAdjacency.SB_R0_A0000);
            Api.SetAdjacency(Enums.TileAdjacency.A0001, Enums.TileGeometryAndRotationAndAdjacency.SB_R0_A0001);
            Api.SetAdjacency(Enums.TileAdjacency.A0010, Enums.TileGeometryAndRotationAndAdjacency.SB_R0_A0010);
            Api.SetAdjacency(Enums.TileAdjacency.A0011, Enums.TileGeometryAndRotationAndAdjacency.SB_R0_A0011);
            Api.SetAdjacency(Enums.TileAdjacency.A0100, Enums.TileGeometryAndRotationAndAdjacency.SB_R0_A0100);
            Api.SetAdjacency(Enums.TileAdjacency.A0101, Enums.TileGeometryAndRotationAndAdjacency.SB_R0_A0101);
            Api.SetAdjacency(Enums.TileAdjacency.A0110, Enums.TileGeometryAndRotationAndAdjacency.SB_R0_A0110);
            Api.SetAdjacency(Enums.TileAdjacency.A0111, Enums.TileGeometryAndRotationAndAdjacency.SB_R0_A0111);
            Api.SetAdjacency(Enums.TileAdjacency.A1000, Enums.TileGeometryAndRotationAndAdjacency.SB_R0_A1000);
            Api.SetAdjacency(Enums.TileAdjacency.A1001, Enums.TileGeometryAndRotationAndAdjacency.SB_R0_A1001);
            Api.SetAdjacency(Enums.TileAdjacency.A1010, Enums.TileGeometryAndRotationAndAdjacency.SB_R0_A1010);
            Api.SetAdjacency(Enums.TileAdjacency.A1011, Enums.TileGeometryAndRotationAndAdjacency.SB_R0_A1011);
            Api.SetAdjacency(Enums.TileAdjacency.A1100, Enums.TileGeometryAndRotationAndAdjacency.SB_R0_A1100);
            Api.SetAdjacency(Enums.TileAdjacency.A1101, Enums.TileGeometryAndRotationAndAdjacency.SB_R0_A1101);
            Api.SetAdjacency(Enums.TileAdjacency.A1110, Enums.TileGeometryAndRotationAndAdjacency.SB_R0_A1110);
            Api.SetAdjacency(Enums.TileAdjacency.A1111, Enums.TileGeometryAndRotationAndAdjacency.SB_R0_A1111);

            Api.End();


            Api.Create(Enums.TileGeometryAndRotation.SA_R0);
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



            

            Api.Create(Enums.TileGeometryAndRotation.TB_R0, (int)GeometryLineSide.East | (int)GeometryLineSide.South);
            Api.AddLine(TileLineSegment.L_C3_C1, GeometryLineSide.North);
            Api.AddLine(TileLineSegment.L_C1_C2, GeometryLineSide.East);
            Api.AddLine(TileLineSegment.L_C2_C3, GeometryLineSide.South);
            
            Api.SetAdjacency(Enums.TileAdjacency.A0000, Enums.TileGeometryAndRotationAndAdjacency.TB_R0_AX00X);
            Api.SetAdjacency(Enums.TileAdjacency.A0010, Enums.TileGeometryAndRotationAndAdjacency.TB_R0_AX01X);
            Api.SetAdjacency(Enums.TileAdjacency.A0100, Enums.TileGeometryAndRotationAndAdjacency.TB_R0_AX10X);
            Api.SetAdjacency(Enums.TileAdjacency.A0110, Enums.TileGeometryAndRotationAndAdjacency.TB_R0_AX11X);
            Api.End();

            Api.Create(Enums.TileGeometryAndRotation.TB_R1,
            (int)GeometryLineSide.South | (int)GeometryLineSide.West);
            Api.AddLine(TileLineSegment.L_C0_C2, GeometryLineSide.North);
            Api.AddLine(TileLineSegment.L_C2_C3, GeometryLineSide.South);
            Api.AddLine(TileLineSegment.L_C3_C0, GeometryLineSide.West);

            Api.SetAdjacency(Enums.TileAdjacency.A0000, Enums.TileGeometryAndRotationAndAdjacency.TB_R1_AXX00);
            Api.SetAdjacency(Enums.TileAdjacency.A0001, Enums.TileGeometryAndRotationAndAdjacency.TB_R1_AXX01);
            Api.SetAdjacency(Enums.TileAdjacency.A0010, Enums.TileGeometryAndRotationAndAdjacency.TB_R1_AXX10);
            Api.SetAdjacency(Enums.TileAdjacency.A0011, Enums.TileGeometryAndRotationAndAdjacency.TB_R1_AXX11);

            Api.End();


            Api.Create(Enums.TileGeometryAndRotation.TB_R2,
            (int)GeometryLineSide.North | (int)GeometryLineSide.East);
            Api.AddLine(TileLineSegment.L_C0_C1, GeometryLineSide.North);
            Api.AddLine(TileLineSegment.L_C1_C2, GeometryLineSide.East);
            Api.AddLine(TileLineSegment.L_C2_C0, GeometryLineSide.South);

            Api.SetAdjacency(Enums.TileAdjacency.A0000, Enums.TileGeometryAndRotationAndAdjacency.TB_R2_A00XX);
            Api.SetAdjacency(Enums.TileAdjacency.A0100, Enums.TileGeometryAndRotationAndAdjacency.TB_R2_A01XX);
            Api.SetAdjacency(Enums.TileAdjacency.A1000, Enums.TileGeometryAndRotationAndAdjacency.TB_R2_A10XX);
            Api.SetAdjacency(Enums.TileAdjacency.A1100, Enums.TileGeometryAndRotationAndAdjacency.TB_R2_A11XX);
            Api.End();


            Api.Create(Enums.TileGeometryAndRotation.TB_R3,
            (int)GeometryLineSide.North | (int)GeometryLineSide.West);
            Api.AddLine(TileLineSegment.L_C0_C1, GeometryLineSide.North);
            Api.AddLine(TileLineSegment.L_C1_C3, GeometryLineSide.East);
            Api.AddLine(TileLineSegment.L_C3_C0, GeometryLineSide.West);

            Api.SetAdjacency(Enums.TileAdjacency.A0000, Enums.TileGeometryAndRotationAndAdjacency.TB_R3_A0XX0);
            Api.SetAdjacency(Enums.TileAdjacency.A0001, Enums.TileGeometryAndRotationAndAdjacency.TB_R3_A0XX0);
            Api.SetAdjacency(Enums.TileAdjacency.A1000, Enums.TileGeometryAndRotationAndAdjacency.TB_R3_A0XX0);
            Api.SetAdjacency(Enums.TileAdjacency.A1001, Enums.TileGeometryAndRotationAndAdjacency.TB_R3_A0XX0);
            Api.End();



            Api.Create(Enums.TileGeometryAndRotation.QP_R0, 0);
            Api.AddLine(TileLineSegment.L_C0_C1, GeometryLineSide.North);

            Api.SetAdjacency(Enums.TileAdjacency.A0000, Enums.TileGeometryAndRotationAndAdjacency.QP_R0_XXXX);
            Api.End();

            Api.Create(Enums.TileGeometryAndRotation.QP_R1, 0);
            Api.AddLine(TileLineSegment.L_C1_C2, GeometryLineSide.North);

            Api.SetAdjacency(Enums.TileAdjacency.A0000, Enums.TileGeometryAndRotationAndAdjacency.QP_R1_XXXX);
            Api.End();

            Api.Create(Enums.TileGeometryAndRotation.QP_R2, 0);
            Api.AddLine(TileLineSegment.L_C2_C3, GeometryLineSide.North);

            Api.SetAdjacency(Enums.TileAdjacency.A0000, Enums.TileGeometryAndRotationAndAdjacency.QP_R2_XXXX);
            Api.End();

            Api.Create(Enums.TileGeometryAndRotation.QP_R3, 0);
            Api.AddLine(TileLineSegment.L_C3_C0, GeometryLineSide.North);

            Api.SetAdjacency(Enums.TileAdjacency.A0000, Enums.TileGeometryAndRotationAndAdjacency.QP_R3_XXXX);
            Api.End();

        }


    }

}
