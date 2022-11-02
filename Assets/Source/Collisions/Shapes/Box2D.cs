namespace Collisions
{
    // More efficient than AABox2D for some use cases.
    public struct Box2D
    {
        // Position
        public float x, y;
        // Size
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
