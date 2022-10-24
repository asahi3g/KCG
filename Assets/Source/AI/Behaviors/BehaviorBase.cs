﻿using Enums;
using System.Collections.Generic;
using KMath;
using System.Windows.Forms.DataVisualization.Charting;
using static Unity.VisualScripting.Metadata;
using AI.Sensor;
using System;

namespace AI
{
    public class BehaviorBase
    {
        public virtual BehaviorType Type { get { return BehaviorType.Error; } }
        public virtual string Name { get { return Type.ToString(); } }
        public virtual List<NodeInfo> Nodes
        {
            get
            {
                List<NodeInfo> nodes = new List<NodeInfo>();
                NodeInfo node = new NodeInfo
                {
                    index = 0,
                    pos = new Vec2f(0, 0),
                    type = NodeType.DecoratorNode,
                    children = new List<int>(),
                };
                nodes.Add(node);
                return nodes;
            }
        }

        public virtual int SensorCount { get { return 0; } }

        public virtual SensorEntity[] Sensors
        {
            get
            {
                int sensorLength = Enum.GetNames(typeof(SensorType)).Length - 1;
                SensorEntity[] sensors = new SensorEntity[sensorLength];
                return sensors;
            }
        }

        public virtual BlackBoardModel AgentBlackboard
        {
            get
            {
                BlackBoardModel blackboard = new BlackBoardModel(Type);
                return blackboard;
            }
        }
    }
}
