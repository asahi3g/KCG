using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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

        void CreateGUI()
        {
            VisualElement root = rootVisualElement;
        
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Source/AI/Editor/BehaviorTreeEditor.uxml");
            VisualElement labelFromUXML = visualTree.CloneTree();
            root.Add(labelFromUXML);

            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Source/AI/Editor/BehaviorTreeEditorStyle.uss");
            root.styleSheets.Add(styleSheet);

            var preMadeStyleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Source/AI/Editor/PresetTemplate.uss");
            root.styleSheets.Add(preMadeStyleSheet);
        }
    }
}