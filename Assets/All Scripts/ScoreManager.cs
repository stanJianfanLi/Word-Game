using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ScoreManager: MonoBehaviour
{
    static private ScoreManager S;

    [Header("Set in Inspector")]
    public List<float> scoreFontSizes = new List<float> { 24, 36, 36, 1 };
    public Vector3 scoreMidPoint = new Vector3(1, 1, 0);
    public float scoreComboDelay = 0.5f;
    public float scoreTravelTime = 3f;

    private RectTransform rectTrans;

    void Awake()
    {
        S = this;
        rectTrans = GetComponent<RectTransform>();
    }

    static public void SCORE(Wyrd wyrd, int combo)
    {
        S.Score(wyrd, combo);
    }

    // Add to the score for this word
    // int combo is the number of this word in a combo
    void Score(Wyrd wyrd, int combo)
    {
        // Get the position of the first Letter in the wyrd
        Vector3 pt = wyrd.letters[0].transform.position;
        // Create a List<> of Bezier points for the FloatingScore
        List<Vector2> pts = new List<Vector2>();

        // Convert the pt to a ViewportPoint. ViewportPoints range from 0 to 1
        //  across the screen and are used for GUI coordinates
        pt = Camera.main.WorldToViewportPoint(pt);
        pt.z = 0;

        // Make pt the first Bezier point
        pts.Add(pt);

        // Add a second Bezier point
        pts.Add(scoreMidPoint);

        // Make the Scoreboard the last Bezier point
        pts.Add(rectTrans.anchorMax);

        // Set the value of the Floating Score
        int value = wyrd.letters.Count * combo;
        FloatingScore fs = Scoreboard.S.CreateFloatingScore(value, pts);

        fs.timeDuration = scoreTravelTime;
        fs.timeStart = Time.time + combo * scoreComboDelay;
        fs.fontSizes = scoreFontSizes;

        // Double the InOut Easing effect
        fs.easingCurve = Easing.InOut + Easing.InOut;

        // Make the text of the FloatingScore something like "3 x 2"
        string txt = wyrd.letters.Count.ToString();
        if (combo > 1)
        {
            txt += " x " + combo;
        }
        fs.GetComponent<Text>().text = txt;
    }
}