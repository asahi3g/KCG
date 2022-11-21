using Entitas;
using KMath;
using System.Collections.Generic;

namespace Vehicle.Pod
{
    [Pod]
    public class StatusComponent : IComponent
    {
        public List<AgentEntity> AgentsInside
            ;
        public int DefaultAgentCount;

        public bool RenderRightPanel;
        public bool RenderLeftPanel;
        public bool RenderTopPanel;
        public bool RenderBottomPanel;

        public bool Exploded;
        public bool RightPanelCollided;
        public bool LeftPanelCollided;
        public bool TopPanelCollided;
        public bool BottomPanelCollided;

        public Vec2f RightPanelPos;
        public Vec2f LeftPanelPos;
        public Vec2f TopPanelPos;
        public Vec2f BottomPanelPos;

        public Vec2f RightPanelOffset;
        public Vec2f LeftPanelOffset;
        public Vec2f TopPanelOffset;
        public Vec2f BottomPanelOffset;

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