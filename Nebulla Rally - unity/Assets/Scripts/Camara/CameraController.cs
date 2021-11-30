using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // The target we are following
    public Transform target;
    // The distance in the x-z plane to the target
    public float distance = 30.0f;
    // the height we want the camera to be above the target
    public float height = 12.0f;
    // How much we 
    public float heightDamping = 2.0f;
    public float rotationDamping = 3.0f;

    void Start()
    {
        //transform.localPosition = target.
    }

    void Update() 
    { 
        // Early out if we don't have a target
        if (!target)
            return;

        Vector3 followpos = new Vector3(0f, height, distance); //posicion de la camara respecto a la nave (centro de la nave, altura especificada, detras de la nave)
        Quaternion lookrotation = Quaternion.identity;

        lookrotation.eulerAngles = new Vector3(30.0f, 180f, 0f); //inclinaci¨®n de la camara (diagonal hacia abajo para mirarla, mirar desde detras de la nave, inclinaci¨®n lateral)

        Matrix4x4 m1 = Matrix4x4.TRS(target.position, target.rotation, Vector3.one);
        Matrix4x4 m2 = Matrix4x4.TRS(followpos, lookrotation, Vector3.one);
        Matrix4x4 combined = m1 * m2;

        // THE WAY TO GET POSITION AND ROTATION FROM A MATRIX4X4:
        Vector3 position = combined.GetColumn(3);

        Quaternion rotation = Quaternion.LookRotation(
        combined.GetColumn(2),
        combined.GetColumn(1)
        );

        Quaternion wantedRotation = rotation;
        Quaternion currentRotation = transform.rotation;

        Vector3 wantedPosition = position;
        Vector3 currentPosition = transform.position;

        currentRotation = Quaternion.Lerp(currentRotation, wantedRotation, rotationDamping * Time.deltaTime);
        currentPosition = Vector3.Lerp(currentPosition, wantedPosition, heightDamping * 4.0f * Time.deltaTime);

        transform.localRotation = currentRotation;
        transform.localPosition = currentPosition;
    }
}
