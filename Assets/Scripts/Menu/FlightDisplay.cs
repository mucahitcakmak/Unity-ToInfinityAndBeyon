using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FlightDisplay : MonoBehaviour
{
    [Header("Flight Descreption")]
    [SerializeField] private TextMeshProUGUI flightName;
    [SerializeField] private TextMeshProUGUI flightDescreption;
    [SerializeField] private TextMeshProUGUI flightPrice;

    [Header("Flight Stats")]
    [SerializeField] private Slider flightMaxSpeed;
    [SerializeField] private Slider fligtAcceleration;

    [Header("Flight Transform")]
    [SerializeField] private Transform flightHolder;



    public void Start(){

    }


    public void DisplayFlight(Flight _flight){
        flightName.text = _flight.flightName;
        //flightDescreption.text = _flight.flightDescreption;

        flightMaxSpeed.value = _flight.maxSpeed;
        fligtAcceleration.value = _flight.acceleration;

        if(flightHolder.childCount > 0){
            Destroy(flightHolder.GetChild(0).gameObject);}

        Instantiate(_flight.flightModel, flightHolder.position, flightHolder.rotation, flightHolder);

        if(SaveManager.instance.flightUnlocked[_flight.flightId]){
            flightPrice.text =  "SELECT";
            if(SaveManager.instance.selected[_flight.flightId]){
                for(int i = 0; i < SaveManager.instance.selected.Length; i++) {
                    SaveManager.instance.selected[i] = false;
                }
                SaveManager.instance.selected[_flight.flightId] = true;
                flightPrice.text =  "SELECTED";
                SaveManager.instance.currentFlight = _flight.flightId;
                SaveManager.instance.Save();
        }else flightPrice.text =  "SELECT";
        }else flightPrice.text =  _flight.flightPrice.ToString();
    }

    public void buyOrSelect(Flight _flight){
        if(SaveManager.instance.selected[_flight.flightId]){
            return;
        }else
        {
            if(SaveManager.instance.flightUnlocked[_flight.flightId])
            {
                for(int i = 0; i < SaveManager.instance.selected.Length; i++) SaveManager.instance.selected[i] = false;
                SaveManager.instance.selected[_flight.flightId] = true;
                flightPrice.text =  "SELECTED";
                SaveManager.instance.currentFlight = _flight.flightId;
                SaveManager.instance.Save();
            }else
            {
                if(SaveManager.instance.money >= _flight.flightPrice)
                {
                    print("Satın alma işlemi gercekleştirildi");
                    SaveManager.instance.flightUnlocked[_flight.flightId] = true;
                    SaveManager.instance.money -= _flight.flightPrice;
                    flightPrice.text =  "SELECT";
                }else print("Para yetersiz");
            }
        }


    }

}
