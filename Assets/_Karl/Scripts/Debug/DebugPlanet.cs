using Enums.PlanetTileMap;
using PlanetTileMap;
using UnityEngine;

public class DebugPlanet : BaseMonoBehaviour
{
    [SerializeField] private DebugChunk _debugChunk;
    
    protected override void Start()
    {
        base.Start();
        App.Instance.GetPlayer().onCurrentPlanetChanged.AddListener(OnCurrentPlanetChanged);
    }
    
    private void OnCurrentPlanetChanged(PlanetLoader.Result result)
    {
        DebugTiles(result.GetPlanetState().TileMap);
    }

    private void DebugTiles(PlanetTileMap.TileMap tileMap)
    {
        ClearDebugTiles();
        if (tileMap == null) return;

        Chunk[] chunks = tileMap.ChunkArray;
        int length = chunks.Length;
        
        int y = tileMap.MapSize.Y;
        int x = tileMap.MapSize.X;

        for(int j = 0; j < y; j++)
        {
            for (int i = 0; i < x; i++)
            {
                Enums.PlanetTileMap.TileID tileID = tileMap.GetFrontTileID(i, j);

                if (tileID == TileID.Air)
                {
                    continue;
                }
                
                TileProperty properties = GameState.TileCreationApi.GetTileProperty(tileID);

                DebugChunk debugChunk = Instantiate(_debugChunk, transform, false);
                debugChunk.SetChunk(i, j, properties);
            }
        }
    }
    
    
    private void ClearDebugTiles()
    {
        transform.DestroyChildren();
    }
}
