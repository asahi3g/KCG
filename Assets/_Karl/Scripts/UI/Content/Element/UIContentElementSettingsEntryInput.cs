using TMPro;
using UnityEngine;

public class UIContentElementSettingsEntryInput : UIContentElementSettingsEntry
{
    [SerializeField] private SOInput _sO;
    [Space]
    [SerializeField] private TMP_Text _tmpTitle;
    [SerializeField] private TMP_Text _tmpPrimary;
    [SerializeField] private TMP_Text _tmpSecondary;

    private static readonly string Unknown = "?";
    private const float UnknownAlpha = 0.2f;
    
#if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();
        if(!IsPlaying()) UpdateLook();
    }
#endif
    
    
    protected override bool CanSelect()
    {
        return false;
    }


    public override void UpdateLook()
    {
        bool soExists = _sO != null;
        

        Set(_tmpTitle, soExists ? _sO.GetTitle() : Unknown, soExists);

        // Primary Key
        if (soExists)
        {
            Set(_tmpPrimary, _sO.GetPrimary());
        }
        else
        {
            Set(_tmpPrimary, Unknown, false);
        }
        
        // Secondary Key
        if (soExists)
        {
            Set(_tmpSecondary, _sO.GetSecondary());
        }
        else
        {
            Set(_tmpSecondary, Unknown, false);
        }

        // GameObject name (easier to understand in inspector)
        if (GetContent() != null)
        {
            if (GetContent().GetPrefab() != null)
            {
                gameObject.name = $"{GetContent().GetPrefab().name} ({(_sO == null ? Unknown : _sO.GetTitle())})";
            }
        }
    }
    
    private void Set(TMP_Text tmp, string text, bool contains)
    {
        if (tmp == null) return;
        tmp.SetText(text);
        tmp.color = tmp.color.SetAlpha(contains ? 1f : UnknownAlpha);
    }
        
    private void Set(TMP_Text tmp, KeyCode keyCode)
    {
        if (tmp == null) return;
        Set(tmp, keyCode.ToStringPretty(), keyCode != KeyCode.None);
    }
    
    
}
