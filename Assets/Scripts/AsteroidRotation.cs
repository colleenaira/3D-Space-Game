using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(0f, 3f, 0f);
    public bool randomizeOnStart = true;

    void Start()
    {
        if (randomizeOnStart)
        {
            rotationSpeed = new Vector3(
                Random.Range(-6f, 8f),
                Random.Range(-6f, 8f),
                Random.Range(-6f, 8f)
            );
        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
