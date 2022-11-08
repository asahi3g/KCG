using System.Collections.Generic;
using KGUI;

namespace Prefab
{
    public class PrefabManager
    {
        private readonly Dictionary<PanelEnums, PanelUI> panelPrefabList = new Dictionary<PanelEnums, PanelUI>();

        public void InitializeResources()
        {
            panelPrefabList.Add(PanelEnums.PlayerStatus, UnityEngine.Resources.Load<PanelUI>("GUIPrefabs/PlayerStatusPanel"));
            panelPrefabList.Add(PanelEnums.PlacementTool, UnityEngine.Resources.Load<PanelUI>("GUIPrefabs/PlacementToolPanel"));
            panelPrefabList.Add(PanelEnums.PlacementMaterialTool, UnityEngine.Resources.Load<PanelUI>("GUIPrefabs/PlacementMaterialToolPanel"));
            panelPrefabList.Add(PanelEnums.PotionTool, UnityEngine.Resources.Load<PanelUI>("GUIPrefabs/PotionToolPanel"));
            panelPrefabList.Add(PanelEnums.GeometryTool, UnityEngine.Resources.Load<PanelUI>("GUIPrefabs/GeometryToolPanel"));
            panelPrefabList.Add(PanelEnums.MechTool, UnityEngine.Resources.Load<PanelUI>("GUIPrefabs/MechToolPanel"));
        }

        public PanelUI GetPanelPrefab(PanelEnums panel)
        {
            return panelPrefabList.GetValueOrDefault(panel);
        }
    }
}
