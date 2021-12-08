using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ship.AI
{

    [System.Serializable]
    public struct Sensor
    {
        public Transform transform;
        public float RayDistance;
        public float HitValidationDistance;
    }

    public enum States
    {
        wait,
        run,
        brake,
        finished
    }

    public class ShipIAManager : MonoBehaviour
    {
        public GameObject Competidores;
        int marker;
        Rigidbody rigidbody;
        GameObject n_vueltas;

        float speed = 0f;

        bool existCollision = false;

        public float maxSpeed = 150.0f;
        private float minSpeed = 0.0f;
        public float turboSpeed = 15.0f;
        public float brakeSpeed = 20.0f;

        public GameObject[] rcPoints;
        public LayerMask layer;
        public LayerMask layer2;
        public float hoverHeight;
        public float distanceHeight;

        private RaycastHit hit;
        private RaycastHit hit2;
        private RaycastHit hit3;
        private RaycastHit hit4;
        private RaycastHit hit5;
        private RaycastHit hit6;

        float turnDirection;

        float detectionHeightR;
        float detectionHeightL;

        // Start is called before the first frame update
        void Start()
        {
            n_vueltas = GameObject.Find("arcocarreras");
            rigidbody = Competidores.GetComponent<Rigidbody>();

        }

        // Update is called once per frame
        void Update()
        {
            if (speed < maxSpeed)
            {
                speed += 10.0f * Time.deltaTime;
            }
            else if (speed > minSpeed)
            {
                speed -= 10.0f * Time.deltaTime;
            }

        }

        void FixedUpdate()
        {
            //rigidbody.AddForce(transform.up * speed * accel);

            rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxSpeed);


            if (speed < maxSpeed)
            {
                rigidbody.velocity = -transform.forward * speed;
            }
            else if (speed > minSpeed)
            {
                rigidbody.velocity = -transform.forward * speed;
            }


            // speed brake if has collide
            if (existCollision == true && speed > brakeSpeed)
            {
                speed = speed - 2f;
            }

            //turbo
            /*
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed += turboSpeed * Time.deltaTime;
            }
            */

            //brake
            /*
            if (Input.GetKey(KeyCode.Space) && speed > minSpeed)
            {
                speed -= brakeSpeed * Time.deltaTime;
            }
            */

            // Adjust its position relative to the ground
            if (Physics.Raycast(rcPoints[0].transform.position, -rcPoints[0].transform.up, out hit, hoverHeight, layer))
            {
                Physics.Raycast(rcPoints[1].transform.position, -rcPoints[1].transform.up, out hit2, hoverHeight, layer);
                Physics.Raycast(rcPoints[2].transform.position, -rcPoints[2].transform.up, out hit3, hoverHeight, layer);

                Vector3 newUp = (hit.normal + hit2.normal + hit3.normal).normalized;

                float wantedHeight = hoverHeight - hit.distance;

                transform.rotation = Quaternion.FromToRotation(transform.up, newUp) * transform.rotation;

                transform.position += new Vector3(0, wantedHeight, 0) * 0.1f;

                Debug.DrawRay(rcPoints[0].transform.position, -rcPoints[0].transform.up * hoverHeight, Color.red);
                Debug.DrawRay(rcPoints[1].transform.position, -rcPoints[1].transform.up * hoverHeight, Color.red);
                Debug.DrawRay(rcPoints[2].transform.position, -rcPoints[2].transform.up * hoverHeight, Color.red);
            }
            else
            {
                transform.position -= Vector3.up * 1.25f;
                Debug.DrawRay(transform.position, -Vector3.up * 30f, Color.blue);
            }

            // left
            if (Physics.Raycast(rcPoints[4].transform.position, -rcPoints[4].transform.forward, out hit5, distanceHeight, layer2))
            {
                detectionHeightL = distanceHeight - hit5.distance;
                if (detectionHeightL < detectionHeightR)
                {
                    Debug.DrawRay(rcPoints[4].transform.position, -rcPoints[4].transform.forward * distanceHeight, Color.red);
                    var rotar = transform.up.y * Time.deltaTime * 20.0f;
                    transform.Rotate(0.0f, rotar, 0.0f);
                    Debug.Log("He chocado a la derecha");
                }
                else
                {
                    Debug.DrawRay(rcPoints[5].transform.position, -rcPoints[5].transform.forward * distanceHeight, Color.red);
                    var rotar = transform.up.y * Time.deltaTime * -20.0f;
                    transform.Rotate(0.0f, rotar, 0.0f);
                    Debug.Log("He chocado a la izquierda");
                }
            }

            //right
           if (Physics.Raycast(rcPoints[5].transform.position, -rcPoints[5].transform.forward, out hit6, distanceHeight, layer2))
            {
                detectionHeightR = distanceHeight - hit6.distance;
                if (detectionHeightL < detectionHeightR)
                {
                    Debug.DrawRay(rcPoints[4].transform.position, -rcPoints[4].transform.forward * distanceHeight, Color.red);
                    var rotar = transform.up.y * Time.deltaTime * 30.0f;
                    transform.Rotate(0.0f, rotar, 0.0f);
                    Debug.Log("He chocado a la derecha");
                }
                else
                {
                    Debug.DrawRay(rcPoints[5].transform.position, -rcPoints[5].transform.forward * distanceHeight, Color.red);
                    var rotar = transform.up.y * Time.deltaTime * -30.0f;
                    transform.Rotate(0.0f, rotar, 0.0f);
                    Debug.Log("He chocado a la izquierda");
                }
            }

            // front
            if (Physics.Raycast(rcPoints[3].transform.position, rcPoints[3].transform.forward, out hit4, distanceHeight, layer2))
            {
                Debug.DrawRay(rcPoints[3].transform.position, rcPoints[3].transform.forward * distanceHeight, Color.red);
                Debug.Log(transform.up);

                if (Physics.Raycast(rcPoints[4].transform.position, rcPoints[4].transform.forward, out hit5, distanceHeight, layer2))
                {
                    //transform.Rotate(0.0f, rotar, 0.0f);
                    Debug.Log("He chocado a la derecha");
                }

                if (Physics.Raycast(rcPoints[5].transform.position, rcPoints[5].transform.forward, out hit6, distanceHeight, layer2)) 
                {
                    //transform.Rotate(0.0f, transform.up * Time.deltaTime * -5.0f, 0.0f);
                    Debug.Log("He chocado a la izquierda");
                }
            }

        }

        void OnTriggerEnter ( Collider other )
        {
            if (other.gameObject.name == "checkpoint" && marker == 0)
            {
                marker++;
            }
            else if(other.gameObject.name == "checkpoint (1)" && marker == 1)
            {
                marker++;
            }
            else if (other.gameObject.name == "checkpoint (2)" && marker == 2)
            {
                marker++;
            }
            else if (other.gameObject.name == "checkpoint (3)" && marker == 3)
            {
                marker++;
            }
            else if (other.gameObject.name == "checkpoint (4)" && marker == 4)
            {
                marker++;
            }
            else if (other.gameObject.name == "checkpoint (5)" && marker == 5)
            {
                marker++;
            }
            else if (other.gameObject.name == "checkpoint (6)" && marker == 6)
            {
                marker++;
            }
            else if (other.gameObject.name == "checkpoint (7)" && marker == 7)
            {
                marker++;
            }
            else if (other.gameObject.name == "checkpoint (8)" && marker == 8)
            {
                marker++;
            }
            else if (other.gameObject.name == "checkpoint (9)" && marker == 9)
            {
                if ( n_vueltas.GetComponent<ContadorVueltas>().vueltas < 3 )
                {
                    marker = 0;
                }
                else
                {
                    //Competidores.Getcomponent<ShipAIManager>.enabled = false;
                    Debug.Log("Hemos acabado");
                }
            }
        }

        void OnCollisionEnter(Collision other)
        {
            existCollision = true;
        }

        void OnCollisionExit(Collision other)
        {
            existCollision = false;
        }

        void OnCollisionStay(Collision other)
        {
            existCollision = true;
        }
    }

}