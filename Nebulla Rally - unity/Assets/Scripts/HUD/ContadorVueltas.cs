using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ContadorVueltas : MonoBehaviour
{
    public int vueltas = 0;
    public Text contadorVueltas;
    public Text GameOver;
    public Text Reset;
    public GameObject ObjPuntos;

    void Start()
    {
        GameOver.enabled = false;
        Reset.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        contadorVueltas.text = vueltas.ToString() + "/3";
        if ( vueltas > 3)
        {
            contadorVueltas.text = "3/3";
            GameObject nave = GameObject.Find("Nave");
            GameOver.enabled = true;
            Reset.enabled = true;
            nave.GetComponent<movimientoNave>().enabled = false;
            if (Input.GetKey(KeyCode.R))
            {
                SceneManager.LoadScene("escenaanimaciones");
            }
            //nave.GetComponent<movimientoNave>().speed = 0;
        }
    }

    void OnTriggerEnter( Collider other)
    {
        if (other.CompareTag("player"))
        {
            vueltas = vueltas + 1;
        }
    }
}
