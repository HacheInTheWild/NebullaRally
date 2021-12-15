using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    //public float hoverHeight;
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
    private int contBomba;
    private int contDisparo;
    [Range(0, 1)]
    public float velocidadDisparo = 0.25f; //4 por segundo

    //Tiempo que tiene que transcurrir hasta el próximo disparo
    private float proximoDisparo;
    private float proximaBomba;

    //Declaro la variable de tipo GameObject que luego asociaremos a nuestro prefab Disparos
    public GameObject disparo;
    public GameObject bomba;

    //Declaro la variable de tipo Transform para la posición del disparador
    public Transform disparador;
    public Transform disparadorBomba;
    private static int reducirBala;
    private static int reducirBomba;

    // Start is called before the first frame update
    void Start()
    {
        n_vueltas = GameObject.Find("arcocarreras");
        rigidbody = Competidores.GetComponent<Rigidbody>();
        contBomba = 0;
        contDisparo = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (reducirBala == 1 && speed > 50) { speed--; }
        else { reducirBala = 0; }

        if (reducirBomba == 1 && speed > 10) { speed--; }
        else { reducirBomba = 0; } 

        if (contDisparo > 0 && Time.time > proximoDisparo)
        {
            //Incremento el valor de proximo disparo
            proximoDisparo = Time.time + velocidadDisparo;
            contDisparo = contDisparo - 1;

            //Instancio un nuevo disparo en esa posición y con esa rotación
            Instantiate(disparo, disparador.position, disparador.rotation);
        }

        if (contBomba > 0 && Input.GetKey(KeyCode.B) && Time.time > proximaBomba)
        {
            //Incremento el valor de proximo disparo
            proximaBomba = Time.time + velocidadDisparo;

            contBomba = contBomba - 1;

            //Instancio un nuevo disparo en esa posición y con esa rotación
            Instantiate(bomba, disparadorBomba.position, disparadorBomba.rotation);
        }

        if (speed < maxSpeed)
        {
            speed += 1f * Time.deltaTime;
        }
        else if (speed > minSpeed)
        {
            speed -= 1f * Time.deltaTime;
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

        /*
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
        */

        // front
        if (Physics.Raycast(rcPoints[3].transform.position, rcPoints[3].transform.forward, out hit4, distanceHeight, layer2))
        {
            Debug.DrawRay(rcPoints[3].transform.position, rcPoints[3].transform.forward * distanceHeight, Color.red);
            speed = speed - 2;

        }

    }

    void OnTriggerEnter ( Collider other )
    {
        //Para que no se destruya con los límites
        if (other.gameObject.tag == "Bala")
        {
            //Destruyo la bala (con la que ha chocado)
            Destroy(other.gameObject);
            reducirBala = 1;
        }

        if (other.gameObject.tag == "Bomba")
        {
            //Destruyo la bomba (con la que ha chocado)
            Destroy(other.gameObject);
            reducirBomba = 1;
        }

        if (other.gameObject.tag == "CogerBomba")
        {
            contBomba += 2;
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "CogerBala")
        {
            contDisparo += 4;
            Destroy(other.gameObject);
        }

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

