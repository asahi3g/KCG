namespace Collisions
{



    public struct Box2D
    {


        public float x, y;
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