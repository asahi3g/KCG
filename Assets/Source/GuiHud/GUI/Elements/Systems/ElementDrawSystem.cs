using System.Collections.Generic;
using UnityEngine;
using KMath;

namespace KGUI.Elements
{
    public class ElementDrawSystem
    {
        public void Draw(UIElementContext UIElementContext)
        {
            var entities = UIElementContext.GetGroup(UIElementMatcher.AllOf(UIElementMatcher.KGUIElementsPosition2D));

            foreach (var entity in entities)
            {
                if (entity.hasKGUIElementsText)
                {
                    var position = entity.kGUIElementsPosition2D;
                    var text = entity.kGUIElementsText;

                    entity.kGUIElementsText.GameObject.Draw();
                }

            }
        }
    }
}

