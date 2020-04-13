using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapHandler : MonoBehaviour
{
    [Header("Clicked Sprites SFX")]
    public SpriteRenderer rightSFX;
    public SpriteRenderer leftSFX;
    public SpriteRenderer upSFX;
    public SpriteRenderer downSFX;

    [Header("Key Settings")]
    public KeyCode right = KeyCode.RightArrow;
    public KeyCode left = KeyCode.LeftArrow;
    public KeyCode up = KeyCode.UpArrow;
    public KeyCode down = KeyCode.DownArrow;



    // Start is called before the first frame update
    void Start()
    {
        rightSFX.enabled = false;
        leftSFX.enabled = false;
        upSFX.enabled = false;
        downSFX.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(right))
        {
            rightSFX.enabled = true;
        }
        else if(Input.GetKeyUp(right)) {
            rightSFX.enabled = false;
        }

        if (Input.GetKeyDown(left))
        {
            leftSFX.enabled = true;
        }
        else if (Input.GetKeyUp(left))
        {
            leftSFX.enabled = false;
        }

        if (Input.GetKeyDown(up))
        {
            upSFX.enabled = true;
        }
        else if (Input.GetKeyUp(up))
        {
            upSFX.enabled = false;
        }

        if (Input.GetKeyDown(down))
        {
            downSFX.enabled = true;
        }
        else if (Input.GetKeyUp(down))
        {
            downSFX.enabled = false;
        }
    }
}
