using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldCounter : MonoBehaviour
{
    public static bool shielded;

    public float shieldDuration=10f;
    public static float shieldcount;

    public GameObject Shield;
    public Image image;

    public Animator animator;

    private void Start()
    {
        SetShield(false);
    }

    private void FixedUpdate()
    {
        if (shielded)
        {
            if (shieldcount > 0)
            {
                shieldcount -= Time.deltaTime;
                if (shieldcount < (shieldDuration/3) && animator.GetCurrentAnimatorStateInfo(0).IsName("shield"))
                {
                    animator.SetTrigger("end");
                }
            }
            else
            {
                SetShield(false);
            }
        }
    }
    public void SetShield(bool s)
    {
        if (s && shielded)
            animator.SetTrigger("redo");
        shieldcount = shieldDuration;
        shielded = s;
        Shield.SetActive(s);
        image.enabled = s;
    }
}
