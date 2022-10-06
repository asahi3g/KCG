

using Enums;
using KMath;
using Node;
using System.Collections.Generic;

namespace AI
{
    public class MarineBehavior : BehaviorBase
    {
        public override BehaviorType Type { get { return BehaviorType.Marine; } }

        /// <returns> return Id of root</returns>
        public AgentController GetAgentController(Contexts enititasContext, int agentID)
        {
            AgentController marineController = new AgentController();
            marineController.BlackBoard = new BlackBoard(agentID);
            marineController.Sensors = new List<Sensor.SensorBase>();
            marineController.AttachSensors(new Sensor.EnemySensor());
            marineController.BehaviorTreeRoot = BehaviorTreeGenerator();

            return marineController;
        }
        public override int BehaviorTreeGenerator()
        {
            List<NodeEntity> nodes = new List<NodeEntity>();
            int rootID = GameState.BehaviorTreeCreationAPI.CreateTree();
            {
                nodes.Add(GameState.BehaviorTreeCreationAPI.AddChild(Enums.NodeType.RepeaterNode));
                {
                    nodes.Add(GameState.BehaviorTreeCreationAPI.AddChild(Enums.NodeType.SelectorNode));
                    {
                        nodes.Add(GameState.BehaviorTreeCreationAPI.AddChild(Enums.NodeType.SequenceNode));
                        {
                            nodes.Add(GameState.BehaviorTreeCreationAPI.AddChild(Enums.NodeType.SelectClosestTarget));
                            nodes.Add(GameState.BehaviorTreeCreationAPI.AddChild(Enums.NodeType.ShootFireWeaponAction));
                            nodes.Add(GameState.BehaviorTreeCreationAPI.AddChild(Enums.NodeType.WaitAction));
                            nodes.Add(GameState.BehaviorTreeCreationAPI.AddChild(Enums.NodeType.WaitAction));

                        }
                        GameState.BehaviorTreeCreationAPI.EndNode();
                        nodes.Add(GameState.BehaviorTreeCreationAPI.AddChild(Enums.NodeType.SequenceNode));
                        {
                            nodes.Add(GameState.BehaviorTreeCreationAPI.AddChild(Enums.NodeType.WaitAction));
                        }
                        GameState.BehaviorTreeCreationAPI.EndNode();
                    }
                    GameState.BehaviorTreeCreationAPI.EndNode();
                }
            }

            AddData(nodes);
            return rootID;
        }

        public void AddData(List<NodeEntity> nodes)
        {
            DurationComponent comp1 = new DurationComponent();
            comp1.Duration = 5f;
            nodes[5].AddComponent(3, comp1);
            DurationComponent comp2 = new DurationComponent();
            comp2.Duration = 3f;
            nodes[6].AddComponent(3, comp2);
            DurationComponent comp3 = new DurationComponent();
            comp3.Duration = 1f;
            nodes[8].AddComponent(3, comp3);
        }
    }
}
