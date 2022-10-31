//imports UnityEngine

using KMath;
using System.Collections;

namespace Events
{
    public class MarineSpawnEvent : UnityEngine.MonoBehaviour
    {
        private AgentEntity enemy;
        private bool Init;

        private void Start()
        {
            StartCoroutine(SpawnMarine());
        }

        IEnumerator SpawnMarine()
        {
            yield return new UnityEngine.WaitForSeconds(1.0f);
            UnityEngine.Vector3 worldPosition = UnityEngine.Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);

            enemy = GameState.Planet.AddAgent(new Vec2f(transform.position.x, transform.position.y), Enums.AgentType.EnemyMarine);

            Init = true;
            yield return null;
        }

        private void Update()
        {
            if(Init)
            {
               transform.position = new UnityEngine.Vector3(enemy.agentPhysicsState.Position.X,
                    enemy.agentPhysicsState.Position.Y);
            }
        }
    }
}
