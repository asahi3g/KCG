using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KMath;
using System.Numerics;
using Unity.VisualScripting;

public class GeometryGUI
{
    public UIElementEntity SQNoSpecular_0Background;
    public UIElementEntity SQNoSpecular_0;
    public UIElementEntity SQ_0Background;
    public UIElementEntity SQ_0;
    public UIElementEntity SQ_1Background;
    public UIElementEntity SQ_1;
    public UIElementEntity SQ_2Background;
    public UIElementEntity SQ_2;
    public UIElementEntity SQ_3Background;
    public UIElementEntity SQ_3;
    public UIElementEntity HSQNoSpecular_0Background;
    public UIElementEntity HSQNoSpecular_0;
    public UIElementEntity HSQNoSpecular_1Background;
    public UIElementEntity HSQNoSpecular_1;
    public UIElementEntity HSQNoSpecular_2Background;
    public UIElementEntity HSQNoSpecular_2;
    public UIElementEntity HSQNoSpecular_3Background;
    public UIElementEntity HSQNoSpecular_3;
    public UIElementEntity HSQ_0Background;
    public UIElementEntity HSQ_0;
    public UIElementEntity HSQ_1Background;
    public UIElementEntity HSQ_1;
    public UIElementEntity HSQ_2Background;
    public UIElementEntity HSQ_2;
    public UIElementEntity HSQ_3Background;
    public UIElementEntity HSQ_3;
    public UIElementEntity SSQ_0Background;
    public UIElementEntity SSQ_0;
    public UIElementEntity SSQ_1Background;
    public UIElementEntity SSQ_1;
    public UIElementEntity SSQ_2Background;
    public UIElementEntity SSQ_2;
    public UIElementEntity SSQ_3Background;
    public UIElementEntity SSQ_3;
    public UIElementEntity TI_0Background;
    public UIElementEntity TI_0;
    public UIElementEntity TI_1Background;
    public UIElementEntity TI_1;
    public UIElementEntity TI_2Background;
    public UIElementEntity TI_2;
    public UIElementEntity TI_3Background;
    public UIElementEntity TI_3;
    public UIElementEntity TO_0Background;
    public UIElementEntity TO_0;
    public UIElementEntity TO_1Background;
    public UIElementEntity TO_1;
    public UIElementEntity TO_2Background;
    public UIElementEntity TO_2;
    public UIElementEntity TO_3Background;
    public UIElementEntity TO_3;
    public UIElementEntity HTD_0Background;
    public UIElementEntity HTD_0;
    public UIElementEntity HTL_1Background;
    public UIElementEntity HTL_1;
    public UIElementEntity HTU_2Background;
    public UIElementEntity HTU_2;
    public UIElementEntity HTR_3Background;
    public UIElementEntity HTR_3;
    public UIElementEntity RHTD_0Background;
    public UIElementEntity RHTD_0;
    public UIElementEntity RHTL_1Background;
    public UIElementEntity RHTL_1;
    public UIElementEntity RHTU_2Background;
    public UIElementEntity RHTU_2;
    public UIElementEntity RHTR_3Background;
    public UIElementEntity RHTR_3;
    public UIElementEntity CSQ_0Background;
    public UIElementEntity CSQ_0;
    public UIElementEntity CSQ_1Background;
    public UIElementEntity CSQ_1;
    public UIElementEntity CSQ_2Background;
    public UIElementEntity CSQ_2;
    public UIElementEntity CSQ_3Background;
    public UIElementEntity CSQ_3;
    public UIElementEntity RCSQ_0Background;
    public UIElementEntity RCSQ_0;
    public UIElementEntity RCSQ_1Background;
    public UIElementEntity RCSQ_1;
    public UIElementEntity RCSQ_2Background;
    public UIElementEntity RCSQ_2;
    public UIElementEntity RCSQ_3Background;
    public UIElementEntity RCSQ_3;

    // Inventory ID
    int inventoryID;

    // Inventory Entity
    InventoryEntity Inventory;

    // Selected Slot
    int selectedSlot;

    // Item Entity
    ItemInventoryEntity item;

