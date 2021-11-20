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

    public void racing()
    {
        SceneManager.LoadScene("Racing Map 1");
    }

}
