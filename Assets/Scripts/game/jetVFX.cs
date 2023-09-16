using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jetVFX : MonoBehaviour
{
    public float engineMinVol = 0f; 
    public float engineMaxVol = .6f;        
    public float engineMinPitch = .3f;      
    public float engineMaxPitch = .8f;

    PlayerInput input;                      
    VehicleMovement movement;    
    AudioSource engineAudio;

    void Start(){
        input = GetComponent<PlayerInput>();
        movement = GetComponent<VehicleMovement>();
        engineAudio = GetComponent<AudioSource>();
    }

    void Update(){
        jetSoundEffect();
    }


    void jetSoundEffect(){
        float speedPercent = movement.GetSpeedPercentage();
        if (engineAudio != null){
            //...modify the volume and pitch based on the speed of the ship
            engineAudio.volume = Mathf.Lerp(engineMinVol, engineMaxVol, speedPercent);
            engineAudio.pitch = Mathf.Lerp(engineMinPitch, engineMaxPitch, speedPercent);
        }
    }


}
