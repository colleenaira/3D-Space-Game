using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipLookTarget : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform ship; // Assign your ship transform here

    void Update()
    {
        // Update the position to match the ship's position but keep the world up direction
        transform.position = ship.position;
    }
}
