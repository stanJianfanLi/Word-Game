using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelPicker : MonoBehaviour
{
    static private System.Random rng;

    [Header("Set in Inspector")]
    public int randomSeed = 12345;

    void Awake()
    {
        rng = new System.Random(randomSeed);
    }

    static public int Next(int max = -1)
    {
        if (max == -1)
        {
            return rng.Next();
        }
        else
        {
            return rng.Next(max);
        }
    }
}