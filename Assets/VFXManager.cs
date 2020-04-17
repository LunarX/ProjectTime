using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    // Paramètres des Hit Particles
    [Header("Hit Particles")]
    public ParticleSystem psRight;
    public ParticleSystem psLeft;
    public ParticleSystem psUp;
    public ParticleSystem psDown;
    private static ParticleSystem[] pss = new ParticleSystem[4];
    
    private static Gradient yellowGradient;
    private static Gradient purpleGradient;

    // Start is called before the first frame update
    void Start()
    {
        // On rassemble les Particuls Systems dans un array
        pss[GameManager.RIGHT] = psRight;
        pss[GameManager.LEFT] = psLeft;
        pss[GameManager.UP] = psUp;
        pss[GameManager.DOWN] = psDown;

        foreach (ParticleSystem ps in pss)
        {
            ps.Stop();
        }

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
    
    public void PlayHaxagones(int direction, int targetType)
    {
        var colorModule = pss[direction].colorOverLifetime;
        if (targetType == Target.SINGLE) { colorModule.color = yellowGradient; }
        else { colorModule.color = purpleGradient; }
        pss[direction].Play();
    }

}
