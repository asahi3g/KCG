
using UnityEngine.Events;

public class CharacterRenderer : BaseMonoBehaviour
{
    private AgentEntity _agent;

    public class Event : UnityEvent<CharacterRenderer> { }

    public AgentEntity GetAgent() => _agent;


    public void SetAgent(AgentEntity agent)
    {
        _agent = agent;
        if (_agent.hasAgentModel3D)
        {
            _agent.agentModel3D.GameObject.transform.parent = transform;
        }
    }
}
