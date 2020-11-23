using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewClimb : MonoBehaviour
{
    public Rigidbody[] armsBody = new Rigidbody[2];
    public ConfigurableJoint[] armsJoint = new ConfigurableJoint[2];

    public Rigidbody[] farmsBody = new Rigidbody[2];
    public ConfigurableJoint[] farmsJoint = new ConfigurableJoint[2];

    private float currentX = 0.0f;
    private float currentY = 0.0f;
    public float speed = 0.1f;
    public float handReachOffset = 0.5f;

    public Transform hipRotateTarget;
    // Start is called before the first frame update
    void Start()
    {
        armsBody[0].solverIterations = 255;
        armsBody[1].solverIterations = 255;
        farmsBody[0].solverIterations = 255;
        farmsBody[1].solverIterations = 255;
    }

    // Update is called once per frame
    void Update()
    {
        currentX = currentX + Input.GetAxis("Mouse X") * speed;
        currentY = currentY + Input.GetAxis("Mouse Y") * speed;

        currentY = Mathf.Clamp(currentY, -1, 1);
    }

    public Vector3 ConvertMoveInputAndPassItToAnimator(Vector3 moveInput)
    {
        //Convert the move input from world positions to local positions so that they have the correct values
        //depending on where we look
        Vector3 localMove = hipRotateTarget.InverseTransformDirection(moveInput);
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
            print("moveInput : " + moveInput);
            // print("hipFacing : " + hipFacing.facingDirection);
        }
        //x = -turnAmount

        //Use following with hipBody
        //return new Vector3(-turnUpDownAmount, 0f, 0f);

        //Use this with hipRotateTarget
        return new Vector3(turnLeftRightdAmount, 0f, 0f);

        //return new Vector3(-turnUpDownAmount, 0f, forwardAmount);
    }

    private void FixedUpdate()
    {
        //var tempInputDirection = Vector3.zero;
        //tempInputDirection = new Vector3(Input.GetAxisRaw("Mouse X"), 0, Input.GetAxisRaw("Mouse Y"));


        //var camFaceDirection = Camera.main.transform.forward;//+ new Vector3(0, Camera.main.transform.forward.y * 5f, 0);
        //                                                     //var camFaceDirection1 = Camera.main.transform.forward;
        //camFaceDirection.Normalize();
        ////if (camFaceDirection.y > 0)
        ////{
        ////    camFaceDirection.y += camFaceDirection.y * 2;
        ////}
        //camFaceDirection.y += handReachOffset;

        //var testVector2 = ConvertMoveInputAndPassItToAnimator(camFaceDirection);
        //////var testVector3 = ConvertMoveInputAndPassItToAnimator(camFaceDirection1);





        //Use the following to make the hands face the camera direction
        //new Quaternion(-0.81f, testVector2.x, testVector2.x, 1);

        var camFaceDirection = Camera.main.transform.forward;
        camFaceDirection.Normalize();
        camFaceDirection.y += handReachOffset;
        var testVector2 = ConvertMoveInputAndPassItToAnimator(camFaceDirection);


        JointDrive x = armsJoint[0].slerpDrive;
        x.positionDamper = 5000;
        x.positionSpring = 100000f;
        x.maximumForce = 100000f;
        armsJoint[0].slerpDrive = x;

        armsJoint[0].targetRotation = new Quaternion(-0, currentY + handReachOffset, 0, 1); //new Quaternion(-0, testVector2.x, 0, 1); //new Quaternion(-0, currentY + handReachOffset, 0, 1);//Quaternion.Euler(inputDirection);//new Quaternion(-0.8f, testVector2.x , 0, 1);


         x = armsJoint[1].slerpDrive;
        x.positionDamper = 5000;
        x.positionSpring = 100000f;
        x.maximumForce = 100000f;
        armsJoint[1].slerpDrive = x;

        armsJoint[1].targetRotation = new Quaternion(-0, -(currentY + handReachOffset), 0, 1);// new Quaternion(-0, -testVector2.x, 0, 1); //new Quaternion(-0, -(currentY + handReachOffset), 0, 1);//Quaternion.Euler(inputDirection);//new Quaternion(-0.8f, testVector2.x , 0, 1);


    }
}
