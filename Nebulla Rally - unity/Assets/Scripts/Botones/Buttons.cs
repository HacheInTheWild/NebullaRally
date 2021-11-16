using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{

    //Title Scene -> Main Menu
    public void Jugar()
    {
        SceneManager.LoadScene(1);
    }

    //Main Menu -> Nivel 1
    public void Carrera()
    {
        SceneManager.LoadScene(2);
    }

    public void SalirJuego()
    {
        Application.Quit();
        Debug.Log("Cerrando juego...");
    }
    

    //Settings
    //Seleccionar nivel
    //Cafe
    //Home
    //Garaje
    //Volver a atras

}
