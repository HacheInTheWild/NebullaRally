using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombas : MonoBehaviour
{
	//Declarlo la variable de tipo RigidBody que luego asociaremos a nuestro objeto
	private Rigidbody bomba;

	//Declaro la variable pública velocidad para poder modificarla desde la Inspector window
	[Range(0, 5)]
	public float velocidad = 1;

	void Start()
	{

		//Capturo el rigidbody del jugador al iniciar el juego
		bomba = GetComponent<Rigidbody>();


		//Aplico movimiento en dirección z positiva (con su velocidad)
		bomba.velocity = (transform.forward * velocidad);
		
	}

	void Update()
	{
		Destroy(bomba.gameObject, 5);
	}

}
