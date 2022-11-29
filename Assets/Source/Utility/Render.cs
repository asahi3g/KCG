using UnityEngine;
using System.Collections.Generic;
using System;
using Object = UnityEngine.Object;

namespace Utility
{

    public class Render
    {
        // Materials are used by immediate draw calls.
        Material[] Materials;

        // Update materials once every frame.
        int CurrentFrame = 0;

        // ID of next material used by immediate drawing.
        int CurrentTexMaterialID = 0;

        public void Initialize(Material material)
        {
            Materials = new Material[64];

            // Initialzie materials.
            for (int i = 0; i < 64; i++)
            {
                Materials[i] = Object.Instantiate(material);
            }
        }

        void ExpandArray()
        {
            int currentLegnth = Materials.Length;
            Array.Resize(ref Materials, Materials.Length + 64);
            for (int i = 0; i < 64; i++)
            {
                Materials[currentLegnth + i] = Object.Instantiate(Materials[0]);
            }
        }

        public void DrawFrame(ref FrameMesh frameMesh, Sprites.SpriteAtlas Atlassprite)
        {
            var mesh = frameMesh.obj.GetComponent<MeshFilter>().sharedMesh;
            mesh.Clear(); // This makes sure you never have out of bounds data.

            var mr = frameMesh.obj.GetComponent<MeshRenderer>();
            mr.sharedMaterial.SetTexture("_MainTex", Atlassprite.Texture);

            mesh.SetVertices(frameMesh.vertices);
            mesh.SetUVs(0, frameMesh.uvs);
            mesh.SetTriangles(frameMesh.triangles, 0);
            mesh.SetColors(frameMesh.colors);
        }

        public void DrawSprite(GameObject gameObject, float x, float y, float w, float h,
            Sprites.Sprite sprite)
        {
            var tex = sprite.Texture;
            var mr = gameObject.GetComponent<MeshRenderer>();
            mr.sharedMaterial.SetTexture("_MainTex", tex);

            var mf = gameObject.GetComponent<MeshFilter>();
            var mesh = mf.sharedMesh;

            gameObject.transform.position = new Vector3(x, y, 0.0f);

            List<int> triangles = new List<int>();
            List<Vector2> uvs = new List<Vector2>();
            List<Vector3> vertices = new List<Vector3>();


            var p0 = new Vector3(x, y, 0);
            var p1 = new Vector3((w), (h), 0);
            var p2 = p0;
            p2.y = p1.y;
            var p3 = p1;
            p3.y = p0.y;

            vertices.Add(p0);
            vertices.Add(p1);
            vertices.Add(p2);
            vertices.Add(p3);

            triangles.Add(0);
            triangles.Add(2);
            triangles.Add(1);
            triangles.Add(0);
            triangles.Add(1);
            triangles.Add(3);

            var uv0 = new Vector2(sprite.TextureCoords.x, sprite.TextureCoords.y + sprite.TextureCoords.w);
            var uv1 = new Vector2(sprite.TextureCoords.x + sprite.TextureCoords.z, sprite.TextureCoords.y);
            var uv2 = uv0;
            uv2.y = uv1.y;
            var uv3 = uv1;
            uv3.y = uv0.y;


            uvs.Add(uv0);
            uvs.Add(uv1);
            uvs.Add(uv2);
            uvs.Add(uv3);


            mesh.SetVertices(vertices);
            mesh.SetUVs(0, uvs);
            mesh.SetTriangles(triangles, 0);
        }

