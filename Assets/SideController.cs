using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideController : MonoBehaviour
{

    // Direction, lié à des ints, car plus simple pour les index des arrays
    private const int RIGHT = 0;
    private const int LEFT = 1;
    private const int UP = 2;
    private const int DOWN = 3;

    // Paramètres des Hit Particles
    [Header("Hit Particles")]
    public ParticleSystem psRight;
    public ParticleSystem psLeft;
    public ParticleSystem psUp;
    public ParticleSystem psDown;
    [HideInInspector]
    public static ParticleSystem[] pss = new ParticleSystem[4];
    [HideInInspector]
    public static Gradient yellowGradient;
    [HideInInspector]
    public static Gradient purpleGradient;

    // Touches pour l'utilisateur
    [Header("Playing Key Settings")]
    public KeyCode right = KeyCode.RightArrow;
    public KeyCode left = KeyCode.LeftArrow;
    public KeyCode up = KeyCode.UpArrow;
    public KeyCode down = KeyCode.DownArrow;

    // Start is called before the first frame update
    void Start()
    {
        // On rassemble les Particuls Systems dans un array
        pss[RIGHT] = psRight;
        pss[LEFT] = psLeft;
        pss[UP] = psUp;
        pss[DOWN] = psDown;

        foreach (ParticleSystem ps in pss)
            ps.Stop();

        // Couleur des Particules
        var myYellow = new Color(1f, 0.95f, 0f);
        var myPurple = new Color(0.7961f, 0.2980f, 0.9490f);

        yellowGradient = new Gradient();
        yellowGradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(myYellow, 0.0f), new GradientColorKey(myYellow, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(0.5f, 0.0f), new GradientAlphaKey(0.25f, 1.0f) }
        );

        purpleGradient = new Gradient();
        purpleGradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(myPurple, 0.0f), new GradientColorKey(myPurple, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(0.5f, 0.0f), new GradientAlphaKey(0.25f, 1.0f) }
        );
    }

    // Update is called once per frame
    void Update()
    {
        // Actions à effectuer, selon les actions de l'utilisateurs (les 4 touches directionnelles, flèches)
        if (Input.GetKeyDown(right))
            targetInitial.ClickTarget(RIGHT);
        if (Input.GetKeyDown(left))
            targetInitial.ClickTarget(LEFT);
        if (Input.GetKeyDown(up))
            targetInitial.ClickTarget(UP);
        if (Input.GetKeyDown(down))
            targetInitial.ClickTarget(DOWN);
    }


}
