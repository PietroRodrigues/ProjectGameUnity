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

    public void AnimationController(float speed, bool atack, bool def, int life, bool victory)
    {
        animator.SetFloat("Speed", speed);
       
        animator.SetBool("atack", atack);

        animator.SetBool("def", def);

        animator.SetBool("victory", victory);

        animator.SetInteger("life", life);

    }

}
