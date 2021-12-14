using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimientoNave : MonoBehaviour
{
    Rigidbody rb;
    

    public float speed = 0.0f;
    public float acceleration = 9.0f;
    public float maxSpeed = 150.0f;
    private float minSpeed = 0.0f;
    public float rotationSpeed = 80f;
    public float turboSpeed = 15.0f;
    public float brakeSpeed = 20.0f;

    private Vector3 moveDirection;

    float turnDirection;
    Vector3 angVel;
    Vector3 shipRot;
    private float rotationAmount;

    //Declaro la variable de tipo GameObject que luego asociaremos a nuestro prefab Disparos
    public GameObject disparo;
    public GameObject bomba;

    //Declaro la variable de tipo Transform para la posición del disparador
    public Transform disparador;
    public Transform disparadorBomba;


    //Declaro la variable de tipo float velocidadDisparo para la velocidad con la que puedo generar disparos
    [Range(0, 1)]
    public float velocidadDisparo = 0.25f; //4 por segundo

    //Tiempo que tiene que transcurrir hasta el próximo disparo
    private float proximoDisparo;
    private int disparos;
    private int bombas;
    private int misiles;
    private float proximaBomba;


    bool existCollision = false;
    private int contBomba;
    private int contDisparo;

    private static int reducirBomba;
    private static int reducirBala;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        contBomba = 0;
        contDisparo = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (reducirBala == 1 && speed > 50) { speed--; }
        else {reducirBala = 0;}

        if (reducirBomba == 1 && speed > 10) { speed--; }
        else { reducirBomba = 0; }

        if (contDisparo > 0 && Input.GetButton("Fire1") && Time.time > proximoDisparo)
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

        turnDirection = Input.GetAxis("Horizontal");
        moveDirection = -transform.forward;

        if (Input.GetKey(KeyCode.W))
        {
            
        }
        else if (speed > minSpeed)
        {
        }
    }

    private void FixedUpdate()
    {

        // Move forward (with acceleration and deceleration)
        if (Input.GetKey(KeyCode.W))
        {
            if (speed >= maxSpeed)
            {
                speed = speed;
            }
            else
            {
                speed += acceleration * Time.deltaTime;
            }

            rb.velocity = moveDirection * speed;
        }
        else if (speed > minSpeed)
        {
            speed -= acceleration * Time.deltaTime;

            rb.velocity = moveDirection * speed;
        }

        // Rotate with input
        if (speed > minSpeed)
        {
            var rotationAmount = turnDirection * Time.deltaTime * rotationSpeed;
            transform.Rotate(0.0f, rotationAmount, 0.0f);
            //shipTilt();
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
    }

    void shipTilt()
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
            angVel.y += 50;
            angVel.z += 20;
            //speed -= 5 * Time.fixedDeltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            angVel.y -= 50;
            angVel.z -= 20;
            //speed -= 5 * Time.fixedDeltaTime;
        }

        angVel -= angVel.normalized * angVel.sqrMagnitude * 0.18f * Time.fixedDeltaTime;

        transform.Rotate(angVel * Time.fixedDeltaTime);

        Vector3 repos = -shipRot.normalized * .015f * (shipRot.sqrMagnitude + 500) * (1 + speed / maxSpeed) * Time.fixedDeltaTime;
        //repos.y = rotationAmount;

        transform.Rotate(repos);
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

        if (other.gameObject.tag == "CogerBomba") {
            contBomba += 2;
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "CogerBala")
        {
            contDisparo += 4;
            Destroy(other.gameObject);
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
