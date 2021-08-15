using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class JoystickMap 
{
    public static bool XboxControl;  

    public static string Btn()
    {

        if (Input.GetButton("A"))
            return "A";

        if (Input.GetButton("B"))
            return "B";

        if (Input.GetButton("X"))
            return "X";

        if (Input.GetButton("Y"))
            return "Y";

        if (Input.GetButton("LB"))
            return "LB";

        if (Input.GetButton("RB"))
            return "RB";

        if (Input.GetButton("Start"))
            return "Start";

        if (Input.GetButton("Back"))
            return "Back";

        if (Input.GetButton("LT"))
            return "LT Ativado";

        if (Input.GetButton("RT"))
            return "RT Ativado";

        if (Input.GetButton("Left Analog Press"))
            return "Left Analog Press";

        if (Input.GetButton("Right Analog Press"))
            return "Right Analog Press";

        return "";


        }

    public static void JoystickActive()
    {
        if (Input.GetButton("A") ||
        Input.GetButton("B") ||
        Input.GetButton("X") ||
        Input.GetButton("Y") ||
        Input.GetButton("LB") ||
        Input.GetButton("RB") ||
        Input.GetButton("Start") ||
        Input.GetButton("Back") ||
        Input.GetButton("LT") ||
        Input.GetButton("RT") ||
        Input.GetButton("Left Analog Press") ||
        Input.GetButton("Right Analog Press") ||
        Input.GetAxisRaw("Left Analog Horizontal") != 0 ||
        Input.GetAxisRaw("Left Analog Vertical") != 0 ||
        Input.GetAxis("Right Analog Horizontal") != 0 ||
        Input.GetAxis("Right Analog Vertical") != 0 ||
        Input.GetAxis("LT") != 0 ||
        Input.GetAxis("RT") != 0 ||
        Input.GetAxis("DPAD Horizontal") != 0 ||
        Input.GetAxis("DPAD Vertical") != 0)
            XboxControl = true;
        else if (Input.anyKey)
        {
            XboxControl = false;
        }
    }

     public static float Axis(string BtnName)
    {
     
        if (BtnName == "L Analog Horizontal")
            return Input.GetAxisRaw("Left Analog Horizontal");
        if (BtnName == "L Analog Vertical")
            return Input.GetAxisRaw("Left Analog Vertical");

        if (BtnName == "R Analog Horizontal")
            return Input.GetAxis("Right Analog Horizontal");
        if (BtnName == "R Analog Vertical")
            return Input.GetAxis("Right Analog Vertical");

        if (BtnName == "LT")
            return Input.GetAxis("LT");
        if (BtnName == "RT")
            return Input.GetAxis("RT");

        if (BtnName == "DPAD Horizontal")
            return Input.GetAxis("DPAD Horizontal");
        if (BtnName == "DPAD Vertical")
            return Input.GetAxis("DPAD Vertical");

        return 0;

    }

    public static float LHorizontal()
    {   
        if (!XboxControl)        
            return  Input.GetAxisRaw("Horizontal");        
        else
            return Mathf.Round(Input.GetAxis("Left Analog Horizontal"));
     
    }

    public static float LVertical()
    {
        if (!XboxControl)
            return Input.GetAxisRaw("Vertical");
        else
            return Mathf.Round(Input.GetAxis("Left Analog Vertical"));
    }

    public static float RHorizontal()
    {

        if (!XboxControl)
            return Input.GetAxis("Mouse X");
        else
            return Input.GetAxis("Right Analog Horizontal");

    }

    public static float RVertical()
    {
        if (!XboxControl)
            return Input.GetAxis("Mouse Y");
        else
            return -Input.GetAxis("Right Analog Vertical");
    }

}
