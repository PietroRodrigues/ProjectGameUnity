using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleDesable : MonoBehaviour
{    // Update is called once per frame
    void Update()
    {
        if (GetComponent<ParticleSystem>().isStopped)
        {
            gameObject.SetActive(false);
        }
        
    }
}
