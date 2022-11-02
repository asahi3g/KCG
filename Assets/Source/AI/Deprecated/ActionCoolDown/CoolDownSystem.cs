namespace ActionCoolDown
{
    public class CoolDownSystem
    {
        float currentTime;

        public void SetCoolDown(Enums.NodeType type, int agentID, float time)
        {
            var entity = GameState.Planet.EntitasContext.actionCoolDown.CreateEntity();
            entity.AddActionCoolDown(type, agentID);
            entity.AddActionCoolDownTime(currentTime + time);
        }

        public bool InCoolDown(Enums.NodeType type, int agentID)
        {
            var coolDownList = GameState.Planet.EntitasContext.actionCoolDown.GetEntitiesWithActionCoolDownAgentID(agentID);
            foreach (var coolDown in coolDownList)
            {
                if (coolDown.actionCoolDown.TypeID == type)
                    return true;
            }

            return false;
        }

        public void Update(float deltaTime)
        {
            currentTime += deltaTime;

            ActionCoolDownEntity[] coolDownList = GameState.Planet.EntitasContext.actionCoolDown.GetEntities();
            for (int i = 0; i < coolDownList.Length; i++)
            {
                if (coolDownList[i].actionCoolDownTime.EndTime < currentTime)
                {
                    coolDownList[i].Destroy();
                }
            }

        }
    }
}
