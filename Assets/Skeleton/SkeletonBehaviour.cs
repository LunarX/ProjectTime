using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBehaviour : MonoBehaviour
{
    [Header("Skeletton Key Settings")]
    public int projectilSpeed = 2;      // Vitesse du squelette
    private Transform rb;               // Variable pour la position (mise à jour pour déplacer l'objet)
    private Vector3 dir;                // Direction de l'objet (incrémenté à la position actuelle)
    private int index;                  // Numéro d'identification de l'objet (pour pouvoir le supprimer)

    GameManager gm;
    public VFXManager vfx;
    
    public void init(int indexV)
    {
        this.index = indexV;
    }

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Transform>();                                         // On récupère les infos concernant la position, rotation, etc...
        dir = Vector3.Normalize(rb.position) * projectilSpeed;         // Direction : Vector3.Normalize(rb.position) est le vecteur, normalisé (pour que toutes les boules aient la même vitesse) ; 0.03 pour diminuer la vitesse

        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        vfx = GameObject.Find("VFXManager").GetComponent<VFXManager>();

    }

    // Update is called once per frame
    void Update()
    {
        rb.position -= dir * Time.deltaTime;                                                         // Mise à jour de la position (par rapport au temps, et non pas des frames)
    }

    private void OnTriggerEnter2D(Collider2D collision)                             // Nom de la fonction qui détecte une collision (est appelée si collision avec l'objet)
    {
        print("Collision !");
        ProjectilBehavior missile = collision.gameObject.GetComponent<ProjectilBehavior>();     // Permet de s'assurer que la collision soit avec un missile (seule une boule de feu contient le Component 'ProjectilBehavior')

        // Si le squelette est touché par un missile, alors 'missile' ne sera pas nul
        if (missile != null)                                                        // Si c'est null, alors ce n'est pas un missile
        {
            
            Destroy(gameObject);                                                     // Détruit l'objet
            gm.sm.TargetHitted("skeletton", "center");
            vfx.PlayPlus10(gameObject.transform.position);      // Pop +5 / +10 
            Orchestrator.dicSkel.Remove(index);
            SoundManager.PlaySound("skeletton");
            print("paf !");
            
        }

        // Si le skelette touche le cercle du centre
        string center = collision.gameObject.name;
        if (center == "Center")
        {
            vfx.PlayMiss(gameObject.transform.position);        // Pop "miss"
            Destroy(Orchestrator.dicSkel[index]);                                                        // Détruit l'objet
            Orchestrator.dicSkel.Remove(index);
            gm.health.DamagePlayer(5);
            SoundManager.PlaySound("missSound");
            
        }

    }

}
