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
    // Start is called before the first frame update
    void Start()
    {
        // Niveau par défault
        PlayerPrefs.SetInt("Difficulty", 0);

        m_Path = Application.dataPath;
        UnityEngine.Debug.Log("dataPath : " + m_Path);
        int firstStringPosition = m_Path.IndexOf("Asset");
        
        Path = m_Path.Substring(0, firstStringPosition);
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
        p.StartInfo.UseShellExecute = true;
        p.StartInfo.FileName = Path + "/Heart_rate_light/output/NOGUI/NOGUI.exe";
        p.StartInfo.WorkingDirectory = Path + "/Heart_rate_light/output/NOGUI";

        // Normalement, on doit pouvoir supprimer ça :
        //p.StartInfo.FileName = "C:\\Users\\Gibran\\Documents\\Gibran\\Ecole\\_Uni\\Master 2\\TimeRythm\\Heart_rate_light\\output\\NOGUI\\NOGUI.exe";
        //p.StartInfo.WorkingDirectory = "C:\\Users\\Gibran\\Documents\\Gibran\\Ecole\\_Uni\\Master 2\\TimeRythm\\Heart_rate_light\\output\\NOGUI";
        p.Start();
    }

    public void ResetBS()
    {
        PlayerPrefs.SetInt("BestScore", 0);
    }
}
