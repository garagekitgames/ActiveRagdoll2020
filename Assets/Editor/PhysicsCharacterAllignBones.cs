using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System;

public class PhysicsCharacterAllignBones : EditorWindow
{
	//Editor window
	public Texture tex;
	private static PhysicsCharacterAllignBones _instance;

	//Container
	private GameObject refContainer;
	private GameObject Container;

	//COMP
	private GameObject refCOMP;
	private GameObject refRotateTarget;

	//Root
	private GameObject refRoot;
	private GameObject RootChildren;
	private GameObject Root;

	//Body
	private GameObject refBody;
	private GameObject BodyChildren;
	private GameObject Body;

	//Head
	private GameObject refHead;
	private GameObject HeadChildren;
	private GameObject Head;

	//UpperRightArm
	private GameObject refUpperRightArm;
	private GameObject UpperRightArmChildren;
	private GameObject UpperRightArm;

	//LowerRightArm
	private GameObject refLowerRightArm;
	private GameObject LowerRightArmChildren;
	private GameObject LowerRightArm;

	//RightHand
	private GameObject refRightHand;
	private GameObject RightHandChildren;
	private GameObject RightHand;

	//UpperLeftArm
	private GameObject refUpperLeftArm;
	private GameObject UpperLeftArmChildren;
	private GameObject UpperLeftArm;

	//LowerLeftArm
	private GameObject refLowerLeftArm;
	private GameObject LowerLeftArmChildren;
	private GameObject LowerLeftArm;

	//RightHand
	private GameObject refLeftHand;
	private GameObject LeftHandChildren;
	private GameObject LeftHand;

	//UpperRightLeg
	private GameObject refUpperRightLeg;
	private GameObject UpperRightLegChildren;
	private GameObject UpperRightLeg;

	//LowerRightLeg
	private GameObject refLowerRightLeg;
	private GameObject LowerRightLegChildren;
	private GameObject LowerRightLeg;

	//RightFoot
	private GameObject refRightFoot;
	private GameObject RightFootChildren;
	private GameObject RightFoot;

	//UpperLeftLeg
	private GameObject refUpperLeftLeg;
	private GameObject UpperLeftLegChildren;
	private GameObject UpperLeftLeg;

	//LowerLeftLeg
	private GameObject refLowerLeftLeg;
	private GameObject LowerLeftLegChildren;
	private GameObject LowerLeftLeg;

	//LeftFoot
	private GameObject refLeftFoot;
	private GameObject LeftFootChildren;
	private GameObject LeftFoot;

	[MenuItem("Physics Character/Physics Character Alligner")]
	static void PhysicsCharacterAllignBonesWindow()
	{
		if (_instance == null)
		{
			PhysicsCharacterAllignBones window = ScriptableObject.CreateInstance(typeof(PhysicsCharacterAllignBones)) as PhysicsCharacterAllignBones;
			window.maxSize = new Vector2(350f, 640f);
			window.minSize = window.maxSize;
			window.ShowUtility();
		}
	}

	void OnEnable()
	{
		_instance = this;
	}


	void TutorialLink()
	{
		Help.BrowseURL("https://www.youtube.com/watch?v=FRGplDfQgLE");
	}

