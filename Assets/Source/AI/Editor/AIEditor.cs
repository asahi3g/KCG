using Assets.Source.Utility.Editor.Generation;
using Enums;
using UnityEditor;
using UnityEditor.Overlays;
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
        VisualElement OverlayWindow;
        TextField newBehaviorTextField;


        [MenuItem("AI/BehaviorTreeEditor")]
        static void Init()
        {
            // Get existing open window or if none, make a new one:
            AIEditor window = (AIEditor)EditorWindow.GetWindow(typeof(AIEditor));
            window.minSize = new Vector2(800, 600);
            window.Show();
        }

        void OpenPopUp()
        {
             OverlayWindow.style.visibility = Visibility.Visible;
        }

        void CreateNewBT()
        {
            string newBehaviorName = newBehaviorTextField.text;
            int typeID = AISystemState.CreateNewBehavior(newBehaviorName);
            var btGen = new BehaviorEnumGen();
            btGen.Initialize();
            string outputText = btGen.TransformText();
            string folder = FileWriterManager.GetFullSourceFilePath();
            FileWriterManager.SaveFile(folder + "\\Enum\\NodeActions", "BehaviorType.cs", outputText);

            toolbarMenu.menu.InsertAction(typeID - 1, $"{AISystemState.Behaviors[typeID].Name}", (a) => { SelectTree((BehaviorType)typeID); });
            OverlayWindow.style.visibility = Visibility.Hidden;
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
            behaviorTree = root.Q<BehaviorTreeView>();
            behaviorTree.OnNodeSelected = inspectorView.UpdateSelection;
            SelectTree((BehaviorType)1);

            toolbarMenu = root.Q<ToolbarMenu>();
            for (int i = 1; i < AISystemState.Behaviors.Length; i++)
            {
                BehaviorType type = (BehaviorType)i;
                toolbarMenu.menu.AppendAction($"{AISystemState.Behaviors[i].Name}", (a) => SelectTree(type));
            }
            toolbarMenu.menu.AppendSeparator();
            toolbarMenu.menu.AppendAction($"{"Create new Behavior"}", (a) => { OpenPopUp(); });
            toolbarMenu.menu.AppendSeparator();
            toolbarMenu.menu.AppendAction($"{"Save Behavior"}", (a) => { SaveBts(); });

            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Source/AI/Editor/Resources/AIEditorStyle.uss");
            root.styleSheets.Add(styleSheet);

            var createTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Source/AI/Editor/Resources/NewBehaviorWindow.uxml");
            OverlayWindow = createTree.CloneTree();
            root.Add(OverlayWindow);
            newBehaviorTextField = OverlayWindow.Q<TextField>();
            OverlayWindow.Q<Button>().clicked += () => CreateNewBT();
            OverlayWindow.style.visibility = Visibility.Hidden;
        }

        void SelectTree(BehaviorType type)
        {
            behaviorTree.Type = type;
            behaviorTree.ClearTree();
            behaviorTree.PopulateView();
        }

        private void OnGUI()
        {
            //Set the container height to the window
            rootVisualElement.Q<VisualElement>("Container").style.height = new StyleLength(position.height);
            OverlayWindow.style.height = new StyleLength(position.height);
        }
    }
}
