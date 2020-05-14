using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreUpdate : MonoBehaviour
{
    public TextMeshProUGUI changingText;
    public int score = 0;
    GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        TextChange();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        changingText.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        score = gm.sm.GetScore();
        TextChange();
    }

    public void TextChange()
    {
        changingText.text = "Score\n" + score.ToString();
    }
}
