namespace Enums.Tile
{
	// R - Rotation
	// A - Adjacency
    public enum TileAdjacencyType
    {
	    Error = 0,
	    EmptyBlock,
        // FB - Full block
        // R0
        FB_R0_A0000,
        FB_R0_A0001,
        FB_R0_A0010,
        FB_R0_A0011,
        FB_R0_A0100,
        FB_R0_A0101,
        FB_R0_A0110,
        FB_R0_A0111,
        FB_R0_A1000,
        FB_R0_A1001,
        FB_R0_A1010,
        FB_R0_A1011,
        FB_R0_A1100,
        FB_R0_A1101,
        FB_R0_A1110,
        FB_R0_A1111,
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
		
		// LBB - L Block Bottom
		// R0
		LBB_R0_A00,
		LBB_R0_A01,
		LBB_R0_A10,
		LBB_R0_A11,
        
        // R1
        LBB_R1_A00,
        LBB_R1_A01,
        LBB_R1_A10,
        LBB_R1_A11,
		
		// R2
		LBB_R2_A00,
		LBB_R2_A01,
		LBB_R2_A10,
		LBB_R2_A11,
		
		// R3
		LBB_R3_A00,
		LBB_R3_A01,
		LBB_R3_A10,
		LBB_R3_A11,
		
		// LBT - L Block Top
		// R0
		LBT_R0_A00,
		LBT_R0_A01,
		LBT_R0_A10,
		LBT_R0_A11,
        
        // R1
        LBT_R1_A00,
        LBT_R1_A01,
        LBT_R1_A10,
        LBT_R1_A11,
		
		// R2
		LBT_R2_A00,
		LBT_R2_A01,
		LBT_R2_A10,
		LBT_R2_A11,
		
		// R3
		LBT_R3_A00,
		LBT_R3_A01,
		LBT_R3_A10,
		LBT_R3_A11,
    }
}

