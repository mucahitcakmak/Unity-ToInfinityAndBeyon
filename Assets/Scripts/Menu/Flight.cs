using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Flight", menuName="ScriptableObject/Flight")]
public class Flight : ScriptableObject
{
    [Header("Descreption")]
    public int flightId;
    public string flightName;
    public string flightDescreption;

    [Header("Stats")]
    public int flightPrice;
    public float maxSpeed;
    public float acceleration;

    [Header("3D Model")]
    public GameObject flightModel;

}
