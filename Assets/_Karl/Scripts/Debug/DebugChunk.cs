using Enums;
using Enums.PlanetTileMap;
using PlanetTileMap;
using TMPro;
using UnityEngine;

public class DebugChunk : MonoBehaviour
{
    [SerializeField] private int _x;
    [SerializeField] private int _y;
    [SerializeField] private TMP_Text _tmp;

    public void SetChunk(int x, int y, TileProperty tileProperty)
    {
        _x = x;
        _y = y;
        transform.localPosition = new Vector3(0.5f + _x, 0.5f + _y, 0f);
        gameObject.name = $"[{_x}, {_y}]    {nameof(TileID)}[{tileProperty.TileID}]    {nameof(Enums.MaterialType)}[{tileProperty.MaterialType}]    {nameof(TileGeometryAndRotation)}[{tileProperty.BlockShapeType}]";
        
        _tmp.SetText(tileProperty.BlockShapeType.ToString());
    }
}
