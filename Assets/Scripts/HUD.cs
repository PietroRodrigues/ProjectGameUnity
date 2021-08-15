using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    public Player player;
    [SerializeField] GameObject[] reliquiasHud = new GameObject[3];
    [SerializeField] GameObject poderHud;
    [SerializeField] GameObject victory;
    [SerializeField] GameObject defeat;
    [SerializeField] Text lifeTxt;    

    public enum reliquias { moedas,livro,coroua, poder}

    [HideInInspector] public bool moedas;
    [HideInInspector] public bool livro;
    [HideInInspector] public bool coroua;

    [HideInInspector] public bool poder;

    void Update()
    {     
        lifeTxt.text = "X " + player.statos.Life.ToString();

        reliquiasHud[0].SetActive(moedas);
        reliquiasHud[1].SetActive(livro);
        reliquiasHud[2].SetActive(coroua);
        poderHud.SetActive(poder);
        player.specialUp = poder;

        if (player.statos.Life == 0) {
            defeat.SetActive(true);
        } else if (player.victory) { 
            victory.SetActive(true);
        }      

        if (player.victory || player.statos.Life == 0)
        {
            if (JoystickMap.Btn() == "A" ||                
                JoystickMap.Btn() == "Start" ||
                Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                SceneManager.LoadScene(0);
            }

        }

    }

}
