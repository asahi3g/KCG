using Entitas;
using Enums;
using Node;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.UIElements;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace AI
{
    public class InspectorView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<InspectorView, UxmlTraits> { }

        Enums.BehaviorType Type;
        ScrollView scrollView;

        public InspectorView()
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
            scrollView.style.right = 0;
            scrollView.style.left = 0;
            container.Add(scrollView);
            Add(container);
        }

        public void Init(BehaviorType type)
        {
            scrollView.contentViewport.Clear();
            Type = type;
        }


        internal void UpdateSelection(NodeView nodeView)
        {
            scrollView.contentViewport.Clear();
            ref BehaviorProperties behavior = ref AISystemState.Behaviors.Get((int)Type);
            AddAttributes(ref behavior, nodeView.Node, scrollView.contentViewport);
        }

        VisualElement AddAttributes(ref BehaviorProperties behavior, NodeInfo nodeInfo, VisualElement visualElement)
        {
            IntegerField integerField = new IntegerField("ID") { value = nodeInfo.index };
            integerField.isReadOnly = true;
            visualElement.Add(integerField);

            TextField enumField = new TextField("Node Type");
            enumField.value = nodeInfo.type.ToString();
            enumField.isReadOnly = true;
            visualElement.Add(enumField);

            NodeBase node = AISystemState.Nodes[(int)nodeInfo.type];
            List<Tuple<string, Type>> blackboardEntries = node.RegisterStates();

            if (blackboardEntries != null)
            {
                if (nodeInfo.entriesID == null)
                    nodeInfo.entriesID = new List<int>(Enumerable.Repeat(-1, blackboardEntries.Count));
                for (int i = 0; i < blackboardEntries.Count; i++)
                {
                    Foldout entryFoldout = Utility.Utils.CreateEntry(blackboardEntries[i]);
                    VisualElement field = Utility.Utils.CreateEntryField(blackboardEntries[i].Item2, behavior.BlackBoard.GetValue(nodeInfo.entriesID[i]), true);
                    entryFoldout.contentContainer.Add(field);
                    DropdownField dropdownField = new DropdownField("ID");
                    List<int> entries = behavior.BlackBoard.GetEntriesOfType(blackboardEntries[i].Item2);

                    foreach (var entryID in entries)
                    {
                        var entryInfo = behavior.BlackBoard.GetEntryInfo(entryID);
                        dropdownField.choices.Add(entryInfo.Name);
                        if (entryInfo.ID == nodeInfo.entriesID[i])
                            dropdownField.index = dropdownField.choices.Count - 1;
                    }
                    int entryIndex = i;
                    entryFoldout.contentContainer.Add(dropdownField);
                    dropdownField.RegisterCallback<ChangeEvent<string>>(x =>
                    {
                        ref BehaviorProperties behavior = ref AISystemState.Behaviors.Get((int)Type);
                        int newValue = behavior.BlackBoard.GetID(x.newValue);
                        nodeInfo.entriesID[entryIndex] = newValue;
                    });

                    visualElement.Add(entryFoldout);
                }
            }

            return visualElement;
        }
    }
}
