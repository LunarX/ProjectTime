using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    private int score = 0;
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
        ScoreChange();

        if ((ResumeMenu.GameIsPaused) && (Input.GetKey(KeyCode.Space))) {
            gm.sm.score += (int)Mathf.Round(100);
            print("Augmentation :" + gm.sm.score);
        }
    }

    public void ScoreChange()
    {
        ScoreText.text = "Score : " + score.ToString();
    }

    public void QuitGame()
    {
        Debug.Log("QUIT !");
        Application.Quit();
    }
}
