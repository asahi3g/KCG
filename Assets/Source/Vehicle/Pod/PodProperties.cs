using KMath;

namespace Vehicle.Pod
{

    public struct PodProperties
    {
        public int PropertiesId;
        public string Name;

        public int SpriteId;
        public Vec2f SpriteSize;

        public Vec2f CollisionSize;
        public Vec2f CollisionOffset;
        public Vec2f Scale;

        public float Rotation;

        public Vec2f AngularVelocity;

        public float AngularMass;
        public float AngularAcceleration;
        public float CenterOfGravity;

        public Vec2f CenterOfRotation;

        public bool AffectedByGravity;

        public Vec2f RadarSize;

        public int PodValue;
        public int Score;

        public Vec2f RightPanel;
        public Vec2f LeftPanel;
        public Vec2f TopPanel;
        public Vec2f BottomPanel;

        public Vec2f RightPanelWidth;
        public Vec2f LeftPanelWidth;
        public Vec2f TopPanelWidth;
        public Vec2f BottomPanelWidth;

        public Vec2f RightPanelHeight;
        public Vec2f LeftPanelHeight;
        public Vec2f TopPanelHeight;
        public Vec2f BottomPanelHeight;
    }
}

