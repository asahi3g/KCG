//imports UnityEngine

using System.Linq;
using Enums;

namespace KGUI
{
    public class PotionToolPanel : PanelUI
    {
        [UnityEngine.SerializeField] private HealthPotionElementUI healthPotionElementUI;

        public override void Init()
        {
            ID = PanelEnums.PotionTool;

            base.Init();
        }
        
        public override void HandleClickEvent(ElementEnums elementID)
        {
            foreach (var element in ElementList.Values.Where(element => element.ID != elementID))
            {
                ((IToggleElement)element).Toggle(false);
            }
        }
        
        public override void OnActivate()
        {
            var selectedInventoryItem = GameState.GUIManager.SelectedInventoryItem;
            if (selectedInventoryItem == null) return;
            
            ref var planet = ref GameState.Planet;
            var inventories = planet.EntitasContext.inventory.GetGroup(InventoryMatcher.AllOf(InventoryMatcher.InventoryID, InventoryMatcher.InventoryName));

            foreach (var inventory in inventories)
            {
                if (inventory.inventoryName.Name != "MaterialBag") continue;

                var materialBag = planet.EntitasContext.inventory
                    .GetEntityWithInventoryID(inventory.inventoryID.ID).inventoryEntity.Slots;

                for (int i = 0; i < materialBag.Length; i++)
                {
                    var materialBagSlot = GameState.InventoryManager.GetItemInSlot(inventory.inventoryID.ID, i);
                    if (materialBagSlot == null) continue;

                    if (ElementList.TryGetValue(ElementEnums.Wire, out var healthPotionElementUI))
                    {
                        //healthPotionElementUI.Border.SetImageColor(selectedInventoryItem.itemPotion.potionType == PotionType.HealthPotion ? UnityEngine.Color.red : UnityEngine.Color.yellow);

                        healthPotionElementUI.gameObject.SetActive(materialBagSlot.itemType.Type == ItemType.HealthPositon);
                    }
                }
            }
        }

        public override void OnDeactivate()
        {
            var selectedInventoryItem = GameState.GUIManager.SelectedInventoryItem;
            selectedInventoryItem.itemPotion.potionType = PotionType.Error;
            
            //healthPotionElementUI.Border.SetImageColor(UnityEngine.Color.yellow);

            foreach (var element in ElementList.Values)
            {
                ((IToggleElement)element).Toggle(false);
            }
        }
    }
}
