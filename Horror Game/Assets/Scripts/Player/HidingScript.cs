using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingScript : MonoBehaviour
{

    static public bool hiding = false;
    static public List<BasicAI> spots = new List<BasicAI>();

   
    private Transform hideObject = null;

    

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Hide")
        {
            hideObject = col.transform;
        }
    }

    private void OnGUI()
    {
        if(hideObject != null && hiding == false)
        {
            string String = "Press '" + KeyManager.controls["Interact"].ToString() + "' to hide";
            
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 1.1f, 100, 30), new GUIContent(String));
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyManager.controls["Interact"]) && hiding)
        {
            hiding = false;
        }
        else if (Input.GetKeyDown(KeyManager.controls["Interact"]) && hideObject != null)
        {
            hiding = true;
            FindObjectOfType<CameraScript>().hidingObject = hideObject;
            FindObjectOfType<CameraScript>().ResetAngles();
        }

        if(hiding == true)
        {
            foreach(BasicAI ai in spots)
            {
                
            }
        }

    }

    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "Hide")
        {
            hideObject = null;
        }
    }

}
