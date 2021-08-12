using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{

    [Header("Settings")]
    public float speedRun;
    public float rotateSpeed;
    
    [Header("Jump Settings")]
    private bool jumping = false;
    public float jumpforce;
    public LayerMask layer;
    private bool isGrounded;
    [SerializeField] Transform GrandCheckPos = null;
    [SerializeField] float GrondCheckSize = 1f;

    float xRaw;
    float zRaw;

    bool walk;
    float speed;
    bool atack;
    bool def;
    int life;
    bool victory;

    bool specialUp = true;

    Rigidbody rb;

    PlayerAnimation anim ;

    // Start is called before the first frame update
    void Start()
    {
        anim = new PlayerAnimation(this.GetComponentInChildren<Animator>());
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Inputs();
        anim.AnimationController(speed, atack, def, life, victory);
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

    private void FixedUpdate() {       

        Movimentation();           
        Rotation();
        jump();       

    }

    void Movimentation()
    {

        if (xRaw != 0 || zRaw != 0)
        {
            speed += 0.2f;

            if (walk)
            {
                if (speed > speedRun/2)
                    speed = speedRun/2;
            }
            else
            {
                if (speed > speedRun)
                    speed = speedRun;
            }
            
            rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * transform.forward);

        }
        else
        {
            speed = 0;
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

        if (hitColliders.Length != 1)
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

    private void OnTriggerStay(Collider other)
    {
        if (atack && specialUp)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("hit");
            }
        }
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GrandCheckPos.position, GrondCheckSize);
    }

}
