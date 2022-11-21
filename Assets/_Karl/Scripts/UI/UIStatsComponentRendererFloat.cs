using Agent;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStatsComponentRendererFloat : UIMonoBehaviour
{
    [SerializeField] private StatsKindFloat _kind;
    [SerializeField] private Identifier _identifier;
    [SerializeField] private UIGroup _group;
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _tmpValue;

    private StatsComponent _stats;
    private static readonly string DecimalFormat = "F1";
    
#if UNITY_EDITOR
    protected override void Reset()
    {
        base.Reset();
        _kind = StatsKindFloat.Unknown;
        _stats = null;
    }

    protected override void OnValidate()
    {
        base.OnValidate();
        if (!IsPlaying())
        {
            if (_kind != StatsKindFloat.Unknown)
            {
                gameObject.name = $"UI Stats Component - {_kind}";
            }

            UpdateLook(true);
        }
    }
#endif

    public void SetStats(StatsComponent stats)
    {
        Clear();
        _stats = stats;
        if (_stats == null) return;
        _group.GetIdentifier().Alter(_identifier, true);
        _stats.GetValue(_kind).onChanged.AddListener(OnStatsChanged);
        UpdateLook(false);
    }

    public void Clear()
    {
        if (_stats == null) return;
        _group.GetIdentifier().Alter(_identifier, false);
        _stats.GetValue(_kind).onChanged.RemoveListener(OnStatsChanged);
        _tmpValue.SetText("?");
        _stats = null;
    }

    private void OnStatsChanged(ContainerFloat.AlterData<float> ev)
    {
        UpdateLook(false);
    }

    private void UpdateLook(bool isEditor)
    {
        // There is editor options because this makes easier to see UI in inspector
        if (isEditor)
        {
            _slider.maxValue = 1f;
            _slider.minValue = 0f;
            _slider.value = 0.75f;
            
            SetText(0f, 1f);
        }
        else
        {
            if (_stats == null)
            {
                _slider.maxValue = 1f;
                _slider.minValue = 0f;
                _slider.value = 0f;
            }
            else
            {
                ContainerFloat container = _stats.GetValue(_kind);

                _slider.maxValue = container.GetMax();
                _slider.minValue = container.GetMin();
                _slider.value = container.GetValue();
            }

            SetText(_slider.value, _slider.maxValue);
        }

        void SetText(float value, float maxValue)
        {
            _tmpValue.SetText($"{FormatValue(value, false)}/{FormatValue(maxValue, false)}");
        }
    }

    private string FormatValue(float value, bool forceShowDecimal)
    {
        if (forceShowDecimal) return value.ToString(DecimalFormat);
        
        bool hasNoFloating = value % 1 == 0f;
        return hasNoFloating ? Mathf.RoundToInt(value).ToString() : value.ToString(DecimalFormat);
    }


}
