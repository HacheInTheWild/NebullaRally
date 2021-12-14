using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevitacionNave : MonoBehaviour
{
    Rigidbody rb;
    public GameObject[] rcPoints;
    public LayerMask layer;
    public float hoverHeight;
    public float fallingSpeed;

    private RaycastHit hit;
    private RaycastHit hit2;
    private RaycastHit hit3;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Adjust its position relative to the ground
        if (Physics.Raycast(rcPoints[0].transform.position, -rcPoints[0].transform.up, out hit, hoverHeight, layer))
        {
            Physics.Raycast(rcPoints[1].transform.position, -rcPoints[1].transform.up, out hit2, hoverHeight, layer);
            Physics.Raycast(rcPoints[2].transform.position, -rcPoints[2].transform.up, out hit3, hoverHeight, layer);

            Vector3 newUp = (hit.normal + hit2.normal + hit3.normal).normalized;

            float wantedHeight = hoverHeight - hit.distance;

            var vectorHeight = new Vector3(transform.position.x, wantedHeight, transform.position.z);


            var aux = Quaternion.FromToRotation(transform.up, newUp) * transform.rotation;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, aux, 0.7f); //keep the spaceship paralel to the ground

            rb.MovePosition(transform.position + Vector3.up * wantedHeight * Time.deltaTime); //adjustment of flying height

            Debug.DrawRay(rcPoints[0].transform.position, -rcPoints[0].transform.up * hoverHeight, Color.red);
            Debug.DrawRay(rcPoints[1].transform.position, -rcPoints[1].transform.up * hoverHeight, Color.red);
            Debug.DrawRay(rcPoints[2].transform.position, -rcPoints[2].transform.up * hoverHeight, Color.red);
        }
        else
        {
            rb.MovePosition(transform.position - Vector3.up * fallingSpeed * Time.deltaTime);
            Debug.DrawRay(transform.position, -Vector3.up * 30f, Color.blue);
        }
    }
}
