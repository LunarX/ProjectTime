using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilBehavior : MonoBehaviour
{
    private int projectilSpeed = 3;
    private Transform rb;
    private Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Transform>();     // On récupère les infos concernant la position, rotation, etc...
        dir = Vector3.Normalize(rb.position) * projectilSpeed * 5;      // Direction : Vector3.Normalize(rb.position) est le vecteur, normalisé (pour que toutes les boules aient la même vitesse) ; 0.03 pour diminuer la vitesse
    }

    // Update is called once per frame
    void Update()
    {
        rb.position = rb.position + dir * Time.deltaTime;                                           // Mise à jour de la position

        if (Mathf.Abs(rb.position.x) > 10 || Mathf.Abs(rb.position.y) > 10)
            Destroy(gameObject);
    }   

    private void OnTriggerEnter2D(Collider2D collision)                             // Nom de la fonction qui détecte une collision (est appelée si collision avec l'objet)
    {
        SkeletonBehaviour skeletton = collision.gameObject.GetComponent<SkeletonBehaviour>();     // Permet de s'assurer que la collision soit avec un missile (seule une boule de feu contient le Component 'ProjectilBehavior')

        // Si le squelette est touché par un missile, alors 'missile' ne sera pas nul
        if (skeletton != null)                                                        // Si c'est null, alors ce n'est pas un missile
            Destroy(gameObject);                                                        // Détruit l'objet
    }
}
