using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApariciónArmas : MonoBehaviour
{
    public GameObject bomba;
    public GameObject bala;
    public Transform ubicacion;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponents<GameObject>().Length < 7) {
            for (int i = GetComponents<GameObject>().Length; i < 7; i++) {
                if (i == 0 || i == 2 || i == 3 || i == 5) { Instantiate(bala, ubicacion.position, ubicacion.rotation); }
                else { Instantiate(bomba, ubicacion.position, ubicacion.rotation); }
            }
        }
    }
}
