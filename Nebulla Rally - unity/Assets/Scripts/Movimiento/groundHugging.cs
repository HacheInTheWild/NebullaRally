using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundHugging : MonoBehaviour
{
    public GameObject carModel;
    private float rotationAmount;
    private RaycastHit hit;
    private RaycastHit hit2;
    private RaycastHit hit3;
    private RaycastHit hit4;
    private RaycastHit hit5;
    private Vector3 forwardDirection;

    public LayerMask layer;
    public float hoverHeight;

    public float speed = 0.0f;
    public float acceleration = 9.0f;
    public float maxSpeed = 150.0f;
    private float minSpeed = 0.0f;
    public float turboSpeed = 15.0f;
    public float brakeSpeed = 20.0f;

    Vector3 angVel;
    Vector3 shipRot;

    float deltaSpeed;
    public GameObject[] rcPoints;
    //public Rigidbody rb;
    
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
    }

    void Update()
    {

        // Rotate to align with terrain
        if (Physics.Raycast(rcPoints[0].transform.position, -rcPoints[0].transform.up, out hit, hoverHeight, layer))
        {
            Physics.Raycast(rcPoints[1].transform.position, -rcPoints[1].transform.up, out hit2, hoverHeight, layer);
            Physics.Raycast(rcPoints[2].transform.position, -rcPoints[2].transform.up, out hit3, hoverHeight, layer);

            Vector3 newUp = (hit.normal + hit2.normal + hit3.normal).normalized;

            float wantedHeight = hoverHeight - hit.distance;

            transform.up -= (transform.up - newUp) * 0.9f;

            this.transform.position += new Vector3(0, wantedHeight, 0) * 0.1f;

            Debug.DrawRay(rcPoints[0].transform.position, -rcPoints[0].transform.up * hoverHeight, Color.red);
            Debug.DrawRay(rcPoints[1].transform.position, -rcPoints[1].transform.up * hoverHeight, Color.red);
            Debug.DrawRay(rcPoints[2].transform.position, -rcPoints[2].transform.up * hoverHeight, Color.red);
        }
        else
        {
            transform.position -= Vector3.up * 0.25f;
            Debug.DrawRay(transform.position, -Vector3.up * 30f, Color.blue);

        }


        // Rotate with input
        if (speed > minSpeed)
        {
            shipRotation();
        }


        // Move forward (with acceleration and deceleration
        if (Input.GetKey(KeyCode.W) && speed < maxSpeed)
        {

            shipMovement(acceleration);
        }
        else if (speed > minSpeed)
        {
            speed -= 10.0f * Time.fixedDeltaTime;
            forwardDirection = carModel.transform.forward;
            transform.position -= forwardDirection * Time.deltaTime * speed;
        }

        //turbo
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed += turboSpeed * Time.fixedDeltaTime;
        }

        //brake
        if (Input.GetKey(KeyCode.Space) && speed > minSpeed)
        {
            speed -= brakeSpeed * Time.fixedDeltaTime;
        }

        //tilt
        if (speed > minSpeed)
        {
            //shipTilt();
        }
    }

    void shipMovement(float Accel)
    {
        speed += Accel * Time.fixedDeltaTime;
            
        forwardDirection = carModel.transform.forward;
        transform.position -= forwardDirection * Time.deltaTime * speed;
    }

    void shipRotation()
    {
        float turnDirection = Input.GetAxis("Horizontal");
        rotationAmount = turnDirection * Time.deltaTime * 80f;
        carModel.transform.Rotate(0.0f, rotationAmount, 0.0f);

        if (Input.GetKey(KeyCode.Space))
        {
            rotationAmount = turnDirection * Time.deltaTime;
            carModel.transform.Rotate(0.0f, rotationAmount, 0.0f);
        }
    }

    void shipTilt()
    {
        shipRot = transform.GetChild(0).localEulerAngles;

        if (shipRot.x > 180) shipRot.x -= 360;
        if (shipRot.y > 180) shipRot.y -= 360;
        if (shipRot.z > 180) shipRot.z -= 360;

        float turn = Input.GetAxis("Horizontal") * Mathf.Abs(Input.GetAxis("Horizontal")) * Time.fixedDeltaTime; ;
        angVel.y += turn * .5f;
        angVel.z -= turn * .5f;

        if (Input.GetKey(KeyCode.D))
        {
            angVel.y += 5;
            angVel.z += 20;
            //speed -= 5 * Time.fixedDeltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            angVel.y -= 5;
            angVel.z -= 20;
            //speed -= 5 * Time.fixedDeltaTime;
        }

        angVel -= angVel.normalized * angVel.sqrMagnitude * 0.65f * Time.fixedDeltaTime;

        transform.GetChild(0).Rotate(angVel * Time.fixedDeltaTime);

        Vector3 repos = -shipRot.normalized * .015f * (shipRot.sqrMagnitude + 500) * (1 + speed / maxSpeed) * Time.fixedDeltaTime;
        repos.y = rotationAmount;

        transform.GetChild(0).Rotate(repos);
    }
    
    void FixedUpdate()
    {

    }
}
