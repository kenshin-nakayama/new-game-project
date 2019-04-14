using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpotManager : MonoBehaviour
{

    static public List<HidingObject> hidingObjects = new List<HidingObject>();

    private void Awake()
    {
        Debug.Log("Awake");
        hidingObjects.Clear();
    }

    public void Update()
    {
        Debug.Log(hidingObjects.Count);
    }

}
