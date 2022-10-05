

using Enums;
using KMath;
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
            int rootID = GameState.BehaviorTreeCreationAPI.CreateTree();
            {
                GameState.BehaviorTreeCreationAPI.AddChild(Enums.NodeType.RepeaterNode);
                {
                    GameState.BehaviorTreeCreationAPI.AddChild(Enums.NodeType.SelectorNode);
                    {
                        GameState.BehaviorTreeCreationAPI.AddChild(Enums.NodeType.SequenceNode);
                        {
                            GameState.BehaviorTreeCreationAPI.AddChild(Enums.NodeType.SelectClosestTarget);
                            GameState.BehaviorTreeCreationAPI.AddChild(Enums.NodeType.ShootFireWeaponAction);
                            GameState.BehaviorTreeCreationAPI.AddChild(Enums.NodeType.WaitAction);
                            GameState.BehaviorTreeCreationAPI.AddChild(Enums.NodeType.WaitAction);

                        }
                        GameState.BehaviorTreeCreationAPI.EndNode();
                        GameState.BehaviorTreeCreationAPI.AddChild(Enums.NodeType.SequenceNode);
                        {
                            GameState.BehaviorTreeCreationAPI.AddChild(Enums.NodeType.WaitAction);
                        }
                        GameState.BehaviorTreeCreationAPI.EndNode();
                    }
                    GameState.BehaviorTreeCreationAPI.EndNode();
                }
            }
         
            return rootID;
        }
    }
}
