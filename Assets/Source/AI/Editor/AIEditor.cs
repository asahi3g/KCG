using Assets.Source.Utility.Editor.Generation;
using Enums;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace AI
{

    public class AIEditor : EditorWindow
    {
        InspectorView inspectorView;
        BehaviorTreeView behaviorTree;
        ToolbarMenu toolbarMenu;


        [MenuItem("AI/BehaviorTreeEditor")]
        static void Init()
        {
            // Get existing open window or if none, make a new one:
            AIEditor window = (AIEditor)EditorWindow.GetWindow(typeof(AIEditor));
            window.minSize = new Vector2(800, 600);
            window.Show();
        }

        void CreateNewBT(string newBehaviorName)
        {
            int typeID = AISystemState.CreateNewBehavior(newBehaviorName);
            var btGen = new BehaviorEnumGen();
            btGen.Initialize();
            string outputText = btGen.TransformText();
            string folder = Utility.FileWriterManager.GetFullSourceFilePath();
            Utility.FileWriterManager.SaveFile(folder + "\\Enum\\NodeActions", "BehaviorType.cs", outputText);

            toolbarMenu.menu.InsertAction(typeID - 1, $"{AISystemState.Behaviors[typeID].Name}", (a) => { SelectTree((BehaviorType)typeID); });
        }

        void OnEnable()
        {
            VisualElement root = rootVisualElement;
        
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
            toolbarMenu.menu.AppendAction($"{"Create new Behavior"}", (a) => { CreateNewBT("testing"); });

            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Source/AI/Editor/Resources/BehaviorTreeEditorStyle.uss");
            root.styleSheets.Add(styleSheet);
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
        }
    }
}
