//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ContextMatcherGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class FloatingTextMatcher {

    public static Entitas.IAllOfMatcher<FloatingTextEntity> AllOf(params int[] indices) {
        return Entitas.Matcher<FloatingTextEntity>.AllOf(indices);
    }

    public static Entitas.IAllOfMatcher<FloatingTextEntity> AllOf(params Entitas.IMatcher<FloatingTextEntity>[] matchers) {
          return Entitas.Matcher<FloatingTextEntity>.AllOf(matchers);
    }

    public static Entitas.IAnyOfMatcher<FloatingTextEntity> AnyOf(params int[] indices) {
          return Entitas.Matcher<FloatingTextEntity>.AnyOf(indices);
    }

    public static Entitas.IAnyOfMatcher<FloatingTextEntity> AnyOf(params Entitas.IMatcher<FloatingTextEntity>[] matchers) {
          return Entitas.Matcher<FloatingTextEntity>.AnyOf(matchers);
    }
}