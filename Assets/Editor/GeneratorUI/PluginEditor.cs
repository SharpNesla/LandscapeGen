using Assets.SimpleGenerator;
using UnityEditor;
using UnityEngine;

namespace Editor.GeneratorUI
{
    [CustomEditor(typeof(TerracoreGenerator))]
    public class PluginEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Generate"))
            {
                (target as TerracoreGenerator).Place();
            }
        }
    }
}