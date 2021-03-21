using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public Text scr;
    public static int score = 0;

    void Update()
    {
        if (int.Parse(scr.text) != score)
        {
            PowerCounter.powerCount += score - int.Parse(scr.text);
            scr.text = score.ToString();
        }
    }
}
