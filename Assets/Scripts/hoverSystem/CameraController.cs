using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform targetPoint;
    public float moveSpeed = 8f, rotateSpeed = 3f;

    void Start(){
        targetPoint = GameObject.Find("CameraTarget").transform;
    }

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, targetPoint.position, moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetPoint.rotation, rotateSpeed * Time.deltaTime);
    }

}
