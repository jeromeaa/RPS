using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rb;
    public static Vector2 prevMoveVel;
    private Vector2 moveVelocity;
    private bool FacingRight;

    //dash
    private static bool dash=false;
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    public static Vector2 Dashh;
    public static Vector2 dir=Vector2.zero;

    // power
    public Animator animator;

    //UI
    public Joystick joystick;

    public GameObject player;

    //private bool up = false;
    //private bool down = false;
    //public float jumpSpeed;
    //public float fallSpeed;
    //public float jumpHeight;
    //[SerializeField]
    //private float Timer;
    //private Vector2 Power;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dashTime = startDashTime;
        //Timer = jumpHeight / jumpSpeed;
    }

    private void Update()
    {
        float hor=0;
        float ver=0;
        if (Mathf.Abs(joystick.Horizontal) >= .2f)
            hor = joystick.Horizontal;
        if (Mathf.Abs(joystick.Vertical) >= .2f)
            ver = joystick.Vertical;
        Vector2 moveInput = new Vector2(hor, ver);
        if(moveInput!=Vector2.zero)
            prevMoveVel = moveVelocity;
        moveVelocity = moveInput.normalized * speed;
        
    }

    private void FixedUpdate()
    {
        //RockPower();
        DashValue();
        if ((joystick.Horizontal >0 && !FacingRight)||(joystick.Horizontal<0 && FacingRight))
            Flip();

        rb.MovePosition(rb.position + (moveVelocity+Dashh) * Time.fixedDeltaTime);
    }
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        FacingRight = !FacingRight;
        // Multiply the player's x local scale by -1.
        Vector3 theScale = player.transform.localScale;
        theScale.x *= -1;
        player.transform.localScale = theScale;
    }

    private void DashValue()
    {
        if (dash && dashTime > 0)
        {
            dashTime -= Time.deltaTime;
            if(dashTime>=startDashTime/2)
                Dashh= dashSpeed * dir;
            else if (dashTime < startDashTime / 2)
                Dashh = (-dashSpeed * dir );
        }
        else if (dash || dashTime<=0)
        {
            Dashh = Vector2.zero;
            rb.velocity = Vector2.zero;
            dashTime = startDashTime;
            dash = false;
        }
    }

    //private void RockPower()
    //{
    //    if (up && Timer >= 0)
    //    {
    //        Timer -= Time.deltaTime;
    //        Power = jumpSpeed * Vector2.up;
    //        if (Timer <= 0)
    //        {
    //            up = false;
    //            down = true;
    //            Timer = (jumpHeight)/ fallSpeed ;
    //        }
    //    }
    //    else if(down && Timer >= 0)
    //    {
    //        Timer -= Time.deltaTime;
    //        Power = fallSpeed * Vector2.down;
    //        if (Timer <= 0)
    //        {
    //            SuperPower();
    //            down = false;
    //            Timer = jumpHeight / jumpSpeed ;
    //            Power = Vector2.zero;
    //        }
    //    }
    //}

    public static void DashHit( Vector2 d)
    {
        dash = true;
        dir = d;
    }

    public void PowerClick()
    {
        animator.SetTrigger("power");
    }

    //private void SuperPower()
    //{
    //    Instantiate(shockwave, transform.position, Quaternion.identity);
    //}
}
