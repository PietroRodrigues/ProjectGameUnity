using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [Header("Settings Patrol")]
    [SerializeField] Transform patrolPoits = null;
    [SerializeField] bool esperandoNaPatrulha = false;
    [SerializeField] float tempoEspera = 3;
    [SerializeField] float mudaProbabilidade = 0.2f;

    [Header("Settings To Stalk")]
    [SerializeField] float rengeAtack = 2;
    [SerializeField] float codalAtack = 2;
    [SerializeField] Transform miraTravada = null;
    [SerializeField] float rotateSpeed = 80;
    [SerializeField] float tempoProcura = 10;
    [SerializeField] float DistanceIgnore = 70;

    [Header("Settings Skill")]
    [SerializeField] Transform particle;


    NavMeshAgent agent;
    Vector3 startpoit;
    Transform alvo;
    int indcPatrulhaAtual;
    bool trafegar;
    bool espera;
    bool patrulhar;
    float deleyTrocaPonts;
    bool persegundo;
    bool startTimeLost;
    float timeLost;
    float deleyAtack;
    bool atackUp;

    Statos statos;
    EnemyAnimation anim;
    bool atack;
    bool hit;
    bool victory;

    private void Start()
    {       
        anim = new EnemyAnimation(this.GetComponentInChildren<Animator>());
        statos = new Statos(3,6);

        startpoit = transform.position;

        agent = GetComponent<NavMeshAgent>();

        if (patrolPoits != null && patrolPoits.childCount >= 2)
        {
            indcPatrulhaAtual = 0;
            SetDestination();
        }

    }

    private void Update()
    {
        if (agent.isStopped)
            statos.Speed = 0;
        else if (!agent.isStopped && persegundo)
            statos.Speed = 6;
        else if(!agent.isStopped && !persegundo)
            statos.Speed = 3;

        agent.speed = statos.Speed;
        Searching();
        Patrol();
        anim.AnimationController(statos.Speed, atack, statos.Life, hit, victory);
        
        if (!atackUp)
        {
            deleyAtack += Time.deltaTime;        

            if (deleyAtack >= codalAtack)
            {
                atackUp = true;             
                deleyAtack = 0;
            }

        }

    }


    private void OnTriggerStay(Collider other)
    {
        ToStalk(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            startTimeLost = true;
        }
    }    

    void ToStalk(Collider other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            if (Vector3.Distance(transform.position, startpoit) >= DistanceIgnore)
            {
                LostTarget();
            }
            else
            {
                Vector3 tg, center;
                tg = new Vector3(other.transform.position.x, other.transform.position.y + 1, other.transform.position.z);
                center = new Vector3(transform.localPosition.x, transform.localPosition.y + 1, transform.localPosition.z);

                RaycastHit hit;
                bool too = Physics.Linecast(center, tg, out hit, 1, QueryTriggerInteraction.Ignore);

                if (too && hit.collider.gameObject.CompareTag("Player"))
                {                   
                    Found(hit.collider.transform);

                    SetDestination();

                    if (Vector3.Distance(transform.position, alvo.position) <= rengeAtack)
                    {
                        agent.isStopped = true;
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, miraTravada.rotation, rotateSpeed * Time.deltaTime);
                        
                        if(atackUp)
                        {
                            atack = !atack;
                                          

                            atackUp = false;                            
                        }

                       
                    }
                    else
                    {
                        if (atack)
                        {                           
                           atack = false;                      
                           atackUp = false;                          

                        }
                        else
                            agent.isStopped = false;
                       
                    }

                }
            }

        }
    }

    void SkillAtack()
    {
        particle.transform.position = new Vector3(alvo.position.x, alvo.position.y + 1, alvo.position.z);
        particle.gameObject.SetActive(true);
        particle.GetComponent<ParticleSystem>().Play();
       
    }

    void Patrol()
    {
        if (!persegundo)
        {
            if (trafegar && agent.remainingDistance <= 1.0f)
            {
                trafegar = false;

                if (esperandoNaPatrulha)
                {
                    espera = true;
                    agent.isStopped = true;
                    deleyTrocaPonts = 0f;
                }
                else
                {
                    
                    ChangePoint();
                    SetDestination();
                }
            }

            if (espera)
            {
                deleyTrocaPonts += Time.deltaTime;

                if (deleyTrocaPonts >= tempoEspera)
                {
                    espera = false;
                    agent.isStopped = false;
                    ChangePoint();
                    SetDestination();
                }
            }
        }
    }

    void ChangePoint()
    {
        if (Random.Range(0f, 1f) <= mudaProbabilidade)
        {
            patrulhar = !patrulhar;
        }

        if (patrulhar)
        {
            indcPatrulhaAtual = (indcPatrulhaAtual + 1) % patrolPoits.childCount;
        }
        else
        {
            if (--indcPatrulhaAtual < 0)
            {
                indcPatrulhaAtual = patrolPoits.childCount - 1;
            }
        }

    }

    void SetDestination()
    {
        if (persegundo)
        {
            miraTravada.LookAt(alvo);
            agent.SetDestination(alvo.position);
        }
        else
        {
            if (patrolPoits != null)
            {
                Vector3 targetV3 = patrolPoits.GetChild(indcPatrulhaAtual).position;
                agent.SetDestination(targetV3);
                trafegar = true;
            }

        }
    }

    void Searching()
    {
        if (startTimeLost)
        {
            timeLost += Time.deltaTime;

            if (timeLost >= tempoProcura)
            {
                LostTarget();
            }

        }
    }

    void LostTarget()
    {
        timeLost = 0;
        startTimeLost = false;
        alvo = null;
        agent.isStopped = false;
        agent.speed = statos.Speed;
        persegundo = false;
        deleyAtack = codalAtack;
    }

    void Found(Transform tg)
    {        
        alvo = tg;
        agent.speed = statos.Speed;
        persegundo = true;
    }

    private void OnDrawGizmos()
    {
        if (patrolPoits != null)
        { 
            Gizmos.color = Color.red;      
        
            foreach (Transform points in patrolPoits)
            {
                Gizmos.DrawWireSphere(points.position, 0.7f);
            }
        }
      
    }

}
