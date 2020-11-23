using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : MonoBehaviour
{
    public Rigidbody chestBody;

    public float jumpCounter = 0;
    public float jumpDelay = 0.4f;
    public float airTimeDelay = 2;
    public bool jumpAnticipation = false;
    public bool inAir = false;
    public float jumpForce = 100;
    public float jumpForwardForce = 50;
    public float facePlantForce = 250;
    protected float facePlantM = 1;
    public float getUpCounter = 0;

    public JointBasedWalking legs;

    public JointBasedMovement movement;

    public ConfigurableJoint[] thighJoints;
    public ConfigurableJoint[] legJoints;

    public ConfigurableJoint[] armsJoint;
    public ConfigurableJoint[] forearmsJoint;

    public ConfigurableJoint hipJoint;

    public bool canJump = true;

    public ConfigurableJoint chestJoint;

    public bool spin = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump") && canJump)
        {
            StartJumpAnticipation();
        }
        if (getUpCounter > 0)
        {
            getUpCounter -= Time.deltaTime;
            //
            // *****************  LIFT ARMS OFF OF THE GROUND SLOWLY WHEN GETTING UP ************
            //
            
        }
        if (jumpAnticipation)
        {
            //***********************************  CROUCHING BEFORE JUMP **********************
            //
            jumpCounter += Time.deltaTime;
            if (jumpCounter >= jumpDelay)
            {
                //legs.enabled = true;
                Jump();
            }
        }
        else if (inAir)
        {
            //***********************************  AIR BORNE **********************
            //
            jumpCounter += Time.deltaTime;
            if (jumpCounter >= airTimeDelay)
            {
                GetUpFromJump();
            }
            //
        }

    }

    void FixedUpdate()
    {
        // *************  I FIND APPLYING FORCES IN FIXED UPDATE TO BE MORE RELIABLE THAN IN UPDATE ****
        
        
        if (inAir)
        {
            //
            // *******************************************  TOWARDS END OF JUMP, FORCE A FACEPLANT *****
            //
            if (jumpCounter > airTimeDelay * 0.05f && jumpCounter < airTimeDelay * 0.8f)
            {
                //chestBody.AddForceAtPosition((movement.inputDirection + Vector3.down) * facePlantForce * facePlantM * Time.deltaTime, chestBody.transform.TransformPoint(Vector3.up * 2), ForceMode.Impulse);
                //
                if (jumpCounter > airTimeDelay * 0.1f && jumpCounter < airTimeDelay * 0.8f)
                {
                    thighJoints[0].targetRotation = new Quaternion(1.45f, 0, 0, thighJoints[0].targetRotation.w);
                    legJoints[0].targetRotation = new Quaternion(-3f, 0, 0, legJoints[0].targetRotation.w);


                    thighJoints[1].targetRotation = new Quaternion(1.45f, 0, 0, thighJoints[0].targetRotation.w);
                    legJoints[1].targetRotation = new Quaternion(-3f, 0, 0, legJoints[0].targetRotation.w);
                }
                    

                if (spin)
                {
                    

                    chestBody.maxAngularVelocity = Mathf.Infinity;

                    var inputDirection = movement.inputDirection;//Camera.main.transform.TransformDirection(movement.inputDirection);
                    inputDirection.y = 0.0f;

                    var testVector2 = ConvertMoveInputAndPassItToAnimator(inputDirection);
                    if (testVector2 != Vector3.zero)
                    {
                        //print("testVector2 : " + testVector2);
                        JointDrive x = chestJoint.slerpDrive;
                        x.positionDamper = 5000;
                        x.positionSpring = 100000;
                        x.maximumForce = 100000;
                        chestJoint.slerpDrive = x;

                        Vector3 jointValue = testVector2 * 90;



                        chestJoint.targetRotation = Quaternion.Euler(jointValue);




                    }

                    else
                    {
                        //print("testVector2 : " + testVector2);
                        //JointDrive x = chestJoint.slerpDrive;
                        //x.positionDamper = 0;
                        //x.positionSpring = fitnessSpring * fitnessReduceFactor;
                        //x.maximumForce = fitnessForce * fitnessReduceFactor;
                        //chestJoint.slerpDrive = x;

                        //chestJoint.targetAngularVelocity = new Vector3(0f, 0f, 0f);



                       
                        JointDrive x = chestJoint.slerpDrive;
                        x.positionDamper = 1000;
                        x.positionSpring = 10000;
                        x.maximumForce = 10000;
                        chestJoint.slerpDrive = x;


                        chestJoint.targetRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));




                        //hipFaceDirection.bodyForward.x = 0;


                    }


                    Vector3 middleFinger = Vector3.Cross(Vector3.up, inputDirection);
                    Debug.DrawRay(chestBody.transform.position, middleFinger * 3, Color.yellow);

                    

                    var inputDirection2 = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

                    //print("inputDirection2 : " + inputDirection2);
                    if (inputDirection2.z > 0)
                    {
                        chestBody.AddTorque(middleFinger * 130);
                    }
                    else
                    {
                        chestBody.AddTorque(middleFinger * 200);
                    }
                    

                    //Debug.Log("angular velocity : " + chestBody.angularVelocity.magnitude);

                }

            }
        }
    }

    public Vector3 ConvertMoveInputAndPassItToAnimator(Vector3 moveInput)
    {
        //Convert the move input from world positions to local positions so that they have the correct values
        //depending on where we look
        Vector3 localMove = chestBody.transform.InverseTransformDirection(moveInput);
        //localMove.Normalize();
        float turnUpDownAmount = localMove.z;
        float turnLeftRightdAmount = localMove.y;

        /* if (turnAmount != 0)
             turnAmount *= 2;

         */
        if (false)
        {
            print("Forward : " + turnUpDownAmount);
            print("Sideways : " + turnLeftRightdAmount);
            // print("testVector : " + testVector);
           // print("inputdirection : " + inputDirection);
            // print("hipFacing : " + hipFacing.facingDirection);
        }
        //x = -turnAmount

        //Use this following with chestBody
        //return new Vector3(-turnUpDownAmount, 0f, 0f);


        //Use this following with rotationTarget
        return new Vector3(-turnUpDownAmount, turnLeftRightdAmount, 0f);

        //return new Vector3(-turnUpDownAmount, 0f, forwardAmount);
    }

    private void StartJumpAnticipation()
    {
        // ***********************  CROUCH A BIT UNTIL THE ACTUAL JUMP *******
        canJump = false;
        legs.StopWalking();
        jumpAnticipation = true;
        legs.enabled = false;

        Debug.Log("$Start Jump Anticipation");

        thighJoints[0].targetRotation = new Quaternion(1.45f, 0, 0, thighJoints[0].targetRotation.w);
        legJoints[0].targetRotation = new Quaternion(-3f, 0, 0, legJoints[0].targetRotation.w);


        thighJoints[1].targetRotation = new Quaternion(1.45f, 0, 0, thighJoints[0].targetRotation.w);
        legJoints[1].targetRotation = new Quaternion(-3f, 0, 0, legJoints[0].targetRotation.w);


        JointDrive x = chestJoint.slerpDrive;
        x.positionDamper = 1000;
        x.positionSpring = 10000;
        x.maximumForce = 10000;
        chestJoint.slerpDrive = x;


        chestJoint.targetRotation = Quaternion.Euler(new Vector3(10f, 0f, 0f));

        // movement.enableChestRotation = false;


        //var inputDirection = movement.inputDirection;//Camera.main.transform.TransformDirection(movement.inputDirection);
        //inputDirection.y = 0.0f;

        //Vector3 middleFinger = Vector3.Cross(Vector3.up, inputDirection);
        //Debug.DrawRay(chestBody.transform.position, middleFinger * 3, Color.yellow);



        //var inputDirection2 = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        //print("inputDirection2 : " + inputDirection2);
        //if (inputDirection2.z > 0)
        //{
        //    chestBody.AddTorque(middleFinger * 150);
        //}
        //else
        //{
        //    chestBody.AddTorque(middleFinger * 500);
        //}
        //movement.LookAtTarget(Camera.main.transform.forward);

        //hipJoint.targetRotation = new Quaternion(3, 0, 0, 1);
        //maintainHeight.desiredHeight = maintainHeightCrouching;

        //var inputDirection2 = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        //hipJoint.targetRotation = new Quaternion(0, -inputDirection2.z, inputDirection2.x, 1);
        jumpCounter = 0;
    }

    private void Jump()
    {
        // ***********************  ACTUALLY JUMP - Launch into the air *******
        //
        //
        // **** DISABLE SOME CONTROLLING COMPONENTS (the height maintaining script on the torso and upright forces on feet) ****
        //

        Debug.Log("$Jump");


        JointDrive x = thighJoints[0].slerpDrive;
        x.positionDamper = 0;
        x.positionSpring = 0;
        x.maximumForce = 0;

        x.positionDamper = 100;
        x.positionSpring = 2000;
        x.maximumForce = 2000;

        thighJoints[0].slerpDrive = x;

        x = thighJoints[1].slerpDrive;
        x.positionDamper = 0;
        x.positionSpring = 0;
        x.maximumForce = 0;

        x.positionDamper = 100;
        x.positionSpring = 2000;
        x.maximumForce = 2000;

        thighJoints[1].slerpDrive = x;

        x = legJoints[0].slerpDrive;
        x.positionDamper = 0;
        x.positionSpring = 0;
        x.maximumForce = 0;

        x.positionDamper = 100;
        x.positionSpring = 2000;
        x.maximumForce = 2000;

        legJoints[0].slerpDrive = x;

        x = legJoints[1].slerpDrive;
        x.positionDamper = 0;
        x.positionSpring = 0;
        x.maximumForce = 0;

        x.positionDamper = 100;
        x.positionSpring = 2000;
        x.maximumForce = 2000;

        legJoints[1].slerpDrive = x;

        //thighJoints[0].targetRotation = new Quaternion(1.45f, 0, 0, thighJoints[0].targetRotation.w);
        //legJoints[0].targetRotation = new Quaternion(-3f, 0, 0, legJoints[0].targetRotation.w);


        //thighJoints[1].targetRotation = new Quaternion(1.45f, 0, 0, thighJoints[0].targetRotation.w);
        //legJoints[1].targetRotation = new Quaternion(-3f, 0, 0, legJoints[0].targetRotation.w);

        thighJoints[0].GetComponent<ConfigurableJoint>().targetRotation = new Quaternion(0, 0, 0, thighJoints[0].GetComponent<ConfigurableJoint>().targetRotation.w);
        legJoints[0].GetComponent<ConfigurableJoint>().targetRotation = new Quaternion(0, 0, 0, legJoints[0].GetComponent<ConfigurableJoint>().targetRotation.w);


        thighJoints[1].GetComponent<ConfigurableJoint>().targetRotation = new Quaternion(0, 0, 0, thighJoints[1].GetComponent<ConfigurableJoint>().targetRotation.w);
        legJoints[1].GetComponent<ConfigurableJoint>().targetRotation = new Quaternion(0, 0, 0, legJoints[1].GetComponent<ConfigurableJoint>().targetRotation.w);

        if(spin)
        {
            x = hipJoint.slerpDrive;
            x.positionDamper = 0;
            x.positionSpring = 0;
            x.maximumForce = 0;

            x.positionDamper = 50;
            x.positionSpring = 400;
            x.maximumForce = 400;
            hipJoint.slerpDrive = x;
            movement.enableChestRotation = false;
        }
        



        //x = armsJoint[0].slerpDrive;
        //x.positionDamper = 5000;
        //x.positionSpring = 100000f;
        //x.maximumForce = 100000f;
        //armsJoint[0].slerpDrive = x;

        
        ////Use the following to make the hands face the camera direction
        ////new Quaternion(-0.81f, testVector2.x, testVector2.x, 1);

        //armsJoint[0].targetRotation = new Quaternion(-0.81f, 2, 2, 1);//new Quaternion(-0.81f, currentY + handReachOffset, currentY + handReachOffset, 1);//Quaternion.Euler(inputDirection);//new Quaternion(-0.8f, testVector2.x , 0, 1);

        //JointDrive x1 = forearmsJoint[0].slerpDrive;
        //x1.positionDamper = 5000;
        //x1.positionSpring = 100000f;
        //x1.maximumForce = 100000f;
        //forearmsJoint[0].slerpDrive = x1;

        
        //forearmsJoint[0].targetRotation = new Quaternion(-0.3f, 0, 0, 1);




        //x = armsJoint[1].slerpDrive;
        //x.positionDamper = 5000;
        //x.positionSpring = 100000f;
        //x.maximumForce = 100000f;
        //armsJoint[1].slerpDrive = x;

        
        ////currentYtestVector2.x
        //armsJoint[1].targetRotation = new Quaternion(0.8f, -(2), 2, 1);//new Quaternion(0.8f, -(currentY+ handReachOffset), currentY + handReachOffset, 1); //new Quaternion(0.8f, -testVector2.x , 0, 1);

        //x1 = forearmsJoint[1].slerpDrive;
        //x1.positionDamper = 5000;
        //x1.positionSpring = 100000f;
        //x1.maximumForce = 100000f;
        //forearmsJoint[1].slerpDrive = x1;

        

        //forearmsJoint[1].targetRotation = new Quaternion(0.3f, 0, 0, 1);
        // **** LAUNCH INTO AIR HERE :
        //
        chestBody.AddForce(Vector3.up * jumpForce + movement.inputDirection * jumpForwardForce, ForceMode.Impulse);
        //chestBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        //
        // **** NEXT: DISABLE ALL THE OTHER CONTROLLING COMPONENTS AND ESSENTIALLY BECOME A RAGDOLL ****
        //
        //maintainHeight.enabled = false;
        jumpCounter = 0;
        jumpAnticipation = false;
        inAir = true;
        legs.enabled = false;
        //chestUpright.enabled = false;
        //faceDirection.enabled = false;
        //
        // ****  SOMETIMES THE FACEPLANT IS GOING TO HAVE MORE FORCE ON IT, BECAUSE RANDOM STRENGTH FACEPLANTS ARE COOL ***
        //
        facePlantM = 0.9f + Random.value * 0.4f;

       // movement.enabled = false;
    }

    private void GetUpFromJump()
    {
        // ***********************  STAND UP AFTER BEING A RAGDOLL *******
        //
        JointDrive x = thighJoints[0].slerpDrive;
        x.positionDamper = 100;
        x.positionSpring = 2000;
        x.maximumForce = 2000;
        thighJoints[0].slerpDrive = x;

        x = thighJoints[1].slerpDrive;
        x.positionDamper = 100;
        x.positionSpring = 2000;
        x.maximumForce = 2000;
        thighJoints[1].slerpDrive = x;

        x = legJoints[0].slerpDrive;
        x.positionDamper = 100;
        x.positionSpring = 2000;
        x.maximumForce = 2000;
        legJoints[0].slerpDrive = x;

        x = legJoints[1].slerpDrive;
        x.positionDamper = 100;
        x.positionSpring = 2000;
        x.maximumForce = 2000;
        legJoints[1].slerpDrive = x;


        x = hipJoint.slerpDrive;
        x.positionDamper = 5000;
        x.positionSpring = 50000;
        x.maximumForce = Mathf.Infinity;
        hipJoint.slerpDrive = x;

        movement.enableChestRotation = true;



        //x = armsJoint[0].slerpDrive;
        //x.positionDamper = 0;
        //x.positionSpring = 0;
        //x.maximumForce = 0;
        //armsJoint[0].slerpDrive = x;


        ////Use the following to make the hands face the camera direction
        ////new Quaternion(-0.81f, testVector2.x, testVector2.x, 1);

        //armsJoint[0].targetRotation = new Quaternion(-0.81f, 0.8f, 0.8f, 1);//new Quaternion(-0.81f, currentY + handReachOffset, currentY + handReachOffset, 1);//Quaternion.Euler(inputDirection);//new Quaternion(-0.8f, testVector2.x , 0, 1);

        //JointDrive x1 = forearmsJoint[0].slerpDrive;
        //x1.positionDamper = 0;
        //x1.positionSpring = 0;
        //x1.maximumForce = 0;
        //forearmsJoint[0].slerpDrive = x1;


        //forearmsJoint[0].targetRotation = new Quaternion(-0.3f, 0, 0, 1);




        //x = armsJoint[1].slerpDrive;
        //x.positionDamper = 0;
        //x.positionSpring = 0;
        //x.maximumForce = 0;
        //armsJoint[1].slerpDrive = x;


        ////currentYtestVector2.x
        //armsJoint[1].targetRotation = new Quaternion(0.8f, -(0.8f), 0.8f, 1);//new Quaternion(0.8f, -(currentY+ handReachOffset), currentY + handReachOffset, 1); //new Quaternion(0.8f, -testVector2.x , 0, 1);

        //x1 = forearmsJoint[1].slerpDrive;
        //x1.positionDamper = 0;
        //x1.positionSpring = 0;
        //x1.maximumForce = 0;
        //forearmsJoint[1].slerpDrive = x1;



        //forearmsJoint[1].targetRotation = new Quaternion(0.3f, 0, 0, 1);


        //
        getUpCounter = 3; // ** JUST USED TO SETTLE THE ARMS ***
        //
        //
        // **** NEXT: REACTIVATE ALL THE OTHER COMPONENTS THAT MOVE THE LIMBS AND TORSO ****
        //
        inAir = false;
        //maintainHeight.enabled = true;
       // maintainHeight.desiredHeight = maintainHeightStanding;
        //faceDirection.enabled = true;
        legs.enabled = true;
        //chestUpright.enabled = true;
        //
        // *** DO A SMALL HOP UPWARD TO START GETTING UP ***
        //
        chestBody.AddForceAtPosition((chestBody.transform.forward * -1 + Vector3.up) * 20, chestBody.transform.TransformPoint(Vector3.up * 0.2f), ForceMode.Impulse);

        canJump = true;
        inAir = false;

        movement.enabled = true;
    }
}
