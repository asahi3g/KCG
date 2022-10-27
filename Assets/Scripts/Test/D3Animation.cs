//imports UnityEngine

using Animancer;

public class D3Animation : UnityEngine.MonoBehaviour
{

    public static int HumanoidCount = 1;
    UnityEngine.GameObject[] HumanoidArray;

    UnityEngine.AnimationClip IdleAnimationClip ;
    UnityEngine.AnimationClip RunAnimationClip ;
    UnityEngine.AnimationClip WalkAnimationClip ;
    UnityEngine.AnimationClip GolfSwingClip;


    AnimancerComponent[] AnimancerComponentArray;

    void Start()
    {

        // get the 3d model from the scene
        //GameObject humanoid = GameObject.Find("DefaultHumanoid");

        // load the 3d model from file
        UnityEngine.GameObject prefab = Engine3D.AssetManager.Singelton.GetModel(Engine3D.ModelType.Stander);

        HumanoidArray = new UnityEngine.GameObject[HumanoidCount];
        AnimancerComponentArray = new AnimancerComponent[HumanoidCount];

        for(int i = 0; i < HumanoidCount; i++)
        {
            HumanoidArray[i] = Instantiate(prefab);
            HumanoidArray[i].transform.position = new UnityEngine.Vector3(i, 0.0f, 0.0f);

            UnityEngine.Vector3 eulers = HumanoidArray[i].transform.rotation.eulerAngles;
            HumanoidArray[i].transform.rotation = UnityEngine.Quaternion.Euler(0, eulers.y + 90, 0);

        }


        


        // create an animancer object and give it a reference to the Animator component
        for(int i = 0; i < HumanoidCount; i++)
        {
            UnityEngine.GameObject animancerComponent = new UnityEngine.GameObject("AnimancerComponent", typeof(AnimancerComponent));
            // get the animator component from the game object
            // this component is used by animancer
            AnimancerComponentArray[i] = animancerComponent.GetComponent<AnimancerComponent>();
            AnimancerComponentArray[i].Animator = HumanoidArray[i].GetComponent<UnityEngine.Animator>();
        }

        
        // load some animation clips from disk
         IdleAnimationClip = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.Idle);
         RunAnimationClip = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.Run);
         WalkAnimationClip = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.Walk);
         GolfSwingClip = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.Flip);


        // play the idle animation
        for(int i = 0; i < HumanoidCount; i++)
        {
            AnimancerComponentArray[i].Play(IdleAnimationClip);
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool run = UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.R);
        bool walk = UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.W);
        bool idle = UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.I);
        bool golf = UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.G);

        for(int i = 0; i < HumanoidCount; i++)
        {
            if (run)
            {
                AnimancerComponentArray[i].Play(RunAnimationClip, 0.25f);
            }
            else if (walk)
            {
                AnimancerComponentArray[i].Play(WalkAnimationClip, 0.25f);
            }
            else if (idle)
            {
                AnimancerComponentArray[i].Play(IdleAnimationClip, 0.25f);
            }
            else if (golf)
            {
                AnimancerComponentArray[i].Play(GolfSwingClip, 0.25f);
            }
        }
    }
}
