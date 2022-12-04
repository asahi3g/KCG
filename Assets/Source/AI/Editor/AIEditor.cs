using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace AI
{

    public class AIEditor : EditorWindow
    {
        InspectorView inspectorView;
        BehaviorTreeView behaviorTreeView;
        ToolbarMenu toolbarMenu;
        VisualElement root;

        VisualElement NewTreeWindow;
        TextField NewBehaviorName;

        VisualElement AddSensorWindow;
        DropdownField NewSensorType;

        VisualElement AddBlackboardEntryWindow;
        TextField NewEntryName;
        DropdownField NewType;


        [MenuItem("AI/BehaviorTreeEditor")]
        static void Init()
        {
            // Get existing open window or if none, make a new one:
            AIEditor window = (AIEditor)EditorWindow.GetWindow(typeof(AIEditor));
            window.minSize = new Vector2(800, 600);
            window.Show();
        }
        void OnEnable()
        {
            root = rootVisualElement;
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Source/AI/Editor/Resources/BehaviorTreeEditor.uxml");
            VisualElement labelFromUXML = visualTree.CloneTree();
            root.Add(labelFromUXML);

            inspectorView = root.Q<InspectorView>();
            behaviorTreeView = root.Q<BehaviorTreeView>();
            behaviorTreeView.OnNodeSelected = inspectorView.UpdateSelection;
            SelectTree(0);

            toolbarMenu = root.Q<ToolbarMenu>();
            for (int i = 1; i < GameState.BehaviorTreeManager.GetLength(); i++)
            {
                ref BehaviorTree.BehaviorTreeExecute bt = ref GameState.BehaviorTreeManager.Get(i);
                AgentEntity agent = bt.GetAgentOwner();
                toolbarMenu.menu.AppendAction($"{agent.agentID.Type.ToString() + " iD: " + agent.agentID.ID.ToString()}", 
                    (a) => SelectTree(i));
            }
        }

        void SelectTree(int id)
        {
            ref BehaviorTree.BehaviorTreeExecute bt = ref GameState.BehaviorTreeManager.Get(id);
            behaviorTreeView.ID = id;
            behaviorTreeView.ClearTree();
            behaviorTreeView.PopulateView();
            behaviorTreeView.Init(id);
            inspectorView.Init(id);
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
