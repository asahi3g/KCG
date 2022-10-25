using Enums;
using Enums.Tile;
using UnityEngine;

namespace KGUI
{
    public class PlacementTool : PanelUI
    {
        [SerializeField] private BedrockElementUI bedrockElementUI;
        [SerializeField] private DirtElementUI dirtElementUI;
        [SerializeField] private PipeElementUI pipeElementUI;
        [SerializeField] private WireElementUI wireElementUI;

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
                ? Color.red
                : Color.yellow);
            dirtElementUI.Border.SetImageColor(selectedInventoryItem.itemTile.TileID == TileID.Moon
                ? Color.red
                : Color.yellow);
            pipeElementUI.Border.SetImageColor(selectedInventoryItem.itemTile.TileID == TileID.Pipe
                ? Color.red
                : Color.yellow);
            wireElementUI.Border.SetImageColor(selectedInventoryItem.itemTile.TileID == TileID.Wire
                ? Color.red
                : Color.yellow);
        }

        public override void OnDeactivate()
        {
            var item = GameState.GUIManager.SelectedInventoryItem;
            if(item != null)
            {
                item.itemTile.TileID = TileID.Error;
            }
            
            bedrockElementUI.Border.SetImageColor(Color.yellow);
            dirtElementUI.Border.SetImageColor(Color.yellow);
            pipeElementUI.Border.SetImageColor(Color.yellow);
            wireElementUI.Border.SetImageColor(Color.yellow);
        }
    }
}
