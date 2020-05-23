using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonGenerator: MonoBehaviour
{

    // Variables pour la génération aléatoire des cibles
    private static float oldTime;
    private static readonly Vector2[] acceptedDirections = { Vector2.left, Vector2.right, Vector2.down, Vector2.up };
    private static int randDir;
    private static float randPosiX;
    private static float randPosiY;
    private static Vector3 posiSkel;
    public static List<Vector2> newVerticies = new List<Vector2>();



    public static GameObject CreateSkel(int indexV)
    {

        GameObject go = new GameObject("Skeletton " + indexV);        // Création formelle d'un GameObject (le paramètre est son nom)

        // On génère aléatoirement sa position X et Y
        randPosiX = Random.Range(-7f, 7f);
        randPosiY = Random.Range(-7f, 7f);

        // Si on pas assez loin du centre, on l'éloigne
        if (Mathf.Abs(randPosiX) < 3)
            randPosiX = Mathf.Sign(randPosiX) * (Mathf.Abs(randPosiX) + 3);
        if (Mathf.Abs(randPosiY) < 3)
            randPosiY = Mathf.Sign(randPosiY) * (Mathf.Abs(randPosiY) + 3);

        // On met formellement sa position dans une variable
        posiSkel = new Vector3(randPosiX, randPosiY, 0);                                                // Bizarrement, ça enregistre -10 pour la coordonnée z, avec la souris... Donc on rectifie en remettant à 0

        
        Vector3 diff = posiSkel - Vector3.zero;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;


        Sprite image = Resources.Load<Sprite>("Enemy/hexagone_squelette");                                        // On récupère l'image du squelette
        SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();                                    // Rajoute le Component 'Sprite Renderer' pour l'objet
        renderer.sprite = image;                                                                        // Fait le lien entre l'image et l'objet

        go.transform.position = posiSkel;                                                               // Indique la position où placer la boule de feu
                                                                                                        //go.transform.LookAt(Vector3.zero);                                                            // Indique la rotation de l'objet
        go.transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
        go.transform.localScale = new Vector3(0.31f, 0.31f, 1f);

        Rigidbody2D rb = go.AddComponent<Rigidbody2D>() as Rigidbody2D;
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.simulated = true;

        BoxCollider2D bc = go.AddComponent<BoxCollider2D>() as BoxCollider2D;
        bc.isTrigger = true;
        bc.size = new Vector2(4.11f, 2.33f);

        SkeletonBehaviour sb = go.AddComponent<SkeletonBehaviour>();
        sb.init(indexV);

        return go;
    }
}
