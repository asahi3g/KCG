//imports UnityEngine

using KMath;
using PlanetTileMap;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

namespace Utility
{
    public struct FrameMesh
    {
        public UnityEngine.GameObject obj;
        public List<UnityEngine.Vector3> vertices;
        public List<UnityEngine.Vector2> uvs;

        public List<UnityEngine.Color> colors;
        public List<int> triangles;

        public List<UnityEngine.Vector2> lightPositions;
        public List<UnityEngine.Vector4> lightColors;

        public FrameMesh(string name, UnityEngine.Material material, UnityEngine.Transform transform, Sprites.SpriteAtlas Atlassprite, int drawOrder = 0)
        {
            obj = new UnityEngine.GameObject(name, typeof(UnityEngine.MeshFilter), typeof(UnityEngine.MeshRenderer));
            obj.transform.SetParent(transform);

            var mat = UnityEngine.Object.Instantiate(material);


            mat.SetTexture("_MainTex", Atlassprite.Texture);

            var mesh = new UnityEngine.Mesh
            {
                indexFormat = UnityEngine.Rendering.IndexFormat.UInt32
            };

            var mf = obj.GetComponent<UnityEngine.MeshFilter>();
            mf.sharedMesh = mesh;
            var mr = obj.GetComponent<UnityEngine.MeshRenderer>();

            UnityEngine.Debug.Log("FRAME_MESH: " + name);

            var Material = new UnityEngine.Material(UnityEngine.Shader.Find("Unlit/ShaderTest"));

            mr.sharedMaterial = Material;

            mr.sortingOrder = drawOrder;

            // Todo: prealocate lists.
            vertices = new List<UnityEngine.Vector3>();
            triangles = new List<int>();
            uvs = new List<UnityEngine.Vector2>();
            colors = new List<UnityEngine.Color>();
            lightPositions = new List<UnityEngine.Vector2>();
            lightColors = new List<UnityEngine.Vector4>();
            //SetLightPosition(new Vector3(1, 0, 1));
        }

        public static void SetLightPosition(UnityEngine.Material material, List<UnityEngine.Vector2> positions, List<UnityEngine.Vector4> colors, Transform tx = null)
        {
            var lightCount = positions.Count;
            material.SetVector("_KCG_LightCount", new Vector4(lightCount, lightCount, lightCount, lightCount));

            for (int i = 0; i < positions.Count; i++)
            {
                string name = "_KCG_LightPosition" + i.ToString();

                var worldPos = new Vector3(positions[i].x, positions[i].y, 0);
  	            material.SetVector(name, new Vector4(worldPos.x, worldPos.y, worldPos.z, 1));
                if (i >= 15)
                {
                    break;
                }
            }
            for (int i = 0; i < colors.Count; i++)
            {
                string name = "_KCG_LightColor" + i.ToString();
  	            material.SetVector(name, new Vector4(colors[i].x, colors[i].y, colors[i].z, 1));
                if (i >= 15)
                {
                    break;
                }
            }

        }

        public void SetLightPosition(Transform tx = null)
        {
            SetLightPosition(this.obj.GetComponent<UnityEngine.MeshRenderer>().sharedMaterial, lightPositions, lightColors, tx);
        }

        public void Clear()
        {
            vertices.Clear();
            uvs.Clear();
            triangles.Clear();
            colors.Clear();
            lightPositions.Clear();
            lightColors.Clear();
        }


        private Vec2f RotatePoint(Vec2f pos, float angle) {
            float tmpX = pos.X;

            float s = (float)System.Math.Sin((double)angle);
            float c = (float)System.Math.Cos((double)angle);

            // rotate point
            return new Vec2f(tmpX * c - pos.Y * s, tmpX * s + pos.Y * c);
        }

        public void UpdateVertex(int index, float x, float y, float w, float h, bool light = false, float r = 0, float g = 0, float b = 0, float a = 0, float angle = 0)
        {
            angle = angle * 3.14f / 180.0f;
            Vec2f topLeft = new Vec2f(0, h);
            Vec2f BottomLeft = new Vec2f(0, 0);
            Vec2f BottomRight = new Vec2f(w, 0);
            Vec2f TopRight = new Vec2f(w, h);

            if (angle != 0)
            {
                topLeft = RotatePoint(topLeft, angle);
                BottomLeft = RotatePoint(BottomLeft, angle);
                BottomRight = RotatePoint(BottomRight, angle);
                TopRight = RotatePoint(TopRight, angle);
            }

            var p0 = new UnityEngine.Vector3(BottomLeft.X + x, BottomLeft.Y + y, 0);
            var p1 = new UnityEngine.Vector3(TopRight.X + x, TopRight.Y + y, 0);
            var p2 = new UnityEngine.Vector3(topLeft.X + x, topLeft.Y + y, 0);
            var p3 = new UnityEngine.Vector3(BottomRight.X + x, BottomRight.Y + y, 0);

            var center = p0 + (p1 - p0) * 0.5f;

            if (light)
            {
                lightPositions.Add(new UnityEngine.Vector2(center.x, center.y));
                lightColors.Add(new UnityEngine.Color(r, g, b, a));
            }

            int triangleIndex = vertices.Count;

            vertices.Add(p0);
            vertices.Add(p1);
            vertices.Add(p2);
            vertices.Add(p3);

            triangles.Add(triangleIndex);
            triangles.Add(triangleIndex + 2);
            triangles.Add(triangleIndex + 1);
            triangles.Add(triangleIndex);
            triangles.Add(triangleIndex + 1);
            triangles.Add(triangleIndex + 3);

            colors.Add(new UnityEngine.Color(r, g, b, a));
            colors.Add(new UnityEngine.Color(r, g, b, a));
            colors.Add(new UnityEngine.Color(r, g, b, a));
            colors.Add(new UnityEngine.Color(r, g, b, a));
        }


