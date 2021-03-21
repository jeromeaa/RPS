using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashSensor : MonoBehaviour
{
    public GameObject player;
    public GameObject[] enemy = new GameObject[3];
    Vector2 dir;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == enemy[(CollisionResult.current - 1 + 3) % 3].tag)
        {
            //Debug.Log("triggerrr");
            dir = collision.gameObject.transform.position - player.transform.position;
            PlayerController.DashHit(dir);
        }
        
    }
}
