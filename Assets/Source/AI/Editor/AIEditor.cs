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
        DropdownField BehaviorTreeList;
        VisualElement root;


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
            BehaviorTreeList = root.Q<DropdownField>();
            BehaviorTreeList.RegisterValueChangedCallback(OnChangeValueCallback);
            BehaviorTreeList.RegisterCallback<PointerDownEvent>(OnPointerDown);

            UpdateDropDownChoices();
            SelectTree(BehaviorTreeList.index);
        }

        void SelectTree(int id)
        {
            behaviorTreeView.ClearTree();
            if (id >= 0)
            {
                ref BehaviorTree.BehaviorTreeExecute bt = ref GameState.BehaviorTreeManager.Get(id);
                behaviorTreeView.ID = id;
                behaviorTreeView.Init(id);
                inspectorView.Init(id);
            }
        }

        void OnChangeValueCallback(ChangeEvent<string> evt)
        {
            Debug.Log($"Behavior tree changed. old: {evt.previousValue}, new: {evt.newValue}");
            SelectTree(BehaviorTreeList.index);
        }

        void OnPointerDown(PointerDownEvent evt)
        {
            BehaviorTreeList.choices.Clear();
            for (int i = 1; i < GameState.BehaviorTreeManager.GetLength(); i++)
            {
                ref BehaviorTree.BehaviorTreeExecute bt = ref GameState.BehaviorTreeManager.Get(i);
                AgentEntity agent = bt.GetAgentOwner();
                BehaviorTreeList.choices.Add(agent.agentID.Type.ToString() + " ID: " + agent.agentID.ID.ToString());
            }
        }

        void UpdateDropDownChoices()
        {
            BehaviorTreeList.choices.Clear();
            for (int i = 1; i < GameState.BehaviorTreeManager.GetLength(); i++)
            {
                ref BehaviorTree.BehaviorTreeExecute bt = ref GameState.BehaviorTreeManager.Get(i);
                AgentEntity agent = bt.GetAgentOwner();
                BehaviorTreeList.choices.Add(agent.agentID.Type.ToString() + " ID: " + agent.agentID.ID.ToString());
            }
        }

        private void OnGUI()
        {
            //Set the container height to the window
            rootVisualElement.Q<VisualElement>("Container").style.height = new StyleLength(position.height);
        }
    }
}
