using System.Runtime.CompilerServices;
using Collisions;
using Enums.Tile;
using KMath;

namespace PlanetTileMap
{
    public static class TileGeometry
    {
        // Takes in TilePoint C0, C1 etc index
        // Returns VeC1f point
        private static readonly Vec2f[] PointsArray;
        
        // Takes in TileLineSegment L_C0_M1, etc index
        // Returns the start and finish point of line
        private static readonly Line2D[] LinesArray;
        
        // Takes in TileShape index
        // Returns number of lines for this shape
        private static readonly int[] ShapeLinesCount;
        
        static TileGeometry()
        {
            PointsArray = new Vec2f[]
            {
                // Error
                default,
                
                // C0, C1, C2, C3
                new(0f, 1f), new(1f, 1f), new(1f, 0f), new(0f, 0f),
                
                // M0, M1, M2, M3
                new(0.5f, 1f), new(1f, 0.5f), new(0.5f, 0f), new(0f, 0.5f)
            };
            
            LinesArray = new Line2D[]
            {
                default, // Error
                
                new(PointsArray[(int)TilePoint.C0], PointsArray[(int)TilePoint.C1]), // L_C0_C1
                new(PointsArray[(int)TilePoint.C0], PointsArray[(int)TilePoint.C2]), // L_C0_C2
                new(PointsArray[(int)TilePoint.C0], PointsArray[(int)TilePoint.C3]), // L_C0_C3
                new(PointsArray[(int)TilePoint.C0], PointsArray[(int)TilePoint.M0]), // L_C0_M0
                new(PointsArray[(int)TilePoint.C0], PointsArray[(int)TilePoint.M1]), // L_C0_M1
                new(PointsArray[(int)TilePoint.C0], PointsArray[(int)TilePoint.M2]), // L_C0_M2
                new(PointsArray[(int)TilePoint.C0], PointsArray[(int)TilePoint.M3]), // L_C0_M3
                
                new(PointsArray[(int)TilePoint.C1], PointsArray[(int)TilePoint.C0]), // L_C1_C0
                new(PointsArray[(int)TilePoint.C1], PointsArray[(int)TilePoint.C2]), // L_C1_C2
                new(PointsArray[(int)TilePoint.C1], PointsArray[(int)TilePoint.C3]), // L_C1_C3
                new(PointsArray[(int)TilePoint.C1], PointsArray[(int)TilePoint.M0]), // L_C1_M0
                new(PointsArray[(int)TilePoint.C1], PointsArray[(int)TilePoint.M1]), // L_C1_M1
                new(PointsArray[(int)TilePoint.C1], PointsArray[(int)TilePoint.M2]), // L_C1_M2
                new(PointsArray[(int)TilePoint.C1], PointsArray[(int)TilePoint.M3]), // L_C1_M3
                
                new(PointsArray[(int)TilePoint.C2], PointsArray[(int)TilePoint.C0]), // L_C2_C0
                new(PointsArray[(int)TilePoint.C2], PointsArray[(int)TilePoint.C1]), // L_C2_C1
                new(PointsArray[(int)TilePoint.C2], PointsArray[(int)TilePoint.C3]), // L_C2_C3
                new(PointsArray[(int)TilePoint.C2], PointsArray[(int)TilePoint.M0]), // L_C2_M0
                new(PointsArray[(int)TilePoint.C2], PointsArray[(int)TilePoint.M1]), // L_C2_M1
                new(PointsArray[(int)TilePoint.C2], PointsArray[(int)TilePoint.M2]), // L_C2_M2
                new(PointsArray[(int)TilePoint.C2], PointsArray[(int)TilePoint.M3]), // L_C2_M3
                
                new(PointsArray[(int)TilePoint.C3], PointsArray[(int)TilePoint.C0]), // L_C3_C0
                new(PointsArray[(int)TilePoint.C3], PointsArray[(int)TilePoint.C1]), // L_C3_C1
                new(PointsArray[(int)TilePoint.C3], PointsArray[(int)TilePoint.C2]), // L_C3_C2
                new(PointsArray[(int)TilePoint.C3], PointsArray[(int)TilePoint.M0]), // L_C3_M0
                new(PointsArray[(int)TilePoint.C3], PointsArray[(int)TilePoint.M1]), // L_C3_M1
                new(PointsArray[(int)TilePoint.C3], PointsArray[(int)TilePoint.M2]), // L_C3_M2
                new(PointsArray[(int)TilePoint.C3], PointsArray[(int)TilePoint.M3]), // L_C3_M3
                
                new(PointsArray[(int)TilePoint.M0], PointsArray[(int)TilePoint.C0]), // L_M0_C0
                new(PointsArray[(int)TilePoint.M0], PointsArray[(int)TilePoint.C1]), // L_M0_C1
                new(PointsArray[(int)TilePoint.M0], PointsArray[(int)TilePoint.C2]), // L_M0_C2
                new(PointsArray[(int)TilePoint.M0], PointsArray[(int)TilePoint.C3]), // L_M0_M0
                new(PointsArray[(int)TilePoint.M0], PointsArray[(int)TilePoint.M1]), // L_M0_M1
                new(PointsArray[(int)TilePoint.M0], PointsArray[(int)TilePoint.M2]), // L_M0_M2
                new(PointsArray[(int)TilePoint.M0], PointsArray[(int)TilePoint.M3]), // L_M0_M3
                
                new(PointsArray[(int)TilePoint.M1], PointsArray[(int)TilePoint.C0]), // L_M1_C0
                new(PointsArray[(int)TilePoint.M1], PointsArray[(int)TilePoint.C1]), // L_M1_C1
                new(PointsArray[(int)TilePoint.M1], PointsArray[(int)TilePoint.C2]), // L_M1_C2
                new(PointsArray[(int)TilePoint.M1], PointsArray[(int)TilePoint.C3]), // L_M1_M0
                new(PointsArray[(int)TilePoint.M1], PointsArray[(int)TilePoint.M0]), // L_M1_M0
                new(PointsArray[(int)TilePoint.M1], PointsArray[(int)TilePoint.M2]), // L_M1_M2
                new(PointsArray[(int)TilePoint.M1], PointsArray[(int)TilePoint.M3]), // L_M1_M3
                
                new(PointsArray[(int)TilePoint.M2], PointsArray[(int)TilePoint.C0]), // L_M2_C0
                new(PointsArray[(int)TilePoint.M2], PointsArray[(int)TilePoint.C1]), // L_M2_C1
                new(PointsArray[(int)TilePoint.M2], PointsArray[(int)TilePoint.C2]), // L_M2_C2
                new(PointsArray[(int)TilePoint.M2], PointsArray[(int)TilePoint.C3]), // L_M2_M0
                new(PointsArray[(int)TilePoint.M2], PointsArray[(int)TilePoint.M0]), // L_M2_M0
                new(PointsArray[(int)TilePoint.M2], PointsArray[(int)TilePoint.M1]), // L_M2_M1
                new(PointsArray[(int)TilePoint.M2], PointsArray[(int)TilePoint.M3]), // L_M2_M3
                
                new(PointsArray[(int)TilePoint.M3], PointsArray[(int)TilePoint.C0]), // L_M3_C0
                new(PointsArray[(int)TilePoint.M3], PointsArray[(int)TilePoint.C1]), // L_M3_C1
                new(PointsArray[(int)TilePoint.M3], PointsArray[(int)TilePoint.C2]), // L_M3_C2
                new(PointsArray[(int)TilePoint.M3], PointsArray[(int)TilePoint.C3]), // L_M3_M0
                new(PointsArray[(int)TilePoint.M3], PointsArray[(int)TilePoint.M0]), // L_M3_M0
                new(PointsArray[(int)TilePoint.M3], PointsArray[(int)TilePoint.M1]), // L_M3_M1
                new(PointsArray[(int)TilePoint.M3], PointsArray[(int)TilePoint.M2]), // L_M3_M2
            };

            ShapeLinesCount = new[]
            {
                0, // Error,
                0, // EmptyBlock
                4, // FullBlock,
                4, // HalfBlock,
                3, // TriangleBlock,
                3, // L1Block - part 1 of L block
                3, // L2Block - part 2 of L block
                1, // QuarterPlatform,
                1, // HalfPlatform,
                1, // FullPlatform,
            };
        }
        
