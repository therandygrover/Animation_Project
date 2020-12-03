using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackAnim : StateMachineBehaviour
{
    bool firedSparks = false;
    GameObject spy;
    ParticleSystem[] spray;
    Game game;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        spy = GameObject.FindGameObjectWithTag("Spider");
        spray = spy.GetComponentsInChildren<ParticleSystem>();
        game = GameObject.FindObjectOfType<Game>();
    }

// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
{
        if (stateInfo.normalizedTime > 0.55f && !firedSparks)
        {
            firedSparks = true;
            if (animator.GetBool("aoe"))
            {
                foreach (ParticleSystem spark in spray)
                {
                    spark.Play();
                }
            }

            if (animator.GetBool("aoe"))
            {
                game.damageGirl(10f);
                game.damageGuy(10f);
            }
            else
            {
                if (spy.GetComponent<character>().charToHit == 1)
                    game.damageGuy(15f);
                else
                    game.damageGirl(15f);
                
            }
    }
}

// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
{
    animator.SetBool("Attacking", false);
    animator.SetBool("aoe", false);
    firedSparks = false;
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
