using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moneda : MonoBehaviour
{
    public GameObject ObjPuntos;
    public int puntosQueDa;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        { 
            ObjPuntos.GetComponent<SistemaPuntos>().monedas += puntosQueDa;
            Destroy(gameObject); 
        }
    }
}


