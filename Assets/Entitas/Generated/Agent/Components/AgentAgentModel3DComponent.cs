//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class AgentEntity {

    public Agent.Model3DComponent agentModel3D { get { return (Agent.Model3DComponent)GetComponent(AgentComponentsLookup.AgentModel3D); } }
    public bool hasAgentModel3D { get { return HasComponent(AgentComponentsLookup.AgentModel3D); } }

    public void AddAgentModel3D(UnityEngine.GameObject newGameObject, UnityEngine.GameObject newLeftHand, UnityEngine.GameObject newRightHand, Agent.Model3DWeaponType newCurrentWeapon, UnityEngine.GameObject newWeapon, Animancer.AnimancerComponent newAnimancerComponent, Enums.AgentAnimationType newAnimationType, Enums.ItemAnimationSet newItemAnimationSet, KMath.Vec3f newModelScale, KMath.Vec2f newAimTarget) {
        var index = AgentComponentsLookup.AgentModel3D;
        var component = (Agent.Model3DComponent)CreateComponent(index, typeof(Agent.Model3DComponent));
        component.GameObject = newGameObject;
        component.LeftHand = newLeftHand;
        component.RightHand = newRightHand;
        component.CurrentWeapon = newCurrentWeapon;
        component.Weapon = newWeapon;
        component.AnimancerComponent = newAnimancerComponent;
        component.AnimationType = newAnimationType;
        component.ItemAnimationSet = newItemAnimationSet;
        component.ModelScale = newModelScale;
        component.AimTarget = newAimTarget;
        AddComponent(index, component);
    }

    public void ReplaceAgentModel3D(UnityEngine.GameObject newGameObject, UnityEngine.GameObject newLeftHand, UnityEngine.GameObject newRightHand, Agent.Model3DWeaponType newCurrentWeapon, UnityEngine.GameObject newWeapon, Animancer.AnimancerComponent newAnimancerComponent, Enums.AgentAnimationType newAnimationType, Enums.ItemAnimationSet newItemAnimationSet, KMath.Vec3f newModelScale, KMath.Vec2f newAimTarget) {
        var index = AgentComponentsLookup.AgentModel3D;
        var component = (Agent.Model3DComponent)CreateComponent(index, typeof(Agent.Model3DComponent));
        component.GameObject = newGameObject;
        component.LeftHand = newLeftHand;
        component.RightHand = newRightHand;
        component.CurrentWeapon = newCurrentWeapon;
        component.Weapon = newWeapon;
        component.AnimancerComponent = newAnimancerComponent;
        component.AnimationType = newAnimationType;
        component.ItemAnimationSet = newItemAnimationSet;
        component.ModelScale = newModelScale;
        component.AimTarget = newAimTarget;
        ReplaceComponent(index, component);
    }

    public void RemoveAgentModel3D() {
        RemoveComponent(AgentComponentsLookup.AgentModel3D);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class AgentMatcher {

    static Entitas.IMatcher<AgentEntity> _matcherAgentModel3D;

    public static Entitas.IMatcher<AgentEntity> AgentModel3D {
        get {
            if (_matcherAgentModel3D == null) {
                var matcher = (Entitas.Matcher<AgentEntity>)Entitas.Matcher<AgentEntity>.AllOf(AgentComponentsLookup.AgentModel3D);
                matcher.componentNames = AgentComponentsLookup.componentNames;
                _matcherAgentModel3D = matcher;
            }

            return _matcherAgentModel3D;
        }
    }
}
