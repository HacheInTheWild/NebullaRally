using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class main_menu_manage : MonoBehaviour
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

    public void EscenaPartidasGuardadas()
    {
        SceneManager.LoadScene("PartidaGuardada");
    }

    public void EscenaOpciones()
    {
        SceneManager.LoadScene("Opciones");
    }
}
