using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using FBLStage.Utils;
using System;

namespace FBLStage.Editor
{
    [CanEditMultipleObjects]
    [_CustomEditor(typeof(SlipDccsPool))]
    [_CustomEditor(typeof(SlipDccs))]
    [_CustomEditor(typeof(SlipFamilyDccs))]
    public class SlipDirectorEditor : UnityEditor.Editor
    {
        public static string[] hiddenPropertyNames =
        {
            "categories",
            "poolCategories"
        };

        public override void OnInspectorGUI()
        {
            var iterator = serializedObject.GetIterator();
            for (bool enterChildren = true; iterator.NextVisible(enterChildren); enterChildren = false)
            {
                if (iterator.name.Equals("m_Script"))
                {
                    GUI.enabled = false;
                    EditorGUILayout.PropertyField(iterator, true);
                    GUI.enabled = true;
                }
                else if (Array.IndexOf(hiddenPropertyNames, iterator.name) < 0)
                {
                    EditorGUILayout.PropertyField(iterator, true);
                }
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}