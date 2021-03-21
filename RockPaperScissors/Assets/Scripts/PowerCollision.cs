using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCollision : MonoBehaviour
{
    private IEnemy EnemyHit;
    public GameObject explosion;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "epaper")
        {
            EnemyHit = collision.gameObject.GetComponent<IEnemy>();
            EnemyHit.Kill();
        }
        if(collision.gameObject.tag == "erock")
        {
            //Explosion
            Instantiate(explosion,collision.gameObject.transform.position + new Vector3(0.15f,0.4f,0), Quaternion.Euler(0,0,Random.Range(0,360)));
            EnemyHit = collision.gameObject.GetComponent<IEnemy>();
            EnemyHit.Kill();
        }
    }
}
