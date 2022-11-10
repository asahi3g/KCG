using System.Linq;

namespace KGUI
{
    public class GeometryToolPanel : PanelUI
    {
        [UnityEngine.SerializeField] private AvailableMaterialType geometryTileMaterial;
        [UnityEngine.Header("Buttons")]
        [UnityEngine.SerializeField] private UnityEngine.UI.Button changeMaterialButton;
        [UnityEngine.SerializeField] private TMPro.TextMeshProUGUI changeMaterialButtonText;

        enum AvailableMaterialType
        {
            Rock = Enums.MaterialType.Rock,
            Metal = Enums.MaterialType.Metal,
        }
        
        public override void Init()
        {
            ID = PanelEnums.GeometryTool;

            changeMaterialButton.onClick.AddListener(OnChangeMaterialButtonClicked);

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
            if(selectedInventoryItem == null) return;

            foreach (var element in ElementList.Values)
            {
                element.gameObject.SetActive(true);
                ((GeometryTileElement)element).ChangeMaterial((Enums.MaterialType)geometryTileMaterial);
            }
            
            changeMaterialButtonText.text = "Material: " + geometryTileMaterial;
            
            ToggleFirstElement();
        }
        public override void OnDeactivate()
        {
            var item = GameState.GUIManager.SelectedInventoryItem;
            if(item != null)
            {
                item.itemTile.TileID = Enums.PlanetTileMap.TileID.Error;
            }
            
            foreach (var element in ElementList.Values)
            {
                ((IToggleElement)element).Toggle(false);
            }
        }
        
        private void ToggleFirstElement()
        {
            var selectedInventoryItem = GameState.GUIManager.SelectedInventoryItem;
            if (selectedInventoryItem == null) return;
            
            if (ElementList.First().Value is IToggleElement firstElement)
            {
                firstElement.Toggle(true);
                selectedInventoryItem.itemTile.TileID = Enums.PlanetTileMap.TileID.SB_R0_Metal;
            }
        }

        #region ChangeMaterialButton Events

        private void OnChangeMaterialButtonClicked()
        {
            geometryTileMaterial = geometryTileMaterial.Next();

            changeMaterialButtonText.text = "Material: " + geometryTileMaterial;
            
            foreach (var element in ElementList.Values)
            {
                ((GeometryTileElement)element).ChangeMaterial((Enums.MaterialType)geometryTileMaterial);
            }
        }

        public void OnChangeMaterialButtonEntered()
        {
            GameState.GUIManager.SelectedInventoryItem.itemTile.InputsActive = false;
        }
        
        public void OnChangeMaterialButtonExited()
        {
            GameState.GUIManager.SelectedInventoryItem.itemTile.InputsActive = true;
        }

        #endregion

    }
}