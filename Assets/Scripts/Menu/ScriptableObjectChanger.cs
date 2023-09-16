using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptableObjectChanger : MonoBehaviour
{

    [SerializeField] private ScriptableObject[] scriptableObjects;
    [SerializeField] private FlightDisplay flightDisplay;
    int currentIndex;

    void Start()
    {
        currentIndex = SaveManager.instance.currentFlight;
        ChangeScriptableObject(0);
    }

   
    public void ChangeScriptableObject(int _change){
        currentIndex += _change;
        if(currentIndex<0) currentIndex = scriptableObjects.Length - 1;
        else if(currentIndex > scriptableObjects.Length - 1) currentIndex = 0;

        if(flightDisplay!=null) flightDisplay.DisplayFlight((Flight)scriptableObjects[currentIndex]);
    }

    public void selectFlight(int _change){
        currentIndex = _change;

        if(flightDisplay!=null) flightDisplay.DisplayFlight((Flight)scriptableObjects[currentIndex]);
    }

    public void currentShowFlight(){
        if(flightDisplay!=null) flightDisplay.buyOrSelect((Flight)scriptableObjects[currentIndex]);
    }




}
