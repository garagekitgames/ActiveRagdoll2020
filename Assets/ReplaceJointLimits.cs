using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ReplaceJointLimits : MonoBehaviour
{

    private string hipName = "Hip";
    private string chestName = "Chest";
    private string headName = "Head";
    private string larmName = "Arm_L";
    private string lfarmName = "FArm_L";
    private string rarmName = "Arm_R";
    private string rfarmName = "FArm_R";
    private string lhandName = "Hand_L";
    private string rhandName = "Hand_R";
    private string lthighName = "Thigh_L";
    private string rthighName = "Thigh_R";
    private string llegName = "Leg_L";
    private string rlegName = "Leg_R";
    private string lfootName = "Foot_L";
    private string rfootName = "Foot_R";

    bool changeJointLimits = true;
    // Start is called before the first frame update
    void Start()
    {
        CharacterJoint[] charJoints = GetComponentsInChildren<CharacterJoint>();
        Transform[] childTransforms = GetComponentsInChildren<Transform>();


        foreach (CharacterJoint charJoint in charJoints)
        {
            ConfigurableJoint confJoint;
            if (!charJoint.transform.GetComponent<ConfigurableJoint>())
            {

                confJoint = charJoint.gameObject.AddComponent<ConfigurableJoint>() as ConfigurableJoint;
                //				confJoint.autoConfigureConnectedAnchor = false;
                confJoint.connectedBody = charJoint.connectedBody;
                confJoint.anchor = charJoint.anchor;
                confJoint.axis = charJoint.axis;
                //				confJoint.connectedAnchor = charJoint.connectedAnchor;
                confJoint.secondaryAxis = charJoint.swingAxis;
                confJoint.xMotion = ConfigurableJointMotion.Locked;
                confJoint.yMotion = ConfigurableJointMotion.Locked;
                confJoint.zMotion = ConfigurableJointMotion.Locked;
                confJoint.angularXMotion = ConfigurableJointMotion.Limited;
                confJoint.angularYMotion = ConfigurableJointMotion.Limited;
                confJoint.angularZMotion = ConfigurableJointMotion.Limited;
                confJoint.lowAngularXLimit = charJoint.lowTwistLimit;
                confJoint.highAngularXLimit = charJoint.highTwistLimit;
                confJoint.angularYLimit = charJoint.swing1Limit;
                confJoint.angularZLimit = charJoint.swing2Limit;
                confJoint.rotationDriveMode = RotationDriveMode.Slerp;

                confJoint.enablePreprocessing = false;
                confJoint.projectionDistance = 0.1f;
                confJoint.projectionAngle = 180f;
                confJoint.projectionMode = JointProjectionMode.PositionAndRotation;
                //				JointDrive temp = confJoint.slerpDrive; // These are left here to remind us how to set the drive
                //				temp.mode = JointDriveMode.Position;
                //				temp.positionSpring = 0f;
                //				confJoint.slerpDrive = temp;
                //				confJoint.targetRotation = Quaternion.identity;
            }
            DestroyImmediate(charJoint);
        }

        foreach (Transform childTransform in childTransforms)
        {

            if (childTransform.name.ToLower().Contains(hipName.ToLower()))
            {
                /*if (!childTransform.gameObject.GetComponent<BodyPartMono>())
                {
                    BodyPartMono bpMono = childTransform.gameObject.AddComponent<BodyPartMono>();
                    bpMono.BodyPart = Hip;
                }*/


            }
            if (childTransform.name.ToLower().Contains(chestName.ToLower()))
            {
                if (childTransform.gameObject.GetComponent<ConfigurableJoint>())
                {
                    ConfigurableJoint confJoint = childTransform.gameObject.GetComponent<ConfigurableJoint>();
                    if (changeJointLimits)
                    {
                        SoftJointLimit LAX = new SoftJointLimit();
                        LAX.limit = -30f;
                        SoftJointLimit HAX = new SoftJointLimit();
                        HAX.limit = 30f;
                        SoftJointLimit Y = new SoftJointLimit();
                        Y.limit = 30f;
                        SoftJointLimit Z = new SoftJointLimit();
                        Z.limit = 30f;

                        confJoint.lowAngularXLimit = LAX;
                        confJoint.highAngularXLimit = HAX;
                        confJoint.angularYLimit = Y;
                        confJoint.angularZLimit = Z;
                    }
                    confJoint.rotationDriveMode = RotationDriveMode.Slerp;

                    confJoint.enablePreprocessing = false;
                    confJoint.projectionDistance = 0.1f;
                    confJoint.projectionAngle = 180f;
                    confJoint.projectionMode = JointProjectionMode.PositionAndRotation;


                    JointDrive x = confJoint.slerpDrive;
                    x.positionSpring = 0;
                    x.positionDamper = 0;
                    x.maximumForce = 0;
                    confJoint.slerpDrive = x;

                }
            }
            if (childTransform.name.ToLower().Contains(headName.ToLower()))
            {
                if (childTransform.gameObject.GetComponent<ConfigurableJoint>())
                {
                    ConfigurableJoint confJoint = childTransform.gameObject.GetComponent<ConfigurableJoint>();
                    if (changeJointLimits)
                    {
                        SoftJointLimit LAX = new SoftJointLimit();
                        LAX.limit = -50f;
                        SoftJointLimit HAX = new SoftJointLimit();
                        HAX.limit = 50f;
                        SoftJointLimit Y = new SoftJointLimit();
                        Y.limit = 50f;
                        SoftJointLimit Z = new SoftJointLimit();
                        Z.limit = 60f;

                        confJoint.lowAngularXLimit = LAX;
                        confJoint.highAngularXLimit = HAX;
                        confJoint.angularYLimit = Y;
                        confJoint.angularZLimit = Z;
                    }
                    confJoint.rotationDriveMode = RotationDriveMode.Slerp;

                    confJoint.enablePreprocessing = false;
                    confJoint.projectionDistance = 0.1f;
                    confJoint.projectionAngle = 180f;
                    confJoint.projectionMode = JointProjectionMode.PositionAndRotation;


                    JointDrive x = confJoint.slerpDrive;
                    x.positionSpring = 1000;
                    x.positionDamper = 100;
                    x.maximumForce = 10;
                    confJoint.slerpDrive = x;

                }
            }
            if (childTransform.name.ToLower().Contains(larmName.ToLower()))
            {
                if (childTransform.gameObject.GetComponent<ConfigurableJoint>())
                {
                    ConfigurableJoint confJoint = childTransform.gameObject.GetComponent<ConfigurableJoint>();
                    if (changeJointLimits)
                    {
                        SoftJointLimit LAX = new SoftJointLimit();
                        LAX.limit = -100;
                        SoftJointLimit HAX = new SoftJointLimit();
                        HAX.limit = 60;
                        SoftJointLimit Y = new SoftJointLimit();
                        Y.limit = 100;
                        SoftJointLimit Z = new SoftJointLimit();
                        Z.limit = 50;

                        confJoint.lowAngularXLimit = LAX;
                        confJoint.highAngularXLimit = HAX;
                        confJoint.angularYLimit = Y;
                        confJoint.angularZLimit = Z;
                    }
                    confJoint.rotationDriveMode = RotationDriveMode.Slerp;

                    confJoint.enablePreprocessing = false;
                    confJoint.projectionDistance = 0.1f;
                    confJoint.projectionAngle = 180f;
                    confJoint.projectionMode = JointProjectionMode.PositionAndRotation;


                    JointDrive x = confJoint.slerpDrive;
                    x.positionSpring = 0;
                    x.positionDamper = 0;
                    x.maximumForce = 0;
                    confJoint.slerpDrive = x;

                }
            }
            if (childTransform.name.ToLower().Contains(rarmName.ToLower()))
            {
                if (childTransform.gameObject.GetComponent<ConfigurableJoint>())
                {
                    ConfigurableJoint confJoint = childTransform.gameObject.GetComponent<ConfigurableJoint>();
                    if (changeJointLimits)
                    {
                        SoftJointLimit LAX = new SoftJointLimit();
                        LAX.limit = -60;
                        SoftJointLimit HAX = new SoftJointLimit();
                        HAX.limit = 100;
                        SoftJointLimit Y = new SoftJointLimit();
                        Y.limit = 100;
                        SoftJointLimit Z = new SoftJointLimit();
                        Z.limit = 50;

                        confJoint.lowAngularXLimit = LAX;
                        confJoint.highAngularXLimit = HAX;
                        confJoint.angularYLimit = Y;
                        confJoint.angularZLimit = Z;
                    }
                    confJoint.rotationDriveMode = RotationDriveMode.Slerp;

                    confJoint.enablePreprocessing = false;
                    confJoint.projectionDistance = 0.1f;
                    confJoint.projectionAngle = 180f;
                    confJoint.projectionMode = JointProjectionMode.PositionAndRotation;


                    JointDrive x = confJoint.slerpDrive;
                    x.positionSpring = 0;
                    x.positionDamper = 0;
                    x.maximumForce = 0;
                    confJoint.slerpDrive = x;

                }
            }
            if (childTransform.name.ToLower().Contains(lfarmName.ToLower()))
            {
                if (childTransform.gameObject.GetComponent<ConfigurableJoint>())
                {
                    ConfigurableJoint confJoint = childTransform.gameObject.GetComponent<ConfigurableJoint>();
                    if (changeJointLimits)
                    {
                        SoftJointLimit LAX = new SoftJointLimit();
                        LAX.limit = -150;
                        SoftJointLimit HAX = new SoftJointLimit();
                        HAX.limit = 20;
                        SoftJointLimit Y = new SoftJointLimit();
                        Y.limit = 20;
                        SoftJointLimit Z = new SoftJointLimit();
                        Z.limit = 30;

                        confJoint.lowAngularXLimit = LAX;
                        confJoint.highAngularXLimit = HAX;
                        confJoint.angularYLimit = Y;
                        confJoint.angularZLimit = Z;
                    }
                    confJoint.rotationDriveMode = RotationDriveMode.Slerp;

                    confJoint.enablePreprocessing = false;
                    confJoint.projectionDistance = 0.1f;
                    confJoint.projectionAngle = 180f;
                    confJoint.projectionMode = JointProjectionMode.PositionAndRotation;


                    JointDrive x = confJoint.slerpDrive;
                    x.positionSpring = 0;
                    x.positionDamper = 0;
                    x.maximumForce = 0;
                    confJoint.slerpDrive = x;

                }
            }
            if (childTransform.name.ToLower().Contains(rfarmName.ToLower()))
            {
                if (childTransform.gameObject.GetComponent<ConfigurableJoint>())
                {
                    ConfigurableJoint confJoint = childTransform.gameObject.GetComponent<ConfigurableJoint>();
                    if (changeJointLimits)
                    {
                        SoftJointLimit LAX = new SoftJointLimit();
                        LAX.limit = -20;
                        SoftJointLimit HAX = new SoftJointLimit();
                        HAX.limit = 150;
                        SoftJointLimit Y = new SoftJointLimit();
                        Y.limit = 20;
                        SoftJointLimit Z = new SoftJointLimit();
                        Z.limit = 30;

                        confJoint.lowAngularXLimit = LAX;
                        confJoint.highAngularXLimit = HAX;
                        confJoint.angularYLimit = Y;
                        confJoint.angularZLimit = Z;
                    }
                    confJoint.rotationDriveMode = RotationDriveMode.Slerp;

                    confJoint.enablePreprocessing = false;
                    confJoint.projectionDistance = 0.1f;
                    confJoint.projectionAngle = 180f;
                    confJoint.projectionMode = JointProjectionMode.PositionAndRotation;


                    JointDrive x = confJoint.slerpDrive;
                    x.positionSpring = 0;
                    x.positionDamper = 0;
                    x.maximumForce = 0;
                    confJoint.slerpDrive = x;

                }
            }
            if (childTransform.name.ToLower().Contains(lhandName.ToLower()))
            {
                if (childTransform.gameObject.GetComponent<ConfigurableJoint>())
                {
                    ConfigurableJoint confJoint = childTransform.gameObject.GetComponent<ConfigurableJoint>();
                    if (changeJointLimits)
                    {
                        SoftJointLimit LAX = new SoftJointLimit();
                        LAX.limit = 0;
                        SoftJointLimit HAX = new SoftJointLimit();
                        HAX.limit = 0;
                        SoftJointLimit Y = new SoftJointLimit();
                        Y.limit = 0;
                        SoftJointLimit Z = new SoftJointLimit();
                        Z.limit = 0;

                        confJoint.lowAngularXLimit = LAX;
                        confJoint.highAngularXLimit = HAX;
                        confJoint.angularYLimit = Y;
                        confJoint.angularZLimit = Z;
                    }
                    confJoint.rotationDriveMode = RotationDriveMode.Slerp;

                    confJoint.enablePreprocessing = false;
                    confJoint.projectionDistance = 0.1f;
                    confJoint.projectionAngle = 180f;
                    confJoint.projectionMode = JointProjectionMode.PositionAndRotation;


                    JointDrive x = confJoint.slerpDrive;
                    x.positionSpring = 0;
                    x.positionDamper = 0;
                    x.maximumForce = 0;
                    confJoint.slerpDrive = x;

                }
            }
            if (childTransform.name.ToLower().Contains(rhandName.ToLower()))
            {
                if (childTransform.gameObject.GetComponent<ConfigurableJoint>())
                {
                    ConfigurableJoint confJoint = childTransform.gameObject.GetComponent<ConfigurableJoint>();
                    if (changeJointLimits)
                    {
                        SoftJointLimit LAX = new SoftJointLimit();
                        LAX.limit = 0;
                        SoftJointLimit HAX = new SoftJointLimit();
                        HAX.limit = 0;
                        SoftJointLimit Y = new SoftJointLimit();
                        Y.limit = 0;
                        SoftJointLimit Z = new SoftJointLimit();
                        Z.limit = 0;

                        confJoint.lowAngularXLimit = LAX;
                        confJoint.highAngularXLimit = HAX;
                        confJoint.angularYLimit = Y;
                        confJoint.angularZLimit = Z;
                    }
                    confJoint.rotationDriveMode = RotationDriveMode.Slerp;

                    confJoint.enablePreprocessing = false;
                    confJoint.projectionDistance = 0.1f;
                    confJoint.projectionAngle = 180f;
                    confJoint.projectionMode = JointProjectionMode.PositionAndRotation;


                    JointDrive x = confJoint.slerpDrive;
                    x.positionSpring = 0;
                    x.positionDamper = 0;
                    x.maximumForce = 0;
                    confJoint.slerpDrive = x;

                }
            }
            if (childTransform.name.ToLower().Contains(lthighName.ToLower()))
            {
                if (childTransform.gameObject.GetComponent<ConfigurableJoint>())
                {
                    ConfigurableJoint confJoint = childTransform.gameObject.GetComponent<ConfigurableJoint>();
                    if (changeJointLimits)
                    {
                        SoftJointLimit LAX = new SoftJointLimit();
                        LAX.limit = -20;
                        SoftJointLimit HAX = new SoftJointLimit();
                        HAX.limit = 130;
                        SoftJointLimit Y = new SoftJointLimit();
                        Y.limit = 65;
                        SoftJointLimit Z = new SoftJointLimit();
                        Z.limit = 40;

                        confJoint.lowAngularXLimit = LAX;
                        confJoint.highAngularXLimit = HAX;
                        confJoint.angularYLimit = Y;
                        confJoint.angularZLimit = Z;
                    }
                    confJoint.rotationDriveMode = RotationDriveMode.Slerp;

                    confJoint.enablePreprocessing = false;
                    confJoint.projectionDistance = 0.1f;
                    confJoint.projectionAngle = 180f;
                    confJoint.projectionMode = JointProjectionMode.PositionAndRotation;


                    JointDrive x = confJoint.slerpDrive;
                    x.positionSpring = 0;
                    x.positionDamper = 0;
                    x.maximumForce = 0;
                    confJoint.slerpDrive = x;

                }
            }
            if (childTransform.name.ToLower().Contains(rthighName.ToLower()))
            {
                if (childTransform.gameObject.GetComponent<ConfigurableJoint>())
                {
                    ConfigurableJoint confJoint = childTransform.gameObject.GetComponent<ConfigurableJoint>();
                    if (changeJointLimits)
                    {
                        SoftJointLimit LAX = new SoftJointLimit();
                        LAX.limit = -20;
                        SoftJointLimit HAX = new SoftJointLimit();
                        HAX.limit = 130;
                        SoftJointLimit Y = new SoftJointLimit();
                        Y.limit = 65;
                        SoftJointLimit Z = new SoftJointLimit();
                        Z.limit = 40;

                        confJoint.lowAngularXLimit = LAX;
                        confJoint.highAngularXLimit = HAX;
                        confJoint.angularYLimit = Y;
                        confJoint.angularZLimit = Z;
                    }
                    confJoint.rotationDriveMode = RotationDriveMode.Slerp;

                    confJoint.enablePreprocessing = false;
                    confJoint.projectionDistance = 0.1f;
                    confJoint.projectionAngle = 180f;
                    confJoint.projectionMode = JointProjectionMode.PositionAndRotation;


                    JointDrive x = confJoint.slerpDrive;
                    x.positionSpring = 0;
                    x.positionDamper = 0;
                    x.maximumForce = 0;
                    confJoint.slerpDrive = x;

                }
            }
            if (childTransform.name.ToLower().Contains(llegName.ToLower()))
            {
                if (childTransform.gameObject.GetComponent<ConfigurableJoint>())
                {
                    ConfigurableJoint confJoint = childTransform.gameObject.GetComponent<ConfigurableJoint>();
                    if (changeJointLimits)
                    {
                        SoftJointLimit LAX = new SoftJointLimit();
                        LAX.limit = -177;
                        SoftJointLimit HAX = new SoftJointLimit();
                        HAX.limit = 0;
                        SoftJointLimit Y = new SoftJointLimit();
                        Y.limit = 5;
                        SoftJointLimit Z = new SoftJointLimit();
                        Z.limit = 5;

                        confJoint.lowAngularXLimit = LAX;
                        confJoint.highAngularXLimit = HAX;
                        confJoint.angularYLimit = Y;
                        confJoint.angularZLimit = Z;
                    }
                    confJoint.rotationDriveMode = RotationDriveMode.Slerp;

                    confJoint.enablePreprocessing = false;
                    confJoint.projectionDistance = 0.1f;
                    confJoint.projectionAngle = 180f;
                    confJoint.projectionMode = JointProjectionMode.PositionAndRotation;


                    JointDrive x = confJoint.slerpDrive;
                    x.positionSpring = 0;
                    x.positionDamper = 0;
                    x.maximumForce = 0;
                    confJoint.slerpDrive = x;

                }
            }
            if (childTransform.name.ToLower().Contains(rlegName.ToLower()))
            {
                if (childTransform.gameObject.GetComponent<ConfigurableJoint>())
                {
                    ConfigurableJoint confJoint = childTransform.gameObject.GetComponent<ConfigurableJoint>();
                    if (changeJointLimits)
                    {
                        SoftJointLimit LAX = new SoftJointLimit();
                        LAX.limit = -177;
                        SoftJointLimit HAX = new SoftJointLimit();
                        HAX.limit = 0;
                        SoftJointLimit Y = new SoftJointLimit();
                        Y.limit = 5;
                        SoftJointLimit Z = new SoftJointLimit();
                        Z.limit = 5;

                        confJoint.lowAngularXLimit = LAX;
                        confJoint.highAngularXLimit = HAX;
                        confJoint.angularYLimit = Y;
                        confJoint.angularZLimit = Z;
                    }
                    confJoint.rotationDriveMode = RotationDriveMode.Slerp;

                    confJoint.enablePreprocessing = false;
                    confJoint.projectionDistance = 0.1f;
                    confJoint.projectionAngle = 180f;
                    confJoint.projectionMode = JointProjectionMode.PositionAndRotation;


                    JointDrive x = confJoint.slerpDrive;
                    x.positionSpring = 0;
                    x.positionDamper = 0;
                    x.maximumForce = 0;
                    confJoint.slerpDrive = x;

                }
            }
            if (childTransform.name.ToLower().Contains(lfootName.ToLower()))
            {
                if (childTransform.gameObject.GetComponent<ConfigurableJoint>())
                {
                    ConfigurableJoint confJoint = childTransform.gameObject.GetComponent<ConfigurableJoint>();
                    if (changeJointLimits)
                    {
                        SoftJointLimit LAX = new SoftJointLimit();
                        LAX.limit = 0;
                        SoftJointLimit HAX = new SoftJointLimit();
                        HAX.limit = 0;
                        SoftJointLimit Y = new SoftJointLimit();
                        Y.limit = 0;
                        SoftJointLimit Z = new SoftJointLimit();
                        Z.limit = 0;

                        confJoint.lowAngularXLimit = LAX;
                        confJoint.highAngularXLimit = HAX;
                        confJoint.angularYLimit = Y;
                        confJoint.angularZLimit = Z;
                    }
                    confJoint.rotationDriveMode = RotationDriveMode.Slerp;

                    confJoint.enablePreprocessing = false;
                    confJoint.projectionDistance = 0.1f;
                    confJoint.projectionAngle = 180f;
                    confJoint.projectionMode = JointProjectionMode.PositionAndRotation;


                    JointDrive x = confJoint.slerpDrive;
                    x.positionSpring = 0;
                    x.positionDamper = 0;
                    x.maximumForce = 0;
                    confJoint.slerpDrive = x;

                }

            }
            if (childTransform.name.ToLower().Contains(rfootName.ToLower()))
            {
                if (childTransform.gameObject.GetComponent<ConfigurableJoint>())
                {
                    ConfigurableJoint confJoint = childTransform.gameObject.GetComponent<ConfigurableJoint>();
                    if (changeJointLimits)
                    {
                        SoftJointLimit LAX = new SoftJointLimit();
                        LAX.limit = 0;
                        SoftJointLimit HAX = new SoftJointLimit();
                        HAX.limit = 0;
                        SoftJointLimit Y = new SoftJointLimit();
                        Y.limit = 0;
                        SoftJointLimit Z = new SoftJointLimit();
                        Z.limit = 0;

                        confJoint.lowAngularXLimit = LAX;
                        confJoint.highAngularXLimit = HAX;
                        confJoint.angularYLimit = Y;
                        confJoint.angularZLimit = Z;
                    }
                    confJoint.rotationDriveMode = RotationDriveMode.Slerp;

                    confJoint.enablePreprocessing = false;
                    confJoint.projectionDistance = 0.1f;
                    confJoint.projectionAngle = 180f;
                    confJoint.projectionMode = JointProjectionMode.PositionAndRotation;


                    JointDrive x = confJoint.slerpDrive;
                    x.positionSpring = 0;
                    x.positionDamper = 0;
                    x.maximumForce = 0;
                    confJoint.slerpDrive = x;

                }

            }

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
