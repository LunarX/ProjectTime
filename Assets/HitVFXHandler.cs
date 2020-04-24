using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitVFXHandler : MonoBehaviour
{
    [Header("Clicked Sprites SFX")]
    public SpriteRenderer rightSFX;
    public SpriteRenderer leftSFX;
    public SpriteRenderer upSFX;
    public SpriteRenderer downSFX;

    GameManager gm;



    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        rightSFX.enabled = false;
        leftSFX.enabled = false;
        upSFX.enabled = false;
        downSFX.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(gm.right))
        {
            rightSFX.enabled = true;
        }
        else if(Input.GetKeyUp(gm.right)) {
            rightSFX.enabled = false;
        }

        if (Input.GetKeyDown(gm.left))
        {
            leftSFX.enabled = true;
        }
        else if (Input.GetKeyUp(gm.left))
        {
            leftSFX.enabled = false;
        }

        if (Input.GetKeyDown(gm.up))
        {
            upSFX.enabled = true;
        }
        else if (Input.GetKeyUp(gm.up))
        {
            upSFX.enabled = false;
        }

        if (Input.GetKeyDown(gm.down))
        {
            downSFX.enabled = true;
        }
        else if (Input.GetKeyUp(gm.down))
        {
            downSFX.enabled = false;
        }
    }
}
