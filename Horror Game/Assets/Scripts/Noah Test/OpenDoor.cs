using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    
   
    public GameObject DoorOne;
    public bool AllowedDoor = true;
    public float timeDelay = 2f;
    public bool timeOk;
    // Start is called before the first frame update


    

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(Input.GetKeyDown(KeyManager.controls["Interact"]))
            {
                DoorOne.SetActive(false);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        DoorOne.SetActive(true);
    }



}