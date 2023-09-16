using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro; 

public class gameUISystem : MonoBehaviour
{

    public CanvasGroup gamePanel;
    public CanvasGroup stopPanel;
    public CanvasGroup settPanel;
    public Slider sensibilityUI;

    PlayerInput playerInput;
    VehicleMovement vehicleMovement;



    void Start(){
        playerInput =  GameObject.Find("Flight").GetComponent<PlayerInput>();
        vehicleMovement =  GameObject.Find("Flight").GetComponent<VehicleMovement>();
        sensibilityUI.value = SaveManager.instance.shipSensibility;
    }



    void Update(){
        
    }

    public void openStopPanel(){
        stopPanel.gameObject.SetActive(true);
        gamePanel.DOFade(0, 0.15f).OnComplete(()=>gamePanel.gameObject.SetActive(false));
        stopPanel.DOFade(1, 0.15f).OnComplete(()=>Time.timeScale=0);
    }


    public void closeStopPanel(){
        Time.timeScale=1;
        gamePanel.gameObject.SetActive(true);
        gamePanel.DOFade(1, 0.15f).OnComplete(()=>stopPanel.gameObject.SetActive(false));
        stopPanel.DOFade(0, 0.15f);
    }

    public void openSettingsPanel(){
        settPanel.gameObject.SetActive(true);
        stopPanel.gameObject.SetActive(false);
    }

    public void closeSettingsPanel(){
        stopPanel.gameObject.SetActive(true);
        settPanel.gameObject.SetActive(false);
    }



    public void goGameSceneBtn(string sceneName){
        Application.LoadLevel(sceneName);
        Time.timeScale=1;
    }


    //FOR UI SYSTEM
    public void joystickSelect(){
        playerInput.isJoystick = true;
        playerInput.dynamicJoystick.gameObject.SetActive(true);
    }

    public void tiltSelect(){
        playerInput.isJoystick = false;
        playerInput.dynamicJoystick.gameObject.SetActive(false);
    }

    public void changeSensibilityGameScene(){
        SaveManager.instance.shipSensibility = sensibilityUI.value;
        playerInput.joyStickTilt = SaveManager.instance.shipSensibility;
        playerInput.keyBoardTilt = SaveManager.instance.shipSensibility;
        SaveManager.instance.Save();
    }

    public void resetSave(){
            Debug.Log("Tüm kayıtlar silindi");
            SaveManager.instance.money = 200; 
            SaveManager.instance.flightUnlocked = new bool[5] {true, false, false, false, false};
            SaveManager.instance.selected = new bool[5] {true, false, false, false, false};
            SaveManager.instance.shipSensibility = 0.5f;
            SaveManager.instance.Save();        
    }
}
