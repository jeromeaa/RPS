using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public static int health;
    private int hp;
    public Image[] images;

    public GameObject gameOverPanel;

    void Start()
    {
        health = 3;
        hp = 3;
    }

    void Update()
    {
        if (health > hp)
        {
            if (health > 3)
            {
                health = 3;
            }
            else
            {
                hp = health;
                images[health-1].color = Color.white;
            }
        }
        else if (health < hp)
        {
            images[health].color = Color.grey;

            if (health <= 0)
            {
                Death();
            }
            else
            {
                hp = health;
            }
        }
    }

    private void Death()
    {
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
    }

}
