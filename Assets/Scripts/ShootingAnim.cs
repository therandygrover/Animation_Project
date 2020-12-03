using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingAnim : StateMachineBehaviour
{
    bool firedSparks = false;
    GameObject mfg;
    ParticleSystem[] sparks;
    Game game;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mfg = GameObject.FindGameObjectWithTag("Player");
        sparks = mfg.GetComponentsInChildren<ParticleSystem>();
        game = GameObject.FindObjectOfType<Game>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bool aoe = animator.GetBool("aoe");

        if (stateInfo.normalizedTime > 0.55f && !firedSparks && !aoe)
        {
            firedSparks = true;            
            foreach (ParticleSystem spark in sparks)
            {
                if (spark.name == "gunSparks" || spark.name == "explosion")
                spark.Play();
            }

            game.damageEnemy(10f);
        }

        if (stateInfo.normalizedTime > (38f/60f) && !firedSparks && aoe)
        {
            firedSparks = true;
            foreach (ParticleSystem spark in sparks)
            {
                if (spark.name == "bigSparks" || spark.name == "bigExplosion")
                    spark.Play();
            }

            game.damageEnemy(20f);
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
