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


[CustomEditor(typeof(AttackCardController))]
[System.Serializable]
public class AttackCardControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        AttackCardController attackCardController = (AttackCardController)target;
        
        EditorGUILayout.PropertyField(serializedObject.FindProperty("cardLegendary"));
        
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("energyCost"));

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        
        EditorGUILayout.PropertyField(serializedObject.FindProperty("duration"));
        
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        
        EditorGUILayout.PropertyField(serializedObject.FindProperty("damage"));

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        
        if (attackCardController.CardLegendary == Card_Enum.CardLegendaryEnum.LegendaryCard)
        {
            SerializedProperty arrayProperty = serializedObject.FindProperty("cardCombineLegendary");
            EditorGUILayout.PropertyField(arrayProperty, true);
        }
        else
        {
            SerializedProperty arrayProperty = serializedObject.FindProperty("cardCombineLegendary");
            EditorGUILayout.PropertyField(arrayProperty, false);
        }
        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();
    }
}

#endif
