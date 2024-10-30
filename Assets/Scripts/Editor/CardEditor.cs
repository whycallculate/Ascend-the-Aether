using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR
    [CustomEditor(typeof(CardScriptableObject))]
    [System.Serializable]
    class CardEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            CardScriptableObject card = (CardScriptableObject)target;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("attackCard"));
            
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            
            EditorGUILayout.PropertyField(serializedObject.FindProperty("defenceCard"));
            
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            
            EditorGUILayout.PropertyField(serializedObject.FindProperty("abilityCard"));

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("strengthCard"));

            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();

            if (GUILayout.Button("Create Card", GUILayout.MinWidth(50), GUILayout.MinHeight(10)))
            {
                if (card.attackCard.IsDataFilled())
                {
                    card.attackCard.CardCreated();
                }
                if(card.defenceCard.IsDataFilled())
                {
                    card.defenceCard.CardCreated();
                }
                if(card.abilityCard.IsDataFilled())
                {
                    card.abilityCard.CardCreated();
                }
                if(card.strengthCard.IsDataFilled())
                {
                    card.strengthCard.CardCreated();
                }
            }

            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
        }

    }
#endif
