using KMath;

namespace Gui
{

    public class GuiElement
    {

        public GuiElement Parent = null;
        public GuiElement[] Children; // dynamic array
        public int ChildrenCount;

        public Vec2f OffsetFromParent;
        public Vec2f Dimensions;
        public Vec2f Scale;

        public Vec2f DrawPosition;
        public Vec2f DrawDimensions;
        public Vec2f DrawScale;

        public bool MouseOverlapLastFrame = false;
        public bool MouseIsHeld = false;
        
        public virtual void OnClicked() {}

        public virtual void OnMouseHeld() {}
        public virtual void OnMouseEntered() {}
        public virtual void OnMouseExit() {}


        public virtual void PostPositionAndScaleUpdate(GuiElement parent) {}
        public void UpdatePositionAndScale(GuiElement parent)
        {
            if (parent != null)
            {
                DrawPosition = parent.DrawPosition + OffsetFromParent;
                DrawScale = parent.DrawScale * Scale;
            }
            else
            {
                DrawPosition = OffsetFromParent;
                DrawScale = Scale;
            }

            DrawDimensions = Dimensions * DrawScale;

            PostPositionAndScaleUpdate(parent);

            for(int i = 0; i < ChildrenCount; i++)
            {
                Children[i].UpdatePositionAndScale(this);
            }
        }

        public virtual void Update(GuiElement parent)
        {
            UnityEngine.Vector3 mousePosition = UnityEngine.Input.mousePosition;

            bool mouseClick = UnityEngine.Input.GetMouseButtonDown(0);
            bool mouseRelease = UnityEngine.Input.GetMouseButtonUp(0);
            

            bool mouseInside = Collisions.Collisions.PointOverlapRect(mousePosition.x, mousePosition.y, DrawPosition.X, DrawPosition.X + DrawDimensions.X, 
            DrawPosition.Y, DrawPosition.Y + DrawDimensions.Y);
            if (mouseInside && !MouseOverlapLastFrame)
            {
                OnMouseEntered();
            }

            if (!mouseInside && MouseOverlapLastFrame)
            {
                OnMouseExit();
                MouseIsHeld = false;
            }

            if (mouseClick && mouseInside && !MouseIsHeld)
            {
                MouseIsHeld = true;
            }

            if (mouseRelease && mouseInside && MouseIsHeld)
            {
                MouseIsHeld = false;
                OnMouseEntered();
                OnClicked();
            }

            if (MouseIsHeld)
            {
                OnMouseHeld();
            }



            MouseOverlapLastFrame = mouseInside;


            for(int i = 0; i < ChildrenCount; i++)
            {
                Children[i].Update(this);
            }
        }

        public virtual void Draw(GuiElement parent) 
        {
            for(int i = 0; i < ChildrenCount; i++)
            {
                Children[i].Draw(this);
            }
        }


        public GuiElement(Vec2f offsetFromParent, Vec2f dimensions)
        {
            Children = new GuiElement[4];
            ChildrenCount = 0;
            OffsetFromParent = offsetFromParent;
            Dimensions = dimensions;
            Scale = new Vec2f(1.0f, 1.0f);
        }

        public void AddChild(GuiElement child)
        {
            if (ChildrenCount + 1 >= Children.Length)
            {
                System.Array.Resize(ref Children, Children.Length + 16);
            }

            Children[ChildrenCount++] = child;
            child.Parent = this;
        }

        public void LayoutCentered()
        {
            if (Parent != null)
            {
                OffsetFromParent = Parent.Dimensions / 2.0f - Dimensions / 2.0f;
            }
        }

        public void LayoutLeft()
        {
            if (Parent != null)
            {
                OffsetFromParent.X = Parent.Dimensions.X / 4.0f - Dimensions.X / 2.0f;
                OffsetFromParent.Y = Parent.Dimensions.Y / 2.0f - Dimensions.Y / 2.0f;
            }
        }
    }
}