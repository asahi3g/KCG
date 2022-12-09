using System;
using System.Collections.Generic;
using Enums.PlanetTileMap;
using Item;
using UnityEngine;

public class UIViewMapLayerType : UIView
{
    [SerializeField] private UIContent _content;
    [SerializeField] private UIContentSelection _selection;
    [SerializeField] private MapLayerType[] _blacklist;


    protected override void Start()
    {
        base.Start();
        Initialize();
        _selection.onSelectWithPrevious.AddListener(OnSelectionSelect);
    }

    protected override void OnGroupOpened()
    {
        Debug.Log("on opening group");
        // Try auto selecting current layer
        if (GetCurrentItemTile(out TileComponent tileComponent))
        {
            if (GetElementByLayer(tileComponent.Layer, out UIContentElementMapLayerType element))
            {
                _selection.SetSelected(element);
            }
        }
    }

    protected override void OnGroupClosed()
    {
        
    }

    private void Initialize()
    {
        _content.Clear();
        
        MapLayerType[] types = (MapLayerType[]) Enum.GetValues(typeof(MapLayerType));
        int length = types.Length;
        HashSet<MapLayerType> blacklist = new HashSet<MapLayerType>(_blacklist);

        for (int i = 0; i < length; i++)
        {
            MapLayerType type = types[i];
            if(blacklist.Contains(type)) continue;
            UIContentElementMapLayerType element = _content.Create<UIContentElementMapLayerType>();
            element.SetLayer(type);
        }
    }
    
    private void OnSelectionSelect(UIContentElement previous, UIContentElement element)
    {
        if (element == null)
        {
            SetTile(MapLayerType.Error);
            return;
        }
        
        UIContentElementMapLayerType impl = (UIContentElementMapLayerType)element;

        SetTile(impl.GetLayer());

        void SetTile(MapLayerType mapLayerType)
        {
            if (GetCurrentItemTile(out TileComponent tileComponent))
            {
                tileComponent.Layer = mapLayerType;
            }
        }
    }


    private bool GetCurrentItem(out ItemInventoryEntity itemInventoryEntity)
    {
        itemInventoryEntity = null;
        
        if (App.Instance.GetPlayer().GetCurrentPlayerAgent(out AgentRenderer agentRenderer))
        {
            itemInventoryEntity = agentRenderer.GetAgent().GetItem();
        }
        return itemInventoryEntity != null;
    }

    private bool GetCurrentItemTile(out TileComponent tileComponent)
    {
        tileComponent = null;
        
        if (GetCurrentItem(out ItemInventoryEntity currentItem))
        {
            if (currentItem.hasItemTile)
            {
                tileComponent = currentItem.itemTile;
            }
        }

        return tileComponent != null;
    }

    private bool GetElementByLayer(MapLayerType mapLayerType, out UIContentElementMapLayerType element)
    {
        element = null;
        UIContentElementMapLayerType[] elements = _content.GetElements<UIContentElementMapLayerType>(true);
        int length = elements.Length;
        for (int i = 0; i < length; i++)
        {
            UIContentElementMapLayerType e = elements[i];
            if (e.GetLayer() == mapLayerType)
            {
                element = e;
                break;
            }
        }
        return element != null;
    }
}
