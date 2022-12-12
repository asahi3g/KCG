
namespace TGen
{
    public enum BlockType
    {
        Error = 0,
        // Square block
        SB,
        // Half Block
        HB,
        // Triangle Block
        TB,
        // Lower part of L triangle block
        L1,
        // Upper part of L triangle block
        L2
    }


    public enum BlockTypeAndRotation
    {
        Error = 0,

        // Empty Block
        EmptyBlock,

        // SquareBlock
        SB_R0,

        // HalfBlock
        HB_R0,
        HB_R1,
        HB_R2,
        HB_R3,

        //TriangleBlock
        TB_R0,
        TB_R1,
        TB_R4,
        TB_R5,
        TB_R2,
        TB_R3,
        TB_R6,
        TB_R7,

        //LBlock
        LB_R0,
        LB_R1,
        LB_R2,
        LB_R3,
        LB_R4,
        LB_R5,
        LB_R6,
        LB_R7,

        //HalfTriangleBlock
        HTB_R0,
        HTB_R1,
        HTB_R2,
        HTB_R3,
        HTB_R4,
        HTB_R5,
        HTB_R6,
        HTB_R7,

        //QuarterPlatform
        QP_R0,
        QP_R1,
        QP_R2,
        QP_R3,

        //HalfPlatform
        HP_R0,
        HP_R1,
        HP_R2,
        HP_R3,

        //FullPlatform
        FP_R0,
        FP_R1,
        FP_R2,
        FP_R3,

        LIGHT
    }

}