using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CollisionResult : MonoBehaviour
{
    public ParticleSystem pouf;
    public ParticleSystem healParticle;
    public ParticleSystem pwrParticle;
    public Text scr;

    public GameObject[] rps=new GameObject[3];
    public GameObject[] enemy = new GameObject[3];
    [HideInInspector]
    public static int current=-1;

    public Animator animator;
    public ParticleSystem stars;

    private IEnemy EnemyHit;

    //OnHit
    public SpriteMask mask;
    public float flashDuration;
    public float totalDuration;
    public float flashTimer;
    public bool damaged;

    //Shield
    public UnityEvent ShieldOn;
    public UnityEvent ShieldOff;


    private void Start()
    {
        damaged = false;
        current = -1;
        flashTimer = totalDuration;
        SwitchChar(Random.Range(0, 3));
    }

    private void Update()
    {
        if (damaged)
            FlashHit();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Hit");
        try
        {
            if (collision.gameObject.tag == enemy[current].tag)
            {
                //Debug.Log("rawa2");
            }
            else if (collision.gameObject.tag == enemy[(current + 1) % 3].tag && damaged == false)
            {
                //Debug.Log("die");
                //SceneManager.LoadScene("GameScene");
                animator.SetTrigger("damage");
                if (!ShieldCounter.shielded)
                    HealthManager.health--;
                else
                {
                    ShieldOff.Invoke();
                }
                if (HealthManager.health > 0)
                {
                    SetPlayerEnnemyColl(false);
                }
                damaged = true;
            }
            else if (collision.gameObject.tag == enemy[(current - 1 + 3) % 3].tag)
            {
                //Debug.Log("kill"); 
                //Instantiate(pouf, collision.gameObject.transform.position, Quaternion.Euler(-90, 0, 0));
                //Destroy(collision.gameObject);
                //score++;
                //ScoreAdded.Invoke();
                //scr.text = score.ToString();
                //Debug.Log("kill");
                //KillEnemy(collision.gameObject);
                EnemyHit =collision.gameObject.GetComponent<IEnemy>();
                EnemyHit.Kill();
            }
        }
        catch { }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "rock")
        {
            SwitchChar(0);
            stars.Play();
            PowerCounter.paperPower = false;
            //Debug.Log("rock");
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "paper")
        {
            SwitchChar(1);
            stars.Play();
            //Debug.Log("paper");
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "scissor")
        {
            SwitchChar(2);
            stars.Play();
            PowerCounter.paperPower = false;
            //Debug.Log("scissors");
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "random")
        {
            int i;
            do
            {
                i = Random.Range(0, 3);
                //Debug.Log(i.ToString());
            }
            while (i == current);
            SwitchChar(i);
            stars.Play();
            //Debug.Log("Done");
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "health")
        {
            HealthManager.health++;
            healParticle.Play();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "power_charge")
        {
            pwrParticle.Play();
            PowerCounter.powerCount = 10;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "shield")
        {
            ShieldOn.Invoke();
            Destroy(collision.gameObject);
        }

    }

    private void SwitchChar(int n)
    {
        if (n != current)
        {
            current = n;
            animator.SetInteger("current", current);
            animator.SetTrigger("switch");
            //rps[(n - 1 + 3) % 3].SetActive(false);
            //rps[(n + 1) % 3].SetActive(false);
            rps[n].SetActive(true);
            
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        ScoreScript.score = 0;
        HealthManager.health = 3;
        SceneManager.LoadScene(0);
    }

    private void FlashHit()
    {
        flashTimer -= Time.deltaTime;
        if (flashTimer > 0)
        {
            mask.enabled = ((flashTimer % flashDuration) < flashDuration / 2);
        }
        else
        {
            mask.enabled = false;
            damaged = false;
            SetPlayerEnnemyColl(true);
            flashTimer = totalDuration;
        }
    }

    private void SetPlayerEnnemyColl(bool b)
    {
        for (int i = 0; i < 3; i++)
        {
            Physics2D.IgnoreLayerCollision(rps[i].layer, enemy[(i + 1) % 3].layer,!b);
        }
    }
}