        /// <summary>
        /// Takes in TilePoint enum
        /// </summary>
        /// <returns>VeC1f for point values</returns>
        [MethodImpl((MethodImplOptions) 256)] // Inline
        public static Vec2f GetTilePointPosition(TilePoint point)
        {
            return point switch
            {
                TilePoint.Error => default,
                TilePoint.C0 => PointsArray[(int) TilePoint.C0],
                TilePoint.C1 => PointsArray[(int) TilePoint.C1],
                TilePoint.C2 => PointsArray[(int) TilePoint.C2],
                TilePoint.C3 => PointsArray[(int) TilePoint.C3],
                TilePoint.M0 => PointsArray[(int) TilePoint.M0],
                TilePoint.M1 => PointsArray[(int) TilePoint.M1],
                TilePoint.M2 => PointsArray[(int) TilePoint.M2],
                TilePoint.M3 => PointsArray[(int) TilePoint.M3],
                _ => default
            };
        }
        
        /// <summary>
        /// Takes in TileLineSegment
        /// </summary>
        /// <returns>The start and finish line</returns>
        [MethodImpl((MethodImplOptions) 256)] // Inline
        public static Line2D GetTileLineSegmentPosition(TileLineSegment lineSegment)
        {
            return lineSegment switch
            {
                TileLineSegment.Error   => LinesArray[(int) TileLineSegment.Error],
                TileLineSegment.L_C0_C1 => LinesArray[(int) TileLineSegment.L_C0_C1],
                TileLineSegment.L_C0_C2 => LinesArray[(int) TileLineSegment.L_C0_C2],
                TileLineSegment.L_C0_C3 => LinesArray[(int) TileLineSegment.L_C0_C3],
                TileLineSegment.L_C0_M0 => LinesArray[(int) TileLineSegment.L_C0_M0],
                TileLineSegment.L_C0_M1 => LinesArray[(int) TileLineSegment.L_C0_M1],
                TileLineSegment.L_C0_M2 => LinesArray[(int) TileLineSegment.L_C0_M2],
                TileLineSegment.L_C0_M3 => LinesArray[(int) TileLineSegment.L_C0_M3],
                TileLineSegment.L_C1_C0 => LinesArray[(int) TileLineSegment.L_C1_C0],
                TileLineSegment.L_C1_C2 => LinesArray[(int) TileLineSegment.L_C1_C2],
                TileLineSegment.L_C1_C3 => LinesArray[(int) TileLineSegment.L_C1_C3],
                TileLineSegment.L_C1_M0 => LinesArray[(int) TileLineSegment.L_C1_M0],
                TileLineSegment.L_C1_M1 => LinesArray[(int) TileLineSegment.L_C1_M1],
                TileLineSegment.L_C1_M2 => LinesArray[(int) TileLineSegment.L_C1_M2],
                TileLineSegment.L_C1_M3 => LinesArray[(int) TileLineSegment.L_C1_M3],
                TileLineSegment.L_C2_C0 => LinesArray[(int) TileLineSegment.L_C2_C0],
                TileLineSegment.L_C2_C1 => LinesArray[(int) TileLineSegment.L_C2_C1],
                TileLineSegment.L_C2_C3 => LinesArray[(int) TileLineSegment.L_C2_C3],
                TileLineSegment.L_C2_M0 => LinesArray[(int) TileLineSegment.L_C2_M0],
                TileLineSegment.L_C2_M1 => LinesArray[(int) TileLineSegment.L_C2_M1],
                TileLineSegment.L_C2_M2 => LinesArray[(int) TileLineSegment.L_C2_M2],
                TileLineSegment.L_C2_M3 => LinesArray[(int) TileLineSegment.L_C2_M3],
                TileLineSegment.L_C3_C0 => LinesArray[(int) TileLineSegment.L_C3_C0],
                TileLineSegment.L_C3_C1 => LinesArray[(int) TileLineSegment.L_C3_C1],
                TileLineSegment.L_C3_C2 => LinesArray[(int) TileLineSegment.L_C3_C2],
                TileLineSegment.L_C3_M0 => LinesArray[(int) TileLineSegment.L_C3_M0],
                TileLineSegment.L_C3_M1 => LinesArray[(int) TileLineSegment.L_C3_M1],
                TileLineSegment.L_C3_M2 => LinesArray[(int) TileLineSegment.L_C3_M2],
                TileLineSegment.L_C3_M3 => LinesArray[(int) TileLineSegment.L_C3_M3],
                TileLineSegment.L_M0_C0 => LinesArray[(int) TileLineSegment.L_M0_C0],
                TileLineSegment.L_M0_C1 => LinesArray[(int) TileLineSegment.L_M0_C1],
                TileLineSegment.L_M0_C2 => LinesArray[(int) TileLineSegment.L_M0_C2],
                TileLineSegment.L_M0_C3 => LinesArray[(int) TileLineSegment.L_M0_C3],
                TileLineSegment.L_M0_M1 => LinesArray[(int) TileLineSegment.L_M0_M1],
                TileLineSegment.L_M0_M2 => LinesArray[(int) TileLineSegment.L_M0_M2],
                TileLineSegment.L_M0_M3 => LinesArray[(int) TileLineSegment.L_M0_M3],
                TileLineSegment.L_M1_C0 => LinesArray[(int) TileLineSegment.L_M1_C0],
                TileLineSegment.L_M1_C1 => LinesArray[(int) TileLineSegment.L_M1_C1],
                TileLineSegment.L_M1_C2 => LinesArray[(int) TileLineSegment.L_M1_C2],
                TileLineSegment.L_M1_C3 => LinesArray[(int) TileLineSegment.L_M1_C3],
                TileLineSegment.L_M1_M0 => LinesArray[(int) TileLineSegment.L_M1_M0],
                TileLineSegment.L_M1_M2 => LinesArray[(int) TileLineSegment.L_M1_M2],
                TileLineSegment.L_M1_M3 => LinesArray[(int) TileLineSegment.L_M1_M3],
                TileLineSegment.L_M2_C0 => LinesArray[(int) TileLineSegment.L_M2_C0],
                TileLineSegment.L_M2_C1 => LinesArray[(int) TileLineSegment.L_M2_C1],
                TileLineSegment.L_M2_C2 => LinesArray[(int) TileLineSegment.L_M2_C2],
                TileLineSegment.L_M2_C3 => LinesArray[(int) TileLineSegment.L_M2_C3],
                TileLineSegment.L_M2_M0 => LinesArray[(int) TileLineSegment.L_M2_M0],
                TileLineSegment.L_M2_M1 => LinesArray[(int) TileLineSegment.L_M2_M1],
                TileLineSegment.L_M2_M3 => LinesArray[(int) TileLineSegment.L_M2_M3],
                TileLineSegment.L_M3_C0 => LinesArray[(int) TileLineSegment.L_M3_C0],
                TileLineSegment.L_M3_C1 => LinesArray[(int) TileLineSegment.L_M3_C1],
                TileLineSegment.L_M3_C2 => LinesArray[(int) TileLineSegment.L_M3_C2],
                TileLineSegment.L_M3_C3 => LinesArray[(int) TileLineSegment.L_M3_C3],
                TileLineSegment.L_M3_M0 => LinesArray[(int) TileLineSegment.L_M3_M0],
                TileLineSegment.L_M3_M1 => LinesArray[(int) TileLineSegment.L_M3_M1],
                TileLineSegment.L_M3_M2 => LinesArray[(int) TileLineSegment.L_M3_M2],
                _ => default
            };
        }

