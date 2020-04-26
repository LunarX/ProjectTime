using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextEnd : MonoBehaviour
{
    public TextMeshProUGUI changingText;
    public int finalScore;
    // Start is called before the first frame update
    void Start()
    {
        TextChange();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TextChange()
    {
        finalScore = PlayerPrefs.GetInt("Score");
        changingText.GetComponent<TextMeshProUGUI>();
        changingText.text = "Ton score : " + finalScore.ToString();
    }
}
