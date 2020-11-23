﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;

public class Dodge2 : MonoBehaviour {

    public Rigidbody chestBody;
    public ConfigurableJoint chestJoint;


    public Vector3 inputDirection;
    public Vector3 rightStickInputDirection;
    //public CharacterInput input;

    public Vector3 testVector;

    public Vector3 testVector2;

    public float dogdgeSpeed = 20f;

    public float dogdgeSpeedUR = 20f;

    public float dogdgeSpeedFD = 20f;

    public float fitnessSpring = 1000f;
    public float fitnessDamper = 100f;
    public float fitnessForce = 1000f;

    public bool sideCam = false;

    public CharacterFaceDirection hipFacing;

    [Range(0.3f, 1f)]
    public float fitnessReduceFactor = 0.5f;
    public bool log;


    public CharacterFaceDirection hipFaceDirection;
    public CharacterUpright hipUpRight;
    //public CharacterThinker character;
    // Use this for initialization
    void Start () {
       // input = GetComponent<CharacterInput>();
        //hipFacing = GetComponent<CharacterFaceDirection>();
        //character = GetComponent<CharacterThinker>();
    }
	
	// Update is called once per frame
	void Update () {


        inputDirection = Vector3.zero;
        //if (input.RHoldRight())
        //{
        //    inputDirection += Vector3.right;
        //}
        //if (input.RHoldLeft())
        //{
        //    inputDirection += Vector3.left;
        //}
        //if (input.RHoldUp())
        //{
        //    inputDirection += Vector3.forward;
        //}
        //if (input.RHoldDown())
        //{
        //    inputDirection += Vector3.back;
        //}

        //if (inputDirection != Vector3.zero)
        //{
        //    // *** MOVE BASED ON INPUT DIRECTION ****
        //    //
        //    inputDirection.Normalize();

        //    if (true)
        //    {
        //        inputDirection = Camera.main.transform.TransformDirection(inputDirection);
        //        inputDirection.y = 0.0f;
        //    }
        //    //               
        //    //Vector3.cl
        //    // handTarget.transform.localPosition = new Vector3(inputDirection.x, inputDirection.z, inputDirection.y);
        //    // print("Right Stick Value : " + inputDirection);

        //    //dodgeTarget.transform.localPosition = new Vector3(0, Input.GetAxisRaw("R_XAxis_" + (input.controllerID + 1)), -Input.GetAxisRaw("R_YAxis_" + (input.controllerID + 1)));
        //    //print("dodgeTarget Right Stick Value : " + new Vector3(Input.GetAxisRaw("R_XAxis_" + (input.controllerID + 1)) * 20f, 0, 20f *-Input.GetAxisRaw("R_YAxis_" + (input.controllerID + 1))));

        //    if(sideCam)
        //    {
        //        if (input.controllerID == 0)
        //        {
        //            testVector = -new Vector3(Input.GetAxisRaw("R_XAxis_" + (input.controllerID + 1)), -Input.GetAxisRaw("R_YAxis_" + (input.controllerID + 1)), 0);

        //        }
        //        else
        //        {
        //            testVector = new Vector3(Input.GetAxisRaw("R_XAxis_" + (input.controllerID + 1)), -Input.GetAxisRaw("R_YAxis_" + (input.controllerID + 1)), 0);

        //        }
        //    }
        //    else
        //    {
        //        testVector = new Vector3(Input.GetAxisRaw("R_YAxis_" + (input.controllerID + 1)), Input.GetAxisRaw("R_XAxis_" + (input.controllerID + 1)), 0);

        //    }
        //    //For 3D game, dodge respective to character, tilting analog stick to right will tilt the character to the character right
        //    //testVector = new Vector3(Input.GetAxisRaw("R_YAxis_" + (input.controllerID + 1)) , Input.GetAxisRaw("R_XAxis_" + (input.controllerID + 1)), 0 );


        //    //For fixed 2d like fighter CAM , dodge respective to player, tilting analog stick to right will tilt the character to the players right



        //    //
        //    /* if (!legs.walking)
        //     {
        //         legs.StartWalking();
        //     }*/
        //    //

        //    //
        //}
        //else
        //{

        //   // ResetChestJoint();
        //    testVector = Vector3.zero;
           


        //}

        rightStickInputDirection = new Vector3(Input.GetAxisRaw("Mouse X"), 0, Input.GetAxisRaw("Mouse Y"));
        if (rightStickInputDirection != Vector3.zero)
        {
            //Debug.Log("Before " + character.rightStickInputDirection);
            rightStickInputDirection.Normalize();
            if (true) // camera relative movement
            {
                rightStickInputDirection = Camera.main.transform.TransformDirection(rightStickInputDirection);
               // character.rightStickInputDirection.z = 0f;
               // character.rightStickInputDirection.y *= 2;
                float magnitude = rightStickInputDirection.sqrMagnitude;
            }
            Debug.Log("After " + rightStickInputDirection);
        }

        //ConvertMoveInputAndPassItToAnimator(new Vector3(Input.GetAxisRaw("R_XAxis_" + (1)), 0, -Input.GetAxisRaw("R_YAxis_" + (1))));
        //if (character.health.alive || !character.health.knockdown || !character.isHurting || !character.isGrabbing || !character.leftgrab || !character.rightgrab)
        //{
            ConvertMoveInputAndPassItToAnimator(rightStickInputDirection);
        //}
            

    }

