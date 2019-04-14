﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    [SerializeField] Transform player; //The player object
    [SerializeField] Transform lookObject;
    [SerializeField] Transform lookHolder;
    [SerializeField] float wallBufferValue = 2f;
    [SerializeField] float distance;  //The distance from the player to hover
    [SerializeField] float height; //The height above the player to hover
    [SerializeField] float speed; //The follow speed of the camera
    public float mouseSpeed = 5f; //The speed of the camera based on mouse input
    float angleY = 0;
    float angleX = 0;
    public Transform hidingObject;
    [SerializeField] LayerMask ignoreLayers;

  
    public void ResetAngles()
    {
        angleY = 0;
        angleX = 0;
    }

    private void Update()
    {

        angleX += Input.GetAxis("Mouse X") * Time.deltaTime * mouseSpeed;
        angleY -= Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSpeed;

        if(hidingObject != null)
        {
            HidingObject hidingScript = hidingObject.GetComponent<HidingObject>();
            angleX = Mathf.Clamp(angleX, -hidingScript.maxX, hidingScript.maxX);
            angleY = Mathf.Clamp(angleY, -hidingScript.maxY, hidingScript.maxY);
        }

        if(angleX >= 360)
        {
            angleX -= 360;
        }

        if (angleX <= -360)
        {
            angleX += 360;
        }

        if (Input.GetKey(KeyManager.controls["CameraMovement"]) && HidingScript.hiding == false)
        {

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            float newY = lookHolder.localPosition.y + Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSpeed * 0.1f;
            newY = Mathf.Clamp(newY, 0, 3);
            lookHolder.localPosition = new Vector3(0, newY, 0);

            float rotY = lookHolder.localEulerAngles.y + Input.GetAxis("Mouse X") * Time.deltaTime * mouseSpeed;
            if (rotY > 360)
            {
                rotY -= 360;
            }
            else if (rotY < -360)
            {
                rotY += 360;
            }

            lookHolder.localEulerAngles = new Vector3(0, rotY, 0);
        }
        else if(HidingScript.hiding == false)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

    }

    private void LateUpdate()
    {

        if (HidingScript.hiding == false)
        {
            lookObject.position = lookHolder.position - lookHolder.forward * distance;

            Vector3 lookObjY = new Vector3(lookObject.position.x, player.position.y, lookObject.position.z);

            Vector3 newPos = new Vector3(lookObject.position.x, player.position.y, lookObject.position.z) + Vector3.up * height;

            Ray ray = new Ray(player.position, lookObjY + Vector3.up*height - player.position);
            RaycastHit hit;
            Debug.DrawRay(player.position, lookObjY + Vector3.up*height - player.position);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ignoreLayers))
            {
                //if (Vector3.Distance(player.position, transform.position) + wallBufferValue > Vector3.Distance(player.position, hit.point))
                
                    newPos = new Vector3(hit.point.x, hit.point.y, hit.point.z) + hit.normal.normalized*0.5f;
                    transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * speed);
             
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * speed);
            }

            transform.LookAt(lookHolder);
        }
        else
        {

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            transform.position = hidingObject.GetComponent<HidingObject>().hidePoint.position;
            transform.rotation = hidingObject.GetComponent<HidingObject>().hidePoint.rotation * Quaternion.AngleAxis(angleX, Vector3.up) * Quaternion.AngleAxis(angleY, Vector3.right);

            

        }
        
    }

}
