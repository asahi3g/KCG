using KMath;
using System.Collections.Generic;
using UnityEngine;

namespace KGUI.Elements
{
    public class ElementSpawnerSystem
    {
        public UIElementEntity SpawnText(UIElementContext UIElementContext, string text, float timeToLive,
            Vec2f position, Vec2f areaSize, int Index, Enums.ElementType elementType)
        {
            var entity = UIElementContext.CreateEntity();

            entity.AddKGUIElementsID(Index);
            entity.AddKGUIElementsPosition2D(position, position);

            Text textObj = new Text();
            textObj.entity = entity;

            entity.AddKGUIElementsText(text, timeToLive, textObj, areaSize);
            entity.AddKGUIElementsType(elementType);

            textObj.Create("UIElementText", entity.kGUIElementsText.Text, GameObject.Find("Canvas").transform, entity.kGUIElementsText.TimeToLive);
            textObj.SetSizeDelta(new Vector2(areaSize.X, areaSize.Y));
            entity.kGUIElementsText.GameObject.startLifeTime = true;

            return entity;
        }

        public UIElementEntity SpawnText(UIElementContext UIElementContext, string text,
            Vec2f position, Vec2f areaSize, int Index, Enums.ElementType elementType)
        {
            var entity = UIElementContext.CreateEntity();

            entity.AddKGUIElementsID(Index);
            entity.AddKGUIElementsPosition2D(position, position);

            Text textObj = new Text();
            textObj.entity = entity;

            entity.AddKGUIElementsText(text, 50.0f, textObj, areaSize);
            entity.AddKGUIElementsType(elementType);

            textObj.Create("UIElementText", entity.kGUIElementsText.Text, GameObject.Find("Canvas").transform, entity.kGUIElementsText.TimeToLive);
            textObj.SetSizeDelta(new Vector2(areaSize.X, areaSize.Y));

            return entity;
        }

        public UIElementEntity SpawnImage(UIElementContext UIElementContext, string Name, Transform parent,
            Sprite sprite, Vec2f position, Vec3f scale, UnityEngine.UI.Image.Type type, int Index, Enums.ElementType elementType)
        {
            var entity = UIElementContext.CreateEntity();

            entity.AddKGUIElementsID(Index);
            entity.AddKGUIElementsPosition2D(position, position);

            entity.AddKGUIElementsImage(Name, sprite, null, scale, 0, 0, "", -1);
            var image = entity.kGUIElementsImage;
            entity.kGUIElementsImage.Image = new Image(image.Name, parent, image.Sprite);

            entity.AddKGUIElementsType(elementType);

            entity.kGUIElementsImage.Image.SetImageType(type);
            entity.kGUIElementsImage.Image.SetPosition(new Vector3(entity.kGUIElementsPosition2D.Value.X, entity.kGUIElementsPosition2D.Value.Y));
            entity.kGUIElementsImage.Image.SetScale(new Vector3(image.Scale.X, image.Scale.Y, image.Scale.Z));

            return entity;
        }

        public UIElementEntity SpawnImage(UIElementContext UIElementContext, string Name, Transform parent,
            Sprite sprite, Vec2f position, Vec3f scale, UnityEngine.UI.Image.Type type, Color color, int Index, Enums.ElementType elementType)
        {
            var entity = UIElementContext.CreateEntity();

            entity.AddKGUIElementsID(Index);
            entity.AddKGUIElementsPosition2D(position, position);

            entity.AddKGUIElementsImage(Name, sprite, null, scale, 0, 0, "", - 1);
            var image = entity.kGUIElementsImage;
            entity.kGUIElementsImage.Image = new Image(image.Name, parent, image.Sprite);

            entity.AddKGUIElementsType(elementType);

            entity.kGUIElementsImage.Image.SetImageType(type);
            entity.kGUIElementsImage.Image.SetImageColor(color);
            entity.kGUIElementsImage.Image.SetPosition(new Vector3(entity.kGUIElementsPosition2D.Value.X, entity.kGUIElementsPosition2D.Value.Y));
            entity.kGUIElementsImage.Image.SetScale(new Vector3(image.Scale.X, image.Scale.Y, image.Scale.Z));

            return entity;
        }
        
