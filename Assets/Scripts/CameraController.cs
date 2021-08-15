using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] Transform foco;
    [SerializeField] bool lockCursor;
    [SerializeField] float distanceFromTarget = 10;
    [SerializeField] float alturaCamera = 0;
    Vector2 pithMinMax = new Vector2(-40, 85);

    public float rotationSmoothTime = 0.12f;
    float mouseSensivity = 2;

    Player player;
    Vector3 rotationSmoothSpeed;
    Vector3 currentRotation;
    RaycastHit hit;
    Vector3 pos;

    float yaw;
    float pitch;

    // Start is called before the first frame update
    void Start()
    {
        player = foco.GetComponentInParent<Player>();

        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
     
    }

    // Update is called once per frame
    void Update()
    {
        JoystickMap.JoystickActive();

        if (JoystickMap.XboxControl)
            mouseSensivity = 0.5f;
        else
            mouseSensivity = 2;

        GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, 40f, 0.1f);
    }

    private void LateUpdate()
    {
        if (!player.victory)
        {            

            if (player.statos.Life != 0){
                          

                yaw += JoystickMap.RHorizontal() * mouseSensivity;
                pitch -= JoystickMap.RVertical() * mouseSensivity;
                pitch = Mathf.Clamp(pitch, pithMinMax.x, pithMinMax.y);

                currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothSpeed, rotationSmoothTime);

                transform.eulerAngles = currentRotation;

                Vector3 limitCam = (foco.position + new Vector3(0, alturaCamera, 0)) - transform.forward * distanceFromTarget;

                bool see = Physics.Linecast(foco.position, limitCam, out hit, 1, QueryTriggerInteraction.Ignore);

                if (!see)
                {
                    pos = limitCam;
                }
                else
                {
                    pos = (foco.position + new Vector3(0, alturaCamera, 0)) - transform.forward * (Vector3.Distance(foco.position, hit.point) - 1f);
                }


                transform.position = pos;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }

        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }

    }
}
