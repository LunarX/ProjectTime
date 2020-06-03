using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Diagnostics;

public class OptionMenu : MonoBehaviour
{
    Process p = new Process();
    public string difficulty;
    private string m_Path;
    private string Path;

    private PythonProgramHandler pph;

    // Start is called before the first frame update
    void Start()
    {
        // Niveau par défault
        PlayerPrefs.SetInt("Difficulty", 0);

        m_Path = Application.dataPath;
        UnityEngine.Debug.Log("dataPath : " + m_Path);
        int firstStringPosition = m_Path.IndexOf("Asset");
        
        Path = m_Path.Substring(0, firstStringPosition);

        pph = GameObject.Find("PythonProgramHandler").GetComponent<PythonProgramHandler>();
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

    public void LaunchBPM()
    {
        pph.RunProgram();
    }

    public void ResetBS()
    {
        PlayerPrefs.SetInt("BestScore", 0);
    }
}
