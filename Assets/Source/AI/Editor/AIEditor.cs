using Assets.Source.Utility.Editor.Generation;
using Enums;
using System;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Utility;

namespace AI
{

    public class AIEditor : EditorWindow
    {
        InspectorView inspectorView;
        BehaviorTreeView behaviorTree;
        ToolbarMenu toolbarMenu;
        VisualElement root;

        VisualElement NewTreeWindow;
        TextField NewBehaviorName;

        SensorView sensorView;
        VisualElement AddSensorWindow;
        DropdownField NewSensorType;

        BlackboardView blackboardView;
        VisualElement AddBlackboardEntryWindow;
        TextField NewEntryName;
        DropdownField NewType;


        [MenuItem("AI/BehaviorTreeEditor")]
        static void Init()
        {
            // Get existing open window or if none, make a new one:
            AIEditor window = (AIEditor)GetWindow(typeof(AIEditor));
            window.minSize = new Vector2(800, 600);
            window.Show();
        }

        void OpenNewBehaviorWindow() => root.Add(NewTreeWindow);
        void OpenAddSensorWindow() => root.Add(AddSensorWindow);
        void OpenAddEntryWindow() => root.Add(AddBlackboardEntryWindow);

        void CreateNewBT()
        {
            string newBehaviorName = NewBehaviorName.text;
            int typeID = AISystemState.CreateNewBehavior(newBehaviorName);
            var btGen = new BehaviorEnumGen();
            btGen.Initialize();
            string outputText = btGen.TransformText();
            string folder = FileWriterManager.GetFullSourceFilePath();
            FileWriterManager.SaveFile(folder + "\\Enum\\NodeActions", "BehaviorType.cs", outputText);

            toolbarMenu.menu.InsertAction(typeID - 1, $"{AISystemState.Behaviors.Get(typeID).Name}", (a) => { SelectTree((BehaviorType)typeID); });
            root.Remove(NewTreeWindow);
        }

        void AddSensor()
        {
            ref BehaviorProperties behavior = ref AISystemState.Behaviors.Get((int)behaviorTree.Type);
            SensorType type = (SensorType)Enum.Parse(typeof(SensorType), NewSensorType.value);
            sensorView.AddSensor(type);

            NewSensorType.choices.Remove(NewSensorType.value);
            NewSensorType.index = 0;
            root.Remove(AddSensorWindow);
        }

        void AddEntry()
        {
            ref BehaviorProperties behavior = ref AISystemState.Behaviors.Get((int)behaviorTree.Type);

            Type type = Utils.GetType(NewType.value);
            blackboardView.AddEntry(type, NewEntryName.text);
            NewSensorType.index = 0;
            root.Remove(AddBlackboardEntryWindow);
        }

        void SaveBts()
        {
            var builder = new BehaviourBuilder();
            builder.Initialize();
            builder.TransformText();
        }

        void OnEnable()
        {
            root = rootVisualElement;
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Source/AI/Editor/Resources/BehaviorTreeEditor.uxml");
            VisualElement labelFromUXML = visualTree.CloneTree();
            root.Add(labelFromUXML);

            inspectorView = root.Q<InspectorView>();
            sensorView = root.Q<SensorView>();
            sensorView.Q<Button>().clicked += () => OpenAddSensorWindow();
            blackboardView = root.Q<BlackboardView>();
            blackboardView.Q<Button>().clicked += () => OpenAddEntryWindow();
            behaviorTree = root.Q<BehaviorTreeView>();
            behaviorTree.OnNodeSelected = inspectorView.UpdateSelection;
            SelectTree((BehaviorType)1);

            toolbarMenu = root.Q<ToolbarMenu>();
            for (int i = 1; i < AISystemState.Behaviors.Length; i++)
            {
                BehaviorType type = (BehaviorType)i;
                toolbarMenu.menu.AppendAction($"{AISystemState.Behaviors.Get(i).Name}", (a) => SelectTree(type));
            }
            toolbarMenu.menu.AppendSeparator();
            toolbarMenu.menu.AppendAction($"{"Create new Behavior"}", (a) => { OpenNewBehaviorWindow(); });
            toolbarMenu.menu.AppendSeparator();
            toolbarMenu.menu.AppendAction($"{"Save Behavior"}", (a) => { SaveBts(); });

            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Source/AI/Editor/Resources/AIEditorStyle.uss");
            root.styleSheets.Add(styleSheet);

            visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Source/AI/Editor/Resources/NewBehaviorWindow.uxml");
            NewTreeWindow = visualTree.CloneTree();
            NewBehaviorName = NewTreeWindow.Q<TextField>();
            NewTreeWindow.Q<Button>().clicked += () => CreateNewBT();
            
            visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Source/AI/Editor/Resources/NewBlackboardEntryWindow.uxml");
            AddBlackboardEntryWindow = visualTree.CloneTree();
            NewType = AddBlackboardEntryWindow.Q<DropdownField>();
            NewEntryName = AddBlackboardEntryWindow.Q<TextField>();
            NewType.choices.Clear();
            NewType.choices.Add(Utils.TypeNameOrAlias(typeof(bool)));
            NewType.choices.Add(Utils.TypeNameOrAlias(typeof(float)));
            NewType.choices.Add(Utils.TypeNameOrAlias(typeof(int)));
            NewType.choices.Add(Utils.TypeNameOrAlias(typeof(KMath.Vec2f)));
            NewType.index = 0;
            AddBlackboardEntryWindow.Q<Button>().clicked += () => AddEntry();

            visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Source/AI/Editor/Resources/NewSensorWindow.uxml");
            AddSensorWindow = visualTree.CloneTree();
            NewSensorType = AddSensorWindow.Q<DropdownField>();
            NewSensorType.choices.Clear();
            NewSensorType.choices.AddRange(Enum.GetNames(typeof(SensorType)).SkipLast(1).ToArray()); // Add all enums but the Error.
            NewSensorType.index = 0;
            AddSensorWindow.Q<Button>().clicked += () => AddSensor();
        }

        void SelectTree(BehaviorType type)
        {
            behaviorTree.Type = type;
            behaviorTree.ClearTree();
            behaviorTree.PopulateView();
            sensorView.Init(type);
            blackboardView.Init(type);
            inspectorView.Init(type);
        }

        private void OnGUI()
        {
            //Set the container height to the window
            rootVisualElement.Q<VisualElement>("Container").style.height = new StyleLength(position.height);
            NewTreeWindow.style.height = new StyleLength(position.height);
            AddSensorWindow.style.height = new StyleLength(position.height);
            AddBlackboardEntryWindow.style.height = new StyleLength(position.height);
        }
    }
}
