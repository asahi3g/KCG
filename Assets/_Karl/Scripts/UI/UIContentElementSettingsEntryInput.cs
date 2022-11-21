using TMPro;
using UnityEngine;

public class UIContentElementSettingsEntryInput : UIContentElementSettingsEntry
{
    [SerializeField] private SOInput _sO;
    [Space]
    [SerializeField] private TMP_Text _tmpTitle;
    [SerializeField] private TMP_Text _tmpPrimary;
    [SerializeField] private TMP_Text _tmpSecondary;

    public int a;

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
        Set(tmp, GetText(keyCode), keyCode != KeyCode.None);
    }
    
    private string GetText(KeyCode keyCode){
        switch(keyCode){
            case KeyCode.Alpha0: return 0.ToString();
            case KeyCode.Alpha1: return 1.ToString();
            case KeyCode.Alpha2: return 2.ToString();
            case KeyCode.Alpha3: return 3.ToString();
            case KeyCode.Alpha4: return 4.ToString();
            case KeyCode.Alpha5: return 5.ToString();
            case KeyCode.Alpha6: return 6.ToString();
            case KeyCode.Alpha7: return 7.ToString();
            case KeyCode.Alpha8: return 8.ToString();
            case KeyCode.Alpha9: return 9.ToString();
            
            case KeyCode.UpArrow: return "Up";
            case KeyCode.DownArrow: return "Down";
            case KeyCode.LeftArrow: return "Left";
            case KeyCode.RightArrow: return "Right";
            
            case KeyCode.Mouse0: return "Right Mouse Button";
            case KeyCode.Mouse1: return "Left Mouse Button";
            case KeyCode.Mouse2: return "Middle Mouse Button";   
        }
        return keyCode.ToString();
    }
}
