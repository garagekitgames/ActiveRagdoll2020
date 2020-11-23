using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PhysicsCharacterBinder : EditorWindow
{
	//Editor window
	public Texture tex;
	private static PhysicsCharacterBinder _instance;

	//Container
	private GameObject refContainer;
	private GameObject Container;

	//COMP
	private GameObject refCOMP;

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

	[MenuItem("Physics Character/Physics Character Binder")]
	static void PhysicsCharacterBinderWindow()
	{
		if (_instance == null)
		{
			PhysicsCharacterBinder window = ScriptableObject.CreateInstance(typeof(PhysicsCharacterBinder)) as PhysicsCharacterBinder;
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

	void BindRagdoll()
	{
		//Find active physics ragdoll player
		refContainer = GameObject.Find("PhysicsCharacter_Base");

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
		//refCOMP = GameObject.Find("APR_COMP");


		//Root
		RootChildren = Root.transform.gameObject;
		refRoot.transform.parent = Root.transform.parent;
		RootChildren.transform.parent = refRoot.transform;
		DestroyImmediate(refRoot.transform.GetChild(0).gameObject);
		DestroyImmediate(refRoot.GetComponent<MeshFilter>());

		//Body
		BodyChildren = Body.transform.gameObject;
		refBody.transform.parent = Body.transform.parent;
		BodyChildren.transform.parent = refBody.transform;
		DestroyImmediate(refBody.transform.GetChild(0).gameObject);
		DestroyImmediate(refBody.GetComponent<MeshFilter>());

		//Head
		HeadChildren = Head.transform.gameObject;
		refHead.transform.parent = Head.transform.parent;
		HeadChildren.transform.parent = refHead.transform;
		DestroyImmediate(refHead.transform.GetChild(0).gameObject);
		DestroyImmediate(refHead.GetComponent<MeshFilter>());

		//UpperRightArm
		UpperRightArmChildren = UpperRightArm.transform.gameObject;
		refUpperRightArm.transform.parent = UpperRightArm.transform.parent;
		UpperRightArmChildren.transform.parent = refUpperRightArm.transform;
		DestroyImmediate(refUpperRightArm.transform.GetChild(0).gameObject);
		DestroyImmediate(refUpperRightArm.GetComponent<MeshFilter>());

		//LowerRightArm
		LowerRightArmChildren = LowerRightArm.transform.gameObject;
		refLowerRightArm.transform.parent = LowerRightArm.transform.parent;
		LowerRightArmChildren.transform.parent = refLowerRightArm.transform;
		DestroyImmediate(refLowerRightArm.transform.GetChild(0).gameObject);
		DestroyImmediate(refLowerRightArm.GetComponent<MeshFilter>());

		//RightHand
		RightHandChildren = RightHand.transform.gameObject;
		refRightHand.transform.parent = RightHand.transform.parent;
		RightHandChildren.transform.parent = refRightHand.transform;
		DestroyImmediate(refRightHand.transform.GetChild(0).gameObject);
		DestroyImmediate(refRightHand.GetComponent<MeshFilter>());

		//UpperLeftArm
		UpperLeftArmChildren = UpperLeftArm.transform.gameObject;
		refUpperLeftArm.transform.parent = UpperLeftArm.transform.parent;
		UpperLeftArmChildren.transform.parent = refUpperLeftArm.transform;
		DestroyImmediate(refUpperLeftArm.transform.GetChild(0).gameObject);
		DestroyImmediate(refUpperLeftArm.GetComponent<MeshFilter>());

		//LowerLeftArm
		LowerLeftArmChildren = LowerLeftArm.transform.gameObject;
		refLowerLeftArm.transform.parent = LowerLeftArm.transform.parent;
		LowerLeftArmChildren.transform.parent = refLowerLeftArm.transform;
		DestroyImmediate(refLowerLeftArm.transform.GetChild(0).gameObject);
		DestroyImmediate(refLowerLeftArm.GetComponent<MeshFilter>());

		//LeftHand
		LeftHandChildren = LeftHand.transform.gameObject;
		refLeftHand.transform.parent = LeftHand.transform.parent;
		LeftHandChildren.transform.parent = refLeftHand.transform;
		DestroyImmediate(refLeftHand.transform.GetChild(0).gameObject);
		DestroyImmediate(refLeftHand.GetComponent<MeshFilter>());

		//UpperRightLeg
		UpperRightLegChildren = UpperRightLeg.transform.gameObject;
		refUpperRightLeg.transform.parent = UpperRightLeg.transform.parent;
		UpperRightLegChildren.transform.parent = refUpperRightLeg.transform;
		DestroyImmediate(refUpperRightLeg.transform.GetChild(0).gameObject);
		DestroyImmediate(refUpperRightLeg.GetComponent<MeshFilter>());

		//LowerRightLeg
		LowerRightLegChildren = LowerRightLeg.transform.gameObject;
		refLowerRightLeg.transform.parent = LowerRightLeg.transform.parent;
		LowerRightLegChildren.transform.parent = refLowerRightLeg.transform;
		DestroyImmediate(refLowerRightLeg.transform.GetChild(0).gameObject);
		DestroyImmediate(refLowerRightLeg.GetComponent<MeshFilter>());

		//RightFoot
		RightFootChildren = RightFoot.transform.gameObject;
		refRightFoot.transform.parent = RightFoot.transform.parent;
		RightFootChildren.transform.parent = refRightFoot.transform;
		DestroyImmediate(refRightFoot.transform.GetChild(0).gameObject);
		DestroyImmediate(refRightFoot.GetComponent<MeshFilter>());

		//UpperLeftLeg
		UpperLeftLegChildren = UpperLeftLeg.transform.gameObject;
		refUpperLeftLeg.transform.parent = UpperLeftLeg.transform.parent;
		UpperLeftLegChildren.transform.parent = refUpperLeftLeg.transform;
		DestroyImmediate(refUpperLeftLeg.transform.GetChild(0).gameObject);
		DestroyImmediate(refUpperLeftLeg.GetComponent<MeshFilter>());

		//LowerLeftLeg
		LowerLeftLegChildren = LowerLeftLeg.transform.gameObject;
		refLowerLeftLeg.transform.parent = LowerLeftLeg.transform.parent;
		LowerLeftLegChildren.transform.parent = refLowerLeftLeg.transform;
		DestroyImmediate(refLowerLeftLeg.transform.GetChild(0).gameObject);
		DestroyImmediate(refLowerLeftLeg.GetComponent<MeshFilter>());

		//LeftFoot
		LeftFootChildren = LeftFoot.transform.gameObject;
		refLeftFoot.transform.parent = LeftFoot.transform.parent;
		LeftFootChildren.transform.parent = refLeftFoot.transform;
		DestroyImmediate(refLeftFoot.transform.GetChild(0).gameObject);
		DestroyImmediate(refLeftFoot.GetComponent<MeshFilter>());

		//COMP
		//refCOMP.transform.parent = Root.transform.root;

		//Container
		Container.transform.parent = refContainer.transform;

		Debug.Log("Physics Character has been binded");

		this.Close();
	}

	void OnDisable()
	{
		_instance = null;
	}
}
