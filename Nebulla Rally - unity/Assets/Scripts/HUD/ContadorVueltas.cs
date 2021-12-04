using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContadorVueltas : MonoBehaviour
{
    public int vueltas = 0;
    public Text contadorVueltas;
    public GameObject ObjPuntos;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        contadorVueltas.text = vueltas.ToString() + "/3";
    }

    void OnTriggerEnter( Collider other)
    {
        if (other.CompareTag("player"))
        {
            vueltas = vueltas + 1;
        }
    }
}
