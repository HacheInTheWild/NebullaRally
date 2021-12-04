using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimientoNave : MonoBehaviour
{
    Rigidbody rb;
    Rigidbody rb2;
    Vector2 inputMov;
    Vector2 inputRot;
    public float velocidad = 10f;
    public float sensibilidad = 2;

    public float speed = 0.0f;
    public float acceleration = 9.0f;
    public float maxSpeed = 150.0f;
    private float minSpeed = 0.0f;
    public float turboSpeed = 15.0f;
    public float brakeSpeed = 20.0f;

    public GameObject[] rcPoints;
    public LayerMask layer;
    public float hoverHeight;

    private RaycastHit hit;
    private RaycastHit hit2;
    private RaycastHit hit3;
    private RaycastHit hit4;
    private RaycastHit hit5;
    private Vector3 moveDirection;

    float turnDirection;

    bool existCollision = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        turnDirection = Input.GetAxis("Horizontal");
        moveDirection = -transform.forward;

        if (Input.GetKey(KeyCode.W) && speed < maxSpeed)
        {
            speed += 10.0f * Time.deltaTime;
        }
        else if (speed > minSpeed)
        {
            speed -= 10.0f * Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {

        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);

        // Move forward (with acceleration and deceleration)
        if (Input.GetKey(KeyCode.W) && speed < maxSpeed)
        {
            rb.velocity = moveDirection * speed;
        }
        else if (speed > minSpeed)
        {
            rb.velocity = moveDirection * speed;
        }

        // Rotate with input
        if (speed > minSpeed)
        {
            var rotationAmount = turnDirection * Time.deltaTime * 80f;
            transform.Rotate(0.0f, rotationAmount, 0.0f);
        }
        
        // speed brake if has collide
        if (existCollision == true && speed > brakeSpeed)
        {
            speed = speed - 2f;
        }

        //turbo
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed += turboSpeed * Time.deltaTime;
        }

        //brake
        if (Input.GetKey(KeyCode.Space) && speed > minSpeed)
        {
            speed -= brakeSpeed * Time.deltaTime;
        }

        // Adjust its position relative to the ground
        if (Physics.Raycast(rcPoints[0].transform.position, -rcPoints[0].transform.up, out hit, hoverHeight, layer))
        {
            Physics.Raycast(rcPoints[1].transform.position, -rcPoints[1].transform.up, out hit2, hoverHeight, layer);
            Physics.Raycast(rcPoints[2].transform.position, -rcPoints[2].transform.up, out hit3, hoverHeight, layer);

            Vector3 newUp = (hit.normal + hit2.normal + hit3.normal).normalized;

            float wantedHeight = hoverHeight - hit.distance;
            var vectprHeight = new Vector3(transform.position.x, wantedHeight, transform.position.z);

            transform.rotation = Quaternion.FromToRotation(transform.up, newUp) * transform.rotation;

            transform.position += new Vector3(0, wantedHeight, 0) * .1f;

            Debug.DrawRay(rcPoints[0].transform.position, -rcPoints[0].transform.up * hoverHeight, Color.red);
            Debug.DrawRay(rcPoints[1].transform.position, -rcPoints[1].transform.up * hoverHeight, Color.red);
            Debug.DrawRay(rcPoints[2].transform.position, -rcPoints[2].transform.up * hoverHeight, Color.red);
        }
        else
        {
            transform.position -= Vector3.up * 1.25f;
            Debug.DrawRay(transform.position, -Vector3.up * 30f, Color.blue);
        }
    }

    void OnCollisionEnter( Collision other )
    {
        existCollision = true;
    }

    void OnCollisionExit (Collision other)
    {
        existCollision = false;
    }

    void OnCollisionStay(Collision other)
    {
        existCollision = true;
    }


}
