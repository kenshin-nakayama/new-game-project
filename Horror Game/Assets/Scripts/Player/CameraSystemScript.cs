using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystemScript : MonoBehaviour
{

    [SerializeField] Transform[] cameras;
    [SerializeField] RenderTexture texture;
    public float battery = 100;
    int count = 0;
    [SerializeField] Transform screen;
    [SerializeField] BatterySystem batterySystem;
    [SerializeField] TextMesh batteryObject;
    [SerializeField] Texture[] batteryTextures;

    [SerializeField] bool up = false;

    [SerializeField] Transform tablet;


    private void Start()
    {
        DisableCameras();
    }

    private void Update()
    {



        if (battery > 0 && up)
        {
            batteryObject.text = Mathf.Round(battery) + "%";
            batteryObject.color = Color.green;
            Debug.Log("Battery: " + battery);
            battery -= Time.deltaTime;
        }
        else
        {
            batteryObject.color = Color.red;
            batteryObject.text = "0%";
        }

        if (battery <= 0 && Input.GetKeyDown(KeyManager.controls["Reload"]) && batterySystem.batteries > 0)
        {
            batterySystem.batteries -= 1;
            battery = 100;
        }

        if (up && Input.GetKeyDown(KeyManager.controls["Tool"]))
        {
            cameras[count].gameObject.SetActive(false);
            count += 1;

            if (count >= cameras.Length)
            {
                count = 0;
            }

        }

        if (up)
        {
            cameras[count].gameObject.SetActive(true);
            cameras[count].GetComponent<Camera>().targetTexture = texture;
        }

        if (HidingScript.hiding && Input.GetKeyDown(KeyManager.controls["Tool"]) && up == false)
        {
            up = true;
            tablet.gameObject.SetActive(true);
            count = 0;
        }

        if (Input.GetKey(KeyManager.controls["Esc"]))
        {
            up = false;
            DisableCameras();
            tablet.gameObject.SetActive(false);
            count = 0;
        }

        if (battery <= 0)
        {
            screen.gameObject.SetActive(false);
            DisableCameras();
        }
        else
        {
            screen.gameObject.SetActive(true);
        }

        if (HidingScript.hiding == false || up == false)
        {
            up = false;
            DisableCameras();
            tablet.gameObject.SetActive(false);
            count = 0;
        }

        Debug.Log(HidingScript.hiding + " hiding");


    }

    private void DisableCameras()
    {
        foreach (Transform obj in cameras)
        {
            obj.gameObject.SetActive(false);
        }
    }

}

