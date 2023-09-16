using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    [SerializeField] List<GameObject> levelParts;

    GameObject instantChildObject;
    Transform Ground;

    void Start(){
        Ground = GameObject.Find("Ground").transform;
    }

    void Update(){
        RaycastHit hit;
        Debug.DrawRay(transform.position , transform.forward*2, Color.yellow);
        if (Physics.Raycast(transform.position, transform.forward, out hit, 2)){
            if(hit.transform.tag == "levelEndPos"){
                Vector3 spawnPosition = hit.transform.gameObject.transform.GetChild(0).transform.position;
                print("End pos");
                GameObject choosenLevelPart = levelParts[Random.Range(0, levelParts.Count)];
                Instantiate(choosenLevelPart, spawnPosition, Quaternion.identity, Ground);
                Destroy(hit.transform.gameObject);
            }
        }  

    }



}
