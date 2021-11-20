using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controlador1p : MonoBehaviour
{
    Rigidbody rb;
    Vector2 inputMov;
    Vector2 inputRot;
    public float velocidad = 10f;
    public float sensibilidad = 2;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        inputMov.x = Input.GetAxis("Horizontal");
        inputMov.y = Input.GetAxis("Vertical");

        inputRot.x = Input.GetAxis("Mouse X") * sensibilidad;
        inputRot.y = Input.GetAxis("Mouse Y") * sensibilidad;
    }

    private void FixedUpdate()
    {

        float vel = velocidad;
        rb.velocity = transform.forward * vel * inputMov.y
                      + transform.right * vel * inputMov.x;

        transform.rotation *= Quaternion.Euler(0, inputRot.x, 0);
    }
}
