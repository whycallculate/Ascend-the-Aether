using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR
    [CustomEditor(typeof(RelicScriptableObject))]
    [System.Serializable]
    class RelicEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            RelicScriptableObject card = (RelicScriptableObject)target;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("statRelic"));

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("statusRelic"));

            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();


        if (GUILayout.Button("Create Relic", GUILayout.MinWidth(50), GUILayout.MinHeight(10)))
            {
                if (card.statRelic.IsDataFilled())
                {
                    card.statRelic.RelicCreated();
                }
                if (card.statusRelic.IsDataFilled())
                {
                    card.statusRelic.RelicCreated();
                }

            }

            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
        }

    }
#endif
