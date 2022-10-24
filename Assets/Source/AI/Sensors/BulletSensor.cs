using Planet;
using System.Collections.Generic;
using System;
using Enums;
using Item;

namespace AI.Sensor
{
    public class BulletSensor : SensorBase
    {
        public override SensorType Type { get { return SensorType.BulletInClip; } }

        public override List<Tuple<string, Type>> GetBlackboardEntries()
        {
            List<Tuple<string, Type>> blackboardEntries = new List<Tuple<string, Type>>()
            {
                CreateEntry("HasBulletInClip", typeof(bool)),
            };
            return blackboardEntries;
        }

        public override void Update(AgentEntity agent, in SensorEntity sensor, ref BlackBoard blackBoard, ref PlanetState planet)
        {
            int HasBulletInClip = sensor.EntriesID[0];
            ItemInventoryEntity item = agent.GetItem(ref planet);
            ItemProprieties itemProperty = GameState.ItemCreationApi.Get(item.itemType.Type);

            if (itemProperty.Group == ItemGroups.Gun)
            {
                if (item.hasItemFireWeaponClip)
                {
                    if (item.itemFireWeaponClip.NumOfBullets == 0)
                    {
                        blackBoard.Set(HasBulletInClip, false);
                        return;
                    }
                }
                blackBoard.Set(HasBulletInClip, true);
                return;
            }
            blackBoard.Set(HasBulletInClip, false); // Weapon not equipped.
        }
    }
}
