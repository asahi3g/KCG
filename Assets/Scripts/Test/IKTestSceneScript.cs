using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class IKTestSceneScript : MonoBehaviour
{
    GameObject Player;

    
    void Start()
    {
        GameObject prefab = Engine3D.AssetManager.Singelton.GetModel(Engine3D.ModelType.Stander);
        Player = GameObject.Instantiate(prefab);

        Player.transform.position = new Vector3(transform.position.x, transform.position.y, -1.0f);
        Vector3 eulers = Player.transform.rotation.eulerAngles;
        Player.transform.rotation = Quaternion.Euler(0, 90, 0);

        Player.AddComponent<RigBuilder>();

        GameObject Rig = new GameObject("Rig 1");
        Rig.transform.parent = Player.transform;
        Rig.AddComponent<Rig>();

        RigLayer rigLayer = new RigLayer(Rig.GetComponent<Rig>(), true);

        Player.GetComponent<RigBuilder>().layers.Add(rigLayer);

        Player.AddComponent<BoneRenderer>();

        Player.GetComponent<BoneRenderer>().transforms = new Transform[52];

        Player.GetComponent<BoneRenderer>().transforms[0] = Player.transform.GetChild(0).GetChild(0); // Bip001 Pelvis

        Player.GetComponent<BoneRenderer>().transforms[1] = Player.transform.GetChild(0).GetChild(0).GetChild(0); // Bip001 L Thigh

        Player.GetComponent<BoneRenderer>().transforms[2] = Player.transform.GetChild(0).GetChild(0).GetChild(1); // Bip001 R Thigh

        Player.GetComponent<BoneRenderer>().transforms[3] = Player.transform.GetChild(0).GetChild(0).GetChild(2); // Bip001 Spine

        Player.GetComponent<BoneRenderer>().transforms[4] = Player.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).
            GetChild(1).GetChild(0); // Bip001 Head

        Player.GetComponent<BoneRenderer>().transforms[5] = Player.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).
            GetChild(1); // Bip001 Neck

        Player.GetComponent<BoneRenderer>().transforms[6] = Player.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0); // Bip001 Spine2

        Player.GetComponent<BoneRenderer>().transforms[7] = Player.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0); // Bip001 L Clavicle

        Player.GetComponent<BoneRenderer>().transforms[8] = Player.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2); // Bip001 R Clavicle

        Player.GetComponent<BoneRenderer>().transforms[9] = Player.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0); // Bip001 Spine1

        Player.GetComponent<BoneRenderer>().transforms[10] = Player.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).
            GetChild(0); // Bip001 R UpperArm

        Player.GetComponent<BoneRenderer>().transforms[11] = Player.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).
            GetChild(0).GetChild(0); // Bip001 R Forearm

        Player.GetComponent<BoneRenderer>().transforms[12] = Player.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).
            GetChild(0).GetChild(0).GetChild(0); // Bip001 R Hand

        Player.GetComponent<BoneRenderer>().transforms[13] = Player.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).
            GetChild(0).GetChild(0).GetChild(0).GetChild(0); // Bip001 R Finger0

        Player.GetComponent<BoneRenderer>().transforms[14] = Player.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).
            GetChild(0).GetChild(0).GetChild(0).GetChild(1); // Bip001 R Finger1

        Player.GetComponent<BoneRenderer>().transforms[15] = Player.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).
            GetChild(0).GetChild(0).GetChild(0).GetChild(2); // Bip001 R Finger2

        Player.GetComponent<BoneRenderer>().transforms[16] = Player.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).
            GetChild(0).GetChild(0).GetChild(0).GetChild(3); // Bip001 R Finger3

        Player.GetComponent<BoneRenderer>().transforms[17] = Player.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).
            GetChild(0).GetChild(0).GetChild(0).GetChild(4); // Bip001 R Finger4

        Player.GetComponent<BoneRenderer>().transforms[18] = Player.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).
            GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0); // Bip001 R Finger01

        Player.GetComponent<BoneRenderer>().transforms[19] = Player.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).
            GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0); // Bip001 R Finger02

        Player.GetComponent<BoneRenderer>().transforms[20] = Player.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).
            GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetChild(0); // Bip001 R Finger11

        Player.GetComponent<BoneRenderer>().transforms[21] = Player.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).
            GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0); // Bip001 R Finger12

        Player.GetComponent<BoneRenderer>().transforms[22] = Player.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).
            GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0); // Bip001 R Finger21

        Player.GetComponent<BoneRenderer>().transforms[23] = Player.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).
            GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0); // Bip001 R Finger22

        Player.GetComponent<BoneRenderer>().transforms[24] = Player.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).
            GetChild(0).GetChild(0).GetChild(0).GetChild(3).GetChild(0); // Bip001 R Finger31

        Player.GetComponent<BoneRenderer>().transforms[25] = Player.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).
            GetChild(0).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0); // Bip001 R Finger32

        Player.GetComponent<BoneRenderer>().transforms[26] = Player.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).
            GetChild(0).GetChild(0).GetChild(0).GetChild(4).GetChild(0); // Bip001 R Finger41

        Player.GetComponent<BoneRenderer>().transforms[27] = Player.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).
            GetChild(0).GetChild(0).GetChild(0).GetChild(4).GetChild(0).GetChild(0); // Bip001 R Finger42

        Player.GetComponent<BoneRenderer>().transforms[28] = Player.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0); // Bip001 R Foot

        Player.GetComponent<BoneRenderer>().transforms[29] = Player.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0); // Bip001 R Toe0

        Player.GetComponent<BoneRenderer>().transforms[30] = Player.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0); // Bip001 R Calf

        Player.GetComponent<BoneRenderer>().transforms[31] = Player.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0); // Bip001 L UpperArm

        Player.GetComponent<BoneRenderer>().transforms[32] = Player.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).
            GetChild(0).GetChild(0); // Bip001 L Forearm

        Player.GetComponent<BoneRenderer>().transforms[33] = Player.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).
            GetChild(0).GetChild(0).GetChild(0); // Bip001 L Hand

        Player.GetComponent<BoneRenderer>().transforms[34] = Player.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).
            GetChild(0).GetChild(0).GetChild(0).GetChild(0); // Bip001 L Finger0

        Player.GetComponent<BoneRenderer>().transforms[35] = Player.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).
            GetChild(0).GetChild(0).GetChild(0).GetChild(1); // Bip001 L Finger1

        Player.GetComponent<BoneRenderer>().transforms[36] = Player.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).
           GetChild(0).GetChild(0).GetChild(0).GetChild(2); // Bip001 L Finger2

        Player.GetComponent<BoneRenderer>().transforms[37] = Player.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).
           GetChild(0).GetChild(0).GetChild(0).GetChild(3); // Bip001 L Finger3

        Player.GetComponent<BoneRenderer>().transforms[38] = Player.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).
           GetChild(0).GetChild(0).GetChild(0).GetChild(4); // Bip001 L Finger4

        Player.GetComponent<BoneRenderer>().transforms[39] = Player.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).
            GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0); // Bip001 L Finger01

        Player.GetComponent<BoneRenderer>().transforms[40] = Player.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).
            GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0); // Bip001 L Finger02

        Player.GetComponent<BoneRenderer>().transforms[41] = Player.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).
            GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetChild(0); // Bip001 L Finger11

        Player.GetComponent<BoneRenderer>().transforms[42] = Player.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).
            GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0); // Bip001 L Finger12

        Player.GetComponent<BoneRenderer>().transforms[43] = Player.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).
            GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0); // Bip001 L Finger21

        Player.GetComponent<BoneRenderer>().transforms[44] = Player.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).
            GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0); // Bip001 L Finger22

        Player.GetComponent<BoneRenderer>().transforms[45] = Player.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).
           GetChild(0).GetChild(0).GetChild(0).GetChild(3).GetChild(0); // Bip001 L Finger31

        Player.GetComponent<BoneRenderer>().transforms[46] = Player.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).
           GetChild(0).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0); // Bip001 L Finger32

        Player.GetComponent<BoneRenderer>().transforms[47] = Player.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).
           GetChild(0).GetChild(0).GetChild(0).GetChild(4).GetChild(0); // Bip001 L Finger41

        Player.GetComponent<BoneRenderer>().transforms[48] = Player.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).
           GetChild(0).GetChild(0).GetChild(0).GetChild(4).GetChild(0).GetChild(0); // Bip001 L Finger42

        Player.GetComponent<BoneRenderer>().transforms[49] = Player.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0); // Bip001 L Foot

        Player.GetComponent<BoneRenderer>().transforms[50] = Player.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0); // Bip001 L Toe0

        Player.GetComponent<BoneRenderer>().transforms[51] = Player.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0); // Bip001 L Calf

        GameObject BodyAim = new GameObject("BodyAim");
        BodyAim.transform.parent = Rig.transform;
        BodyAim.AddComponent<MultiAimConstraint>();
        BodyAim.GetComponent<MultiAimConstraint>().weight = 1.0f;
        BodyAim.GetComponent<MultiAimConstraint>().data.constrainedObject = Player.transform.GetChild(0).GetChild(0).GetChild(2);
        BodyAim.GetComponent<MultiAimConstraint>().data.aimAxis = MultiAimConstraintData.Axis.Y;
        BodyAim.GetComponent<MultiAimConstraint>().data.upAxis = MultiAimConstraintData.Axis.X_NEG;
        GameObject emptyGO = new GameObject("Target");
        Transform Target = emptyGO.transform;
        Target.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        var Bodydata = BodyAim.GetComponent<MultiAimConstraint>().data.sourceObjects;
        Bodydata = new WeightedTransformArray(1);
        Bodydata.SetWeight(0, 1.0f);
        Bodydata.SetTransform(0, Target);
        BodyAim.GetComponent<MultiAimConstraint>().data.sourceObjects = Bodydata;

        Player.GetComponent<RigBuilder>().Build();

        GameObject Aim = new GameObject("Aim");
        Aim.transform.parent = Rig.transform;
        Aim.AddComponent<MultiAimConstraint>();
        Aim.GetComponent<MultiAimConstraint>().data.constrainedObject = Player.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).
            GetChild(0).GetChild(0).GetChild(0);
        Aim.GetComponent<MultiAimConstraint>().data.aimAxis = MultiAimConstraintData.Axis.X_NEG;
        Aim.GetComponent<MultiAimConstraint>().data.upAxis = MultiAimConstraintData.Axis.Y_NEG;
        var Aimdata = Aim.GetComponent<MultiAimConstraint>().data.sourceObjects;
        Aimdata = new WeightedTransformArray(1);
        Aimdata.SetWeight(0, 1.0f);
        Aimdata.SetTransform(0, Target);
        Aim.GetComponent<MultiAimConstraint>().data.sourceObjects = Aimdata;

        Player.GetComponent<RigBuilder>().Build();

        GameObject SecondHandGrabWeapon = new GameObject("SecondHandGrabWeapon");
        SecondHandGrabWeapon.transform.parent = Rig.transform;
        SecondHandGrabWeapon.AddComponent<TwoBoneIKConstraint>();
        SecondHandGrabWeapon.GetComponent<TwoBoneIKConstraint>().weight = 1.0f;
        SecondHandGrabWeapon.GetComponent<TwoBoneIKConstraint>().data.root = Player.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0);
        SecondHandGrabWeapon.GetComponent<TwoBoneIKConstraint>().data.mid = Player.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).
            GetChild(0).GetChild(0);
        SecondHandGrabWeapon.GetComponent<TwoBoneIKConstraint>().data.tip = Player.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).
            GetChild(0).GetChild(0).GetChild(0);

        GameObject grabTarget = new GameObject("SecondHandGrabWeapon_target");
        grabTarget.transform.parent = Player.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).
            GetChild(0).GetChild(0).GetChild(0);
        grabTarget.transform.position = new Vector3(-0.141000003f, 0.349000007f, 0.0270000007f);
        grabTarget.transform.eulerAngles = new Vector3(3.25567222f, 170.530273f, 158.903412f);
        grabTarget.transform.localScale = new Vector3(0.0999999791f, 0.0999999791f, 0.0999999866f);

        GameObject hintTarget = new GameObject("SecondHandGrabWeapon_hint");
        hintTarget.transform.parent = Player.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).
            GetChild(0).GetChild(0).GetChild(0);
        hintTarget.transform.position = new Vector3(-9.09439087f, 12.3656187f, -7.37642956f);
        hintTarget.transform.eulerAngles = new Vector3(312.55481f, 65.0677643f, 187.059296f);
        hintTarget.transform.localScale = new Vector3(0.0999999791f, 0.0999999791f, 0.0999999866f);

        SecondHandGrabWeapon.GetComponent<TwoBoneIKConstraint>().data.target = Player.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).
            GetChild(0).GetChild(0).GetChild(0).GetChild(5);
        SecondHandGrabWeapon.GetComponent<TwoBoneIKConstraint>().data.hint = Player.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).
            GetChild(0).GetChild(0).GetChild(0).GetChild(6);
    }

    
    void Update()
    {
        
    }
}
