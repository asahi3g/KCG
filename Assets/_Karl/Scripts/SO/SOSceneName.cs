using UnityEngine;

[CreateAssetMenu(fileName = "Scene Name - ", menuName = "SO/Scene Name", order = 1)]
public class SOSceneName : SOValue<string>
{
    [SerializeField] private UnityEngine.Object _scene;
    
#if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();
        UpdateSceneName();
    }
#endif

    protected override void Awake()
    {
        base.Awake();
        UpdateSceneName();
    }

    private void UpdateSceneName()
    {
        // This will only work in Editor, in build this object will always be null
        if (_scene == null) return;
        SetValue(_scene.name);
    }
}
