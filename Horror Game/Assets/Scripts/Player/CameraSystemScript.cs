using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystemScript : MonoBehaviour
{

    [SerializeField] Transform[] cameras;
    [SerializeField] RenderTexture texture;
    int count = 0;

    bool up = false;

    [SerializeField] Transform tablet;


    private void Start()
    {
        DisableCameras();
    }

    private void Update()
    {

        if(up && Input.GetKeyDown(KeyManager.controls["Tool"]))
        {
            cameras[count].gameObject.SetActive(false);
            count += 1;

            if(count >= cameras.Length)
            {
                count = 0;
            }

        }

        if(up)
        {
            cameras[count].gameObject.SetActive(true);
            cameras[count].GetComponent<Camera>().targetTexture = texture;
        }

        if(HidingScript.hiding && Input.GetKeyDown(KeyManager.controls["Tool"]) && up == false)
        {
            up = true;
            tablet.gameObject.SetActive(true);
            count = 0;
        }
        else if(Input.GetKeyDown(KeyManager.controls["Escape"]) && up == true)
        {
            up = false;
            DisableCameras();
            tablet.gameObject.SetActive(false);
            count = 0;
        }

        if(HidingScript.hiding == false || up == false)
        {
            up = false;
            DisableCameras();
            tablet.gameObject.SetActive(false);
            count = 0;
        }


    }

    private void DisableCameras()
    {
        foreach(Transform obj in cameras)
        {
            obj.gameObject.SetActive(false);
        }
    }

}

