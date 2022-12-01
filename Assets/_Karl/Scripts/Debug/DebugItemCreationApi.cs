using Item;
using UnityEngine;

public class DebugItemCreationApi : DebugBase
{
    [SerializeField] private ItemProperties[] PropertiesArray;
    [SerializeField] private FireWeaponProperties[] WeaponList;
    
    protected override void OnUpdate()
    {
        base.OnUpdate();
        PropertiesArray = GameState.ItemCreationApi.GetAll();
        WeaponList = GameState.ItemCreationApi.GetAllWeapons();
    }
}
