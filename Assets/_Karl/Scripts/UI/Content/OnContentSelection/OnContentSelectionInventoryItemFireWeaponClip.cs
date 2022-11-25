using System;
using Item;
using Item.FireWeapon;
using TMPro;
using UnityEngine;

public class OnContentSelectionInventoryItemFireWeaponClip : OnContentSelectionInventory
{
    [Header(H_A + "Item Fire Weapon Clip" + H_B)]
    [SerializeField] private UIContent _content;
    [SerializeField] private TMP_Text _tmpBullets;
    [SerializeField] private GameObject _reload;
    [SerializeField] private TMP_Text _tmpReload;
    [SerializeField] private SOInput _inputReload;

    // Current target to render
    private EventData _e;

    private class EventData
    {
        public readonly ClipComponent clipComponent;
        public readonly FireWeaponPropreties fireWeaponPropreties;

        public EventData(ClipComponent clipComponent, FireWeaponPropreties fireWeaponPropreties)
        {
            this.clipComponent = clipComponent;
            this.fireWeaponPropreties = fireWeaponPropreties;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        _tmpReload.SetText($"Reload ({DebugExtensions.ToStringPretty(_inputReload.GetPrimary().ToStringPretty())})");
    }

    // We run in update loop because for now there are no events to register to
    private void Update()
    {
        UpdateLook();
    }

    protected override void OnContentSelectionInventoryEvent(UIContentElementInventorySlot previous, UIContentElementInventorySlot element)
    {
        base.OnContentSelectionInventoryEvent(previous, element);
        ClearLook();
        
        if (element == null)
        {
            AlterGroup(false);
            return;
        }

        if (element.GetItem(out ItemInventoryEntity itemInventoryEntity))
        {
            
            if (itemInventoryEntity.hasItemFireWeaponClip)
            {
                ClipComponent clipComponent = itemInventoryEntity.itemFireWeaponClip;
                FireWeaponPropreties fireWeaponPropreties = GameState.ItemCreationApi.GetWeapon(itemInventoryEntity.itemType.Type);

                int length = fireWeaponPropreties.ClipSize;
                for (int i = 0; i < length; i++)
                {
                    UIContentElementWeaponBullet bullet = _content.Create<UIContentElementWeaponBullet>();
                }

                _e = new EventData(clipComponent, fireWeaponPropreties);
                UpdateLook();
                AlterGroup(true);
            }
            else
            {
                AlterGroup(false);
            }
        }
        else
        {
            AlterGroup(false);
        }
    }


    private void ClearLook()
    {
        _e = null;
        _tmpBullets.Clear();
        _content.Clear();
    }

    private void UpdateLook()
    {
        if (_e == null) return;
        int cur = _e.clipComponent.NumOfBullets;
        int max = _e.fireWeaponPropreties.ClipSize;
        
        // Bullet count
        _tmpBullets.SetText($"{cur}/{max}");

        // Toggle bullets
        UIContentElementWeaponBullet[] bullets = _content.GetElements<UIContentElementWeaponBullet>(true);
        int length = bullets.Length;
        for (int i = 0; i < length; i++)
        {
            bullets[i].SetVisibility(i < cur);
        }
        
        // Reload banner
        _reload.SetActive(_e.clipComponent.IsEmpty());
    }
}
