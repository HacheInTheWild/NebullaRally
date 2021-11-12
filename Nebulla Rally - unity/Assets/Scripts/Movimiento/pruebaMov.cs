using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}


public class pruebaMov : MonoBehaviour
{
    public float speed;
    public float tilt;
    public Boundary boundary;

    Rigidbody r;

    void Start()
    {
        r = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = -Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        r.velocity = movement * speed;

        r.position = new Vector3
        (
            Mathf.Clamp(r.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(r.position.z, boundary.zMin, boundary.zMax)
        );

        r.rotation = Quaternion.Euler(0.0f, 0.0f, r.velocity.x * -tilt);
    }
}
