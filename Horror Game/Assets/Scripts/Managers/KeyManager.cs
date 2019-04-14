using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{

    static public Dictionary<string, KeyCode> controls = new Dictionary<string, KeyCode>();

    private void Start()
    {
        if (controls.Count == 0)
        {
            controls.Add("Interact", KeyCode.E);
            controls.Add("CameraMovement", KeyCode.Mouse1);
            controls.Add("Tool", KeyCode.F);
            controls.Add("Escape", KeyCode.Escape);
        }
    }

    public void Example()
    {
        if(Input.GetKeyDown(KeyManager.controls["Interact"]))
        {
            //Interact
        }
    }

}
