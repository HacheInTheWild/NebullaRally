using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{

    public void play()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void EscenaNuevaPartida()
    {
        SceneManager.LoadScene("PantallaPrincipal");
    }

    public void EscenaPartidasGuardadas()
    {
        SceneManager.LoadScene("PartidaGuardada");
    }
    public void EscenaCafe()
    {
        SceneManager.LoadScene("Cafetería");
    }

    public void EscenaGaraje()
    {
        SceneManager.LoadScene("Garaje");
    }

    public void EscenaCasa()
    {
        SceneManager.LoadScene("Casa");
    }

    public void EscenaCircuitos()
    {
        SceneManager.LoadScene("Circuitos");
    }

    public void VolverEscenaPrincipal()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void EscenaOpciones()
    {
        SceneManager.LoadScene("Opciones");
    }

    public void EscenaTestMonedero()
    {
        SceneManager.LoadScene("Circuito1");
    }

    public void VolverEscena()
    {
        SceneManager.LoadScene("PantallaPrincipal");
    }

    public void racing()
    {
        SceneManager.LoadScene("Racing Map 1");
    }

    public void Salir()
    {
        Application.Quit();
    }
}
