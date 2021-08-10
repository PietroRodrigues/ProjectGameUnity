using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Moviment Settings")]
    public float speedRun;
    public float speedSprint;
    public float rotateSpeed;

    [Header("Jump Settings")]
    private bool jumping = false;
    public float jumpforce;
    public LayerMask layer;
    private bool isGrounded;
    [SerializeField] Transform GrandCheckPos = null;
    [SerializeField] float GrondCheckSize = 1f;

    [Header("Animation Settings") ]
    public Animator anim;

    float speed;
    Rigidbody rb;

    float xRaw;
    float zRaw;

    float x;
    float z;

    void Start() {  
        
        rb = GetComponent<Rigidbody>();
        speed = speedRun;
        StaticProperts._Player = gameObject;

    }

    void Update() {
        
    }


    private void FixedUpdate() {       

        Movimentation();           
        Rotation();
        jump();       

    }

    void Movimentation(){

        if (xRaw != 0 || zRaw!= 0)
        {
            rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * transform.forward);

        }

    }


    void MovimentationFocos(){
        //Set Direction Focos
        
        float speedDivid;

        if (xRaw != 0 && zRaw!= 0)
            speedDivid = 2;
        else
            speedDivid = 1;
        
       
        if(zRaw == 1){
            rb.MovePosition(rb.position + (speed / speedDivid) * Time.fixedDeltaTime * transform.forward);
        }else if(zRaw == -1){
            rb.MovePosition(rb.position + (speed / speedDivid) * Time.fixedDeltaTime * -transform.forward);
        }
    
        if(xRaw == 1){
            rb.MovePosition(rb.position + (speed / speedDivid) * Time.fixedDeltaTime * transform.right);
        }else if(xRaw == -1){
            rb.MovePosition(rb.position + (speed / speedDivid) * Time.fixedDeltaTime * -transform.right);
        }
       

        float camY = Camera.main.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, camY + transform.rotation.y, 0), Time.deltaTime * 5);
    }

    void jump(){

        Collider[] hitColliders = Physics.OverlapSphere(GrandCheckPos.position, GrondCheckSize, layer,QueryTriggerInteraction.Ignore);

        if(hitColliders.Length != 1){
            isGrounded = false;              
        }else{
            isGrounded = true;                            
        } 
      

        if(jumping && isGrounded){

            rb.AddForce(transform.up * jumpforce, ForceMode.Impulse);
            jumping = false;
        }

    }

    void Rotation(){

        float camY = Camera.main.transform.rotation.eulerAngles.y;
        
        if(zRaw == 1){
           transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, camY, 0), Time.deltaTime * 5);
        }
        if(zRaw == -1){
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, camY -180, 0), Time.deltaTime * 5);
        }
        if(xRaw == 1){
           transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, camY + 90, 0), Time.deltaTime * 5);
        }
        if(xRaw == -1){
           transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, camY -90, 0), Time.deltaTime * 5);
        }

    }


}
