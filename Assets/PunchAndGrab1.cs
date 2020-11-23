using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchAndGrab1 : MonoBehaviour
{
    public Vector3 inputDirection;

    public Rigidbody[] armsBody = new Rigidbody[2];
    public ConfigurableJoint[] armsJoint = new ConfigurableJoint[2];

    public Rigidbody[] forearmsBody = new Rigidbody[2];
    public ConfigurableJoint[] forearmsJoint = new ConfigurableJoint[2];

    public Rigidbody[] handsBody = new Rigidbody[2];
    public ConfigurableJoint[] handsJoint = new ConfigurableJoint[2];

    public Rigidbody chestBody;
    public ConfigurableJoint chestJoint;

    public Rigidbody hipBody;

    public Rigidbody headBody;
    public ConfigurableJoint headJoint;


    private float leftbuttonClickTimer = 0f;
    private float rightbuttonClickTimer = 0f;

    public bool leftgrab = false;
    public bool leftpunch = false;

    public bool rightgrab = false;
    public bool rightpunch = false;

    private float leftpunchTimer = 0.0f;
    private float rightpunchTimer = 0.0f;

    public float punchSpeed = 0.5f;


    public bool alignSpine = true;

    public float horizontalPullForce = 800f;
    public float verticalPullForce = 1000f;

    public float punchPower = 1300f;

    public float headbuttPower = 1000f;

    public float minThrow = 100f;
    public float maxThrow = 400f;

    public float humanThrowScale = 15f;
    public Vector3 pullForceVector;
    public Vector3 climbForceVector;


    private float xForceDirection = 0f, zForceDirection = 0f;
    private float initialPunchSpeed = 0f;

    private float initialPunchPower;



    public bool leftThrow = false;
    public bool rightThrow = false;

    public bool climbing = false;

    public GrabCheck leftGrabCheck;
    public GrabCheck rightGrabCheck;


    public Vector3 grabPointLeft;

    public Vector3 grabPointRight;

    public Vector3 target;

    public Movement myCharacterMovement;
    // Start is called before the first frame update
    void Start()
    {
        initialPunchSpeed = punchSpeed;

        initialPunchPower = punchPower;
        myCharacterMovement = GetComponent<Movement>();
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(grabPointLeft, 0.3F);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(grabPointRight, 0.3F);

    }
    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    private void FixedUpdate()
    {
        FindTarget();
        Punching();
    }
    public void CheckInput()
    {

        //inputDirection = Vector3.zero;
        ////inputDirection = new Vector3(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"), 0);


        ////if (inputDirection != Vector3.zero)
        ////{
        ////    inputDirection.Normalize();
        ////    inputDirection = Camera.main.transform.TransformDirection(inputDirection);
        ////    inputDirection.z = 0f;



        ////}


        //inputDirection = Camera.main.transform.forward * 100f;//+ new Vector3(0, Camera.main.transform.forward.y * 5f, 0);

        ////if (inputDirection.y > 0)
        ////{
        ////    inputDirection.y += inputDirection.y * 3;
        ////}

        //inputDirection.y +=  1f;
        //inputDirection.Normalize();

        //inputDirection.z = 0f;

        //Vector3 localMove = chestBody.transform.InverseTransformDirection(inputDirection);


        //if (myCharacterMovement.inputDirection.x > 0.5f || myCharacterMovement.inputDirection.x < -0.5f)
        //{
        //    xForceDirection = myCharacterMovement.inputDirection.x;
        //}
        //else
        //{

        //    xForceDirection = inputDirection.x;
        //}

        //if (myCharacterMovement.inputDirection.z > 0.5f || myCharacterMovement.inputDirection.z < -0.5f)
        //{
        //    zForceDirection = myCharacterMovement.inputDirection.z;
        //}
        //else
        //{
        //    zForceDirection = inputDirection.z;
        //}



        //Vector3 horizontalVelocity = chestBody.velocity;

        //float speed = chestBody.velocity.sqrMagnitude;

        //Vector3 verticalHandVelocity = handsBody[0].velocity;





        //if (leftGrabCheck.hasJoint || rightGrabCheck.hasJoint)
        //{
        //    //chestBody.velocity *= -30f * Time.deltaTime;
        //}
        //pullForceVector = new Vector3(xForceDirection * (horizontalPullForce / (1 + Mathf.Abs(horizontalVelocity.x) * 4)), inputDirection.y * verticalPullForce, zForceDirection * (horizontalPullForce / (1 + Mathf.Abs(horizontalVelocity.z) * 4)));


        //if (inputDirection.y <= 0)
        //{
        //    climbForceVector = new Vector3(xForceDirection * (horizontalPullForce / (1 + Mathf.Abs(horizontalVelocity.x) * 4)), inputDirection.y * verticalPullForce, zForceDirection * (horizontalPullForce / (1 + Mathf.Abs(horizontalVelocity.z) * 4)));
        //}
        //else
        //{
        //    climbForceVector = new Vector3(xForceDirection * (horizontalPullForce / (1 + Mathf.Abs(horizontalVelocity.x) * 4)), inputDirection.y * verticalPullForce / 10f, zForceDirection * (horizontalPullForce / (1 + Mathf.Abs(horizontalVelocity.z) * 4)));
        //}




        //LEft and Right Punch Grab and Throw 
        if (Input.GetButtonDown("Fire1"))
        {

            leftbuttonClickTimer = 0f;
            leftgrab = false;

        }

        else if (Input.GetButton("Fire1"))
        {
            //print("Key Held");
            if (leftbuttonClickTimer < 1f) // as long as key is held down increase time, this records how long the key is held down
            {
                leftbuttonClickTimer += Time.deltaTime; //this records how long the key is held down
            }
            if (leftbuttonClickTimer > 0.2f && leftgrab == false)    // if the button is pressed for more than 0.2 seconds grab
            {
                leftgrab = true;


            }
            else    // else ready the arm, pull back for punch / grab
            {
                //call punch action readying
            }

        }

        else if (Input.GetButtonUp("Fire1"))
        {
            //print("Key Released");
            if (leftbuttonClickTimer <= 0.2f && !leftpunch) // as long as key is held down increase time, this records how long the key is held down
            {
                leftpunch = true;
                leftbuttonClickTimer = 0f;
            }
            else
            {
                leftThrow = true;

            }
        }

        if (Input.GetButtonDown("Fire2"))
        {

            rightbuttonClickTimer = 0f;
            rightgrab = false;

        }

        else if (Input.GetButton("Fire2"))
        {

            if (rightbuttonClickTimer < 1f) // as long as key is held down increase time, this records how long the key is held down
            {
                rightbuttonClickTimer += Time.deltaTime; //this records how long the key is held down
            }
            if (rightbuttonClickTimer > 0.2f && rightgrab == false)    // if the button is pressed for more than 0.2 seconds grab
            {
                rightgrab = true;


            }
            else    // else ready the arm, pull back for punch / grab
            {
                //call punch action readying
            }

        }

        else if (Input.GetButtonUp("Fire2"))
        {
            //print("Key Released");
            if (rightbuttonClickTimer <= 0.2f && !rightpunch) // as long as key is held down increase time, this records how long the key is held down
            {
                rightpunch = true;
                rightbuttonClickTimer = 0f;
            }
            else
            {
                rightThrow = true;
                //if (rightGrabCheck.mycollision != null)
                //{

                //    if (rightGrabCheck.mycollision != null)
                //    {
                //        if (rightGrabCheck.mycollision.GetComponent<InteractableObject>() != null)
                //        {
                //            if (rightGrabCheck.mycollision.GetComponent<InteractableObject>().partOfRagdoll)
                //            {
                //                /* print("Throw Force : " + rightGrabCheck.mycollision.GetComponent<Rigidbody>().velocity * rightGrabCheck.mycollision.GetComponent<Rigidbody>().velocity.magnitude * 500f / rightGrabCheck.mycollision.GetComponent<Rigidbody>().mass);
                //                 print("Throw Force MAgnitude: " + Vector3.ClampMagnitude(rightGrabCheck.mycollision.GetComponent<Rigidbody>().velocity * rightGrabCheck.mycollision.GetComponent<Rigidbody>().velocity.magnitude * 500f / rightGrabCheck.mycollision.GetComponent<Rigidbody>().mass, 1000f));
                //                 */ //rightGrabCheck.mycollision.GetComponent<Rigidbody>().AddForce(Vector3.ClampMagnitude(rightGrabCheck.mycollision.GetComponent<Rigidbody>().velocity* 1500f , 3000f) * Time.deltaTime, ForceMode.VelocityChange);

                //                rightGrabCheck.mycollision.GetComponent<Rigidbody>().AddForce(Vector3.ClampMagnitude(rightGrabCheck.mycollision.GetComponent<Rigidbody>().velocity * rightGrabCheck.mycollision.GetComponent<Rigidbody>().velocity.magnitude * minThrow * humanThrowScale / rightGrabCheck.mycollision.GetComponent<Rigidbody>().mass, maxThrow * humanThrowScale) * Time.deltaTime, ForceMode.VelocityChange);
                //            }
                //        }
                //    }

                //    rightGrabCheck.mycollision.GetComponent<Rigidbody>().AddForce(Vector3.ClampMagnitude(rightGrabCheck.mycollision.GetComponent<Rigidbody>().velocity * rightGrabCheck.mycollision.GetComponent<Rigidbody>().velocity.magnitude * minThrow / rightGrabCheck.mycollision.GetComponent<Rigidbody>().mass, maxThrow) * Time.deltaTime, ForceMode.VelocityChange);

                //}

                // climbing = false;



            }
        }
    }

    public void Punching()
    {
        if (leftThrow)
        {

            if (leftGrabCheck.mycollision != null)
            {
                if (leftGrabCheck.mycollision.GetComponent<Rigidbody>())
                {
                    if (leftGrabCheck.mycollision.name.Contains("PCHR"))
                    {
                        leftGrabCheck.mycollision.GetComponent<Rigidbody>().AddForce(Vector3.ClampMagnitude(leftGrabCheck.mycollision.GetComponent<Rigidbody>().velocity * leftGrabCheck.mycollision.GetComponent<Rigidbody>().velocity.magnitude * minThrow * humanThrowScale / leftGrabCheck.mycollision.GetComponent<Rigidbody>().mass, maxThrow * humanThrowScale) * Time.deltaTime, ForceMode.VelocityChange);

                    }

                    leftGrabCheck.mycollision.GetComponent<Rigidbody>().AddForce(Vector3.ClampMagnitude(leftGrabCheck.mycollision.GetComponent<Rigidbody>().velocity * leftGrabCheck.mycollision.GetComponent<Rigidbody>().velocity.magnitude * minThrow / leftGrabCheck.mycollision.GetComponent<Rigidbody>().mass, maxThrow) * Time.deltaTime, ForceMode.VelocityChange);

                }


            }
            leftThrow = false;
            leftgrab = false;
            leftGrabCheck.grabNow = false;
        }

        if (rightThrow)
        {
            if (rightGrabCheck.mycollision != null)
            {

                if (rightGrabCheck.mycollision.GetComponent<Rigidbody>())
                {
                    if (rightGrabCheck.mycollision.name.Contains("PCHR"))
                    {
                        rightGrabCheck.mycollision.GetComponent<Rigidbody>().AddForce(Vector3.ClampMagnitude(rightGrabCheck.mycollision.GetComponent<Rigidbody>().velocity * rightGrabCheck.mycollision.GetComponent<Rigidbody>().velocity.magnitude * minThrow * humanThrowScale / rightGrabCheck.mycollision.GetComponent<Rigidbody>().mass, maxThrow * humanThrowScale) * Time.deltaTime, ForceMode.VelocityChange);

                    }


                    rightGrabCheck.mycollision.GetComponent<Rigidbody>().AddForce(Vector3.ClampMagnitude(rightGrabCheck.mycollision.GetComponent<Rigidbody>().velocity * rightGrabCheck.mycollision.GetComponent<Rigidbody>().velocity.magnitude * minThrow / rightGrabCheck.mycollision.GetComponent<Rigidbody>().mass, maxThrow) * Time.deltaTime, ForceMode.VelocityChange);

                }

            }
            rightThrow = false;
            rightgrab = false;
            rightGrabCheck.grabNow = false;
        }

        if (leftgrab)
        {

            if (target != null)
            {
                Vector3 a;

                a = (target - handsBody[0].transform.position).normalized;




                armsBody[0].AddForce(-(a * 200f) * Time.deltaTime, ForceMode.VelocityChange);
                handsBody[0].AddForce(a * 200f * Time.deltaTime, ForceMode.VelocityChange);

                leftGrabCheck.grabNow = true;
                if (leftGrabCheck.hasJoint)
                {
                    if (leftGrabCheck.mycollision != null)
                    {
                        if (leftGrabCheck.mycollision.name.Contains("PCHR"))
                        {
                            forearmsBody[0].AddForce(-((pullForceVector) * 0.8f) * Time.deltaTime, ForceMode.VelocityChange);

                            handsBody[0].AddForce(pullForceVector * 1f * Time.deltaTime, ForceMode.VelocityChange);
                        }
                        else
                        {
                            forearmsBody[0].AddForce(-((pullForceVector) * 1f) * Time.deltaTime, ForceMode.VelocityChange);

                            handsBody[0].AddForce(pullForceVector * 1f * Time.deltaTime, ForceMode.VelocityChange);

                        }

                    }
                }


                armsBody[0].AddForce(-((inputDirection) * 300f) * Time.deltaTime, ForceMode.VelocityChange);

                handsBody[0].AddForce((inputDirection) * 300f * Time.deltaTime, ForceMode.VelocityChange);


            }


        }

        if (rightgrab)
        {

            if (target != null)
            {
                Vector3 a;

                a = (target - handsBody[1].transform.position).normalized;





                armsBody[1].AddForce(-(a * 200f) * Time.deltaTime, ForceMode.VelocityChange);
                handsBody[1].AddForce(a * 200f * Time.deltaTime, ForceMode.VelocityChange);

                rightGrabCheck.grabNow = true;

                if (rightGrabCheck.hasJoint)
                {
                    if (rightGrabCheck.mycollision != null)
                    {
                        if (rightGrabCheck.mycollision.name.Contains("PCHR"))
                        {
                            forearmsBody[1].AddForce(-((pullForceVector) * 0.8f) * Time.deltaTime, ForceMode.VelocityChange);

                            handsBody[1].AddForce(pullForceVector * 1f * Time.deltaTime, ForceMode.VelocityChange);
                        }
                        else
                        {
                            forearmsBody[1].AddForce(-((pullForceVector) * 1f) * Time.deltaTime, ForceMode.VelocityChange);
                            handsBody[1].AddForce(pullForceVector * 1f * Time.deltaTime, ForceMode.VelocityChange);


                        }

                    }
                }



                armsBody[1].AddForce(-((inputDirection) * 300f) * Time.deltaTime, ForceMode.VelocityChange);

                handsBody[1].AddForce((inputDirection) * 300f * Time.deltaTime, ForceMode.VelocityChange);

            }


        }


        if (leftpunch)
        {
            //EffectsController.PlayPunchSound(handsBody[0].position, handsBody[0].velocity.sqrMagnitude, handsBody[0].name);
            leftpunchTimer += Time.deltaTime;
            if (leftpunchTimer < punchSpeed * 0.2f)
            {

                JointDrive x = armsJoint[0].slerpDrive;
                x.positionDamper = 100f;
                x.positionSpring = 1000f;
                x.maximumForce = 1000f;
                armsJoint[0].slerpDrive = x;

                Vector3 jointValue = new Vector3(20f, 0f, 0f);

                armsJoint[0].targetAngularVelocity = jointValue;

                JointDrive x1 = forearmsJoint[0].slerpDrive;
                x1.positionDamper = 100f;
                x1.positionSpring = 1000f;
                x1.maximumForce = 1000f;
                forearmsJoint[0].slerpDrive = x1;

                Vector3 jointValue1 = new Vector3(-20f, 0f, 0f);

                forearmsJoint[0].targetAngularVelocity = jointValue1;

                chestBody.velocity *= -10f * Time.deltaTime;
            }
            if (leftpunchTimer >= punchSpeed * 0.4f && leftpunchTimer < punchSpeed * 0.6f)
            {
                JointDrive x = armsJoint[0].slerpDrive;
                x.positionDamper = 0f;
                x.positionSpring = 0f;
                x.maximumForce = 0f;
                armsJoint[0].slerpDrive = x;

                Vector3 jointValue = new Vector3(20f, 0f, 0f);

                armsJoint[0].targetAngularVelocity = jointValue;

                JointDrive x1 = forearmsJoint[0].slerpDrive;
                x1.positionDamper = 0f;
                x1.positionSpring = 0f;
                x1.maximumForce = 0f;
                forearmsJoint[0].slerpDrive = x1;

                Vector3 jointValue1 = new Vector3(0f, 0f, 0f);

                forearmsJoint[0].targetAngularVelocity = jointValue1;
                //handsBody[0].AddForceAtPosition((handsBody[0].transform.forward ) * 10, handsBody[0].transform.TransformPoint(Vector3.back * 0.2f), ForceMode.Impulse);

                //handsBody[0].AddForce((chestBody.transform.position + chestBody.transform.forward + (chestBody.transform.forward * -1) / 2f - handsBody[0].transform.position).normalized * 1000 * Time.deltaTime, ForceMode.VelocityChange);

                if (target != null)
                {
                    Vector3 a = (target - handsBody[0].transform.position).normalized;
                    armsBody[0].AddForce(-(a * punchPower * 0.34f) * Time.deltaTime, ForceMode.VelocityChange);
                    handsBody[0].AddForce(a * punchPower * 1f * Time.deltaTime, ForceMode.VelocityChange);
                    /* armsBody[0].AddForce(-(a * 20f), ForceMode.VelocityChange);
                     handsBody[0].AddForce(a * 30f , ForceMode.VelocityChange);*/
                    if (leftpunchTimer >= punchSpeed * 0.59f && leftpunchTimer < punchSpeed * 0.6f)
                    {
                        //EffectsController.PlayPunchSound(handsBody[0].position, handsBody[0].velocity.sqrMagnitude, handsBody[0].name);
                        //EffectsController.Shake(0.03f, 0.1f);
                    }

                }
                //if (leftpunch || rightpunch)
                // {
                chestBody.velocity *= -30f * Time.deltaTime;
                //}


            }
            if (leftpunchTimer >= punchSpeed * 0.6f && leftpunchTimer < punchSpeed * 1f)
            {

            }
            if (leftpunchTimer >= punchSpeed * 1f)
            {
                leftpunch = false;
                leftpunchTimer = 0f;

            }
        }


        if (rightpunch)
        {
            rightpunchTimer += Time.deltaTime;
            if (rightpunchTimer < punchSpeed * 0.2f)
            {

                JointDrive x = armsJoint[1].slerpDrive;
                x.positionDamper = 100f;
                x.positionSpring = 1000f;
                x.maximumForce = 1000f;
                armsJoint[1].slerpDrive = x;

                Vector3 jointValue = new Vector3(-20f, 0f, 0f);

                armsJoint[1].targetAngularVelocity = jointValue;

                JointDrive x1 = forearmsJoint[1].slerpDrive;
                x1.positionDamper = 100f;
                x1.positionSpring = 1000f;
                x1.maximumForce = 1000f;
                forearmsJoint[1].slerpDrive = x1;

                Vector3 jointValue1 = new Vector3(20f, 0f, 0f);

                forearmsJoint[1].targetAngularVelocity = jointValue1;

                chestBody.velocity *= -10f * Time.deltaTime;
            }
            if (rightpunchTimer >= punchSpeed * 0.4f && rightpunchTimer < punchSpeed * 0.6f)
            {
                JointDrive x = armsJoint[1].slerpDrive;
                x.positionDamper = 0f;
                x.positionSpring = 0f;
                x.maximumForce = 0f;
                armsJoint[1].slerpDrive = x;

                Vector3 jointValue = new Vector3(0f, 0f, 0f);

                armsJoint[1].targetAngularVelocity = jointValue;

                JointDrive x1 = forearmsJoint[1].slerpDrive;
                x1.positionDamper = 0f;
                x1.positionSpring = 0f;
                x1.maximumForce = 0f;
                forearmsJoint[1].slerpDrive = x1;

                Vector3 jointValue1 = new Vector3(0f, 0f, 0f);

                forearmsJoint[1].targetAngularVelocity = jointValue1;
                //handsBody[0].AddForceAtPosition((handsBody[0].transform.forward ) * 10, handsBody[0].transform.TransformPoint(Vector3.back * 0.2f), ForceMode.Impulse);

                //handsBody[0].AddForce((chestBody.transform.position + chestBody.transform.forward + (chestBody.transform.forward * -1) / 2f - handsBody[0].transform.position).normalized * 1000 * Time.deltaTime, ForceMode.VelocityChange);

                if (target != null)
                {
                    Vector3 a = (target - handsBody[1].transform.position).normalized;
                    armsBody[1].AddForce(-(a * punchPower * 0.34f) * Time.deltaTime, ForceMode.VelocityChange);
                    handsBody[1].AddForce(a * punchPower * 1f * Time.deltaTime, ForceMode.VelocityChange);
                    if (rightpunchTimer >= punchSpeed * 0.59f && rightpunchTimer < punchSpeed * 0.6f)
                    {
                        //EffectsController.PlayPunchSound(handsBody[1].position, handsBody[1].velocity.sqrMagnitude, handsBody[1].name);
                        //EffectsController.Shake(0.03f, 0.1f);
                        print("handsBody[1].velocity.sqrMagnitude" + handsBody[1].velocity.sqrMagnitude);
                    }
                    /*armsBody[1].AddForce(-(a * 20f), ForceMode.VelocityChange);
                    handsBody[1].AddForce(a * 30f, ForceMode.VelocityChange);*/

                }
                //if (leftpunch || rightpunch)
                //{
                chestBody.velocity *= -30f * Time.deltaTime;
                //}


            }
            if (rightpunchTimer >= punchSpeed * 0.6f && rightpunchTimer < punchSpeed * 1f)
            {

            }
            if (rightpunchTimer >= punchSpeed * 1f)
            {
                rightpunch = false;
                rightpunchTimer = 0f;

            }
        }
    }

    public void FindTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(hipBody.transform.position, 1.5f);
        bool targetFound = false;
        Vector3 tempTarget = Vector3.zero;
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.name.Contains("PCHR_Head") && hitCollider.transform.root != this.transform)
            {
                Debug.Log($"Found Target : {hitCollider.name}");
                tempTarget = hitCollider.transform.position;

                targetFound = true;
                break;
            }
            else
            {
                //target = hipBody.transform.TransformPoint(Vector3.forward * 2f + Vector3.left * 1.5f);
                targetFound = false;
            }
        }

        if (targetFound)
        {
            target = tempTarget;
        }
        else
        {
            var camFaceDirection = Camera.main.transform.forward * 100f;//+ new Vector3(0, Camera.main.transform.forward.y * 5f, 0);
            camFaceDirection.Normalize();
            //if(camFaceDirection.y > 0)
            //{
            //    camFaceDirection.y += camFaceDirection.y * 3;
            //}
            camFaceDirection.y += 1f;

            Vector3 localMove = chestBody.transform.InverseTransformDirection(camFaceDirection);
            //localMove.Normalize();
            float turnUpDownAmount = localMove.x;
            float turnLeftRightdAmount = localMove.y;
            target = headBody.transform.TransformPoint(localMove);
        }
    }
}
