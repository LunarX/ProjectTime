using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextEnd : MonoBehaviour
{
    [Header("Texte à modifier")]
    public TextMeshProUGUI changingText;
    private int finalScore;

    // Start is called before the first frame update
    void Start()
    {
        TextChange();
    }


    // Change le texte pour le score de fin
    public void TextChange()
    {
        finalScore = PlayerPrefs.GetInt("Score");
        changingText.GetComponent<TextMeshProUGUI>();
        changingText.text = "Ton score : " + finalScore.ToString();
    }
}