        public void UpdateVertex(Vec2f[] verts, float x, float y, float angle = 0)
        {
            angle = angle * 3.14f / 180.0f;

            if (angle != 0)
            {
                for(int i = 0; i < verts.Length; i++)
                {
                    verts[i] = RotatePoint(verts[i], angle);
                }
            }

            int triangleIndex = vertices.Count;

            for(int i = 0; i < verts.Length; i++)
            {
                vertices.Add(new UnityEngine.Vector3(verts[i].X + x, verts[i].Y + y, 0));
                triangles.Add(triangleIndex + i);
                colors.Add(new UnityEngine.Color(1.0f, 1.0f, 1.0f, 1.0f));
            }
        }

        public void UpdateUV(UnityEngine.Vector4 textureCoords, int index)
        {
            var uv0 = new UnityEngine.Vector2(textureCoords.x, textureCoords.y + textureCoords.w);
            var uv1 = new UnityEngine.Vector2(textureCoords.x + textureCoords.z, textureCoords.y);
            var uv2 = uv0; uv2.y = uv1.y;
            var uv3 = uv1; uv3.y = uv0.y;

            uvs.Add(uv0);
            uvs.Add(uv1);
            uvs.Add(uv2);
            uvs.Add(uv3);
        }

        public void UpdateColor(int index, UnityEngine.Color color, bool light = false)
        {
            colors.Add(color);
            colors.Add(color);
            colors.Add(color);
            colors.Add(color);

            if (light)
                lightColors.Add(color);

        }

        public void UpdateUV(Vec2f[] coords)
        {
            for(int i = 0; i < coords.Length; i++)
            {
                uvs.Add(new UnityEngine.Vector2(coords[i].X, coords[i].Y));
            }
        }
    }


    internal static class ObjectMesh
    {
        //FIX: Do UnityEngine.CreateMesh, not using UnityEngine

        public static UnityEngine.GameObject CreateObjectMesh(UnityEngine.Transform parent, string name,
                     int sortingOrder, UnityEngine.Material material)
        {
            var go = new UnityEngine.GameObject(name, typeof(UnityEngine.MeshFilter), typeof(UnityEngine.MeshRenderer));
            go.transform.SetParent(parent);

            var mesh = new UnityEngine.Mesh
            {
                indexFormat = UnityEngine.Rendering.IndexFormat.UInt32
            };

            var mf = go.GetComponent<UnityEngine.MeshFilter>();
            mf.sharedMesh = mesh;

            var mat = UnityEngine.Object.Instantiate(material);
            var mr = go.GetComponent<UnityEngine.MeshRenderer>();

            mr.sharedMaterial = mat;
            mr.sortingOrder = sortingOrder;

            return go;
        }

        public static UnityEngine.GameObject CreateEmptyObjectMesh()
        {
            var go = new UnityEngine.GameObject("sprite", typeof(UnityEngine.MeshFilter), typeof(UnityEngine.MeshRenderer));

            var mesh = new UnityEngine.Mesh
            {
                indexFormat = UnityEngine.Rendering.IndexFormat.UInt32
            };

            var mf = go.GetComponent<UnityEngine.MeshFilter>();
            mf.sharedMesh = mesh;
            var mr = go.GetComponent<UnityEngine.MeshRenderer>();

            return go;
        }

        public static UnityEngine.GameObject CreateObjectText(UnityEngine.Transform parent, UnityEngine.Vector2 pos, string name, int sortingOrder)
        {
            var go = new UnityEngine.GameObject(name, typeof(UnityEngine.TextMesh));
            go.transform.SetParent(parent);
            go.GetComponent<UnityEngine.Transform>().position = pos;

            var textMesh = go.GetComponent<UnityEngine.TextMesh>();
            textMesh.anchor = UnityEngine.TextAnchor.LowerLeft;
            UnityEngine.Font ArialFont = (UnityEngine.Font)UnityEngine.Resources.GetBuiltinResource(typeof(UnityEngine.Font), "Arial.ttf");
            textMesh.font = ArialFont;

            var mr = go.GetComponent<UnityEngine.MeshRenderer>();
            mr.sharedMaterial = ArialFont.material;
            mr.sortingOrder = sortingOrder;

            return go;
        }

        public static UnityEngine.GameObject CreateEmptyTextGameObject(string name = "sprite")
        {
            var go = new UnityEngine.GameObject(name, typeof(UnityEngine.TextMesh));

            var textMesh = go.GetComponent<UnityEngine.TextMesh>();
            textMesh.anchor = UnityEngine.TextAnchor.LowerLeft;
            UnityEngine.Font ArialFont = (UnityEngine.Font)UnityEngine.Resources.GetBuiltinResource(typeof(UnityEngine.Font), "Arial.ttf");
            textMesh.font = ArialFont;

            var mr = go.GetComponent<UnityEngine.MeshRenderer>();
            mr.sharedMaterial = ArialFont.material;

            return go;
        }

        public static bool isOnScreen(float x, float y)
        {
            var posMax = UnityEngine.Camera.main.ScreenToWorldPoint(new UnityEngine.Vector3(UnityEngine.Screen.safeArea.xMax, UnityEngine.Screen.safeArea.yMax, 0));
            var posMin = UnityEngine.Camera.main.ScreenToWorldPoint(new UnityEngine.Vector3(UnityEngine.Screen.safeArea.xMin, UnityEngine.Screen.safeArea.yMin, 0));
            if (x > posMax.x ||
                x < posMin.x - 1.0f ||
                y > posMax.y ||
                y < posMin.y - 1.0f)
            {
                return false;
            }

            return true;
        }
    }
}
