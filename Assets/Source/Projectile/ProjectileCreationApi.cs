using System;
using System.Collections.Generic;
using KMath;


namespace Projectile
{
    public class ProjectileCreationApi
    {
        // Start is called before the first frame update

        private int CurrentIndex;
        private ProjectileProperties[] PropertiesArray;

        private Dictionary<string, int> NameToID;

        public ProjectileCreationApi()
        {
            NameToID = new Dictionary<string, int>();
            PropertiesArray = new ProjectileProperties[1024];
            for (int i = 0; i < PropertiesArray.Length; i++)
            {
                PropertiesArray[i] = new ProjectileProperties();
            }
            CurrentIndex = -1;
        }

        public ProjectileProperties Get(int Id)
        {
            if (Id >= 0 && Id < PropertiesArray.Length)
            {
                return PropertiesArray[Id];
            }

            return new ProjectileProperties();
        }

        public ref ProjectileProperties GetRef(int Id)
        {
            return ref PropertiesArray[Id];
        }

        public ProjectileProperties Get(string name)
        {
            int value;
            bool exists = NameToID.TryGetValue(name, out value);
            if (exists)
            {
                return Get(value);
            }

            return new ProjectileProperties();
        }

        public void Create(int Id)
        {
            while (Id >= PropertiesArray.Length)
            {
                Array.Resize(ref PropertiesArray, PropertiesArray.Length * 2);
            }

            CurrentIndex = Id;
            if (CurrentIndex != -1)
                PropertiesArray[CurrentIndex].PropertiesId = CurrentIndex;
        }

        public void SetName(string name)
        {
            if (CurrentIndex == -1) return;

            if (!NameToID.ContainsKey(name))
            {
                NameToID.Add(name, CurrentIndex);
            }

            PropertiesArray[CurrentIndex].Name = name;
        }

        public void SetSpriteId(int SpriteId)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
                PropertiesArray[CurrentIndex].SpriteId = SpriteId;
        }

