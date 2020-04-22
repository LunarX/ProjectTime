using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBehaviour : MonoBehaviour
{
    public int projectilSpeed = 2;
    private Transform rb;
    private Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Transform>();     // On récupère les infos concernant la position, rotation, etc...
        dir = Vector3.Normalize(rb.position) * 0.003f * projectilSpeed;      // Direction : Vector3.Normalize(rb.position) est le vecteur, normalisé (pour que toutes les boules aient la même vitesse) ; 0.03 pour diminuer la vitesse

    }

    // Update is called once per frame
    void Update()
    {
        // A modifier (pas besoin de recalculer à chaque fois la direction)
        
        rb.position -= dir;                                           // Mise à jour de la position
    }

    private void OnTriggerEnter2D(Collider2D collision)                             // Nom de la fonction qui détecte une collision (est appelée si collision avec l'objet)
    {

        ProjectilBehavior missile = collision.gameObject.GetComponent<ProjectilBehavior>();     // Permet de s'assurer que la collision soit avec un missile (seule une boule de feu contient le Component 'ProjectilBehavior')

        // Si le squelette est touché par un missile, alors 'missile' ne sera pas nul
        if (missile != null)                                                        // Si c'est null, alors ce n'est pas un missile
        {
            Destroy(gameObject);                                                        // Détruit l'objet
            ScoreManager.TargetHitted("skeletton", "center");
        }

        // Si le skelette touche le cercle du centre
        string center = collision.gameObject.name;
        if (center == "Center")
        {
            Destroy(gameObject);                                                        // Détruit l'objet
            Orchestrator.numbSkel -= 1;
        }

    }

}
