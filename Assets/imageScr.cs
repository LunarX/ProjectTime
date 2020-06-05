using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class imageScr : MonoBehaviour
{
    private PythonConnexion PC;
    private GameObject image;
    private Image img;
    private Sprite redDot;
    private Sprite greenDot;
    public string trois;

    void Start()
    {

        PC = GameObject.Find("PythonConnexion").GetComponent<PythonConnexion>();
        image = GameObject.Find("image");
        img = image.GetComponent<Image>();

        redDot = Resources.Load<Sprite>("Enemy/red");
        greenDot = Resources.Load<Sprite>("Enemy/green");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("BPM") == 1)
        {
            if (PC.equipementMesures.avgIsReady)
            {
                img.sprite = greenDot;
            }
            else
                img.sprite = redDot;
        }
        else
            img.sprite = greenDot;
    }
}
