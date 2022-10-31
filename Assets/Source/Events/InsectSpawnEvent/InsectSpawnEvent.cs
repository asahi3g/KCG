//imports UnityEngine

using KMath;
using System.Collections;

namespace Events
{
    public class InsectSpawnEvent : UnityEngine.MonoBehaviour
    {
        private AgentEntity enemy;
        private bool Init;

        private void Start()
        {
            StartCoroutine(SpawnInsect());
        }

        IEnumerator SpawnInsect()
        {
            yield return new UnityEngine.WaitForSeconds(1.0f);

            enemy = GameState.Planet.AddAgent(new Vec2f(transform.position.x, transform.position.y), Enums.AgentType.EnemyInsect);

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
