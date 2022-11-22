using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TransformShaker))]
public class TransformShakerEditor : BaseEditor<TransformShaker>
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Test(0.1f);
        Test(0.2f);
        Test(0.3f);
        Test(0.4f);
        Test(0.5f);
        Test(0.6f);
        Test(0.7f);
        Test(0.8f);
        Test(0.9f);
        Test(1.0f);
    }

    private void Test(float value)
    {
        if (GUILayout.Button($"Shake {value}"))
        {
            target.Shake(value);
        }
    }
}
