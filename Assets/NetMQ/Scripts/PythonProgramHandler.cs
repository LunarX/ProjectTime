using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class PythonProgramHandler : MonoBehaviour
{
    private Process p;

    private static PythonProgramHandler instance;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Call this function to start running the python program
    public void RunProgram()
    {
        p = new Process();

        var exePath = Application.streamingAssetsPath + "\\Heart_rate_light\\NOGUI.exe";
        var wdPath = Application.streamingAssetsPath + "\\Heart_rate_light";

        p.StartInfo.UseShellExecute = true;

        p.StartInfo.FileName = exePath;
        p.StartInfo.WorkingDirectory = wdPath;

        p.Start();
    }

    // This will be called when the game is closed and will take care of closing the python program as well
    void OnDestroy()
    {
        if(p != null && !p.HasExited)
        {
            p.CloseMainWindow();
        }
    }
}
