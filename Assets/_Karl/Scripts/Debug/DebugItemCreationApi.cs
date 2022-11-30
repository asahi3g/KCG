using Item;
using UnityEngine;

public class DebugItemCreationApi : MonoBehaviour
{
    [SerializeField] private ItemProperties[] PropertiesArray;
    [SerializeField] private FireWeaponProperties[] WeaponList;
    
    void Update()
    {
        PropertiesArray = GameState.ItemCreationApi.GetAll();
        WeaponList = GameState.ItemCreationApi.GetAllWeapons();
    }
}
