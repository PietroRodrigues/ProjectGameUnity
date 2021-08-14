using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atacks : MonoBehaviour
{
    Enemy enemyScript;
    Player playerScript;

    private void Start()
    {
        if (transform.GetComponentInParent<Enemy>() != null)
        {
            enemyScript = transform.GetComponentInParent<Enemy>();
        }
        if (transform.GetComponentInParent<Player>() != null)
        {
            playerScript = transform.GetComponentInParent<Player>();
        }

    }

    public void EnemeyAtack1()
    {
        enemyScript.SkillAtack(1);        
    }

    public void EnemeyAtack2()
    {
        enemyScript.SkillAtack(2);
    }

    public void PlayerAtackParticle()
    {
        playerScript.SkillAtack();
    }

    public void hitOff()
    {
        if(enemyScript != null)
            enemyScript.hit = false;

        if (playerScript != null)
            playerScript.hit = false;

    }

}
