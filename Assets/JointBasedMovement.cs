using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FacingDirection
{
    Input,
    CameraForward,
    RightStick
}
public class JointBasedMovement : MonoBehaviour
{

    public Vector3 inputDirection;
    Vector3 rightInputDirection;
    public JointBasedWalking legs;

    public Rigidbody chestBody;
    public float additionalForce = 100;
    // Start is called before the first frame update

    public ConfigurableJoint rootJoint;

    protected Vector3 currentFacing = Vector3.zero;
    protected Vector3 currentFacing2 = Vector3.zero;
    private Quaternion startingRotation;

    Quaternion localToJointSpace;
    Quaternion startLocalRotation;

    public Transform rotationTarget;
    public Transform positionTarget;

    public FacingDirection facingDirection = FacingDirection.Input;

    public ConfigurableJoint chestJoint;

    public bool enableChestRotation = true;
    void Start()
    {

        Vector3 forward = Vector3.Cross(rootJoint.axis, rootJoint.secondaryAxis);
        Vector3 up = rootJoint.secondaryAxis;


        localToJointSpace = Quaternion.LookRotation(forward, up);
        startLocalRotation = chestBody.transform.localRotation * localToJointSpace;

        currentFacing = chestBody.transform.forward;
        startingRotation =  chestBody.transform.localRotation;// * Quaternion.Euler(new Vector3(0, 0, 90)); // * Quaternion.Euler(new Vector3(0, 0, 90))

        //positionTarget.rotation = chestBody.rotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       rotationTarget.position = chestBody.position;
        //positionTarget.position = chestBody.position;
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
            currentFacing = chestBody.transform.forward;
            currentFacing.y = 0;
            currentFacing.Normalize();
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
            //LookAtTarget(rightInputDirection);

            rightInputFaceDirection = rightInputDirection;

        }
        else
        {
            //LookAtTarget(currentFacing);
            rightInputFaceDirection = currentFacing2;
        }

        var camFaceDirection = Camera.main.transform.forward;
        //camFaceDirection.y = 0;
        //camFaceDirection.Normalize();

        if (enableChestRotation)
        {
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

    float speed = 0.1f;
    public void LookAtTarget(Vector3 dirToLookTarget)
    {
        //Look towards target
        //Vector3 dirToLookTarget = (agent.nextPosition - APR_Parts[0].transform.position).normalized;

        //var tempVelocity = (agent.nextPosition - APR_Parts[0].transform.position).magnitude;

        //MoveSpeed = startMoveSpeed + tempVelocity;

        var chestVector = dirToLookTarget;
        if (enableChestRotation)
        {
            dirToLookTarget.y = 0;
        }
        else
        {

        }
            
        dirToLookTarget.Normalize();
        float targetAngle = 90 - Mathf.Atan2(dirToLookTarget.z, dirToLookTarget.x) * Mathf.Rad2Deg;

        Debug.DrawRay(chestBody.transform.position, dirToLookTarget * 3, Color.green);

        var rotation = Quaternion.LookRotation(dirToLookTarget);//* Quaternion.Euler(new Vector3(0, 90, 0));

        rotationTarget.rotation = rotation;

        

        var localRot = rotation * Quaternion.Inverse(chestBody.transform.parent.rotation);

        var locRot2 = rotationTarget.localRotation;
        //target.transform.eulerAngles = Vector3.up * targetAngle;

        //Debug.Log(targetAngle);


        //dir = Head.transform.position - Body.transform.position;
        // Quaternion XLookRotation1 = Quaternion.LookRotation(dir, refBody.transform.up) * Quaternion.Euler(new Vector3(0, 90, 0));


        //var rotation = Quaternion.LookRotation(dirToLookTarget, -chestBody.transform.right );
        //rootJoint.targetRotation = Quaternion.Slerp(rootJoint.targetRotation, Quaternion.Inverse(rotation), Time.time * speed);

        //rootJoint.targetRotation = Quaternion.Slerp(rootJoint.targetRotation, Quaternion.Inverse(Quaternion.Euler(Vector3.left * targetAngle) * startingRotation), Time.time * speed);
        //APR_Parts[0].GetComponent<ConfigurableJoint>().targetRotation = Quaternion.Inverse(Quaternion.Euler(Vector3.up * targetAngle) * startingRotation);

        //rootJoint.SetTargetRotationLocal(Quaternion.Euler(0, 90, 0), startingRotation);




        rootJoint.targetRotation = Quaternion.Inverse(localToJointSpace) * Quaternion.Inverse(locRot2 * Quaternion.Euler(new Vector3(0, 0, -90))) * startLocalRotation;
        //if (chestVector.y < -0.5f)
        //{
        //    chestVector.y += chestVector.y * 2;
        //}

        //chestVector.y += 0.5f;
        var testVector2 = ConvertMoveInputAndPassItToAnimator(chestVector);

        if(enableChestRotation)
        {
            if (testVector2 != Vector3.zero)
            {
                //print("testVector2 : " + testVector2);
                JointDrive x = chestJoint.slerpDrive;
                x.positionDamper = 0;
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



               // print("testVector2 : " + testVector2);
                JointDrive x = chestJoint.slerpDrive;
                x.positionDamper = 0;
                x.positionSpring = 100000;
                x.maximumForce = 100000;
                chestJoint.slerpDrive = x;


                chestJoint.targetRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));




                //hipFaceDirection.bodyForward.x = 0;


            }
        }
        

    }


    public Vector3 ConvertMoveInputAndPassItToAnimator(Vector3 moveInput)
    {
        //Convert the move input from world positions to local positions so that they have the correct values
        //depending on where we look
        Vector3 localMove = rotationTarget.InverseTransformDirection(moveInput);
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

        //Use this following with chestBody
        //return new Vector3(-turnUpDownAmount, 0f, 0f);


        //Use this following with rotationTarget
        return new Vector3(turnLeftRightdAmount, 0f, 0f);

        //return new Vector3(-turnUpDownAmount, 0f, forwardAmount);
    }

    //private void FixedUpdate()
    //{


    //    //ApplyStandingAndWalkingDrag();


       

    //}

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


}
