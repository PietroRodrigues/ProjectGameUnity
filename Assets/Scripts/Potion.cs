using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            if(other.GetComponentInParent<Player>() != null)
            {
                if (other.GetComponentInParent<Player>().statos.Life < 10)
                {
                    if (other.GetComponentInParent<Player>().statos.Life + 3 > 10)
                        other.GetComponentInParent<Player>().statos.Life = 10;
                    else
                        other.GetComponentInParent<Player>().statos.Life += 3;
                    
                    Destroy(this.gameObject);

                }                

            }
        }
    }


}
