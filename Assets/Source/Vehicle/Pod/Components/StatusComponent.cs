using Entitas;
using System.Collections.Generic;

namespace Vehicle.Pod
{
    [Pod]
    public class StatusComponent : IComponent
    {
        public List<AgentEntity> AgentsInside;

        public KMath.Vec2f RightPanel;
        public KMath.Vec2f LeftPanel;
        public KMath.Vec2f TopPanel;
        public KMath.Vec2f BottomPanel;

        public KMath.Vec2f RightPanelWidth;
        public KMath.Vec2f LeftPanelWidth;
        public KMath.Vec2f TopPanelWidth;
        public KMath.Vec2f BottomPanelWidth;

        public KMath.Vec2f RightPanelHeight;
        public KMath.Vec2f LeftPanelHeight;
        public KMath.Vec2f TopPanelHeight;
        public KMath.Vec2f BottomPanelHeight;
    }
}