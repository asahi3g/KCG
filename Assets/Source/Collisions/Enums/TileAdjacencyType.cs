namespace Collisions
{
	// R - Rotation
	// A - Adjacency
    public enum TileAdjacencyType
    {
	    Error = 0,
	    EmptyBlock,
        // SB - Square block
        // R0
        SB_R0_A0000,
        SB_R0_A0001,
        SB_R0_A0010,
        SB_R0_A0011,
        SB_R0_A0100,
        SB_R0_A0101,
        SB_R0_A0110,
        SB_R0_A0111,
        SB_R0_A1000,
        SB_R0_A1001,
        SB_R0_A1010,
        SB_R0_A1011,
        SB_R0_A1100,
        SB_R0_A1101,
        SB_R0_A1110,
        SB_R0_A1111,
        // HB - half block
		// R0
        HB_R0_A000,
        HB_R0_A001,
        HB_R0_A010,
        HB_R0_A011,
        HB_R0_A100,
        HB_R0_A101,
        HB_R0_A110,
        HB_R0_A111,
        
        // R1
        HB_R1_A000,
        HB_R1_A001,
        HB_R1_A010,
        HB_R1_A011,
        HB_R1_A100,
        HB_R1_A101,
        HB_R1_A110,
        HB_R1_A111,
		
		// R2
        HB_R2_A000,
        HB_R2_A001,
        HB_R2_A010,
        HB_R2_A011,
        HB_R2_A100,
        HB_R2_A101,
        HB_R2_A110,
        HB_R2_A111,
		
		// R3
        HB_R3_A000,
        HB_R3_A001,
        HB_R3_A010,
        HB_R3_A011,
        HB_R3_A100,
        HB_R3_A101,
        HB_R3_A110,
        HB_R3_A111,
		// TB - Triangle Block
		// R0
        TB_R0_A00,
        TB_R0_A01,
        TB_R0_A10,
        TB_R0_A11,
        
        // R1
        TB_R1_A00,
        TB_R1_A01,
        TB_R1_A10,
        TB_R1_A11,
		
		// R2
        TB_R2_A00,
        TB_R2_A01,
        TB_R2_A10,
        TB_R2_A11,
		
		// R3
        TB_R3_A00,
        TB_R3_A01,
        TB_R3_A10,
        TB_R3_A11,

        // L1 - Part 1 of L block
		// R0
		L1_R0_A00,
		L1_R0_A01,
		L1_R0_A10,
		L1_R0_A11,
        
        // R1
        L1_R1_A00,
        L1_R1_A01,
        L1_R1_A10,
        L1_R1_A11,
		
		// R2
		L1_R2_A00,
		L1_R2_A01,
		L1_R2_A10,
		L1_R2_A11,
		
		// R3
		L1_R3_A00,
		L1_R3_A01,
		L1_R3_A10,
		L1_R3_A11,
		
		// R4
		L1_R4_A00,
		L1_R4_A01,
		L1_R4_A10,
		L1_R4_A11,
		
		// R5
		L1_R5_A00,
		L1_R5_A01,
		L1_R5_A10,
		L1_R5_A11,
		
		// R6
		L1_R6_A00,
		L1_R6_A01,
		L1_R6_A10,
		L1_R6_A11,
		
		// R7
		L1_R7_A00,
		L1_R7_A01,
		L1_R7_A10,
		L1_R7_A11,
		
		// L2 - Part 2 of L block
		// R0
		L2_R0_A000,
		L2_R0_A001,
		L2_R0_A010,
		L2_R0_A011,
		L2_R0_A100,
		L2_R0_A101,
		L2_R0_A110,
		L2_R0_A111,
        
        // R1
        L2_R1_A000,
        L2_R1_A001,
        L2_R1_A010,
        L2_R1_A011,
        L2_R1_A100,
        L2_R1_A101,
        L2_R1_A110,
        L2_R1_A111,
		
		// R2
		L2_R2_A000,
		L2_R2_A001,
		L2_R2_A010,
		L2_R2_A011,
		L2_R2_A100,
		L2_R2_A101,
		L2_R2_A110,
		L2_R2_A111,
		
		// R3
		L2_R3_A000,
		L2_R3_A001,
		L2_R3_A010,
		L2_R3_A011,
		L2_R3_A100,
		L2_R3_A101,
		L2_R3_A110,
		L2_R3_A111,
		
		// R4
		L2_R4_A000,
		L2_R4_A001,
		L2_R4_A010,
		L2_R4_A011,
		L2_R4_A100,
		L2_R4_A101,
		L2_R4_A110,
		L2_R4_A111,
        
		// R5
		L2_R5_A000,
		L2_R5_A001,
		L2_R5_A010,
		L2_R5_A011,
		L2_R5_A100,
		L2_R5_A101,
		L2_R5_A110,
		L2_R5_A111,
		
		// R6
		L2_R6_A000,
		L2_R6_A001,
		L2_R6_A010,
		L2_R6_A011,
		L2_R6_A100,
		L2_R6_A101,
		L2_R6_A110,
		L2_R6_A111,
		
		// R7
		L2_R7_A000,
		L2_R7_A001,
		L2_R7_A010,
		L2_R7_A011,
		L2_R7_A100,
		L2_R7_A101,
		L2_R7_A110,
		L2_R7_A111,
		
		//QP - Quarter Platform
		// R0
		QP_R0_A0,
		QP_R0_A1,

		// R1
		QP_R1_A0,
		QP_R1_A1,

		// R2
		QP_R2_A0,
		QP_R2_A1,

		// R3
		QP_R3_A0,
		QP_R3_A1,

		//HP - Half Platform
		// R0
		HP_R0_A0,
		HP_R0_A1,

		// R1
		HP_R1_A0,
		HP_R1_A1,

		// R2
		HP_R2_A0,
		HP_R2_A1,
		
		// R3
		HP_R3_A0,
		HP_R3_A1,
		
		// FP - Full Platform
		// R0
		FP_R0_A0,
		FP_R0_A1,
		
		// R1
		FP_R1_A0,
		FP_R1_A1,

		// R2
		FP_R2_A0,
		FP_R2_A1,
		
		// R3
		FP_R3_A0,
		FP_R3_A1,
    }
}


