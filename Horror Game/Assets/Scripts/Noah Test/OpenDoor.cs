using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public float doorSpeed;
    public bool doorOpen = false;
    public GameObject DoorOne;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                doorOpen = true;
            }
            
        }
       
    }
    // Update is called once per frame
    void Update()
    {
        if(doorOpen == true)
        {
            DoorOne.SetActive(false);
        }
        
        if(doorOpen == false)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                DoorOne.SetActive(true);
            }
        }
    }
}