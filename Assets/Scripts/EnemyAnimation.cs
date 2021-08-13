using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation
{
    Animator animator;

    public EnemyAnimation(Animator animator)
    {
        this.animator = animator;

    }

    public void AnimationController(float speed, bool atack, int life,bool hit, bool victory)
    {
        animator.SetFloat("speed", speed);

        animator.SetBool("atack", atack);

        animator.SetBool("victory", victory);

        animator.SetInteger("life", life);

        animator.SetBool("hit", hit);

    }
}
