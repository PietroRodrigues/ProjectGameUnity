using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteJoystickBtns : MonoBehaviour {

    public float LeftAnalogHorizontal;
    public float LeftAnalogVertical;
    public float RightAnalogHorizontal;
    public float RightAnalogVertical;
    public float Lt;
    public float Rt;
    public float DPADHorizontal;
    public float DPADVertical;

    // Update is called once per frame
    void Update () {

        //Botoes

        if (Input.GetButton("A"))
            print("A");
        
        if (Input.GetButton("B"))
            print("B");
        
        if (Input.GetButton("X"))
            print("X");

        if (Input.GetButton("Y"))
            print("Y");

        if (Input.GetButton("LB"))
            print("LB");

        if (Input.GetButton("RB"))
            print("RB");

        if (Input.GetButton("Start"))
            print("Start");

        if (Input.GetButton("Back"))
            print("Back");

        //===============================================
        if (Input.GetButton("LT"))
            print("LT Ativado");


        if (Input.GetButton("RT"))
            print("RT Ativado");
        //===============================================
      

        if (Input.GetButton("Left Analog Press"))
            print("Left Analog Press");

        if (Input.GetButton("Right Analog Press"))
            print("Right Analog Press");

        //Eixos

        LeftAnalogHorizontal = Input.GetAxisRaw("Left Analog Horizontal");
        LeftAnalogVertical = Input.GetAxisRaw("Left Analog Vertical");

        RightAnalogHorizontal = Input.GetAxis("Right Analog Horizontal");
        RightAnalogVertical = Input.GetAxis("Right Analog Vertical");

        Lt = Input.GetAxis("LT");
        Rt = Input.GetAxis("RT");

        DPADHorizontal = Input.GetAxis("DPAD Horizontal");
        DPADVertical = Input.GetAxis("DPAD Vertical");

    }
}
