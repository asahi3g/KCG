namespace Collisions
{
    /// <summary>
    /// More efficient than AABox2D for some use cases.
    /// </summary>
    public struct Box2D
    {
        /// <summary>
        /// Position
        /// </summary>
        public float x, y;
        /// <summary>
        /// Size
        /// </summary>
        public float w, h;

        public Box2D(float x, float y, float w, float h)
        {
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
        }
    }
}
