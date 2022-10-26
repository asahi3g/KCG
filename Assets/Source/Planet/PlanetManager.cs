using System;
using System.IO;
using System.Text;
using Enums.PlanetTileMap;
using KMath;
using PlanetTileMap;

namespace Planet
{



    public class PlanetManager
    {


        public static PlanetState Load(string filePath, int playerPositionX, int playerPositionY)
        {
            PlanetState planet = new PlanetState();
            planet.Init(new Vec2i(0, 0));

            TileMap tileMap;
            using (var stream = File.Open(filePath, FileMode.Open))
            {
                using (var reader = new BinaryReader(stream, Encoding.UTF8, false))
                {
                    int width = reader.ReadInt32();
                    int height = reader.ReadInt32();
                    tileMap = new TileMap(new Vec2i(width, height));

                    for(int y = 0; y < height; y++)
                    {
                        for(int x = 0; x < width; x++)
                        {
                            tileMap.SetBackTile(x, y, (TileID)reader.ReadInt32());
                        }
                    }

                    for(int y = 0; y < height; y++)
                    {
                        for(int x = 0; x < width; x++)
                        {
                            tileMap.SetMidTile(x, y, (TileID)reader.ReadInt32());
                        }
                    }

                    for(int y = 0; y < height; y++)
                    {
                        for(int x = 0; x < width; x++)
                        {
                            tileMap.SetFrontTile(x, y, (TileID)reader.ReadInt32());
                        }
                    }

                    int mechSize = reader.ReadInt32();
                    for(int mechIndex = 0; mechIndex < mechSize; mechIndex++)
                    {
                        int mechType = reader.ReadInt32();
                        float x = (float)reader.ReadDouble();
                        float y = (float)reader.ReadDouble();

                        planet.AddMech(new Vec2f(x, y), (Enums.MechType)mechType);
                    }


                    tileMap.UpdateBackTileMapPositions(playerPositionX, playerPositionY);
                    tileMap.UpdateMidTileMapPositions(playerPositionX, playerPositionY);
                    tileMap.UpdateFrontTileMapPositions(playerPositionX, playerPositionY);

                    planet.TileMap = tileMap;
                }
            }

            return planet;
        }


        public static void Save(PlanetState planet, string filePath)
        {
            TileMap tileMap = planet.TileMap;
            int width = tileMap.MapSize.X;
            int height = tileMap.MapSize.Y;

            

            using (BinaryWriter binWriter =  
                new BinaryWriter(File.Open(filePath, FileMode.Create)))  
            {  
                binWriter.Write(width);
                binWriter.Write(height);

                for(int y = 0; y < height; y++)
                {
                    for(int x = 0; x < width; x++)
                    {
                        binWriter.Write((int)tileMap.GetBackTileID(x, y));
                    }
                }
                for(int y = 0; y < height; y++)
                {
                    for(int x = 0; x < width; x++)
                    {
                        binWriter.Write((int)tileMap.GetMidTileID(x, y));
                    }
                }
                for(int y = 0; y < height; y++)
                {
                    for(int x = 0; x < width; x++)
                    {
                        binWriter.Write((int)tileMap.GetFrontTileID(x, y));
                    }
                }

                binWriter.Write(planet.MechList.Length);
                for(int mechIndex = 0; mechIndex < planet.MechList.Length; mechIndex++)
                {
                    MechEntity mechEntity = planet.MechList.Get(mechIndex);
                    var position = mechEntity.mechPosition2D;
                    var typeComponent = mechEntity.mechType;

                    binWriter.Write((int)typeComponent.mechType);
                    binWriter.Write((double)position.Value.X);
                    binWriter.Write((double)position.Value.Y);
                }
            }  
        }
    }
}