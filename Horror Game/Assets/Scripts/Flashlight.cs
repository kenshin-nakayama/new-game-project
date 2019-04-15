using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public bool on = false;

    

    public GameObject ModelFlash;

    //---------------------------------

    private void Start()
    {
        on = false;
        ModelFlash.SetActive(false);
    }

    void TurnOFF()
    {
        on = false;

        if (Input.GetKeyDown(KeyCode.B))
        {

            ModelFlash.SetActive(false);
        }
    }
    void TurnON()
    {
        on = true;

        if (Input.GetKeyDown(KeyCode.F))
        {

            ModelFlash.SetActive(true);
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyManager.controls["Tool"]) && on == true)
        {
            ModelFlash.SetActive(false);
            on = false;
        }
        else if (Input.GetKeyDown(KeyManager.controls["Tool"]))
        {
            ModelFlash.SetActive(true);
            on = true;
        }
    }
}
 