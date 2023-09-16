using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitInstantiate : MonoBehaviour
{


    [Header("Rays")]
    Transform Ray1;
    Transform Ray2;
    [Header("Vegetation Objects")]
    public GameObject grass1;
    public GameObject bush1;
    public GameObject rock1;
    public GameObject flower1;
    [Header("General Settings")]
    public float minRange = 5f;
    public float maxRange = 10f;

    VehicleMovement vehicleMovement;

    void Start(){
        vehicleMovement = GameObject.Find("Flight").GetComponent<VehicleMovement>();
        Ray1 = GameObject.Find("Vray1").transform;
        Ray2 = GameObject.Find("Vray2").transform;
    }

    void Update(){
        checkSpeed();

        createRay(bush1, Ray1, minRange, Color.red, 0f, 360f);
        createRay(bush1, Ray1, minRange, Color.red, 0f, 360f);
        createRay(bush1, Ray1, minRange, Color.red, 0f, 360f);
        createRay(bush1, Ray1, minRange, Color.red, 0f, 360f);
        createRay(grass1, Ray1, maxRange, Color.green, 0f, 360f);
        createRay(grass1, Ray1, maxRange, Color.green, 0f, 360f);
        createRay(rock1, Ray2, minRange, Color.red, 0f, 360f);
        createRay(rock1, Ray2, maxRange, Color.green, 0f, 360f);
        createRay(flower1, Ray2, minRange, Color.red, 0f, 360f);
    }

    void checkSpeed(){
        float currentSpeed = vehicleMovement.speed;
        if(currentSpeed<=30){
            createRay(bush1, Ray2, maxRange, Color.green, 0f, 360f);
            createRay(bush1, Ray1, minRange, Color.red, 0f, 360f);
            createRay(bush1, Ray1, minRange, Color.red, 0f, 360f);
            createRay(grass1, Ray2, minRange, Color.red, 0f, 360f);
            createRay(grass1, Ray2, minRange, Color.red, 0f, 360f);
            createRay(grass1, Ray2, minRange, Color.red, 0f, 360f);
            createRay(rock1, Ray1, maxRange, Color.green, 0f, 360f);
            createRay(rock1, Ray1, maxRange, Color.green, 0f, 360f);
        }else if(currentSpeed<=40){
            tekrarla();
            tekrarla();
        }else if(currentSpeed<=60){
            tekrarla();
            tekrarla();
            tekrarla();
            tekrarla();
        }
    }

    void createRay(GameObject Vobject, Transform Vposition, float range, Color color, float minRange, float maxRange){
        RaycastHit hit;
        float randomXY = Random.Range(minRange, maxRange);
        Vector3 direction = new Vector3(Mathf.Cos(randomXY), Mathf.Sin(randomXY), 0);
        Debug.DrawRay(Vposition.position , direction*range, color);
        if (Physics.Raycast(Vposition.position, direction, out hit, range)){
            if(hit.transform.tag == "Speeder"){
                Vector3 incomingVec = hit.point - Vposition.position;
                Vector3 reflectVec = Vector3.Reflect(incomingVec, hit.normal);
                Instantiate(Vobject, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal), hit.transform);
                float pleaseRandom = Random.Range(0.3f, 0.4f);
                Vobject.transform.localScale = new Vector3(pleaseRandom, pleaseRandom, pleaseRandom);
            }
        }  
    }


    void tekrarla(){
        createRay(bush1, Ray2, minRange, Color.red, 0f, 360f);
        createRay(bush1, Ray2, minRange, Color.red, 0f, 360f);
        createRay(bush1, Ray2, minRange, Color.red, 0f, 360f);
        createRay(grass1, Ray1, maxRange, Color.green, 0f, 360f);
        createRay(grass1, Ray1, maxRange, Color.green, 0f, 360f);
        createRay(rock1, Ray2, minRange, Color.red, 0f, 360f);
        createRay(flower1, Ray2, maxRange, Color.green, 0f, 360f);
        createRay(flower1, Ray2, maxRange, Color.green, 0f, 360f);        
    }


}
