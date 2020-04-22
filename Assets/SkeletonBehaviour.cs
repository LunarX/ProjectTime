using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBehaviour : MonoBehaviour
{
    public int projectilSpeed = 5;      // Vitesse du squelette
    private Transform rb;               // Variable pour la position (mise à jour pour déplacer l'objet)
    private Vector3 dir;                // Direction de l'objet (incrémenté à la position actuelle)
    private int index;                  // Numéro d'identification de l'objet (pour pouvoir le supprimer)

    public void init(int indexV)
    {
        this.index = indexV;
    }

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Transform>();                                         // On récupère les infos concernant la position, rotation, etc...
        dir = Vector3.Normalize(rb.position) * 0.003f * projectilSpeed;         // Direction : Vector3.Normalize(rb.position) est le vecteur, normalisé (pour que toutes les boules aient la même vitesse) ; 0.03 pour diminuer la vitesse

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
            Destroy(Orchestrator.dicSkel[index]);                                                     // Détruit l'objet
            Orchestrator.numbSkel -= 1;
            ScoreManager.TargetHitted("skeletton", "center");
            Orchestrator.dicSkel.Remove(index);
        }

        // Si le skelette touche le cercle du centre
        string center = collision.gameObject.name;
        if (center == "Center")
        {
            // TODO : modifier la destruction, en détruisant l'objet spécifique, en non pas juste gameObject (il faut donc stocker les objets dans une liste)
            Destroy(Orchestrator.dicSkel[index]);                                                        // Détruit l'objet
            print("Machin " + index + " détruit !");
            Orchestrator.numbSkel -= 1;
            Orchestrator.dicSkel.Remove(index);
        }

    }

}
