//imports UnityEngine


using Enums.Tile;

namespace KGUI
{
    public class PlacementTool : PanelUI
    {
        [UnityEngine.SerializeField] private BedrockElementUI bedrockElementUI;
        [UnityEngine.SerializeField] private DirtElementUI dirtElementUI;
        [UnityEngine.SerializeField] private PipeElementUI pipeElementUI;
        [UnityEngine.SerializeField] private WireElementUI wireElementUI;

        public override void Init()
        {
            ID = PanelEnums.PlacementTool;
            
            UIElementList.Add(bedrockElementUI.ID, bedrockElementUI);
            UIElementList.Add(dirtElementUI.ID, dirtElementUI);
            UIElementList.Add(pipeElementUI.ID, pipeElementUI);
            UIElementList.Add(wireElementUI.ID, wireElementUI);

            base.Init();
        }

        public override void OnActivate()
        {
            var selectedInventoryItem = GameState.GUIManager.SelectedInventoryItem;
            if(selectedInventoryItem == null) return;
            
            bedrockElementUI.gameObject.SetActive(true);
            dirtElementUI.gameObject.SetActive(true);
            wireElementUI.gameObject.SetActive(true);
            pipeElementUI.gameObject.SetActive(true);

            // If Selected     = Red
            // If Not Selected = Yellow
            bedrockElementUI.Border.SetImageColor(selectedInventoryItem.itemTile.TileID == TileID.Bedrock
                ? UnityEngine.Color.red
                : UnityEngine.Color.yellow);
            dirtElementUI.Border.SetImageColor(selectedInventoryItem.itemTile.TileID == TileID.Moon
                ? UnityEngine.Color.red
                : UnityEngine.Color.yellow);
            pipeElementUI.Border.SetImageColor(selectedInventoryItem.itemTile.TileID == TileID.Pipe
                ? UnityEngine.Color.red
                : UnityEngine.Color.yellow);
            wireElementUI.Border.SetImageColor(selectedInventoryItem.itemTile.TileID == TileID.Wire
                ? UnityEngine.Color.red
                : UnityEngine.Color.yellow);
        }

        public override void OnDeactivate()
        {
            var item = GameState.GUIManager.SelectedInventoryItem;
            if(item != null)
            {
                item.itemTile.TileID = TileID.Error;
            }
            
            bedrockElementUI.Border.SetImageColor(UnityEngine.Color.yellow);
            dirtElementUI.Border.SetImageColor(UnityEngine.Color.yellow);
            pipeElementUI.Border.SetImageColor(UnityEngine.Color.yellow);
            wireElementUI.Border.SetImageColor(UnityEngine.Color.yellow);
        }
    }
}
