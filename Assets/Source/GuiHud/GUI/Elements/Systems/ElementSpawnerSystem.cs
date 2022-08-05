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
    }
}

