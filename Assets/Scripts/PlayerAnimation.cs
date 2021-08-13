using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation
{
    Animator animator;


    public PlayerAnimation(Animator animator)
    {
        this.animator = animator;

    }

    public void AnimationController(float speed, bool atack, bool def,bool hit, int life, bool victory)
    {
        animator.SetFloat("speed", speed);
       
        animator.SetBool("atack", atack);

        animator.SetBool("def", def);

        animator.SetBool("victory", victory);

        animator.SetInteger("life", life);

        animator.SetBool("hit", hit);

    }

}