	void OnGUI()
	{
		GUI.skin.label.wordWrap = true;
		GUILayout.ExpandWidth(false);

		EditorGUILayout.Space();
		GUILayout.Label(tex);

		EditorGUILayout.Space();
		GUILayout.Label("Import the APR_Player into the scene, align and scale it to fit your model, then link the respective bones of your model below");

		EditorGUILayout.Space();
		GUILayout.Label("Note: The APR_Player box model will represent your colliders as well");

		EditorGUILayout.Space();
		GUILayout.Label("Here is a tutorial video link of this process:");
		if (GUILayout.Button("Watch Tutorial Video"))
		{
			TutorialLink();
		}



		//New Model fields
		EditorGUILayout.Space();
		EditorGUILayout.Space();

		//Container
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Model Container");
		Container = (GameObject)EditorGUILayout.ObjectField(Container, typeof(GameObject), true, GUILayout.Width(180));
		EditorGUILayout.EndHorizontal();
		//Root
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Hip Bone");
		Root = (GameObject)EditorGUILayout.ObjectField(Root, typeof(GameObject), true, GUILayout.Width(180));
		EditorGUILayout.EndHorizontal();
		//body
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Chest Bone");
		Body = (GameObject)EditorGUILayout.ObjectField(Body, typeof(GameObject), true, GUILayout.Width(180));
		EditorGUILayout.EndHorizontal();
		//Head
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Head Bone");
		Head = (GameObject)EditorGUILayout.ObjectField(Head, typeof(GameObject), true, GUILayout.Width(180));
		EditorGUILayout.EndHorizontal();
		//UpperRightArm
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Upper Right Arm Bone");
		UpperRightArm = (GameObject)EditorGUILayout.ObjectField(UpperRightArm, typeof(GameObject), true, GUILayout.Width(180));
		EditorGUILayout.EndHorizontal();
		//LowerRightArm
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Lower Right Arm Bone");
		LowerRightArm = (GameObject)EditorGUILayout.ObjectField(LowerRightArm, typeof(GameObject), true, GUILayout.Width(180));
		EditorGUILayout.EndHorizontal();
		//RightHand
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Right Hand Bone");
		RightHand = (GameObject)EditorGUILayout.ObjectField(RightHand, typeof(GameObject), true, GUILayout.Width(180));
		EditorGUILayout.EndHorizontal();
		//UpperLeftArm
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Upper Left Arm Bone");
		UpperLeftArm = (GameObject)EditorGUILayout.ObjectField(UpperLeftArm, typeof(GameObject), true, GUILayout.Width(180));
		EditorGUILayout.EndHorizontal();
		//LowerLeftArm
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Lower Left Arm Bone");
		LowerLeftArm = (GameObject)EditorGUILayout.ObjectField(LowerLeftArm, typeof(GameObject), true, GUILayout.Width(180));
		EditorGUILayout.EndHorizontal();
		//LeftHand
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Left Hand Bone");
		LeftHand = (GameObject)EditorGUILayout.ObjectField(LeftHand, typeof(GameObject), true, GUILayout.Width(180));
		EditorGUILayout.EndHorizontal();
		//UpperRightLeg
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Upper Right Leg Bone");
		UpperRightLeg = (GameObject)EditorGUILayout.ObjectField(UpperRightLeg, typeof(GameObject), true, GUILayout.Width(180));
		EditorGUILayout.EndHorizontal();
		//LowerRightLeg
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Lower Right Leg Bone");
		LowerRightLeg = (GameObject)EditorGUILayout.ObjectField(LowerRightLeg, typeof(GameObject), true, GUILayout.Width(180));
		EditorGUILayout.EndHorizontal();
		//RightFoot
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Right Foot Bone");
		RightFoot = (GameObject)EditorGUILayout.ObjectField(RightFoot, typeof(GameObject), true, GUILayout.Width(180));
		EditorGUILayout.EndHorizontal();
		//UpperLeftLeg
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Upper Left Leg Bone");
		UpperLeftLeg = (GameObject)EditorGUILayout.ObjectField(UpperLeftLeg, typeof(GameObject), true, GUILayout.Width(180));
		EditorGUILayout.EndHorizontal();
		//LowerLeftLeg
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Lower Left Leg Bone");
		LowerLeftLeg = (GameObject)EditorGUILayout.ObjectField(LowerLeftLeg, typeof(GameObject), true, GUILayout.Width(180));
		EditorGUILayout.EndHorizontal();
		//LeftFoot
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Left Foot Bone");
		LeftFoot = (GameObject)EditorGUILayout.ObjectField(LeftFoot, typeof(GameObject), true, GUILayout.Width(180));
		EditorGUILayout.EndHorizontal();

		//Button
		EditorGUILayout.Space();
		EditorGUILayout.Space();

		if (GUILayout.Button("Bind Physics Character"))
		{
			BindRagdoll();
		}
	}

	private static Transform GetLongestTransform(Transform limb)
	{
		float longestF = -1;
		Transform longestT = null;

		// find the farest object that attached to 'limb'
		foreach (Transform t in limb.GetComponentsInChildren<Transform>())
		{
			float length = (limb.position - t.position).sqrMagnitude;
			if (length > longestF)
			{
				longestF = length;
				longestT = t;
			}
		}

		return longestT;
	}

	static Vector3 GetXyzDirectionV(Vector3 node)
	{
		var d = GetXyzDirection(node);

		switch (d)
		{
			case 0: return Vector3.right;
			case 1: return Vector3.up;
			case 2: return Vector3.forward;
		}

		throw new InvalidOperationException();
	}

