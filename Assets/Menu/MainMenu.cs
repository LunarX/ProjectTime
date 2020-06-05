using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    private PythonConnexion PC;
    private GameObject image;
    private Image img;
    void Start()
    {
        PC = GameObject.Find("PythonConnexion").GetComponent<PythonConnexion>();
        image = GameObject.Find("image");
        //img = img.GetComponent<Image>();
    }


    public void PlayGame ()
    {
        if (PC != null)
        {
            if (PC.equipementMesures.avgIsReady)
            {
                SceneManager.LoadScene("MainScene");
                print(PC.equipementMesures.averageBpm);
            }
                
        }
            
    }

    public void QuitGame()
    {
        Debug.Log("QUIT !");
        Application.Quit();
    }

}
