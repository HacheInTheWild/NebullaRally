using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Garaje : MonoBehaviour
{
    private GameObject canvas;
    private GameObject canvasColor;

    void Start()
    {
        canvas = GameObject.Find("Canvas");
        canvasColor = GameObject.Find("CanvasSk");
        canvasColor.SetActive(false);
    }

    public void PersonalizarNave()
    {
        canvas.SetActive(false);
        canvasColor.SetActive(true);
    }

    public void VolverGaraje()
    {
        canvasColor.SetActive(false);
        canvas.SetActive(true);
    }

    public void VolverEscena()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
