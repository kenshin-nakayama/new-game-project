using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwo : MonoBehaviour
{

    public float jumpSpeed = 5f;

    void Jump()
    {
        transform.Translate(Vector3.up * jumpSpeed);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }



}