        /// <summary>
        /// Takes in TileShape
        /// </summary>
        /// <returns>TileShape collision lines count</returns>
        [MethodImpl((MethodImplOptions) 256)] // Inline
        public static int GetLinesCount(TileShape shape)
        {
            return shape switch
            {
                TileShape.Error             => ShapeLinesCount[(int) TileShape.Error],
                TileShape.EmptyBlock        => ShapeLinesCount[(int) TileShape.EmptyBlock],
                TileShape.FullBlock         => ShapeLinesCount[(int) TileShape.FullBlock],
                TileShape.HalfBlock         => ShapeLinesCount[(int) TileShape.HalfBlock],
                TileShape.L1Block           => ShapeLinesCount[(int) TileShape.L1Block],
                TileShape.L2Block           => ShapeLinesCount[(int) TileShape.L2Block],
                TileShape.TriangleBlock     => ShapeLinesCount[(int) TileShape.TriangleBlock],
                TileShape.QuarterPlatform   => ShapeLinesCount[(int) TileShape.QuarterPlatform],
                TileShape.HalfPlatform      => ShapeLinesCount[(int) TileShape.HalfPlatform],
                TileShape.FullPlatform      => ShapeLinesCount[(int) TileShape.FullPlatform],
                _ => 0
            };
        }
        
