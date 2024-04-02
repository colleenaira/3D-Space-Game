using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    public Transform ship; // Assign your ship transform here

    void LateUpdate()
    {
        // You can add smoothing here if desired
        transform.position = ship.position;
    }
}
