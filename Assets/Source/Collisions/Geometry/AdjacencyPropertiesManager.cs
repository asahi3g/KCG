using System;
using System.Collections.Generic;
using KMath;


namespace Collisions
{
    public class AdjacencyPropertiesManager
    {
        // Start is called before the first frame update

        private int CurrentIndex; // current index into the Offset Array

        private AdjacencyProperties[] PropertiesArray; // an array of offsets into the LineSegment array 

        private int LineOffset;
        private int LineCount;
        private TileLineSegment[] SegmentArray;


        public AdjacencyPropertiesManager()
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
            AdjacencyPropertiesManager Api = GameState.AdjacencyPropertiesManager;

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.SB_R0_A0000);
            Api.AddLine(TileLineSegment.L_C0_C1); // north
            Api.AddLine(TileLineSegment.L_C1_C2); // east 
            Api.AddLine(TileLineSegment.L_C2_C3); // south 
            Api.AddLine(TileLineSegment.L_C3_C0); // west 
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.SB_R0_A0001);
             Api.AddLine(TileLineSegment.L_C0_C1); // north
            Api.AddLine(TileLineSegment.L_C1_C2); // east 
            Api.AddLine(TileLineSegment.L_C2_C3); // south 
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.SB_R0_A0010);
            Api.AddLine(TileLineSegment.L_C0_C1); // north
            Api.AddLine(TileLineSegment.L_C1_C2); // east 
            Api.AddLine(TileLineSegment.L_C3_C0); // west 
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.SB_R0_A0011);
             Api.AddLine(TileLineSegment.L_C0_C1); // north
            Api.AddLine(TileLineSegment.L_C1_C2); // east 
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.SB_R0_A0100);
             Api.AddLine(TileLineSegment.L_C0_C1); // north
            Api.AddLine(TileLineSegment.L_C2_C3); // south 
            Api.AddLine(TileLineSegment.L_C3_C0); // west 
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.SB_R0_A0101);
            Api.AddLine(TileLineSegment.L_C0_C1); // north 
            Api.AddLine(TileLineSegment.L_C2_C3); // south 
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.SB_R0_A0110);
             Api.AddLine(TileLineSegment.L_C0_C1); // north 
            Api.AddLine(TileLineSegment.L_C3_C0); // west 
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.SB_R0_A0111);
             Api.AddLine(TileLineSegment.L_C0_C1); // north
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.SB_R0_A1000);
            Api.AddLine(TileLineSegment.L_C1_C2); // east 
            Api.AddLine(TileLineSegment.L_C2_C3); // south 
            Api.AddLine(TileLineSegment.L_C3_C0); // west 
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.SB_R0_A1001);
            Api.AddLine(TileLineSegment.L_C1_C2); // east 
            Api.AddLine(TileLineSegment.L_C2_C3); // south 
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.SB_R0_A1010);
            Api.AddLine(TileLineSegment.L_C1_C2); // east 
            Api.AddLine(TileLineSegment.L_C3_C0); // west 
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.SB_R0_A1011);
            Api.AddLine(TileLineSegment.L_C1_C2); // east 
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.SB_R0_A1100);
            Api.AddLine(TileLineSegment.L_C2_C3); // south 
            Api.AddLine(TileLineSegment.L_C3_C0); // west 
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.SB_R0_A1101);
            Api.AddLine(TileLineSegment.L_C2_C3); // south 
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.SB_R0_A1110);
            Api.AddLine(TileLineSegment.L_C3_C0); // west 
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

                    

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L1_R0_A0XX0);
            Api.AddLine(TileLineSegment.L_C0_M0); // north
            Api.AddLine(TileLineSegment.L_M0_C3); // east / south
            Api.AddLine(TileLineSegment.L_C3_C0); // west
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L1_R0_A0XX1);
            Api.AddLine(TileLineSegment.L_C0_M0); // north
            Api.AddLine(TileLineSegment.L_M0_C3); // east / south
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L1_R0_A1XX0);

            Api.AddLine(TileLineSegment.L_M0_C3); // east / south
            Api.AddLine(TileLineSegment.L_C3_C0); // west
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L1_R0_A1XX1);
            Api.AddLine(TileLineSegment.L_M0_C3); // east / south
            Api.End();











            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L1_R1_A00XX);
            Api.AddLine(TileLineSegment.L_C0_C1); // north
            Api.AddLine(TileLineSegment.L_C1_M1); // east
            Api.AddLine(TileLineSegment.L_M1_C0); // west / south
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L1_R1_A01XX);
            Api.AddLine(TileLineSegment.L_C0_C1); // north
            Api.AddLine(TileLineSegment.L_M1_C0); // west / south
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L1_R1_A10XX);
            Api.AddLine(TileLineSegment.L_C1_M1); // east
            Api.AddLine(TileLineSegment.L_M1_C0); // west / south
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L1_R1_A11XX);
            Api.AddLine(TileLineSegment.L_M1_C0); // west / south
            Api.End();


        




            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L1_R2_AX00X);
            Api.AddLine(TileLineSegment.L_M2_C1); // north / west
            Api.AddLine(TileLineSegment.L_C1_C2); // east
            Api.AddLine(TileLineSegment.L_C2_M2); // south
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L1_R2_AX01X);
            Api.AddLine(TileLineSegment.L_M2_C1); // north / west
            Api.AddLine(TileLineSegment.L_C1_C2); // east
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L1_R2_AX10X);
            Api.AddLine(TileLineSegment.L_M2_C1); // north / west
            Api.AddLine(TileLineSegment.L_C2_M2); // south
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L1_R2_AX11X);
            Api.AddLine(TileLineSegment.L_M2_C1); // north / west
            Api.End();

        













            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L1_R3_AXX00);
            Api.AddLine(TileLineSegment.L_C0_M1); // north / east
            Api.AddLine(TileLineSegment.L_M1_M3); // south
            Api.AddLine(TileLineSegment.L_M3_C0); // west
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L1_R3_AXX01);
            Api.AddLine(TileLineSegment.L_C0_M1); // north / east
            Api.AddLine(TileLineSegment.L_M1_M3); // south
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L1_R3_AXX10);
            Api.AddLine(TileLineSegment.L_C0_M1); // north / east
            Api.AddLine(TileLineSegment.L_M3_C0); // west
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L1_R3_AXX11);
            Api.AddLine(TileLineSegment.L_C0_M1); // north / east
            Api.End();

           






            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L1_R4_AXX00);
            Api.AddLine(TileLineSegment.L_C0_M2); // north / east
            Api.AddLine(TileLineSegment.L_M2_C3); // south
            Api.AddLine(TileLineSegment.L_C3_C0); // west
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L1_R4_AXX01);
            Api.AddLine(TileLineSegment.L_C0_M2); // north / east
            Api.AddLine(TileLineSegment.L_M2_C3); // south
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L1_R4_AXX10);
            Api.AddLine(TileLineSegment.L_C0_M2); // north / east
            Api.AddLine(TileLineSegment.L_C3_C0); // west
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L1_R4_AXX11);
            Api.AddLine(TileLineSegment.L_C0_M2); // north / east
            Api.End();

    








            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L1_R5_A0XX0);
            Api.AddLine(TileLineSegment.L_C0_C1); // north
            Api.AddLine(TileLineSegment.L_C1_M3); // east / south
            Api.AddLine(TileLineSegment.L_M3_C0); // west
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L1_R5_A0XX1);
            Api.AddLine(TileLineSegment.L_C0_C1); // north
            Api.AddLine(TileLineSegment.L_C1_M3); // east / south
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L1_R5_A1XX0);
            Api.AddLine(TileLineSegment.L_C1_M3); // east / south
            Api.AddLine(TileLineSegment.L_M3_C0); // west
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L1_R5_A1XX1);
            Api.AddLine(TileLineSegment.L_C1_M3); // east / south
            Api.End();

        





            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L1_R6_A00XX);
            Api.AddLine(TileLineSegment.L_M0_C1); // north
            Api.AddLine(TileLineSegment.L_C1_C2); // east
            Api.AddLine(TileLineSegment.L_C2_M0); // south / west
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L1_R6_A01XX);
            Api.AddLine(TileLineSegment.L_M0_C1); // north
            Api.AddLine(TileLineSegment.L_C2_M0); // south / west
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L1_R6_A10XX);
            Api.AddLine(TileLineSegment.L_C1_C2); // east
            Api.AddLine(TileLineSegment.L_C2_M0); // south / west
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L1_R6_A11XX);
            Api.AddLine(TileLineSegment.L_C2_M0); // south / west
            Api.End();

         


            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L1_R7_AX00X);
            Api.AddLine(TileLineSegment.L_M1_C2); // east
            Api.AddLine(TileLineSegment.L_C2_C3); // south
            Api.AddLine(TileLineSegment.L_C3_M1); // west / north
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L1_R7_AX01X);
            Api.AddLine(TileLineSegment.L_M1_C2); // east
            Api.AddLine(TileLineSegment.L_C3_M1); // west / north
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L1_R7_AX10X);
            Api.AddLine(TileLineSegment.L_C2_C3); // south
            Api.AddLine(TileLineSegment.L_C3_M1); // west / north
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L1_R7_AX11X);
            Api.AddLine(TileLineSegment.L_C3_M1); // west / north
            Api.End();

         







            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R0_A0X00);
            Api.AddLine(TileLineSegment.L_C0_C1); // north
            Api.AddLine(TileLineSegment.L_C1_M2); // east
            Api.AddLine(TileLineSegment.L_M2_C3); // south
            Api.AddLine(TileLineSegment.L_C3_C0); // west
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R0_A0X01);
            Api.AddLine(TileLineSegment.L_C0_C1); // north
            Api.AddLine(TileLineSegment.L_C1_M2); // east
            Api.AddLine(TileLineSegment.L_M2_C3); // south
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R0_A0X10);
            Api.AddLine(TileLineSegment.L_C0_C1); // north
            Api.AddLine(TileLineSegment.L_C1_M2); // east
            Api.AddLine(TileLineSegment.L_C3_C0); // west
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R0_A0X11);
            Api.AddLine(TileLineSegment.L_C0_C1); // north
            Api.AddLine(TileLineSegment.L_C1_M2); // east
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R0_A1X00);
            Api.AddLine(TileLineSegment.L_C1_M2); // east
            Api.AddLine(TileLineSegment.L_M2_C3); // south
            Api.AddLine(TileLineSegment.L_C3_C0); // west
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R0_A1X01);
            Api.AddLine(TileLineSegment.L_C1_M2); // east
            Api.AddLine(TileLineSegment.L_M2_C3); // south
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R0_A1X10);
            Api.AddLine(TileLineSegment.L_C1_M2); // east
            Api.AddLine(TileLineSegment.L_C3_C0); // west
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R0_A1X11);
            Api.AddLine(TileLineSegment.L_C1_M2); // east
            Api.End();














            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R1_AX000);
            Api.AddLine(TileLineSegment.L_C0_M1); // north 
            Api.AddLine(TileLineSegment.L_M1_C2); // east
            Api.AddLine(TileLineSegment.L_C2_C3); // south
            Api.AddLine(TileLineSegment.L_C3_C0); // west
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R1_AX001);
            Api.AddLine(TileLineSegment.L_C0_M1); // north 
            Api.AddLine(TileLineSegment.L_M1_C2); // east
            Api.AddLine(TileLineSegment.L_C2_C3); // south
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R1_AX010);
            Api.AddLine(TileLineSegment.L_C0_M1); // north 
            Api.AddLine(TileLineSegment.L_M1_C2); // east
            Api.AddLine(TileLineSegment.L_C3_C0); // west
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R1_AX011);
            Api.AddLine(TileLineSegment.L_C0_M1); // north 
            Api.AddLine(TileLineSegment.L_M1_C2); // east
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R1_AX100);
            Api.AddLine(TileLineSegment.L_C0_M1); // north 
            Api.AddLine(TileLineSegment.L_C2_C3); // south
            Api.AddLine(TileLineSegment.L_C3_C0); // west
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R1_AX101);
            Api.AddLine(TileLineSegment.L_C0_M1); // north 
            Api.AddLine(TileLineSegment.L_C2_C3); // south
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R1_AX110);
            Api.AddLine(TileLineSegment.L_C0_M1); // north 
            Api.AddLine(TileLineSegment.L_C3_C0); // west
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R1_AX111);
            Api.AddLine(TileLineSegment.L_C0_M1); // north 
            Api.End();










            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R2_A000X);
            Api.AddLine(TileLineSegment.L_M0_C1); // north
            Api.AddLine(TileLineSegment.L_C1_C2); // east
            Api.AddLine(TileLineSegment.L_C2_C3); // south
            Api.AddLine(TileLineSegment.L_C3_M0); // west
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R2_A001X);
            Api.AddLine(TileLineSegment.L_M0_C1); // north
            Api.AddLine(TileLineSegment.L_C1_C2); // east
            Api.AddLine(TileLineSegment.L_C3_M0); // west
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R2_A010X);
            Api.AddLine(TileLineSegment.L_M0_C1); // north
            Api.AddLine(TileLineSegment.L_C2_C3); // south
            Api.AddLine(TileLineSegment.L_C3_M0); // west
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R2_A011X);
            Api.AddLine(TileLineSegment.L_M0_C1); // north
            Api.AddLine(TileLineSegment.L_C3_M0); // west
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R2_A100X);
            Api.AddLine(TileLineSegment.L_C1_C2); // east
            Api.AddLine(TileLineSegment.L_C2_C3); // south
            Api.AddLine(TileLineSegment.L_C3_M0); // west
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R2_A101X);
            Api.AddLine(TileLineSegment.L_C1_C2); // east
            Api.AddLine(TileLineSegment.L_C3_M0); // west
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R2_A110X);
            Api.AddLine(TileLineSegment.L_C2_C3); // south
            Api.AddLine(TileLineSegment.L_C3_M0); // west
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R2_A111X);
            Api.AddLine(TileLineSegment.L_C3_M0); // west
            Api.End();





















            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R3_A00X0);
            Api.AddLine(TileLineSegment.L_C0_C1); // north
            Api.AddLine(TileLineSegment.L_C1_C2); // east
            Api.AddLine(TileLineSegment.L_C2_M3); // south
            Api.AddLine(TileLineSegment.L_M3_C0); // west
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R3_A00X1);
            Api.AddLine(TileLineSegment.L_C0_C1); // north
            Api.AddLine(TileLineSegment.L_C1_C2); // east
            Api.AddLine(TileLineSegment.L_C2_M3); // south
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R3_A01X0);
            Api.AddLine(TileLineSegment.L_C0_C1); // north
            Api.AddLine(TileLineSegment.L_C2_M3); // south
            Api.AddLine(TileLineSegment.L_M3_C0); // west
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R3_A01X1);
            Api.AddLine(TileLineSegment.L_C0_C1); // north
            Api.AddLine(TileLineSegment.L_C2_M3); // south
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R3_A10X0);
            Api.AddLine(TileLineSegment.L_C1_C2); // east
            Api.AddLine(TileLineSegment.L_C2_M3); // south
            Api.AddLine(TileLineSegment.L_M3_C0); // west
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R3_A10X1);
            Api.AddLine(TileLineSegment.L_C1_C2); // east
            Api.AddLine(TileLineSegment.L_C2_M3); // south
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R3_A11X0);
            Api.AddLine(TileLineSegment.L_C2_M3); // south
            Api.AddLine(TileLineSegment.L_M3_C0); // west
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R3_A11X1);
            Api.AddLine(TileLineSegment.L_C2_M3); // south
            Api.End();





























            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R4_A000X);
            Api.AddLine(TileLineSegment.L_C0_C1); // north
            Api.AddLine(TileLineSegment.L_C1_C2); // east
            Api.AddLine(TileLineSegment.L_C2_M2); // south
            Api.AddLine(TileLineSegment.L_M2_C0); // west
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R4_A001X);
            Api.AddLine(TileLineSegment.L_C0_C1); // north
            Api.AddLine(TileLineSegment.L_C1_C2); // east
            Api.AddLine(TileLineSegment.L_M2_C0); // west
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R4_A010X);
            Api.AddLine(TileLineSegment.L_C0_C1); // north
            Api.AddLine(TileLineSegment.L_C2_M2); // south
            Api.AddLine(TileLineSegment.L_M2_C0); // west
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R4_A011X);
            Api.AddLine(TileLineSegment.L_C0_C1); // north
            Api.AddLine(TileLineSegment.L_M2_C0); // west
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R4_A100X);
            Api.AddLine(TileLineSegment.L_C1_C2); // east
            Api.AddLine(TileLineSegment.L_C2_M2); // south
            Api.AddLine(TileLineSegment.L_M2_C0); // west
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R4_A101X);
            Api.AddLine(TileLineSegment.L_C1_C2); // east
            Api.AddLine(TileLineSegment.L_M2_C0); // west
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R4_A110X);
            Api.AddLine(TileLineSegment.L_C2_M2); // south
            Api.AddLine(TileLineSegment.L_M2_C0); // west
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R4_A111X);
            Api.AddLine(TileLineSegment.L_M2_C0); // west
            Api.End();

















            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R5_A00X0);
            Api.AddLine(TileLineSegment.L_C0_C1); // north
            Api.AddLine(TileLineSegment.L_C1_M1); // east
            Api.AddLine(TileLineSegment.L_M1_C3); // south
            Api.AddLine(TileLineSegment.L_C3_C0); // west
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R5_A00X1);
            Api.AddLine(TileLineSegment.L_C0_C1); // north
            Api.AddLine(TileLineSegment.L_C1_M1); // east
            Api.AddLine(TileLineSegment.L_M1_C3); // south
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R5_A01X0);
            Api.AddLine(TileLineSegment.L_C0_C1); // north
            Api.AddLine(TileLineSegment.L_M1_C3); // south
            Api.AddLine(TileLineSegment.L_C3_C0); // west
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R5_A01X1);
            Api.AddLine(TileLineSegment.L_C0_C1); // north
            Api.AddLine(TileLineSegment.L_M1_C3); // south
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R5_A10X0);
            Api.AddLine(TileLineSegment.L_C1_M1); // east
            Api.AddLine(TileLineSegment.L_M1_C3); // south
            Api.AddLine(TileLineSegment.L_C3_C0); // west
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R5_A10X1);
            Api.AddLine(TileLineSegment.L_C1_M1); // east
            Api.AddLine(TileLineSegment.L_M1_C3); // south
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R5_A11X0);
            Api.AddLine(TileLineSegment.L_M1_C3); // south
            Api.AddLine(TileLineSegment.L_C3_C0); // west
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R5_A11X1);
            Api.AddLine(TileLineSegment.L_M1_C3); // south
            Api.End();



















            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R6_A0X00);
            Api.AddLine(TileLineSegment.L_C0_M0); // north
            Api.AddLine(TileLineSegment.L_M0_C2); // east
            Api.AddLine(TileLineSegment.L_C2_C3); // south
            Api.AddLine(TileLineSegment.L_C3_C0); // west
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R6_A0X01);
            Api.AddLine(TileLineSegment.L_C0_M0); // north
            Api.AddLine(TileLineSegment.L_M0_C2); // east
            Api.AddLine(TileLineSegment.L_C2_C3); // south
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R6_A0X10);
            Api.AddLine(TileLineSegment.L_C0_M0); // north
            Api.AddLine(TileLineSegment.L_M0_C2); // east
            Api.AddLine(TileLineSegment.L_C3_C0); // west
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R6_A0X11);
            Api.AddLine(TileLineSegment.L_C0_M0); // north
            Api.AddLine(TileLineSegment.L_M0_C2); // east
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R6_A1X00);
            Api.AddLine(TileLineSegment.L_M0_C2); // east
            Api.AddLine(TileLineSegment.L_C2_C3); // south
            Api.AddLine(TileLineSegment.L_C3_C0); // west
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R6_A1X01);
            Api.AddLine(TileLineSegment.L_M0_C2); // east
            Api.AddLine(TileLineSegment.L_C2_C3); // south
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R6_A1X10);
            Api.AddLine(TileLineSegment.L_M0_C2); // east
            Api.AddLine(TileLineSegment.L_C3_C0); // west
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R6_A1X11);
            Api.AddLine(TileLineSegment.L_M0_C2); // east
            Api.End();



















            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R7_AX000);
            Api.AddLine(TileLineSegment.L_M3_C1); // north
            Api.AddLine(TileLineSegment.L_C1_C2); // east
            Api.AddLine(TileLineSegment.L_C2_C3); // south
            Api.AddLine(TileLineSegment.L_C3_M3); // west
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R7_AX001);
            Api.AddLine(TileLineSegment.L_M3_C1); // north
            Api.AddLine(TileLineSegment.L_C1_C2); // east
            Api.AddLine(TileLineSegment.L_C2_C3); // south
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R7_AX010);
            Api.AddLine(TileLineSegment.L_M3_C1); // north
            Api.AddLine(TileLineSegment.L_C1_C2); // east
            Api.AddLine(TileLineSegment.L_C3_M3); // west
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R7_AX011);
            Api.AddLine(TileLineSegment.L_M3_C1); // north
            Api.AddLine(TileLineSegment.L_C1_C2); // east
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R7_AX100);
            Api.AddLine(TileLineSegment.L_M3_C1); // north
            Api.AddLine(TileLineSegment.L_C2_C3); // south
            Api.AddLine(TileLineSegment.L_C3_M3); // west
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R7_AX101);
            Api.AddLine(TileLineSegment.L_M3_C1); // north
            Api.AddLine(TileLineSegment.L_C2_C3); // south
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R7_AX110);
            Api.AddLine(TileLineSegment.L_M3_C1); // north
            Api.AddLine(TileLineSegment.L_C3_M3); // west
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.L2_R7_AX111);
            Api.AddLine(TileLineSegment.L_M3_C1); // north
            Api.End();





            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.TB_R0_AX00X);
            Api.AddLine(TileLineSegment.L_C3_C1); // north / west 
            Api.AddLine(TileLineSegment.L_C1_C2); // east 
            Api.AddLine(TileLineSegment.L_C2_C3); // south 
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.TB_R0_AX01X);
            Api.AddLine(TileLineSegment.L_C3_C1); // north / west 
            Api.AddLine(TileLineSegment.L_C1_C2); // east 
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.TB_R0_AX10X);
            Api.AddLine(TileLineSegment.L_C3_C1); // north / west 
            Api.AddLine(TileLineSegment.L_C2_C3); // south 
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.TB_R0_AX11X);
            Api.AddLine(TileLineSegment.L_C3_C1); // north / west 
            Api.End();
         





            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.TB_R1_AXX00);
            Api.AddLine(TileLineSegment.L_C0_C2); // north / east
            Api.AddLine(TileLineSegment.L_C2_C3); // south 
            Api.AddLine(TileLineSegment.L_C3_C0); // west 
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.TB_R1_AXX01);
            Api.AddLine(TileLineSegment.L_C0_C2); // north / east
            Api.AddLine(TileLineSegment.L_C2_C3); // south 
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.TB_R1_AXX10);
            Api.AddLine(TileLineSegment.L_C0_C2); // north / east
            Api.AddLine(TileLineSegment.L_C3_C0); // west  
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.TB_R1_AXX11);
            Api.AddLine(TileLineSegment.L_C0_C2); // north / east
            Api.End();

           





            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.TB_R2_A00XX);
            Api.AddLine(TileLineSegment.L_C0_C1); // north
            Api.AddLine(TileLineSegment.L_C1_C2); // east 
            Api.AddLine(TileLineSegment.L_C2_C0); // west  / south 
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.TB_R2_A01XX);
            Api.AddLine(TileLineSegment.L_C0_C1); // north
            Api.AddLine(TileLineSegment.L_C2_C0); // west  / south 
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.TB_R2_A10XX);
            Api.AddLine(TileLineSegment.L_C1_C2); // east 
            Api.AddLine(TileLineSegment.L_C2_C0); // west  / south 
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.TB_R2_A11XX); 
            Api.AddLine(TileLineSegment.L_C2_C0); // west  / south 
            Api.End();

        



            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.TB_R3_A0XX0);
            Api.AddLine(TileLineSegment.L_C0_C1); // north
            Api.AddLine(TileLineSegment.L_C1_C3); // east  / south 
            Api.AddLine(TileLineSegment.L_C3_C0); // west 
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.TB_R3_A0XX1);
            Api.AddLine(TileLineSegment.L_C0_C1); // north
            Api.AddLine(TileLineSegment.L_C1_C3); // east  / south 
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.TB_R3_A1XX0);
            Api.AddLine(TileLineSegment.L_C1_C3); // east  / south 
            Api.AddLine(TileLineSegment.L_C3_C0); // west 
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.TB_R3_A1XX1);
            Api.AddLine(TileLineSegment.L_C1_C3); // east  / south 
            Api.End();



            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.QP_R0_XXXX);
            Api.AddLine(TileLineSegment.L_C0_C1);
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.QP_R1_XXXX);
            Api.AddLine(TileLineSegment.L_C1_C2);  
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.QP_R2_XXXX);
            Api.AddLine(TileLineSegment.L_C2_C3);
            Api.End();

            Api.Create(Enums.TileGeometryAndRotationAndAdjacency.QP_R3_XXXX);
            Api.AddLine(TileLineSegment.L_C3_C0);
            Api.End();

        
        }


    }

}
