using System.Collections.Generic;
using UnityEngine;
using KMath;

namespace KGUI.Elements
{
    public class ElementUpdateSystem
    {
        List<UIElementEntity> ToRemoveEntities = new List<UIElementEntity>();

        public void Update(ref Planet.PlanetState planetState, float deltaTime)
        {
            var entities = planetState.EntitasContext.uIElement.GetGroup(UIElementMatcher.AllOf(UIElementMatcher.KGUIElementsPosition2D));

            foreach (var entity in entities)
            {
                if(entity.hasKGUIElementsText)
                {
                    var text = entity.kGUIElementsText;
                    var position = entity.kGUIElementsPosition2D;

                    text.GameObject.Update();
                    text.GameObject.UpdateText(text.Text);
                    text.GameObject.SetPosition(new Vector3(position.Value.X, position.Value.Y, 0.0f));
                    text.GameObject.SetSizeDelta(new Vector2(text.SizeDelta.X, text.SizeDelta.Y));

                }

                if (entity.hasKGUIElementsImage)
                {
                    var image = entity.kGUIElementsImage;
                    var position = entity.kGUIElementsPosition2D;

                    image.Image.Update();
                    image.Image.SetPosition(new Vector3(position.Value.X, position.Value.Y, 0.0f));
                    image.Image.SetScale(new Vector3(image.Scale.X, image.Scale.Y, image.Scale.Z));
                }
            }

            foreach(var entity in ToRemoveEntities)
            {
                planetState.RemoveUIElement(entity.kGUIElementsID.Index);
            }
            ToRemoveEntities.Clear();
        }
    }
}
