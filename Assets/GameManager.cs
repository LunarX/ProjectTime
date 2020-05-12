using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

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


    private List<GameObject> right_stack = new List<GameObject>();
    private List<GameObject> left_stack = new List<GameObject>();
    private List<GameObject> up_stack = new List<GameObject>();
    private List<GameObject> down_stack = new List<GameObject>();
    [HideInInspector]
    public List<GameObject>[] stacks = new List<GameObject>[4]; // Besoin d'y mettre en public, sinon, pas possible d'y accéder depuis Orchestrator

    public TargetGenerator tg;
    

    private AudioSource bgm;
    private AudioClip hard, normal, easy;

    public PythonConnexion PC;

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


        // PythonConnexion
        PC = GameObject.Find("PythonConnexion").GetComponent<PythonConnexion>();
        if (PC != null)
            print("PC trouvé !");

    }

    // Update is called once per frame
    void Update()
    {
        CheckTargets();
        //DebugTargetGeneration();
        CheckHealth();

        print("BPM = " + PC.equipementMesures.bpm);


    }

    void CheckHealth()
    {
        if (health.curHealth < 0)
        {
            print("T'ES MOOOOOOOOOOOORT !");
            PlayerPrefs.SetInt("Score", sm.GetScore());
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
        if (Input.GetKeyDown(right))
        {
            ClickTarget(RIGHT);
        }
        if (Input.GetKeyDown(left))
        {
            ClickTarget(LEFT);
        }
        if (Input.GetKeyDown(up))
        {
            ClickTarget(UP);
        }
        if (Input.GetKeyDown(down))
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
    }

    // Affiche le score (se met à jour automatiquement)
    void OnGUI()
    {
        var w = 100;
        var h = 50;
        GUI.Box(new Rect(Screen.width-w, Screen.height-h, w, h), "Score : " + sm.GetScore());

        GUI.Box(new Rect(0, Screen.height - h, w, h), "Score : " + PC.equipementMesures.bpm);
    }

}
