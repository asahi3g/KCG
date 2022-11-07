//imports UnityEngine

using KMath;

public class CameraFollow
{
    // Follow Offset
    private Vec2f offset = new Vec2f(0f, 1.3f);

    // Camera Follow Speed
    [UnityEngine.Range(0, 10)]
    public float followSpeed = 3.0f;

    // Player Position
    Vec2f PlayerPos;

    // Follow Condition
    public bool canFollow = false;

    // Doc: https://docs.unity3d.com/ScriptReference/MonoBehaviour.Update.html
    public void Update()
    {
        // Can Camera Follow Player?
        if(canFollow)
        {
            // Check if projectile has hit a enemy.
            var entities = GameState.Planet.EntitasContext.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentID));
            foreach (var entity in entities)
            {
                if(entity.isAgentPlayer)
                {
                    // Set Player Position from Player Entity
                    PlayerPos = new Vec2f(entity.agentPhysicsState.Position.X, entity.agentPhysicsState.Position.Y) + offset;
                }
            }

            // Follow Player Position
            UnityEngine.Camera.main.transform.position = UnityEngine.Vector3.Slerp(UnityEngine.Camera.main.transform.position, new UnityEngine.Vector3(PlayerPos.X, PlayerPos.Y, -10.0f), followSpeed * UnityEngine.Time.deltaTime);
        }
        
    }
}
