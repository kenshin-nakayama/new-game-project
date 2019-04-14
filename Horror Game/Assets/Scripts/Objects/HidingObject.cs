using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingObject : MonoBehaviour
{

    public Transform hidePoint;
    public float maxY = 25;
    public float maxX = 25;

    private void Start()
    {
        Debug.Log("Start");
        HidingSpotManager.hidingObjects.Add(this);
    }

}
