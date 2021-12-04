using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Velocimetro : MonoBehaviour
{
    public Text velocidad;
    float speed;
    GameObject nave;

    void Start()
    {
        nave = GameObject.Find("Nave");
    }

    void Update()
    {
        velocidad.text = "" + (int)nave.GetComponent<movimientoNave>().speed * 2;
    }
}
