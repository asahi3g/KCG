using UnityEngine;
using UnityEngine.UIElements;
using AI.Sensor;
using System.Collections.Generic;
using Enums;
using System;
using System.Linq;
using static Unity.Burst.Intrinsics.X86.Avx;
using static UnityEngine.EventSystems.EventTrigger;
using UnityEditor.Experimental.GraphView;

namespace AI
{
    public class SensorView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<SensorView, UxmlTraits> { }

        Enums.BehaviorType Type;
        ScrollView scrollView;
        public SensorView()
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
            button.text = "Add Sensor";
            Add(button);
        }

        public void Init(BehaviorType type)
        {
            scrollView.contentViewport.Clear();
            Type = type;
            ref BehaviorProperties behavior = ref AISystemState.Behaviors.Get((int)Type);

            for (int i = 0; i < behavior.SensorCount; i++)
            {
                SensorEntity sensor = behavior.Sensors[i];
                List<Tuple<string, Type>> states = AISystemState.Sensors[(int)sensor.Type].GetBlackboardEntries();
                AddEntries(i, ref states);
            }
        }

        public void AddSensor(SensorType type)
        {
            ref BehaviorProperties behavior = ref AISystemState.Behaviors.Get((int)Type);
            ref SensorEntity sensor = ref behavior.Sensors[behavior.SensorCount];
            List<Tuple<string, Type>> states = AISystemState.Sensors[(int)sensor.Type].GetBlackboardEntries();
            sensor.Type = type;
            sensor.EntriesID = new List<int>(Enumerable.Repeat(-1, states.Count));

            AddEntries(behavior.SensorCount++, ref states);
        }

        private void AddEntries(int sensorIndex, ref List<Tuple<string, Type>> states)
        {
            ref BehaviorProperties behavior = ref AISystemState.Behaviors.Get((int)Type);
            ref SensorEntity sensor = ref behavior.Sensors[sensorIndex];

            Foldout foldout = new Foldout();
            foldout.text = sensor.Type.ToString();
            foldout.value = true;

            for (int i = 0; i < sensor.EntriesID.Count; i++)
            {
                var state = states[i];
                Foldout entryFoldout = Utility.Utils.CreateEntry(state);
                VisualElement field = Utility.Utils.CreateEntryField(state.Item2, behavior.BlackBoard.GetValue(sensor.EntriesID[i]), true);
                foldout.contentContainer.Add(field);
                DropdownField dropdownField = new DropdownField("ID");
                List<int> entries = behavior.BlackBoard.GetEntriesOfType(state.Item2);
                foreach (var entryID in entries)
                {
                    var entryInfo = behavior.BlackBoard.GetEntryInfo(entryID);
                    dropdownField.choices.Add(entryInfo.Name);
                    if (entryInfo.ID == sensor.EntriesID[i])
                        dropdownField.index = dropdownField.choices.Count - 1;
                }
                int entryIndex = i;
                dropdownField.RegisterCallback<ChangeEvent<string>>(x =>
                {
                    ref BehaviorProperties behavior = ref AISystemState.Behaviors.Get((int)Type);
                    int newValue = behavior.BlackBoard.GetID(x.newValue);
                    behavior.Sensors[sensorIndex].EntriesID[entryIndex] = newValue;
                });
                entryFoldout.contentContainer.Add(dropdownField);
                foldout.contentContainer.Add(entryFoldout);
            }
            scrollView.contentViewport.Add(foldout);
        }
    }
}
