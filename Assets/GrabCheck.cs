using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabCheck : MonoBehaviour {
    public bool hasJoint;
    public bool grabNow;
    public Collider targetCollider;
    public Collider mycollision;
    //public ForceTests myForceTest;
   // public ConfigurableJoint childConfigJoint;
    //public GameObject childTransform;
    public bool grabSuccess =  false;
    public ConfigurableJoint test;
    public FixedJoint test2;
    // Use this for initialization
    void Start () {

        //myForceTest = transform.root.GetComponent<ForceTests>();
        //childTransform = transform.GetChild(0).gameObject;

        //if(childTransform)
        //    childConfigJoint = childTransform.GetComponent<ConfigurableJoint>();
        

    }
	
	// Update is called once per frame
	void Update () {

        /*if(myForceTest.target)
        {
            targetCollider = myForceTest.target;
        }*/
        

        if(!grabNow)
        {
            Destroy(test);
            hasJoint = false;
            mycollision = null;
        }
        

    }

    private void OnJointBreak(float breakForce)
    {
        grabSuccess = false;
        hasJoint = false;
        mycollision = null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //mycollision = collision.collider;
        //if (collision.transform.root != transform.root && collision.gameObject.GetComponent<Rigidbody>() != null && !hasJoint && grabNow && targetCollider == collision.collider)

        if (collision.transform.root != transform.root  && !hasJoint && grabNow)
        {
            grabSuccess = true;

            test = gameObject.AddComponent<ConfigurableJoint>(); ;
            // childConfigJoint.connectedBody = collision.rigidbody;

            test.connectedBody = collision.rigidbody;
            test.anchor = new Vector3(0, -0.2f, 0);

            //test.connectedBody = collision.rigidbody;
            //this.actor.controlHandeler.leftCanClimb = true;
            test.xMotion = ConfigurableJointMotion.Locked;
            test.yMotion = ConfigurableJointMotion.Locked;
            test.zMotion = ConfigurableJointMotion.Locked;

            test.angularXMotion = ConfigurableJointMotion.Free;
            test.angularYMotion = ConfigurableJointMotion.Free;
            test.angularZMotion = ConfigurableJointMotion.Free;

            //test.xMotion = ConfigurableJointMotion.Limited;
            //test.yMotion = ConfigurableJointMotion.Limited;
            //test.zMotion = ConfigurableJointMotion.Limited;
            //SoftJointLimit testLinearLimit = new SoftJointLimit();
            //testLinearLimit.limit = 0.001f;
            //testLinearLimit.bounciness = 1f;
            //test.linearLimit = testLinearLimit;

            //SoftJointLimitSpring testLinearLimitSpring = new SoftJointLimitSpring();
            //testLinearLimitSpring.spring = 1000;
            //testLinearLimitSpring.damper = 1;
            //test.linearLimitSpring = testLinearLimitSpring;

            test.enablePreprocessing = false;
            test.projectionDistance = 0.1f;
            test.projectionAngle = 180f;
            test.projectionMode = JointProjectionMode.PositionAndRotation;
            test.enableCollision = false;

            test.breakForce = 2000000f;
            test.breakTorque = 2000000f;

            //test2 = gameObject.AddComponent<FixedJoint>();

            //test2.connectedBody = collision.rigidbody;
            //test2.anchor = new Vector3(0, -0.2f, 0);

            //test2.enablePreprocessing = false;
            
            //test2.enableCollision = false;

            //test2.breakForce = 2000000f;
            //test2.breakTorque = 2000000f;

            /*this.actor.bodyHandeler.leftGrabRigidbody = collisionRigidbody;
            this.actor.bodyHandeler.leftGrabInteractable = collisionInteractable;*/

            mycollision = collision.collider;

            hasJoint = true;
        }
        else
        {
           
            grabSuccess = false;
        }
    }
}