        /// <summary>
        /// Takes in current shape from Property
        /// </summary>
        /// <returns>All lines for each TileShape+Rotation</returns>
        public static Line2D[] GetShapeLines(TileShapeAndRotation shape)
        {
            switch (shape)
            {
                case TileShapeAndRotation.Error:
                    return null;
                case TileShapeAndRotation.EmptyBlock:
                    return null;
                case TileShapeAndRotation.FB_R0:
                    return new[]
                    {
                        GetTileLineSegmentPosition(TileLineSegment.L_C0_C1),
                        GetTileLineSegmentPosition(TileLineSegment.L_C1_C2),
                        GetTileLineSegmentPosition(TileLineSegment.L_C2_C3),
                        GetTileLineSegmentPosition(TileLineSegment.L_C3_C0),
                    };

                #region Half Block
                case TileShapeAndRotation.HB_R0:
                    return new[]
                    {
                        GetTileLineSegmentPosition(TileLineSegment.L_C0_M0),
                        GetTileLineSegmentPosition(TileLineSegment.L_M0_C2),
                        GetTileLineSegmentPosition(TileLineSegment.L_M2_C3),
                        GetTileLineSegmentPosition(TileLineSegment.L_C3_C0),
                    };
                case TileShapeAndRotation.HB_R1:
                    return new[]
                    {
                        GetTileLineSegmentPosition(TileLineSegment.L_C0_C1),
                        GetTileLineSegmentPosition(TileLineSegment.L_C1_M1),
                        GetTileLineSegmentPosition(TileLineSegment.L_M1_M3),
                        GetTileLineSegmentPosition(TileLineSegment.L_M3_C0),
                    };
                case TileShapeAndRotation.HB_R2:
                    return new[]
                    {
                        GetTileLineSegmentPosition(TileLineSegment.L_M0_C1),
                        GetTileLineSegmentPosition(TileLineSegment.L_C1_C2),
                        GetTileLineSegmentPosition(TileLineSegment.L_C2_M2),
                        GetTileLineSegmentPosition(TileLineSegment.L_M2_M0),
                    };
                case TileShapeAndRotation.HB_R3:
                    return new[]
                    {
                        GetTileLineSegmentPosition(TileLineSegment.L_M3_M1),
                        GetTileLineSegmentPosition(TileLineSegment.L_M1_C2),
                        GetTileLineSegmentPosition(TileLineSegment.L_C2_C3),
                        GetTileLineSegmentPosition(TileLineSegment.L_C3_M3),
                    };
                #endregion
                
                #region Triangle Block
                case TileShapeAndRotation.TB_R0:
                    return new[]
                    {
                        GetTileLineSegmentPosition(TileLineSegment.L_C0_C1),
                        GetTileLineSegmentPosition(TileLineSegment.L_C1_C3),
                        GetTileLineSegmentPosition(TileLineSegment.L_C3_C0),
                    };
                case TileShapeAndRotation.TB_R1:
                    return new[]
                    {
                        GetTileLineSegmentPosition(TileLineSegment.L_C0_C1),
                        GetTileLineSegmentPosition(TileLineSegment.L_C1_C2),
                        GetTileLineSegmentPosition(TileLineSegment.L_C2_C0),
                    };
                case TileShapeAndRotation.TB_R2:
                    return new[]
                    {
                        GetTileLineSegmentPosition(TileLineSegment.L_C1_C2),
                        GetTileLineSegmentPosition(TileLineSegment.L_C2_C3),
                        GetTileLineSegmentPosition(TileLineSegment.L_C3_C1),
                    };
                case TileShapeAndRotation.TB_R3:
                    return new[]
                    {
                        GetTileLineSegmentPosition(TileLineSegment.L_C0_C2),
                        GetTileLineSegmentPosition(TileLineSegment.L_C2_C3),
                        GetTileLineSegmentPosition(TileLineSegment.L_C3_C0),
                    };
                #endregion
                
                #region L1Block
                case TileShapeAndRotation.L1_R0:
                    return new[]
                    {
                        GetTileLineSegmentPosition(TileLineSegment.L_C0_M0),
                        GetTileLineSegmentPosition(TileLineSegment.L_M0_C3),
                        GetTileLineSegmentPosition(TileLineSegment.L_C3_C0),
                    };
                case TileShapeAndRotation.L1_R1:
                    return new[]
                    {
                        GetTileLineSegmentPosition(TileLineSegment.L_C0_C1),
                        GetTileLineSegmentPosition(TileLineSegment.L_C1_M1),
                        GetTileLineSegmentPosition(TileLineSegment.L_M1_C0),
                    };
                case TileShapeAndRotation.L1_R2:
                    return new[]
                    {
                        GetTileLineSegmentPosition(TileLineSegment.L_C1_C2),
                        GetTileLineSegmentPosition(TileLineSegment.L_C2_M2),
                        GetTileLineSegmentPosition(TileLineSegment.L_M2_C1),
                    };
                case TileShapeAndRotation.L1_R3:
                    return new[]
                    {
                        GetTileLineSegmentPosition(TileLineSegment.L_C0_M1),
                        GetTileLineSegmentPosition(TileLineSegment.L_M1_M3),
                        GetTileLineSegmentPosition(TileLineSegment.L_M3_C0),
                    };
                case TileShapeAndRotation.L1_R4:
                    return new[]
                    {
                        GetTileLineSegmentPosition(TileLineSegment.L_C0_M2),
                        GetTileLineSegmentPosition(TileLineSegment.L_M2_C3),
                        GetTileLineSegmentPosition(TileLineSegment.L_C3_C0),
                    };
                case TileShapeAndRotation.L1_R5:
                    return new[]
                    {
                        GetTileLineSegmentPosition(TileLineSegment.L_C0_C1),
                        GetTileLineSegmentPosition(TileLineSegment.L_C1_M3),
                        GetTileLineSegmentPosition(TileLineSegment.L_M3_C0),
                    };
                case TileShapeAndRotation.L1_R6:
                    return new[]
                    {
                        GetTileLineSegmentPosition(TileLineSegment.L_M1_C1),
                        GetTileLineSegmentPosition(TileLineSegment.L_C1_C2),
                        GetTileLineSegmentPosition(TileLineSegment.L_C2_M1),
                    };
                case TileShapeAndRotation.L1_R7:
                    return new[]
                    {
                        GetTileLineSegmentPosition(TileLineSegment.L_C1_M1),
                        GetTileLineSegmentPosition(TileLineSegment.L_M1_M3),
                        GetTileLineSegmentPosition(TileLineSegment.L_M3_C1),
                    };
                #endregion
                
                #region L2Block
                case TileShapeAndRotation.L2_R0:
                    return new[]
                    {
                        GetTileLineSegmentPosition(TileLineSegment.L_C0_C1),
                        GetTileLineSegmentPosition(TileLineSegment.L_C1_M2),
                        GetTileLineSegmentPosition(TileLineSegment.L_M2_C3),
                        GetTileLineSegmentPosition(TileLineSegment.L_C3_C0),
                    };
                case TileShapeAndRotation.L2_R1:
                    return new[]
                    {
                        GetTileLineSegmentPosition(TileLineSegment.L_C0_C1),
                        GetTileLineSegmentPosition(TileLineSegment.L_C1_C2),
                        GetTileLineSegmentPosition(TileLineSegment.L_C2_M3),
                        GetTileLineSegmentPosition(TileLineSegment.L_M3_C0),
                    };
                case TileShapeAndRotation.L2_R2:
                    return new[]
                    {
                        GetTileLineSegmentPosition(TileLineSegment.L_M0_C1),
                        GetTileLineSegmentPosition(TileLineSegment.L_C1_C2),
                        GetTileLineSegmentPosition(TileLineSegment.L_C2_C3),
                        GetTileLineSegmentPosition(TileLineSegment.L_C3_M0),
                    };
                case TileShapeAndRotation.L2_R3:
                    return new[]
                    {
                        GetTileLineSegmentPosition(TileLineSegment.L_C0_M1),
                        GetTileLineSegmentPosition(TileLineSegment.L_M1_C2),
                        GetTileLineSegmentPosition(TileLineSegment.L_C2_C3),
                        GetTileLineSegmentPosition(TileLineSegment.L_C3_C0),
                    };

                case TileShapeAndRotation.L2_R4:
                    return new[]
                    {
                        GetTileLineSegmentPosition(TileLineSegment.L_C0_C1),
                        GetTileLineSegmentPosition(TileLineSegment.L_C1_C2),
                        GetTileLineSegmentPosition(TileLineSegment.L_C2_M2),
                        GetTileLineSegmentPosition(TileLineSegment.L_M2_C0),
                    };
 
                case TileShapeAndRotation.L2_R5:
                    return new[]
                    {
                        GetTileLineSegmentPosition(TileLineSegment.L_C1_C2),
                        GetTileLineSegmentPosition(TileLineSegment.L_C2_C3),
                        GetTileLineSegmentPosition(TileLineSegment.L_C3_M3),
                        GetTileLineSegmentPosition(TileLineSegment.L_M3_C1),
                    };

                case TileShapeAndRotation.L2_R6:
                    return new[]
                    {
                        GetTileLineSegmentPosition(TileLineSegment.L_C0_M0),
                        GetTileLineSegmentPosition(TileLineSegment.L_M0_C2),
                        GetTileLineSegmentPosition(TileLineSegment.L_C2_C3),
                        GetTileLineSegmentPosition(TileLineSegment.L_C3_C0),
                    };
                case TileShapeAndRotation.L2_R7:
                    return new[]
                    {
                        GetTileLineSegmentPosition(TileLineSegment.L_C0_C1),
                        GetTileLineSegmentPosition(TileLineSegment.L_C1_C2),
                        GetTileLineSegmentPosition(TileLineSegment.L_C2_M3),
                        GetTileLineSegmentPosition(TileLineSegment.L_M3_C0),
                    };
                #endregion

                #region Quarter Platform
                case TileShapeAndRotation.QP_R0:
                    return new[]
                    {
                        GetTileLineSegmentPosition(TileLineSegment.L_C0_C1),
                    };
                case TileShapeAndRotation.QP_R1:
                    return new[]
                    {
                        GetTileLineSegmentPosition(TileLineSegment.L_C1_C2),
                    };
                case TileShapeAndRotation.QP_R2:
                    return new[]
                    {
                        GetTileLineSegmentPosition(TileLineSegment.L_C2_C3),
                    };
                case TileShapeAndRotation.QP_R3:
                    return new[]
                    {
                        GetTileLineSegmentPosition(TileLineSegment.L_C3_C0),
                    };
                #endregion

                #region Half Platform
                case TileShapeAndRotation.HP_R0:
                    return new[]
                    {
                        GetTileLineSegmentPosition(TileLineSegment.L_C0_C1),
                    };
                case TileShapeAndRotation.HP_R1:
                    return new[]
                    {
                        GetTileLineSegmentPosition(TileLineSegment.L_C1_C2),
                    };
                case TileShapeAndRotation.HP_R2:
                    return new[]
                    {
                        GetTileLineSegmentPosition(TileLineSegment.L_C2_C3),
                    };
                case TileShapeAndRotation.HP_R3:
                    return new[]
                    {
                        GetTileLineSegmentPosition(TileLineSegment.L_C3_C0),
                    };
                #endregion

                #region Full Platform
                case TileShapeAndRotation.FP_R0:
                    return new[]
                    {
                        GetTileLineSegmentPosition(TileLineSegment.L_C0_C1),
                    };
                case TileShapeAndRotation.FP_R1:
                    return new[]
                    {
                        GetTileLineSegmentPosition(TileLineSegment.L_C1_C2),
                    };
                case TileShapeAndRotation.FP_R2:
                    return new[]
                    {
                        GetTileLineSegmentPosition(TileLineSegment.L_C2_C3),
                    };
                case TileShapeAndRotation.FP_R3:
                    return new[]
                    {
                        GetTileLineSegmentPosition(TileLineSegment.L_C3_C0),
                    };
                #endregion
                
                default:
                    return null;
            }
        }

