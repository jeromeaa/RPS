using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerCounter : MonoBehaviour
{
    public static float powerCount;
    private float count;

    public Button powerBtn;

    public static bool paperPower=false;
    public float paperPowerDuration=5f;

    public Animator animator;

    private void Start()
    {
        paperPower = false;
        powerCount = 0;
        powerBtn.interactable = false;
        transform.localScale = new Vector3(1, 0, 1);
    }

    private void Update()
    {
        if (powerCount != count)
        {
            if (powerCount > 10)
            {
                powerCount = 10;
                count = 10;
                transform.localScale = new Vector3(1, 1, 1);
                if (powerBtn.interactable == false)
                    powerBtn.interactable = true;
            }
            else
            {
                count = powerCount;
                transform.localScale = new Vector3(1, 0.1f * powerCount, 1);
                if (powerCount == 10 && !paperPower)
                {
                    powerBtn.interactable = true;
                }
            }
        }
    }
    private void FixedUpdate()
    {
        if (paperPower)
        {
            if (powerCount>0)
            {
                powerCount -= 0.02f * (10 / paperPowerDuration);
            }
            else
            {
                paperPower = false;
                powerCount = 0;
                animator.SetTrigger("stop");
            }
        }
    }

    public void PowerReset()
    {
        powerBtn.interactable = false;

        if (CollisionResult.current == 0 || CollisionResult.current == 2)
        {
            powerCount = 0;
            
            transform.localScale = new Vector3(1, 0, 1);
        }
        else if (CollisionResult.current == 1)
        {
            paperPower = true;
        }
    }
}
