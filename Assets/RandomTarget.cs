using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTarget : MonoBehaviour {

    private const int RIGHT = 0;
    private const int LEFT = 1;
    private const int UP = 2;
    private const int DOWN = 3;


    private List<GameObject> right_stack = new List<GameObject>();
    private List<GameObject> left_stack = new List<GameObject>();
    private List<GameObject> up_stack = new List<GameObject>();
    private List<GameObject> down_stack = new List<GameObject>();
    private List<GameObject>[] stacks = new List<GameObject>[4];


    // Start is called before the first frame update
    void Start()
    {
        stacks[RIGHT] = right_stack;
        stacks[LEFT] = left_stack;
        stacks[UP] = up_stack;
        stacks[DOWN] = down_stack;
    }


    // Update is called once per frame
    void Update()
    {

        // Handles the destruction of missed targets
        // Iterate through all targets currently loaded, and find the ones that should 
        // be destroyed because the user missed it for too long and destroy them.
        //DeleteMissedTargets(right_stack);
        //DeleteMissedTargets(left_stack);
        //DeleteMissedTargets(up_stack);
        //DeleteMissedTargets(down_stack);
    }


    // Loops through the list given as argument, and checks for each target if it should
    // be considered as missed and destroyed, if so removes it from the list and destroys it.
    //void DeleteMissedTargets(List<GameObject> stack)
    //{
    //    for (int i = stack.Count - 1; i >= 0; i--)
    //    {
    //        Target t = stack[i].GetComponent(typeof(Target)) as Target;
    //        if (t.GetTimeBeforeDeletion() <= 0)
    //        {
    //            Destroy(stack[i]);
    //            stack.RemoveAt(i);
    //        }
    //    }
    //}

    



}
