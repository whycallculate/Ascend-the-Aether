using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR

#region  Card editör

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
    
#endregion


#region  Attack Card Editör

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


#endregion


#region  Ability Card Editör

[CustomEditor(typeof(AbilityCardController))]
[System.Serializable]
public class AbilityCardControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        AbilityCardController abilityCardController = (AbilityCardController)target;

        
        
        EditorGUILayout.PropertyField(serializedObject.FindProperty("cardLegendary"));
        
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("energyCost"));

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        
        EditorGUILayout.PropertyField(serializedObject.FindProperty("duration"));
        
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        
        EditorGUILayout.PropertyField(serializedObject.FindProperty("ability"));

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        
        if (abilityCardController.CardLegendary == Card_Enum.CardLegendaryEnum.LegendaryCard)
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

#endregion


#region  Defence Card Editör

[CustomEditor(typeof(DefenceCardController))]
[System.Serializable]
public class DefenceCardControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DefenceCardController defenceCardController = (DefenceCardController)target;

        
        
        EditorGUILayout.PropertyField(serializedObject.FindProperty("cardLegendary"));
        
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("energyCost"));

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        
        EditorGUILayout.PropertyField(serializedObject.FindProperty("duration"));
        
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        
        EditorGUILayout.PropertyField(serializedObject.FindProperty("defence"));

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        
        EditorGUILayout.PropertyField(serializedObject.FindProperty("cardMovement"));
        if (defenceCardController.CardLegendary == Card_Enum.CardLegendaryEnum.LegendaryCard)
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

#endregion


#region  Strength Card Editör

[CustomEditor(typeof(StrengthCardController))]
[System.Serializable]
public class StrengthCardControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        StrengthCardController strengthCardController = (StrengthCardController)target;

        
        EditorGUILayout.PropertyField(serializedObject.FindProperty("cardLegendary"));
        
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("energyCost"));

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        
        EditorGUILayout.PropertyField(serializedObject.FindProperty("duration"));
        
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        
        EditorGUILayout.PropertyField(serializedObject.FindProperty("strength"));

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("cardMovement"));

        
        if (strengthCardController.CardLegendary == Card_Enum.CardLegendaryEnum.LegendaryCard)
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

#endregion



#endif
