using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileGenerator : MonoBehaviour
{

    public GameObject viseur;
    private Quaternion rotaViseur;
    private float angleViseur;
    private Vector3 posiSouris;
    private Transform transfoV;

    void Start()
    {
        transfoV = viseur.transform;
    }

    // Update is called once per frame
    void Update()
    {

        viseurDirection();

        // Get input from player
        if (Input.GetMouseButtonDown(0))
        {
            // On récupère la position de la souris
            Vector3 mousePosition = Input.mousePosition;                    
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
 
            mousePosition.z = 0;                                                                            // Bizarrement, ça enregistre -10 pour la coordonnée z, avec la souris... Donc on rectifie en remettant à 0
            Vector3 posiFire = Vector3.Normalize(mousePosition);
            posiFire.z = 0;

            float angle = Vector3.Angle(mousePosition, Vector3.right);                                      // Angle entre la position de la souris et l'axe x (1, 0) (vecteur avec le point origine (0, 0) )

            if (mousePosition.y < 0)                                                                        // 'angle' est entre 0 et 180, donc on a besoin de rajouter le signe +/- (- si on en dessous de l'axe x)
            {
                angle = 0 - angle;
            }

            Quaternion rota = Quaternion.Euler(0, 0, angle + 180);                                          // Variable pour la Rotation, de type Quaternion

            // Création de l'objet, et rajout des Components
            GameObject go = new GameObject("Boule de feu");                                                 // Création de l'objet

            Sprite image = Resources.Load<Sprite>("Fireball/lazer");
            //Sprite image = Resources.Load<Sprite>("Fireball/spritesheet-512px-by-197px-per-frame 1");       // On récupère l'image de la boule de feu      
            //AnimationClip animation_Fireball = Resources.Load<AnimationClip>("Fireball/Fireball_AC");       // On récupère l'animation de la boule de feu

            SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();                                    // Rajoute le Component 'Sprite Renderer' pour l'objet
            renderer.sprite = image;                                                                        // Fait le lien entre l'image et l'objet

            //Animator animator = go.AddComponent<Animator>();                                                // Rajoute le Component 'Animator'
            //animator.runtimeAnimatorController = Resources.Load("Fireball/Fireball_AC") as RuntimeAnimatorController;       // Ajoute l'animation à l'objet
            go.transform.position = posiFire;                                                          // Indique la position où placer la boule de feu
            go.transform.rotation = rota;                                                                   // Indique la rotation de l'objet
            Vector3 scaleChange = new Vector3(0.3f, 0.3f, 1f);
            go.transform.localScale = scaleChange;

            BoxCollider2D bc2D = go.AddComponent<BoxCollider2D>();                                          // Component qui permet la détection de collision
            bc2D.isTrigger = true;                                                                          // Permet qu'une action soit effectuée en cas de collision

            ProjectilBehavior pb = go.AddComponent<ProjectilBehavior>();                                    // Script qui définit le comportement de chaque boule de feu crée
        }

    }

    public void viseurDirection()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = new Vector2(
            mousePosition.x - transfoV.position.x,
            mousePosition.y - transfoV.position.y
            );

        transfoV.right = direction;
    }
}