    public void Initialize(ref Planet.PlanetState planet)
    {
        int TileSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\GeometryMetal\\metal_tiles_geometry.png", 288, 736);

        int SQNoSpecularID = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(TileSheet, 1, 1, 0);

        // Initialize Pipe Widget
        SQNoSpecular_0Background = planet.AddUIImage("SQNoSpecular_0Background", GameObject.Find("Canvas").transform, "Assets\\StreamingAssets\\Items\\AdminIcon\\Tools\\white_square.png",
            Vec2f.Zero, new Vec3f(0.4f, 0.4f, 0.4f), 225, 225);
        SQNoSpecular_0Background.kGUIElementsImage.Image.SetImageMidBottom();
        SQNoSpecular_0Background.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.3f, 0.3f, 0.3f));
        SQNoSpecular_0Background.kGUIElementsImage.Image.SetPosition(new UnityEngine.Vector3(-330f, -100f));
        SQNoSpecular_0Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        SQNoSpecular_0Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);


        SQNoSpecular_0 = planet.AddUIImage("SQNoSpecular_0", SQNoSpecular_0Background.kGUIElementsImage.Image.GetTransform(), 32, 32, SQNoSpecularID, 0, Vec2f.Zero, new Vec3f(0.8f, 0.8f, 0.8f), Enums.AtlasType.Gui);
        SQNoSpecular_0.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.4f, 0.4f, 0.4f));

        int SQ_0ID = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(TileSheet, 19, 21, 0);

        // Initialize Pipe Widget
        SQ_0Background = planet.AddUIImage("SQ_0Background", GameObject.Find("Canvas").transform, "Assets\\StreamingAssets\\Items\\AdminIcon\\Tools\\white_square.png",
            Vec2f.Zero, new Vec3f(0.4f, 0.4f, 0.4f), 225, 225);
        SQ_0Background.kGUIElementsImage.Image.SetImageMidBottom();
        SQ_0Background.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.3f, 0.3f, 0.3f));
        SQ_0Background.kGUIElementsImage.Image.SetPosition(new UnityEngine.Vector3(-280f, -100f));
        SQ_0Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        SQ_0Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);

        SQ_0 = planet.AddUIImage("SQ_0", SQ_0Background.kGUIElementsImage.Image.GetTransform(), 32, 32, SQ_0ID, 0, Vec2f.Zero, new Vec3f(0.8f, 0.8f, 0.8f), Enums.AtlasType.Gui);
        SQ_0.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.4f, 0.4f, 0.4f));

        int SQ_1ID = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(TileSheet, 21, 21, 0);

        // Initialize Pipe Widget
        SQ_1Background = planet.AddUIImage("SQ_1Background", GameObject.Find("Canvas").transform, "Assets\\StreamingAssets\\Items\\AdminIcon\\Tools\\white_square.png",
            Vec2f.Zero, new Vec3f(0.4f, 0.4f, 0.4f), 225, 225);
        SQ_1Background.kGUIElementsImage.Image.SetImageMidBottom();
        SQ_1Background.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.3f, 0.3f, 0.3f));
        SQ_1Background.kGUIElementsImage.Image.SetPosition(new UnityEngine.Vector3(-280f, -50f));
        SQ_1Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        SQ_1Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);

        SQ_1 = planet.AddUIImage("SQ_1", SQ_1Background.kGUIElementsImage.Image.GetTransform(), 32, 32, SQ_1ID, 0, Vec2f.Zero, new Vec3f(0.8f, 0.8f, 0.8f), Enums.AtlasType.Gui);
        SQ_1.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.4f, 0.4f, 0.4f));

        int SQ_2ID = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(TileSheet, 23, 21, 0);

        // Initialize Pipe Widget
        SQ_2Background = planet.AddUIImage("SQ_2Background", GameObject.Find("Canvas").transform, "Assets\\StreamingAssets\\Items\\AdminIcon\\Tools\\white_square.png",
            Vec2f.Zero, new Vec3f(0.4f, 0.4f, 0.4f), 225, 225);
        SQ_2Background.kGUIElementsImage.Image.SetImageMidBottom();
        SQ_2Background.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.3f, 0.3f, 0.3f));
        SQ_2Background.kGUIElementsImage.Image.SetPosition(new UnityEngine.Vector3(-280f, 0f));
        SQ_2Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        SQ_2Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);

        SQ_2 = planet.AddUIImage("SQ_2", SQ_2Background.kGUIElementsImage.Image.GetTransform(), 32, 32, SQ_2ID, 0, Vec2f.Zero, new Vec3f(0.8f, 0.8f, 0.8f), Enums.AtlasType.Gui);
        SQ_2.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.4f, 0.4f, 0.4f));

        int SQ_3ID = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(TileSheet, 25, 21, 0);

        // Initialize Pipe Widget
        SQ_3Background = planet.AddUIImage("SQ_3Background", GameObject.Find("Canvas").transform, "Assets\\StreamingAssets\\Items\\AdminIcon\\Tools\\white_square.png",
            Vec2f.Zero, new Vec3f(0.4f, 0.4f, 0.4f), 225, 225);
        SQ_3Background.kGUIElementsImage.Image.SetImageMidBottom();
        SQ_3Background.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.3f, 0.3f, 0.3f));
        SQ_3Background.kGUIElementsImage.Image.SetPosition(new UnityEngine.Vector3(-280f, 50f));
        SQ_3Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        SQ_3Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);

        SQ_3 = planet.AddUIImage("SQ_3", SQ_3Background.kGUIElementsImage.Image.GetTransform(), 32, 32, SQ_3ID, 0, Vec2f.Zero, new Vec3f(0.8f, 0.8f, 0.8f), Enums.AtlasType.Gui);
        SQ_3.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.4f, 0.4f, 0.4f));

        int HSQNoSpecular_0ID = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(TileSheet, 1, 3, 0);

        // Initialize Pipe Widget
        HSQNoSpecular_0Background = planet.AddUIImage("HSQNoSpecular_0Background", GameObject.Find("Canvas").transform, "Assets\\StreamingAssets\\Items\\AdminIcon\\Tools\\white_square.png",
            Vec2f.Zero, new Vec3f(0.4f, 0.4f, 0.4f), 225, 225);
        HSQNoSpecular_0Background.kGUIElementsImage.Image.SetImageMidBottom();
        HSQNoSpecular_0Background.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.3f, 0.3f, 0.3f));
        HSQNoSpecular_0Background.kGUIElementsImage.Image.SetPosition(new UnityEngine.Vector3(-230f, -100f));
        HSQNoSpecular_0Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        HSQNoSpecular_0Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);

        HSQNoSpecular_0 = planet.AddUIImage("HSQNoSpecular_0", HSQNoSpecular_0Background.kGUIElementsImage.Image.GetTransform(), 32, 32, HSQNoSpecular_0ID, 0, Vec2f.Zero, new Vec3f(0.8f, 0.8f, 0.8f), Enums.AtlasType.Gui);
        HSQNoSpecular_0.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.4f, 0.4f, 0.4f));

        int HSQNoSpecular_1ID = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(TileSheet, 3, 3, 0);

        // Initialize Pipe Widget
        HSQNoSpecular_1Background = planet.AddUIImage("HSQNoSpecular_1Background", GameObject.Find("Canvas").transform, "Assets\\StreamingAssets\\Items\\AdminIcon\\Tools\\white_square.png",
            Vec2f.Zero, new Vec3f(0.4f, 0.4f, 0.4f), 225, 225);
        HSQNoSpecular_1Background.kGUIElementsImage.Image.SetImageMidBottom();
        HSQNoSpecular_1Background.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.3f, 0.3f, 0.3f));
        HSQNoSpecular_1Background.kGUIElementsImage.Image.SetPosition(new UnityEngine.Vector3(-230f, -50f));
        HSQNoSpecular_1Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        HSQNoSpecular_1Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);

        HSQNoSpecular_1 = planet.AddUIImage("HSQNoSpecular_1", HSQNoSpecular_1Background.kGUIElementsImage.Image.GetTransform(), 32, 32, HSQNoSpecular_1ID, 0, Vec2f.Zero, new Vec3f(0.8f, 0.8f, 0.8f), Enums.AtlasType.Gui);
        HSQNoSpecular_1.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.4f, 0.4f, 0.4f));

        int HSQNoSpecular_2ID = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(TileSheet, 5, 3, 0);

        // Initialize Pipe Widget
        HSQNoSpecular_2Background = planet.AddUIImage("HSQNoSpecular_2Background", GameObject.Find("Canvas").transform, "Assets\\StreamingAssets\\Items\\AdminIcon\\Tools\\white_square.png",
            Vec2f.Zero, new Vec3f(0.4f, 0.4f, 0.4f), 225, 225);
        HSQNoSpecular_2Background.kGUIElementsImage.Image.SetImageMidBottom();
        HSQNoSpecular_2Background.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.3f, 0.3f, 0.3f));
        HSQNoSpecular_2Background.kGUIElementsImage.Image.SetPosition(new UnityEngine.Vector3(-230f, 0f));
        HSQNoSpecular_2Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        HSQNoSpecular_2Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);

        HSQNoSpecular_2 = planet.AddUIImage("HSQNoSpecular_2", HSQNoSpecular_2Background.kGUIElementsImage.Image.GetTransform(), 32, 32, HSQNoSpecular_2ID, 0, Vec2f.Zero, new Vec3f(0.8f, 0.8f, 0.8f), Enums.AtlasType.Gui);
        HSQNoSpecular_2.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.4f, 0.4f, 0.4f));

        int HSQNoSpecular_3ID = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(TileSheet, 7, 3, 0);

        // Initialize Pipe Widget
        HSQNoSpecular_3Background = planet.AddUIImage("HSQNoSpecular_3Background", GameObject.Find("Canvas").transform, "Assets\\StreamingAssets\\Items\\AdminIcon\\Tools\\white_square.png",
            Vec2f.Zero, new Vec3f(0.4f, 0.4f, 0.4f), 225, 225);
        HSQNoSpecular_3Background.kGUIElementsImage.Image.SetImageMidBottom();
        HSQNoSpecular_3Background.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.3f, 0.3f, 0.3f));
        HSQNoSpecular_3Background.kGUIElementsImage.Image.SetPosition(new UnityEngine.Vector3(-230f, 50f));
        HSQNoSpecular_3Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        HSQNoSpecular_3Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);

        HSQNoSpecular_3 = planet.AddUIImage("HSQNoSpecular_3", HSQNoSpecular_3Background.kGUIElementsImage.Image.GetTransform(), 32, 32, HSQNoSpecular_3ID, 0, Vec2f.Zero, new Vec3f(0.8f, 0.8f, 0.8f), Enums.AtlasType.Gui);
        HSQNoSpecular_3.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.4f, 0.4f, 0.4f));

        int HSQ_0ID = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(TileSheet, 19, 19, 0);

        // Initialize Pipe Widget
        HSQ_0Background = planet.AddUIImage("HSQ_0Background", GameObject.Find("Canvas").transform, "Assets\\StreamingAssets\\Items\\AdminIcon\\Tools\\white_square.png",
            Vec2f.Zero, new Vec3f(0.4f, 0.4f, 0.4f), 225, 225);
        HSQ_0Background.kGUIElementsImage.Image.SetImageMidBottom();
        HSQ_0Background.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.3f, 0.3f, 0.3f));
        HSQ_0Background.kGUIElementsImage.Image.SetPosition(new UnityEngine.Vector3(-180f, -100f));
        HSQ_0Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        HSQ_0Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);

        HSQ_0 = planet.AddUIImage("HSQ_0", HSQ_0Background.kGUIElementsImage.Image.GetTransform(), 32, 32, HSQ_0ID, 0, Vec2f.Zero, new Vec3f(0.8f, 0.8f, 0.8f), Enums.AtlasType.Gui);
        HSQ_0.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.4f, 0.4f, 0.4f));

        int HSQ_1ID = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(TileSheet, 21, 19, 0);

        // Initialize Pipe Widget
        HSQ_1Background = planet.AddUIImage("HSQ_1Background", GameObject.Find("Canvas").transform, "Assets\\StreamingAssets\\Items\\AdminIcon\\Tools\\white_square.png",
            Vec2f.Zero, new Vec3f(0.4f, 0.4f, 0.4f), 225, 225);
        HSQ_1Background.kGUIElementsImage.Image.SetImageMidBottom();
        HSQ_1Background.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.3f, 0.3f, 0.3f));
        HSQ_1Background.kGUIElementsImage.Image.SetPosition(new UnityEngine.Vector3(-180f, -50f));
        HSQ_1Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        HSQ_1Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);


        HSQ_1 = planet.AddUIImage("HSQ_1", HSQ_1Background.kGUIElementsImage.Image.GetTransform(), 32, 32, HSQ_1ID, 0, Vec2f.Zero, new Vec3f(0.8f, 0.8f, 0.8f), Enums.AtlasType.Gui);
        HSQ_1.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.4f, 0.4f, 0.4f));

        int HSQ_2ID = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(TileSheet, 23, 19, 0);

        // Initialize Pipe Widget
        HSQ_2Background = planet.AddUIImage("HSQ_2Background", GameObject.Find("Canvas").transform, "Assets\\StreamingAssets\\Items\\AdminIcon\\Tools\\white_square.png",
            Vec2f.Zero, new Vec3f(0.4f, 0.4f, 0.4f), 225, 225);
        HSQ_2Background.kGUIElementsImage.Image.SetImageMidBottom();
        HSQ_2Background.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.3f, 0.3f, 0.3f));
        HSQ_2Background.kGUIElementsImage.Image.SetPosition(new UnityEngine.Vector3(-180f, 0f));
        HSQ_2Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        HSQ_2Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);


        HSQ_2 = planet.AddUIImage("HSQ_2", HSQ_2Background.kGUIElementsImage.Image.GetTransform(), 32, 32, HSQ_2ID, 0, Vec2f.Zero, new Vec3f(0.8f, 0.8f, 0.8f), Enums.AtlasType.Gui);
        HSQ_2.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.4f, 0.4f, 0.4f));

        int HSQ_3ID = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(TileSheet, 25, 19, 0);

        // Initialize Pipe Widget
        HSQ_3Background = planet.AddUIImage("HSQ_3Background", GameObject.Find("Canvas").transform, "Assets\\StreamingAssets\\Items\\AdminIcon\\Tools\\white_square.png",
            Vec2f.Zero, new Vec3f(0.4f, 0.4f, 0.4f), 225, 225);
        HSQ_3Background.kGUIElementsImage.Image.SetImageMidBottom();
        HSQ_3Background.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.3f, 0.3f, 0.3f));
        HSQ_3Background.kGUIElementsImage.Image.SetPosition(new UnityEngine.Vector3(-180f, 50f));
        HSQ_3Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        HSQ_3Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);


        HSQ_3 = planet.AddUIImage("HSQ_3", HSQ_3Background.kGUIElementsImage.Image.GetTransform(), 32, 32, HSQ_3ID, 0, Vec2f.Zero, new Vec3f(0.8f, 0.8f, 0.8f), Enums.AtlasType.Gui);
        HSQ_3.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.4f, 0.4f, 0.4f));

        int SSQ_0ID = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(TileSheet, 10, 17, 0);

        // Initialize Pipe Widget
        SSQ_0Background = planet.AddUIImage("SSQ_0Background", GameObject.Find("Canvas").transform, "Assets\\StreamingAssets\\Items\\AdminIcon\\Tools\\white_square.png",
            Vec2f.Zero, new Vec3f(0.4f, 0.4f, 0.4f), 225, 225);
        SSQ_0Background.kGUIElementsImage.Image.SetImageMidBottom();
        SSQ_0Background.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.3f, 0.3f, 0.3f));
        SSQ_0Background.kGUIElementsImage.Image.SetPosition(new UnityEngine.Vector3(-130f, -100f));
        SSQ_0Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        SSQ_0Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);


        SSQ_0 = planet.AddUIImage("SSQ_0", SSQ_0Background.kGUIElementsImage.Image.GetTransform(), 32, 32, SSQ_0ID, 0, Vec2f.Zero, new Vec3f(0.8f, 0.8f, 0.8f), Enums.AtlasType.Gui);
        SSQ_0.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.4f, 0.4f, 0.4f));

        int SSQ_1ID = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(TileSheet, 12, 17, 0);

        // Initialize Pipe Widget
        SSQ_1Background = planet.AddUIImage("SSQ_1Background", GameObject.Find("Canvas").transform, "Assets\\StreamingAssets\\Items\\AdminIcon\\Tools\\white_square.png",
            Vec2f.Zero, new Vec3f(0.4f, 0.4f, 0.4f), 225, 225);
        SSQ_1Background.kGUIElementsImage.Image.SetImageMidBottom();
        SSQ_1Background.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.3f, 0.3f, 0.3f));
        SSQ_1Background.kGUIElementsImage.Image.SetPosition(new UnityEngine.Vector3(-130f, -50f));
        SSQ_1Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        SSQ_1Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);


        SSQ_1 = planet.AddUIImage("SSQ_1", SSQ_1Background.kGUIElementsImage.Image.GetTransform(), 32, 32, SSQ_1ID, 0, Vec2f.Zero, new Vec3f(0.8f, 0.8f, 0.8f), Enums.AtlasType.Gui);
        SSQ_1.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.4f, 0.4f, 0.4f));

        int SSQ_2ID = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(TileSheet, 14, 17, 0);

        // Initialize Pipe Widget
        SSQ_2Background = planet.AddUIImage("SSQ_2Background", GameObject.Find("Canvas").transform, "Assets\\StreamingAssets\\Items\\AdminIcon\\Tools\\white_square.png",
            Vec2f.Zero, new Vec3f(0.4f, 0.4f, 0.4f), 225, 225);
        SSQ_2Background.kGUIElementsImage.Image.SetImageMidBottom();
        SSQ_2Background.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.3f, 0.3f, 0.3f));
        SSQ_2Background.kGUIElementsImage.Image.SetPosition(new UnityEngine.Vector3(-130f, 0f));
        SSQ_2Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        SSQ_2Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);


        SSQ_2 = planet.AddUIImage("SSQ_2", SSQ_2Background.kGUIElementsImage.Image.GetTransform(), 32, 32, SSQ_2ID, 0, Vec2f.Zero, new Vec3f(0.8f, 0.8f, 0.8f), Enums.AtlasType.Gui);
        SSQ_2.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.4f, 0.4f, 0.4f));

        int SSQ_3ID = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(TileSheet, 16, 17, 0);

        // Initialize Pipe Widget
        SSQ_3Background = planet.AddUIImage("SSQ_3Background", GameObject.Find("Canvas").transform, "Assets\\StreamingAssets\\Items\\AdminIcon\\Tools\\white_square.png",
            Vec2f.Zero, new Vec3f(0.4f, 0.4f, 0.4f), 225, 225);
        SSQ_3Background.kGUIElementsImage.Image.SetImageMidBottom();
        SSQ_3Background.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.3f, 0.3f, 0.3f));
        SSQ_3Background.kGUIElementsImage.Image.SetPosition(new UnityEngine.Vector3(-130f, 50f));
        SSQ_3Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        SSQ_3Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);


        SSQ_3 = planet.AddUIImage("SSQ_3", SSQ_3Background.kGUIElementsImage.Image.GetTransform(), 32, 32, SSQ_3ID, 0, Vec2f.Zero, new Vec3f(0.8f, 0.8f, 0.8f), Enums.AtlasType.Gui);
        SSQ_3.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.4f, 0.4f, 0.4f));

        int TI_0ID = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(TileSheet, 1, 5, 0);

        // Initialize Pipe Widget
        TI_0Background = planet.AddUIImage("TI_0Background", GameObject.Find("Canvas").transform, "Assets\\StreamingAssets\\Items\\AdminIcon\\Tools\\white_square.png",
            Vec2f.Zero, new Vec3f(0.4f, 0.4f, 0.4f), 225, 225);
        TI_0Background.kGUIElementsImage.Image.SetImageMidBottom();
        TI_0Background.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.3f, 0.3f, 0.3f));
        TI_0Background.kGUIElementsImage.Image.SetPosition(new UnityEngine.Vector3(-80f, -100f));
        TI_0Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        TI_0Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);


        TI_0 = planet.AddUIImage("TI_0", TI_0Background.kGUIElementsImage.Image.GetTransform(), 32, 32, TI_0ID, 0, Vec2f.Zero, new Vec3f(0.8f, 0.8f, 0.8f), Enums.AtlasType.Gui);
        TI_0.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.4f, 0.4f, 0.4f));

        int TI_1ID = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(TileSheet, 3, 5, 0);

        // Initialize Pipe Widget
        TI_1Background = planet.AddUIImage("TI_1Background", GameObject.Find("Canvas").transform, "Assets\\StreamingAssets\\Items\\AdminIcon\\Tools\\white_square.png",
            Vec2f.Zero, new Vec3f(0.4f, 0.4f, 0.4f), 225, 225);
        TI_1Background.kGUIElementsImage.Image.SetImageMidBottom();
        TI_1Background.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.3f, 0.3f, 0.3f));
        TI_1Background.kGUIElementsImage.Image.SetPosition(new UnityEngine.Vector3(-80f, -50f));
        TI_1Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        TI_1Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);


        TI_1 = planet.AddUIImage("TI_1", TI_1Background.kGUIElementsImage.Image.GetTransform(), 32, 32, TI_1ID, 0, Vec2f.Zero, new Vec3f(0.8f, 0.8f, 0.8f), Enums.AtlasType.Gui);
        TI_1.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.4f, 0.4f, 0.4f));

        int TI_2ID = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(TileSheet, 1, 7, 0);

        // Initialize Pipe Widget
        TI_2Background = planet.AddUIImage("TI_2Background", GameObject.Find("Canvas").transform, "Assets\\StreamingAssets\\Items\\AdminIcon\\Tools\\white_square.png",
            Vec2f.Zero, new Vec3f(0.4f, 0.4f, 0.4f), 225, 225);
        TI_2Background.kGUIElementsImage.Image.SetImageMidBottom();
        TI_2Background.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.3f, 0.3f, 0.3f));
        TI_2Background.kGUIElementsImage.Image.SetPosition(new UnityEngine.Vector3(-80f, 0f));
        TI_2Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        TI_2Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);


        TI_2 = planet.AddUIImage("TI_2", TI_2Background.kGUIElementsImage.Image.GetTransform(), 32, 32, TI_2ID, 0, Vec2f.Zero, new Vec3f(0.8f, 0.8f, 0.8f), Enums.AtlasType.Gui);
        TI_2.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.4f, 0.4f, 0.4f));

        int TI_3ID = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(TileSheet, 3, 7, 0);

        // Initialize Pipe Widget
        TI_3Background = planet.AddUIImage("TI_3Background", GameObject.Find("Canvas").transform, "Assets\\StreamingAssets\\Items\\AdminIcon\\Tools\\white_square.png", Vec2f.Zero, new Vec3f(0.4f, 0.4f, 0.4f), 225, 225);

        TI_3Background.kGUIElementsImage.Image.SetImageMidBottom();
        TI_3Background.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.3f, 0.3f, 0.3f));
        TI_3Background.kGUIElementsImage.Image.SetPosition(new UnityEngine.Vector3(-80f, 50f));
        TI_3Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        TI_3Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);


        TI_3 = planet.AddUIImage("TI_3", TI_3Background.kGUIElementsImage.Image.GetTransform(), 32, 32, TI_3ID, 0, Vec2f.Zero, new Vec3f(0.8f, 0.8f, 0.8f), Enums.AtlasType.Gui);
        TI_3.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.4f, 0.4f, 0.4f));

        int TO_0ID = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(TileSheet, 5, 5, 0);

        // Initialize Pipe Widget
        TO_0Background = planet.AddUIImage("TO_0Background", GameObject.Find("Canvas").transform, "Assets\\StreamingAssets\\Items\\AdminIcon\\Tools\\white_square.png", Vec2f.Zero, new Vec3f(0.4f, 0.4f, 0.4f), 225, 225);

        TO_0Background.kGUIElementsImage.Image.SetImageMidBottom();
        TO_0Background.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.3f, 0.3f, 0.3f));
        TO_0Background.kGUIElementsImage.Image.SetPosition(new UnityEngine.Vector3(-30f, -100f));
        TO_0Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        TO_0Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);


        TO_0 = planet.AddUIImage("TO_0", TO_0Background.kGUIElementsImage.Image.GetTransform(), 32, 32, TO_0ID, 0, Vec2f.Zero, new Vec3f(0.8f, 0.8f, 0.8f), Enums.AtlasType.Gui);
        TO_0.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.4f, 0.4f, 0.4f));

        int TO_1ID = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(TileSheet, 7, 5, 0);

        // Initialize Pipe Widget
        TO_1Background = planet.AddUIImage("TO_1Background", GameObject.Find("Canvas").transform, "Assets\\StreamingAssets\\Items\\AdminIcon\\Tools\\white_square.png", Vec2f.Zero, new Vec3f(0.4f, 0.4f, 0.4f), 225, 225);

        TO_1Background.kGUIElementsImage.Image.SetImageMidBottom();
        TO_1Background.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.3f, 0.3f, 0.3f));
        TO_1Background.kGUIElementsImage.Image.SetPosition(new UnityEngine.Vector3(-30f, -50f));
        TO_1Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        TO_1Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);


        TO_1 = planet.AddUIImage("TO_1", TO_1Background.kGUIElementsImage.Image.GetTransform(), 32, 32, TO_1ID, 0, Vec2f.Zero, new Vec3f(0.8f, 0.8f, 0.8f), Enums.AtlasType.Gui);
        TO_1.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.4f, 0.4f, 0.4f));

        int TO_2ID = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(TileSheet, 5, 7, 0);

        // Initialize Pipe Widget
        TO_2Background = planet.AddUIImage("TO_2Background", GameObject.Find("Canvas").transform, "Assets\\StreamingAssets\\Items\\AdminIcon\\Tools\\white_square.png", Vec2f.Zero, new Vec3f(0.4f, 0.4f, 0.4f), 225, 225);

        TO_2Background.kGUIElementsImage.Image.SetImageMidBottom();
        TO_2Background.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.3f, 0.3f, 0.3f));
        TO_2Background.kGUIElementsImage.Image.SetPosition(new UnityEngine.Vector3(-30f, 0f));
        TO_2Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        TO_2Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);


        TO_2 = planet.AddUIImage("TO_2", TO_2Background.kGUIElementsImage.Image.GetTransform(), 32, 32, TO_2ID, 0, Vec2f.Zero, new Vec3f(0.8f, 0.8f, 0.8f), Enums.AtlasType.Gui);
        TO_2.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.4f, 0.4f, 0.4f));

        int TO_3ID = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(TileSheet, 7, 7, 0);

        // Initialize Pipe Widget
        TO_3Background = planet.AddUIImage("TO_3Background", GameObject.Find("Canvas").transform, "Assets\\StreamingAssets\\Items\\AdminIcon\\Tools\\white_square.png", Vec2f.Zero, new Vec3f(0.4f, 0.4f, 0.4f), 225, 225);

        TO_3Background.kGUIElementsImage.Image.SetImageMidBottom();
        TO_3Background.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.3f, 0.3f, 0.3f));
        TO_3Background.kGUIElementsImage.Image.SetPosition(new UnityEngine.Vector3(-30f, 50f));
        TO_3Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        TO_3Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);


        TO_3 = planet.AddUIImage("TO_3", TO_3Background.kGUIElementsImage.Image.GetTransform(), 32, 32, TO_3ID, 0, Vec2f.Zero, new Vec3f(0.8f, 0.8f, 0.8f), Enums.AtlasType.Gui);
        TO_3.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.4f, 0.4f, 0.4f));

        int HTD_0ID = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(TileSheet, 1, 9, 0);

        // Initialize Pipe Widget
        HTD_0Background = planet.AddUIImage("HTD_0Background", GameObject.Find("Canvas").transform, "Assets\\StreamingAssets\\Items\\AdminIcon\\Tools\\white_square.png", Vec2f.Zero, new Vec3f(0.4f, 0.4f, 0.4f), 225, 225);

        HTD_0Background.kGUIElementsImage.Image.SetImageMidBottom();
        HTD_0Background.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.3f, 0.3f, 0.3f));
        HTD_0Background.kGUIElementsImage.Image.SetPosition(new UnityEngine.Vector3(20f, -100f));
        HTD_0Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        HTD_0Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);


        HTD_0 = planet.AddUIImage("HTD_0", HTD_0Background.kGUIElementsImage.Image.GetTransform(), 32, 32, HTD_0ID, 0, Vec2f.Zero, new Vec3f(0.8f, 0.8f, 0.8f), Enums.AtlasType.Gui);
        HTD_0.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.4f, 0.4f, 0.4f));

        int HTL_1ID = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(TileSheet, 3, 9, 0);

        // Initialize Pipe Widget
        HTL_1Background = planet.AddUIImage("HTL_1Background", GameObject.Find("Canvas").transform, "Assets\\StreamingAssets\\Items\\AdminIcon\\Tools\\white_square.png", Vec2f.Zero, new Vec3f(0.4f, 0.4f, 0.4f), 225, 225);

        HTL_1Background.kGUIElementsImage.Image.SetImageMidBottom();
        HTL_1Background.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.3f, 0.3f, 0.3f));
        HTL_1Background.kGUIElementsImage.Image.SetPosition(new UnityEngine.Vector3(20f, -50f));
        HTL_1Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        HTL_1Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);


        HTL_1 = planet.AddUIImage("HTL_1", HTL_1Background.kGUIElementsImage.Image.GetTransform(), 32, 32, HTL_1ID, 0, Vec2f.Zero, new Vec3f(0.8f, 0.8f, 0.8f), Enums.AtlasType.Gui);
        HTL_1.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.4f, 0.4f, 0.4f));

        int HTU_2ID = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(TileSheet, 1, 11, 0);

        // Initialize Pipe Widget
        HTU_2Background = planet.AddUIImage("HTU_2Background", GameObject.Find("Canvas").transform, "Assets\\StreamingAssets\\Items\\AdminIcon\\Tools\\white_square.png", Vec2f.Zero, new Vec3f(0.4f, 0.4f, 0.4f), 225, 225);

        HTU_2Background.kGUIElementsImage.Image.SetImageMidBottom();
        HTU_2Background.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.3f, 0.3f, 0.3f));
        HTU_2Background.kGUIElementsImage.Image.SetPosition(new UnityEngine.Vector3(20f, 0f));
        HTU_2Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        HTU_2Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);


        HTU_2 = planet.AddUIImage("HTU_2", HTU_2Background.kGUIElementsImage.Image.GetTransform(), 32, 32, HTU_2ID, 0, Vec2f.Zero, new Vec3f(0.8f, 0.8f, 0.8f), Enums.AtlasType.Gui);
        HTU_2.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.4f, 0.4f, 0.4f));

        int HTR_3ID = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(TileSheet, 3, 11, 0);

        // Initialize Pipe Widget
        HTR_3Background = planet.AddUIImage("HTR_3Background", GameObject.Find("Canvas").transform, "Assets\\StreamingAssets\\Items\\AdminIcon\\Tools\\white_square.png", Vec2f.Zero, new Vec3f(0.4f, 0.4f, 0.4f), 225, 225);

        HTR_3Background.kGUIElementsImage.Image.SetImageMidBottom();
        HTR_3Background.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.3f, 0.3f, 0.3f));
        HTR_3Background.kGUIElementsImage.Image.SetPosition(new UnityEngine.Vector3(20f, 50f));
        HTR_3Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        HTR_3Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);


        HTR_3 = planet.AddUIImage("HTR_3", HTR_3Background.kGUIElementsImage.Image.GetTransform(), 32, 32, HTR_3ID, 0, Vec2f.Zero, new Vec3f(0.8f, 0.8f, 0.8f), Enums.AtlasType.Gui);
        HTR_3.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.4f, 0.4f, 0.4f));

        int RHTD_0ID = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(TileSheet, 5, 11, 0);

        // Initialize Pipe Widget
        RHTD_0Background = planet.AddUIImage("RHTD_0Background", GameObject.Find("Canvas").transform, "Assets\\StreamingAssets\\Items\\AdminIcon\\Tools\\white_square.png", Vec2f.Zero, new Vec3f(0.4f, 0.4f, 0.4f), 225, 225);

        RHTD_0Background.kGUIElementsImage.Image.SetImageMidBottom();
        RHTD_0Background.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.3f, 0.3f, 0.3f));
        RHTD_0Background.kGUIElementsImage.Image.SetPosition(new UnityEngine.Vector3(70f, -100f));
        RHTD_0Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        RHTD_0Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);


        RHTD_0 = planet.AddUIImage("RHTD_0", RHTD_0Background.kGUIElementsImage.Image.GetTransform(), 32, 32, RHTD_0ID, 0, Vec2f.Zero, new Vec3f(0.8f, 0.8f, 0.8f), Enums.AtlasType.Gui);
        RHTD_0.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.4f, 0.4f, 0.4f));

        int RHTL_1ID = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(TileSheet, 7, 11, 0);

        // Initialize Pipe Widget
        RHTL_1Background = planet.AddUIImage("RHTL_1Background", GameObject.Find("Canvas").transform, "Assets\\StreamingAssets\\Items\\AdminIcon\\Tools\\white_square.png", Vec2f.Zero, new Vec3f(0.4f, 0.4f, 0.4f), 225, 225);

        RHTL_1Background.kGUIElementsImage.Image.SetImageMidBottom();
        RHTL_1Background.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.3f, 0.3f, 0.3f));
        RHTL_1Background.kGUIElementsImage.Image.SetPosition(new UnityEngine.Vector3(70f, -50f));
        RHTL_1Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        RHTL_1Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);


        RHTL_1 = planet.AddUIImage("RHTL_1", RHTL_1Background.kGUIElementsImage.Image.GetTransform(), 32, 32, RHTL_1ID, 0, Vec2f.Zero, new Vec3f(0.8f, 0.8f, 0.8f), Enums.AtlasType.Gui);
        RHTL_1.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.4f, 0.4f, 0.4f));

        int RHTU_2ID = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(TileSheet, 5, 9, 0);

        // Initialize Pipe Widget
        RHTU_2Background = planet.AddUIImage("RHTU_2Background", GameObject.Find("Canvas").transform, "Assets\\StreamingAssets\\Items\\AdminIcon\\Tools\\white_square.png", Vec2f.Zero, new Vec3f(0.4f, 0.4f, 0.4f), 225, 225);

        RHTU_2Background.kGUIElementsImage.Image.SetImageMidBottom();
        RHTU_2Background.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.3f, 0.3f, 0.3f));
        RHTU_2Background.kGUIElementsImage.Image.SetPosition(new UnityEngine.Vector3(70f, 0f));
        RHTU_2Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        RHTU_2Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);


        RHTU_2 = planet.AddUIImage("RHTU_2", RHTU_2Background.kGUIElementsImage.Image.GetTransform(), 32, 32, RHTU_2ID, 0, Vec2f.Zero, new Vec3f(0.8f, 0.8f, 0.8f), Enums.AtlasType.Gui);
        RHTU_2.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.4f, 0.4f, 0.4f));

        int RHTR_3ID = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(TileSheet, 7, 9, 0);

        // Initialize Pipe Widget
        RHTR_3Background = planet.AddUIImage("RHTR_3Background", GameObject.Find("Canvas").transform, "Assets\\StreamingAssets\\Items\\AdminIcon\\Tools\\white_square.png", Vec2f.Zero, new Vec3f(0.4f, 0.4f, 0.4f), 225, 225);

        RHTR_3Background.kGUIElementsImage.Image.SetImageMidBottom();
        RHTR_3Background.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.3f, 0.3f, 0.3f));
        RHTR_3Background.kGUIElementsImage.Image.SetPosition(new UnityEngine.Vector3(70f, 50f));
        RHTR_3Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        RHTR_3Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);


        RHTR_3 = planet.AddUIImage("RHTR_3", RHTR_3Background.kGUIElementsImage.Image.GetTransform(), 32, 32, RHTR_3ID, 0, Vec2f.Zero, new Vec3f(0.8f, 0.8f, 0.8f), Enums.AtlasType.Gui);
        RHTR_3.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.4f, 0.4f, 0.4f));

        int CSQ_0ID = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(TileSheet, 1, 13, 0);

        // Initialize Pipe Widget
        CSQ_0Background = planet.AddUIImage("CSQ_0Background", GameObject.Find("Canvas").transform, "Assets\\StreamingAssets\\Items\\AdminIcon\\Tools\\white_square.png", Vec2f.Zero, new Vec3f(0.4f, 0.4f, 0.4f), 225, 225);

        CSQ_0Background.kGUIElementsImage.Image.SetImageMidBottom();
        CSQ_0Background.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.3f, 0.3f, 0.3f));
        CSQ_0Background.kGUIElementsImage.Image.SetPosition(new UnityEngine.Vector3(120f, -100f));
        CSQ_0Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        CSQ_0Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);


        CSQ_0 = planet.AddUIImage("CSQ_0", CSQ_0Background.kGUIElementsImage.Image.GetTransform(), 32, 32, CSQ_0ID, 0, Vec2f.Zero, new Vec3f(0.8f, 0.8f, 0.8f), Enums.AtlasType.Gui);
        CSQ_0.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.4f, 0.4f, 0.4f));

        int CSQ_1ID = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(TileSheet, 3, 13, 0);

        // Initialize Pipe Widget
        CSQ_1Background = planet.AddUIImage("CSQ_1Background", GameObject.Find("Canvas").transform, "Assets\\StreamingAssets\\Items\\AdminIcon\\Tools\\white_square.png", Vec2f.Zero, new Vec3f(0.4f, 0.4f, 0.4f), 225, 225);

        CSQ_1Background.kGUIElementsImage.Image.SetImageMidBottom();
        CSQ_1Background.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.3f, 0.3f, 0.3f));
        CSQ_1Background.kGUIElementsImage.Image.SetPosition(new UnityEngine.Vector3(120f, -50f));
        CSQ_1Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        CSQ_1Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);


        CSQ_1 = planet.AddUIImage("CSQ_1", CSQ_1Background.kGUIElementsImage.Image.GetTransform(), 32, 32, CSQ_1ID, 0, Vec2f.Zero, new Vec3f(0.8f, 0.8f, 0.8f), Enums.AtlasType.Gui);
        CSQ_1.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.4f, 0.4f, 0.4f));

        int CSQ_2ID = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(TileSheet, 1, 15, 0);

        // Initialize Pipe Widget
        CSQ_2Background = planet.AddUIImage("CSQ_2Background", GameObject.Find("Canvas").transform, "Assets\\StreamingAssets\\Items\\AdminIcon\\Tools\\white_square.png", Vec2f.Zero, new Vec3f(0.4f, 0.4f, 0.4f), 225, 225);

        CSQ_2Background.kGUIElementsImage.Image.SetImageMidBottom();
        CSQ_2Background.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.3f, 0.3f, 0.3f));
        CSQ_2Background.kGUIElementsImage.Image.SetPosition(new UnityEngine.Vector3(120f, 0f));
        CSQ_2Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        CSQ_2Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);


        CSQ_2 = planet.AddUIImage("CSQ_2", CSQ_2Background.kGUIElementsImage.Image.GetTransform(), 32, 32, CSQ_2ID, 0, Vec2f.Zero, new Vec3f(0.8f, 0.8f, 0.8f), Enums.AtlasType.Gui);
        CSQ_2.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.4f, 0.4f, 0.4f));

        int CSQ_3ID = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(TileSheet, 3, 15, 0);

        // Initialize Pipe Widget
        CSQ_3Background = planet.AddUIImage("CSQ_3Background", GameObject.Find("Canvas").transform, "Assets\\StreamingAssets\\Items\\AdminIcon\\Tools\\white_square.png", Vec2f.Zero, new Vec3f(0.4f, 0.4f, 0.4f), 225, 225);

        CSQ_3Background.kGUIElementsImage.Image.SetImageMidBottom();
        CSQ_3Background.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.3f, 0.3f, 0.3f));
        CSQ_3Background.kGUIElementsImage.Image.SetPosition(new UnityEngine.Vector3(120f, 50f));
        CSQ_3Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        CSQ_3Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);


        CSQ_3 = planet.AddUIImage("CSQ_3", CSQ_3Background.kGUIElementsImage.Image.GetTransform(), 32, 32, CSQ_3ID, 0, Vec2f.Zero, new Vec3f(0.8f, 0.8f, 0.8f), Enums.AtlasType.Gui);
        CSQ_3.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.4f, 0.4f, 0.4f));

        int RCSQ_0ID = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(TileSheet, 5, 13, 0);

        // Initialize Pipe Widget
        RCSQ_0Background = planet.AddUIImage("RCSQ_0Background", GameObject.Find("Canvas").transform, "Assets\\StreamingAssets\\Items\\AdminIcon\\Tools\\white_square.png", Vec2f.Zero, new Vec3f(0.4f, 0.4f, 0.4f), 225, 225);

        RCSQ_0Background.kGUIElementsImage.Image.SetImageMidBottom();
        RCSQ_0Background.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.3f, 0.3f, 0.3f));
        RCSQ_0Background.kGUIElementsImage.Image.SetPosition(new UnityEngine.Vector3(170f, -100f));
        RCSQ_0Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        RCSQ_0Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);


        RCSQ_0 = planet.AddUIImage("RCSQ_0", RCSQ_0Background.kGUIElementsImage.Image.GetTransform(), 32, 32, RCSQ_0ID, 0, Vec2f.Zero, new Vec3f(0.8f, 0.8f, 0.8f), Enums.AtlasType.Gui);
        RCSQ_0.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.4f, 0.4f, 0.4f));

        int RCSQ_1ID = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(TileSheet, 7, 13, 0);

        // Initialize Pipe Widget
        RCSQ_1Background = planet.AddUIImage("RCSQ_1Background", GameObject.Find("Canvas").transform, "Assets\\StreamingAssets\\Items\\AdminIcon\\Tools\\white_square.png", Vec2f.Zero, new Vec3f(0.4f, 0.4f, 0.4f), 225, 225);

        RCSQ_1Background.kGUIElementsImage.Image.SetImageMidBottom();
        RCSQ_1Background.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.3f, 0.3f, 0.3f));
        RCSQ_1Background.kGUIElementsImage.Image.SetPosition(new UnityEngine.Vector3(170f, -50f));
        RCSQ_1Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        RCSQ_1Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);


        RCSQ_1 = planet.AddUIImage("RCSQ_1", RCSQ_1Background.kGUIElementsImage.Image.GetTransform(), 32, 32, RCSQ_1ID, 0, Vec2f.Zero, new Vec3f(0.8f, 0.8f, 0.8f), Enums.AtlasType.Gui);
        RCSQ_1.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.4f, 0.4f, 0.4f));

        int RCSQ_2ID = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(TileSheet, 5, 15, 0);

        // Initialize Pipe Widget
        RCSQ_2Background = planet.AddUIImage("RCSQ_2Background", GameObject.Find("Canvas").transform, "Assets\\StreamingAssets\\Items\\AdminIcon\\Tools\\white_square.png", Vec2f.Zero, new Vec3f(0.4f, 0.4f, 0.4f), 225, 225);

        RCSQ_2Background.kGUIElementsImage.Image.SetImageMidBottom();
        RCSQ_2Background.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.3f, 0.3f, 0.3f));
        RCSQ_2Background.kGUIElementsImage.Image.SetPosition(new UnityEngine.Vector3(170f, 0f));
        RCSQ_2Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        RCSQ_2Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);


        RCSQ_2 = planet.AddUIImage("RCSQ_2", RCSQ_2Background.kGUIElementsImage.Image.GetTransform(), 32, 32, RCSQ_2ID, 0, Vec2f.Zero, new Vec3f(0.8f, 0.8f, 0.8f), Enums.AtlasType.Gui);
        RCSQ_2.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.4f, 0.4f, 0.4f));

        int RCSQ_3ID = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(TileSheet, 7, 15, 0);

        // Initialize Pipe Widget
        RCSQ_3Background = planet.AddUIImage("RCSQ_3Background", GameObject.Find("Canvas").transform, "Assets\\StreamingAssets\\Items\\AdminIcon\\Tools\\white_square.png", Vec2f.Zero, new Vec3f(0.4f, 0.4f, 0.4f), 225, 225);

        RCSQ_3Background.kGUIElementsImage.Image.SetImageMidBottom();
        RCSQ_3Background.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.3f, 0.3f, 0.3f));
        RCSQ_3Background.kGUIElementsImage.Image.SetPosition(new UnityEngine.Vector3(170f, 50f));
        RCSQ_3Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        RCSQ_3Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);


        RCSQ_3 = planet.AddUIImage("RCSQ_3", RCSQ_3Background.kGUIElementsImage.Image.GetTransform(), 32, 32, RCSQ_3ID, 0, Vec2f.Zero, new Vec3f(0.8f, 0.8f, 0.8f), Enums.AtlasType.Gui);
        RCSQ_3.kGUIElementsImage.Image.SetScale(new UnityEngine.Vector3(0.4f, 0.4f, 0.4f));
    }

    public void Update(ref Planet.PlanetState planet, AgentEntity agentEntity)
    {
        inventoryID = agentEntity.agentInventory.InventoryID;

        // Set Inventory Elements
        Inventory = planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID);

        // Set Selected Slot
        selectedSlot = Inventory.inventoryEntity.SelectedSlotID;

        // Create Item
        item = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext, inventoryID, selectedSlot);

        if(item.itemType.Type == Enums.ItemType.GeometryPlacementTool)
        {

            if (SQNoSpecular_0.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition) ||
                SQ_0.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition) ||
                SQ_1.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition) ||
                SQ_2.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition) ||
                SQ_3.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition) ||
                HSQNoSpecular_0.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition) ||
                HSQNoSpecular_1.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition) ||
                HSQNoSpecular_2.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition) ||
                HSQNoSpecular_3.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition) ||
                HSQ_0.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition) ||
                HSQ_1.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition) ||
                HSQ_2.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition) ||
                HSQ_3.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition) ||
                SSQ_0.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition) ||
                SSQ_1.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition) ||
                SSQ_2.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition) ||
                SSQ_3.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition) ||
                TI_0.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition) ||
                TI_1.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition) ||
                TI_2.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition) ||
                TI_3.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition) ||
                TO_0.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition) ||
                TO_1.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition) ||
                TO_2.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition) ||
                TO_3.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition) ||
                HTD_0.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition) ||
                HTL_1.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition) ||
                HTU_2.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition) ||
                HTR_3.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition) ||
                RHTD_0.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition) ||
                RHTL_1.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition) ||
                RHTU_2.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition) ||
                RHTR_3.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition) ||
                CSQ_0.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition) ||
                CSQ_1.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition) ||
                CSQ_2.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition) ||
                CSQ_3.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition) ||
                RCSQ_0.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition) ||
                RCSQ_1.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition) ||
                RCSQ_2.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition) ||
                RCSQ_3.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition))
            {
                if(item.itemType.Type == Enums.ItemType.GeometryPlacementTool)
                {
                    if(item.hasItemTile)
                        item.itemTile.InputsActive = false;
                }
            }
            else
            {
                if (item.hasItemTile)
                    item.itemTile.InputsActive = true;
            }

            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                if(SQNoSpecular_0Background.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition))
                {
                    if (item.itemType.Type == Enums.ItemType.GeometryPlacementTool)
                    {
                        if (item.hasItemTile)
                        {
                            item.itemTile.TileID = Enums.Tile.TileID.SQNoSpecular_0;
                            ResetColors();
                            SQNoSpecular_0Background.kGUIElementsImage.Image.SetImageColor(Color.red);
                        }
                    }
                }
                if (SQ_0Background.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition))
                {
                    if (item.itemType.Type == Enums.ItemType.GeometryPlacementTool)
                    {
                        if (item.hasItemTile)
                        {
                            item.itemTile.TileID = Enums.Tile.TileID.SQ_0;
                            ResetColors();

                            SQ_0Background.kGUIElementsImage.Image.SetImageColor(Color.red);
                        }
                    }
                }
                if (SQ_1Background.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition))
                {
                    if (item.itemType.Type == Enums.ItemType.GeometryPlacementTool)
                    {
                        if (item.hasItemTile)
                        {
                            item.itemTile.TileID = Enums.Tile.TileID.SQ_1;
                            ResetColors();
                            SQ_1Background.kGUIElementsImage.Image.SetImageColor(Color.red);

                        }
                    }
                }
                if (SQ_2Background.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition))
                {
                    if (item.itemType.Type == Enums.ItemType.GeometryPlacementTool)
                    {
                        if (item.hasItemTile)
                        {
                            item.itemTile.TileID = Enums.Tile.TileID.SQ_2;
                            ResetColors();
                            SQ_2Background.kGUIElementsImage.Image.SetImageColor(Color.red);
                        }
                    }
                }
                if (SQ_3Background.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition))
                {
                    if (item.itemType.Type == Enums.ItemType.GeometryPlacementTool)
                    {
                        if (item.hasItemTile)
                        {
                            item.itemTile.TileID = Enums.Tile.TileID.SQ_3;
                            ResetColors();
                            SQ_3Background.kGUIElementsImage.Image.SetImageColor(Color.red);
                        }
                    }
                }
                if (HSQNoSpecular_0Background.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition))
                {
                    if (item.itemType.Type == Enums.ItemType.GeometryPlacementTool)
                    {
                        if (item.hasItemTile)
                        {
                            item.itemTile.TileID = Enums.Tile.TileID.HSQNoSpecular_0;
                            ResetColors();
                            HSQNoSpecular_0Background.kGUIElementsImage.Image.SetImageColor(Color.red);
                        }
                    }
                }
                if (HSQNoSpecular_1Background.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition))
                {
                    if (item.itemType.Type == Enums.ItemType.GeometryPlacementTool)
                    {
                        if (item.hasItemTile)
                        {
                            item.itemTile.TileID = Enums.Tile.TileID.HSQNoSpecular_1;
                            ResetColors();
                            HSQNoSpecular_1Background.kGUIElementsImage.Image.SetImageColor(Color.red);
                        }

                    }
                }
                if (HSQNoSpecular_2Background.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition))
                {
                    if (item.itemType.Type == Enums.ItemType.GeometryPlacementTool)
                    {
                        if (item.hasItemTile)
                        {
                            item.itemTile.TileID = Enums.Tile.TileID.HSQNoSpecular_2;
                            ResetColors();
                            HSQNoSpecular_2Background.kGUIElementsImage.Image.SetImageColor(Color.red);
                        }
                    }
                }
                if (HSQNoSpecular_3Background.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition))
                {
                    if (item.itemType.Type == Enums.ItemType.GeometryPlacementTool)
                    {
                        if (item.hasItemTile)
                        {
                            item.itemTile.TileID = Enums.Tile.TileID.HSQNoSpecular_3;
                            ResetColors();
                            HSQNoSpecular_3Background.kGUIElementsImage.Image.SetImageColor(Color.red);
                        }
                    }
                }
                if (HSQ_0Background.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition))
                {
                    if (item.itemType.Type == Enums.ItemType.GeometryPlacementTool)
                    {
                        if (item.hasItemTile)
                        {
                            item.itemTile.TileID = Enums.Tile.TileID.HSQ_0;
                            ResetColors();
                            HSQ_0Background.kGUIElementsImage.Image.SetImageColor(Color.red);
                        }
                    }
                }
                if (HSQ_1Background.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition))
                {
                    if (item.itemType.Type == Enums.ItemType.GeometryPlacementTool)
                    {
                        if (item.hasItemTile)
                        {
                            item.itemTile.TileID = Enums.Tile.TileID.HSQ_1;
                            ResetColors();
                            HSQ_1Background.kGUIElementsImage.Image.SetImageColor(Color.red);
                        }
                    }
                }
                if (HSQ_2Background.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition))
                {
                    if (item.itemType.Type == Enums.ItemType.GeometryPlacementTool)
                    {
                        if (item.hasItemTile)
                        {
                            item.itemTile.TileID = Enums.Tile.TileID.HSQ_2;
                            ResetColors();
                            HSQ_2Background.kGUIElementsImage.Image.SetImageColor(Color.red);
                        }
                    }
                }
                if (HSQ_3Background.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition))
                {
                    if (item.itemType.Type == Enums.ItemType.GeometryPlacementTool)
                    {
                        if (item.hasItemTile)
                        {
                            item.itemTile.TileID = Enums.Tile.TileID.HSQ_3;
                            ResetColors();
                            HSQ_3Background.kGUIElementsImage.Image.SetImageColor(Color.red);
                        }
                    }
                }
                if (SSQ_0Background.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition))
                {
                    if (item.itemType.Type == Enums.ItemType.GeometryPlacementTool)
                    {
                        if (item.hasItemTile)
                        {
                            item.itemTile.TileID = Enums.Tile.TileID.SSQ_0;
                            ResetColors();
                            SSQ_0Background.kGUIElementsImage.Image.SetImageColor(Color.red);
                        }
                    }
                }
                if (SSQ_1Background.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition))
                {
                    if (item.itemType.Type == Enums.ItemType.GeometryPlacementTool)
                    {
                        if (item.hasItemTile)
                        {
                            item.itemTile.TileID = Enums.Tile.TileID.SSQ_1;
                            ResetColors();
                            SSQ_1Background.kGUIElementsImage.Image.SetImageColor(Color.red);
                        }
                    }
                }
                if (SSQ_2Background.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition))
                {
                    if (item.itemType.Type == Enums.ItemType.GeometryPlacementTool)
                    {
                        if (item.hasItemTile)
                        {
                            item.itemTile.TileID = Enums.Tile.TileID.SSQ_2;
                            ResetColors();
                            SSQ_2Background.kGUIElementsImage.Image.SetImageColor(Color.red);
                        }
                    }
                }
                if (SSQ_3Background.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition))
                {
                    if (item.itemType.Type == Enums.ItemType.GeometryPlacementTool)
                    {
                        if (item.hasItemTile)
                        {
                            item.itemTile.TileID = Enums.Tile.TileID.SSQ_3;
                            ResetColors();
                            SSQ_3Background.kGUIElementsImage.Image.SetImageColor(Color.red);
                        }
                    }
                }
                if (TI_0Background.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition))
                {
                    if (item.itemType.Type == Enums.ItemType.GeometryPlacementTool)
                    {
                        if (item.hasItemTile)
                        {
                            item.itemTile.TileID = Enums.Tile.TileID.TI_0;
                            ResetColors();
                            TI_0Background.kGUIElementsImage.Image.SetImageColor(Color.red);
                        }
                    }
                }
                if (TI_1Background.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition))
                {
                    if (item.itemType.Type == Enums.ItemType.GeometryPlacementTool)
                    {
                        if (item.hasItemTile)
                        {
                            item.itemTile.TileID = Enums.Tile.TileID.TI_1;
                            ResetColors();
                            TI_1Background.kGUIElementsImage.Image.SetImageColor(Color.red);
                        }
                    }
                }
                if (TI_2Background.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition))
                {
                    if (item.itemType.Type == Enums.ItemType.GeometryPlacementTool)
                    {
                        if (item.hasItemTile)
                        {
                            item.itemTile.TileID = Enums.Tile.TileID.TI_2;
                            ResetColors();

                            TI_2Background.kGUIElementsImage.Image.SetImageColor(Color.red);
                        }
                    }
                }
                if (TI_3Background.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition))
                {
                    if (item.itemType.Type == Enums.ItemType.GeometryPlacementTool)
                    {
                        if (item.hasItemTile)
                        {
                            item.itemTile.TileID = Enums.Tile.TileID.TI_3;
                            ResetColors();

                            TI_3Background.kGUIElementsImage.Image.SetImageColor(Color.red);
                        }
                    }
                }
                if (TO_0Background.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition))
                {
                    if (item.itemType.Type == Enums.ItemType.GeometryPlacementTool)
                    {
                        if (item.hasItemTile)
                        {
                            item.itemTile.TileID = Enums.Tile.TileID.TO_0;
                            ResetColors();

                            TO_0Background.kGUIElementsImage.Image.SetImageColor(Color.red);
                        }
                    }
                }
                if (TO_1Background.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition))
                {
                    if (item.itemType.Type == Enums.ItemType.GeometryPlacementTool)
                    {
                        if (item.hasItemTile)
                        {
                            item.itemTile.TileID = Enums.Tile.TileID.TO_1;
                            ResetColors();

                            TO_1Background.kGUIElementsImage.Image.SetImageColor(Color.red);
                        }
                    }
                }
                if (TO_2Background.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition))
                {
                    if (item.itemType.Type == Enums.ItemType.GeometryPlacementTool)
                    {
                        if (item.hasItemTile)
                        {
                            item.itemTile.TileID = Enums.Tile.TileID.TO_2;
                            ResetColors();

                            TO_2Background.kGUIElementsImage.Image.SetImageColor(Color.red);
                        }
                    }
                }
                if (TO_3Background.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition))
                {
                    if (item.itemType.Type == Enums.ItemType.GeometryPlacementTool)
                    {
                        if (item.hasItemTile)
                        {
                            item.itemTile.TileID = Enums.Tile.TileID.TO_3;
                            ResetColors();

                            TO_3Background.kGUIElementsImage.Image.SetImageColor(Color.red);
                        }
                    }
                }
                if (HTD_0Background.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition))
                {
                    if (item.itemType.Type == Enums.ItemType.GeometryPlacementTool)
                    {
                        if (item.hasItemTile)
                        {
                            item.itemTile.TileID = Enums.Tile.TileID.HTD_0;
                            ResetColors();

                            HTD_0Background.kGUIElementsImage.Image.SetImageColor(Color.red);
                        }
                    }
                }
                if (HTL_1Background.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition))
                {
                    if (item.itemType.Type == Enums.ItemType.GeometryPlacementTool)
                    {
                        if (item.hasItemTile)
                        {
                            item.itemTile.TileID = Enums.Tile.TileID.HTL_1;
                            ResetColors();

                            HTL_1Background.kGUIElementsImage.Image.SetImageColor(Color.red);
                        }
                    }
                }
                if (HTU_2Background.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition))
                {
                    if (item.itemType.Type == Enums.ItemType.GeometryPlacementTool)
                    {
                        if (item.hasItemTile)
                        {
                            item.itemTile.TileID = Enums.Tile.TileID.HTU_2;
                            ResetColors();

                            HTU_2Background.kGUIElementsImage.Image.SetImageColor(Color.red);
                        }
                    }
                }
                if (HTR_3Background.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition))
                {
                    if (item.itemType.Type == Enums.ItemType.GeometryPlacementTool)
                    {
                        if (item.hasItemTile)
                        {
                            item.itemTile.TileID = Enums.Tile.TileID.HTR_3;
                            ResetColors();

                            HTR_3Background.kGUIElementsImage.Image.SetImageColor(Color.red);
                        }
                    }
                }
                if (RHTD_0Background.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition))
                {
                    if (item.itemType.Type == Enums.ItemType.GeometryPlacementTool)
                    {
                        if (item.hasItemTile)
                        {
                            item.itemTile.TileID = Enums.Tile.TileID.RHTD_0;
                            ResetColors();

                            RHTD_0Background.kGUIElementsImage.Image.SetImageColor(Color.red);
                        }
                    }
                }
                if (RHTL_1Background.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition))
                {
                    if (item.itemType.Type == Enums.ItemType.GeometryPlacementTool)
                    {
                        if (item.hasItemTile)
                        {
                            item.itemTile.TileID = Enums.Tile.TileID.RHTL_1;
                            ResetColors();

                            RHTL_1Background.kGUIElementsImage.Image.SetImageColor(Color.red);

                        }
                    }
                }
                if (RHTU_2Background.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition))
                {
                    if (item.itemType.Type == Enums.ItemType.GeometryPlacementTool)
                    {
                        if (item.hasItemTile)
                        {
                            item.itemTile.TileID = Enums.Tile.TileID.RHTU_2;
                            ResetColors();

                            RHTU_2Background.kGUIElementsImage.Image.SetImageColor(Color.red);
                        }
                    }
                }
                if (RHTR_3Background.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition))
                {
                    if (item.itemType.Type == Enums.ItemType.GeometryPlacementTool)
                    {
                        if (item.hasItemTile)
                        {
                            item.itemTile.TileID = Enums.Tile.TileID.RHTR_3;
                            ResetColors();

                            RHTR_3Background.kGUIElementsImage.Image.SetImageColor(Color.red);
                        }
                    }
                }
                if (CSQ_0Background.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition))
                {
                    if (item.itemType.Type == Enums.ItemType.GeometryPlacementTool)
                    {
                        if (item.hasItemTile)
                        {
                            item.itemTile.TileID = Enums.Tile.TileID.CSQ_0;
                            ResetColors();

                            CSQ_0Background.kGUIElementsImage.Image.SetImageColor(Color.red);
                        }
                    }
                }
                if (CSQ_1Background.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition))
                {
                    if (item.itemType.Type == Enums.ItemType.GeometryPlacementTool)
                    {
                        if (item.hasItemTile)
                        {
                            item.itemTile.TileID = Enums.Tile.TileID.CSQ_1;
                            ResetColors();

                            CSQ_1Background.kGUIElementsImage.Image.SetImageColor(Color.red);
                        }
                    }
                }
                if (CSQ_2Background.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition))
                {
                    if (item.itemType.Type == Enums.ItemType.GeometryPlacementTool)
                    {
                        if (item.hasItemTile)
                        {
                            item.itemTile.TileID = Enums.Tile.TileID.CSQ_2;
                            ResetColors();

                            CSQ_2Background.kGUIElementsImage.Image.SetImageColor(Color.red);
                        }
                    }
                }
                if (CSQ_3Background.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition))
                {
                    if (item.itemType.Type == Enums.ItemType.GeometryPlacementTool)
                    {
                        if (item.hasItemTile)
                        {
                            item.itemTile.TileID = Enums.Tile.TileID.CSQ_3;
                            ResetColors();

                            CSQ_3Background.kGUIElementsImage.Image.SetImageColor(Color.red);
                        }
                    }
                }
                if (RCSQ_0Background.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition))
                {
                    if (item.itemType.Type == Enums.ItemType.GeometryPlacementTool)
                    {
                        if (item.hasItemTile)
                        {
                            item.itemTile.TileID = Enums.Tile.TileID.RCSQ_0;
                            ResetColors();

                            RCSQ_0Background.kGUIElementsImage.Image.SetImageColor(Color.red);
                        }
                    }
                }
                if (RCSQ_1Background.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition))
                {
                    if (item.itemType.Type == Enums.ItemType.GeometryPlacementTool)
                    {
                        if (item.hasItemTile)
                        {
                            item.itemTile.TileID = Enums.Tile.TileID.RCSQ_1;
                            ResetColors();

                            RCSQ_1Background.kGUIElementsImage.Image.SetImageColor(Color.red);
                        }
                    }
                }
                if (RCSQ_2Background.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition))
                {
                    if (item.itemType.Type == Enums.ItemType.GeometryPlacementTool)
                    {
                        if (item.hasItemTile)
                        {
                            item.itemTile.TileID = Enums.Tile.TileID.RCSQ_2;
                            ResetColors();

                            RCSQ_2Background.kGUIElementsImage.Image.SetImageColor(Color.red);
                        }
                    }
                }
                if (RCSQ_3Background.kGUIElementsImage.Image.IsMouseOver(HUD.HUDManager.guiManager.CursorPosition))
                {
                    if (item.itemType.Type == Enums.ItemType.GeometryPlacementTool)
                    {
                        if (item.hasItemTile)
                        {
                            item.itemTile.TileID = Enums.Tile.TileID.RCSQ_3;
                            ResetColors();

                            RCSQ_3Background.kGUIElementsImage.Image.SetImageColor(Color.red);
                        }
                    }
                }
            }

            SQ_0Background.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            SQ_0.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            SQ_1Background.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            SQ_1.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            SQ_2Background.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            SQ_2.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            SQ_3Background.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            SQ_3.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            SQNoSpecular_0Background.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            SQNoSpecular_0.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            HSQNoSpecular_0Background.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            HSQNoSpecular_0.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            HSQNoSpecular_1Background.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            HSQNoSpecular_1.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            HSQNoSpecular_2Background.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            HSQNoSpecular_2.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            HSQNoSpecular_3Background.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            HSQNoSpecular_3.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            HSQ_0Background.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            HSQ_0.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            HSQ_1Background.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            HSQ_1.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            HSQ_2Background.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            HSQ_2.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            HSQ_3Background.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            HSQ_3.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            SSQ_0Background.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            SSQ_0.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            SSQ_1Background.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            SSQ_1.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            SSQ_2Background.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            SSQ_2.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            SSQ_3Background.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            SSQ_3.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            TI_0Background.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            TI_0.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            TI_1Background.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            TI_1.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            TI_2Background.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            TI_2.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            TI_3Background.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            TI_3.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            TO_0Background.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            TO_0.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            TO_1Background.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            TO_1.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            TO_2Background.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            TO_2.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            TO_3Background.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            TO_3.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            HTD_0Background.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            HTD_0.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            HTL_1Background.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            HTL_1.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            HTU_2Background.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            HTU_2.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            HTR_3Background.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            HTR_3.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            RHTD_0Background.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            RHTD_0.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            RHTL_1Background.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            RHTL_1.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            RHTU_2Background.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            RHTU_2.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            RHTR_3Background.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            RHTR_3.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            CSQ_0Background.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            CSQ_0.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            CSQ_1Background.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            CSQ_1.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            CSQ_2Background.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            CSQ_2.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            CSQ_3Background.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            CSQ_3.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            RCSQ_0Background.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            RCSQ_0.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            RCSQ_1Background.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            RCSQ_1.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            RCSQ_2Background.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            RCSQ_2.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            RCSQ_3Background.kGUIElementsImage.Image.GetGameObject().SetActive(true);
            RCSQ_3.kGUIElementsImage.Image.GetGameObject().SetActive(true);

        }
        else
        {
            SQ_0Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            SQ_0.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            SQ_1Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            SQ_1.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            SQ_2Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            SQ_2.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            SQ_3Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            SQ_3.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            SQNoSpecular_0Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            SQNoSpecular_0.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            HSQNoSpecular_0Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            HSQNoSpecular_0.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            HSQNoSpecular_1Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            HSQNoSpecular_1.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            HSQNoSpecular_2Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            HSQNoSpecular_2.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            HSQNoSpecular_3Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            HSQNoSpecular_3.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            HSQ_0Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            HSQ_0.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            HSQ_1Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            HSQ_1.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            HSQ_2Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            HSQ_2.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            HSQ_3Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            HSQ_3.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            SSQ_0Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            SSQ_0.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            SSQ_1Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            SSQ_1.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            SSQ_2Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            SSQ_2.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            SSQ_3Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            SSQ_3.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            TI_0Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            TI_0.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            TI_1Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            TI_1.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            TI_2Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            TI_2.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            TI_3Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            TI_3.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            TO_0Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            TO_0.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            TO_1Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            TO_1.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            TO_2Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            TO_2.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            TO_3Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            TO_3.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            HTD_0Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            HTD_0.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            HTL_1Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            HTL_1.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            HTU_2Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            HTU_2.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            HTR_3Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            HTR_3.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            RHTD_0Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            RHTD_0.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            RHTL_1Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            RHTL_1.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            RHTU_2Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            RHTU_2.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            RHTR_3Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            RHTR_3.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            CSQ_0Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            CSQ_0.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            CSQ_1Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            CSQ_1.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            CSQ_2Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            CSQ_2.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            CSQ_3Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            CSQ_3.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            RCSQ_0Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            RCSQ_0.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            RCSQ_1Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            RCSQ_1.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            RCSQ_2Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            RCSQ_2.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            RCSQ_3Background.kGUIElementsImage.Image.GetGameObject().SetActive(false);
            RCSQ_3.kGUIElementsImage.Image.GetGameObject().SetActive(false);
        }


    }

    private void ResetColors()
    {
        SQ_0Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        SQ_1Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        SQ_2Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        SQ_3Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        SQNoSpecular_0Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        HSQNoSpecular_0Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        HSQNoSpecular_1Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        HSQNoSpecular_2Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        HSQNoSpecular_3Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        HSQ_0Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        HSQ_1Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        HSQ_2Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        HSQ_3Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        SSQ_0Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        SSQ_1Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        SSQ_2Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        SSQ_3Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        TI_0Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        TI_1Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        TI_2Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        TI_3Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        TO_0Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        TO_1Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        TO_2Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        TO_3Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        HTD_0Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        HTL_1Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        HTU_2Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        HTR_3Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        RHTD_0Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        RHTL_1Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        RHTU_2Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        RHTR_3Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        CSQ_0Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        CSQ_1Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        CSQ_2Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        CSQ_3Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        RCSQ_0Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        RCSQ_1Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        RCSQ_2Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
        RCSQ_3Background.kGUIElementsImage.Image.SetImageColor(Color.yellow);
    }
}
