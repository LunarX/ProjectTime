using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternGenerator
{
    private List<List<(int[] directions, float timeOffset)>> patterns;
    private System.Random rand;

    public PatternGenerator()
    {
        rand = new System.Random(3);
        patterns = new List<List<(int[] directions, float timeOffset)>>();

        patterns.Add(
            new List<(int[] directions, float timeOffset)>
            {
                (directions: new int[] { GameManager.UP }, 0f),
                (directions: new int[] { GameManager.UP }, 0.5f),
                (directions: new int[] { GameManager.LEFT, GameManager.RIGHT }, 1f)
            }
        );

        patterns.Add(
            new List<(int[] directions, float timeOffset)>
            {
                (directions: new int[] { GameManager.UP }, 0f),
                (directions: new int[] { GameManager.RIGHT }, 0.25f),
                (directions: new int[] { GameManager.DOWN }, 0.5f),
                (directions: new int[] { GameManager.LEFT }, 0.75f)
            }
        );

        patterns.Add(
            new List<(int[] directions, float timeOffset)>
            {
                (directions: new int[] { GameManager.UP }, 0f),
                (directions: new int[] { GameManager.LEFT }, 0.25f),
                (directions: new int[] { GameManager.DOWN }, 0.5f),
                (directions: new int[] { GameManager.RIGHT }, 0.75f),
                (directions: new int[] { GameManager.DOWN }, 1f),
                (directions: new int[] { GameManager.LEFT }, 1.25f),
                (directions: new int[] { GameManager.UP }, 1.5f)
            }
        );
    }

    public List<(int[], float)> GetRandomPattern()
    {
        var idx = rand.Next(patterns.Count);
        var pattern = new List<(int[] directions, float timeOffset)>(patterns[idx]);

        var rotation = rand.Next(4);

        for (var i= 0; i < pattern.Count; i++)
        {
            for(var j=0; j<pattern[i].directions.Length; j++)
            {
                pattern[i].directions[j] = (pattern[i].directions[j] + rotation) % 4;
            }
            //Debug.Log("[" + string.Join(", ", pattern[i].directions) + "]" + " - " + pattern[i].timeOffset);
        }

        return pattern;
    }
}
