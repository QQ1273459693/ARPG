using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_StabState : StateMachineBehaviour
{
    public int Pos;
    public Rigidbody2D rb;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (rb==null)
        {
            rb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        }
        switch (Pos)
        {
            case 1:
                PlayerController.instance.Stab = true;
                PlayerController.instance.StabDir = 2;
                break;
            case 2:
                PlayerController.instance.Stab = true;
                PlayerController.instance.StabDir = -2;
                break;
            case 3:
                PlayerController.instance.Stab = true;
                PlayerController.instance.StabDir = 1;
                break;
            default:
                break;
        }
        //rb.AddForceAtPosition
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
        
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    rb.gravityScale = 0;
    //}

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
