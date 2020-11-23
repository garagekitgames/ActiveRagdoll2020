using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyForceController : MonoBehaviour
{
    [Header("Height Settings")]
    new protected Rigidbody rigidbody;
    public bool maintainHeight;
    public float desiredHeight = 1;
    public float initialDesiredHeight = 1;
    public float pullUpForce = 10;
    public float leadTime = 0.3f; // *** THIS IS USED TO SLOW DOWN WHEN APPROACHING THE DESIRED HEIGHT, INSTEAD OF OVERSHOOTING BACK AND FORTH **
    public Transform inRelationTo = null;
    //
    public float groundHeight = 0;
    public float nextActionTime = 0.0f;
    public float period = 1f;

    [Header("Upright Settings")]
    public bool keepUpright = true;
    public float uprightForce = 10;
    public float uprightOffset = 1.45f;
    public float additionalUpwardForce = 10;
    public float dampenAngularForce = 0;
    public Vector3 uprightDirection = new Vector3(0, 1, 0);


    [Header("Face Direction Settings")]
    public bool faceDirection;
    public float fDLeadTime = 0.3f;
    public Vector3 bodyForward = new Vector3(0, 0, 2);
    public Vector3 facingDirection = Vector3.zero;
    public float facingForce = 800;
    public Transform target;
    //
    //public float leadTime = 0; // *** THIS IS USED TO SLOW DOWN WHEN APPROACHING THE DESIRED DIRECTION, INSTEAD OF OVERSHOOTING BACK AND FORTH **
    //
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        initialDesiredHeight = desiredHeight;

        //rigidbody.maxAngularVelocity = 30; // **** CANNOT APPLY HIGH ANGULAR FORCES UNLESS THE MAXANGULAR VELOCITY IS INCREASED ****
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(maintainHeight)
        {
            MaintainHeight();
        }

        if (keepUpright)
        {
            KeepUpright();
        }

        if (faceDirection)
        {
            FaceDirection();
        }


        if (dampenAngularForce > 0)
        {
            rigidbody.angularVelocity *= (1 - Time.deltaTime * dampenAngularForce);
        }
    }



    public void MaintainHeight()
    {
        // ***** TRY HOLD A OBJECT AT A SPECIFIC HEIGHT (optionally in relation to another object) ***
        //
        // if (Time.time > nextActionTime)
        // {
        RaycastHit groundHit;
        if (Physics.Raycast(new Ray(transform.position, Vector3.down), out groundHit, 100, 1 << LayerMask.NameToLayer("Ground"))) //if (Physics.Raycast(new Ray(transform.position, Vector3.down), out groundHit, 100, 1 << LayerMask.NameToLayer("Ground")))
        {
            groundHeight = groundHit.point.y;
        }
        //   nextActionTime += period;
        //}
        float diff = (groundHeight + desiredHeight) - (transform.position.y + rigidbody.velocity.y * leadTime);
        if (inRelationTo != null)
        {
            diff = inRelationTo.TransformPoint(Vector3.up * desiredHeight).y - (transform.position.y + rigidbody.velocity.y * leadTime);
        }
        float dist = Mathf.Abs(diff);
        float pullM = Mathf.Clamp01(dist / 0.3f);
        rigidbody.AddForce(new Vector3(0, Mathf.Sign(diff) * pullUpForce * pullM * Time.deltaTime, 0), ForceMode.Impulse);
    }


    public void KeepUpright()
    {
        // ***** USE TWO FORCES PULLING UP AND DOWN AT THE TOP AND BOTTOM OF THE OBJECT RESPECTIVELY TO PULL IT UPRIGHT ***
        //
        //  *** THIS TECHNIQUE CAN BE USED FOR PULLING AN OBJECT TO FACE ANY VECTOR ***
        //
        rigidbody.AddForceAtPosition(new Vector3(0, (uprightForce + additionalUpwardForce), 0),
            transform.position + transform.TransformPoint(uprightDirection), ForceMode.Force);
        //
        rigidbody.AddForceAtPosition(new Vector3(0, -uprightForce, 0),
            transform.position + transform.TransformPoint(-uprightDirection), ForceMode.Force);
        //  
    }


    public void FaceDirection()
    {




        if (target != null)
        {
            facingDirection = target.transform.position - transform.position;
            facingDirection.Normalize();
            //facingDirection = target.transform.position;
        }

        if (fDLeadTime == 0)
        {
            //facingDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            //facingDirection.Normalize();
            // ****** JUST PULL WITH TWO STRINGS TO FACE DIRECTION *****
            //
            if (facingDirection != Vector3.zero)
            {
                //print($"Force Direction : {facingForce * facingDirection * Time.deltaTime} | Body Forward : {rigidbody.transform.TransformDirection(bodyForward)}");
                // *********************  FACE CHEST TOWARDS THE INPUT DIRECTION *******
                //
                //rigidbody.AddForceAtPosition(facingForce * facingDirection * Time.deltaTime, transform.position + transform.TransformPoint(bodyForward), ForceMode.Impulse);
                //rigidbody.AddForceAtPosition(-facingForce * facingDirection * Time.deltaTime, transform.position + transform.TransformPoint(-bodyForward), ForceMode.Impulse);

                rigidbody.AddForceAtPosition(facingForce * facingDirection * Time.deltaTime, rigidbody.transform.TransformDirection(bodyForward), ForceMode.Impulse);
                rigidbody.AddForceAtPosition(-facingForce * facingDirection * Time.deltaTime, rigidbody.transform.TransformDirection(-bodyForward), ForceMode.Impulse);

                //                   
                //                    
            }
            //print($"Force Direction : {facingForce * facingDirection * Time.deltaTime} | Body Forward : {transform.position + transform.TransformPoint(bodyForward)}");
        }
        else
        {
            // ******** TRY PULL TOWARDS DIRECTION FACTORING IN VELOCITY (ie. decelerate towards the target) ***********
            //
            Vector3 targetPoint = transform.position + facingDirection * bodyForward.magnitude;
            Vector3 currentPoint = transform.TransformPoint(bodyForward);
            Vector3 reversePoint = transform.TransformPoint(-bodyForward);
            Vector3 velocity = rigidbody.GetPointVelocity(currentPoint);
            //
            Vector3 diff = targetPoint - (currentPoint + velocity * leadTime);
            //
            //
            rigidbody.AddForceAtPosition(facingForce * diff * Time.deltaTime, currentPoint, ForceMode.Impulse);
            rigidbody.AddForceAtPosition(-facingForce * diff * Time.deltaTime, reversePoint, ForceMode.Impulse);
            //  
            //
        }
    }
}
