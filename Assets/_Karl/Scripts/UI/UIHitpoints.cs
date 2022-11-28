using UnityEngine;
using UnityEngine.UI;

public class UIHitpoints : BaseMonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private SOValueGradient _gradient;
    [SerializeField] private Image _imageFill;
    [SerializeField] private Image _factionStatus;
    private ContainerInt _container;

    public void SetHitpoints(ContainerInt container)
    {
        _container = container;
        if (_container == null) return;
        _container.onChanged.AddListener(OnContainerChanged);
        UpdateLook();
    }

    private void UpdateLook()
    {
        float value = _container.GetValueNormalized();
        _slider.value = value;
        _imageFill.color = _gradient.GetValue().Evaluate(value);
    }

    private void OnContainerChanged(ContainerInt.AlterData<int> alterData)
    {
        UpdateLook();
    }

    public void SetFactionFriendly(bool isFriendly)
    {
        _factionStatus.enabled = isFriendly;
    }
}
