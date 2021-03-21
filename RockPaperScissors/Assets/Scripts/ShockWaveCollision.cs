using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWaveCollision : MonoBehaviour
{
    public GameObject pouf;
    private IEnemy EnemyHit;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "escissors" || collision.gameObject.tag == "epaper")
        {
            EnemyHit = collision.gameObject.GetComponent<IEnemy>();
            EnemyHit.Kill();
        }
    }
}
