using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using DG.Tweening;
using TMPro; 

public class menuUI : MonoBehaviour
{
    [Header("UIPages")]
    public RectTransform rBlackScreen;
    public RectTransform lBlackScreen;
    public RectTransform menuScreen;
    public RectTransform marketScreen;
    public RectTransform marketMenuScreen;
    public RectTransform settScreen;
    public RectTransform settMenuScreen;
    [Header("For Save DATA")]
    public Slider sensibilityUI;
    [Header("Models")]
    public Transform show;


    void Start()
    {
        DOTween.Init(true, false, LogBehaviour.Verbose);

        if (PlayerPrefs.GetInt("FIRSTTIMEOPENING", 1) == 1){
            Debug.Log("First Time Opening");
            PlayerPrefs.SetInt("FIRSTTIMEOPENING", 0);
            SaveManager.instance.flightUnlocked[0] = true;
            SaveManager.instance.flightUnlocked[1] = false;
            SaveManager.instance.flightUnlocked[2] = false;
            SaveManager.instance.flightUnlocked[3] = false;
            SaveManager.instance.flightUnlocked[4] = false;
            SaveManager.instance.selected[0] = true;
            SaveManager.instance.selected[1] = false;
            SaveManager.instance.selected[2] = false;
            SaveManager.instance.selected[3] = false;
            SaveManager.instance.selected[4] = false;
            SaveManager.instance.shipSensibility = 0.5f;

            SaveManager.instance.money += 10000; 
            SaveManager.instance.Save();
        }
        else{
            Debug.Log("NOT First Time Opening");
        }

        sensibilityUI.value = SaveManager.instance.shipSensibility;
    }


    public void Update(){
        SaveManager.instance.shipSensibility = sensibilityUI.value;
        SaveManager.instance.Save();
    }

    public void playBtn(){
        //audio.PlayOneShot(gelSound);
        rBlackScreen.DOAnchorPosX(950, 0.6f);
        lBlackScreen.DOAnchorPosX(-950, 0.6f);
        Invoke("loadScene", 1.8f);
    }    

    public void marketBtn(){
        //audio.PlayOneShot(gelSound);
        show.DOMoveY(-0.3f, 0.6f);
        menuScreen.DOAnchorPosY(300f, 0.6f);
        marketMenuScreen.DOAnchorPosY(-33f, 0.6f);
        marketScreen.DOAnchorPosY(66, 0.6f);
    }
    public void marketCloseBtn(){
        //audio.PlayOneShot(gelSound);
        show.DOMoveY(-8f, 0.6f);
        menuScreen.DOAnchorPosY(-33f, 0.6f);
        marketMenuScreen.DOAnchorPosY(300f, 0.6f);
        marketScreen.DOAnchorPosY(-1500, 0.6f);
    }

    public void settBtn(){
        //audio.PlayOneShot(gelSound);
        menuScreen.DOAnchorPosY(300f, 0.6f);
        settMenuScreen.DOAnchorPosY(-33f, 0.6f);
        settScreen.DOAnchorPosY(0, 0.6f);
    }
    public void settCloseBtn(){
        //audio.PlayOneShot(gelSound);
        menuScreen.DOAnchorPosY(-33f, 0.6f);
        settMenuScreen.DOAnchorPosY(300f, 0.6f);
        settScreen.DOAnchorPosY(-1500, 0.6f);
    }



    public void resetSave(){
            Debug.Log("Tüm kayıtlar silindi");
            SaveManager.instance.money = 200; 
            SaveManager.instance.flightUnlocked = new bool[5] {true, false, false, false, false};
            SaveManager.instance.selected = new bool[5] {true, false, false, false, false};
            SaveManager.instance.shipSensibility = 0.5f;
            sensibilityUI.value = 0.5f;
            SaveManager.instance.Save();        
    }

    public void btnAnim(string btnName, Vector2 vec1, Vector2 vec2){
        RectTransform btn = GameObject.Find(btnName).GetComponent<RectTransform>();
        btn.DOSizeDelta(vec1, 0.15f).OnComplete(()=>btn.DOSizeDelta(vec2, 0.15f));
    }
    void loadScene(){
        Application.LoadLevel("gameScene");
    }

}
