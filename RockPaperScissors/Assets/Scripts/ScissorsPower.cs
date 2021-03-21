using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScissorsPower : StateMachineBehaviour
{
    public GameObject power;
    private GameObject sensor;
    private GameObject player;
    private GameObject scissors;
    private Collider2D coll;
    private GameObject pwr;
    public float dashSpeed;
    Vector2 dir;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        sensor = GameObject.FindGameObjectWithTag("DashSensor");
        player = GameObject.FindGameObjectWithTag("MainPlayer");
        scissors = GameObject.FindGameObjectWithTag("pscissors");
        coll = scissors.GetComponent<Collider2D>();
        coll.enabled = false;
        Vector2 x= Vector2.left;
        sensor.SetActive(false);
        dir = PlayerController.prevMoveVel.normalized;
        PlayerController.Dashh = dashSpeed * dir;
        if (player.transform.localScale.x == -1)
            x = Vector2.right;
        pwr=Instantiate(power, player.transform.position+new Vector3(0,0.5f,0), Quaternion.Euler(0,0,Vector2.SignedAngle(x,dir)), player.transform);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerController.Dashh = Vector2.zero;
        new WaitForSeconds(0.2f);
        coll.enabled = true;
        sensor.SetActive(true);
        Destroy(pwr);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
