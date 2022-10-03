using UnityEngine.UIElements;

namespace Utility
{
    /// <summary>
    /// Expose properties that a custom control represents and other functional aspects 
    /// of its behavior as UXML properties, and expose properties that affect the look of 
    /// a custom control as USS properties.
    /// Source: https://docs.unity3d.com/2021.3/Documentation/Manual/UIE-create-custom-controls.html
    /// </summary>
    public class SplitView : TwoPaneSplitView
    {
        public new class UxmlFactory : UxmlFactory<SplitView, UxmlTraits> { }
    }
}
