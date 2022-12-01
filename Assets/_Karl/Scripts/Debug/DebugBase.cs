
public abstract class DebugBase : BaseMonoBehaviour
{

    private void Update()
    {
        if (!GameState.IsInitialized) return;
        OnUpdate();
    }

    protected virtual void OnUpdate(){}
}
