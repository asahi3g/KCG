using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 A MANAGE CLASS TO MANAGE SPRITE TILE PROPERTIES
*/
namespace TileProperties
{
    class TilePropertiesManager
    {
        // Tile properties
        public PlanetTileProperties[] TileProperties;

        public static TilePropertiesManager _instance;
        public static TilePropertiesManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TilePropertiesManager();
                return _instance;
            }
        }

        // First of all, we initial core elements to avoid crashes. Because, if we initialize relatives before core, and because realtives uses core 
        public static void InitCore()
        {
            
        }

        // Once we initialize all core elemets, then we can initialize other parts that is use core elements when working
        public static void InitCoreRelatives()
        {
            
        }

        // Reciving information from Tile property with index
        public PlanetTileProperties GetTileProperty(int index) 
        { 
            return TileProperties[index];
        }
    }
}
