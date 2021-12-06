using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticulasMotores : MonoBehaviour
{
    public ParticleSystem central;
    public ParticleSystem derecho;
    public ParticleSystem izquierdo;

    // Start is called before the first frame update
    void Start()
    {
        var em1 = central.emission;
            em1.enabled = false;
        var em2 = derecho.emission;
            em2.enabled = false;
        var em3 = izquierdo.emission;
            em3.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        var em1 = central.emission;
        var em2 = derecho.emission;
        var em3 = izquierdo.emission;
        if (Input.GetKeyDown(KeyCode.W))
        {
            em1.enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            em3.enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            em2.enabled = true;
        }
        
    }
}
