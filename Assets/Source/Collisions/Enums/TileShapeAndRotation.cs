namespace Collisions
{
    public enum TileShapeAndRotation
    {
        Error = 0,
        
        // Empty Block
        EmptyBlock,
        
        // FullBlock
        FullBlock,
        FullPlatform,

        // HalfBlock
        HB_R1,
        HB_R2,
        HB_R3,
        HB_R4,
        
        //TriangleBlock
        TB_R1,
        TB_R2,
        TB_R3,
        TB_R4,

        //L1 - part 1 of L block
        L1_R1,
        L1_R2,
        L1_R3,
        L1_R4,
        L1_R5,
        L1_R6,
        L1_R7,
        L1_R8,
        
        //L2 - part 2 of L block
        L2_R1,
        L2_R2,
        L2_R3,
        L2_R4,
        L2_R5,
        L2_R6,
        L2_R7,
        L2_R8,
        
        //QP - Quarter Platform
        QP_R1,
        QP_R2,
        QP_R3,
        QP_R4,

        //HP - Half Platform
        HP_R1,
        HP_R2,
        HP_R3,
        HP_R4,
    }
}
