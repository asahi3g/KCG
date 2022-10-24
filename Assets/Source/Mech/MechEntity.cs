using Mech;

public partial class MechEntity
{
    public MechProperties GetProperties() => GameState.MechCreationApi.Get(mechType.mechType);
}