        public void SetSize(Vec2f size)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
                PropertiesArray[CurrentIndex].Size = size;
        }

        public void SetAnimation(Animation.AnimationType animationType)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].HasAnimation = true;
                PropertiesArray[CurrentIndex].AnimationType = animationType;
            }
        }

        public void SetStartVelocity(float startVelocity)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
                PropertiesArray[CurrentIndex].StartVelocity = startVelocity;
        }

        public void SetRamp(float maxVelocity)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].Flags |= ProjectileProperties.ProjFlags.CanRamp;
                PropertiesArray[CurrentIndex].MaxVelocity = maxVelocity;
            }
        }

        public void SetLinearDrag(float linearDrag, float cutOff)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].Flags |= ProjectileProperties.ProjFlags.HasLinearDrag;
                PropertiesArray[CurrentIndex].LinearDrag = linearDrag;
                PropertiesArray[CurrentIndex].LinearCutOff = cutOff;
            }
        }

        public void SetRampAcceleration(float acceleration)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
                PropertiesArray[CurrentIndex].RampAcceleration = acceleration;
        }

        public void SetDeltaRotation(float deltaRotation)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
                PropertiesArray[CurrentIndex].DeltaRotation = deltaRotation;
        }

        public void SetAffectedByGravity()
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
                PropertiesArray[CurrentIndex].Flags |= ProjectileProperties.ProjFlags.AffectedByGravity;
        }

        public void SetBounce(float value)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].Flags |= ProjectileProperties.ProjFlags.CanBounce;
                PropertiesArray[CurrentIndex].BounceValue = value;

            }
        }

        public void End()
        {
            if (PropertiesArray[CurrentIndex].MaxVelocity < PropertiesArray[CurrentIndex].StartVelocity)
                PropertiesArray[CurrentIndex].MaxVelocity = PropertiesArray[CurrentIndex].StartVelocity;

            CurrentIndex = -1;
        }

        // Sheets
        public int OreSpriteSheet;
        public int GrenadeSpriteSheet;

        // ID's
        public int OreIcon;
        public int GrenadeSpriteId;
        public int GrenadeSprite5;

        public void InitializeResources()
        {
            OreSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Items\\Ores\\Gems\\Hexagon\\gem_hexagon_1.png", 16, 16);
            GrenadeSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Projectiles\\Grenades\\Grenade\\Grenades1.png", 16, 16);
            GrenadeSprite5 = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Projectiles\\Grenades\\Grenade\\Grenades5.png", 16, 16);

            OreIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(OreSpriteSheet, 0, 0, Enums.AtlasType.Particle);
            GrenadeSpriteId = GameState.SpriteAtlasManager.CopySpriteToAtlas(GrenadeSpriteSheet, 0, 0, Enums.AtlasType.Particle);
            GrenadeSprite5 = GameState.SpriteAtlasManager.CopySpriteToAtlas(GrenadeSprite5, 0, 0, Enums.AtlasType.Particle);

            GameState.ProjectileCreationApi.Create((int)Enums.ProjectileType.Arrow);
            GameState.ProjectileCreationApi.SetName("arrow");
            GameState.ProjectileCreationApi.SetSpriteId(OreIcon);
            GameState.ProjectileCreationApi.SetSize(new Vec2f(0.5f, 0.5f));
            GameState.ProjectileCreationApi.SetStartVelocity(20.0f);
            GameState.ProjectileCreationApi.SetAffectedByGravity();
            GameState.ProjectileCreationApi.End();

            GameState.ProjectileCreationApi.Create((int)Enums.ProjectileType.Rocket);
            GameState.ProjectileCreationApi.SetName("rocket");
            GameState.ProjectileCreationApi.SetSpriteId(GrenadeSpriteId);
            GameState.ProjectileCreationApi.SetSize(new Vec2f(0.5f, 0.5f));
            GameState.ProjectileCreationApi.SetStartVelocity(20.0f);
            GameState.ProjectileCreationApi.SetRamp(40f);
            GameState.ProjectileCreationApi.SetRampAcceleration(4.0f);
            GameState.ProjectileCreationApi.End();

            GameState.ProjectileCreationApi.Create((int)Enums.ProjectileType.GasGrenade);
            GameState.ProjectileCreationApi.SetName("gas_grenade");
            GameState.ProjectileCreationApi.SetSpriteId(GrenadeSprite5);
            GameState.ProjectileCreationApi.SetSize(new Vec2f(0.5f, 0.5f));
            GameState.ProjectileCreationApi.SetStartVelocity(15.0f);
            GameState.ProjectileCreationApi.SetLinearDrag(8f, 0f);
            GameState.ProjectileCreationApi.SetBounce(0.4f);
            GameState.ProjectileCreationApi.SetAffectedByGravity();
            GameState.ProjectileCreationApi.End();

            GameState.ProjectileCreationApi.Create((int)Enums.ProjectileType.Grenade);
            GameState.ProjectileCreationApi.SetName("grenade");
            GameState.ProjectileCreationApi.SetSpriteId(GrenadeSpriteId);
            GameState.ProjectileCreationApi.SetSize(new Vec2f(0.5f, 0.5f));
            GameState.ProjectileCreationApi.SetStartVelocity(15.0f);
            GameState.ProjectileCreationApi.SetAffectedByGravity();
            GameState.ProjectileCreationApi.End();

            GameState.ProjectileCreationApi.Create((int)Enums.ProjectileType.Bullet);
            GameState.ProjectileCreationApi.SetName("bullet");
            GameState.ProjectileCreationApi.SetSpriteId(OreIcon);
            GameState.ProjectileCreationApi.SetSize(new Vec2f(0.1f, 0.1f));
            GameState.ProjectileCreationApi.SetStartVelocity(25.0f);
            GameState.ProjectileCreationApi.SetLinearDrag(0.73f, 0.01f);
            GameState.ProjectileCreationApi.SetAffectedByGravity();
            GameState.ProjectileCreationApi.End();

            GameState.ProjectileCreationApi.Create((int)Enums.ProjectileType.FragGrenade);
            GameState.ProjectileCreationApi.SetName("frag-grenade");
            GameState.ProjectileCreationApi.SetSpriteId(GrenadeSpriteId);
            GameState.ProjectileCreationApi.SetDeltaRotation(180.0f);
            GameState.ProjectileCreationApi.SetSize(new Vec2f(0.5f, 0.5f));
            GameState.ProjectileCreationApi.SetStartVelocity(20.0f);
            GameState.ProjectileCreationApi.SetAffectedByGravity();
            GameState.ProjectileCreationApi.End();

            GameState.ProjectileCreationApi.Create((int)Enums.ProjectileType.Flare);
            GameState.ProjectileCreationApi.SetName("Flare");
            GameState.ProjectileCreationApi.SetSpriteId(GrenadeSprite5);
            GameState.ProjectileCreationApi.SetSize(new Vec2f(0.5f, 0.5f));
            GameState.ProjectileCreationApi.SetStartVelocity(15.0f);
            GameState.ProjectileCreationApi.SetLinearDrag(8f, 0f);
            GameState.ProjectileCreationApi.SetBounce(0.4f);
            GameState.ProjectileCreationApi.SetAffectedByGravity();
            GameState.ProjectileCreationApi.End();
        }
    }

}
