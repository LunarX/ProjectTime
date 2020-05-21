using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Diagnostics;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public const int RIGHT = 0;
    public const int UP = 1;
    public const int LEFT = 2;
    public const int DOWN = 3;


    [Header("Playing Key Settings")]
    public KeyCode right = KeyCode.RightArrow;
    public KeyCode left = KeyCode.LeftArrow;
    public KeyCode up = KeyCode.UpArrow;
    public KeyCode down = KeyCode.DownArrow;
    public KeyCode right2 = KeyCode.D;
    public KeyCode left2 = KeyCode.A;
    public KeyCode up2 = KeyCode.W;
    public KeyCode down2 = KeyCode.S;

    [Header("Settings")]
    [Tooltip("Distance to the origin of the target when it spawns")]
    public float maxRange = 4.5f;
    [Tooltip("Distance to the origin of the target when it has to stop because it reached the center")]
    public float minRange = 0.74f;
    [Tooltip("Scaling factor of the size of the targets' sprites")]
    public float scalingFactor = 0.85f;
    public VFXManager vfx;

    private const float centerRadius = 0.175f;
    private const float borderRadius = 0.35f;

    //[Tooltip("Time the target sticks at the end of its path before being considered as missed")]
    //public float disapearanceTime = 0.2f;
    //[Tooltip("Time it takes for a target to scale to its final size before starting to move")]
    //public float scalingTime = 1.2f;

    [HideInInspector]
    public ScoreManager sm;


    [Header("Testing Key Settings")]
    public KeyCode t_up = KeyCode.W;
    public KeyCode t_down = KeyCode.S;
    public KeyCode t_left = KeyCode.A;
    public KeyCode t_right = KeyCode.D;
    public KeyCode t_double_vertical = KeyCode.E;
    public KeyCode t_double_horizontal = KeyCode.Q;

    [Header("Santé du joueur :")]
    public HealthBar healthBar;
    [HideInInspector]
    public Health health;

    private int bestscore;

    private List<GameObject> right_stack = new List<GameObject>();
    private List<GameObject> left_stack = new List<GameObject>();
    private List<GameObject> up_stack = new List<GameObject>();
    private List<GameObject> down_stack = new List<GameObject>();
    [HideInInspector]
    public List<GameObject>[] stacks = new List<GameObject>[4]; // Besoin d'y mettre en public, sinon, pas possible d'y accéder depuis Orchestrator

    public TargetGenerator tg;


    private AudioSource bgm;
    private AudioClip hard, normal, easy;
    private Orchestrator orch;

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

    private float slowdownActualLength;
    private bool isRecovering = false;


    // Start is called before the first frame update
    void Start()
    {
        stacks[RIGHT] = right_stack;
        stacks[LEFT] = left_stack;
        stacks[UP] = up_stack;
        stacks[DOWN] = down_stack;

        tg = new TargetGenerator(maxRange, minRange, scalingFactor);
        health = GetComponent<Health>();
        health.healthBar = healthBar;

        sm = new ScoreManager();
        //sm = GetComponent<ScoreManager>()


        // BGM Management
        AudioClip[] clips = new AudioClip[3];
        clips[2] = Resources.Load<AudioClip>("BGM/BossMain");
        clips[1] = Resources.Load<AudioClip>("BGM/RoccoW-Electric_Donkey_Muscles");
        clips[0] = Resources.Load<AudioClip>("BGM/Komiku-Together_we_are_stronger");

        bgm = gameObject.AddComponent<AudioSource>();
        bgm.loop = true;
        bgm.clip = clips[PlayerPrefs.GetInt("Difficulty")];
        bgm.Play();

        bestscore = PlayerPrefs.GetInt("BestScore");
        print("Best score :" + bestscore);

        orch = GetComponent<Orchestrator>();

        slowdownActualLength = slowdownLength * slowdownFactor;
    }

    void Update()
    {
        CheckTargets();
        CheckHealth();

        //print("difficulty: " + (1.5f - 0.5f * orch.GetDifficulty()));
        //bgm.pitch = (2 - orch.GetDifficulty());
        bgm.pitch = (1.5f - 0.5f*orch.GetDifficulty());

        if (Input.GetKeyDown(activateSlowdown))
        {
            DoSlowmotion();
        }

        if (isRecovering)
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

    void CheckHealth()
    {
        if (health.curHealth < 0)
        {
            PlayerPrefs.SetInt("Score", sm.GetScore());
            if (sm.GetScore() > bestscore)
                PlayerPrefs.SetInt("BestScore", sm.GetScore());
            SceneManager.LoadScene("EndMenu");
        }
    }

    void DebugTargetGeneration()
    {
        // For testing purposes, generate targets using keyboard.
        if (Input.GetKeyDown(t_up))
        {
            GameObject t = tg.GenerateSingleTarget(UP, 3f, 0.2f);
            up_stack.Add(t);
        }
        if (Input.GetKeyDown(t_down))
        {
            GameObject t = tg.GenerateSingleTarget(DOWN, 3f, 0.2f);
            down_stack.Add(t);
        }
        if (Input.GetKeyDown(t_left))
        {
            GameObject t = tg.GenerateSingleTarget(LEFT, 3f, 0.2f);
            left_stack.Add(t);
        }
        if (Input.GetKeyDown(t_right))
        {
            GameObject t = tg.GenerateSingleTarget(RIGHT, 3f, 0.2f);
            right_stack.Add(t);
        }

        if (Input.GetKeyDown(t_double_horizontal))
        {
            var ts = tg.GenerateDoubleTarget(LEFT, RIGHT, 3f, 0.2f);
            right_stack.Add(ts.Item1);
            left_stack.Add(ts.Item2);
        }

        if (Input.GetKeyDown(t_double_vertical))
        {
            var ts = tg.GenerateDoubleTarget(UP, DOWN, 3f, 0.2f);
            down_stack.Add(ts.Item1);
            up_stack.Add(ts.Item2);
        }
    }

    void CheckTargets()
    {
        // Handles the click hit of targets
        if (Input.GetKeyDown(right) || Input.GetKeyDown(right2))
        {
            ClickTarget(RIGHT);
        }
        if (Input.GetKeyDown(left) || Input.GetKeyDown(left2))
        {
            ClickTarget(LEFT);
        }
        if (Input.GetKeyDown(up) || Input.GetKeyDown(up2))
        {
            ClickTarget(UP);
        }
        if (Input.GetKeyDown(down) || Input.GetKeyDown(down2))
        {
            ClickTarget(DOWN);
        }

        // Handles the destruction of missed targets
        // For each stack check if the oldest target should be destroyed because the user missed
        // it for too long and destroy them.
        DeleteMissedTargets(RIGHT);
        DeleteMissedTargets(LEFT);
        DeleteMissedTargets(UP);
        DeleteMissedTargets(DOWN);

    }

    // Loops through the list given as argument, and checks for each target if it should
    // be considered as missed and destroyed, if so removes it from the list and destroys it.
    void DeleteMissedTargets(int direction)
    {
        if (stacks[direction].Count > 0)
        {
            int i = 0;
            Target t = stacks[direction][i].GetComponent(typeof(Target)) as Target;
            if (t.GetTimeBeforeDeletion() <= 0)
            {
                vfx.PlayMiss(t.transform.position);
                Destroy(stacks[direction][i]);
                stacks[direction].RemoveAt(i);
                OnMiss();
            }
        }
    }

    // Computes if a target has been hit or not and removes it if it does.
    void ClickTarget(int direction)
    {
        //List<GameObject> stack
        if (stacks[direction].Count > 0)
        {
            var idx = 0; // stacks[direction].Count - 1;
            var t = stacks[direction][idx].GetComponent(typeof(Target)) as Target;
            var d = t.GetDistanceLeft();    // Distance entre la cible et le controlleur (?)
            //print("D = " + d);
            if (d < borderRadius)           // Si on a atteint au suffisamment bon moment
            {
                vfx.PlayHaxagones(direction, t.GetTargetType());    // Active les effets visuels ??

                Destroy(stacks[direction][idx]);
                stacks[direction].RemoveAt(idx);

                if(d < centerRadius)        // Si la cible a été atteint au centre (meilleur score)
                {
                    sm.TargetHitted("Circle", "center");
                    vfx.PlayPlus10(t.transform.position);
                }
                else                        // TODO : A repenser : impossible d'atteindre cette condition... Les cibles sont trop rapide et l'écart trop petit pour atteindre la cible, mais pas le centre...
                {
                    sm.TargetHitted("Circle", "border");
                    vfx.PlayPlus5(t.transform.position);
                }
                SoundManager.PlaySound("targetHit");
            }

        }
    }

    // OnMiss is called each time a target is missed
    void OnMiss()
    {
        health.DamagePlayer(10);
        sm.combos = 1;
    }

    void DoSlowmotion()
    {
        print("Activating Bullet Time");
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;

        Invoke("BackToNormal", slowdownActualLength);
    }

    private void BackToNormal()
    {
        print("Starting Recovery");
        isRecovering = true;
    }
}