        public UIElementEntity SpawnImage(UIElementContext UIElementContext, string Name, Transform parent,
            Sprite sprite, Vec2f position, Vec3f scale, Vec2f size, UnityEngine.UI.Image.Type type, Color color, int Index, Enums.ElementType elementType)
        {
            var entity = UIElementContext.CreateEntity();

            entity.AddKGUIElementsID(Index);
            entity.AddKGUIElementsPosition2D(position, position);

            entity.AddKGUIElementsImage(Name, sprite, null, scale, 0, 0, "", - 1);
            var image = entity.kGUIElementsImage;
            entity.kGUIElementsImage.Image = new Image(image.Name, parent, image.Sprite);

            entity.AddKGUIElementsType(elementType);

            entity.kGUIElementsImage.Image.SetImageType(type);
            entity.kGUIElementsImage.Image.SetImageColor(color);
            entity.kGUIElementsImage.Image.SetPosition(new Vector3(entity.kGUIElementsPosition2D.Value.X, entity.kGUIElementsPosition2D.Value.Y));
            entity.kGUIElementsImage.Image.SetScale(new Vector3(image.Scale.X, image.Scale.Y, image.Scale.Z));
            entity.kGUIElementsImage.Image.SetSize(new Vector2(size.X, size.Y));

            return entity;
        }

        public UIElementEntity SpawnImage(UIElementContext UIElementContext, string Name, Transform parent,
            Sprite sprite, Vec2f position, Vec3f scale, int Index, Enums.ElementType elementType)
        {
            var entity = UIElementContext.CreateEntity();

            entity.AddKGUIElementsID(Index);
            entity.AddKGUIElementsPosition2D(position, position);

            entity.AddKGUIElementsImage(Name, sprite, null, scale, 0, 0, "", -1);
            var image = entity.kGUIElementsImage;
            entity.kGUIElementsImage.Image = new Image(image.Name, parent, image.Sprite);

            entity.AddKGUIElementsType(elementType);

            entity.kGUIElementsImage.Image.SetPosition(new Vector3(entity.kGUIElementsPosition2D.Value.X, entity.kGUIElementsPosition2D.Value.Y));
            entity.kGUIElementsImage.Image.SetScale(new Vector3(image.Scale.X, image.Scale.Y, image.Scale.Z));

            return entity;
        }

        public UIElementEntity SpawnImage(UIElementContext UIElementContext, string Name, Transform parent,
            string path, Vec2f position, Vec3f scale, int width, int height, int Index, Enums.ElementType elementType)
        {
            var entity = UIElementContext.CreateEntity();

            entity.AddKGUIElementsID(Index);
            entity.AddKGUIElementsPosition2D(position, position);

            entity.AddKGUIElementsImage(Name, null, null, scale, width, height, path, -1);
            var image = entity.kGUIElementsImage;
            entity.kGUIElementsImage.Image = new Image(image.Name, parent, image.Width, image.Height, image.Path);

            entity.AddKGUIElementsType(elementType);

            entity.kGUIElementsImage.Image.SetPosition(new Vector3(entity.kGUIElementsPosition2D.Value.X, entity.kGUIElementsPosition2D.Value.Y));
            entity.kGUIElementsImage.Image.SetScale(new Vector3(image.Scale.X, image.Scale.Y, image.Scale.Z));

            return entity;
        }
        
