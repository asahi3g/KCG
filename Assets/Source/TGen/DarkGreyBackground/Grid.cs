//import UnityEngine

using KMath;

namespace TGen.DarkGreyBackground
{
    public class BackgroundGrid
    {
        public GridTile[,] GridTiles;

        public bool Initialized;

        public void InitStage1(Vec2i mapSize)
        {
            GridTiles = new GridTile[mapSize.X, mapSize.Y];
            Initialized = true;
        }

        public void SetTile(int x, int y, int isotype)
        {
            if(x >= 0 && x < GridTiles.GetLength(0) && y >= 0 && y < GridTiles.GetLength(1))
            {
                GridTiles[x, y].TileIsoType = isotype;
                UnityEngine.Debug.Log(string.Format("Placed tile {0} at ({1}, {2})", ((BlockTypeAndRotation)isotype).ToString(), x.ToString(), y.ToString()));
            }
            else
            {
                UnityEngine.Debug.LogError(string.Format("Position ({0}, {1}) out of range", x, y));
            }
        }

        public void InitStage2()
        {

        }

        public void Update()
        {

        }

        public void Draw()
        {

        }

    }
}
