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
        if(Input.GetKeyDown(gm.right) || Input.GetKeyDown(gm.right2))
        {
            rightSFX.enabled = true;
        }
        else if(Input.GetKeyUp(gm.right) || Input.GetKeyUp(gm.right2)) {
            rightSFX.enabled = false;
        }

        if (Input.GetKeyDown(gm.left) || Input.GetKeyDown(gm.left2))
        {
            leftSFX.enabled = true;
        }
        else if (Input.GetKeyUp(gm.left) || Input.GetKeyUp(gm.left2))
        {
            leftSFX.enabled = false;
        }

        if (Input.GetKeyDown(gm.up) || Input.GetKeyDown(gm.up2))
        {
            upSFX.enabled = true;
        }
        else if (Input.GetKeyUp(gm.up) || Input.GetKeyUp(gm.up2))
        {
            upSFX.enabled = false;
        }

        if (Input.GetKeyDown(gm.down) || Input.GetKeyDown(gm.down2))
        {
            downSFX.enabled = true;
        }
        else if (Input.GetKeyUp(gm.down) || Input.GetKeyUp(gm.down2))
        {
            downSFX.enabled = false;
        }
    }
}
