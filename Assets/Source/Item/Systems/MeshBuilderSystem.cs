﻿namespace Item
{
    public class MeshBuilderSystem
    {
        public Utility.FrameMesh Mesh;

        public void Initialize(UnityEngine.Material material, UnityEngine.Transform transform, int drawOrder = 0)
        {
            Mesh = new Utility.FrameMesh("ItemsGameObject", material, transform,
                GameState.SpriteAtlasManager.GetSpriteAtlas(Enums.AtlasType.Particle), drawOrder);
        }

        public void UpdateMesh()
        {            
            Mesh.Clear();
            int index = 0;

            ItemParticleEntity[] items = GameState.Planet.EntitasContext.itemParticle.GetEntities();
            foreach (var entity in items)
            {
                ItemProperties proprieties = GameState.ItemCreationApi.GetItemProperties(entity.itemType.Type);

                int SpriteID = proprieties.SpriteID;
                UnityEngine.Vector4 textureCoords = GameState.SpriteAtlasManager.GetSprite(SpriteID, Enums.AtlasType.Particle).TextureCoords;


                float x, y;
                if (entity.hasItemDrawPosition2D)
                {
                    x = entity.itemDrawPosition2D.Position.X;
                    y = entity.itemDrawPosition2D.Position.Y;
                }
                else
                {
                    if (entity.hasItemPhysicsState)
                    {
                        x = entity.itemPhysicsState.Position.X;
                        y = entity.itemPhysicsState.Position.Y;
                    }
                    else
                    {
                        continue;
                    }
                }

                float w = ItemProperties.Width;
                float h = ItemProperties.Height;

                if (!Utility.ObjectMesh.isOnScreen(x, y))
                    continue;

                // Update UVs
                Mesh.UpdateUV(textureCoords, (index) * 4);
                // Update Vertices
                Mesh.UpdateVertex((index * 4), x, y, w, h);
                index++;
            }
        }
    }
}
