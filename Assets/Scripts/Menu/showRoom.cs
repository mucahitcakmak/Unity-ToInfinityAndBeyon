using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showRoom : MonoBehaviour
{

    public float rotSpeed = 20f;

    void OnMouseDrag(){
        float rotX = Input.GetAxis("Mouse X")*rotSpeed*Mathf.Deg2Rad;
        transform.RotateAround(Vector3.up, -rotX);
    }


}
