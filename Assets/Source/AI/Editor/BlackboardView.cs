using Enums;
using System;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Entitas;
using KMath;

namespace AI
{
    public class BlackboardView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<BlackboardView, UxmlTraits> { }

        BehaviorType Type;
        ScrollView scrollView;

        public BlackboardView()
        {
            Type = Enums.BehaviorType.Error;
            VisualElement container = new VisualElement();
            container.name = "Container";
            container.style.flexGrow = 1;
            container.style.flexShrink = 0;
            scrollView = new ScrollView();
            scrollView.style.position = Position.Absolute;
            scrollView.style.top = 0;
            scrollView.style.bottom = 0;
            scrollView.style.left = 0;
            scrollView.style.right = 0;
            container.Add(scrollView);
            Add(container);
            Button button = new Button();
            button.text = "Add Entry";
            Add(button);

            // Trigger event to update inspector view and sensor view option lists.
            scrollView.contentViewport.RegisterCallback<AttachToPanelEvent> (x => { Debug.Log("Entry Added"); });
        }
    }
}
