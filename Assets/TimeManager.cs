using UnityEngine;
using System.Collections;

public class TimeManager : MonoBehaviour
{
    [Header("Bullet Time Settings")]
    [Tooltip("The speed at which the game will be running during bullet time")]
    [Range(0, 1)]
    public float slowdownFactor = 0.05f;

    [Tooltip("Time after which the bullet time will start to disapear")]
    public float slowdownLength = 4f;

    [Tooltip("The time it takes to the bullet time to go back to normal speed")]
    public float recoveryLength = 1.5f;

    [Tooltip("The key to press to activate bullet time")]
    public KeyCode activateSlowdown = KeyCode.V;

    //private float activationTime = 0;
    private bool isRecovering = false;
    private float slowdownActualLength;

    void Start()
    {
        slowdownActualLength = slowdownLength * slowdownFactor;
    }

    void Update()
    {
        print(Time.timeScale);
        if (Input.GetKeyDown(activateSlowdown))
        {
            DoSlowmotion();
        }

        //if(Time.time - activationTime > slowdownActualLength && !isRecovering)
        //{
        //    isRecovering = true;
        //}

        if(isRecovering)
        {
            Time.timeScale += (1.0f / recoveryLength) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);

            if (Time.timeScale == 1.0f)
            {
                Time.fixedDeltaTime = Time.deltaTime;
                isRecovering = false;
            }
        }
    }

    private void BackToNormal()
    {
        Debug.Log("Starting Recovery");
        isRecovering = true;
    }

    public void DoSlowmotion()
    {
        Debug.Log("Activating Bullet Time");
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;

        //activationTime = Time.time;

        Invoke("BackToNormal", slowdownActualLength);
    }
}
