using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CuentaAtras : MonoBehaviour
{
    public Text cuentaText;
    public int cuentaMAX;
    private int cuentaAtras;
    // Start is called before the first frame update
    void Start()
    {
        cuentaText.enabled = true;
        StartCoroutine(iniciarJuego());
        print("Llego");
    }

    // Cuenta atras
    public IEnumerator iniciarJuego()
    {

        GameObject nave = GameObject.Find("Nave");
        nave.GetComponent<movimientoNave>().enabled = false;

        for( cuentaAtras = cuentaMAX; cuentaAtras > 0; cuentaAtras--)
        {
            cuentaText.text = cuentaAtras.ToString();
            yield return new WaitForSeconds(1);
        }

        nave.GetComponent<movimientoNave>().enabled = true;
        cuentaText.enabled = false;
    }
}
