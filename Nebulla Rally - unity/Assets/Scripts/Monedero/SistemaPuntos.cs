using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SistemaPuntos : MonoBehaviour
{
    public int monedas;
    public Text textoMonedas;

    private void Update()
    {
        textoMonedas.text = "Monedas " + monedas.ToString();
    }
}
