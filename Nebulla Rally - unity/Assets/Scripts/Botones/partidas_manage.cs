using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class partidas_manage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EscenaNuevaPartida()
    {
        SceneManager.LoadScene("PantallaPrincipal");
    }

    public void VolverEscena()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
