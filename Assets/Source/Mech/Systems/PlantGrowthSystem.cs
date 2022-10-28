//imports UnityEngine

using KMath;
using System.Collections.Generic;
using System;

namespace Mech
{
    public class PlantGrowthSystem
    {
        public void Update()
        {
            List<MechEntity> lights = new List<MechEntity>();

            ref var planet = ref GameState.Planet;
            for (int i = 0; i < planet.MechList.Length; i++)
            {
                MechEntity mech = planet.MechList.Get(i);
                if (mech.mechType.mechType == Enums.MechType.Light)
                    lights.Add(mech);
            }

            for (int i = 0; i < planet.MechList.Length; i++)
            {
                MechEntity plant = planet.MechList.Get(i);

                if (plant.GetProperties().Group != Enums.MechGroup.Plant)
                    continue;

                if (plant.mechPlant.PlantGrowth >= 100)
                    continue;

                int lightLevel = 0;
                for (int j = 0;  j< lights.Count; j++)
                {
                    if (Vec2f.Distance(lights[j].mechPosition2D.Value, plant.mechPosition2D.Value) < 10.0f)
                        lightLevel++;
                }

                if (plant.mechPlant.WaterLevel > 0 && lightLevel > 0)
                {
                    plant.mechPlant.WaterLevel = Math.Max(plant.mechPlant.WaterLevel - UnityEngine.Time.deltaTime * 0.4f, 0);
                    plant.mechPlant.PlantGrowth = Math.Min(plant.mechPlant.PlantGrowth + (UnityEngine.Time.deltaTime * 5f), plant.mechPlant.GrowthTarget);
                }

                if (plant.mechPlant.PlantGrowth > 50 && (plant.mechPlant.PlantGrowth - 100) < -0.005f)
                {
                    plant.mechSprite2D.SpriteId = plant.GetProperties().Stage2Sprite;
                    plant.mechSprite2D.Size = plant.GetProperties().Stage2SpriteSize;
                    continue;
                }
                if ((plant.mechPlant.PlantGrowth - 100) > -0.005f)
                {
                    plant.mechSprite2D.SpriteId = plant.GetProperties().Stage3Sprite;
                    plant.mechSprite2D.Size = plant.GetProperties().Stage2SpriteSize;
                    continue;
                }
            }
        }
    }
}
