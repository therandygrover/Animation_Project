using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healAnim : StateMachineBehaviour
{
    bool firedSparks = false;
    GameObject guy;
    ParticleSystem[] healEffect;
    Game game;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        guy = GameObject.FindGameObjectWithTag("Guy");
        healEffect = guy.GetComponentsInChildren<ParticleSystem>();
        game = GameObject.FindObjectOfType<Game>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime > (40f/60f) && !firedSparks && !animator.GetBool("aoe"))
        {
            firedSparks = true;
            foreach (ParticleSystem spark in healEffect)
            {
                if (spark.name == "Heal particles")
                    spark.Play();
            }

            game.healGirl(20f);
        }
        if (stateInfo.normalizedTime > (20f / 60f) && !firedSparks && animator.GetBool("aoe"))
        {
            firedSparks = true;
            foreach (ParticleSystem spark in healEffect)
            {
                if (spark.name == "aoeHealEffect")
                    spark.Play();
            }

            game.healGirl(15f);
            game.healGuy(15f);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Attacking", false);
        animator.SetBool("aoe", false);

        firedSparks = false;
        foreach (ParticleSystem spark in healEffect)
        {
                spark.Stop();
        }
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
