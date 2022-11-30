namespace Enums
{
    // Used by inventory system to impose restriction.
    public enum ItemGroupType
    {
        Error = -1,
        None = 0,           // Does nothing, resources, stackable
        Helmet = 1,
        Armour,
        Gloves,
        Ring,
        Belt,
        Dye,
        Gun,
        Weapon,
        Potion,
        Mech,
        BuildTools
    }
}
