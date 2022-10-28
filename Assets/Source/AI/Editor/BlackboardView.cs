using Enums;
using System;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
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
            Type = BehaviorType.Error;
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

        public void Init(BehaviorType type)
        {
            scrollView.contentViewport.Clear();
            Type = type;
            BlackBoardModel blackboard = AISystemState.Behaviors.Get((int)Type).BlackBoard;

            for (int i = 0; i < blackboard.Length; i++)
            {
                AddEntry(i);
            }
        }

        public void AddEntry(Type type, string name)
        {
            BlackBoardModel blackboard = AISystemState.Behaviors.Get((int)Type).BlackBoard;
             int id = blackboard.Register(type, name);

            AddEntry(blackboard.IDToIndex[id]);
        }

        private void AddEntry(int index)
        {
            BlackBoardModel blackboard = AISystemState.Behaviors.Get((int)Type).BlackBoard;

            Tuple<string, Type> entry = new Tuple<string, Type>(blackboard.Entries[index].Name, blackboard.Entries[index].GetEntryType());
            Foldout foldout = Utility.Utils.CreateEntry(entry);
            VisualElement field = Utility.Utils.CreateEntryField(entry.Item2, blackboard.GetValue(blackboard.Entries[index].ID));
            RegisterCallback(index, entry.Item2, field);
            foldout.contentContainer.Add(field);
            IntegerField ID = new IntegerField("ID");
            ID.value = blackboard.Entries[index].ID;
            ID.isReadOnly = true;
            foldout.contentContainer.Add(ID);
            scrollView.contentViewport.Add(foldout);
        }

        private void RegisterCallback(int index, Type type, VisualElement field)
        {

            if (type == typeof(Boolean))
            {
                field.RegisterCallback<ChangeEvent<bool>>(x =>
                {
                    ref BehaviorProperties behavior = ref AISystemState.Behaviors.Get((int)Type);
                    int id = behavior.BlackBoard.Entries[index].ID;
                    int i = behavior.BlackBoard.Data.IDToIndex[id];
                    behavior.BlackBoard.Data.BoolEntries[i] = x.newValue;

                });
            }
            else if (type == typeof(int))
            {
                field.RegisterCallback<ChangeEvent<int>>(x =>
                {
                    ref BehaviorProperties behavior = ref AISystemState.Behaviors.Get((int)Type);
                    int id = behavior.BlackBoard.Entries[index].ID;
                    int i = behavior.BlackBoard.Data.IDToIndex[id];
                    behavior.BlackBoard.Data.IntEntries[i] = x.newValue; 

                });
            }
            else if (type == typeof(float))
            {
                field.RegisterCallback<ChangeEvent<float>>(x =>
                {
                    ref BehaviorProperties behavior = ref AISystemState.Behaviors.Get((int)Type);
                    int id = behavior.BlackBoard.Entries[index].ID;
                    int i = behavior.BlackBoard.Data.IDToIndex[id];
                    behavior.BlackBoard.Data.FloatEnties[i] = x.newValue;

                });
            }
            else if (type == typeof(Vec2f))
            {
                field.RegisterCallback<ChangeEvent<Vector2>>(x =>
                {
                    ref BehaviorProperties behavior = ref AISystemState.Behaviors.Get((int)Type);
                    int id = behavior.BlackBoard.Entries[index].ID;
                    int i = behavior.BlackBoard.Data.IDToIndex[id];
                    behavior.BlackBoard.Data.VecEntries[i] = new Vec2f(x.newValue.x, x.newValue.y);
                });
            }
        }
    }
}
