using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy
{
    public float speed=5f;
    private float moveSpeed;

    private Vector2 moveSpot;
    public float minX=-58f;
    public float maxX=57f;
    public float minY=-33f;
    public float maxY=32f;

    public GameObject playerFollow;
    public GameObject playerFlee;

    private Transform target;
    private bool follow=false;
    private bool flee = false;

    private bool FacingRight;

    public ParticleSystem pouf;

    void Start()
    {
        moveSpot= new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        moveSpeed = speed;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    void FixedUpdate()
    {
        if (follow) 
        {
            moveSpot = target.position;
        }

        if (flee)
        {
            moveSpot = target.position;
        }
        
        transform.position = (Vector2.MoveTowards(transform.position, moveSpot, moveSpeed * Time.deltaTime));

        if ((transform.position.x > moveSpot.x && FacingRight) || (transform.position.x < moveSpot.x && !FacingRight) && moveSpeed>0)
            Flip();
        
        if (Vector2.Distance(transform.position, moveSpot) < 0.2f)
        {
            moveSpot= new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            moveSpeed = speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == playerFollow.tag)
        {
            follow = true;
            moveSpeed = 1.1f * speed;
        }
        else if(collision.gameObject.tag == playerFlee.tag)
        {
            flee = true;
            moveSpeed = -speed;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == playerFollow.tag || collision.gameObject.tag==playerFlee.tag)
        {
            follow = false;
            flee = false;
            moveSpot = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            moveSpeed = speed;
        }

    }
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        FacingRight = !FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void Kill()
    {
        ScoreScript.score++;
        Instantiate(pouf, transform.position, Quaternion.Euler(-90, 0, 0));
        Destroy(gameObject);
    }
}