        /// <summary>
        /// Takes in current shape from Property
        /// </summary>
        /// <returns>Rotates shape 90 degree clockwise and outputs the new shape enum</returns>
        public static TileShapeAndRotation RotateShape(TileShapeAndRotation shape)
        {
            switch (shape)
            {
                case TileShapeAndRotation.Error:
                    return TileShapeAndRotation.Error;
                case TileShapeAndRotation.EmptyBlock:
                    return TileShapeAndRotation.EmptyBlock;
                case TileShapeAndRotation.FB_R0:
                    return TileShapeAndRotation.FB_R0;

                #region Rotate Half Block shape
                
                case TileShapeAndRotation.HB_R0:
                    return TileShapeAndRotation.HB_R1;
                case TileShapeAndRotation.HB_R1:
                    return TileShapeAndRotation.HB_R2;
                case TileShapeAndRotation.HB_R2:
                    return TileShapeAndRotation.HB_R3;
                case TileShapeAndRotation.HB_R3:
                    return TileShapeAndRotation.HB_R0;
                
                #endregion 
                
                #region Rotate Triangle Block shape

                case TileShapeAndRotation.TB_R0:
                    return TileShapeAndRotation.TB_R1;
                case TileShapeAndRotation.TB_R1:
                    return TileShapeAndRotation.TB_R2;
                case TileShapeAndRotation.TB_R2:
                    return TileShapeAndRotation.TB_R3;
                case TileShapeAndRotation.TB_R3:
                    return TileShapeAndRotation.TB_R0;    
                
                #endregion
                
                #region Rotate L1 shape

                case TileShapeAndRotation.L1_R0:
                    return TileShapeAndRotation.L1_R1;
                case TileShapeAndRotation.L1_R1:
                    return TileShapeAndRotation.L1_R2;
                case TileShapeAndRotation.L1_R2:
                    return TileShapeAndRotation.L1_R3;
                case TileShapeAndRotation.L1_R3:
                    return TileShapeAndRotation.L1_R0;
                case TileShapeAndRotation.L1_R4:
                    return TileShapeAndRotation.L1_R5;
                case TileShapeAndRotation.L1_R5:
                    return TileShapeAndRotation.L1_R6;
                case TileShapeAndRotation.L1_R6:
                    return TileShapeAndRotation.L1_R7;
                case TileShapeAndRotation.L1_R7:
                    return TileShapeAndRotation.L1_R4;

                #endregion

                #region Rotate L2 shape

                case TileShapeAndRotation.L2_R0:
                    return TileShapeAndRotation.L2_R1;
                case TileShapeAndRotation.L2_R1:
                    return TileShapeAndRotation.L2_R2;
                case TileShapeAndRotation.L2_R2:
                    return TileShapeAndRotation.L2_R3;
                case TileShapeAndRotation.L2_R3:
                    return TileShapeAndRotation.L2_R0;
                case TileShapeAndRotation.L2_R4:
                    return TileShapeAndRotation.L2_R5;
                case TileShapeAndRotation.L2_R5:
                    return TileShapeAndRotation.L2_R6;
                case TileShapeAndRotation.L2_R6:
                    return TileShapeAndRotation.L2_R7;
                case TileShapeAndRotation.L2_R7:
                    return TileShapeAndRotation.L2_R4;

                #endregion

                #region Rotate Quarter Platform shape

                case TileShapeAndRotation.QP_R0:
                    return TileShapeAndRotation.QP_R1;
                case TileShapeAndRotation.QP_R1:
                    return TileShapeAndRotation.QP_R2;
                case TileShapeAndRotation.QP_R2:
                    return TileShapeAndRotation.QP_R3;
                case TileShapeAndRotation.QP_R3:
                    return TileShapeAndRotation.QP_R0;

                #endregion

                #region Rotate Half Platform shape

                case TileShapeAndRotation.HP_R0:
                    return TileShapeAndRotation.HP_R1;
                case TileShapeAndRotation.HP_R1:
                    return TileShapeAndRotation.HP_R2;
                case TileShapeAndRotation.HP_R2:
                    return TileShapeAndRotation.HP_R3;
                case TileShapeAndRotation.HP_R3:
                    return TileShapeAndRotation.HP_R0;

                #endregion

                #region Rotate Full Platform shape

                case TileShapeAndRotation.FP_R0:
                    return TileShapeAndRotation.FP_R1;
                case TileShapeAndRotation.FP_R1:
                    return TileShapeAndRotation.FP_R2;
                case TileShapeAndRotation.FP_R2:
                    return TileShapeAndRotation.FP_R3;
                case TileShapeAndRotation.FP_R3:
                    return TileShapeAndRotation.FP_R0;

                #endregion

                default:
                    return TileShapeAndRotation.Error;
            }
        }
        
