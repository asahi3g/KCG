using UnityEngine;
using UnityEngine.UI;

public abstract class OnButton : BaseMonoBehaviour
{
    [SerializeField] private Button _button;
    
#if UNITY_EDITOR
    protected override void Reset()
    {
        base.Reset();
        _button = GetComponent<Button>();
    }
#endif

    protected override void OnEnable()
    {
        base.OnEnable();
        if(_button != null) _button.onClick.AddListener(OnClick);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        if(_button != null) _button.onClick.RemoveListener(OnClick);
    }

    protected abstract void OnClick();
}
