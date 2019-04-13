using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{

    [SerializeField] private float baseSpeed = 5f; //The walking speed of the player
    [SerializeField] private float runMultiplier = 1.5f; //How much faster running is compared to walking
    [SerializeField] private float turnSpeed = 3f; //How quickly the player turns
    [SerializeField] private Transform lookObject;
    [SerializeField] Transform playerModel;
    private float speed = 5f; //The players current speed;
    

    private void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift)) //Checking if the player holding down the run key and applying the run speed
        {
            speed = baseSpeed * runMultiplier;
        }
        else
        {
            speed = baseSpeed;
        }


        Rigidbody rigidbody = GetComponent<Rigidbody>();

        Vector3 vel = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z).normalized;

        if (vel.magnitude > 0)
        {
            playerModel.rotation = Quaternion.Lerp(playerModel.rotation, Quaternion.LookRotation(vel, Vector3.up), turnSpeed * Time.deltaTime);
        }

    }

    private void FixedUpdate()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();


        Vector3 forward = new Vector3(lookObject.forward.x, 0, lookObject.forward.z);
        Vector3 right = new Vector3(lookObject.right.x, 0, lookObject.right.z);

        if (HidingScript.hiding == false)
        {
            rigidbody.isKinematic = false;
            playerModel.gameObject.SetActive(true);

            if (Input.GetKey(KeyCode.Mouse0) == false && true == false)
            {
                forward = playerModel.forward;
                right = playerModel.right;
            }


            Vector3 movementVector = Input.GetAxisRaw("Vertical") * forward + Input.GetAxisRaw("Horizontal") * right; //Calculating the velocity vector of the player based on input

            movementVector = movementVector.normalized * speed + new Vector3(0, rigidbody.velocity.y, 0); //Normalizing the vector so that walking diagonally is not faster. Also adding the players previous vertical velocity so they cant float in mid air

            rigidbody.velocity = movementVector;

          

        }
        else
        {
            rigidbody.isKinematic = true;
            playerModel.gameObject.SetActive(false);
        }

    }


}
