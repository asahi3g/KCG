using Enums;
using UnityEngine;

namespace KGUI
{
    public class PotionToolUI : UIPanel
    {
        [SerializeField] private HealthPotionElementUI healthPotionElementUI;

        public override void Update()
        {
            var selectedInventoryItem = GameState.GUIManager.SelectedInventoryItem;
            if (selectedInventoryItem == null) return;

            var planet = GameState.GUIManager.Planet;
            var inventories =
                planet.EntitasContext.inventory.GetGroup(InventoryMatcher.AllOf(InventoryMatcher.InventoryID,
                    InventoryMatcher.InventoryName));

            foreach (var inventory in inventories)
            {
                if (inventory.inventoryName.Name != "MaterialBag") continue;

                var materialBag = planet.EntitasContext.inventory
                    .GetEntityWithInventoryID(inventory.inventoryID.ID).inventoryEntity.Slots;

                for (int i = 0; i < materialBag.Length; i++)
                {
                    var materialBagSlot = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext, inventory.inventoryID.ID, i);
                    if (materialBagSlot == null) continue;

                    healthPotionElementUI.gameObject.SetActive(materialBagSlot.itemType.Type == ItemType.HealthPositon);

                    if (selectedInventoryItem.hasItemPotion)
                    {
                        healthPotionElementUI.Border.SetImageColor(selectedInventoryItem.itemPotion.potionType == PotionType.HealthPotion ? Color.red : Color.yellow);
                    }
                }
            }
        }

        public override void Init()
        {
            ID = UIPanelID.PotionTool;
            
            UIElementList.Add(healthPotionElementUI.ID, healthPotionElementUI);

            base.Init();
        }

        public override void OnDeactivate()
        {
            var selectedInventoryItem = GameState.GUIManager.SelectedInventoryItem;
            selectedInventoryItem.itemPotion.potionType = PotionType.Error;
            
            healthPotionElementUI.Border.SetImageColor(Color.yellow);
        }
    }
}