        public void DrawQuadColor(GameObject gameObject, float x, float y, float w, float h,
            Color color)
        {
            var mr = gameObject.GetComponent<MeshRenderer>();
            mr.sharedMaterial.color = color;

            var mf = gameObject.GetComponent<MeshFilter>();
            var mesh = mf.sharedMesh;

            gameObject.transform.position = new Vector3(x, y, 0.0f);

            List<int> triangles = new List<int>();
            List<Vector3> verticies = new List<Vector3>();

            var p0 = new Vector3(x, y, 0);
            var p1 = new Vector3((w), (h), 0);
            var p2 = p0;
            p2.y = p1.y;
            var p3 = p1;
            p3.y = p0.y;

            verticies.Add(p0);
            verticies.Add(p1);
            verticies.Add(p2);
            verticies.Add(p3);

            triangles.Add(0);
            triangles.Add(2);
            triangles.Add(1);
            triangles.Add(0);
            triangles.Add(1);
            triangles.Add(3);

            mesh.SetVertices(verticies);
            mesh.SetTriangles(triangles, 0);
        }


        public void DrawString(GameObject gameObject, float x, float y, float characterSize, string label, int fontSize,
            Color color, int sortOrder)
        {
            var textMesh = gameObject.GetComponent<TextMesh>();
            var mr = gameObject.GetComponent<MeshRenderer>();
            mr.sortingOrder = sortOrder;

            gameObject.transform.position = new Vector2(x, y);
            textMesh.text = label;
            textMesh.characterSize = characterSize;
            textMesh.fontSize = fontSize;
            textMesh.color = color;
        }

        // These functions draw immediately on screen. 
        // It doesn't work inside OnUpdate, because camera clear screen before drawing.
        public void DrawSpriteNow(float x, float y, float w, float h, Sprites.Sprite sprite)
        {
            DrawGlSprite(x, y, w, h, sprite);
        }

        public void DrawQuadColorNow(float x, float y, float w, float h, Color color)
        {
            DrawGlQuad(x, y, w, h, color);
        }

        // These functions should only be used inside OnGUI
        // We use GUI functions because of bugs with GL.Push and GL.Pop functions.
        // [x, y] = (0, 0) is lower left coner of the screen.
        // [x, y] = (Screen.Width, ScreenHeight) is upper right coner of the screen.
        public void DrawSpriteGui(float x, float y, float w, float h, Sprites.Sprite sprite)
        {
            y += h;
            y = Screen.height - y;
            Rect pos = new Rect(x, y, w, h);

            Vector4 texCoord = sprite.TextureCoords;
            Rect textCoord = new Rect(texCoord.x, texCoord.y + texCoord.w, texCoord.z, -texCoord.w);

            GUI.DrawTextureWithTexCoords(pos, sprite.Texture, textCoord);
        }

        public void DrawQuadColorGui(float x, float y, float w, float h, Color color)
        {
            y += h;
            y = Screen.height - y;

            Rect pos = new Rect(x, y, w, h);

            Texture2D texture = new Texture2D(1, 1);
            texture.SetPixel(0, 0, color);
            texture.Apply();

            GUI.DrawTexture(pos, texture);
        }

        public void DrawStringGui(float x, float y, float w, float h, string label, int fontSize = 16,
            TextAnchor alignment = TextAnchor.UpperLeft, Color color = default(Color))
        {
            y += h;
            y = Screen.height - y;

            Rect pos = new Rect(x, y, w, h);

            GUI.contentColor = color;
            GUI.skin.label.font = (Font) Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
            GUI.skin.label.fontSize = fontSize;
            GUI.skin.label.alignment = alignment;
            GUI.Label(pos, label);
        }

         public void DrawStringGui(float x, float y, float w, float h, string label, Font font, int fontSize = 16,
            TextAnchor alignment = TextAnchor.UpperLeft, Color color = default(Color))
        {
            y += h;
            y = Screen.height - y;

            Rect pos = new Rect(x, y, w, h);

            GUI.contentColor = color;
            GUI.skin.label.font = font;
            GUI.skin.label.fontSize = fontSize;
            GUI.skin.label.alignment = alignment;
            GUI.Label(pos, label);
        }


