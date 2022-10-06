using Entitas;
using KMath;
using Node;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Utility;

namespace AI
{
    public class InspectorView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<InspectorView, UxmlTraits> { }

        public InspectorView()
        {
        }

        internal void UpdateSelection(NodeView nodeView)
        {
            ScrollView scrollView = this.Q<ScrollView>();
            scrollView.contentViewport.Clear();
            AddChildrenAttributes(Contexts.sharedInstance.node.GetEntityWithNodeIDID(nodeView.NodeID), scrollView.contentViewport);
        }

        void AddChildrenAttributes(NodeEntity nodeEntity, VisualElement visualElement)
        {
            VisualElement newVisualElement = AddAttributes(nodeEntity, visualElement);
            var nodes = nodeEntity.GetChildren(Contexts.sharedInstance.node);
            foreach (var node in nodes)
            {
                AddChildrenAttributes(node, newVisualElement);
            }
        }

        VisualElement AddAttributes(NodeEntity nodeEntity, VisualElement visualElement)
        {
            Foldout foldout = new Foldout();
            foldout.text = nodeEntity.nodeID.TypeID.ToString();
            foldout.value = true;
            IntegerField integerField = new IntegerField("ID") { value = nodeEntity.nodeID.ID };
            integerField.isReadOnly = true;
            foldout.contentContainer.Add(integerField);

            TextField enumField = new TextField("Node Type");
            enumField.value = nodeEntity.nodeID.TypeID.ToString();
            enumField.isReadOnly = true;
            foldout.contentContainer.Add(enumField);

            NodeBase node = nodeEntity.GetNodeSystem();
            List<Type> componentTypes = node.RegisterComponents();

            if (componentTypes != null)
            {
                //if (componentTypes.Count > 0)
                //    foldout.contentContainer.Add(new SpacerField());

                foreach (var type in componentTypes)
                {
                    int compIndex = Array.IndexOf(NodeComponentsLookup.componentTypes, type);
                    if (nodeEntity.GetComponent(compIndex) == null)
                    {
                        nodeEntity.AddComponent(compIndex, (IComponent)Activator.CreateInstance(type));
                    }
                    FieldInfo[] fields = type.GetFields();

                    foreach (FieldInfo field in fields)
                    {
                        VisualElement visualField = CreateCostumComponentField(nodeEntity.GetComponent(compIndex), field);
                        foldout.contentContainer.Add(visualField);
                    }
                }
            }

            visualElement.Add(foldout);
            return foldout;
        }

        public VisualElement CreateCostumComponentField(IComponent comnp, FieldInfo field)
        {
            VisualElement element = null;
            if (field.GetType() == typeof(Boolean))
            {
                Toggle bField = new Toggle();
                bField.value = (bool)field.GetValue(comnp);
                element = bField;
                element.RegisterCallback<ChangeEvent<Boolean>>(x =>
                {
                    field.SetValue(comnp, x.newValue);
                });
            }
            else if (field.GetType() == typeof(Int32) || field.GetType() == typeof(Int64))
            {
                IntegerField iField = new IntegerField(field.Name);
                iField.value = (int)field.GetValue(comnp);
                element = iField;
                element.RegisterCallback<ChangeEvent<Int32>>(x =>
                {
                    field.SetValue(comnp, x.newValue);
                });
            }
            else if (field.FieldType == typeof(float))
            {
                FloatField fField = new FloatField(field.Name);
                fField.value = (float)field.GetValue(comnp);
                element = fField;
                element.RegisterCallback<ChangeEvent<Single>>(x =>
                {
                    field.SetValue(comnp, x.newValue);
                });
            }

            else if (field.FieldType == typeof(double))
            {
                DoubleField dField = new DoubleField(field.Name);
                dField.value = (double)field.GetValue(comnp);
                element = dField;
                element.RegisterCallback<ChangeEvent<double>>(x =>
                {
                    field.SetValue(comnp, x.newValue);
                });
            }

            else if (field.FieldType == typeof(string))
            {
                TextField sField = new TextField(field.Name);
                sField.value = (string)field.GetValue(comnp);
                element = sField;
                element.RegisterCallback<ChangeEvent<string>>(x =>
                {
                    field.SetValue(comnp, x.newValue);
                });
            }
            else if (field.FieldType == typeof(Vec2f))
            {
                Vector2Field v2fField = new Vector2Field(field.Name);
                v2fField.value = ((Vec2f)field.GetValue(comnp)).GetVector2();
                element = v2fField;
                element.RegisterCallback<ChangeEvent<Vector2>>(x =>
                {
                    Vec2f value = new Vec2f(x.newValue.x, x.newValue.y);
                    field.SetValue(comnp, value);
                });
            }
            else if (field.FieldType == typeof(Vec3f))
            {
                Vector3Field vec3Field = new Vector3Field(field.Name);
                vec3Field.value = ((Vec3f)field.GetValue(comnp)).GetVector3();
                element = vec3Field;
                element.RegisterCallback<ChangeEvent<Vector3>>(x =>
                {
                    Vec3f value = new Vec3f(x.newValue.x, x.newValue.y, x.newValue.z);
                    field.SetValue(comnp, value);
                });
            }
            else if (field.FieldType == typeof(Vec2i))
            {
                Vector2IntField iField = new Vector2IntField(field.Name);
                iField.value = ((Vec2i)field.GetValue(comnp)).GetVector2();
                element = iField;
                element.RegisterCallback<ChangeEvent<Vector2Int>>(x =>
                {
                    Vec2i value = new Vec2i(x.newValue.x, x.newValue.y);
                    field.SetValue(comnp, value);
                });
            }
            else if (field.FieldType == typeof(Vec3i))
            {
                Vector3IntField iField = new Vector3IntField(field.Name);
                iField.value = ((Vec3i)field.GetValue(comnp)).GetVector3();
                element = iField;
                element.RegisterCallback<ChangeEvent<Vector3Int>>(x =>
                {
                    Vec3i value = new Vec3i(x.newValue.x, x.newValue.y, x.newValue.z);
                    field.SetValue(comnp, value);
                });
            }
            else
            {
                element = new Label("Field of type: " + field.GetType().ToString() + " isn't yet supported.");
            }

            // Todo deals with arrays.
            return element;
        }
    }
}
