using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Identifier))]
public class IdentifierEditor : BaseEditor<Identifier>
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        IdentifierObject obj = target.GetObject();

        if (obj == null)
        {
            GUILayout.Label($"None ({nameof(IdentifierObject)} null)");
        }
        else
        {
            IdentifierObjectCollection col = obj.GetDependencies();
            
            if (col == null)
            {
                GUILayout.Label($"None ({nameof(IdentifierObjectCollection)} null)");
            }
            else
            {
                List<IdentifierObject> list = col.Get();

                if (list == null)
                {
                    GUILayout.Label($"None ({nameof(List<IdentifierObject>)} null)");
                }
                else
                {
                    int length = list.Count;
                    GUILayout.Box($"Dependencies ({length})");

                    for (int i = 0; i < length; i++)
                    {
                        IdentifierObject iid = list[i];
                        GUILayout.Label($"{i} - {(iid == null ? "null" : iid.GetKey())}");
                    }
                }
            }
        }
    }
}
