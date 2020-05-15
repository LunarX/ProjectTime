using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreUpdate : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI ComboText;
    public int score = 0;
    public float combo = 1;
    GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        ScoreChange();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        ScoreText.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        score = gm.sm.GetScore();
        combo = gm.sm.combos;
        ScoreChange();
        ComboChange();
    }

    public void ScoreChange()
    {
        ScoreText.text = "Score\n" + score.ToString();
    }

    public void ComboChange()
    {
        ComboText.text = "x " + System.Math.Round(combo, 2);
    }
}
