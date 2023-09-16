using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [Header("Platform Switch")]
    public bool isMobile;

    [Header("Mobile Settings")]
    public DynamicJoystick dynamicJoystick;
    public bool joyStickMode = true;

    [Header("Win Settings")]
    public bool mouseMode = true;

    [Header("General Settings")]
    public bool cursorVis = false;
    public ParticleSystem speedEffect;
    public Texture2D cursorTexture;
    public float flightSpeed = 30f;
    public float flightMaxSpeed = 100f;
    public float speedUp = 4f;
    public  float rotSpeed = 15f;

    private Vector2 lookInput, screenCenter, mouseDistance;
    CursorMode cursorMode = CursorMode.Auto;
    Vector2 hotSpot = Vector2.zero;

    void Start(){
        screenCenter.x = Screen.width*0.5f;
        screenCenter.y = Screen.height*0.5f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.None;
    }


    void Update(){
        //CURSOR
        if(cursorVis){Cursor.visible = false;}
        else{Cursor.visible = true;}//Cursor.lockState = CursorLockMode.Confined;
        //Cursor.lockState = CursorLockMode.Confined;
        //Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);


        var main = speedEffect.main;
        if(flightSpeed>=35){
            float pleaseSpeed = Mathf.Lerp(main.simulationSpeed, flightSpeed/100*4f, 0.1f);
            main.simulationSpeed = pleaseSpeed;
        }else{
            main.simulationSpeed = 0;
        }          

        rotSpeed = flightSpeed/2 + 3;
        //PLATFORM SWITCH
        if(isMobile){
            MobileMode();
        }else{
            WinMode();
        }

    }


    private void OnTriggerStay(Collider coll){
        if(coll.gameObject.CompareTag("Speeder")){
            if(flightSpeed<=flightMaxSpeed){
                flightSpeed += speedUp*Time.deltaTime;
            }
        }

        if(coll.gameObject.CompareTag("Plant")){
            float pleaseScale = Mathf.Lerp(0.3f, 0.6f, 1.5f);
            coll.transform.localScale = new Vector3(pleaseScale, pleaseScale, pleaseScale);
        }        

    }



    //MODES
    public void MobileMode(){
        if(joyStickMode){joyStickModeFunc();}
        else{tiltModeFunc();}
    }

    public void WinMode(){
        if(mouseMode){mouseModeFunc();}
        else{keyModeFunc();}
    }



    //CONTROLLER
    public void joyStickModeFunc(){
        float horizontal = dynamicJoystick.Horizontal*0.4f;
        float vertical = dynamicJoystick.Vertical*0.3f;

        generalMove(horizontal, vertical);
    }


    public void tiltModeFunc(){
        float horizontal = Input.acceleration.x;
        float vertical = Input.acceleration.y;

        generalMove(horizontal, vertical);
    }

    public void mouseModeFunc(){
        //Mouse Input
        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;

        mouseDistance.x = (lookInput.x - screenCenter.x) / screenCenter.y;
        mouseDistance.y = (lookInput.y - screenCenter.y) / screenCenter.y;

        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);

        generalMove(mouseDistance.x, mouseDistance.y);
    }

    public void keyModeFunc(){
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        generalMove(horizontal, vertical);
    }



    //MOVEMENT
    public void generalMove(float xVar, float yVar){
        Vector3 moveVector = transform.forward*flightSpeed;

        Vector3 yaw = xVar * transform.right * rotSpeed * Time.deltaTime*1.5f;
        Vector3 pitch = yVar * transform.up * rotSpeed * Time.deltaTime*1.5f;
        Vector3 dir = yaw + pitch;

        float maxX = Quaternion.LookRotation(moveVector+dir).eulerAngles.x;

        if(maxX < 90 && maxX > 70 || maxX > 270 && maxX < 290){   
        }else{
            moveVector += dir;
            transform.rotation = Quaternion.LookRotation(moveVector);
        }

        transform.position += moveVector * Time.deltaTime;
    }



}
