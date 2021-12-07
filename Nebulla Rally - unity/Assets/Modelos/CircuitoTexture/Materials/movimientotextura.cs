using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimientotextura : MonoBehaviour
{

     public int materialIndex = 0;
    public Vector2 uvAnimationRate = new Vector2( 1.0f, 0.0f );
    public string textureName = "_MainTex";


 Vector2 uvOffset = Vector2.zero;

    // Start is called before the first frame update

    // Update is called once per frame
    void LateUpdate()
    {
         uvOffset += ( uvAnimationRate * Time.deltaTime );
     if( GetComponent<Renderer>().enabled )
     {
         GetComponent<Renderer>().materials[ materialIndex ].SetTextureOffset( textureName, uvOffset );
     }     
    }
}
