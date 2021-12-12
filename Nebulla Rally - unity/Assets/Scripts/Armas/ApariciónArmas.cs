using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApariciónArmas : MonoBehaviour
{
    public GameObject bomba;
    public GameObject bala;
    public Transform ubicacion;
    public Transform ubicacion1;
    public Transform ubicacion2;
    public Transform ubicacion3;
    public Transform ubicacion4;
    public Transform ubicacion5;
    private int cont;
    private int cont1;
    private int cont2;
    private int cont3;
    private int cont4;
    private int cont5;

    // Start is called before the first frame update
    void Start()
    {
        cont = 0;
        cont1 = 0;
        cont2 = 0;
        cont3 = 0;
        cont4 = 0;
        cont5 = 0;
        (Instantiate(bala, ubicacion.position, ubicacion.rotation) as GameObject).transform.parent = ubicacion.transform;
        (Instantiate(bala, ubicacion1.position, ubicacion1.rotation) as GameObject).transform.parent = ubicacion1.transform;
        (Instantiate(bomba, ubicacion2.position, ubicacion2.rotation) as GameObject).transform.parent = ubicacion2.transform;
        (Instantiate(bala, ubicacion3.position, ubicacion3.rotation) as GameObject).transform.parent = ubicacion3.transform;
        (Instantiate(bomba, ubicacion4.position, ubicacion4.rotation) as GameObject).transform.parent = ubicacion4.transform;
        (Instantiate(bala, ubicacion5.position, ubicacion5.rotation) as GameObject).transform.parent = ubicacion5.transform;
    }

    // Update is called once per frame
    void Update()
    {

        if (ubicacion.childCount < 1) {
            cont++;
            if (cont > 240) 
            { 
                cont = 0;
                (Instantiate(bala, ubicacion.position, ubicacion.rotation) as GameObject).transform.parent = ubicacion.transform;
            }
            
        }

        if (ubicacion1.childCount < 1)
        {
            cont1++;
            if (cont1 > 240) 
            { 
                cont1 = 0;
                (Instantiate(bala, ubicacion1.position, ubicacion1.rotation) as GameObject).transform.parent = ubicacion1.transform; 
            }
           
        }

        if (ubicacion2.childCount < 1)
        {
            cont2++;
            if (cont2 > 240) 
            { 
                cont2 = 0;
                (Instantiate(bomba, ubicacion2.position, ubicacion2.rotation) as GameObject).transform.parent = ubicacion2.transform; 
            }
           
            
        }

        if (ubicacion3.childCount < 1)
        {
            cont3++;
            if (cont3 > 240)
            {
                cont3 = 0;
                (Instantiate(bala, ubicacion3.position, ubicacion3.rotation) as GameObject).transform.parent = ubicacion3.transform;
            }
            
        }

        if (ubicacion4.childCount < 1)
        {
            cont4++;
            if (cont4 > 240)
            {
                cont4 = 0;
                (Instantiate(bomba, ubicacion4.position, ubicacion4.rotation) as GameObject).transform.parent = ubicacion4.transform;
            }
           
        }

        if (ubicacion5.childCount < 1)
        {
            cont5++;
            if (cont5 > 240) 
            { 
                cont5 = 0;
                (Instantiate(bala, ubicacion5.position, ubicacion5.rotation) as GameObject).transform.parent = ubicacion5.transform; 
            }
            
        }
    }
}