        public UIElementEntity SpawnImage(UIElementContext UIElementContext, string Name, Transform parent,
            string path, Vec2f position, Vec3f scale, Vec2f size, int width, int height, int Index, Enums.ElementType elementType)
        {
            var entity = UIElementContext.CreateEntity();

            entity.AddKGUIElementsID(Index);
            entity.AddKGUIElementsPosition2D(position, position);

            entity.AddKGUIElementsImage(Name, null, null, scale, width, height, path, -1);
            var image = entity.kGUIElementsImage;
            entity.kGUIElementsImage.Image = new Image(image.Name, parent, image.Width, image.Height, image.Path);

            entity.AddKGUIElementsType(elementType);

            entity.kGUIElementsImage.Image.SetPosition(new Vector3(entity.kGUIElementsPosition2D.Value.X, entity.kGUIElementsPosition2D.Value.Y));
            entity.kGUIElementsImage.Image.SetScale(new Vector3(image.Scale.X, image.Scale.Y, image.Scale.Z));
            entity.kGUIElementsImage.Image.SetSize(new Vector2(size.X, size.Y));

            return entity;
        }

        public UIElementEntity SpawnImage(UIElementContext UIElementContext, string Name, Transform parent,
            int tileSpriteID, Vec2f position, Vec3f scale, int width, int height, int Index, Enums.ElementType elementType)
        {
            var entity = UIElementContext.CreateEntity();

            entity.AddKGUIElementsID(Index);
            entity.AddKGUIElementsPosition2D(position, position);

            entity.AddKGUIElementsImage(Name, null, null, scale, width, height, "", tileSpriteID);
            var image = entity.kGUIElementsImage;
            entity.kGUIElementsImage.Image = new Image(image.Name, parent, image.Width, image.Height, tileSpriteID);

            entity.AddKGUIElementsType(elementType);

            entity.kGUIElementsImage.Image.SetPosition(new Vector3(entity.kGUIElementsPosition2D.Value.X, entity.kGUIElementsPosition2D.Value.Y));
            entity.kGUIElementsImage.Image.SetScale(new Vector3(image.Scale.X, image.Scale.Y, image.Scale.Z));

            return entity;
        }

        public UIElementEntity SpawnImage(UIElementContext UIElementContext, string Name, Transform parent,
            int width, int height, int tileSpriteID, Vec2f position, Vec3f scale, int Index, Enums.AtlasType atlasType,
            Enums.ElementType elementType)
        {
            var entity = UIElementContext.CreateEntity();

            entity.AddKGUIElementsID(Index);
            entity.AddKGUIElementsPosition2D(position, position);

            entity.AddKGUIElementsImage(Name, null, null, scale, width, height, "", tileSpriteID);
            var image = entity.kGUIElementsImage;
            entity.kGUIElementsImage.Image = new Image(image.Name, parent, image.Width, image.Height, tileSpriteID, atlasType);

            entity.AddKGUIElementsType(elementType);

            entity.kGUIElementsImage.Image.SetPosition(new Vector3(entity.kGUIElementsPosition2D.Value.X, entity.kGUIElementsPosition2D.Value.Y));
            entity.kGUIElementsImage.Image.SetScale(new Vector3(image.Scale.X, image.Scale.Y, image.Scale.Z));

            return entity;
        }

        public UIElementEntity SpawnImage(UIElementContext UIElementContext, string Name, Transform parent,
            int width, int height, int tileSpriteID, Vec2f position, Vec3f scale, int Index, Enums.AtlasType atlasType,
                Enums.ElementType elementType, bool hasMultiplePositions, Vec2f pos1, Vec2f pos2)
        {
            var entity = UIElementContext.CreateEntity();

            entity.AddKGUIElementsID(Index);
            entity.AddKGUIElementsPosition2D(position, position);

            entity.AddKGUIElementsImage(Name, null, null, scale, width, height, "", tileSpriteID);
            var image = entity.kGUIElementsImage;
            entity.kGUIElementsImage.Image = new Image(image.Name, parent, image.Width, image.Height, tileSpriteID, atlasType);

            entity.AddKGUIElementsType(elementType);

            entity.kGUIElementsImage.Image.SetPosition(new Vector3(entity.kGUIElementsPosition2D.Value.X, entity.kGUIElementsPosition2D.Value.Y));
            entity.kGUIElementsImage.Image.SetScale(new Vector3(image.Scale.X, image.Scale.Y, image.Scale.Z));

            if(hasMultiplePositions)
            {
                entity.AddKGUIElementsMultiplePosition(pos1, pos2);
            }


            return entity;
        }

    }
}

