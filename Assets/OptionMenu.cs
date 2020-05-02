using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionMenu : MonoBehaviour
{

    public string difficulty;
    // Start is called before the first frame update
    void Start()
    {
        // Niveau par défault
        PlayerPrefs.SetInt("Difficulty", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void easyDifficulty()
    {
        difficulty = "easy";
        // METTRE LA DIFFICULTE dans PLAYER_INFO
        PlayerPrefs.SetInt("Difficulty", 0);
    }
    public void mediumDifficulty()
    {
        difficulty = "medium";
        PlayerPrefs.SetInt("Difficulty", 1);
    }
    public void hardDifficulty()
    {
        difficulty = "hard";
        PlayerPrefs.SetInt("Difficulty", 2);
    }
}