	/// <summary>
	/// Get the most appropriate direction in terms of PhysX (0,1,2 directions)
	/// </summary>
	static int GetXyzDirection(Vector3 node)
	{
		float x = Mathf.Abs(node.x);
		float y = Mathf.Abs(node.y);
		float z = Mathf.Abs(node.z);

		if (x > y & x > z)      // x is the bigest
			return 0;
		if (y > x & y > z)      // y is the bigest
			return 1;

		// z is the bigest
		return 2;
	}

	private Vector3 Abs(Vector3 v)
	{
		return new Vector3(
			Mathf.Abs(v.x),
			Mathf.Abs(v.y),
			Mathf.Abs(v.z)
			);
	}

	void BindRagdoll()
	{
		//Find active physics ragdoll player
		refContainer = GameObject.Find("PhysicsCharacter_GangBeasts_v3");

		if (PrefabUtility.GetPrefabInstanceStatus(refContainer) != PrefabInstanceStatus.NotAPrefab)
		{
			PrefabUtility.UnpackPrefabInstance(refContainer, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);
		}

		if (PrefabUtility.GetPrefabInstanceStatus(Container) != PrefabInstanceStatus.NotAPrefab)
		{
			PrefabUtility.UnpackPrefabInstance(Container, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);
		}

		refRoot = GameObject.Find("PCHR_Hip");
		refBody = GameObject.Find("PCHR_Chest");
		refHead = GameObject.Find("PCHR_Head");
		refUpperRightArm = GameObject.Find("PCHR_Arm_R");
		refLowerRightArm = GameObject.Find("PCHR_Elbow_R");
		refRightHand = GameObject.Find("PCHR_Hand_R");
		refUpperLeftArm = GameObject.Find("PCHR_Arm_L");
		refLowerLeftArm = GameObject.Find("PCHR_Elbow_L");
		refLeftHand = GameObject.Find("PCHR_Hand_L");
		refUpperRightLeg = GameObject.Find("PCHR_Thigh_R");
		refLowerRightLeg = GameObject.Find("PCHR_Leg_R");
		refRightFoot = GameObject.Find("PCHR_Foot_R");
		refUpperLeftLeg = GameObject.Find("PCHR_Thigh_L");
		refLowerLeftLeg = GameObject.Find("PCHR_Leg_L");
		refLeftFoot = GameObject.Find("PCHR_Foot_L");

		refRotateTarget = GameObject.Find("PCHR_RotateTarget");
		//refCOMP = GameObject.Find("APR_COMP");



		//Root
		refRoot.transform.position = Root.transform.position;
		Vector3 dir = Body.transform.position - Root.transform.position;
		Quaternion XLookRotation = Quaternion.LookRotation(dir, refRoot.transform.up) * Quaternion.Euler(new Vector3(0, 90, 0));
		refRoot.transform.rotation = XLookRotation;

        RootChildren = Root.transform.gameObject;
        refRoot.transform.parent = Root.transform.parent;
		if (refRotateTarget)
		{
			refRotateTarget.transform.parent = Root.transform.parent;
			refRotateTarget.transform.position = refRoot.transform.position;
			refRotateTarget.transform.rotation = refRoot.transform.rotation;
			refRotateTarget.transform.localScale = refRoot.transform.localScale;
		}
		RootChildren.transform.parent = refRoot.transform;
        DestroyImmediate(refRoot.transform.GetChild(0).gameObject);
        DestroyImmediate(refRoot.GetComponent<MeshFilter>());


		
		
		//Vector3 pelvisSize = new Vector3(0.32f, 0.31f, 0.3f);
		//Vector3 pelvisCenter = new Vector3(00f, 0.06f, -0.01f);
		//refRoot.transform.GetComponent<BoxCollider>().size = Abs(refRoot.transform.InverseTransformVector(pelvisSize));
		//refRoot.transform.GetComponent<BoxCollider>().center = refRoot.transform.InverseTransformVector(pelvisCenter);





		////Body
		///
		refBody.transform.position = Body.transform.position;
		dir = Head.transform.position - Body.transform.position;
		Quaternion XLookRotation1 = Quaternion.LookRotation(dir, refBody.transform.up) * Quaternion.Euler(new Vector3(0, 90, 0));
		refBody.transform.rotation = XLookRotation1;

		BodyChildren = Body.transform.gameObject;
        refBody.transform.parent = Body.transform.parent;
        BodyChildren.transform.parent = refBody.transform;
        DestroyImmediate(refBody.transform.GetChild(0).gameObject);
        DestroyImmediate(refBody.GetComponent<MeshFilter>());

		// Chest collider
		//Vector3 chestSize = new Vector3(0.34f, 0.34f, 0.28f);

		//float y = (pelvisSize.y + chestSize.y) / 2f + pelvisCenter.y;
		//y -= refBody.transform.position.y - refRoot.transform.position.y;
		//refBody.transform.GetComponent<BoxCollider>().size = Abs(refBody.transform.InverseTransformVector(chestSize));
		//refBody.transform.GetComponent<BoxCollider>().center = refBody.transform.InverseTransformVector(new Vector3(0f, y, -0.03f));



		////Head
		refHead.transform.position = Head.transform.position;
		dir = Vector3.up;
		Quaternion XLookRotation2 = Quaternion.LookRotation(dir, refHead.transform.up) * Quaternion.Euler(new Vector3(0, 90, 0));
		refBody.transform.rotation = XLookRotation2;

        HeadChildren = Head.transform.gameObject;
        refHead.transform.parent = Head.transform.parent;
        HeadChildren.transform.parent = refHead.transform;
        DestroyImmediate(refHead.transform.GetChild(0).gameObject);
        DestroyImmediate(refHead.GetComponent<MeshFilter>());

		// head
		//refHead.transform.GetComponent<SphereCollider>().radius = 0.1f;
		//refHead.transform.GetComponent<SphereCollider>().center = refHead.transform.InverseTransformVector(new Vector3(0f, 0.09f, 0.03f));


		////UpperRightArm

		refUpperRightArm.transform.position = UpperRightArm.transform.position;
		dir = LowerRightArm.transform.position - UpperRightArm.transform.position;
		Quaternion XLookRotation3 = Quaternion.LookRotation(dir, refUpperRightArm.transform.forward) * Quaternion.Euler(new Vector3(-90, 0, 0));
		refUpperRightArm.transform.rotation = XLookRotation3;


        UpperRightArmChildren = UpperRightArm.transform.gameObject;
        refUpperRightArm.transform.parent = UpperRightArm.transform.parent;
        UpperRightArmChildren.transform.parent = refUpperRightArm.transform;
        DestroyImmediate(refUpperRightArm.transform.GetChild(0).gameObject);
        DestroyImmediate(refUpperRightArm.GetComponent<MeshFilter>());


		////LowerRightArm

		refLowerRightArm.transform.position = LowerRightArm.transform.position;
		dir = RightHand.transform.position - LowerRightArm.transform.position;
		Quaternion XLookRotation4 = Quaternion.LookRotation(dir, refLowerRightArm.transform.forward) * Quaternion.Euler(new Vector3(-90, -0, 0));
		refLowerRightArm.transform.rotation = XLookRotation4;

        LowerRightArmChildren = LowerRightArm.transform.gameObject;
        refLowerRightArm.transform.parent = LowerRightArm.transform.parent;
        LowerRightArmChildren.transform.parent = refLowerRightArm.transform;
        DestroyImmediate(refLowerRightArm.transform.GetChild(0).gameObject);
        DestroyImmediate(refLowerRightArm.GetComponent<MeshFilter>());

        refRightHand.transform.position = RightHand.transform.position;
		if(RightHand.transform.childCount > 0)
        {
			Debug.Log("Found Child! : "+ RightHand.transform.GetChild(0).name);
            dir = RightHand.transform.GetChild(0).gameObject.transform.position - RightHand.transform.position;
			Quaternion XLookRotation5 = Quaternion.LookRotation(dir, refRightHand.transform.forward) * Quaternion.Euler(new Vector3(-90, -0, 0));
			refRightHand.transform.rotation = XLookRotation5;
        }


		//CapsuleSizeChange 
		float totalLength = UpperRightArm.transform.InverseTransformPoint(RightHand.transform.position).magnitude;
		CapsuleCollider upperCapsule = refUpperRightArm.GetComponent<CapsuleCollider>();
		var boneEndPos = UpperRightArm.transform.InverseTransformPoint(LowerRightArm.transform.position);
		//upperCapsule.direction = Vector3.left;
		upperCapsule.radius = totalLength * 0.12f;
		upperCapsule.height = boneEndPos.magnitude;
		upperCapsule.center = new Vector3(0, -boneEndPos.magnitude / 2, 0);

		// limbLower
		CapsuleCollider endCapsule = refLowerRightArm.GetComponent<CapsuleCollider>();
		boneEndPos = LowerRightArm.transform.InverseTransformPoint(RightHand.transform.position);
		//endCapsule.direction = GetXyzDirection(boneEndPos);
		endCapsule.radius = totalLength * 0.12f;
		endCapsule.height = boneEndPos.magnitude;
		endCapsule.center = new Vector3(0, -boneEndPos.magnitude / 2, 0);


        ////RightHand
        RightHandChildren = RightHand.transform.gameObject;
        refRightHand.transform.parent = RightHand.transform.parent;
        RightHandChildren.transform.parent = refRightHand.transform;
        DestroyImmediate(refRightHand.transform.GetChild(0).gameObject);
        DestroyImmediate(refRightHand.GetComponent<MeshFilter>());


		////UpperLeftArm
		refUpperLeftArm.transform.position = UpperLeftArm.transform.position;
		dir = LowerLeftArm.transform.position - UpperLeftArm.transform.position;
		Quaternion XLookRotation6 = Quaternion.LookRotation(dir, refUpperLeftArm.transform.forward) * Quaternion.Euler(new Vector3(-90, 0, 0));
		refUpperLeftArm.transform.rotation = XLookRotation6;

        UpperLeftArmChildren = UpperLeftArm.transform.gameObject;
        refUpperLeftArm.transform.parent = UpperLeftArm.transform.parent;
        UpperLeftArmChildren.transform.parent = refUpperLeftArm.transform;
        DestroyImmediate(refUpperLeftArm.transform.GetChild(0).gameObject);
        DestroyImmediate(refUpperLeftArm.GetComponent<MeshFilter>());


		////LowerLeftArm

		refLowerLeftArm.transform.position = LowerLeftArm.transform.position;
		dir = LeftHand.transform.position - LowerLeftArm.transform.position;
		Quaternion XLookRotation7 = Quaternion.LookRotation(dir, refLowerLeftArm.transform.forward) * Quaternion.Euler(new Vector3(-90, 0, 0));
		refLowerLeftArm.transform.rotation = XLookRotation7;

        LowerLeftArmChildren = LowerLeftArm.transform.gameObject;
        refLowerLeftArm.transform.parent = LowerLeftArm.transform.parent;
        LowerLeftArmChildren.transform.parent = refLowerLeftArm.transform;
        DestroyImmediate(refLowerLeftArm.transform.GetChild(0).gameObject);
        DestroyImmediate(refLowerLeftArm.GetComponent<MeshFilter>());

        refLeftHand.transform.position = LeftHand.transform.position;
		if (LeftHand.transform.childCount > 0)
		{
			Debug.Log("Found Child! : " + LeftHand.transform.GetChild(0).name);
			dir = LeftHand.transform.GetChild(0).gameObject.transform.position - LeftHand.transform.position;
			Quaternion XLookRotation8 = Quaternion.LookRotation(dir, refLeftHand.transform.forward) * Quaternion.Euler(new Vector3(-90, 0, 0));
			refLeftHand.transform.rotation = XLookRotation8;
		}



		//CapsuleSizeChange 
		float totalLength2 = UpperLeftArm.transform.InverseTransformPoint(LeftHand.transform.position).magnitude;
		CapsuleCollider upperCapsule2 = refUpperLeftArm.GetComponent<CapsuleCollider>();
		var boneEndPos2 = UpperLeftArm.transform.InverseTransformPoint(LowerLeftArm.transform.position);
		//upperCapsule.direction = Vector3.left;
		upperCapsule2.radius = totalLength2 * 0.12f;
		upperCapsule2.height = boneEndPos2.magnitude;
		upperCapsule2.center = new Vector3(0, -boneEndPos2.magnitude / 2, 0);

		// limbLower
		CapsuleCollider endCapsule2 = refLowerLeftArm.GetComponent<CapsuleCollider>();
		boneEndPos2 = LowerLeftArm.transform.InverseTransformPoint(LeftHand.transform.position);
		//endCapsule.direction = GetXyzDirection(boneEndPos);
		endCapsule2.radius = totalLength2 * 0.12f;
		endCapsule2.height = boneEndPos2.magnitude;
		endCapsule2.center = new Vector3(0, -boneEndPos2.magnitude / 2, 0);
        ////LeftHand
        LeftHandChildren = LeftHand.transform.gameObject;
        refLeftHand.transform.parent = LeftHand.transform.parent;
        LeftHandChildren.transform.parent = refLeftHand.transform;
        DestroyImmediate(refLeftHand.transform.GetChild(0).gameObject);
        DestroyImmediate(refLeftHand.GetComponent<MeshFilter>());



		////UpperRightLeg
		refUpperRightLeg.transform.position = UpperRightLeg.transform.position;
		dir = LowerRightLeg.transform.position - UpperRightLeg.transform.position;
		Quaternion XLookRotation9 = Quaternion.LookRotation(dir, refUpperRightLeg.transform.up) * Quaternion.Euler(new Vector3(0, 90, 0));
		refUpperRightLeg.transform.rotation = XLookRotation9;
		refUpperRightLeg.transform.localScale = Vector3.one;

		UpperRightLegChildren = UpperRightLeg.transform.gameObject;
        refUpperRightLeg.transform.parent = UpperRightLeg.transform.parent;
        UpperRightLegChildren.transform.parent = refUpperRightLeg.transform;
        DestroyImmediate(refUpperRightLeg.transform.GetChild(0).gameObject);
        DestroyImmediate(refUpperRightLeg.GetComponent<MeshFilter>());



		////LowerRightLeg

		refLowerRightLeg.transform.position = LowerRightLeg.transform.position;
		dir = RightFoot.transform.position - LowerRightLeg.transform.position;
		Quaternion XLookRotation10 = Quaternion.LookRotation(dir, refLowerRightLeg.transform.up) * Quaternion.Euler(new Vector3(0, 90, 0));
		refLowerRightLeg.transform.rotation = XLookRotation10;
		refLowerRightLeg.transform.localScale = Vector3.one;

		LowerRightLegChildren = LowerRightLeg.transform.gameObject;
        refLowerRightLeg.transform.parent = LowerRightLeg.transform.parent;
        LowerRightLegChildren.transform.parent = refLowerRightLeg.transform;
        DestroyImmediate(refLowerRightLeg.transform.GetChild(0).gameObject);
        DestroyImmediate(refLowerRightLeg.GetComponent<MeshFilter>());


        refRightFoot.transform.position = RightFoot.transform.position;
		refRightFoot.transform.localScale = Vector3.one;
		//if (RightFoot.transform.childCount > 0)
		//{
		//	Debug.Log("Found Child! : " + RightFoot.transform.GetChild(0).name);
		//	dir = RightFoot.transform.GetChild(0).gameObject.transform.position - RightFoot.transform.position;
		//	Quaternion XLookRotation11 = Quaternion.LookRotation(dir, refRightFoot.transform.up) * Quaternion.Euler(new Vector3(0, 90, 0));
		//	refRightFoot.transform.rotation = XLookRotation11;

		//}



		//CapsuleSizeChange 
		float totalLength3 = UpperRightLeg.transform.InverseTransformPoint(RightFoot.transform.position).magnitude;
		CapsuleCollider upperCapsule3 = refUpperRightLeg.GetComponent<CapsuleCollider>();
		var boneEndPos3 = UpperRightLeg.transform.InverseTransformPoint(LowerRightLeg.transform.position);
		//upperCapsule.direction = Vector3.left;
		upperCapsule3.radius = totalLength3 * 0.12f;
		upperCapsule3.height = boneEndPos3.magnitude;
		upperCapsule3.center = new Vector3(-boneEndPos3.magnitude / 2, 0, 0);

		// limbLower
		CapsuleCollider endCapsule3 = refLowerRightLeg.GetComponent<CapsuleCollider>();
		boneEndPos3 = LowerRightLeg.transform.InverseTransformPoint(RightFoot.transform.position);
		//endCapsule.direction = GetXyzDirection(boneEndPos);
		endCapsule3.radius = totalLength3 * 0.12f;
		endCapsule3.height = boneEndPos3.magnitude;
		endCapsule3.center = new Vector3(-boneEndPos3.magnitude / 2, 0, 0);
        ////RightFoot
        RightFootChildren = RightFoot.transform.gameObject;
        refRightFoot.transform.parent = RightFoot.transform.parent;
        RightFootChildren.transform.parent = refRightFoot.transform;
        DestroyImmediate(refRightFoot.transform.GetChild(0).gameObject);
        DestroyImmediate(refRightFoot.GetComponent<MeshFilter>());



		////UpperLeftLeg
		refUpperLeftLeg.transform.position = UpperLeftLeg.transform.position;
		dir = LowerLeftLeg.transform.position - UpperLeftLeg.transform.position;
		Quaternion XLookRotation12 = Quaternion.LookRotation(dir, refUpperLeftLeg.transform.up) * Quaternion.Euler(new Vector3(0, 90, 0));
		refUpperLeftLeg.transform.rotation = XLookRotation12;
		refUpperLeftLeg.transform.localScale = Vector3.one;

		UpperLeftLegChildren = UpperLeftLeg.transform.gameObject;
        refUpperLeftLeg.transform.parent = UpperLeftLeg.transform.parent;
        UpperLeftLegChildren.transform.parent = refUpperLeftLeg.transform;
        DestroyImmediate(refUpperLeftLeg.transform.GetChild(0).gameObject);
        DestroyImmediate(refUpperLeftLeg.GetComponent<MeshFilter>());




		////LowerLeftLeg

		refLowerLeftLeg.transform.position = LowerLeftLeg.transform.position;
		dir = LeftFoot.transform.position - LowerLeftLeg.transform.position;
		Quaternion XLookRotation13 = Quaternion.LookRotation(dir, refLowerLeftLeg.transform.up) * Quaternion.Euler(new Vector3(0, 90, 0));
		refLowerLeftLeg.transform.rotation = XLookRotation13;
		refLowerLeftLeg.transform.localScale = Vector3.one;

		LowerLeftLegChildren = LowerLeftLeg.transform.gameObject;
        refLowerLeftLeg.transform.parent = LowerLeftLeg.transform.parent;
        LowerLeftLegChildren.transform.parent = refLowerLeftLeg.transform;
        DestroyImmediate(refLowerLeftLeg.transform.GetChild(0).gameObject);
        DestroyImmediate(refLowerLeftLeg.GetComponent<MeshFilter>());


        refLeftFoot.transform.position = LeftFoot.transform.position;
		refLeftFoot.transform.localScale = Vector3.one;
		//if (LeftFoot.transform.childCount > 0)
		//{
		//	Debug.Log("Found Child! : " + LeftFoot.transform.GetChild(0).name);
		//	dir = LeftFoot.transform.GetChild(0).gameObject.transform.position - LeftFoot.transform.position;
		//	Quaternion XLookRotation14 = Quaternion.LookRotation(dir, refLeftFoot.transform.up) * Quaternion.Euler(new Vector3(0, 90, 0));
		//	refLeftFoot.transform.rotation = XLookRotation14;
		//}



		//CapsuleSizeChange 
		float totalLength4 = UpperLeftLeg.transform.InverseTransformPoint(LeftFoot.transform.position).magnitude;
		CapsuleCollider upperCapsule4 = refUpperLeftLeg.GetComponent<CapsuleCollider>();
		var boneEndPos4 = UpperLeftLeg.transform.InverseTransformPoint(LowerLeftLeg.transform.position);
		//upperCapsule.direction = Vector3.left;
		upperCapsule4.radius = totalLength4 * 0.12f;
		upperCapsule4.height = boneEndPos4.magnitude;
		upperCapsule4.center = new Vector3(-boneEndPos4.magnitude / 2, 0, 0);

		// limbLower
		CapsuleCollider endCapsule4 = refLowerLeftLeg.GetComponent<CapsuleCollider>();
		boneEndPos4 = LowerLeftLeg.transform.InverseTransformPoint(LeftFoot.transform.position);
		//endCapsule.direction = GetXyzDirection(boneEndPos);
		endCapsule4.radius = totalLength4 * 0.12f;
		endCapsule4.height = boneEndPos4.magnitude;
		endCapsule4.center = new Vector3(-boneEndPos4.magnitude / 2, 0, 0);


        ////LeftFoot
        LeftFootChildren = LeftFoot.transform.gameObject;
        refLeftFoot.transform.parent = LeftFoot.transform.parent;
        LeftFootChildren.transform.parent = refLeftFoot.transform;
        DestroyImmediate(refLeftFoot.transform.GetChild(0).gameObject);
        DestroyImmediate(refLeftFoot.GetComponent<MeshFilter>());

        ////COMP
        ////refCOMP.transform.parent = Root.transform.root;

        ////Container
        Container.transform.parent = refContainer.transform;

        Debug.Log("Physics Character has been binded");

		this.Close();
	}

	void OnDisable()
	{
		_instance = null;
	}
}
