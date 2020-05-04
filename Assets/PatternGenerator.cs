using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternGenerator
{
    private List<List<(int[] directions, float timeOffset)>> patterns;
    private System.Random rand;

    private readonly Dictionary<string, int> keywords = new Dictionary<string, int> { { "UP", GameManager.UP }, { "DOWN", GameManager.DOWN }, { "LEFT", GameManager.LEFT }, { "RIGHT", GameManager.RIGHT } };

    public PatternGenerator()
    {
        rand = new System.Random(3);
        patterns = new List<List<(int[] directions, float timeOffset)>>();

        //var file = Resources.Load("patterns") as TextAsset;
        //var patternsSource = file.text;

        //string[] stringSeparators = new string[] { "\r\n\r\n", "\n\n" };
        //var textPatterns = patternsSource.Split(stringSeparators, StringSplitOptions.None);

        //foreach(var p in textPatterns)
        //{
        //    if(!String.IsNullOrEmpty(p))
        //    {
        //        List<(int[] directions, float timeOffset)> pattern = new List<(int[] directions, float timeOffset)>();

        //        var lines = p.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
        //        foreach (var line in lines)
        //        {
        //            if (!String.IsNullOrEmpty(line))
        //            {
        //                List<int> dir = new List<int>();
        //                float offset = -1f;

        //                var values = line.Split(' ');
        //                for(int i = 0; i<values.Length - 1; i++)
        //                {
        //                    //Debug.Log(i + " : " + values[i]);
        //                    dir.Add(keywords[values[i]]);
        //                }

        //                offset = float.Parse(values[values.Length - 1]);
        //                //Debug.Log("[" + dir.ToArray() + "]" + " - " + offset);

        //                pattern.Add((directions: dir.ToArray(), timeOffset: offset));
        //            }
        //        }

        //        patterns.Add(pattern);
        //        //Debug.Log("====");
        //    }
        //}

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

    // ..?
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
