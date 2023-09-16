using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightChoose : MonoBehaviour
{
    public GameObject[] flights;
    
    void Awake(){
        chooseFlight(SaveManager.instance.currentFlight);
    }

    public void chooseFlight(int _index){
        foreach(GameObject flight in flights){
            flight.SetActive(false);
        }
        flights[_index].SetActive(true);
    }

    
    
}
