using UnityEngine;
using PathCreation;

public class Follower : MonoBehaviour
{
    public GameObject Competidores;
    int marker;
    Rigidbody rigidbody;
    GameObject n_vueltas;

    bool existCollision = false;

    public float maxSpeed = 0.1f;
    private float minSpeed = 0.0f;
    public float turboSpeed = 15.0f;
    public float brakeSpeed = 13.0f;

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

    public PathCreator pathcreator;
    public float speed = 0f;
    float distanceTravelled;

    void Start()
    {
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
            speed += 0.5f * Time.deltaTime;
        }
        else if (speed > minSpeed)
        {
            speed -= 1f * Time.deltaTime;
        }

        distanceTravelled += speed * Time.deltaTime;
        transform.position = pathcreator.path.GetPointAtDistance(-distanceTravelled);
        transform.rotation = pathcreator.path.GetRotationAtDistance(-distanceTravelled);
    }

    void FixedUpdate()
    {
        // speed brake if has collide
        if (existCollision == true && speed > brakeSpeed)
        {
            speed = speed - 2f;
        }

        if (Physics.Raycast(rcPoints[0].transform.position, rcPoints[0].transform.forward, out hit4, distanceHeight, layer2) || Physics.Raycast(rcPoints[1].transform.position, rcPoints[1].transform.forward, out hit5, distanceHeight, layer2) || Physics.Raycast(rcPoints[2].transform.position, rcPoints[2].transform.forward, out hit6, distanceHeight, layer2))
        {
            Debug.DrawRay(rcPoints[0].transform.position, rcPoints[0].transform.forward * distanceHeight, Color.red);
            speed = speed - 2;
        }
    }

    void OnTriggerEnter(Collider other)
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

