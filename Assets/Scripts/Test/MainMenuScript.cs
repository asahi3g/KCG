using UnityEngine;
using Enums.PlanetTileMap;
using KMath;
using System.Linq;
using System.Collections.Generic;
using System;
using Collisions;
using PlanetTileMap;

class MainMenuScript : MonoBehaviour
{
    [SerializeField] Material Material;

    Gui.GuiElement Root;
    Gui.MainMenuButtonPanel MainMenuButtonPanel;

    public void Start()
    {
        Initialize();
    }

    // create the sprite atlas for testing purposes
    public void Initialize()
    {
        Application.targetFrameRate = 60;

        GameResources.Initialize();

        Root = new Gui.GuiElement(new Vec2f(), new Vec2f(Screen.width, Screen.height));
        MainMenuButtonPanel = new Gui.MainMenuButtonPanel(new Vec2f(700f, 400f));
        Root.AddChild(MainMenuButtonPanel);
        MainMenuButtonPanel.LayoutLeft();
    }

    public void Update()
    {
        Root.Dimensions = new Vec2f(Screen.width, Screen.height);

    }
    Texture2D texture;


    private void OnGUI()
    {
         // check if the sprite atlas teSetTilextures needs to be updated
            for(int type = 0; type < GameState.SpriteAtlasManager.AtlasArray.Length; type++)
            {
                GameState.SpriteAtlasManager.UpdateAtlasTexture(type);
            }

        Root.UpdatePositionAndScale(null);
        Root.Update(null);
        Root.Draw(null);
    }

    private void OnDrawGizmos()
    {
      
    }

}

