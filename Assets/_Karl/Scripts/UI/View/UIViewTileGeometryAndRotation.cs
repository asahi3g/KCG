using System;
using System.Collections.Generic;
using Enums;
using Enums.PlanetTileMap;
using Item;
using UnityEngine;

public class UIViewTileGeometryAndRotation : UIView
{
    [Header(H_A + "Geometry And Rotation" + H_B)]
    [SerializeField] private UIContent _content;
    [SerializeField] private UIContentSelection _selection;
    [SerializeField] private TileGeometryAndRotation[] _blacklist;

    protected override void Start()
    {
        base.Start();
        Initialize();
        _selection.onSelectWithPrevious.AddListener(OnSelectionSelect);
    }

    protected override void OnGroupOpened()
    {
        
    }

    protected override void OnGroupClosed()
    {
        
    }

    private void OnSelectionSelect(UIContentElement previous, UIContentElement element)
    {
        if (element == null)
        {
            _selection.ClearSelection();
            SetTile(TileID.Error);
            return;
        }
        
        UIContentElementTileGeometryAndRotation impl = (UIContentElementTileGeometryAndRotation)element;
        
        // This is hardcode sh*tcode because of TileID - which implements only Metal.
        // Instead tile id should be recognizer by tuple pair (shape + material).
        string tileName = $"{impl.GetTile()}_Metal";
        if (Enum.TryParse<TileID>(tileName, out TileID value))
        {
            SetTile(value);
        }
        else
        {
            Debug.LogWarning($"{nameof(TileID)} with value '{tileName}' does not exist");
        }

        void SetTile(TileID tileID)
        {
            if (App.Instance.GetPlayer().GetCurrentPlayerAgent(out AgentRenderer agentRenderer))
            {
                ItemInventoryEntity currentItem = agentRenderer.GetAgent().GetItem();
                
                if (currentItem != null)
                {
                    if (currentItem.hasItemTile)
                    {
                        Debug.Log($"Setting tile '{tileID}'");
                        currentItem.itemTile.TileID = tileID;
                    }
                }
            }
        }
    }

    private void Initialize()
    {
        _content.Clear();
        
        TileGeometryAndRotation[] types = (TileGeometryAndRotation[]) Enum.GetValues(typeof(TileGeometryAndRotation));
        int length = types.Length;
        HashSet<TileGeometryAndRotation> blacklist = new HashSet<TileGeometryAndRotation>(_blacklist);

        for (int i = 0; i < length; i++)
        {
            TileGeometryAndRotation type = types[i];
            if(blacklist.Contains(type)) continue;
            UIContentElementTileGeometryAndRotation element = _content.Create<UIContentElementTileGeometryAndRotation>();
            element.SetTile(type);
        }

        if (GetCurrentItemTile(out TileComponent tileComponent))
        {
            // TODO: tileId should be tuple data pair shape + material
            /*
            if (GetElementByType(tileComponent.TileID, out UIContentElementTileGeometryAndRotation element))
            {
            
            }
            */
        }
    }
    
    private bool GetElementByType(TileGeometryAndRotation tileGeometryAndRotation, out UIContentElementTileGeometryAndRotation element)
    {
        element = null;
        UIContentElementTileGeometryAndRotation[] elements = _content.GetElements<UIContentElementTileGeometryAndRotation>(true);
        int length = elements.Length;
        for (int i = 0; i < length; i++)
        {
            UIContentElementTileGeometryAndRotation e = elements[i];
            if (e.GetTile() == tileGeometryAndRotation)
            {
                element = e;
                break;
            }
        }
        return element != null;
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
}
