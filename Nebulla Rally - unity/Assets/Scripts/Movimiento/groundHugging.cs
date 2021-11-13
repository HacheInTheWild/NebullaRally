using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundHugging : MonoBehaviour
{
    public GameObject carModel;
    public Transform raycastPoint;
    private float hoverHeight = 10.0f;
    public float speed = 0.0f;
    private float terrainHeight;
    private float rotationAmount;
    private RaycastHit hit;
    private Vector3 pos;
    private Vector3 forwardDirection;

    private float maxSpeed = 150.0f;
    private float minSpeed = 0.0f;
    private float turboSpeed = 15.0f;
    private float brakeSpeed = 20.0f;


    //public Vector3 cameraOffset; //I use (0,1,-3)

    float deltaSpeed;

    void Start()
    {

    }

    void Update()
    {
        // Keep at specific height above terrain
        pos = transform.position;
        float terrainHeight = Terrain.activeTerrain.SampleHeight(pos);
        transform.position = new Vector3(pos.x,
                                         terrainHeight + hoverHeight,
                                         pos.z);

        // Rotate to align with terrain
        Physics.Raycast(raycastPoint.position, Vector3.down, out hit);
        transform.up -= (transform.up - hit.normal) * 0.1f;

        // Rotate with input
        if (speed > minSpeed)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                rotationAmount = Input.GetAxis("Horizontal") * 50.0f;
                rotationAmount *= Time.deltaTime;
                carModel.transform.Rotate(0.0f, rotationAmount, 0.0f);
            }
            rotationAmount = Input.GetAxis("Horizontal") * 30.0f;
            rotationAmount *= Time.deltaTime;
            carModel.transform.Rotate(0.0f, rotationAmount, 0.0f);
        }


        // Move forward (with acceleration and deceleration
        if (Input.GetKey(KeyCode.W) && speed < maxSpeed) { 
            
            speed += 9.0f * Time.fixedDeltaTime;
            
            forwardDirection = carModel.transform.forward;
            transform.position -= forwardDirection * Time.deltaTime * speed;
        }
        else if(speed > minSpeed)
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

    }

    /*void FixedUpdate()
    {
        deltaSpeed = speed;
        //moves camera (make sure you're GetChild()ing the camera's index)
        //I don't mind directly connecting this to the speed of the ship, because that always changes smoothly
        this.gameObject.transform.GetChild(1).localPosition = cameraOffset + new Vector3(0, 0, -deltaSpeed * .001f);


        float sqrOffset = transform.GetChild(1).localPosition.sqrMagnitude;
        Vector3 offsetDir = transform.GetChild(1).localPosition.normalized;
    }*/

}