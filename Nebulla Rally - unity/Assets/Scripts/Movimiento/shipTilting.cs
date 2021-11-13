using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipTilting : MonoBehaviour
{
    Rigidbody r;
    public float tilt;
    Vector3 angVel;
    Vector3 shipRot;



    void Start()
    {
        //r = this.gameObject.GetComponent<Rigidbody>();
    }

    
    void Update()
    {

        shipRot = transform.localEulerAngles;

        if (shipRot.x > 180) shipRot.x -= 360;
        if (shipRot.y > 180) shipRot.y -= 360;
        if (shipRot.z > 180) shipRot.z -= 360;

        float turn = Input.GetAxis("Horizontal") * Mathf.Abs(Input.GetAxis("Horizontal")) * Time.fixedDeltaTime; ;
        angVel.y += turn * .5f;
        angVel.z -= turn * .5f;

        if (Input.GetKey(KeyCode.D))
        {
            angVel.y += 20;
            angVel.z += 50;
        }
        else if(Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.Space))
        {
            angVel.y += 12;
            angVel.z -= 30;
        }

        if (Input.GetKey(KeyCode.A))
        {
            angVel.y -= 20;
            angVel.z -= 50;
        }

        angVel -= angVel.normalized * angVel.sqrMagnitude * 3f * Time.fixedDeltaTime;

        transform.Rotate(angVel * Time.fixedDeltaTime);

        transform.Rotate(-shipRot.normalized * .015f * (shipRot.sqrMagnitude + 500) * (1 + 50 / 150) * Time.fixedDeltaTime);

        /*
        if (turn != 0)
        {
            //r.rotation = Quaternion.Euler(0.0f, 0.0f, -tilt * Time.fixedDeltaTime);

        }*/
    }
}
