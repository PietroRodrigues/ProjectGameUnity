using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{

    [Header("Settings")]
    [SerializeField] float speedRun = 8;
    [SerializeField] int life = 10;
    [HideInInspector] public bool specialUp = false;

    [Header("Jump Settings")]
    private bool jumping = false;
    public float jumpforce;
    public LayerMask layer;
    private bool isGrounded;
    [SerializeField] Transform GrandCheckPos = null;
    [SerializeField] float GrondCheckSize = 1f;

    [SerializeField] GameObject particleFire;
    [SerializeField] GameObject particleAtack;
    [SerializeField] ParticleSystem[] particleEfect;
    Transform alvo;

    float xRaw;
    float zRaw;

    [HideInInspector] public Statos statos;
    [HideInInspector] public bool def;
    [HideInInspector] public bool hit;
    [HideInInspector] public bool victory;
    bool walk;
    bool atack;    
    bool opemChest;

    Rigidbody rb;

    PlayerAnimation anim ;


    // Start is called before the first frame update
    void Start()
    {
        anim = new PlayerAnimation(this.GetComponentInChildren<Animator>());
        rb = GetComponent<Rigidbody>();
        statos = new Statos(life, speedRun);
    }

    private void Update()
    {
        Special();
        Inputs();
        anim.AnimationController(statos.Speed, atack, def, hit, statos.Life, victory);
    }


    private void FixedUpdate()
    {
        if (!victory) {
            if (statos.Life != 0)
            {
                Movimentation();
                Rotation();
                jump();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && !other.isTrigger)
        {
            alvo = other.transform;
        }
        else if (other.gameObject.GetComponent<Baus>() != null && !other.isTrigger)
        {
            if(opemChest)
                OpenChests(other.gameObject.GetComponent<Baus>());
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && !other.isTrigger)
        {
            alvo = null;
        }
    }

    void Inputs()
    {
        xRaw = Input.GetAxisRaw("Horizontal");
        zRaw = Input.GetAxisRaw("Vertical");
        
        if (Input.GetKeyDown(KeyCode.Space)){
            jumping = true;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            walk = true;
        }
        else
        {
            walk = false;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            opemChest = true;
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            opemChest = false;
        }

        if (Input.GetMouseButton(0))
        {
            atack = true;
            xRaw = 0;
            zRaw = 0;
        
        }
        else if (Input.GetMouseButton(1)) { 

            def = true;
            xRaw = 0;
            zRaw = 0;

        }else
        {
            def = false;
            atack = false;         
        }

    }   

    void Special()
    {
        if (specialUp)
        {
            foreach (ParticleSystem efect in particleEfect)
            {
                if(efect.isStopped)
                    efect.Play();
            }

        }
    }

    void Movimentation()
    {

        if (xRaw != 0 || zRaw != 0)
        {
            if (walk)
            {               
                statos.Speed = speedRun/2;
            }
            else
            {
                statos.Speed = speedRun;
            }
            
            rb.MovePosition(rb.position + statos.Speed * Time.fixedDeltaTime * transform.forward);

        }
        else
        {
            statos.Speed = 0;
        }

    }

    void Rotation()
    {

        float camY = Camera.main.transform.rotation.eulerAngles.y;

        if (zRaw == 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, camY, 0), Time.deltaTime * 5);
        }
        if (zRaw == -1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, camY - 180, 0), Time.deltaTime * 5);
        }
        if (xRaw == 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, camY + 90, 0), Time.deltaTime * 5);
        }
        if (xRaw == -1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, camY - 90, 0), Time.deltaTime * 5);
        }

    }

    void jump()
    {
        Collider[] hitColliders = Physics.OverlapSphere(GrandCheckPos.position, GrondCheckSize, layer, QueryTriggerInteraction.Ignore);
              
        if (hitColliders.Length != 2)
        {
            isGrounded = false;
        }
        else
        {
            isGrounded = true;
        }

        if (jumping && isGrounded)
        {

            rb.AddForce(transform.up * jumpforce, ForceMode.Impulse);
            jumping = false;
        }
        else if(!isGrounded)
        {
            rb.AddForce(-transform.up * jumpforce/12, ForceMode.Impulse);
          
        }

    }

    public void SkillAtack()
    {
        if (specialUp)
        {
            if (alvo != null)
            {
                int mobsDeath = 0;
                Enemy enemy = alvo.GetComponentInParent<Enemy>();

                particleFire.transform.position = new Vector3(alvo.position.x, alvo.position.y + 1, alvo.position.z);
                particleFire.gameObject.SetActive(true);
                particleFire.GetComponent<ParticleSystem>().Play();

                enemy.hit = true;
                enemy.statos.Life--;

                if (enemy.statos.Life == 0)
                {
                    foreach (Enemy mob in FindObjectsOfType<Enemy>())
                    {
                        if (mob.statos.Life == 0)
                            mobsDeath++;
                    }

                    if (mobsDeath == FindObjectsOfType<Enemy>().Length)
                    {
                        victory = true;
                    }
                }
            }
        }
        else
        {
            if (alvo != null)
            {
                Enemy enemy = alvo.GetComponentInParent<Enemy>();

                particleAtack.transform.position = new Vector3(alvo.position.x, alvo.position.y + 1, alvo.position.z);
                particleAtack.gameObject.SetActive(true);
                particleAtack.GetComponent<ParticleSystem>().Play();

                if(Random.Range(0,1) < 0.2f)
                {
                    enemy.stunStatos = true;
                }
            }
        }
    }

    void OpenChests(Baus bau)
    {
        bau.OpenChest();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GrandCheckPos.position, GrondCheckSize);
    }

}
