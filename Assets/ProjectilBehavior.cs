using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilBehavior : MonoBehaviour
{

    public int projectilSpeed = 1;
    private Transform rb;
    private Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Transform>();     // On récupère les infos concernant la position, rotation, etc...
        dir = Vector3.Normalize(rb.position) * 0.03f * projectilSpeed;      // Direction : Vector3.Normalize(rb.position) est le vecteur, normalisé (pour que toutes les boules aient la même vitesse) ; 0.03 pour diminuer la vitesse
    }

    // Update is called once per frame
    void Update()
    {
        
        rb.position = rb.position + dir ;                                           // Mise à jour de la position
        if (Mathf.Abs(rb.position.x) > 10 || Mathf.Abs(rb.position.y) > 10)
            {
            Destroy(gameObject);
        }
    }   

    private void OnTriggerEnter2D(Collider2D collision)                             // Nom de la fonction qui détecte une collision (est appelée si collision avec l'objet)
    {
        Destroy(gameObject);                                                        // Détruit l'objet
    }
}
