using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pantalla_principal_manager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EscenaCafe()
    {
        SceneManager.LoadScene("Cafeter�a");
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
}
