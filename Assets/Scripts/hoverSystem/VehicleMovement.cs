//This script handles all of the physics behaviors for the player's ship. The primary functions
//are handling the hovering and thrust calculations. 

using UnityEngine;

public class VehicleMovement : MonoBehaviour
{
    [Header("Stats")]
    public float speed;                     //The current forward speed of the ship
    public float maxSpeed = 90f;                    
    public float acceleration = 2f;                     
    public float driveForce = 17f;          //The force that the engine generates
    public float maxDriveForce = 50f;          
    public float extraSpeeder = 2f;          
    public ParticleSystem speedEffect;

    [Header("Drive Settings")]
    public float slowingVelFactor = .99f;   //The percentage of velocity the ship maintains when not thrusting (e.g., a value of .99 means the ship loses 1% velocity when not thrusting)
    public float brakingVelFactor = .95f;   //The percentage of velocty the ship maintains when braking
    public float angleOfRoll = 30f;         //The angle that the ship "banks" into a turn

    [Header("Hover Settings")]
    public float hoverHeight = 1.5f;        //The height the ship maintains when hovering
    public float maxGroundDist = 5f;        //The distance the ship can be above the ground before it is "falling"
    public float hoverForce = 300f;         //The force of the ship's hovering
    public LayerMask whatIsGround;          //A layer mask to determine what layer the ground is on
    public PIDController hoverPID;          //A PID controller to smooth the ship's hovering

    [Header("Physics Settings")]
    public Transform shipBody;              //A reference to the ship's body, this is for cosmetics
    public float terminalVelocity = 100f;   //The max speed the ship can go
    public float hoverGravity = 20f;        //The gravity applied to the ship while it is on the ground
    public float fallGravity = 80f;         //The gravity applied to the ship while it is falling

    [HideInInspector] public Rigidbody rigidBody;                    //A reference to the ship's rigidbody
    PlayerInput input;                      //A reference to the player's input                 
    bool isOnGround;                        //A flag determining if the ship is currently on the ground


    void Start(){
        rigidBody = GetComponent<Rigidbody>();
        input = GetComponent<PlayerInput>();
    }


    void FixedUpdate(){
        speed = Vector3.Dot(rigidBody.velocity, transform.forward);
        CalculatHover();
        CalculatePropulsion();


        //SPEED PARTİCLE
        var main = speedEffect.main;
        if(speed>=50){
            speedEffect.gameObject.SetActive(true);
            float pleaseSpeed = Mathf.Lerp(main.simulationSpeed, speed/10f, 0.1f);
            main.simulationSpeed = pleaseSpeed;
        }else{
            main.simulationSpeed = 0;
            speedEffect.gameObject.SetActive(false);
        }

    }

    void CalculatHover()
    {
        Vector3 groundNormal;

        Ray ray = new Ray(transform.position, -transform.up);

        RaycastHit hitInfo;

        isOnGround = Physics.Raycast(ray, out hitInfo, maxGroundDist, whatIsGround);

        //If the ship is on the ground...
        if (isOnGround){
            //...determine how high off the ground it is...
            float height = hitInfo.distance;
            //...save the normal of the ground...
            groundNormal = hitInfo.normal.normalized;
            //...use the PID controller to determine the amount of hover force needed...
            float forcePercent = hoverPID.Seek(hoverHeight, height);
            
            //...calulcate the total amount of hover force based on normal (or "up") of the ground...
            Vector3 force = groundNormal * hoverForce * forcePercent;
            //...calculate the force and direction of gravity to adhere the ship to the 
            //track (which is not always straight down in the world)...
            Vector3 gravity = -groundNormal * hoverGravity * height;

            //...and finally apply the hover and gravity forces
            rigidBody.AddForce(force, ForceMode.Acceleration);
            rigidBody.AddForce(gravity, ForceMode.Acceleration);
        }
        //...Otherwise...
        else{
            groundNormal = Vector3.up;

            Vector3 gravity = -groundNormal * fallGravity;
            rigidBody.AddForce(gravity, ForceMode.Acceleration);

            rigidBody.AddForce(transform.forward * driveForce, ForceMode.Acceleration);
            driveForce = Mathf.Lerp(driveForce, maxDriveForce, acceleration*0.1f*Time.fixedDeltaTime);            
        }

        Vector3 projection = Vector3.ProjectOnPlane(transform.forward, groundNormal);
        Quaternion rotation = Quaternion.LookRotation(projection, groundNormal);

        rigidBody.MoveRotation(Quaternion.Lerp(rigidBody.rotation, rotation, Time.deltaTime * 10f));

        float angle = angleOfRoll * -input.rudder;

        Quaternion bodyRotation = transform.rotation * Quaternion.Euler(0f, 0f, angle);
        shipBody.rotation = Quaternion.Lerp(shipBody.rotation, bodyRotation, Time.deltaTime * 10f);
    }

    void CalculatePropulsion(){
        float rotationTorque = input.rudder - rigidBody.angularVelocity.y;
        rigidBody.AddRelativeTorque(0f, rotationTorque, 0f, ForceMode.VelocityChange);
        float sidewaysSpeed = Vector3.Dot(rigidBody.velocity, transform.right);
        Vector3 sideFriction = -transform.right * (sidewaysSpeed / Time.fixedDeltaTime); 
        rigidBody.AddForce(sideFriction, ForceMode.Acceleration);

        if (input.thruster <= 0f)
            rigidBody.velocity *= slowingVelFactor;


        if (!isOnGround)
            return;


        if (input.isBraking)
            rigidBody.velocity *= brakingVelFactor;

      /*  float propulsion = driveForce*input.thruster - drag * Mathf.Clamp(speed, 0f, terminalVelocity);
        rigidBody.AddForce(transform.forward * propulsion, ForceMode.Acceleration);*/
    }

    public float GetSpeedPercentage(){
        return rigidBody.velocity.magnitude / terminalVelocity;
    }



    private void OnTriggerStay(Collider coll){
        if(coll.gameObject.CompareTag("Speeder")){
            if(speed<=maxSpeed){
                rigidBody.AddForce(transform.forward * driveForce, ForceMode.Acceleration);
                driveForce = Mathf.Lerp(driveForce, maxDriveForce, acceleration*0.1f*Time.fixedDeltaTime);                
            }
        }
        if(coll.gameObject.CompareTag("extraSpeeder")){
            driveForce = Mathf.Lerp(driveForce, maxDriveForce, acceleration*extraSpeeder*Time.fixedDeltaTime);                
        }

    }


}
