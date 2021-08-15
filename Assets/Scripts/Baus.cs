using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baus : MonoBehaviour
{
    HUD hud;
    [SerializeField] GameObject bOpen;
    [SerializeField] GameObject bClosed;
    public HUD.reliquias reliquia;

    private void Start()
    {
        hud = FindObjectOfType<HUD>();
    }

    public void OpenChest()
    {
        switch (reliquia)
        {
            case HUD.reliquias.moedas:
                hud.moedas = true;
                Open();               
                break;
            case HUD.reliquias.livro:
                hud.livro = true;
                Open();                
                break;
            case HUD.reliquias.coroua:
                hud.coroua = true;
                Open();                
                break;
            case HUD.reliquias.poder:
                if (hud.moedas && hud.livro && hud.coroua)
                {
                    hud.poder = true;
                    Open();                    
                }
                break;
            default:
                break;
        }  

    }

    void Open()
    {
        bOpen.SetActive(true);
        bClosed.SetActive(false);        
    }

}