        // Helper Functions.
        private void DrawGlSprite(float x, float y, float w, float h,
            Sprites.Sprite sprite)
        {
            if (Time.frameCount != CurrentFrame) // Todo: make this atomic.
            {
                CurrentFrame = Time.frameCount;
                CurrentTexMaterialID = 0;
            }

            if (CurrentTexMaterialID >= Materials.Length)
                ExpandArray();

            Vector4 texCoord = sprite.TextureCoords;
            var uv0 = new Vector2(texCoord.x, texCoord.y + texCoord.w);
            var uv2 = new Vector2(texCoord.x + texCoord.z, texCoord.y);
            var uv1 = uv0;
            uv1.y = uv2.y;
            var uv3 = uv2;
            uv3.y = uv0.y;

            var mat = Materials[CurrentTexMaterialID++];
            mat.SetTexture("_MainTex", sprite.Texture);
            mat.SetPass(0);

            GL.Begin(GL.QUADS);

            GL.TexCoord2(uv0.x, uv0.y);
            GL.Vertex3(x, y, 0);

            GL.TexCoord2(uv1.x, uv1.y);
            GL.Vertex3(x, (y + h), 0);

            GL.TexCoord2(uv2.x, uv2.y);
            GL.Vertex3(x + w, y + h, 0);

            GL.TexCoord2(uv3.x, uv3.y);
            GL.Vertex3((x + w), y, 0);

            GL.End();
        }

        private void DrawGlQuad(float x, float y, float w, float h, Color color)
        {
            if (Time.frameCount != CurrentFrame) // Todo: make this atomic.
            {
                CurrentFrame = Time.frameCount;
                CurrentTexMaterialID = 0;
            }

            if (CurrentTexMaterialID >= Materials.Length)
                ExpandArray();

            var mat = Materials[CurrentTexMaterialID++];
            mat.SetColor("_Color", color);

            mat.SetPass(0);
            GL.Begin(GL.QUADS);
            GL.Color(color);

            GL.Vertex3(x, y, 0);
            GL.Vertex3(x, (y + h), 0);
            GL.Vertex3(x + w, y + h, 0);
            GL.Vertex3((x + w), y, 0);

            GL.End();
        }

        // Create Unity Sprite from PNG and add it to Sprite Atlas Manager
        public Sprite CreateUnitySprite(string path, int width, int height, Enums.AtlasType atlasType)
        {
            var iconSheet = GameState.SpriteLoader.GetSpriteSheetID(path, width, height);
            int iconID = GameState.SpriteAtlasManager.CopySpriteToAtlas(iconSheet, 0, 0, atlasType);
            var iconSpriteData = new byte[width * height * 4];
            GameState.SpriteAtlasManager.GetSpriteBytes(iconID, iconSpriteData, atlasType);
            var iconTex = Texture.CreateTextureFromRGBA(atlasType.ToString(), iconSpriteData, width, height);
            return Sprite.Create(iconTex, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f));
        }
        
        // Get sprite from SpriteAtlasManager and create Unity Sprite from it
        public Sprite CreateUnitySprite(int iconID, int width, int height, Enums.AtlasType atlasType)
        {
            var iconSpriteData = new byte[width * height * 4];
            GameState.SpriteAtlasManager.GetSpriteBytes(iconID, iconSpriteData, atlasType);
            var iconTex = Texture.CreateTextureFromRGBA(atlasType.ToString(), iconSpriteData, width, height);
            return Sprite.Create(iconTex, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f));
        }
        
        // Get sprite from TileSpriteAtlasManager and create Unity Sprite from it
        public Sprite CreateUnitySprite(int iconID, int width, int height)
        {
            var iconSpriteData = new byte[width * height * 4];
            
            GameState.TileSpriteAtlasManager.GetSpriteBytes(iconID, iconSpriteData);
            var iconTex = Texture.CreateTextureFromRGBA(iconID.ToString(), iconSpriteData, width, height);
            return Sprite.Create(iconTex, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f));
        }
    }
}
