//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ContextMatcherGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class MechMatcher {

    public static Entitas.IAllOfMatcher<MechEntity> AllOf(params int[] indices) {
        return Entitas.Matcher<MechEntity>.AllOf(indices);
    }

    public static Entitas.IAllOfMatcher<MechEntity> AllOf(params Entitas.IMatcher<MechEntity>[] matchers) {
          return Entitas.Matcher<MechEntity>.AllOf(matchers);
    }

    public static Entitas.IAnyOfMatcher<MechEntity> AnyOf(params int[] indices) {
          return Entitas.Matcher<MechEntity>.AnyOf(indices);
    }

    public static Entitas.IAnyOfMatcher<MechEntity> AnyOf(params Entitas.IMatcher<MechEntity>[] matchers) {
          return Entitas.Matcher<MechEntity>.AnyOf(matchers);
    }
}