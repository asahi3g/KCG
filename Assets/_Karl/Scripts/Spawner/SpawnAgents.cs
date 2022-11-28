using System.Collections;
using Enums;
using KMath;
using UnityEngine;

public class SpawnAgents : BaseMonoBehaviour
{
    [SerializeField] private AgentType[] _types;
    [SerializeField] private float _initialDelay;
    [SerializeField] private float _delay;
    [SerializeField] private float _quantity;
    [SerializeField] private int _faction;
    [SerializeField] private float _maxRadius;


    protected override void Awake()
    {
        base.Awake();
        App.Instance.GetPlayer().onCurrentPlanetChanged.AddListener(OnCurrentPlanetChanged);
    }

    private void OnCurrentPlanetChanged(IPlanetCreationResult planetCreationResult)
    {
        if (planetCreationResult == null) return;
        StartCoroutine(SpawnHandler(planetCreationResult));
    }


    private IEnumerator SpawnHandler(IPlanetCreationResult planetCreationResult)
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

                    planetCreationResult.GetPlanetRenderer().CreateAgent(GetSpawnPosition(), agentType, _faction, out AgentRenderer agentRenderer);
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
