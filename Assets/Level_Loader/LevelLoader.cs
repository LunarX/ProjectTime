using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    private PythonConnexion PC;
    [Header("Elements pour la transition")]
    public Animator transition;
    public float transitionTime = 1f;

    private GameObject image;
    private Image img;

    void Start()
    {
        PC = GameObject.Find("PythonConnexion").GetComponent<PythonConnexion>();
        image = GameObject.Find("image");
        //img = img.GetComponent<Image>();
        PlayerPrefs.SetInt("BPM", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            LoadNextLevel();
        }




    }

    public void LoadNextLevel()
    {
        //SceneManager.LoadScene(1);
        if (PlayerPrefs.GetInt("BPM") == 1)
        {
            if (PC.equipementMesures.avgIsReady)
            {
                StartCoroutine(LoadLevel(1));
            }
            else
                print("NON !");

        }
        else
        {
            StartCoroutine(LoadLevel(1));
            print("PC pas trouvé !");
        }
            
        
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        // Play animation
        transition.SetTrigger("Start");

        // Wait
        yield return new WaitForSeconds(transitionTime);

        // Load scene
        SceneManager.LoadScene(levelIndex);
    }

    public void PlayGame()
    {
        //SceneManager.LoadScene("MainScene");

        LoadNextLevel();
    }

    public void QuitGame()
    {
        Debug.Log("QUIT !");
        Application.Quit();
    }
}