        /// <summary>
        /// Takes in current shape from Property
        /// </summary>
        /// <returns>flips/mirrors the shape over the x axis and return the new shape enum</returns>
        public static TileShapeAndRotation MirrorShape(TileShapeAndRotation shape)
        {
            switch (shape)
            {
                case TileShapeAndRotation.Error:
                    return TileShapeAndRotation.Error;
                case TileShapeAndRotation.EmptyBlock:
                    return TileShapeAndRotation.EmptyBlock;
                case TileShapeAndRotation.FB_R0:
                    return TileShapeAndRotation.FB_R0;

                #region Mirror Half Block Shape

                case TileShapeAndRotation.HB_R0:
                    return TileShapeAndRotation.HB_R2;
                case TileShapeAndRotation.HB_R1:
                    return TileShapeAndRotation.HB_R1;
                case TileShapeAndRotation.HB_R2:
                    return TileShapeAndRotation.HB_R0;
                case TileShapeAndRotation.HB_R3:
                    return TileShapeAndRotation.HB_R3;

                #endregion

                #region Mirror Triangle Block Shape

                case TileShapeAndRotation.TB_R0:
                    return TileShapeAndRotation.TB_R1;
                case TileShapeAndRotation.TB_R1:
                    return TileShapeAndRotation.TB_R0;
                case TileShapeAndRotation.TB_R2:
                    return TileShapeAndRotation.TB_R3;
                case TileShapeAndRotation.TB_R3:
                    return TileShapeAndRotation.TB_R2;

                #endregion

                #region Mirror L1 Block Shape

                case TileShapeAndRotation.L1_R0:
                    return TileShapeAndRotation.L1_R6;
                case TileShapeAndRotation.L1_R1:
                    return TileShapeAndRotation.L1_R5;
                case TileShapeAndRotation.L1_R2:
                    return TileShapeAndRotation.L1_R4;
                case TileShapeAndRotation.L1_R3:
                    return TileShapeAndRotation.L1_R7;
                case TileShapeAndRotation.L1_R4:
                    return TileShapeAndRotation.L1_R2;
                case TileShapeAndRotation.L1_R5:
                    return TileShapeAndRotation.L1_R1;
                case TileShapeAndRotation.L1_R6:
                    return TileShapeAndRotation.L1_R0;
                case TileShapeAndRotation.L1_R7:
                    return TileShapeAndRotation.L1_R3;

                #endregion

                #region Mirror L2 Block Shape

                case TileShapeAndRotation.L2_R0:
                    return TileShapeAndRotation.L2_R4;
                case TileShapeAndRotation.L2_R1:
                    return TileShapeAndRotation.L2_R7;
                case TileShapeAndRotation.L2_R2:
                    return TileShapeAndRotation.L2_R6;
                case TileShapeAndRotation.L2_R3:
                    return TileShapeAndRotation.L2_R5;
                case TileShapeAndRotation.L2_R4:
                    return TileShapeAndRotation.L2_R0;
                case TileShapeAndRotation.L2_R5:
                    return TileShapeAndRotation.L2_R3;
                case TileShapeAndRotation.L2_R6:
                    return TileShapeAndRotation.L2_R2;
                case TileShapeAndRotation.L2_R7:
                    return TileShapeAndRotation.L2_R1;

                #endregion

                #region Mirror Qaurter Platform Shape

                case TileShapeAndRotation.QP_R0:
                    return TileShapeAndRotation.QP_R0;
                case TileShapeAndRotation.QP_R1:
                    return TileShapeAndRotation.QP_R3;
                case TileShapeAndRotation.QP_R2:
                    return TileShapeAndRotation.QP_R2;
                case TileShapeAndRotation.QP_R3:
                    return TileShapeAndRotation.QP_R1;

                #endregion

                #region Mirror Half Platform Shape

                case TileShapeAndRotation.HP_R0:
                    return TileShapeAndRotation.HP_R0;
                case TileShapeAndRotation.HP_R1:
                    return TileShapeAndRotation.HP_R3;
                case TileShapeAndRotation.HP_R2:
                    return TileShapeAndRotation.HP_R2;
                case TileShapeAndRotation.HP_R3:
                    return TileShapeAndRotation.HP_R1;

                #endregion

                #region Mirror Full Platform Shape

                case TileShapeAndRotation.FP_R0:
                    return TileShapeAndRotation.FP_R0;
                case TileShapeAndRotation.FP_R1:
                    return TileShapeAndRotation.FP_R3;
                case TileShapeAndRotation.FP_R2:
                    return TileShapeAndRotation.FP_R2;
                case TileShapeAndRotation.FP_R3:
                    return TileShapeAndRotation.FP_R1;

                #endregion

                default:
                    return TileShapeAndRotation.Error;
            }
        }
    }
}
