using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointBasedWalking : MonoBehaviour
{
    public float legsCounter = 0;
    public int legIndex = 0;
    public int altlegIndex = 1;
    public Rigidbody[] feet;
    public Rigidbody[] legs;
    public Rigidbody[] thighs;
    public Rigidbody[] shins;
    public CharacterMaintainHeight[] legHeights;
    public Rigidbody chestBody;
    //
    public float legRate = 0.7f;
    public float legRateIncreaseByVelocity = 1f;
    //
    public float liftForce = 4;
    public float holdDownForce = 100;
    public float moveForwardForce = 30;
    public float inFrontVelocityM = 0.4f;
    public float chestBendDownForce = 60;
    //
    public bool walking = false;
    //
    public Vector3 inputDirection;

    public ConfigurableJoint chestJoint;


    public float StepDuration = 0.2f;
    public float StepHeight = 1.7f;
    public float FeetMountForce = 25f;
    public ConfigurableJoint[] thighJoints = new ConfigurableJoint[2];
    public ConfigurableJoint[] legJoints = new ConfigurableJoint[2];
    public float lerpspeed = 10f;

    public void StopWalking()
    {
        //legHeights[legIndex].enabled = false;
        //legHeights[altlegIndex].enabled = false;
        //
        walking = false;
        //
        legsCounter = legRate * 0.99f;
    }
    //
    public void StartWalking()
    {
        TakeStep();
        walking = true;
    }
    void Start()
    {
        StopWalking();
        foreach (Rigidbody r in legs) r.maxAngularVelocity = 40;
        foreach (Rigidbody r in feet) r.maxAngularVelocity = 40;
        foreach (Rigidbody r in thighs) r.maxAngularVelocity = 40;
        foreach (Rigidbody r in shins) r.maxAngularVelocity = 40;
    }


    void FixedUpdate()
    {
        

        //ConvertMoveInputAndPassItToAnimator(inputDirection);

        // *************  I FIND APPLYING FORCES IN FIXED UPDATE TO BE MORE RELIABLE THAN IN UPDATE ****
        //
        //
        if (walking)
        {
            // ********************** THIS USES COUNTERS AND VARIOUS FORCES ON DIFFERENT PARTS OF THE LEGS TO PERFORM A GOOSE STEP *********
            //
            Vector3 horizontalVelocity = inputDirection;
            


            horizontalVelocity.y = 0;
            //
            float speed = chestBody.velocity.magnitude;
            horizontalVelocity.Normalize();
            //
            //
            legsCounter += Time.deltaTime * (1 + speed * legRateIncreaseByVelocity); // *** THE GOOSESTEP IS FASTER THE FASTER THE BODY TRAVELS ***
            //
            if (legsCounter >= legRate)
            {

                //thighJoints[legIndex].GetComponent<ConfigurableJoint>().targetRotation = new Quaternion(1, thighJoints[legIndex].GetComponent<ConfigurableJoint>().targetRotation.y, thighJoints[legIndex].GetComponent<ConfigurableJoint>().targetRotation.z, thighJoints[legIndex].GetComponent<ConfigurableJoint>().targetRotation.w);
                //legJoints[legIndex].GetComponent<ConfigurableJoint>().targetRotation = new Quaternion(-1, legJoints[legIndex].GetComponent<ConfigurableJoint>().targetRotation.y, legJoints[legIndex].GetComponent<ConfigurableJoint>().targetRotation.z, legJoints[legIndex].GetComponent<ConfigurableJoint>().targetRotation.w);


                //thighJoints[altlegIndex].GetComponent<ConfigurableJoint>().targetRotation = new Quaternion(-1, thighJoints[altlegIndex].GetComponent<ConfigurableJoint>().targetRotation.y, thighJoints[altlegIndex].GetComponent<ConfigurableJoint>().targetRotation.z, thighJoints[altlegIndex].GetComponent<ConfigurableJoint>().targetRotation.w);
                //legJoints[altlegIndex].GetComponent<ConfigurableJoint>().targetRotation = new Quaternion(1, legJoints[altlegIndex].GetComponent<ConfigurableJoint>().targetRotation.y, legJoints[altlegIndex].GetComponent<ConfigurableJoint>().targetRotation.z, legJoints[altlegIndex].GetComponent<ConfigurableJoint>().targetRotation.w);
                
                
                
                //float thighValue = Mathf.Lerp(thighJoints[legIndex].targetRotation.x, 0f, Time.deltaTime * lerpspeed);// Mathf.Lerp(thighJoints[legIndex].targetRotation.x, 0.4f, legsCounter / legRate * 0.25f * 10);
                //float legValue = Mathf.Lerp(legJoints[legIndex].targetRotation.x, -0f, Time.deltaTime * lerpspeed);//legsCounter / legRate * 0.25f * 10);

                //thighJoints[legIndex].targetRotation = new Quaternion(thighValue, 0, 0, thighJoints[legIndex].targetRotation.w);
                //legJoints[legIndex].targetRotation = new Quaternion(legValue, 0, 0, legJoints[legIndex].targetRotation.w);

                

                //
                TakeStep();  // ** CHANGE LEG ***
                //
            }
            //
            if (legsCounter > legRate * 0.75f)
            {
                // ***********************  END OF STEP, PLACING FOOT BACK ON THE GROUND ********
                //
                //float smoothTime = 0.3f;
                //float yVelocity = 0.0f;
                //float thighValue = Mathf.SmoothDamp(thighJoints[legIndex].targetRotation.x, 0, ref yVelocity, legRate);
                //float legValue = Mathf.SmoothDamp(legJoints[legIndex].targetRotation.x, 0, ref yVelocity, legRate);
                float thighValue = Mathf.Lerp(thighJoints[legIndex].targetRotation.x, 0, Time.deltaTime * lerpspeed); //(legsCounter - legRate * 0.75f) / legRate * 0.25f * 10);
                float legValue = Mathf.Lerp(legJoints[legIndex].targetRotation.x, 0, Time.deltaTime * lerpspeed); //(legsCounter - legRate * 0.75f) / legRate * 0.25f * 10);
                thighJoints[legIndex].targetRotation = new Quaternion(thighValue, 0, 0, thighJoints[legIndex].targetRotation.w);
                legJoints[legIndex].targetRotation = new Quaternion(legValue, 0, 0, legJoints[legIndex].targetRotation.w);

                //float thighValue1 = Mathf.Lerp(thighJoints[altlegIndex].targetRotation.x, -0.5f, Time.deltaTime * lerpspeed); //(legsCounter - legRate * 0.75f) / legRate * 0.25f * 10);
                //float legValue1 = Mathf.Lerp(legJoints[altlegIndex].targetRotation.x, -0.5f, Time.deltaTime * lerpspeed); //(legsCounter - legRate * 0.75f) / legRate * 0.25f * 10);
                //thighJoints[altlegIndex].targetRotation = new Quaternion(thighValue1, 0, 0, thighJoints[altlegIndex].targetRotation.w);
                //legJoints[altlegIndex].targetRotation = new Quaternion(thighValue1, 0, 0, legJoints[altlegIndex].targetRotation.w);


                //thighJoints[legIndex].targetRotation = new Quaternion(0, 0, 0, thighJoints[legIndex].targetRotation.w);
                //legJoints[legIndex].targetRotation = new Quaternion(0, 0, 0, legJoints[legIndex].targetRotation.w);

                //thighJoints[legIndex].targetRotation = Quaternion.Slerp(thighJoints[legIndex].targetRotation, new Quaternion(0, 0, 0, thighJoints[legIndex].targetRotation.w), legsCounter);
                //legJoints[legIndex].targetRotation = Quaternion.Slerp(legJoints[legIndex].targetRotation, new Quaternion(0, 0, 0, legJoints[legIndex].targetRotation.w), legsCounter);


                //FORCE
                legs[legIndex].AddForce(Vector3.up * liftForce * Time.deltaTime, ForceMode.Impulse);
                //
                float inFrontM = Mathf.Clamp01(speed * inFrontVelocityM + 0.75f);
                //
                legs[legIndex].AddForce(horizontalVelocity * moveForwardForce * inFrontM * Time.deltaTime, ForceMode.Impulse);
                legs[altlegIndex].AddForce(-horizontalVelocity * moveForwardForce * inFrontM * Time.deltaTime, ForceMode.Impulse);
                //
                legs[legIndex].AddForce(-Vector3.up * holdDownForce * 0.5f * Time.deltaTime, ForceMode.Impulse);
                //
                legs[altlegIndex].AddForce(-Vector3.up * holdDownForce * Time.deltaTime, ForceMode.Impulse);

            }
            else if (legsCounter > legRate * 0.5f)
            {
                // ***********************  3rd PHASE OF STEP, STRAIGHTENING LEG OUT IN FRONT ********
                //
                //thighJoints[legIndex].targetRotation = new Quaternion(1f, thighJoints[legIndex].targetRotation.y, thighJoints[legIndex].targetRotation.z, thighJoints[legIndex].targetRotation.w);
                //legJoints[legIndex].targetRotation = new Quaternion(-8f, legJoints[legIndex].targetRotation.y, legJoints[legIndex].targetRotation.z, legJoints[legIndex].targetRotation.w);
                //float smoothTime = 0.3f;
                //float yVelocity = 0.0f;
                //float thighValue = Mathf.SmoothDamp(thighJoints[legIndex].targetRotation.x, 1f, ref yVelocity, legRate * 0.75f);
                //float legValue = Mathf.SmoothDamp(legJoints[legIndex].targetRotation.x, -8f, ref yVelocity, legRate * 0.75f);
                float thighValue = Mathf.Lerp(thighJoints[legIndex].targetRotation.x, 0.5f, Time.deltaTime * lerpspeed); //(legsCounter - legRate * 0.5f) / legRate * 0.25f * 10);
                float legValue = Mathf.Lerp(legJoints[legIndex].targetRotation.x, -2, Time.deltaTime * lerpspeed); //(legsCounter - legRate * 0.5f) / legRate * 0.25f * 10);

               // Debug.Log("thighValue : " + thighValue+ " | Time Value : "+ (legsCounter - legRate * 0.5f) / legRate * 0.25f * 10);

                thighJoints[legIndex].targetRotation = new Quaternion(thighValue, thighJoints[legIndex].targetRotation.y, thighJoints[legIndex].targetRotation.z, thighJoints[legIndex].targetRotation.w);
                legJoints[legIndex].targetRotation = new Quaternion(legValue, legJoints[legIndex].targetRotation.y, legJoints[legIndex].targetRotation.z, legJoints[legIndex].targetRotation.w);

                //float thighValue1 = Mathf.Lerp(thighJoints[altlegIndex].targetRotation.x, -0.1f, Time.deltaTime * lerpspeed); //(legsCounter - legRate * 0.75f) / legRate * 0.25f * 10);
                //float legValue1 = Mathf.Lerp(legJoints[altlegIndex].targetRotation.x, -0.1f, Time.deltaTime * lerpspeed); //(legsCounter - legRate * 0.75f) / legRate * 0.25f * 10);
                //thighJoints[altlegIndex].targetRotation = new Quaternion(thighValue1, 0, 0, thighJoints[altlegIndex].targetRotation.w);
                //legJoints[altlegIndex].targetRotation = new Quaternion(legValue1, 0, 0, legJoints[altlegIndex].targetRotation.w);

                //thighJoints[legIndex].targetRotation = Quaternion.Slerp(thighJoints[legIndex].targetRotation, new Quaternion(1f, thighJoints[legIndex].targetRotation.y, thighJoints[legIndex].targetRotation.z, thighJoints[legIndex].targetRotation.w), legsCounter);
                //legJoints[legIndex].targetRotation = Quaternion.Slerp(legJoints[legIndex].targetRotation, new Quaternion(-8f, legJoints[legIndex].targetRotation.y, legJoints[legIndex].targetRotation.z, legJoints[legIndex].targetRotation.w), legsCounter);



                //FORCE

                feet[legIndex].AddForce(horizontalVelocity * moveForwardForce * 0.4f * Time.deltaTime, ForceMode.Impulse);
                feet[altlegIndex].AddForce(-horizontalVelocity * moveForwardForce * 0.4f * Time.deltaTime, ForceMode.Impulse);
                //
                // chestBody.AddForceAtPosition(-Vector3.up * liftForce * 0.1f * Time.deltaTime, legs[legIndex].transform.position, ForceMode.Impulse);
                //
                thighs[legIndex].AddForceAtPosition(((horizontalVelocity + Vector3.up * 0.5f) * liftForce * 2) * Time.deltaTime, legs[legIndex].transform.TransformPoint(Vector3.left * 2), ForceMode.Impulse);
                thighs[legIndex].AddForceAtPosition(((-horizontalVelocity + Vector3.up * 0.5f) * liftForce * 2) * Time.deltaTime, legs[legIndex].transform.TransformPoint(Vector3.right * 2), ForceMode.Impulse);
                //
                shins[legIndex].AddForceAtPosition(horizontalVelocity * liftForce * Time.deltaTime * 3, legs[legIndex].transform.TransformPoint(Vector3.left * 2), ForceMode.Impulse);
                shins[legIndex].AddForceAtPosition(-horizontalVelocity * liftForce * Time.deltaTime * 3, legs[legIndex].transform.TransformPoint(Vector3.right * 2), ForceMode.Impulse);
                //
                legs[altlegIndex].AddForce(-Vector3.up * holdDownForce * Time.deltaTime, ForceMode.Impulse);
                //
                // chestBody.AddForceAtPosition(-horizontalVelocity * moveForwardForce * 1 * Time.deltaTime, chestBody.transform.TransformPoint(Vector3.right * 1.5f), ForceMode.Impulse);
                shins[legIndex].AddForceAtPosition(horizontalVelocity * moveForwardForce * 1 * Time.deltaTime, legs[legIndex].transform.TransformPoint(Vector3.left * 2), ForceMode.Impulse);
                //
                if (legsCounter > legRate * 0.66f)
                {
                    // *** ABOUT TO PLANT FOOT ***
                    //
                    if (chestBody.transform.InverseTransformPoint(feet[legIndex].transform.position).z < 0)
                    {
                        // **** FOOT BEHIND CHEST ****
                        //
                        //legsCounter = legRate * 0.66f; // **** DON'T PROGRESS YET ****** HOLD FOOTSTEP ****
                    }
                }

            }
            else if (legsCounter > legRate * 0.25f)
            {
                // ***********************  2nd PHASE OF STEP, BENDING THE KNEE AND LIFTING THIGH ********
                //
                
                float thighValue = Mathf.Lerp(thighJoints[legIndex].targetRotation.x, 0.5f, Time.deltaTime * lerpspeed); //(legsCounter - legRate * 0.25f) / legRate * 0.25f * 10);
                float legValue = Mathf.Lerp(legJoints[legIndex].targetRotation.x, -2f, Time.deltaTime * lerpspeed);//(legsCounter - legRate * 0.25f) / legRate * 0.25f * 10);

                thighJoints[legIndex].targetRotation = new Quaternion( thighValue, thighJoints[legIndex].targetRotation.y, thighJoints[legIndex].targetRotation.z, thighJoints[legIndex].targetRotation.w);
                legJoints[legIndex].targetRotation = new Quaternion(legValue, legJoints[legIndex].targetRotation.y, legJoints[legIndex].targetRotation.z, legJoints[legIndex].targetRotation.w);


                //thighJoints[legIndex].targetRotation = new Quaternion(0.5f, thighJoints[legIndex].targetRotation.y, thighJoints[legIndex].targetRotation.z, thighJoints[legIndex].targetRotation.w);
                //legJoints[legIndex].targetRotation = new Quaternion(-2f, legJoints[legIndex].targetRotation.y, legJoints[legIndex].targetRotation.z, legJoints[legIndex].targetRotation.w);


                //thighJoints[legIndex].targetRotation = Quaternion.Slerp(thighJoints[legIndex].targetRotation, new Quaternion(0.5f, thighJoints[legIndex].targetRotation.y, thighJoints[legIndex].targetRotation.z, thighJoints[legIndex].targetRotation.w), legsCounter);
                //legJoints[legIndex].targetRotation = Quaternion.Slerp(legJoints[legIndex].targetRotation, new Quaternion(-2f, legJoints[legIndex].targetRotation.y, legJoints[legIndex].targetRotation.z, legJoints[legIndex].targetRotation.w), legsCounter);



                //FORCE

                feet[legIndex].AddForce(horizontalVelocity * moveForwardForce * 0.4f * Time.deltaTime, ForceMode.Impulse);
                feet[altlegIndex].AddForce(-horizontalVelocity * moveForwardForce * 0.4f * Time.deltaTime, ForceMode.Impulse);
                //
                // chestBody.AddForceAtPosition(Vector3.up * liftForce * 0.1f * Time.deltaTime, legs[legIndex].transform.position, ForceMode.Impulse);
                thighs[legIndex].AddForceAtPosition(((horizontalVelocity + Vector3.up * 0.5f) * liftForce * 2) * Time.deltaTime, legs[legIndex].transform.TransformPoint(Vector3.left * 2), ForceMode.Impulse);
                thighs[legIndex].AddForceAtPosition(((-horizontalVelocity - Vector3.up * 0.5f) * liftForce * 2) * Time.deltaTime, legs[legIndex].transform.TransformPoint(Vector3.right * 2), ForceMode.Impulse);
                //
                shins[legIndex].AddForceAtPosition(-horizontalVelocity * liftForce * Time.deltaTime, legs[legIndex].transform.TransformPoint(Vector3.left * 2), ForceMode.Impulse);
                shins[legIndex].AddForceAtPosition(horizontalVelocity * liftForce * Time.deltaTime, legs[legIndex].transform.TransformPoint(Vector3.right * 2), ForceMode.Impulse);
                //Vector3.right
                legs[altlegIndex].AddForce(-Vector3.up * holdDownForce * Time.deltaTime, ForceMode.Impulse);

            }
            else
            {
                // ***********************  BEGINNING OF STEP, LIFTING NEW FOOT OFF THE GROUND ********
                //

                //thighJoints[legIndex].targetRotation = Quaternion.Slerp(thighJoints[legIndex].targetRotation,  new Quaternion(0.4f, thighJoints[legIndex].targetRotation.y, thighJoints[legIndex].targetRotation.z, thighJoints[legIndex].targetRotation.w), legsCounter);
                //legJoints[legIndex].targetRotation = Quaternion.Slerp(legJoints[legIndex].targetRotation, new Quaternion(-0.4f, legJoints[legIndex].targetRotation.y, legJoints[legIndex].targetRotation.z, legJoints[legIndex].targetRotation.w), legsCounter);
                //float smoothTime = 0.3f;
                //float yVelocity = 0.0f;
                //float thighValue = Mathf.SmoothDamp(thighJoints[legIndex].targetRotation.x, 0.4f, ref yVelocity, legRate * 0.25f);
                //float legValue = Mathf.SmoothDamp(legJoints[legIndex].targetRotation.x, -0.4f, ref yVelocity, legRate * 0.25f);

                float thighValue = Mathf.Lerp(thighJoints[legIndex].targetRotation.x, 0.4f, Time.deltaTime * lerpspeed);// Mathf.Lerp(thighJoints[legIndex].targetRotation.x, 0.4f, legsCounter / legRate * 0.25f * 10);
                float legValue = Mathf.Lerp(legJoints[legIndex].targetRotation.x, -0.4f, Time.deltaTime * lerpspeed);//legsCounter / legRate * 0.25f * 10);

                thighJoints[legIndex].targetRotation = new Quaternion(thighValue, thighJoints[legIndex].targetRotation.y, thighJoints[legIndex].targetRotation.z, thighJoints[legIndex].targetRotation.w);
                legJoints[legIndex].targetRotation = new Quaternion(legValue, legJoints[legIndex].targetRotation.y, legJoints[legIndex].targetRotation.z, legJoints[legIndex].targetRotation.w);

                //thighJoints[legIndex].targetRotation = new Quaternion(0.4f, thighJoints[legIndex].targetRotation.y, thighJoints[legIndex].targetRotation.z, thighJoints[legIndex].targetRotation.w);
                //legJoints[legIndex].targetRotation = new Quaternion(-0.4f, legJoints[legIndex].targetRotation.y, legJoints[legIndex].targetRotation.z, legJoints[legIndex].targetRotation.w);




                //thighJoints[altlegIndex].GetComponent<ConfigurableJoint>().targetRotation = new Quaternion(thighJoints[altlegIndex].GetComponent<ConfigurableJoint>().targetRotation.x - 1 * legsCounter, thighJoints[altlegIndex].GetComponent<ConfigurableJoint>().targetRotation.y, thighJoints[altlegIndex].GetComponent<ConfigurableJoint>().targetRotation.z, thighJoints[altlegIndex].GetComponent<ConfigurableJoint>().targetRotation.w);
                //legJoints[altlegIndex].GetComponent<ConfigurableJoint>().targetRotation = new Quaternion(0, legJoints[altlegIndex].GetComponent<ConfigurableJoint>().targetRotation.y, legJoints[altlegIndex].GetComponent<ConfigurableJoint>().targetRotation.z, legJoints[altlegIndex].GetComponent<ConfigurableJoint>().targetRotation.w);



                ///FORCE
                feet[legIndex].AddForce(Vector3.up * liftForce * Time.deltaTime, ForceMode.Impulse);
                // 
                // chestBody.AddForceAtPosition(-Vector3.up * liftForce * 0.1f * Time.deltaTime, legs[legIndex].transform.position, ForceMode.Impulse);
                //
                //
                thighs[legIndex].AddForceAtPosition(((horizontalVelocity) * liftForce * 2) * Time.deltaTime, legs[legIndex].transform.TransformPoint(Vector3.left * 2), ForceMode.Impulse);
                thighs[legIndex].AddForceAtPosition(((-horizontalVelocity) * liftForce * 2) * Time.deltaTime, legs[legIndex].transform.TransformPoint(Vector3.right * 2), ForceMode.Impulse);
                //
                legs[legIndex].AddForce(horizontalVelocity * moveForwardForce * 1 * Time.deltaTime, ForceMode.Impulse);
                legs[altlegIndex].AddForce(-horizontalVelocity * moveForwardForce * 1 * Time.deltaTime, ForceMode.Impulse);
                // 
                feet[altlegIndex].AddForce(-Vector3.up * holdDownForce * Time.deltaTime, ForceMode.Impulse);
            }
            //
        }
        else
        {
            // *****************  NOT WALKING - HOLD THE FEET TO THE GROUND ***********
            //
            //rootJoint.targetRotation = Quaternion.Slerp(rootJoint.targetRotation, Quaternion.Inverse(rotation), Time.time * lerpspeed);

            float thighValue = Mathf.Lerp(thighJoints[legIndex].targetRotation.x, 0f, Time.deltaTime * lerpspeed);// Mathf.Lerp(thighJoints[legIndex].targetRotation.x, 0.4f, legsCounter / legRate * 0.25f * 10);
            float legValue = Mathf.Lerp(legJoints[legIndex].targetRotation.x, -0f, Time.deltaTime * lerpspeed);//legsCounter / legRate * 0.25f * 10);

            float thighValue1 = Mathf.Lerp(thighJoints[altlegIndex].targetRotation.x, 0f, Time.deltaTime * lerpspeed);// Mathf.Lerp(thighJoints[legIndex].targetRotation.x, 0.4f, legsCounter / legRate * 0.25f * 10);
            float legValue1 = Mathf.Lerp(legJoints[altlegIndex].targetRotation.x, -0f, Time.deltaTime * lerpspeed);//legsCounter / legRate * 0.25f * 10);

            thighJoints[legIndex].targetRotation = new Quaternion(thighValue, 0, 0, thighJoints[legIndex].targetRotation.w);
            legJoints[legIndex].targetRotation = new Quaternion(legValue, 0, 0, legJoints[legIndex].targetRotation.w);

            thighJoints[altlegIndex].targetRotation = new Quaternion(thighValue1, 0, 0, thighJoints[altlegIndex].targetRotation.w);
            legJoints[altlegIndex].targetRotation = new Quaternion(legValue1, 0, 0, legJoints[altlegIndex].targetRotation.w);



            //thighJoints[legIndex].targetRotation = Quaternion.Slerp(thighJoints[legIndex].targetRotation, new Quaternion(0, 0, 0, thighJoints[legIndex].targetRotation.w), legsCounter);
            //legJoints[legIndex].targetRotation = Quaternion.Slerp(legJoints[legIndex].targetRotation, new Quaternion(0, 0, 0, legJoints[legIndex].targetRotation.w), legsCounter);



            //thighJoints[altlegIndex].targetRotation = Quaternion.Slerp(thighJoints[altlegIndex].targetRotation, new Quaternion(0, 0, 0, thighJoints[altlegIndex].targetRotation.w), legsCounter);
            //legJoints[altlegIndex].targetRotation = Quaternion.Slerp(legJoints[altlegIndex].targetRotation, new Quaternion(0, 0, 0, legJoints[altlegIndex].targetRotation.w), legsCounter);



            //FORCE
            legs[legIndex].AddForce(-Vector3.up * holdDownForce * Time.deltaTime, ForceMode.Impulse);
            legs[altlegIndex].AddForce(-Vector3.up * holdDownForce * Time.deltaTime, ForceMode.Impulse);

        }
        //
    }

    private void TakeStep()
    {
        // *** CHANGES THE LEG ***
        //
        legIndex = (legIndex + 1) % 2;
        //
        altlegIndex = 1 - legIndex;
        //
        legsCounter -= legRate;
        //
        //foreach (var item in legHeights)
        //{
        //    print(item);
        //}
        //Debug.Log($" Leg Index : {legIndex} | Leg Alt Index : {altlegIndex} | {legHeights[legIndex].name}");
        //print(legHeights.Length);
        //legHeights[legIndex].enabled = true;
        //legHeights[altlegIndex].enabled = false;


    }
}
