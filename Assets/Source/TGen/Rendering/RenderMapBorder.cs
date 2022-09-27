using UnityEngine;
using KMath;
using System.Collections.Generic;

namespace TGen
{
    public class RenderMapBorder
    {
        public Utility.FrameMesh Mesh;

        private Color borderColor = new Color(1F, 0F, 0F, 1F);

        private float borderThickness = 0.1F;

        public void Initialize(Material material, Transform transform, int w, int h, int drawOrder = 0)
        {
            Mesh = new Utility.FrameMesh("MapBorderGameObject", material, transform,
                GameState.SpriteAtlasManager.GetSpriteAtlas(Enums.AtlasType.Generic), drawOrder);

            var mr = Mesh.obj.GetComponent<MeshRenderer>();
            mr.sharedMaterial.color = borderColor;

            var mf = Mesh.obj.GetComponent<MeshFilter>();
            var mesh = mf.sharedMesh;

            List<int> triangles = new List<int>();
            List<Vector3> verticies = new List<Vector3>();

            AddQuad(ref verticies, ref triangles, 0F, 0F, w, borderThickness);
            AddQuad(ref verticies, ref triangles, 0F, 0F, borderThickness, h);

            AddQuad(ref verticies, ref triangles, 0F, h, w, borderThickness);
            AddQuad(ref verticies, ref triangles, w, 0F, borderThickness, h);

            mesh.SetVertices(verticies);
            mesh.SetTriangles(triangles, 0);
        }

        private void AddQuad(ref List<Vector3> vertices, ref List<int> triangles, float x, float y, float w, float h)
        {
            Vec2f topLeft = new Vec2f(x, y + h);
            Vec2f BottomLeft = new Vec2f(x, y);
            Vec2f BottomRight = new Vec2f(x + w, y);
            Vec2f TopRight = new Vec2f(x + w, y + h);

            var p0 = new Vector3(BottomLeft.X, BottomLeft.Y, 0);
            var p1 = new Vector3(TopRight.X, TopRight.Y, 0);
            var p2 = new Vector3(topLeft.X, topLeft.Y, 0);
            var p3 = new Vector3(BottomRight.X, BottomRight.Y, 0);

            vertices.Add(p0);
            vertices.Add(p1);
            vertices.Add(p2);
            vertices.Add(p3);

            triangles.Add(vertices.Count - 4);
            triangles.Add(vertices.Count - 2);
            triangles.Add(vertices.Count - 3);
            triangles.Add(vertices.Count - 4);
            triangles.Add(vertices.Count - 3);
            triangles.Add(vertices.Count - 1);
        }

    }
}