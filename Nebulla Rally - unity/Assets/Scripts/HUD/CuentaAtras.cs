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
        GameObject nave1 = GameObject.Find("Nave (1)");
        GameObject nave2 = GameObject.Find("Nave (2)");
        GameObject nave3 = GameObject.Find("Nave (3)");

        nave.GetComponent<movimientoNave>().enabled = false;
        nave1.GetComponent<Follower>().enabled = false;
        nave2.GetComponent<Follower>().enabled = false;
        nave3.GetComponent<Follower>().enabled = false;

        for ( cuentaAtras = cuentaMAX; cuentaAtras > 0; cuentaAtras--)
        {
            cuentaText.text = cuentaAtras.ToString();
            yield return new WaitForSeconds(1);
        }

        nave.GetComponent<movimientoNave>().enabled = true;
        nave1.GetComponent<Follower>().enabled = true;
        nave2.GetComponent<Follower>().enabled = true;
        nave3.GetComponent<Follower>().enabled = true;
        cuentaText.enabled = false;
    }
}
