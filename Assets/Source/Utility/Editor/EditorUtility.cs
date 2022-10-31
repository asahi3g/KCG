using KMath;
using System;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using UnityEngine;

namespace Utility
{
    public partial class Utils
    {
        static Dictionary<Type, string> TypeToName = new Dictionary<Type, string>
        {
        { typeof(bool), "bool" },
        { typeof(byte), "byte" },
        { typeof(char), "char" },
        { typeof(decimal), "decimal" },
        { typeof(double), "double" },
        { typeof(float), "float" },
        { typeof(int), "int" },
        { typeof(long), "long" },
        { typeof(object), "object" },
        { typeof(sbyte), "sbyte" },
        { typeof(short), "short" },
        { typeof(string), "string" },
        { typeof(uint), "uint" },
        { typeof(ulong), "ulong" },
        { typeof(Vec2f), "Vec2f" },
        { typeof(void), "void" }
    };

        static Dictionary<string, Type> NameToType = new Dictionary<string, Type>
        {
        { "bool", typeof(bool) },
        { "byte", typeof(byte) },
        { "char", typeof(char) },
        { "decimal", typeof(decimal) },
        { "double", typeof(double) },
        { "float", typeof(float) },
        { "int", typeof(int) },
        { "long", typeof(long) },
        { "object", typeof(object) },
        { "sbyte", typeof(sbyte) },
        { "short", typeof(short) },
        { "string", typeof(string) },
        { "uint", typeof(uint) },
        { "ulong", typeof(ulong) },
        { "Vec2f", typeof(Vec2f) },
        { "void", typeof(void) }
    };

        static public string TypeNameOrAlias(Type type)
        {
            if (TypeToName.TryGetValue(type, out string alias))
                return alias;

            return type.Name;
        }

        static public Type GetType(string name)
        {
            if (NameToType.TryGetValue(name, out Type alias))
                return alias;

            return null;
        }

        static public Foldout CreateEntry(Tuple<string, Type> entry)
        {
            Foldout foldout = new Foldout();
            foldout.text = entry.Item1;
            foldout.value = true;
            TextField type = new TextField("Type:");
            type.value = TypeNameOrAlias(entry.Item2);
            type.isReadOnly = true;
            foldout.contentContainer.Add(type);
            return foldout;
        }

        static public VisualElement CreateEntryField(Type type, object value, bool isReadOnly = false)
        {
            VisualElement element = null;
            if (type == typeof(Boolean))
            {
                Toggle bField = new Toggle("Value");
                if (value != null)
                    bField.value = (bool)value;
                element = bField;
            }
            else if (type == typeof(Int32) || type == typeof(Int64))
            {
                IntegerField iField = new IntegerField("Value");
                if (value != null)
                    iField.value = (Int32)value;
                iField.isReadOnly = isReadOnly;
                element = iField;
            }
            else if (type == typeof(float))
            {
                FloatField fField = new FloatField("Value");
                if (value != null)
                    fField.value = (float)value;
                fField.isReadOnly = isReadOnly;
                element = fField;
            }

            else if (type == typeof(double))
            {
                DoubleField dField = new DoubleField("Value");
                if (value != null)
                    dField.value = (double)value;
                dField.isReadOnly = isReadOnly;
                element = dField;
            }

            else if (type == typeof(string))
            {
                TextField sField = new TextField("Value");
                if (value != null)
                    sField.value = (string)value;
                sField.isReadOnly = isReadOnly;
                element = sField;
            }
            else if (type == typeof(Vec2f))
            {
                Vector2Field v2fField = new Vector2Field("Value");
                if (value != null)
                {
                    Vec2f vecValue = (Vec2f)value;
                    v2fField.value = new Vector2(vecValue.X, vecValue.Y);
                }
                element = v2fField;
            }
            else if (type == typeof(Vec3f))
            {
                Vector3Field vec3Field = new Vector3Field("Value");
                if (value != null)
                {
                    Vec3f vecValue = (Vec3f)value;
                    vec3Field.value = new Vector3(vecValue.X, vecValue.Y, vecValue.Z);
                }
                element = vec3Field;
            }
            else if (type == typeof(Vec2i))
            {
                Vector2IntField iField = new Vector2IntField("Value");
                if (value != null)
                {
                    Vec2i vecValue = (Vec2i)value;
                    iField.value = new Vector2Int(vecValue.X, vecValue.Y);
                }
                element = iField;
            }
            else if (type == typeof(Vec3i))
            {
                Vector3IntField iField = new Vector3IntField("Value");
                if (value != null)
                {
                    Vec3i vecValue = (Vec3i)value;
                    iField.value = new Vector3Int(vecValue.x, vecValue.y, vecValue.z);
                }
                element = iField;
            }
            else
            {
                element = new Label("Field of type: " + type.ToString() + " isn't yet supported.");
            }

            // Todo deals with arrays.
            return element;
        }
    }
}
