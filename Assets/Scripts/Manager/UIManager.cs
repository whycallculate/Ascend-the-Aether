using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }
            return instance;
        }
    }

    [SerializeField] private CharacterUI characterUI;
    public CharacterUI CharacterUI { get { return characterUI; }  set { characterUI = value; } }

    public RectTransform[] cardPos;
    [SerializeField] private Button nextTourButton;
    public Button NextTourButton { get { return nextTourButton;}}

    [SerializeField] private TextMeshProUGUI energyNumber_Text;
    public TextMeshProUGUI EnergyNumber_Text {get { return energyNumber_Text;}}

}
