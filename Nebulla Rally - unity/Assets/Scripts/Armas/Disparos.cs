using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparos : MonoBehaviour
{
	//Declarlo la variable de tipo RigidBody que luego asociaremos a nuestro objeto
	private Rigidbody bala1;
	private Rigidbody bala2;

	//Declaro la variable pública velocidad para poder modificarla desde la Inspector window
	[Range(20, 1000)]
	public float velocidad = 20;

	void Start()
	{

		//Capturo el rigidbody del jugador al iniciar el juego
		bala1 = GetComponent<Rigidbody>();
		bala2 = GetComponent<Rigidbody>();


		//Aplico movimiento en dirección z positiva (con su velocidad)
		bala1.velocity = -(transform.forward * velocidad);
		bala2.velocity = -(transform.forward * velocidad);
	}

	void Update()
	{
		Destroy(bala1.gameObject, 4); 
		Destroy(bala2.gameObject, 4);
	}
	
}
