using System.Collections;
using Agent;
using Enums;
using KMath;
using UnityEngine;

public class SpawnAgents : BaseMonoBehaviour
{
    [SerializeField] private AgentType[] _types;
    [SerializeField] private float _initialDelay;
    [SerializeField] private float _delay;
    [SerializeField] private float _quantity;
    [SerializeField] private AgentFaction _faction;
    [SerializeField] private float _maxRadius;


    protected override void Awake()
    {
        base.Awake();
        App.Instance.GetPlayer().onCurrentPlanetChanged.AddListener(OnCurrentPlanetChanged);
    }

    private void OnCurrentPlanetChanged(PlanetLoader.Result result)
    {
        if (result == null) return;
        StartCoroutine(SpawnHandler(result));
    }


    private IEnumerator SpawnHandler(PlanetLoader.Result planetCreationResult)
    {
        yield return new WaitForSeconds(_initialDelay);
        for (int i = 0; i < _quantity; i++)
        {
            if (_types != null)
            {
                int length = _types.Length;
                if (length > 0)
                {
                    AgentType agentType;
                        
                    if (length == 1) agentType = _types[0];
                    else agentType = _types[Random.Range(0, length)];

                    AgentEntity agentEntity = planetCreationResult.GetPlanetState().AddAgent(GetSpawnPosition(), agentType, _faction);
                    if (agentEntity.hasAgentAgent3DModel)
                    {
                        if (agentEntity.agentAgent3DModel.Renderer)
                        {
                            agentEntity.agentAgent3DModel.Renderer.transform.parent = transform;
                        }
                    }
                }
            }
            yield return new WaitForSeconds(_delay);
        }
    }

    private Vec2f GetSpawnPosition()
    {
        Vector2 pos = transform.position;
        if (_maxRadius > 0f)
        {
            pos += (new Vector2(Random.value, Random.value) * (Random.Range(0, 1f)));
        }
        return new Vec2f(pos.x, pos.y);
    }
}
