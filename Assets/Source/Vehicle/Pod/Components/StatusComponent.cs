using Entitas;
using KMath;
using System.Collections.Generic;

namespace Vehicle.Pod
{
    [Pod]
    public class StatusComponent : IComponent
    {
        public List<AgentEntity> AgentsInside;

        public Vec2f RightPanel;
        public Vec2f LeftPanel;
        public Vec2f TopPanel;
        public Vec2f BottomPanel;

        public float RightPanelWidth;
        public float LeftPanelWidth;
        public float TopPanelWidth;
        public float BottomPanelWidth;

        public float RightPanelHeight;
        public float LeftPanelHeight;
        public float TopPanelHeight;
        public float BottomPanelHeight;
    }
}