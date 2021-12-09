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

    private Vector3 moveDirection;

    float turnDirection;
    Vector3 angVel;
    Vector3 shipRot;
    private float rotationAmount;

    //Declaro la variable de tipo GameObject que luego asociaremos a nuestro prefab Disparos
    public GameObject disparo;
    public GameObject bomba;

    //Declaro la variable de tipo Transform para la posici�n del disparador
    public Transform disparador;
    public Transform disparadorBomba;


    //Declaro la variable de tipo float velocidadDisparo para la velocidad con la que puedo generar disparos
    [Range(0, 1)]
    public float velocidadDisparo = 0.25f; //4 por segundo

    //Tiempo que tiene que transcurrir hasta el pr�ximo disparo
    private float proximoDisparo;
    private int disparos;
    private int bombas;
    private int misiles;
    private float proximaBomba;


    bool existCollision = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > proximoDisparo)
        {

            //Incremento el valor de proximo disparo
            proximoDisparo = Time.time + velocidadDisparo;



            //Instancio un nuevo disparo en esa posici�n y con esa rotaci�n
            Instantiate(disparo, disparador.position, disparador.rotation);
        }

        if (Input.GetKey(KeyCode.B) && Time.time > proximaBomba)
        {

            //Incremento el valor de proximo disparo
            proximaBomba = Time.time + velocidadDisparo;



            //Instancio un nuevo disparo en esa posici�n y con esa rotaci�n
            Instantiate(bomba, disparadorBomba.position, disparadorBomba.rotation);
        }

        turnDirection = Input.GetAxis("Horizontal");
        moveDirection = -transform.forward;

        if (Input.GetKey(KeyCode.W))
        {
            if (speed >= maxSpeed) 
            {
                speed += 0.00001f * Time.deltaTime;
            }
            else
            {
                speed += acceleration * Time.deltaTime;
            }
        }
        else if (speed > minSpeed)
        {
            speed -= acceleration * Time.deltaTime;
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
       
        //Para que no se destruya con los l�mites
        if (other.gameObject.tag == "Bala")
        {
            //Destruyo la bala (con la que ha chocado)
            Destroy(other.gameObject);
            speed = speed / 10;
           

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
