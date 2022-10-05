using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace AI
{

    public class AIEditor : EditorWindow
    {
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
            VisualElement root = rootVisualElement;
        
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Source/AI/Editor/Resources/BehaviorTreeEditor.uxml");
            VisualElement labelFromUXML = visualTree.CloneTree();
            root.Add(labelFromUXML);

            InspectorView inspectorView = root.Q<InspectorView>();
            BehaviorTreeView bt = root.Q<BehaviorTreeView>();
            bt.OnNodeSelected = inspectorView.UpdateSelection;

            ToolbarMenu toolbarMenu = root.Q<ToolbarMenu>();
            foreach (var behavior in AISystemState.Behaviors)
            {
                if (behavior.TypeID != Enums.BehaviorType.Error)
                    toolbarMenu.menu.AppendAction($"{behavior.TypeID.ToString()}", (a) =>{ bt.PopulateView(behavior.RootID); });
            }

            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Source/AI/Editor/Resources/BehaviorTreeEditorStyle.uss");
            root.styleSheets.Add(styleSheet);
        }

        private void OnGUI()
        {
            //Set the container height to the window
            rootVisualElement.Q<VisualElement>("Container").style.height = new StyleLength(position.height);
        }
    }
}
