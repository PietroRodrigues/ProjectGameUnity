using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] Transform miraTravada;
    Vector3 center;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
       
    }

    private void Update()
    {
        
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {           

            Vector3 tg, center;
            tg = new Vector3(other.transform.position.x, other.transform.position.y + 1, other.transform.position.z);
            center = new Vector3(transform.localPosition.x, transform.localPosition.y + 1, transform.localPosition.z);

            RaycastHit hit;
            bool too = Physics.Linecast(center, tg, out hit, 1, QueryTriggerInteraction.Ignore);
                      
            if (too && hit.collider.gameObject.CompareTag("Player"))
            {
                miraTravada.LookAt(hit.point);

                Debug.DrawLine(center, new Vector3(hit.point.x, hit.point.y, hit.point.z) - miraTravada.forward * 8);
                Vector3 alvo = new Vector3(hit.point.x, hit.point.y, hit.point.z) - miraTravada.forward * 8;
                
                
                if(Vector3.Distance(center, alvo) >= 2)
                   agent.SetDestination(alvo);
                else
                {
                
                }
            }

        }
    }

}
