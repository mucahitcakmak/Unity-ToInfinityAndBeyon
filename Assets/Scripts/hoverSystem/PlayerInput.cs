//This script handles reading inputs from the player and passing it on to the vehicle. We 
//separate the input code from the behaviour code so that we can easily swap controls 
//schemes or even implement and AI "controller". Works together with the VehicleMovement script

using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("Platform")]
    public bool isMobile;

    [Header("Mobile")]
    public DynamicJoystick dynamicJoystick;
    public bool isJoystick;
    public float joyStickTilt = 0.6f;

    [Header("Windows")]
    public bool isKeyboard;
    public float keyBoardTilt = 0.6f;
    private Vector2 lookInput, screenCenter, mouseDistance;


    [Header("Variables")]
    string verticalAxisName = "Vertical";        //The name of the thruster axis
    string horizontalAxisName = "Horizontal";    //The name of the rudder axis
    string brakingKey = "Brake";                 //The name of the brake button

    //We hide these in the inspector because we want 
    //them public but we don't want people trying to change them
    public float thruster;            //The current thruster value
    public float rudder;              //The current rudder value
    public bool isBraking;            //The current brake value

    void Start(){
        joyStickTilt = SaveManager.instance.shipSensibility;
        keyBoardTilt = SaveManager.instance.shipSensibility;
        screenCenter.x = Screen.width*0.5f;
        screenCenter.y = Screen.height*0.5f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        thruster = 0;
        if(isMobile){
            if(isJoystick){
                joyStickModeFunc();
            }else{
                tiltModeFunc();
                }
        }else{
            if(isKeyboard){
                keyModeFunc();
            }else{
                mouseModeFunc();
            }
        }

    }




    //CONTROLLER
    public void joyStickModeFunc(){
        rudder = dynamicJoystick.Horizontal*joyStickTilt;
        //thruster = dynamicJoystick.Vertical*0.3f;
    }


    public void tiltModeFunc(){
        rudder = Input.acceleration.x*joyStickTilt*1.5f;
        //thruster = Input.acceleration.y;
    }

    public void mouseModeFunc(){
        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;

        mouseDistance.x = (lookInput.x - screenCenter.x) / screenCenter.y;
        mouseDistance.y = (lookInput.y - screenCenter.y) / screenCenter.y;

        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);
        //thruster = mouseDistance.y;
        rudder = mouseDistance.x*keyBoardTilt;
    }

    public void keyModeFunc(){
        //thruster = Input.GetAxis(verticalAxisName);
        rudder = Input.GetAxis(horizontalAxisName)*keyBoardTilt;
        //isBraking = Input.GetButton(brakingKey);
    }



}