    void ConvertMoveInputAndPassItToAnimator(Vector3 moveInput)
    {
        //Convert the move input from world positions to local positions so that they have the correct values
        //depending on where we look
        Vector3 localMove = chestBody.transform.InverseTransformDirection(moveInput);
        //localMove.Normalize();
        float turnAmount = localMove.y;
        float forwardAmount = localMove.z;

        /* if (turnAmount != 0)
             turnAmount *= 2;

         */
         if(log)
        {
            print("Forward : " + forwardAmount);
            print("Sideways : " + turnAmount);
            print("testVector : " + testVector);
            print("inputdirection : " + inputDirection);
            print("hipFacing : " + hipFacing.facingDirection);
        }

        testVector2 = new Vector3(-forwardAmount, turnAmount, 0f);
    }

    public void ResetChestJoint()
    {
        JointDrive x = chestJoint.slerpDrive;
        x.positionDamper = 100f;
        x.positionSpring = 1000f;
        x.maximumForce = 1000f;
        chestJoint.slerpDrive = x;

        Vector3 jointValue = new Vector3(0f, 0f, 0f);

        chestJoint.targetAngularVelocity = jointValue;
    }

    private void FixedUpdate()
    {
        if (testVector2 != Vector3.zero)
        {
            print("testVector2 : " + testVector2);
            JointDrive x = chestJoint.slerpDrive;
            x.positionDamper = fitnessDamper * fitnessReduceFactor;
            x.positionSpring = fitnessSpring * fitnessReduceFactor;
            x.maximumForce = fitnessForce * fitnessReduceFactor;
            chestJoint.slerpDrive = x;

            Vector3 jointValue = testVector2 * dogdgeSpeed;

            chestJoint.targetAngularVelocity = jointValue;


            //Vector3 jointValue2 = testVector2 * dogdgeSpeedFD;

            //hipFaceDirection.bodyForward.x = jointValue2.x;

            //Vector3 jointValue3 = testVector2 * dogdgeSpeedUR;


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
            x.positionSpring = fitnessSpring * fitnessReduceFactor;
            x.maximumForce = fitnessForce * fitnessReduceFactor;
            chestJoint.slerpDrive = x;

            //Vector3 jointValue = testVector2 * dogdgeSpeed;

            chestJoint.targetAngularVelocity = new Vector3(0f, 0f, 0f);


            

            //hipFaceDirection.bodyForward.x = 0;

           
        }


        //hipUpRight.upRightDirection.y = -jointValue3.y;

    }
}