using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public Vector3 inputDirection;
    public Vector3 rightInputDirection;
    protected Vector3 currentFacing = Vector3.zero;
    protected Vector3 currentFacing2 = Vector3.zero;
    public CharacterFaceDirection[] faceDirections;
    public Walking legs;
    public Rigidbody chestBody;
    // Start is called before the first frame update
    public float additionalForce = 100;

    public FacingDirection facingDirection = FacingDirection.Input;

    public ConfigurableJoint chestJoint;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        inputDirection = Vector3.zero;
        inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 inputFaceDirection = Vector3.zero;
        if (inputDirection != Vector3.zero)
        {

            inputDirection = Camera.main.transform.TransformDirection(inputDirection);
            inputDirection.y = 0.0f;

            // *** MOVE BASED ON INPUT DIRECTION ****
            //
            inputDirection.Normalize();
            //               
            //currentFacing = chestBody.transform.forward;
            //currentFacing.y = 0;
            //currentFacing.Normalize();
            //

            if (legs)
            {
                if (!legs.walking)
                {
                    legs.StartWalking();
                }
                legs.inputDirection = inputDirection;
            }

            //

            inputFaceDirection = inputDirection;

            //
        }
        else
        {
            // *** STAND STILL WHEN ZERO INPUT ****
            //
            inputFaceDirection = currentFacing;
            //
            if (legs)
            {
                if (legs.walking)
                {
                    legs.StopWalking();
                }
            }
                
        }



        rightInputDirection = Vector3.zero;
        rightInputDirection = new Vector3(Input.GetAxisRaw("Mouse X"), 0, Input.GetAxisRaw("Mouse Y"));
        Vector3 rightInputFaceDirection = Vector3.zero;
        if (rightInputDirection != Vector3.zero)
        {
            rightInputDirection = Camera.main.transform.TransformDirection(rightInputDirection);
            rightInputDirection.y = 0.0f;

            // *** MOVE BASED ON INPUT DIRECTION ****
            //
            rightInputDirection.Normalize();
            //               
            currentFacing2 = chestBody.transform.forward;
            currentFacing2.y = 0;
            currentFacing2.Normalize();

            rightInputFaceDirection = rightInputDirection;
            
        }
        else
        {
            rightInputFaceDirection = currentFacing2;
            
        }


        var camFaceDirection = Camera.main.transform.forward;// + new Vector3(0, Camera.main.transform.forward.y * 2.5f, 0);
        //camFaceDirection.y = 0;
        //camFaceDirection.Normalize();

        switch (facingDirection)
        {
            case FacingDirection.Input:
                LookAtTarget(inputFaceDirection);
                break;
            case FacingDirection.CameraForward:
                LookAtTarget(camFaceDirection);
                break;
            case FacingDirection.RightStick:
                LookAtTarget(rightInputFaceDirection);
                break;
            default:
                break;
        }
    }

    public void LookAtTarget(Vector3 dirToLookTarget)
    {
        foreach (var faceDirection in faceDirections)
        {
            faceDirection.facingDirection = dirToLookTarget;

        }

        var testVector2 = ConvertMoveInputAndPassItToAnimator(dirToLookTarget);
        if (testVector2 != Vector3.zero)
        {
            print("testVector2 : " + testVector2);
            JointDrive x = chestJoint.slerpDrive;
            x.positionDamper = 100;
            x.positionSpring = 1000;
            x.maximumForce = 1000;
            chestJoint.slerpDrive = x;

            Vector3 jointValue = testVector2 * 50;

            //chestJoint.targetAngularVelocity = jointValue;


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



            print("testVector2 : " + testVector2);
            JointDrive x = chestJoint.slerpDrive;
            x.positionDamper = 0;
            x.positionSpring = 1000;
            x.maximumForce = 1000;
            chestJoint.slerpDrive = x;

            //Vector3 jointValue = testVector2 * dogdgeSpeed;

           // chestJoint.targetAngularVelocity = new Vector3(0f, 0f, 0f);

            chestJoint.targetRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));




            //hipFaceDirection.bodyForward.x = 0;


        }
    }

    public Vector3 ConvertMoveInputAndPassItToAnimator(Vector3 moveInput)
    {
        //Convert the move input from world positions to local positions so that they have the correct values
        //depending on where we look
        Vector3 localMove = chestBody.transform.InverseTransformDirection(moveInput);
        //localMove.Normalize();
        float turnUpDownAmount = localMove.x;
        float turnLeftRightdAmount = localMove.y;

        /* if (turnAmount != 0)
             turnAmount *= 2;

         */
        if (false)
        {
            print("Forward : " + turnUpDownAmount);
            print("Sideways : " + turnLeftRightdAmount);
           // print("testVector : " + testVector);
            print("inputdirection : " + inputDirection);
           // print("hipFacing : " + hipFacing.facingDirection);
        }
        //x = -turnAmount
        return new Vector3(-turnUpDownAmount, 0f, 0f);

        //return new Vector3(-turnUpDownAmount, 0f, forwardAmount);
    }


    private void FixedUpdate()
    {


        ApplyStandingAndWalkingDrag();


        if (inputDirection != Vector3.zero)
        {
            //// *********************  MOVE CHEST IN THE INPUT DIRECTION *******
            ////
            //// *** (THIS IS ZERO IN THE PROJECT BY DEFAULT, I PREFER HAVING THE LEGS PULL THE BODY FORWARD ***
            ////
            chestBody.AddForce(additionalForce * inputDirection * Time.deltaTime, ForceMode.Impulse);
            ////                   
            ////                    
        }

        

    }

    private void ApplyStandingAndWalkingDrag()
    {
        // ***********  APPLY DRAGS! **
        //
        // THIS, along with the powerful facing direction forces, ACTUALLY MAKES THE CHARACTERS LESS INTERACTIBLE, BECAUSE THEY CAN'T PUSH EACH OTHER MUCH *****
        // SOFTER FORCES CAN BE BETTER, BUT THOSE NEED MORE TWEEKING, IDEALLY JUST ENOUGH FORCE TO ACHIEVE THE EFFECT WITHOUT BECOMING LOCKED INTO THAT POSITION OR DIRECTION ***
        //
        if (inputDirection == Vector3.zero)
        {
            // ***** WHEN STANDING STILL, APPLY A DRAG BASED ON HOW FAST THE TORSO IS TRAVELLING ***
            //
            Vector3 horizontalVelocity = chestBody.velocity;
            horizontalVelocity.y = 0;
            //
            float speed = horizontalVelocity.magnitude;
            //
            chestBody.velocity *= (1 - Mathf.Clamp(speed * 20f + 10, 0, 50) * Time.fixedDeltaTime);
        }
        else
        {
            // ***** APPLY A POWERFUL DRAG FORCE IF THE TORSO ISN'T TRAVELLING IN THE INPUT DIRECITON, ALLOWS FOR TIGHT TURNS ***
            //
            Vector3 horizontalVelocity = chestBody.velocity;
            horizontalVelocity.y = 0;
            //
            float m = 1 - (1 + Vector3.Dot(horizontalVelocity.normalized, inputDirection)) / 2f;
            chestBody.velocity *= (1 - (m * 100) * Time.fixedDeltaTime);
            //print("lv : " + horizontalVelocity.normalized + " |  hv : " + inputDirection + " | dot product : " + Vector3.Dot(horizontalVelocity.normalized, inputDirection) + " | m : " + m + " Final : " + (1 - (m * 100) * Time.fixedDeltaTime));



        }
        //
    }


    //private void ApplyStandingAndWalkingDrag()
    //{
    //    // ***********  APPLY DRAGS! **
    //    //
    //    // THIS, along with the powerful facing direction forces, ACTUALLY MAKES THE CHARACTERS LESS INTERACTIBLE, BECAUSE THEY CAN'T PUSH EACH OTHER MUCH *****
    //    // SOFTER FORCES CAN BE BETTER, BUT THOSE NEED MORE TWEEKING, IDEALLY JUST ENOUGH FORCE TO ACHIEVE THE EFFECT WITHOUT BECOMING LOCKED INTO THAT POSITION OR DIRECTION ***
    //    //
    //    if (inputDirection == Vector3.zero)
    //    {
    //        // ***** WHEN STANDING STILL, APPLY A DRAG BASED ON HOW FAST THE TORSO IS TRAVELLING ***
    //        //
    //        Vector3 horizontalVelocity = chestBody.velocity;
    //        horizontalVelocity.y = 0;
    //        //
    //        float speed = horizontalVelocity.magnitude;
    //        //
    //        chestBody.velocity *= (1 - (Mathf.Clamp(speed * dragMultiplier + dragAdded, 0, 50)) * reduceDragValue * Time.fixedDeltaTime);
    //    }
    //    else
    //    {
    //        // ***** APPLY A POWERFUL DRAG FORCE IF THE TORSO ISN'T TRAVELLING IN THE INPUT DIRECITON, ALLOWS FOR TIGHT TURNS ***
    //        //
    //        Vector3 horizontalVelocity = chestBody.velocity;
    //        horizontalVelocity.y = 0;
    //        //
    //        // print(horizontalVelocity);
    //        float m = 1 - (1 + Vector3.Dot(horizontalVelocity.normalized, inputDirection)) / 2f;
    //        chestBody.velocity *= (1 - (m * 100) * Time.fixedDeltaTime);
    //    }
    //    //
    //}
}
